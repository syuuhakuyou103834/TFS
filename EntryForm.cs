using CCWin;
using CCWin.SkinClass;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Spreadsheet;
//using DocumentFormat.OpenXml.Wordprocessing;

using MathWorks.MATLAB.NET.Arrays;
using Recipe_LibraryNative;  //20230710_PeaktoPeak change Library Name

//using SharpCifs.Util.Sharpen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestCom;
using Thin_Film_Thickness.Forms;
using ZedGraph;

namespace Thin_Film_Thickness
{
    public partial class StageForm : CCSkinMain
    {
        public enum OperatingStateEnumeration
        {
            Unknown = 0,
            Idle_MPre = 1,
            Busy = 2,
            Measuring = 3,
            Measure_Complete = 4,
            Idle_MPost = 5,
            Error = 9,
        }

        public int currentStatus = (int)OperatingStateEnumeration.Unknown;
        public string Root_path = @"C:\ThinFilmThickness";
        private StreamWriter thicknessWriter;

        //stage用
        private double[][] order;
        private int orderIndex;
        readonly int timeInterval = 50;    //200
        private BackgroundWorker workerForOrigin = new BackgroundWorker();
        private BackgroundWorker workerForHome = new BackgroundWorker();
        private BackgroundWorker workerForEjection = new BackgroundWorker();
        private BackgroundWorker workerForMeasure = new BackgroundWorker();
        private BackgroundWorker workerForStgMove = new BackgroundWorker(); //20231026 Stage Move
        private BackgroundWorker workerForAutoMeasSet = new BackgroundWorker(); //20240105 AutoMeasure

        private string Stage_Size = "8inch";

        public double Current_MechPointX = 0;
        public double Current_MechPointY = 0;
        public double Next_MechPointX = 0;
        public double Next_MechPointY = 0;
        public double Prev_MechPointX = 0;
        public double Prev_MechPointY = 0;
        public double Stage_LimitPointX = 0;
        public double Stage_LimitPointY = 0;

        public double Measure_HomePointX = 0;
        public double Measure_HomePointY = 0;
        public double Measure_MaxLimitX = 0;
        public double Measure_MaxLimitY = 0;
        public double Measure_MinLimitX = 0;
        public double Measure_MinLimitY = 0;
        public double Measure_RefPointX = 0;
        public double Measure_RefPointY = 0;
        public double Measure_DarkPointX = 0;
        public double Measure_DarkPointY = 0;

        public double Current_MeasPointX = 0;
        public double Current_MeasPointY = 0;
        public double Next_MeasPointX = 0;
        public double Next_MeasPointY = 0;
        public double Prev_MeasPointX = 0;
        public double Prev_MeasPointY = 0;

        public static bool bMoving = false;
        public static bool bWaitRecv = false;
        public bool bStageMoving
        {
            get { return Label_information.Text.Equals("Stage moving..."); }
        }

        //spectrometer用
        private readonly Dictionary<ushort, ushort> smoothPix = new Dictionary<ushort, ushort>();
        private static IntPtr hMainWnd;
        private int deviceHandle;
        private uint measurementIndex;
        private ushort nrPixels;
        private ushort startPixel;
        private ushort stopPixel;
        private Avaspec.PixelArrayType m_Lambda = new Avaspec.PixelArrayType();
        private Avaspec.PixelArrayType m_Spectrum = new Avaspec.PixelArrayType();
        private Avaspec.PixelArrayType reference_Spectrum = new Avaspec.PixelArrayType();
        private double[][] theoreticalR;
        private Avaspec.PixelArrayType dark_Spectrum = new Avaspec.PixelArrayType();
        private double[][] interDark;
        private double[][] interRef;
        public double[][] source;
        private double[][] interSample;
        private StreamWriter swForRawData;
        private DateTime startDateTime;
        private uint l_Time;

        //zedgraph用
        private double minWave = 440;
        private double maxWave = 850;
        private PointPairList refList = new PointPairList();
        private PointPairList darkList = new PointPairList();
        private PointPairList samList = new PointPairList();
        private PointPairList reflectanceList = new PointPairList();
        private PointPairList fittingList = new PointPairList();
        private LineItem refCurve;
        private LineItem darkCurve;
        private LineItem samCurve;
        private LineItem reflectanceCurve;
        private LineItem fittingCurve;

        //fitting
        MatlabDll fitting;
        private string strPreFilmType;

        //20231229 TPC Server Communicatiion(MPS)
        Server_TCP TPCforMPC;
        //20230218 Meas csv > Excel Convwert
        MeasXYT_forExcel MeasExcel;
        public bool b_MeasAuto;
        public static bool b_MeasFinished = true;
        public string AutoFilePath = "";
        public string AutoFileName = "";
        public string AutoMapFilePath = "";
        public string AutoMapFileName = "";
        public string str_Wafer_ID = "";
        public string str_LotNum = "";
        public bool AutoMPre = false;
        public bool AutoMPost = false;
        public bool b_MeasStop = false; //20241022 Measure error stop

        private string RecipeFile_Path = @"Z:\TFS\Mrecipe\Recipe_Read.csv";
        private string MeasureFileFolder = @"Z:\TFS";
        private string MapFolderPath = @"Z:\TFS\MMap";
        private int MeasErrCnt; //20240222 Errcnt
        //private string RecipeFile_Path = @"\\192.168.10.31\Measure\TFS\Mrecipe\Recipe_Read.csv";
        //private string MeasureFileFolder = @"\\192.168.10.31\Measure\TFS";
        //private string MapFolderPath = @"\\192.168.10.31\Measure\TFS\MMap";

        //FV-Aligner
        public static FVA_Main FVA;//20240803-1YZQ
        public bool b_operateAlignment;
        public bool b_AlignmentInitialPos;
        public bool b_AlignmentCorPos;
        private bool b_RecipewithAlignment;

        private double PreAlign_InitX;
        private double PreAlign_InitY;
        private double Align_InitX;
        private double Align_InitY;

        private double X_AlignTol;
        private double Y_AlignTol;
        private double X_AlignCorrection = 0;
        private double Y_AlignCorrection = 0;
        private double X_AlignCorrectionorg = 0;
        private double Y_AlignCorrectionorg = 0;
        private double X_AngleCorrection = 0;
        private double Y_AngleCorrection = 0;
        private double X_Devicepitch;
        private double Y_Devicepitch;
        private int X_Deviceshot;
        private int Y_Deviceshot;

        private int Pattern_Num;

        private DateTime Pat_StartTime;
        //20241105 Focus Unit
        public MCM_MPHCtrl MCM_Ctrl;

        //Objective Lens Slider
        ObjLensChg GIP_Com;

        private DateTime tick_MeastimeoutErrchk;
        private DateTime pre_tick_MeastimeoutErrchk;
        private double Err_pasttime;
        private double Meas_timeout = 120;

        #region 20240803-1YZQ
        public static double dOrgX = 0;
        public static double dOrgY = 0;
        public static double dX_Grad = 0;
        public static double dY_Grad = 0;
        public static double dX_Pitch = 1;
        public static double dY_Pitch = 1;
        public static double dX_Init = 0;
        public static double dY_Init = 0;
        public static int nX_Shot = 1;
        public static int nY_Shot = 1;
        #endregion

        public StageForm()
        {
            InitializeComponent();

            workerForOrigin.DoWork += new DoWorkEventHandler(checkOrigin);
            workerForHome.DoWork += new DoWorkEventHandler(checkHome);
            workerForEjection.DoWork += new DoWorkEventHandler(checkEjection);
            workerForMeasure.DoWork += new DoWorkEventHandler(checkStageStatus);
            workerForStgMove.DoWork += new DoWorkEventHandler(checkStageMove);  //20231026 StageMove
            workerForAutoMeasSet.DoWork += new DoWorkEventHandler(checkAutoMeasSet);    //20240105 AutoMeasure

            workerForOrigin.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CompleteOrigin);
            workerForHome.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CompleteHome);
            workerForEjection.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CompleteEjection);
            workerForMeasure.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CompleteMoving);
            workerForStgMove.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CompleteMoving);  //20231026 StageMove
            workerForAutoMeasSet.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CompleteAutoMeasSetting);    //20240105 AutoMeasure

            if (!Directory.Exists(Root_path))
            {
                Directory.CreateDirectory(Root_path);
            }

            if (CConfig.GetBool(E_MacCfg.DebugMode))
            {
                logTestDebugToolStripMenuItem.Visible = true;
                cmdTestDebugToolStripMenuItem.Visible = true;
                btn_PatternAlign.Visible = true;
                btnAngleCorrection.Visible = true;
            }
            else
            {
                logTestDebugToolStripMenuItem.Visible = false;
                cmdTestDebugToolStripMenuItem.Visible = false;
                btn_PatternAlign.Visible = false;
                btnAngleCorrection.Visible = false;
                tlpOperate.RowStyles[1].Height = 0;
            }


            #region SETTING

            DialogResult dr = MessageBox.Show("Do you want to change the recipe setting first?", "Recipe setting", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {
                RecipeLoadSave();
            }
            #endregion
        }

        private void checkOrigin(object sender, DoWorkEventArgs e)
        {
            e.Result = checkStatus(workerForOrigin, e);
        }

        private void checkHome(object sender, DoWorkEventArgs e)
        {
            e.Result = checkStatus(workerForHome, e);
        }

        private void checkEjection(object sender, DoWorkEventArgs e)
        {
            e.Result = checkStatus(workerForEjection, e);
        }
        //20231020_Stage Move
        private void checkStageStatus(object sender, DoWorkEventArgs e)
        {
            e.Result = checkStatus(workerForMeasure, e);
        }

        private void checkStageMove(object sender, DoWorkEventArgs e)
        {
            e.Result = checkStatus(workerForStgMove, e);
        }

        //20240105 AutoMeasure
        private void checkAutoMeasSet(object sender, DoWorkEventArgs e)
        {
            e.Result = checkMeasSetStatus(workerForAutoMeasSet, e);
        }

        private int checkStatus(object sender, DoWorkEventArgs e)
        {

            DisableBtn();

            while (true)
            {
                StageWrite("!:" + "\r\n");
                Thread.Sleep(timeInterval);
                if (ReadTxtReceived().Equals("R"))
                {
                    DebugLog("StageStatus,RecvOK");
                    break;
                }
            }
            Debug.Print("checkStatus pass," + DateTime.Now.ToString());
            //if (currentStatus == (int)OperatingStateEnumeration.Idle) EnableBtn();
            if (currentStatus != (int)OperatingStateEnumeration.Measuring && currentStatus != (int)OperatingStateEnumeration.Busy)
                EnableBtn();
            //currentStatus = (int)OperatingStateEnumeration.Unknown;//20240803-2YZQ
            return -1;
        }

        //20240105 AutoMeasure
        private int checkMeasSetStatus(object sender, DoWorkEventArgs e)
        {
            //stage check
            while (true)
            {
                StageWrite("!:" + "\r\n");
                Thread.Sleep(timeInterval);
                if (ReadTxtReceived().Equals("R")) break;
            }

            if (b_MeasAuto == false ||
                str_LotNum == "" || str_Wafer_ID == "" ||
                AutoMapFilePath == "" || AutoFileName == ""
                )
            {
                currentStatus = (int)OperatingStateEnumeration.Error;
            }
            return -1;
        }

        //20231229 TCP
        public int Get_CurrentStatus()
        {
            int status = 0;
            status = currentStatus;
            return status;
        }

        public void Update_TPCRcvdata(string str_rcv)
        {
            txt_rcvdata.Text = txt_rcvdata.Text + Environment.NewLine + str_rcv;
        }

        public void Update_TPCSenddata(string str_send)
        {
            txt_senddata.Text = txt_senddata.Text + str_send;
        }

        private void CompleteOrigin(object sender, RunWorkerCompletedEventArgs e)
        {
            Label_information.Text = "ステージ機械原点復帰完了です。";
            Current_MechPointX = 0;
            Current_MechPointY = 0;
            Current_MeasPointX = 0;
            Current_MeasPointY = 0;

            Thread.Sleep(timeInterval / 2);

            Next_MeasPointX = 0;
            Next_MeasPointY = 0;

            //Next_MechPointX = Stage_LimitPointX / 2 + Measure_HomePointX;
            //Next_MechPointY = Stage_LimitPointY / 2 - Measure_HomePointY;
            Next_MechPointX = Stage_LimitPointX / 2 - Measure_HomePointX;
            Next_MechPointY = Stage_LimitPointY / 2 + Measure_HomePointY;

            //double dHome_x = (Stage_LimitPointX / 2 + Measure_HomePointX) * 1000;
            //double dHome_y = (Stage_LimitPointY / 2 - Measure_HomePointY) * 1000;
            double dHome_x = (Stage_LimitPointX / 2 - Measure_HomePointX) * 1000;
            double dHome_y = (Stage_LimitPointY / 2 + Measure_HomePointY) * 1000;

            string cmd = "A:W+P" + dHome_x + "+P" + dHome_y;
            //StageWrite("A:W+P100000+P100000" + "\r\n");
            StageWrite(cmd + "\r\n");
            StageWrite("G:" + "\r\n");
            Label_information.Text = "Please wait a moment while the stage is moving to the home position.";

            DebugLog("WorkStart,Home");

            workerForHome.RunWorkerAsync();
        }

        private void CompleteHome(object sender, RunWorkerCompletedEventArgs e)
        {
            Label_information.Text = "The stage is currently idle. " + "To insert/eject the Wafer, press the Tray Eject button." + Environment.NewLine + "To measure, check the coordinates on the right and press the Start button.";
            currentStatus = (int)OperatingStateEnumeration.Unknown;
            Prev_MeasPointX = Current_MeasPointX;
            Prev_MeasPointY = Current_MeasPointY;
            Prev_MechPointX = Current_MechPointX;
            Prev_MechPointY = Current_MechPointY;

            Current_MechPointX = Next_MechPointX;
            Current_MechPointY = Next_MechPointY;
            Current_MeasPointX = Next_MeasPointX;
            Current_MeasPointY = Next_MeasPointY;
            lbl_XPV2.Text = Current_MeasPointX.ToString("F3");
            lbl_Ypv2.Text = Current_MeasPointY.ToString("F3");
            TopLeftPanel3.Enabled = true;
            EnableBtn();
            Menu.Enabled = true;
        }

        private void CompleteEjection(object sender, RunWorkerCompletedEventArgs e)
        {
            Current_MechPointX = Next_MechPointX;
            Current_MechPointY = Next_MechPointY;
            Current_MeasPointX = Next_MeasPointX;
            Current_MeasPointY = Next_MeasPointY;

            lbl_XPV2.Text = Current_MeasPointX.ToString("F3");
            lbl_Ypv2.Text = Current_MeasPointY.ToString("F3");
            //Label_information.Text = "トレー移動完了です。" + Environment.NewLine + "Waferを入れ/取出してください。";
            Label_information.Text = "Tray movement is complete." + Environment.NewLine + "Please insert or remove the Wafer.";
            EnableBtn();
            if (AutoMPre == true)
            {
                currentStatus = (int)OperatingStateEnumeration.Idle_MPre;
                AutoMPre = false;
            }
            else if (AutoMPost == true)
            {
                currentStatus = (int)OperatingStateEnumeration.Idle_MPost;
                //currentStatus = (int)OperatingStateEnumeration.Measure_Complete;
                AutoMPost = false;
            }
            else
            {
                currentStatus = (int)OperatingStateEnumeration.Unknown;
                //currentStatus = (int)OperatingStateEnumeration.Idle_MPre;
            }
            //currentStatus = (int)OperatingStateEnumeration.Unknown;
        }

        private void CompleteMoving(object sender, RunWorkerCompletedEventArgs e)
        {
            Prev_MeasPointX = Current_MeasPointX;
            Prev_MeasPointY = Current_MeasPointY;
            Prev_MechPointX = Current_MechPointX;
            Prev_MechPointY = Current_MechPointY;

            Current_MechPointX = Next_MechPointX;
            Current_MechPointY = Next_MechPointY;
            Current_MeasPointX = Next_MeasPointX;
            Current_MeasPointY = Next_MeasPointY;

            lbl_XPV2.Text = Current_MeasPointX.ToString("F3");
            lbl_Ypv2.Text = Current_MeasPointY.ToString("F3");
            if (currentStatus == (int)OperatingStateEnumeration.Measuring)
            {
                //Thread.Sleep(timeInterval + 25);
                if (b_operateAlignment == false)
                    StageWrite("T:M" + "\r\n");
            }
            else
            {
                //Label_information.Text = "ステージ現在アイドル状態です。" + "Waferを入れる/取出すには、Tray Ejectボタンを押してください。" + Environment.NewLine + "測定するには、右側の座標をご確認の上Startボタンを押してください。";
                Label_information.Text = "The stage is currently idle. " + "To insert/eject the Wafer, press the Tray Eject button. " + Environment.NewLine + "To measure, check the coordinates on the right and press the Start button.";
                //if(b_MeasAuto!= true) currentStatus = (int)OperatingStateEnumeration.Unknown;

                currentStatus = (int)OperatingStateEnumeration.Unknown;
                TopLeftPanel3.Enabled = true;
                EnableBtn();
                Menu.Enabled = true;
            }
            if (b_AlignmentCorPos == true)
            {
                //FVA.FVAMMVA();
                b_AlignmentCorPos = false;
            }
            if (b_AlignmentInitialPos == true) b_AlignmentInitialPos = false;
            bMoving = false;

        }

        //20240105 AutoMeas
        private void CompleteAutoMeasSetting(object sender, RunWorkerCompletedEventArgs e)
        {
            //------- AutoMeasure Setting Completed and Measure Start --------//
            MeasErrCnt = 0;
            Err_pasttime = 0;
            //Map File Read
            ReadTxt();

            if (b_MeasAuto == false && order == null || order.Length == 0 || order[0].Length != 2)
            {
                //MessageBox.Show("Please check the coordinates, there should be 2 columns (X and Y).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                currentStatus = (int)OperatingStateEnumeration.Error;
            }

            //Reference file Read
            ReadReferenceTxt();
            for (int i = 0; i < order.Length; i++)
            {
                DataGrid.Rows[i].Cells[2].Value = null;
                DataGrid.Rows[i].Cells[3].Value = null;
                DataGrid.Rows[i].Cells[4].Value = null;
            }
            refList.Clear();
            darkList.Clear();
            samList.Clear();
            reflectanceList.Clear();
            fittingList.Clear();
            IntensityGraphControl.AxisChange();
            IntensityGraphControl.Invalidate();
            ReflectanceGraphControl.AxisChange();
            ReflectanceGraphControl.Invalidate();

            if (AutoFilePath == "" && AutoFileName == "")
            {
                currentStatus = (int)OperatingStateEnumeration.Error;
                return;
            }

            //20241029_SimFileDelete before Auto
            if (File.Exists(AutoFilePath + "\\" + AutoFileName + ".csv") == true)
            {
                File.Delete(AutoFilePath + "\\" + AutoFileName + ".csv");
            }

            string filePath = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\TempMData\\MData.csv";
            //string filePath = "\\192.168.10.31\\Measuer\\TFS\\Testlot\\MData.txt";
            //DialogResult dr = MessageBox.Show("現在選択されているレシピ: " + Properties.Settings.Default.recipeName + System.Environment.NewLine +
            //    "(屈折率@" + Properties.Settings.Default.refractiveWL + "nmが保存されます)" + System.Environment.NewLine +
            //    "名前を付けて保存", "測定データ保存先", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            //if (dr == DialogResult.Cancel) return;
            //else if (dr == DialogResult.OK)
            //{
            //filePath = InitialShowSaveFileDialog();
            //}
            //filePath = InitialShowSaveFileAuto();   //Measure File name Setting
            if (filePath == null || filePath.Length == 0) return;
            else
            {
                thicknessWriter = new StreamWriter(filePath);
            }
            //thicknessWriter.WriteLine("X".PadRight(8) + "Y".PadRight(8) + "Thickness(nm)".PadRight(16) + "FitQuality".PadRight(16) + "RefractiveIndex@" + Properties.Settings.Default.refractiveWL + "nm");
            //thicknessWriter.WriteLine("X" + "\t" + "Y" + "\t" + "Thickness(nm)" + "\t" + "FitQuality" + "\t" + "RefractiveIndex@" + Properties.Settings.Default.refractiveWL + "nm");
            thicknessWriter.WriteLine("X" + "," + "Y" + "," + "Thickness(nm)" + "," + "FitQuality" + ","
                                      + "RefractiveIndex@" + Properties.Settings.Default.refractiveWL + "nm");
            thicknessWriter.Flush();

            currentStatus = (int)OperatingStateEnumeration.Measuring;
            DisableBtn();
            Menu.Enabled = false;

            startDateTime = DateTime.Now;
            StageManager.folderPath = System.IO.Path.Combine(Root_path, startDateTime.ToString("yyyyMMdd_HHmmss"));
            Directory.CreateDirectory(StageManager.folderPath);

            string rawDataFileName = StageManager.folderPath + "\\RawData_" + startDateTime.ToString("yyyyMMdd'_'HHmmss") + ".csv";
            CreateRawDataFile(rawDataFileName);
            dinitFocPos = MCM_Ctrl.Get_curMCM_Pos();
            //Pattern Alignment
            if (b_RecipewithAlignment == true)
            {
                Label_information.Text = "During Pattern Alignment.";
                //20240924-1
                DeviceParaPreGet();

                //if (true)//CConfig.GetBool(E_MacCfg.DebugMode))//20240804-1YZQ
                //AutoPatternAlignment(false, 2, true);
                //20240924-1 
                if (cbxUseAngleCorrection.Checked && !(Pattern_Num < 1))
                {
                    double Pat_elapsedtime = 0;
                    Pat_StartTime = DateTime.Now;

                    AutoAngleCorrection();
                    if (chk_UseFocusSearch.Checked && FVA.b_Alignment_NG == true)
                    {
                        double Fstep = 0.01;
                        double step_FP = 0;
                        b_FocMove = false;

                        step_FP = dinitFocPos + 0.07;

                        do
                        {
                            DeviceParaReset();
                            step_FP = step_FP - Fstep;
                            Focus_Moving(step_FP);
                            Thread.Sleep(100);
                            b_FocMove = true;
                            AutoAngleCorrection();

                            Pat_elapsedtime = DateTime.Now.Subtract(Pat_StartTime).TotalSeconds;

                            System.Windows.Forms.Application.DoEvents();
                            if (b_MeasStop == true || Pat_elapsedtime > 600)
                                break;

                        } while (FVA.b_Alignment_NG == true && step_FP > dinitFocPos - 0.07);
                    }
                }
                else
                    AutoPatternAlignment(false, 1, true);
                //else
                //    AutoPatternAlignment(false, 1, true);
                if (FVA.b_Alignment_NG == true)
                {
                    //MessageBox.Show("Auto Alinment is failed." + Environment.NewLine + "Stop Measurment.", "Alignment NG", MessageBoxButtons.OK);
                    //currentStatus = (int)OperatingStateEnumeration.Error;
                    DeviceParaReset();
                    thicknessWriter.Close();
                    StagePort.Write("L:E" + "\r\n");
                    Thread.Sleep(100);

                    b_MeasStop = true;
                    Label_information.Text = "Stop Measurement.";
                    if (b_FocMove == true)
                    {
                        Focus_Moving(dinitFocPos);
                        b_FocMove = false;
                    }

                    if (b_MeasAuto == true)
                    {
                        currentStatus = (int)OperatingStateEnumeration.Error;
                    }
                    else
                    {
                        currentStatus = (int)OperatingStateEnumeration.Unknown;
                    }
                    Menu.Enabled = true;
                    EnableBtn();
                    b_MeasFinished = true;
                    return;
                }
                else
                {
                    PreAlign_InitX = Convert.ToDouble(txt_C1_Xinit.Text);
                    PreAlign_InitY = Convert.ToDouble(txt_C1_Yinit.Text);
                    //20240924-1 chg
                    //Align_InitX = Convert.ToDouble(lbl_XPV2.Text);
                    //Align_InitY = Convert.ToDouble(lbl_Ypv2.Text);
                    Align_InitX = Convert.ToDouble(dInitAlignX);
                    Align_InitY = Convert.ToDouble(dInitAlignY);

                    if (CConfig.GetBool(E_MacCfg.DebugMode))
                    {
                        X_AlignCorrection = dInitGapX;
                        Y_AlignCorrection = dInitGapY;
                    }
                    else
                    {
                        X_AlignCorrection = Convert.ToDouble(lbl_DispXGap.Text);
                        Y_AlignCorrection = Convert.ToDouble(lbl_DispGapY.Text);
                    }
                    X_AngleCorrection = Convert.ToDouble(txt_XGrad.Text);
                    Y_AngleCorrection = Convert.ToDouble(txt_YGrad.Text);
                    X_Devicepitch = Convert.ToDouble(txt_Xpitch.Text);
                    Y_Devicepitch = Convert.ToDouble(txt_Ypitch.Text);
                    X_Deviceshot = Convert.ToInt32(txt_XShot.Text);
                    Y_Deviceshot = Convert.ToInt32(txt_YShot.Text);
                }
            }

            PrepareSpectrometer();
            SetXYaxisScale(m_Lambda.Value[startPixel], m_Lambda.Value[stopPixel]);

            orderIndex = 0;
            measurementIndex = 0;
            int l_Res = Avaspec.AVS_Measure(deviceHandle, hMainWnd, 1);
            Debug.Print("AVS_Meas Start : " + measurementIndex);
            //if (Avaspec.ERR_SUCCESS != l_Res)
            if (l_Res != Avaspec.ERR_SUCCESS)
            {
                //MessageBox.Show("Error in AVS_Measure: " + l_Res.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                currentStatus = (int)OperatingStateEnumeration.Error;
                return;
            }
            Thread.Sleep(300);

            Label_information.Text = "Measuring...";

            double dDark_X, dDark_Y;
            switch (Properties.Settings.Default.Dark_Point)
            {
                case 1:
                    dDark_X = 178;
                    dDark_Y = 178;
                    break;
                case 2:
                    dDark_X = 22;
                    dDark_Y = 178;
                    break;
                case 3:
                    dDark_X = 178;
                    dDark_Y = 22;
                    break;
                case 4:
                    dDark_X = 22;
                    dDark_Y = 22;
                    break;
                default:
                    dDark_X = 0;
                    dDark_Y = 0;
                    break;
            }

            Next_MechPointX = dDark_X;
            Next_MechPointY = dDark_Y;
            //Next_MeasPointX = dDark_X - Stage_LimitPointX / 2 - Measure_HomePointX;
            //Next_MeasPointY = Stage_LimitPointY / 2 -dDark_Y - Measure_HomePointY;
            Next_MeasPointX = Stage_LimitPointX / 2 - dDark_X - Measure_HomePointX;
            Next_MeasPointY = dDark_Y - Stage_LimitPointY / 2 - Measure_HomePointY;
            //First for the dark measurement
            //StagePort.Write("A:W+P" + 0 * 1000 + "+P" + 100 * 1000 + "\r\n");
            StagePort.Write("A:W+P" + dDark_X * 1000 + "+P" + dDark_Y * 1000 + "\r\n");
            StagePort.Write("G:" + "\r\n");
            measurementIndex++;
            DebugLog("WorkStart2");
            workerForMeasure.RunWorkerAsync();
        }

        private void StageForm_Load(object sender, EventArgs e)
        {
            hMainWnd = this.Handle;
            this.Location = new Point(0, 0);
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            TxtPath.Text = Properties.Settings.Default.filePath;


            //TopLeftPanel3の配置
            int interval = (TopLeftPanel3.Size.Width - 800) / 5;
            int height = (TopLeftPanel3.Size.Height - 50) / 2;
            BtnEject.Location = new System.Drawing.Point(interval, height + 1);
            BtnHome.Location = new System.Drawing.Point(interval * 2 + 200, height + 1);
            BtnStart.Location = new System.Drawing.Point(interval * 3 + 400, height + 1);
            BtnStop.Location = new System.Drawing.Point(interval * 4 + 600, height + 1);

            //TopLeftPanelの配置
            interval = (TopLeftPanel.Size.Width - 667) / 4;
            height = (TopLeftPanel.Size.Height - 20) / 2;
            LabelPort.Location = new System.Drawing.Point(interval, height);
            PortComboBox.Location = new System.Drawing.Point(interval + 98, height);
            LabelTime.Location = new System.Drawing.Point(interval * 3 + 88 + 150 + 200, height);
            height = (TopLeftPanel.Size.Height - LabelElapsedTime.Height) / 2;
            LabelElapsedTime.Location = new System.Drawing.Point(interval * 3 + 88 + 150 + 200 + 119, height);
            height = (TopLeftPanel.Size.Height - 40) / 2;
            BtnConnect.Location = new System.Drawing.Point(interval * 2 + 88 + 50 + 10, height + 10);
            btnReConnect.Location = new System.Drawing.Point(interval * 2 + 88 + 75 + 10, height + 10); ;

            //slitサイズとsmoothing pixelの関係
            smoothPix.Add(10, 0);
            smoothPix.Add(25, 1);
            smoothPix.Add(50, 2);
            smoothPix.Add(100, 3);
            smoothPix.Add(200, 7);

            //一番下の状態欄をクリア
            mStatusStripStatus.Text = "";
            mStatusStripTxtReceived.Text = "";

            PortComboBox.Items.Clear();
            //20241105 Focus Unit
            cmb_FcsComPort.Items.Clear();

            string[] ports = SerialPort.GetPortNames();
            List<string> portNames = ports.ToList();
            portNames.Sort();
            foreach (string port in ports)
            {
                PortComboBox.Items.Add(port);
                cmb_FcsComPort.Items.Add(port); //20241105 Focus Unit
            }
            string defaultCom = "COM" + Properties.Settings.Default.DefaultComNum;
            string FcsdefaultCom = "COM" + Properties.Settings.Default.FcsDefaultCom;
            for (int i = 0; i < ports.Length; i++)
            {
                if (ports[i].Equals(defaultCom))
                    PortComboBox.SelectedIndex = i;
                //20241105 Focus Unit
                if (ports[i].Equals(FcsdefaultCom))
                    cmb_FcsComPort.SelectedIndex = i;
            }

            //if (ports.Length < 1)
            //{
            //    MessageBox.Show("Please check USB connection and confirm serial port from Windows Device Manager.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    Environment.Exit(0);
            //}
            //PortComboBox.SelectedIndex = 0;

            //if (TxtPath.Text != null && TxtPath.Text.Length != 0)
            //{
            //    if (System.IO.File.Exists(TxtPath.Text.Trim()))
            //    {
            //        ReadTxt();
            //    }          
            //}

            // Load FVA pitch settings from application settings
            try
            {
                if (Properties.StageSettings.Default.FVA_Pitch_X > 0)
                {
                    FVA_Main.pitch_X = Properties.StageSettings.Default.FVA_Pitch_X;
                }
                if (Properties.StageSettings.Default.FVA_Pitch_Y > 0)
                {
                    FVA_Main.pitch_Y = Properties.StageSettings.Default.FVA_Pitch_Y;
                }
                DebugLog($"FVA Settings loaded: Pitch X = {FVA_Main.pitch_X}, Pitch Y = {FVA_Main.pitch_Y}");
            }
            catch (Exception ex)
            {
                DebugLog($"Error loading FVA Settings: {ex.Message}");
            }

            InitializeZedGraph();
        }

        private void MatlabStart()
        {
            fitting = new MatlabDll();
            double[,] temp = new double[2, 1];
            temp[0, 0] = 111; temp[1, 0] = 222;
            fitting.add(1, new MWNumericArray(temp));
        }

        private void InitializeZedGraph()
        {
            //ZedGraph
            IntensityGraphControl.GraphPane.Title.Text = "Intensity";
            IntensityGraphControl.GraphPane.XAxis.Title.Text = "Wavelength (nm)";
            IntensityGraphControl.GraphPane.YAxis.Title.Text = "Counts";
            IntensityGraphControl.GraphPane.XAxis.MajorGrid.IsVisible = true;
            IntensityGraphControl.GraphPane.YAxis.MajorGrid.IsVisible = true;

            ReflectanceGraphControl.GraphPane.Title.Text = "Reflectance";
            ReflectanceGraphControl.GraphPane.XAxis.Title.Text = "Wavelength (nm)";
            ReflectanceGraphControl.GraphPane.YAxis.Title.Text = "Reflectance (%)";
            ReflectanceGraphControl.GraphPane.XAxis.MajorGrid.IsVisible = true;
            ReflectanceGraphControl.GraphPane.YAxis.MajorGrid.IsVisible = true;

            refCurve = IntensityGraphControl.GraphPane.AddCurve("Reference", refList, System.Drawing.Color.Orange, SymbolType.None);
            darkCurve = IntensityGraphControl.GraphPane.AddCurve("Dark", darkList, System.Drawing.Color.Gray, SymbolType.None);
            samCurve = IntensityGraphControl.GraphPane.AddCurve("Sample", samList, System.Drawing.Color.DarkGreen, SymbolType.None);
            reflectanceCurve = ReflectanceGraphControl.GraphPane.AddCurve("Measuring", reflectanceList, System.Drawing.Color.Blue, SymbolType.None);
            fittingCurve = ReflectanceGraphControl.GraphPane.AddCurve("Fitting", fittingList, System.Drawing.Color.Red, SymbolType.None);

            IntensityGraphControl.AxisChange();
            IntensityGraphControl.Refresh();
            ReflectanceGraphControl.AxisChange();
            ReflectanceGraphControl.Refresh();
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            string name = PortComboBox.Text.Trim();
            if (name == null || name.Length == 0)
            {
                MessageBox.Show("Serial Port could not be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            StagePort.PortName = name;

            try
            {
                StagePort.Open();
            }
            catch
            {
                MessageBox.Show("Invalid Serial Port is selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            StagePort.Write("F:M1D" + "\r\n");
            StagePort.Write("ONZ");

            StagePort.Write("D:WS2500F25000R100S2500F25000R100" + "\r\n");
            //StagePort.Write("D:WS2000F20000R100S2000F20000R100" + "\r\n");
            GraphTabControl.SelectedTab = GraphTabControl.TabPages[1];
            OpenSpectrometer();

            //測定原点および測定リミットの変更
            switch (Properties.StageSettings.Default.StageSize)
            {
                case "8inch":
                    Stage_LimitPointX = 200;
                    Stage_LimitPointY = 200;
                    break;
                case "12inch":
                    Stage_LimitPointX = 300;
                    Stage_LimitPointY = 300;
                    break;
                case "other":
                    break;
            }
            Measure_HomePointX = Properties.StageSettings.Default.X_Home;
            Measure_HomePointY = Properties.StageSettings.Default.Y_Home;

            lbl_XHomepv.Text = Measure_HomePointX.ToString();
            lbl_YHomepv.Text = Measure_HomePointY.ToString();

            Measure_MaxLimitX = Stage_LimitPointX / 2 - Measure_HomePointX;
            Measure_MaxLimitY = Stage_LimitPointY / 2 - Measure_HomePointY;

            Measure_MinLimitX = -Stage_LimitPointX / 2 - Measure_HomePointX;
            Measure_MinLimitY = -Stage_LimitPointY / 2 - Measure_HomePointY;

            StagePort.Write("H:W" + "\r\n");
            Label_information.Text = "Please wait a moment while the stage machine returns to its origin.\r\n";

            DebugLog("WorkStart,Org");
            workerForOrigin.RunWorkerAsync();
            currentStatus = (int)OperatingStateEnumeration.Busy;

            BtnConnect.Enabled = false;
            PortComboBox.Enabled = false;
            BtnStop.Enabled = true;
            TimerForRefresh.Enabled = true;

            //20231229 TCP Server Communicatiion(MPS)
            //Server_TCP TPCforMPC;
            TPCforMPC = new Server_TCP();   //これは必須
            TPCforMPC.initTCPServer();      //これも必須

            //20240420 FVAlingner connect
            FVA = new FVA_Main();
            FVA.initFVA();

            //20241105 Focus Unit
            MCM_Ctrl = new MCM_MPHCtrl();

            name = cmb_FcsComPort.Text.Trim();
            if (name == null || name.Length == 0)
            {
                MessageBox.Show("Serial Port could not be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            MCM_Ctrl.MCM_PortName = name;
            try
            {
                bool b_MCMConnect = false;
                b_MCMConnect = MCM_Ctrl.MCMConnect();
                if (b_MCMConnect == true)
                {
                    cmb_FcsComPort.Enabled = false;
                    double curPos = 0;
                    int cur_EncPos = MCM_Ctrl.int_CurMCMEnc;
                    curPos = MCM_Ctrl.Get_curMCM_Pos();
                    lbl_CurFcsPos.Text = curPos.ToString("F3");
                }
            }
            catch
            {
                MessageBox.Show("Focus Unit Serial is invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //lbl_CurFcsPos.Text = "----";
                lbl_CurFcsPos.BackColor = System.Drawing.Color.Pink;
                //return;
            }

            //20240420 Objective Lens control
            GIP_Com = new ObjLensChg();

            //20240218 csv > Excel Convert
            MeasExcel = new MeasXYT_forExcel();

            Thread th = new Thread(MatlabStart);
            th.IsBackground = true;
            th.Start();
        }

        private void OpenSpectrometer()
        {
            Avaspec.AVS_Done();
            int l_Port = Avaspec.AVS_Init(0);

            if (l_Port >= 1)
            {
                uint l_RequiredSize = 0;
                uint l_Size = ((uint)l_Port) * (uint)Marshal.SizeOf(typeof(Avaspec.AvsIdentityType));

                Avaspec.AvsIdentityType[] l_Id = new Avaspec.AvsIdentityType[l_Port];
                int l_NrDevices = Avaspec.AVS_GetList(l_Size, ref l_RequiredSize, l_Id);
                deviceHandle = Avaspec.AVS_Activate(ref l_Id[0]);
                if (Avaspec.AVS_Register(this.Handle) == false)
                    MessageBox.Show("Handle registration failed or function not supported on OS", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (Avaspec.AVS_UseHighResAdc(deviceHandle, true) != Avaspec.ERR_SUCCESS)
                    MessageBox.Show("High Res mode not supported", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Avaspec.AVS_GetNumPixels(deviceHandle, ref nrPixels);
                dark_Spectrum.Value = new double[nrPixels];
                reference_Spectrum.Value = new double[nrPixels];

                if (Avaspec.AVS_GetLambda(deviceHandle, ref m_Lambda) == Avaspec.ERR_SUCCESS)
                {
                    SetXYaxisScale(m_Lambda.Value[0], m_Lambda.Value[nrPixels - 1]);
                    StageManager.lambda = new double[nrPixels];
                    Buffer.BlockCopy(m_Lambda.Value, 0, StageManager.lambda, 0, nrPixels * sizeof(double));
                }
                else
                {
                    MessageBox.Show("Get Wavelength failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                Avaspec.AVS_Done();
                MessageBox.Show("Error Activating Spectrometer, please check USB connection!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
        }

        private void SetXYaxisScale(double minwav, double maxwav)
        {
            minWave = minwav;
            maxWave = maxwav;

            //まずはX軸
            IntensityGraphControl.GraphPane.XAxis.Scale.Min = minwav;
            IntensityGraphControl.GraphPane.XAxis.Scale.Max = maxwav;
            ReflectanceGraphControl.GraphPane.XAxis.Scale.Min = minwav;
            ReflectanceGraphControl.GraphPane.XAxis.Scale.Max = maxwav;

            //Y軸
            IntensityGraphControl.GraphPane.YAxis.Scale.Min = -1000;
            IntensityGraphControl.GraphPane.YAxis.Scale.Max = 70000;
            ReflectanceGraphControl.GraphPane.YAxis.Scale.Min = 0;
            ReflectanceGraphControl.GraphPane.YAxis.Scale.Max = 100;

            IntensityGraphControl.AxisChange();
            IntensityGraphControl.Refresh();
            ReflectanceGraphControl.AxisChange();
            ReflectanceGraphControl.Refresh();
        }

        private void StageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (currentStatus == (int)OperatingStateEnumeration.Measuring || currentStatus == (int)OperatingStateEnumeration.Busy)
            {
                var res = MessageBox.Show(this, "Stage is working, You really want to quit?", "Exit",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (res != DialogResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }
                BtnStop.PerformClick();
            }
            if (StagePort != null && StagePort.IsOpen) BtnHome.PerformClick();

            Properties.Settings.Default.filePath = TxtPath.Text;
            Properties.Settings.Default.Save();
            Properties.StageSettings.Default.Save();

            int l_Res = Avaspec.AVS_Deactivate(deviceHandle);
            Avaspec.AVS_Done();

            try
            {
                StagePort.Close();
            }
            catch
            {

            }
        }

        private void StagePort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            UpdateResponse(StagePort.ReadExisting());
        }

        private delegate void Response(string answer);
        private void UpdateResponse(string answer)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Response(UpdateResponse), answer);
                return;
            }
            mStatusStripTxtReceived.Text = answer.Trim();
        }

        private delegate void PortWrite(string command);
        private void StageWrite(string command)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new PortWrite(StageWrite), command);
                return;
            }
            try
            {
                StagePort.Write(command);
            }
            catch (Exception ex)
            {
                currentStatus = (int)OperatingStateEnumeration.Error;
            }
        }

        private delegate void Disable();
        private void DisableBtn()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Disable(DisableBtn));
                return;
            }
            BtnEject.Enabled = false;
            BtnHome.Enabled = false;
            BtnStart.Enabled = false;
        }

        private delegate void Enable();
        private void EnableBtn()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Enable(EnableBtn));
                return;
            }
            BtnEject.Enabled = true;
            BtnHome.Enabled = true;
            BtnStart.Enabled = true;
        }

        private delegate string Read();
        private string ReadTxtReceived()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Read(ReadTxtReceived));
            }
            return mStatusStripTxtReceived.Text;
        }

        private void openCoordinatesTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog myDlg = new OpenFileDialog();

            myDlg.Title = "Select the coordinate table file:";
            //myDlg.Filter = "txt files|*.txt";
            //myDlg.FilterIndex = 1;
            myDlg.Filter = "txt files|*.txt|CSV file|*.csv";
            myDlg.FilterIndex = 2;
            myDlg.InitialDirectory = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Map\\MapSample";
            myDlg.RestoreDirectory = true;

            DialogResult dret = myDlg.ShowDialog();
            if (dret == DialogResult.Cancel)
            {
                return;
            }

            TxtPath.Text = myDlg.FileName;
            myDlg.Dispose();

            ReadTxt();
        }

        private void ReadTxt()
        {
            StreamReader sr = new StreamReader(TxtPath.Text, Encoding.Default);
            string newLine;
            if ((newLine = sr.ReadLine()) == null)
            {
                MessageBox.Show("Please check the text file format, the first line should be \"X    Y\".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            string[] header = newLine.Trim().Split('\t');
            if (header.Length != 2)
            {
                MessageBox.Show("Please check the text file format, there should be only 2 columns (X and Y).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!header[0].Equals("X") || !header[1].Equals("Y"))
            {
                MessageBox.Show("Please check the text file format, the first line should only contain the header \"X    Y\".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string[] stringArray = new string[2];

            List<double[]> list = new List<double[]>();

            DataGrid.Rows.Clear();

            while ((newLine = sr.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(newLine)) break;

                stringArray = newLine.Trim().Split('\t');
                if (stringArray.Length != 2)
                {
                    MessageBox.Show("Please check the text file format, there should be only 2 columns (X and Y).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                double x = double.Parse(stringArray[0]);
                double y = double.Parse(stringArray[1]);

                //if (x > 100 || x < -100 || y > 100 || y < -100)
                if (x > Measure_MaxLimitX || x < Measure_MinLimitX ||
                    y > Measure_MaxLimitY || y < Measure_MinLimitX)
                {
                    MessageBox.Show("Please check the coordinates, none of them should be larger than 100 or smaller than -100.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                list.Add(new double[] { x, y });

                DataGrid.Rows.Add(new string[] { stringArray[0], stringArray[1], "", "" });
                DataGrid.Refresh();
            }
            sr.Close();

            #region init para get auto  //20240731-1YZQ

            order = list.ToArray();
            //if (CConfig.GetBool(E_MacCfg.DebugMode))
            //{
            //    int xShot = (int)list[0][0];
            //    int yShot = (int)list[0][1];

            //    list.RemoveAt(0);
            //    order = list.ToArray();
            //}

            double tempInitX = list[0][0];
            double tempInitY = list[0][1];
            for (int i = 0; i < list.Count; i++)
            {
                if (Math.Abs(tempInitX) > Math.Abs(list[i][0]))
                {
                    tempInitX = list[i][0];
                }
                if (Math.Abs(tempInitY) > Math.Abs(list[i][1]))
                {
                    tempInitY = list[i][1];
                }
            }

            //dOrgX = double.Parse(tempInitX.ToString());
            //dOrgY = double.Parse(tempInitY.ToString());
            dOrgX = tempInitX;
            dOrgY = tempInitY;
            txt_C1_Xinit.Text = tempInitX.ToString();
            txt_C1_Yinit.Text = tempInitY.ToString();

            #endregion

            DataGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //DataGrid.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;           
        }

        private void ReadReferenceTxt()
        {
            string refPath = System.Windows.Forms.Application.StartupPath + "\\Si100reference.txt";
            switch (Properties.Settings.Default.ReflectancePath)
            {
                case 1:
                    refPath = System.Windows.Forms.Application.StartupPath + "\\Si100reference.txt";
                    break;
                case 2:
                    refPath = System.Windows.Forms.Application.StartupPath + "\\LN100reference.txt";
                    break;
                default:
                    break;
            }
            StreamReader sr = new StreamReader(refPath, Encoding.Default);

            string newLine;
            string[] splitArray;
            List<double[]> list = new List<double[]>();

            while ((newLine = sr.ReadLine()) != null)
            {
                if (String.IsNullOrEmpty(newLine)) break;

                splitArray = newLine.Trim().Split('\t');

                list.Add(new double[] { double.Parse(splitArray[0]), double.Parse(splitArray[1]) });
            }
            sr.Close();

            theoreticalR = list.ToArray();
        }

        private bool Read_RecipeCSV()
        {
            bool b_ok = true;
            File.Copy(RecipeFile_Path, Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Temp\\Recipe_read.csv", true);

            string recipe_csvpath = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Temp\\Recipe_read.csv";
            StreamReader sr = new StreamReader(recipe_csvpath, Encoding.Default);
            string newLine;
            string string_temp;
            if ((newLine = sr.ReadLine()) == null)
            {
                return false;
            }

            //lot Folder
            string_temp = sr.ReadLine();
            AutoFilePath = string_temp;
            txt_Recipeinfo.Text = string_temp;

            //Wafer_ID
            string_temp = sr.ReadLine();
            AutoFileName = string_temp + DateTime.Now.ToString("yyyyMMDDhhmmss");// ".csv";
            txt_Recipeinfo.Text = txt_Recipeinfo.Text + Environment.NewLine + string_temp;
            //Film Type
            string_temp = sr.ReadLine();
            b_ok = Set_AutoFilmType(string_temp);
            txt_Recipeinfo.Text = txt_Recipeinfo.Text + Environment.NewLine + string_temp;
            //Map Type
            string_temp = sr.ReadLine();
            AutoMapFileName = string_temp; // + ".csv"
            //File.Copy(RecipeFile_Path, Path.GetDirectoryName(Application.ExecutablePath) + "\\Map\\" + AutoMapFileName, true);
            File.Copy(MapFolderPath + "\\" + AutoMapFileName, Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Map\\" + AutoMapFileName, true);
            AutoMapFilePath = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Map\\" + AutoMapFileName;
            sr.Close();
            txt_Recipeinfo.Text = txt_Recipeinfo.Text + Environment.NewLine + string_temp;
            TxtPath.Text = AutoMapFilePath;

            ReadTxt();

            return b_ok;
        }

        private bool Set_AutoFilmType(string temp)
        {
            bool ft_ok = true;
            strPreFilmType = temp;
            switch (temp)
            {
                case "AlScN on Si":
                    Properties.Settings.Default.recipeName = "AlScN on Si";
                    Properties.Settings.Default.Ref_Point = 2;
                    Properties.Settings.Default.Dark_Point = 4;
                    Properties.Settings.Default.ReflectancePath = 1;
                    Properties.Settings.Default.integration = 50;
                    b_RecipewithAlignment = false;
                    break;

                case "SiO2 on Si":
                    Properties.Settings.Default.recipeName = "SiO2 on Si";
                    Properties.Settings.Default.Ref_Point = 2;
                    Properties.Settings.Default.Dark_Point = 4;
                    Properties.Settings.Default.ReflectancePath = 1;
                    Properties.Settings.Default.integration = 50;
                    b_RecipewithAlignment = false;
                    break;

                case "Al2O3 on Si":
                    Properties.Settings.Default.recipeName = "Al2O3 on Si";
                    Properties.Settings.Default.Ref_Point = 2;
                    Properties.Settings.Default.Dark_Point = 4;
                    Properties.Settings.Default.ReflectancePath = 1;
                    Properties.Settings.Default.integration = 50;
                    b_RecipewithAlignment = false;
                    break;

                case "TiO2 on Si(Fitting)":
                    Properties.Settings.Default.recipeName = "TiO2 on Si";
                    Properties.Settings.Default.Ref_Point = 2;
                    Properties.Settings.Default.Dark_Point = 4;
                    Properties.Settings.Default.ReflectancePath = 1;
                    Properties.Settings.Default.integration = 50;
                    b_RecipewithAlignment = false;
                    break;

                // 20230805 Add SiO2 on LN sub(fitting)
                case "SiO2 on LN(Fitting)":
                    Properties.Settings.Default.recipeName = "SiO2 on LN";
                    Properties.Settings.Default.Ref_Point = 1;
                    Properties.Settings.Default.Dark_Point = 4;
                    Properties.Settings.Default.ReflectancePath = 2;
                    Properties.Settings.Default.integration = 100;
                    b_RecipewithAlignment = false;
                    break;

                // 20230905 Add SiO2 2micron Measure
                case "SiO2 on Si Sub_2micron":
                    Properties.Settings.Default.recipeName = "SiO2 on Si Sub_2micron";
                    Properties.Settings.Default.Ref_Point = 1;
                    Properties.Settings.Default.Dark_Point = 4;
                    Properties.Settings.Default.ReflectancePath = 1;
                    Properties.Settings.Default.integration = 50;
                    b_RecipewithAlignment = false;
                    break;

                // 20230905 Add SiO2 3micron Measure
                case "SiO2 on LN Sub_3micron":
                    Properties.Settings.Default.recipeName = "SiO2 on LN Sub_3micron";
                    Properties.Settings.Default.Ref_Point = 1;
                    Properties.Settings.Default.Dark_Point = 4;
                    Properties.Settings.Default.ReflectancePath = 1;
                    Properties.Settings.Default.integration = 100;
                    b_RecipewithAlignment = false;
                    break;

                case "SiO2(PtoP)":
                    Properties.Settings.Default.recipeName = "SiO2(PtoP)";
                    b_RecipewithAlignment = false;
                    break;
                case "SiO2(PtoP Ref:Si)":
                    Properties.Settings.Default.recipeName = "SiO2(PtoP)";
                    Properties.Settings.Default.Ref_Point = 2;
                    Properties.Settings.Default.Dark_Point = 4;
                    Properties.Settings.Default.ReflectancePath = 1;
                    Properties.Settings.Default.integration = 50;
                    b_RecipewithAlignment = false;
                    break;

                case "SiO2(PtoP Ref:LN)":
                    Properties.Settings.Default.recipeName = "SiO2(PtoP)";
                    Properties.Settings.Default.Ref_Point = 1;
                    Properties.Settings.Default.Dark_Point = 4;
                    Properties.Settings.Default.ReflectancePath = 2;
                    Properties.Settings.Default.integration = 100;
                    b_RecipewithAlignment = false;
                    break;

                case "SiO2(PtoP Ref:Si Device)":
                    Properties.Settings.Default.recipeName = "SiO2(PtoP)";
                    Properties.Settings.Default.Ref_Point = 1;
                    Properties.Settings.Default.Dark_Point = 4;
                    Properties.Settings.Default.ReflectancePath = 2;
                    Properties.Settings.Default.integration = 50;
                    b_RecipewithAlignment = true;
                    break;

                case "SiO2(PtoP Ref:LN Device)":
                    Properties.Settings.Default.recipeName = "SiO2(PtoP)";
                    Properties.Settings.Default.Ref_Point = 1;
                    Properties.Settings.Default.Dark_Point = 4;
                    Properties.Settings.Default.ReflectancePath = 2;
                    Properties.Settings.Default.integration = 100;
                    b_RecipewithAlignment = true;
                    break;

                default:
                    ft_ok = false;
                    break;

            }
            return ft_ok;
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            //if (currentStatus != (int)OperatingStateEnumeration.Idle) return;
            if (currentStatus == (int)OperatingStateEnumeration.Measuring || currentStatus == (int)OperatingStateEnumeration.Busy) return;
            if (order == null || order.Length == 0 || order[0].Length != 2)
            {
                MessageBox.Show("Please check the coordinates, there should be 2 columns (X and Y).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            b_MeasAuto = false;
            b_MeasStop = false;
            MeasErrCnt = 0;
            Err_pasttime = 0;

            Set_AutoFilmType(Properties.Settings.Default.recipeName);

            ReadReferenceTxt();
            for (int i = 0; i < order.Length; i++)
            {
                DataGrid.Rows[i].Cells[2].Value = null;
                DataGrid.Rows[i].Cells[3].Value = null;
                DataGrid.Rows[i].Cells[4].Value = null;
            }
            refList.Clear();
            darkList.Clear();
            samList.Clear();
            reflectanceList.Clear();
            fittingList.Clear();
            IntensityGraphControl.AxisChange();
            IntensityGraphControl.Invalidate();
            ReflectanceGraphControl.AxisChange();
            ReflectanceGraphControl.Invalidate();

            string filePath = "";
            DialogResult dr = MessageBox.Show("Currently selected recipe: " + Properties.Settings.Default.recipeName + System.Environment.NewLine +
                "(Refractive index @" + Properties.Settings.Default.refractiveWL + "nm will be saved)" + System.Environment.NewLine +
                "Save as", "Measurement data storage destination", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.Cancel) return;
            else if (dr == DialogResult.OK)
            {
                filePath = InitialShowSaveFileDialog();
            }

            if (filePath == null || filePath.Length == 0) return;
            else
            {
                thicknessWriter = new StreamWriter(filePath);
            }
            //thicknessWriter.WriteLine("X".PadRight(8) + "Y".PadRight(8) + "Thickness(nm)".PadRight(16) + "FitQuality".PadRight(16) + "RefractiveIndex@" + Properties.Settings.Default.refractiveWL + "nm");
            //thicknessWriter.WriteLine("X" + "," + "Y" + "," + "Thickness(nm)" + "," + "FitQuality" + "," + "RefractiveIndex@" + Properties.Settings.Default.refractiveWL + "nm");
            thicknessWriter.WriteLine("X" + "," + "Y" + "," + "Thickness(nm)" + "," + "FitQuality" + ","
                                       + "RefractiveIndex@" + Properties.Settings.Default.refractiveWL + "nm");
            thicknessWriter.Flush();

            currentStatus = (int)OperatingStateEnumeration.Measuring;

            DisableBtn();
            Menu.Enabled = false;

            //PrepareSpectrometer();
            //SetXYaxisScale(m_Lambda.Value[startPixel], m_Lambda.Value[stopPixel]);

            startDateTime = DateTime.Now;
            StageManager.folderPath = System.IO.Path.Combine(Root_path, startDateTime.ToString("yyyyMMdd_HHmmss"));
            Directory.CreateDirectory(StageManager.folderPath);

            string rawDataFileName = StageManager.folderPath + "\\RawData_" + startDateTime.ToString("yyyyMMdd'_'HHmmss") + ".csv";
            CreateRawDataFile(rawDataFileName);

            //Pattern Alignment
            if (b_RecipewithAlignment == true)
            {
                Label_information.Text = "During Pattern Alignment.";
                //20240924-1
                DeviceParaPreGet();

                //if (true)//CConfig.GetBool(E_MacCfg.DebugMode))//20240804-1YZQ
                //AutoPatternAlignment(false, 2, true);
                //20240924-1 
                if (cbxUseAngleCorrection.Checked && !(Pattern_Num < 1))
                {
                    double Pat_elapsedtime = 0;
                    Pat_StartTime = DateTime.Now;

                    AutoAngleCorrection();
                    if (chk_UseFocusSearch.Checked && FVA.b_Alignment_NG == true)
                    {
                        double Fstep = 0.01;
                        double step_FP = 0;
                        b_FocMove = false;

                        step_FP = dinitFocPos + 0.15;

                        do
                        {
                            DeviceParaReset();
                            step_FP = step_FP - Fstep;
                            Focus_Moving(step_FP);
                            Thread.Sleep(100);
                            b_FocMove = true;
                            AutoAngleCorrection();

                            Pat_elapsedtime = DateTime.Now.Subtract(Pat_StartTime).TotalSeconds;


                            System.Windows.Forms.Application.DoEvents();
                            if (b_MeasStop == true || Pat_elapsedtime > 600)
                                break;

                        } while (FVA.b_Alignment_NG == true && step_FP > dinitFocPos - 0.15);
                    }
                }
                else
                    AutoPatternAlignment(false, 1, true);
                //else
                //    AutoPatternAlignment(false, 1, true);
                if (FVA.b_Alignment_NG == true)
                {
                    //MessageBox.Show("Auto Alinment is failed." + Environment.NewLine + "Stop Measurment.", "Alignment NG", MessageBoxButtons.OK);
                    //currentStatus = (int)OperatingStateEnumeration.Error;
                    DeviceParaReset();
                    thicknessWriter.Close();
                    StagePort.Write("L:E" + "\r\n");
                    Thread.Sleep(100);

                    b_MeasStop = true;
                    Label_information.Text = "Stop Measurement.";
                    if (b_FocMove == true)
                    {
                        Focus_Moving(dinitFocPos);
                        b_FocMove = false;
                    }

                    if (b_MeasAuto == true)
                    {
                        currentStatus = (int)OperatingStateEnumeration.Error;
                    }
                    else
                    {
                        currentStatus = (int)OperatingStateEnumeration.Unknown;
                    }
                    Menu.Enabled = true;
                    EnableBtn();
                    b_MeasFinished = true;
                    return;
                }
                else
                {
                    PreAlign_InitX = Convert.ToDouble(txt_C1_Xinit.Text);
                    PreAlign_InitY = Convert.ToDouble(txt_C1_Yinit.Text);
                    //20240924-1 chg
                    //Align_InitX = Convert.ToDouble(lbl_XPV2.Text);
                    //Align_InitY = Convert.ToDouble(lbl_Ypv2.Text);
                    Align_InitX = Convert.ToDouble(dInitAlignX);
                    Align_InitY = Convert.ToDouble(dInitAlignY);

                    if (CConfig.GetBool(E_MacCfg.DebugMode))
                    {
                        X_AlignCorrection = dInitGapX;
                        Y_AlignCorrection = dInitGapY;
                    }
                    else
                    {
                        X_AlignCorrection = Convert.ToDouble(lbl_DispXGap.Text);
                        Y_AlignCorrection = Convert.ToDouble(lbl_DispGapY.Text);
                    }
                    X_AngleCorrection = Convert.ToDouble(txt_XGrad.Text);
                    Y_AngleCorrection = Convert.ToDouble(txt_YGrad.Text);
                    X_Devicepitch = Convert.ToDouble(txt_Xpitch.Text);
                    Y_Devicepitch = Convert.ToDouble(txt_Ypitch.Text);
                    X_Deviceshot = Convert.ToInt32(txt_XShot.Text);
                    Y_Deviceshot = Convert.ToInt32(txt_YShot.Text);
                }
            }

            PrepareSpectrometer();
            SetXYaxisScale(m_Lambda.Value[startPixel], m_Lambda.Value[stopPixel]);

            orderIndex = 0;
            measurementIndex = 0;
            hMainWnd = this.Handle;

            int l_Res = Avaspec.AVS_Measure(deviceHandle, hMainWnd, 1);
            Debug.Print("AVS_Meas Start : " + measurementIndex);
            //if (b_MeasAuto==false && Avaspec.ERR_SUCCESS != l_Res)
            if (l_Res != Avaspec.ERR_SUCCESS)
            {
                currentStatus = (int)OperatingStateEnumeration.Error;
                if (b_MeasAuto == false)
                {
                    MessageBox.Show("Error in AVS_Measure: " + l_Res.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            Label_information.Text = "Measuring...";

            double dDark_X, dDark_Y;
            switch (Properties.Settings.Default.Dark_Point)
            {
                case 1:
                    dDark_X = 178;
                    dDark_Y = 178;
                    break;
                case 2:
                    dDark_X = 22;
                    dDark_Y = 178;
                    break;
                case 3:
                    dDark_X = 178;
                    dDark_Y = 22;
                    break;
                case 4:
                    dDark_X = 22;
                    dDark_Y = 22;
                    break;
                default:
                    dDark_X = 0;
                    dDark_Y = 0;
                    break;
            }

            Next_MechPointX = dDark_X;
            Next_MechPointY = dDark_Y;
            //Next_MeasPointX = dDark_X - Stage_LimitPointX / 2 - Measure_HomePointX;
            //Next_MeasPointY = Stage_LimitPointY / 2 -dDark_Y - Measure_HomePointY;
            Next_MeasPointX = Stage_LimitPointX / 2 - dDark_X - Measure_HomePointX;
            Next_MeasPointY = dDark_Y - Stage_LimitPointY / 2 - Measure_HomePointY;
            //First for the dark measurement
            //StagePort.Write("A:W+P" + 0 * 1000 + "+P" + 100 * 1000 + "\r\n");
            StagePort.Write("A:W+P" + dDark_X * 1000 + "+P" + dDark_Y * 1000 + "\r\n");
            StagePort.Write("G:" + "\r\n");
            measurementIndex++;
            DebugLog("WorkStart1");
            workerForMeasure.RunWorkerAsync();
        }

        //20240105_AutoMeasure
        public void AutoMeasure(string str_para)
        {
            if (!b_MeasFinished)
                return;
            b_MeasFinished = false;

            b_MeasAuto = true;
            b_MeasStop = false;
            //AutoMPre = false;

            string[] header = str_para.Trim().Split(',');
            str_LotNum = header[0];
            AutoFilePath = MeasureFileFolder + "\\" + str_LotNum;
            str_Wafer_ID = header[1];
            AutoFileName = str_Wafer_ID; //+".csv"
            bool b_ok = Set_AutoFilmType(header[2]);
            AutoMapFileName = header[3].Replace("\r", "") + ".csv";
            AutoMapFilePath = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Map\\" + AutoMapFileName;
            if (currentStatus == (int)OperatingStateEnumeration.Measuring || currentStatus == (int)OperatingStateEnumeration.Busy) return;
            Pattern_Num = Convert.ToInt32(lbl_PatternNum.Text);
            //20240924-1_Angle correction_del
            //if (cbxUseAngleCorrection.Checked && !(Pattern_Num < 0))
            //    AutoAngleCorrection();
            TxtPath.Text = AutoMapFilePath;
            workerForAutoMeasSet.RunWorkerAsync();
        }

        private void PrepareSpectrometer()
        {
            Avaspec.MeasConfigType l_PrepareMeasData = new Avaspec.MeasConfigType();
            startPixel = Properties.Settings.Default.startIndex;
            stopPixel = Properties.Settings.Default.stopIndex;
            l_PrepareMeasData.m_StartPixel = startPixel;
            l_PrepareMeasData.m_StopPixel = stopPixel;
            l_PrepareMeasData.m_IntegrationTime = Properties.Settings.Default.integration;
            l_PrepareMeasData.m_IntegrationDelay = 0;
            l_PrepareMeasData.m_NrAverages = Properties.Settings.Default.average;
            l_PrepareMeasData.m_CorDynDark.m_Enable = 1;
            l_PrepareMeasData.m_CorDynDark.m_ForgetPercentage = 100;
            l_PrepareMeasData.m_Smoothing.m_SmoothPix = smoothPix[Properties.Settings.Default.slitSize];
            l_PrepareMeasData.m_Smoothing.m_SmoothModel = 0;
            l_PrepareMeasData.m_SaturationDetection = 0;
            l_PrepareMeasData.m_Trigger.m_Mode = 1;    // 0 for software; 1 for HW Trigger; 2 for single scan
            l_PrepareMeasData.m_Trigger.m_Source = 0;
            l_PrepareMeasData.m_Trigger.m_SourceType = 0;
            l_PrepareMeasData.m_Control.m_StrobeControl = 0;
            l_PrepareMeasData.m_Control.m_LaserDelay = 0;
            l_PrepareMeasData.m_Control.m_LaserWidth = 0;
            l_PrepareMeasData.m_Control.m_LaserWaveLength = 0;
            l_PrepareMeasData.m_Control.m_StoreToRam = 0;
            int l_Res = Avaspec.AVS_PrepareMeasure(deviceHandle, ref l_PrepareMeasData);
            if (l_Res != Avaspec.ERR_SUCCESS)
            {
                currentStatus = (int)OperatingStateEnumeration.Error;
                if (b_MeasAuto == false)
                {
                    MessageBox.Show("Error in PrepareMeasure: " + l_Res.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
        }

        private void CreateRawDataFile(string fileName)
        {
            DeleteFiles(Root_path, 60);
            swForRawData = new StreamWriter(fileName);
            string firstLine = "Index," + "ElapsedTime[sec]," + "X_Position," + "Y_Position," + "WaveLength[nm],";

            for (ushort pixel = 0; pixel < stopPixel - startPixel; pixel++)
            {
                firstLine += Math.Round(m_Lambda.Value[startPixel + pixel], 5) + ",";
            }
            firstLine += Math.Round(m_Lambda.Value[stopPixel], 5);
            swForRawData.WriteLine(firstLine);
            swForRawData.Flush();
        }

        private void DeleteFiles(string directory, int lifeCycle)
        {
            if (!Directory.Exists(directory)) return;
            string[] folders = Directory.GetDirectories(directory);
            foreach (string folder in folders)
            {
                DirectoryInfo dI = new DirectoryInfo(folder);
                TimeSpan time = DateTime.Now - dI.CreationTime;
                if (time.Days > lifeCycle)
                {
                    dI.Delete(true);
                }
            }
        }

        private string ShowSaveFileDialog()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "テキスト（スペース区切り）（*.txt）|*.txt|csv（コンマ区切り）（*.csv）|*.csv";
            sfd.FilterIndex = 2;
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                return sfd.FileName.ToString();
            }
            return "";
        }

        private string InitialShowSaveFileDialog()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            //sfd.Filter = "テキスト（スペース区切り）（*.txt）|*.txt";
            sfd.Filter = "CSV（タブ区切り）（*.csv）|*.csv";
            sfd.FilterIndex = 1;
            sfd.InitialDirectory = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\ManualData";
            //sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                return sfd.FileName.ToString();
            }
            return "";
        }

        private string InitialShowSaveFileAuto()
        {
            return "";
        }
        private void BtnEject_Click(object sender, EventArgs e)
        {
            //if (currentStatus != (int)OperatingStateEnumeration.Unknown) return;
            if (currentStatus == (int)OperatingStateEnumeration.Measuring || currentStatus == (int)OperatingStateEnumeration.Busy) return;
            double dPoint_x = 3.5;
            //double dPoint_x = Stage_LimitPointX / 2;
            double dPoint_y = Stage_LimitPointY + 0.5; //4,44

            Next_MechPointX = dPoint_x;
            Next_MechPointY = dPoint_y;

            //Next_MeasPointX = -Measure_HomePointX;
            //Next_MeasPointY = -Stage_LimitPointY / 2 - Measure_HomePointY;
            Next_MeasPointX = dPoint_x - Stage_LimitPointX / 2 - Measure_HomePointX;
            Next_MeasPointY = -dPoint_y + Stage_LimitPointY / 2 + Measure_HomePointY;

            dPoint_x = (dPoint_x) * 1000;
            dPoint_y = (dPoint_y) * 1000;
            AutoMPre = true;
            string cmd = "A:W+P" + dPoint_x + "+P" + dPoint_y;
            StagePort.Write(cmd + "\r\n");
            //StagePort.Write("A:W+P199000+P100000" + "\r\n");
            StagePort.Write("G:" + "\r\n");

            Label_information.Text = "Tray moving...";
            currentStatus = (int)OperatingStateEnumeration.Busy;

            DebugLog("WorkStart,Eject");

            workerForEjection.RunWorkerAsync();
        }

        public void MoveSendPos()
        {
            if (currentStatus == (int)OperatingStateEnumeration.Busy || currentStatus == (int)OperatingStateEnumeration.Measuring) return;

            double dPoint_x = 3.5;
            //double dPoint_x = Stage_LimitPointX / 2;
            double dPoint_y = Stage_LimitPointY + 0.5;

            Next_MechPointX = dPoint_x;
            Next_MechPointY = dPoint_y;

            //Next_MeasPointX = -Measure_HomePointX;
            //Next_MeasPointY = -Stage_LimitPointY / 2 - Measure_HomePointY;
            Next_MeasPointX = dPoint_x - Stage_LimitPointX / 2 - Measure_HomePointX; ;
            Next_MeasPointY = -dPoint_y + Stage_LimitPointY / 2 + Measure_HomePointY;

            dPoint_x = (dPoint_x) * 1000;
            dPoint_y = (dPoint_y) * 1000;

            string cmd = "A:W+P" + dPoint_x + "+P" + dPoint_y;
            StagePort.Write(cmd + "\r\n");
            //StagePort.Write("A:W+P199000+P100000" + "\r\n");
            StagePort.Write("G:" + "\r\n");
            Label_information.Text = "Tray moving...";


            DebugLog("WorkStart,SendPos");
            currentStatus = (int)OperatingStateEnumeration.Busy;
            workerForEjection.RunWorkerAsync();
        }

        public bool Read_Recipe()
        {
            //Recipe File Read
            return Read_RecipeCSV(); ;
        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            //if (currentStatus != (int)OperatingStateEnumeration.Idle) return;
            if (currentStatus == (int)OperatingStateEnumeration.Measuring || currentStatus == (int)OperatingStateEnumeration.Busy) return;

            double dHome_x = Measure_HomePointX;
            double dHome_y = Measure_HomePointY;

            Next_MeasPointX = 0;
            Next_MeasPointY = 0;
            //Next_MechPointX = dHome_x + 100;
            //Next_MechPointY = 100 - dHome_y;
            Next_MechPointX = 100 - dHome_x;
            Next_MechPointY = dHome_y + 100;

            //dHome_x = (dHome_x + 100) * 1000;
            //dHome_y = (100 - dHome_y) * 1000;
            dHome_x = (100 - dHome_x) * 1000;
            dHome_y = (dHome_y + 100) * 1000;

            string cmd = "A:W+P" + dHome_x + "+P" + dHome_y;
            //StagePort.Write("A:W+P100000+P100000" + "\r\n");
            StagePort.Write(cmd + "\r\n");
            StagePort.Write("G:" + "\r\n");
            Label_information.Text = "Move to Home Position...";

            DebugLog("WorkStart,Org");
            currentStatus = (int)OperatingStateEnumeration.Busy;
            workerForHome.RunWorkerAsync();
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            b_MeasStop = true;
            StagePort.Write("L:E" + "\r\n");
            Thread.Sleep(500);

            StopSpectrometer();
            Label_information.Text = "Stop Measurement.";
            //if (b_MeasAuto == true)
            //{
            //currentStatus = (int)OperatingStateEnumeration.Error;
            //}
            if (!b_MeasAuto)
            {
                currentStatus = (int)OperatingStateEnumeration.Unknown;
            }
            Menu.Enabled = true;
            EnableBtn();
            b_MeasFinished = true;
            DebugLog("Emergency Stop.");
        }

        public void MeasAutoStop()
        {
            b_MeasStop = true;
            StagePort.Write("L:E" + "\r\n");
            Thread.Sleep(500);

            StopSpectrometer();
            Label_information.Text = "Stop Measurement.";
            if (b_MeasAuto == true)
            {
                currentStatus = (int)OperatingStateEnumeration.Error;
            }
            else
            {
                currentStatus = (int)OperatingStateEnumeration.Unknown;
            }
            Menu.Enabled = true;
            EnableBtn();
        }

        private void StopSpectrometer()
        {
            int l_Res = Avaspec.AVS_StopMeasure(deviceHandle);
            //if (Avaspec.ERR_SUCCESS != l_Res)
            if (l_Res != Avaspec.ERR_SUCCESS)
            {
                MessageBox.Show("Error in StopMeasure: " + l_Res.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (swForRawData != null)
            {
                try
                {
                    swForRawData.Close();
                }
                catch
                {
                }
            }
            if (thicknessWriter != null)
            {
                try
                {
                    thicknessWriter.Close();
                }
                catch
                {
                }
            }
            //if (b_MeasAuto == true)
            //{
            //    currentStatus = (int)OperatingStateEnumeration.Error;
            //}
            //else
            //{
            //    currentStatus = (int)OperatingStateEnumeration.Unknown;
            //}
        }

        private void Finish_Measure()
        {
            //int l_Res = Avaspec.AVS_StopMeasure(deviceHandle);
            //if (Avaspec.ERR_SUCCESS != l_Res)
            //if (l_Res != Avaspec.ERR_SUCCESS)
            //{
            //    MessageBox.Show("Error in StopMeasure: " + l_Res.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            if (swForRawData != null)
            {
                try
                {
                    swForRawData.Close();
                }
                catch
                {
                }
            }
            if (thicknessWriter != null)
            {
                try
                {
                    thicknessWriter.Close();
                }
                catch
                {
                }
            }
        }

        private void ZedGraphClear()
        {
            samList.Clear();
            reflectanceList.Clear();
            fittingList.Clear();
            ReflectanceGraphControl.AxisChange();
            ReflectanceGraphControl.Invalidate();
        }

        protected override void WndProc(ref Message a_WinMess)
        {
            if (a_WinMess.Msg == Avaspec.WM_MEAS_READY)
            {
                DebugLog($"WinMes,{a_WinMess.Msg}");//20240914-2YZQ:Measure stoped reason check
                if ((int)a_WinMess.WParam == Avaspec.ERR_SUCCESS)
                {
                    int i_res;
                    i_res = Avaspec.AVS_GetScopeData(deviceHandle, ref l_Time, ref m_Spectrum);
                    Thread.Sleep(20);
                    //if (Avaspec.AVS_GetScopeData(deviceHandle, ref l_Time, ref m_Spectrum) == Avaspec.ERR_SUCCESS)
                    if (i_res == Avaspec.ERR_SUCCESS)
                    {
                        ZedGraphClear();
                        //measurementIndex++;
                        Err_pasttime = 0;
                        StringBuilder sb = new StringBuilder();
                        double elapsedTime = Math.Round(DateTime.Now.Subtract(startDateTime).TotalSeconds, 3);
                        DebugLog($"measurementIndex,{measurementIndex}");//20240914-2YZQ
                        if (measurementIndex == 1)
                        {
                            Debug.Print("measurementIndex == 1");
                            Thread.Sleep(1000);

                            darkList.Clear();
                            sb.Append("0,").Append(elapsedTime).Append(",").Append(Current_MeasPointX.ToString("F3")).
                                        Append(",").Append(Current_MeasPointY.ToString("F3")).Append(",Dark(ADC Counts),");
                            for (ushort pixel = 0; pixel <= stopPixel - startPixel; pixel++)
                            {
                                dark_Spectrum.Value[pixel] = m_Spectrum.Value[pixel];
                                sb.Append(Math.Round(dark_Spectrum.Value[pixel], 5)).Append(",");
                                darkList.Add(m_Lambda.Value[startPixel + pixel], dark_Spectrum.Value[pixel]);
                            }
                            sb.Remove(sb.Length - 1, 1);
                            swForRawData.WriteLine(sb.ToString());
                            swForRawData.Flush();

                            IntensityGraphControl.AxisChange();
                            IntensityGraphControl.Invalidate();

                            int res = Avaspec.AVS_Measure(deviceHandle, hMainWnd, 1);
                            Debug.Print("AVS_Meas Start : " + measurementIndex);
                            //int res = Avaspec.AVS_MeasureCallback(deviceHandle, hMainWnd, 1);
                            if (Avaspec.ERR_SUCCESS != res)
                            {
                                if (b_MeasAuto == false)
                                {
                                    MessageBox.Show("Error in AVS_Measure: " + res.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                currentStatus = (int)OperatingStateEnumeration.Error;
                                //currentStatus = (int)
                                //MessageBox.Show("Error in AVS_Measure: " + res.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            //for the reference measurement
                            //StagePort.Write("A:W+P" + 30 * 1000 + "+P" + 100 * 1000 + "\r\n");
                            double dref_X, dref_Y;
                            switch (Properties.Settings.Default.Ref_Point)
                            {
                                case 1:
                                    dref_X = 178;
                                    dref_Y = 178;
                                    break;
                                case 2:
                                    dref_X = 22;
                                    dref_Y = 178;
                                    break;
                                case 3:
                                    dref_X = 178;
                                    dref_Y = 22;
                                    break;
                                case 4:
                                    dref_X = 22;
                                    dref_Y = 22;
                                    break;
                                default:
                                    dref_X = 0;
                                    dref_Y = 0;
                                    break;
                            }

                            Next_MechPointX = dref_X;
                            Next_MechPointY = dref_Y;
                            //Next_MeasPointX = dref_X - Stage_LimitPointX / 2 - Measure_HomePointX;
                            //Next_MeasPointY = Stage_LimitPointY / 2 - dref_Y - Measure_HomePointY;
                            Next_MeasPointX = Stage_LimitPointX / 2 - dref_X - Measure_HomePointX;
                            Next_MeasPointY = dref_Y - Stage_LimitPointY / 2 - Measure_HomePointY;
                            //StagePort.Write("A:W+P" + 190 * 1000 + "+P" + 10 * 1000 + "\r\n");
                            StagePort.Write("A:W+P" + dref_X * 1000 + "+P" + dref_Y * 1000 + "\r\n");
                            StagePort.Write("G:" + "\r\n");
                            measurementIndex++;
                            Thread.Sleep(100);

                            DebugLog("WorkStart3");
                            TryRunStageRefThread(5);
                            //workerForMeasure.RunWorkerAsync();

                        }
                        else if (measurementIndex == 2)
                        {
                            Debug.Print("measurementIndex == 2");

                            Thread.Sleep(100);
                            refList.Clear();
                            sb.Append("0,").Append(elapsedTime).Append(",").Append(Current_MeasPointX.ToString("F3")).
                                        Append(",").Append(Current_MeasPointY.ToString("F3")).Append(",Reference(ADC Counts),");

                            for (ushort pixel = 0; pixel <= stopPixel - startPixel; pixel++)
                            {
                                reference_Spectrum.Value[pixel] = m_Spectrum.Value[pixel];
                                sb.Append(Math.Round(reference_Spectrum.Value[pixel], 5)).Append(",");
                                refList.Add(m_Lambda.Value[startPixel + pixel], reference_Spectrum.Value[pixel]);
                            }
                            sb.Remove(sb.Length - 1, 1);
                            swForRawData.WriteLine(sb.ToString());
                            swForRawData.Flush();

                            IntensityGraphControl.AxisChange();
                            IntensityGraphControl.Invalidate();

                            Recalculate();

                            //Pattern Alignment
                            if (b_RecipewithAlignment == true)
                            {
                                swForRawData.WriteLine("Init X :" + PreAlign_InitX.ToString("F3"));
                                swForRawData.WriteLine("Init Y :" + PreAlign_InitY.ToString("F3"));
                                swForRawData.WriteLine("Align Init X :" + Align_InitX.ToString("F3"));
                                swForRawData.WriteLine("Align Init Y :" + Align_InitY.ToString("F3"));
                                swForRawData.WriteLine("Align X [mm] :" + X_AlignCorrection.ToString("F3"));
                                swForRawData.WriteLine("Align Y [mm] :" + Y_AlignCorrection.ToString("F3"));
                                swForRawData.WriteLine("Align Tol X [mm] : " + X_AlignTol.ToString("F3"));
                                swForRawData.WriteLine("Align Tol Y [mm] : " + Y_AlignTol.ToString("F3"));
                                swForRawData.WriteLine("Angle X :" + X_AngleCorrection.ToString("F3"));
                                swForRawData.WriteLine("Angle Y :" + Y_AngleCorrection.ToString("F3"));
                                swForRawData.WriteLine("Pitch X [mm] :" + X_Devicepitch.ToString("F3"));
                                swForRawData.WriteLine("Pitch Y [mm] :" + Y_Devicepitch.ToString("F3"));
                                swForRawData.WriteLine("Shot X :" + X_Deviceshot.ToString("F3"));
                                swForRawData.WriteLine("Shot Y :" + Y_Deviceshot.ToString("F3"));
                            }

                            swForRawData.WriteLine("Film Type : " + strPreFilmType); //Properties.Settings.Default.recipeName
                            swForRawData.Flush();

                            string fourthLine = "Index," + "ElapsedTime[sec]," + "X_Position ," + "Y_Position ," + "WaveLength[nm],";
                            for (int i = 0; i < source.Length; i++) //measurement
                            {
                                fourthLine += source[i][0] + ",";
                            }
                            fourthLine += ",";
                            for (int i = 0; i < source.Length; i++) //fitting
                            {
                                fourthLine += source[i][0] + ",";
                            }

                            // 20230710 Add Peak to Peak
                            switch (Properties.Settings.Default.recipeName)
                            {
                                case "AlScN on Si":
                                    fourthLine += ",n0,n1,n2,k0,k1,k2,d,FitQuality";
                                    break;

                                case "SiO2 on Si":
                                    fourthLine += ",n0,n1,d,FitQuality";
                                    break;

                                case "Al2O3 on Si":
                                    fourthLine += ",n0,n1,d,FitQuality";
                                    break;

                                case "TiO2 on Si":
                                    fourthLine += ",A,E0,C,Eg,n_infinite,d,FitQuality";
                                    break;

                                // 20230805 Add SiO2 on LN sub(fitting)
                                case "SiO2 on LN":
                                    fourthLine += ",n0,n1,d,FitQuality";
                                    break;

                                // 20230905 Add SiO2 2micron Measure
                                case "SiO2 on Si Sub_2micron":
                                    fourthLine += ",n0,n1,d,FitQuality";
                                    break;
                                // 20230905 Add SiO2 3micron Measure
                                case "SiO2 on LN Sub_3micron":
                                    fourthLine += ",n0,n1,d,FitQuality";
                                    break;

                                case "SiO2(PtoP)":
                                    fourthLine += ",d,Peak01_X,Peak01_Y,Peak02_X,Peak02_Y,Peak03_X,Peak03_Y,Peak04_X,Peak04_Y,Peak05_X,Peak05_Y" +
                                                  ",Peak06_X,Peak06_Y,Peak07_X,Peak07_Y,Peak08_X,Peak08_Y,Peak09_X,Peak09_Y,Peak10_X,Peak10_Y" +
                                                  ",Peak11_X,Peak11_Y,Peak12_X,Peak12_Y,Peak13_X,Peak13_Y,Peak14_X,Peak14_Y,Peak15_X,Peak15_Y" +
                                                  ",Peak16_X,Peak16_Y,Peak17_X,Peak17_Y,Peak18_X,Peak18_Y,Peak19_X,Peak19_Y,Peak20_X,Peak20_Y" +
                                                  "Thickness01,Thickness02,Thickness03,Thickness04,Thickness05,Thickness06,Thickness07,Thickness08,Thickness09" +
                                                  ",Thickness10,Thickness11,Thickness12,Thickness13,Thickness14,Thickness15" +
                                                  ",Thickness16,Thickness17,Thickness18,Thickness19,FitQuality";
                                    break;
                                default:
                                    break;
                            }

                            swForRawData.WriteLine(fourthLine);
                            swForRawData.Flush();

                            int res = Avaspec.AVS_Measure(deviceHandle, hMainWnd, 1);
                            Debug.Print("AVS_Meas Start : " + measurementIndex);
                            //int res = Avaspec.AVS_MeasureCallback(deviceHandle, hMainWnd, 1);
                            //if (Avaspec.ERR_SUCCESS != res)
                            if (res != Avaspec.ERR_SUCCESS)
                            {
                                if (b_MeasAuto == false)
                                {
                                    MessageBox.Show("Error in AVS_Measure: " + res.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    currentStatus = (int)OperatingStateEnumeration.Error;
                                }
                            }
                            //Map position Correction
                            if (b_RecipewithAlignment == true)
                            {
                                MappingReCalc();
                                /*
                                if (true)//20240805-1YZQ CConfig.GetBool(E_MacCfg.DebugMode))
                                {
                                    double stepX, stepY, angleCorrectionX, angleCorrectionY, stepCorrectX, stepCorrectY;
                                    stepX = order[orderIndex][0];
                                    stepY = order[orderIndex][1];
                                    angleCorrectionX = (X_AngleCorrection) * ((stepY + Y_AlignCorrection - Align_InitY) / (Y_Devicepitch));
                                    angleCorrectionY = (Y_AngleCorrection) * ((stepX + X_AlignCorrection - Align_InitX) / (X_Devicepitch));
                                    stepCorrectX = dXMoveXGrad * ((stepX + X_AlignCorrection - Align_InitX) / (X_Devicepitch));
                                    stepCorrectY = dYMoveYGrad * ((stepX + X_AlignCorrection - Align_InitX) / (X_Devicepitch));
                                    order[orderIndex][0] = stepX + X_AlignCorrection + angleCorrectionX + stepCorrectX;
                                    order[orderIndex][1] = stepY + Y_AlignCorrection + angleCorrectionY + stepCorrectY;
                                }
                                else
                                {
                                    order[orderIndex][0] = order[orderIndex][0] + X_AlignCorrection + (X_AngleCorrection) * ((order[orderIndex][1] + Y_AlignCorrection - Align_InitY) / (Y_Devicepitch));
                                    order[orderIndex][1] = order[orderIndex][1] + Y_AlignCorrection + (Y_AngleCorrection) * ((order[orderIndex][0] + X_AlignCorrection - Align_InitX) / (X_Devicepitch));
                                }
                                */
                            }

                            Next_MeasPointX = order[orderIndex][0];
                            Next_MeasPointY = order[orderIndex][1];
                            //Next_MechPointX = Stage_LimitPointX / 2 + Measure_HomePointX + order[orderIndex][0];
                            //Next_MechPointY = Stage_LimitPointY / 2 - Measure_HomePointY - order[orderIndex][1];
                            Next_MechPointX = Stage_LimitPointX / 2 - Measure_HomePointX - order[orderIndex][0];
                            Next_MechPointY = Stage_LimitPointY / 2 + Measure_HomePointY + order[orderIndex][1];

                            //これからサンプル反射率測定に入る
                            //string cmd = "A:W+P" + (Stage_LimitPointX / 2 + Measure_HomePointX + order[orderIndex][0]) * 1000 + "+P" + (Stage_LimitPointY / 2 - Measure_HomePointY - order[orderIndex][1]) * 1000;
                            string cmd = "A:W+P" + (Stage_LimitPointX / 2 - Measure_HomePointX - order[orderIndex][0]) * 1000 + "+P" + (Stage_LimitPointY / 2 + Measure_HomePointY + order[orderIndex][1]) * 1000;
                            StagePort.Write(cmd + "\r\n");
                            //StagePort.Write("A:W+P" + (100 - order[orderIndex][0]) * 1000 + "+P" + (100 + order[orderIndex][1]) * 1000 + "\r\n");
                            StagePort.Write("G:" + "\r\n");
                            measurementIndex++;
                            DebugLog("WorkStart4");
                            workerForMeasure.RunWorkerAsync();
                        }
                        else
                        {
                            Debug.Print("measurementIndex == else");

                            Thread.Sleep(100);
                            sb.Append(measurementIndex - 2).Append(",").Append(elapsedTime).Append(",").
                                            Append(order[orderIndex][0]).Append(",").Append(order[orderIndex][1]).Append(",Sample(R%),");
                            for (ushort pixel = 0; pixel <= stopPixel - startPixel; pixel++)
                            {
                                samList.Add(m_Lambda.Value[startPixel + pixel], m_Spectrum.Value[pixel]);
                            }
                            IntensityGraphControl.AxisChange();
                            IntensityGraphControl.Invalidate();

                            Interpolate();

                            double[,] temp = new double[1, source.Length];
                            //MessageBox.Show("source.length equals: " + source.Length + "; first WL:" + source[0][0] + "; last WL:" + source[source.Length - 1][0]);

                            double reflectance = -1;
                            double denominator = 1;
                            for (int i = 0; i < source.Length; i++)
                            {
                                denominator = source[i][1] - interDark[i][1];
                                reflectance = denominator == 0 ? -1 : 100.0 * (interSample[i][1] - interDark[i][1]) / denominator;
                                reflectanceList.Add(source[i][0], reflectance);
                                sb.Append(Math.Round(reflectance, 5)).Append(",");

                                temp[0, i] = reflectance;
                            }
                            sb.Append(",");

                            MWArray x = new MWNumericArray(temp);
                            MWArray y = new MWNumericArray(double.Parse(Properties.Settings.Default.refractiveWL));
                            object res = null;

                            //20230710 Add Peak to Peak
                            switch (Properties.Settings.Default.recipeName)
                            {
                                case "AlScN on Si":
                                    res = fitting.AlScN(2, x, y);
                                    break;

                                case "SiO2 on Si":
                                    res = fitting.SiO2(2, x, y);
                                    break;

                                case "Al2O3 on Si":
                                    res = fitting.Al2O3(2, x, y);
                                    break;

                                case "TiO2 on Si":
                                    res = fitting.TiO2(2, x, y);
                                    break;
                                // 20230805 Add SiO2 on LN Sub
                                case "SiO2 on LN":
                                    res = fitting.LN_SiO2(2, x, y);
                                    break;
                                // 20230905 Add SiO2 2micron Measure(Si, LN)
                                case "SiO2 on Si Sub_2micron":
                                    res = fitting.SiO2_2micron(2, x, y);
                                    break;
                                // 20230905 Add SiO2 2micron Measure(Si, LN)
                                case "SiO2 on LN Sub_3micron":
                                    res = fitting.LN_SiO2_3micron(2, x, y);
                                    break;
                                case "SiO2(PtoP)":
                                    res = fitting.SiO2_PtoP_v3(2, x, y);
                                    break;
                                default:
                                    break;
                            }

                            object[] results = (object[])res;
                            double[,] data = (double[,])results[0];
                            double[,] N = (double[,])results[1];

                            for (int i = 0; i < source.Length; i++)
                            {
                                fittingList.Add(source[i][0], data[i, 0]);
                                sb.Append(Math.Round(data[i, 0], 5)).Append(",");
                            }
                            sb.Append(",");

                            // 20230710 Add Peak to Peak
                            switch (Properties.Settings.Default.recipeName)
                            {
                                case "AlScN on Si":
                                    sb.Append(data[source.Length, 0]).Append(",").Append(data[source.Length + 1, 0]).Append(",").Append(data[source.Length + 2, 0]).Append(",").Append(data[source.Length + 3, 0]).Append(",").Append(data[source.Length + 4, 0]).Append(",").Append(data[source.Length + 5, 0]).Append(",").Append(data[source.Length + 6, 0]).Append(",").Append(100 - data[source.Length + 7, 0]);
                                    DataGrid.Rows[orderIndex].Cells[2].Value = Math.Round(data[source.Length + 6, 0], 3);
                                    DataGrid.Rows[orderIndex].Cells[3].Value = Math.Round((100 - data[source.Length + 7, 0]), 5);
                                    DataGrid.Rows[orderIndex].Cells[4].Value = Math.Round(N[0, 0], 3);
                                    break;

                                case "SiO2 on Si":
                                    sb.Append(data[source.Length, 0]).Append(",").Append(data[source.Length + 1, 0]).Append(",").Append(data[source.Length + 2, 0]).Append(",").Append(100 - data[source.Length + 3, 0]);
                                    DataGrid.Rows[orderIndex].Cells[2].Value = Math.Round(data[source.Length + 2, 0], 3);
                                    DataGrid.Rows[orderIndex].Cells[3].Value = Math.Round((100 - data[source.Length + 3, 0]), 5);
                                    DataGrid.Rows[orderIndex].Cells[4].Value = Math.Round(N[0, 0], 3);
                                    break;

                                case "Al2O3 on Si":
                                    sb.Append(data[source.Length, 0]).Append(",").Append(data[source.Length + 1, 0]).Append(",").Append(data[source.Length + 2, 0]).Append(",").Append(100 - data[source.Length + 3, 0]);
                                    DataGrid.Rows[orderIndex].Cells[2].Value = Math.Round(data[source.Length + 2, 0], 3);
                                    DataGrid.Rows[orderIndex].Cells[3].Value = Math.Round((100 - data[source.Length + 3, 0]), 5);
                                    DataGrid.Rows[orderIndex].Cells[4].Value = Math.Round(N[0, 0], 3);
                                    break;

                                case "TiO2 on Si":
                                    sb.Append(data[source.Length, 0]).Append(",").Append(data[source.Length + 1, 0]).Append(",").Append(data[source.Length + 2, 0]).Append(",").Append(data[source.Length + 3, 0]).Append(",").Append(data[source.Length + 4, 0]).Append(",").Append(data[source.Length + 5, 0]).Append(",").Append(100 - data[source.Length + 6, 0]);
                                    DataGrid.Rows[orderIndex].Cells[2].Value = Math.Round(data[source.Length + 5, 0], 3);
                                    DataGrid.Rows[orderIndex].Cells[3].Value = Math.Round((100 - data[source.Length + 6, 0]), 5);
                                    DataGrid.Rows[orderIndex].Cells[4].Value = Math.Round(N[0, 0], 3);
                                    break;

                                // 20230805 Add Sio2 on LN(Fitting)
                                case "SiO2 on LN":
                                    sb.Append(data[source.Length, 0]).Append(",").Append(data[source.Length + 1, 0]).Append(",").Append(data[source.Length + 2, 0]).Append(",").Append(100 - data[source.Length + 3, 0]);
                                    DataGrid.Rows[orderIndex].Cells[2].Value = Math.Round(data[source.Length + 2, 0], 3);
                                    DataGrid.Rows[orderIndex].Cells[3].Value = Math.Round((100 - data[source.Length + 3, 0]), 5);
                                    DataGrid.Rows[orderIndex].Cells[4].Value = Math.Round(N[0, 0], 3);
                                    break;
                                // 20230905 Add Sio2 2micron Measure(Si, LN)
                                case "SiO2 on Si Sub_2micron":
                                    sb.Append(data[source.Length, 0]).Append(",").Append(data[source.Length + 1, 0]).Append(",").Append(data[source.Length + 2, 0]).Append(",").Append(100 - data[source.Length + 3, 0]);
                                    DataGrid.Rows[orderIndex].Cells[2].Value = Math.Round(data[source.Length + 2, 0], 3);
                                    DataGrid.Rows[orderIndex].Cells[3].Value = Math.Round((100 - data[source.Length + 3, 0]), 5);
                                    DataGrid.Rows[orderIndex].Cells[4].Value = Math.Round(N[0, 0], 3);
                                    break;
                                // 20230905 Add Sio2 3micron Measure(Si, LN)
                                case "SiO2 on LN Sub_3micron":
                                    sb.Append(data[source.Length, 0]).Append(",").Append(data[source.Length + 1, 0]).Append(",").Append(data[source.Length + 2, 0]).Append(",").Append(100 - data[source.Length + 3, 0]);
                                    DataGrid.Rows[orderIndex].Cells[2].Value = Math.Round(data[source.Length + 2, 0], 3);
                                    DataGrid.Rows[orderIndex].Cells[3].Value = Math.Round((100 - data[source.Length + 3, 0]), 5);
                                    DataGrid.Rows[orderIndex].Cells[4].Value = Math.Round(N[0, 0], 3);
                                    break;
                                case "SiO2(PtoP)":
                                    sb.Append(data[source.Length, 0]).Append(",").Append(data[source.Length + 1, 0]).Append(",").Append(data[source.Length + 2, 0]).Append(",").Append(data[source.Length + 3, 0]).Append(",").Append(data[source.Length + 4, 0]).Append(",").Append(data[source.Length + 5, 0]).Append(",").Append(data[source.Length + 6, 0]).Append(",").Append(data[source.Length + 7, 0])
                                        .Append(",").Append(data[source.Length + 8, 0]).Append(",").Append(data[source.Length + 9, 0]).Append(",").Append(data[source.Length + 10, 0]).Append(",").Append(data[source.Length + 11, 0]).Append(",").Append(data[source.Length + 12, 0]).Append(",").Append(data[source.Length + 13, 0]).Append(",").Append(data[source.Length + 14, 0]).Append(",").Append(data[source.Length + 15, 0])
                                        .Append(",").Append(data[source.Length + 16, 0]).Append(",").Append(data[source.Length + 17, 0]).Append(",").Append(data[source.Length + 18, 0]).Append(",").Append(data[source.Length + 19, 0]).Append(",").Append(data[source.Length + 20, 0]).Append(",").Append(data[source.Length + 21, 0]).Append(",").Append(data[source.Length + 22, 0]).Append(",").Append(data[source.Length + 23, 0])
                                        .Append(",").Append(data[source.Length + 24, 0]).Append(",").Append(data[source.Length + 25, 0]).Append(",").Append(data[source.Length + 26, 0]).Append(",").Append(data[source.Length + 27, 0]).Append(",").Append(data[source.Length + 28, 0]).Append(",").Append(data[source.Length + 29, 0]).Append(",").Append(data[source.Length + 30, 0]).Append(",").Append(data[source.Length + 31, 0])
                                        .Append(",").Append(data[source.Length + 32, 0]).Append(",").Append(data[source.Length + 33, 0]).Append(",").Append(data[source.Length + 34, 0]).Append(",").Append(data[source.Length + 35, 0]).Append(",").Append(data[source.Length + 36, 0]).Append(",").Append(data[source.Length + 37, 0]).Append(",").Append(data[source.Length + 38, 0]).Append(",").Append(data[source.Length + 39, 0])
                                        .Append(",").Append(data[source.Length + 40, 0]).Append(",").Append(data[source.Length + 41, 0]).Append(",").Append(data[source.Length + 42, 0]).Append(",").Append(data[source.Length + 43, 0]).Append(",").Append(data[source.Length + 44, 0]).Append(",").Append(data[source.Length + 45, 0]).Append(",").Append(data[source.Length + 46, 0]).Append(",").Append(data[source.Length + 47, 0])
                                        .Append(",").Append(data[source.Length + 48, 0]).Append(",").Append(data[source.Length + 49, 0]).Append(",").Append(data[source.Length + 50, 0]).Append(",").Append(data[source.Length + 51, 0]).Append(",").Append(data[source.Length + 52, 0]).Append(",").Append(data[source.Length + 53, 0]).Append(",").Append(data[source.Length + 54, 0]).Append(",").Append(data[source.Length + 55, 0])
                                        .Append(",").Append(data[source.Length + 56, 0]).Append(",").Append(data[source.Length + 57, 0]).Append(",").Append(data[source.Length + 58, 0]).Append(",").Append(data[source.Length + 59, 0]).Append(",");
                                    DataGrid.Rows[orderIndex].Cells[2].Value = Math.Round(data[source.Length, 0], 3);
                                    DataGrid.Rows[orderIndex].Cells[3].Value = Math.Round((100 - data[source.Length + 60, 0]), 5);
                                    DataGrid.Rows[orderIndex].Cells[4].Value = Math.Round(N[0, 0], 3);
                                    break;
                            }

                            swForRawData.WriteLine(sb.ToString());
                            swForRawData.Flush();

                            string result = "";
                            result += DataGrid.Rows[orderIndex].Cells[0].Value.ToString() + ",";//.PadRight(8);
                            result += DataGrid.Rows[orderIndex].Cells[1].Value.ToString() + ",";//.PadRight(8);
                            result += DataGrid.Rows[orderIndex].Cells[2].Value.ToString() + ",";// + ",";//.PadRight(16);
                            result += DataGrid.Rows[orderIndex].Cells[3].Value.ToString() + ",";//.PadRight(16);
                            result += DataGrid.Rows[orderIndex].Cells[4].Value.ToString();

                            thicknessWriter.WriteLine(result);
                            thicknessWriter.Flush();
                            ReflectanceGraphControl.AxisChange();
                            ReflectanceGraphControl.Invalidate();

                            if (currentStatus != (int)OperatingStateEnumeration.Measuring)
                            {
                                return;
                            }
                            int resu;
                            if (orderIndex < order.Length - 1)
                            {
                                resu = Avaspec.AVS_Measure(deviceHandle, hMainWnd, 1);
                                DebugLog("AVS_Meas Start :, " + measurementIndex);
                                //Debug.Print("AVS_Meas Start : " + measurementIndex);
                                //if (Avaspec.ERR_SUCCESS != resu)
                                if (resu != Avaspec.ERR_SUCCESS)
                                {
                                    currentStatus = (int)OperatingStateEnumeration.Error;
                                    MessageBox.Show("Error in AVS_Measure: " + resu.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    if (b_MeasAuto == false)
                                    {
                                        MessageBox.Show("Error in AVS_Measure: " + resu.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    else
                                    {
                                        currentStatus = (int)OperatingStateEnumeration.Error;
                                    }
                                }
                            }

                            orderIndex++; //次のポジション
                            if (orderIndex == order.Length)
                            {
                                resu = Avaspec.AVS_StopMeasure(deviceHandle);

                                //StopSpectrometer();
                                Finish_Measure();
                                measurementIndex = 0;
                                if (b_FocMove == true)
                                    Focus_Moving(dinitFocPos);
                                if (b_MeasAuto == false)
                                {
                                    currentStatus = (int)OperatingStateEnumeration.Unknown;
                                    Label_information.Text = "The stage is currently idle. " + "To insert/eject the Wafer, press the Tray Eject button.";
                                    MessageBox.Show("Measurement is completed！", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    //20240924-1 Auto Angle Collection parameter reset
                                    DeviceParaReset();
                                    currentStatus = (int)OperatingStateEnumeration.Measure_Complete;
                                    b_MeasAuto = false;
                                    //file txt or csv `>> xlsx conversion
                                    //File.Copy(Path.GetDirectoryName(Application.ExecutablePath) + "\\TempMData\\MData_Temp.xlsx", Path.GetDirectoryName(Application.ExecutablePath) + "\\" + AutoFileName + ".xlsx", true);
                                    // 20240218 csv > Excel convert  Excel_Conversion();
                                    MeasExcel.ReadMeasFilePath = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\TempMData\\MData.csv";
                                    //File.Copy(Path.GetDirectoryName(Application.ExecutablePath) + "\\TempMData\\MData.xlsx", Path.GetDirectoryName(Application.ExecutablePath) + "\\TempMData\\" + AutoFileName + ".xlsx", true);
                                    MeasExcel.WriteMeasFilePath = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\TempMData\\MData_Sim.csv";
                                    //MeasExcel.WriteMeasFilePath = Path.GetDirectoryName(Application.ExecutablePath) + "\\TempMData\\" + AutoFileName + ".xlsx";
                                    MeasExcel.exStart_Csv();

                                    //Copy data to shared folder
                                    Directory.CreateDirectory(AutoFilePath);
                                    File.Copy(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\TempMData\\MData.csv",
                                                AutoFilePath + "\\" + AutoFileName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv", true);
                                    File.Copy(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\TempMData\\MData_Sim.csv",
                                                AutoFilePath + "\\" + AutoFileName + ".csv", true);
                                    //File.Copy(Path.GetDirectoryName(Application.ExecutablePath) + "\\TempMData\\" + AutoFileName + ".xlsx", AutoFilePath + "\\" + AutoFileName + ".xlsx", true);
                                    //Converting and copying data for analysis


                                    Label_information.Text = "Measurement is completed！" + "And Moving Tray eject Position.";
                                    AutoMPost = true;
                                    MoveSendPos();
                                }

                                if (strPreFilmType != "")
                                {
                                    Properties.Settings.Default.recipeName = strPreFilmType;
                                }
                                EnableBtn();
                                Menu.Enabled = true;
                                //Label_information.Text = "The stage is currently idle. " + "To insert/eject the Wafer, press the Tray Eject button.";

                                b_MeasFinished = true;
                                return;
                            }

                            //Map Correction Position
                            if (b_RecipewithAlignment == true)
                            {
                                MappingReCalc();

                            }

                            Next_MeasPointX = order[orderIndex][0];
                            Next_MeasPointY = order[orderIndex][1];
                            //Next_MechPointX = Stage_LimitPointX / 2 + Measure_HomePointX + order[orderIndex][0];
                            //Next_MechPointY = Stage_LimitPointY / 2 - Measure_HomePointY - order[orderIndex][1];
                            Next_MechPointX = Stage_LimitPointX / 2 - Measure_HomePointX - order[orderIndex][0];
                            Next_MechPointY = Stage_LimitPointY / 2 + Measure_HomePointY + order[orderIndex][1];

                            //string cmd = "A:W+P" + (Stage_LimitPointX / 2 + Measure_HomePointX + order[orderIndex][0] + dcor_X) * 1000 +
                            //               "+P" + (Stage_LimitPointY / 2 - Measure_HomePointY - order[orderIndex][1] + dcor_Y) * 1000;
                            string cmd = "A:W+P" + (Stage_LimitPointX / 2 - Measure_HomePointX - order[orderIndex][0]) * 1000 +
                                           "+P" + (Stage_LimitPointY / 2 + Measure_HomePointY + order[orderIndex][1]) * 1000;
                            StagePort.Write(cmd + "\r\n");
                            //StagePort.Write("A:W+P" + (100 - order[orderIndex][0]) * 1000 + "+P" + (100 + order[orderIndex][1]) * 1000 + "\r\n");
                            StagePort.Write("G:" + "\r\n");
                            measurementIndex++;

                            DebugLog("WorkStart5");

                            workerForMeasure.RunWorkerAsync();
                        }
                    }
                    else
                    {
                        currentStatus = (int)OperatingStateEnumeration.Error;
                    }
                }
                else
                {
                    currentStatus = (int)OperatingStateEnumeration.Error;
                }
            }
            else
            {
                base.WndProc(ref a_WinMess);
                // 20240227 Spectrometer Freeze timeout check
                if (currentStatus == (int)OperatingStateEnumeration.Measuring
                    && measurementIndex > 0)
                {
                    if (Err_pasttime == 0)
                    {
                        pre_tick_MeastimeoutErrchk = DateTime.Now;
                        tick_MeastimeoutErrchk = DateTime.Now;
                        Err_pasttime = 1;
                    }
                    else
                    {
                        tick_MeastimeoutErrchk = DateTime.Now;
                        Err_pasttime = DateTime.Now.Subtract(pre_tick_MeastimeoutErrchk).TotalSeconds;
                    }
                    if (Err_pasttime > Meas_timeout)
                    {
                        currentStatus = (int)(int)OperatingStateEnumeration.Error;
                        //MeasAutoStop();
                    }
                }
            }
        }

        private void Recalculate()
        {
            List<double[]> listDark = new List<double[]>();
            List<double[]> listRef = new List<double[]>();
            List<double[]> listSource = new List<double[]>();
            for (int i = 0; i < theoreticalR.Length; i++)
            {
                for (int pixel = 0; pixel < stopPixel - startPixel; pixel++)
                {
                    if (m_Lambda.Value[startPixel + pixel] <= theoreticalR[i][0] && m_Lambda.Value[startPixel + pixel + 1] > theoreticalR[i][0])
                    {
                        double xDiff = m_Lambda.Value[startPixel + pixel + 1] - m_Lambda.Value[startPixel + pixel];
                        double refDiff = reference_Spectrum.Value[pixel + 1] - reference_Spectrum.Value[pixel];
                        double darkDiff = dark_Spectrum.Value[pixel + 1] - dark_Spectrum.Value[pixel];
                        double newDark = dark_Spectrum.Value[pixel] + (theoreticalR[i][0] - m_Lambda.Value[startPixel + pixel]) / xDiff * darkDiff;
                        double newRef = reference_Spectrum.Value[pixel] + (theoreticalR[i][0] - m_Lambda.Value[startPixel + pixel]) / xDiff * refDiff;
                        listDark.Add(new double[] { theoreticalR[i][0], newDark });
                        listRef.Add(new double[] { theoreticalR[i][0], newRef });
                        listSource.Add(new double[] { theoreticalR[i][0], 100 * (newRef - newDark) / theoreticalR[i][1] + newDark });
                        break;
                    }
                }
            }
            interDark = listDark.ToArray();
            interRef = listRef.ToArray();
            source = listSource.ToArray();
        }

        private void Interpolate()
        {
            List<double[]> list = new List<double[]>();
            for (int i = 0; i < theoreticalR.Length; i++)
            {
                for (int pixel = 0; pixel < stopPixel - startPixel; pixel++)
                {
                    if (m_Lambda.Value[startPixel + pixel] <= theoreticalR[i][0] && m_Lambda.Value[startPixel + pixel + 1] > theoreticalR[i][0])
                    {
                        double xDiff = m_Lambda.Value[startPixel + pixel + 1] - m_Lambda.Value[startPixel + pixel];
                        double yDiff = m_Spectrum.Value[pixel + 1] - m_Spectrum.Value[pixel];
                        list.Add(new double[] { theoreticalR[i][0], m_Spectrum.Value[pixel] + (theoreticalR[i][0] - m_Lambda.Value[startPixel + pixel]) / xDiff * yDiff });
                        break;
                    }
                }
            }
            interSample = list.ToArray();
        }

        private void IntensityGraphControl_ContextMenuBuilder(ZedGraphControl sender, ContextMenuStrip menuStrip, Point mousePt, ZedGraphControl.ContextMenuObjectState objState)
        {
            foreach (ToolStripMenuItem items in menuStrip.Items)
            {
                if ((string)items.Tag == "set_default")
                {
                    // remove the menu item
                    menuStrip.Items.Remove(items);
                    // or, just disable the item with this
                    //item.Enabled = false; 
                    break;
                }
            }

            // create a new menu item
            ToolStripMenuItem item = new ToolStripMenuItem();
            // This is the user-defined Tag so you can find this menu item later if necessary
            item.Name = "defaultAxis";
            item.Tag = "defaultAxis";
            // This is the text that will show up in the menu
            item.Text = "Set Scale to Default";
            // Add a handler that will respond when that menu item is selected
            item.Click += new System.EventHandler(SetDefaultXAxis);
            // Add the menu item to the menu
            menuStrip.Items.Add(item);
        }

        private void ReflectanceGraphControl_ContextMenuBuilder(ZedGraphControl sender, ContextMenuStrip menuStrip, Point mousePt, ZedGraphControl.ContextMenuObjectState objState)
        {
            foreach (ToolStripMenuItem items in menuStrip.Items)
            {
                if ((string)items.Tag == "set_default")
                {
                    // remove the menu item
                    menuStrip.Items.Remove(items);
                    // or, just disable the item with this
                    //item.Enabled = false; 
                    break;
                }
            }

            // create a new menu item
            ToolStripMenuItem item = new ToolStripMenuItem();
            // This is the user-defined Tag so you can find this menu item later if necessary
            item.Name = "defaultAxis";
            item.Tag = "defaultAxis";
            // This is the text that will show up in the menu
            item.Text = "Set Scale to Default";
            // Add a handler that will respond when that menu item is selected
            item.Click += new System.EventHandler(SetDefaultXAxis);
            // Add the menu item to the menu
            menuStrip.Items.Add(item);
        }

        protected void SetDefaultXAxis(object sender, System.EventArgs e)
        {
            if (GraphTabControl.SelectedTab == GraphTabControl.TabPages[0])
            {
                IntensityGraphControl.GraphPane.XAxis.Scale.Min = minWave;
                IntensityGraphControl.GraphPane.XAxis.Scale.Max = maxWave;
                IntensityGraphControl.GraphPane.YAxis.Scale.Min = -1000;
                IntensityGraphControl.GraphPane.YAxis.Scale.Max = 70000;
                IntensityGraphControl.AxisChange();
                IntensityGraphControl.Invalidate();
            }
            else if (GraphTabControl.SelectedTab == GraphTabControl.TabPages[1])
            {
                ReflectanceGraphControl.GraphPane.XAxis.Scale.Min = minWave;
                ReflectanceGraphControl.GraphPane.XAxis.Scale.Max = maxWave;
                ReflectanceGraphControl.GraphPane.YAxis.Scale.Min = 0;
                ReflectanceGraphControl.GraphPane.YAxis.Scale.Max = 100;
                ReflectanceGraphControl.AxisChange();
                ReflectanceGraphControl.Invalidate();
            }
        }

        private void saveResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filePath = "";
            DialogResult dr = MessageBox.Show("名前を付けて保存", "保存先", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.Cancel) return;
            else if (dr == DialogResult.OK)
            {
                filePath = ShowSaveFileDialog();
            }

            if (filePath == null || filePath.Length == 0) return;
            else
            {
                thicknessWriter = new StreamWriter(filePath);
            }
            if (filePath.Substring(filePath.Length - 3).Equals("csv"))
            {
                thicknessWriter.WriteLine("X,Y,Thickness(nm),FitQuality,RefractiveIndex@" + Properties.Settings.Default.refractiveWL);
                foreach (DataGridViewRow row in DataGrid.Rows)
                {
                    for (int i = 0; i <= row.Cells.Count - 2; i++)
                    {
                        thicknessWriter.Write(row.Cells[i].Value + ",");
                    }
                    thicknessWriter.WriteLine(row.Cells[row.Cells.Count - 1]);
                    thicknessWriter.Flush();
                }
                thicknessWriter.Close();
            }
            else if (filePath.Substring(filePath.Length - 3).Equals("txt"))
            {
                //thicknessWriter.WriteLine("X".PadRight(8) + "Y".PadRight(8) + "Thickness(nm)".PadRight(16) + "FitQuality".PadRight(16) + "RefractiveIndex@" + Properties.Settings.Default.refractiveWL);
                //thicknessWriter.WriteLine("X" + "\t" + "Y" + "\t" + "Thickness(nm)" + "\t" + "FitQuality" + "\t" + "RefractiveIndex@" + Properties.Settings.Default.refractiveWL);
                thicknessWriter.WriteLine("X" + "," + "Y" + "," + "Thickness(nm)");// + "," + "FitQuality" + "," 
                                                                                   // + "RefractiveIndex@" + Properties.Settings.Default.refractiveWL);
                foreach (DataGridViewRow row in DataGrid.Rows)
                {
                    //thicknessWriter.Write(row.Cells[0].Value.ToString().PadRight(8));
                    //thicknessWriter.Write(row.Cells[1].Value.ToString().PadRight(8));
                    //thicknessWriter.Write(row.Cells[2].Value.ToString().PadRight(16));
                    //thicknessWriter.Write(row.Cells[3].Value.ToString().PadRight(16));
                    thicknessWriter.Write(row.Cells[0].Value.ToString() + ",");
                    thicknessWriter.Write(row.Cells[1].Value.ToString() + ",");
                    thicknessWriter.Write(row.Cells[2].Value.ToString() + ",");// + ",");
                    thicknessWriter.Write(row.Cells[3].Value.ToString() + ",");
                    thicknessWriter.WriteLine(row.Cells[row.Cells.Count - 1]);
                    thicknessWriter.Flush();
                }
                thicknessWriter.Close();
            }
        }

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Spectrometer_Config hardDlg = new Spectrometer_Config();
            DialogResult dret = hardDlg.ShowDialog();
            hardDlg.Dispose();
        }

        private void PortComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TimerForRefresh_Tick(object sender, EventArgs e)
        {
            mStatusStripStatus.Text = Enum.GetName(typeof(OperatingStateEnumeration), currentStatus);
            if (currentStatus == (int)OperatingStateEnumeration.Measuring)
            {
                LabelElapsedTime.Text = DateTime.Now.Subtract(startDateTime).TotalSeconds.ToString("F3") + " s";
            }
            else
            {
                //LabelElapsedTime.Text = "";
            }
        }

        private void ThinFilmModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Model modelDlg = new Model();
            DialogResult dret = modelDlg.ShowDialog();
            modelDlg.Dispose();
        }


        private void spectrometerConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Spectrometer_Config hardDlg = new Spectrometer_Config();
            DialogResult dret = hardDlg.ShowDialog();
            hardDlg.Dispose();
        }

        private void btn_Limitset_Click(object sender, EventArgs e)
        {
            switch (cmb_Stgsize.SelectedItem)
            {
                case "8inch":
                    txt_Xlimit.Text = "200";
                    txt_Ylimit.Text = "200";
                    Stage_LimitPointX = 200;
                    Stage_LimitPointY = 200;
                    txt_Xlimit.Enabled = false;
                    txt_Ylimit.Enabled = false;
                    Properties.StageSettings.Default.StageSize = "8inch";
                    break;
                case "12inch":
                    txt_Xlimit.Text = "300";
                    txt_Ylimit.Text = "300";
                    Stage_LimitPointX = 300;
                    Stage_LimitPointY = 300;
                    txt_Xlimit.Enabled = false;
                    txt_Ylimit.Enabled = false;
                    Properties.StageSettings.Default.StageSize = "12inch";
                    break;
            }
        }

        private void btn_MeasOrgSet_Click(object sender, EventArgs e)
        {
            double dsetX_Home = Convert.ToDouble(txt_Xmeasorg_sv.Text);
            double dsetY_Home = Convert.ToDouble(txt_Ymeasorg_sv.Text);

            lbl_XHomepv.Text = dsetX_Home.ToString();
            lbl_YHomepv.Text = dsetY_Home.ToString();

            Measure_HomePointX = dsetX_Home;
            Measure_HomePointY = dsetY_Home;
            Properties.StageSettings.Default.X_Home = dsetX_Home;
            Properties.StageSettings.Default.Y_Home = dsetY_Home;
            Measure_MaxLimitX = +Stage_LimitPointX / 2 - dsetX_Home;
            Measure_MinLimitX = -Stage_LimitPointX / 2 - dsetX_Home;
            Measure_MaxLimitY = +Stage_LimitPointY / 2 - dsetY_Home;
            Measure_MinLimitY = -Stage_LimitPointY / 2 - dsetY_Home;
            Current_MeasPointX = Current_MeasPointX - Measure_HomePointX;
            Current_MeasPointY = Current_MeasPointY - Measure_HomePointY;
            lbl_XPV2.Text = Current_MeasPointX.ToString("F3");
            lbl_Ypv2.Text = Current_MeasPointY.ToString("F3");
        }

        private void btn_XYMovePos_Click(object sender, EventArgs e)
        {
            //if (currentStatus != (int)OperatingStateEnumeration.Idle) return;
            if (currentStatus == (int)OperatingStateEnumeration.Measuring || currentStatus == (int)OperatingStateEnumeration.Busy) return;

            bool chk_ret = false;
            double chk_result = 0;

            chk_ret = double.TryParse(txt_XMovepos.Text, out chk_result);
            if (chk_ret == false && chk_result == 0)
            {
                txt_XMovepos.Text = Current_MeasPointX.ToString();
            }

            chk_ret = double.TryParse(txt_YMovePos.Text, out chk_result);
            if (chk_ret == false && chk_result == 0)
            {
                txt_YMovePos.Text = Current_MeasPointY.ToString();
            }

            double dset_PointX = Convert.ToDouble(txt_XMovepos.Text);
            double dset_PointY = Convert.ToDouble(txt_YMovePos.Text);
            if (dset_PointX < Measure_MinLimitX || dset_PointX > Measure_MaxLimitX ||
                dset_PointY < Measure_MinLimitY || dset_PointY > Measure_MaxLimitY)
            {
                MessageBox.Show("X or Y point is out of range." + Environment.NewLine +
                                   "X range : " + Measure_MinLimitX + "≦ X ≦ " + Measure_MaxLimitX + Environment.NewLine +
                                   "Y range : " + Measure_MinLimitY + "≦ Y ≦ " + Measure_MaxLimitY, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Next_MeasPointX = dset_PointX;
            Next_MeasPointY = dset_PointY;

            //dset_PointX = Stage_LimitPointX / 2 + dset_PointX + Measure_HomePointX;
            //dset_PointY = Stage_LimitPointY / 2 - dset_PointY - Measure_HomePointY;
            dset_PointX = Stage_LimitPointX / 2 - dset_PointX - Measure_HomePointX;
            dset_PointY = Stage_LimitPointY / 2 + dset_PointY + Measure_HomePointY;
            Next_MechPointX = dset_PointX;
            Next_MechPointY = dset_PointY;

            string cmd = "A:W+P" + dset_PointX * 1000 + "+P" + dset_PointY * 1000;
            //StagePort.Write("A:W+P100000+P100000" + "\r\n");
            StagePort.Write(cmd + "\r\n");
            StagePort.Write("G:" + "\r\n");

            Label_information.Text = "Stage moving...";
            workerForStgMove.RunWorkerAsync();
            currentStatus = (int)OperatingStateEnumeration.Busy;
        }

        private void btn_MoveStep_Click(object sender, EventArgs e)
        {
            //if (currentStatus != (int)OperatingStateEnumeration.Idle) return;
            if (currentStatus == (int)OperatingStateEnumeration.Measuring || currentStatus == (int)OperatingStateEnumeration.Busy) return;
            bool chk_ret = false;
            double chk_result = 0;

            chk_ret = double.TryParse(txt_Xstpsv.Text, out chk_result);
            if (chk_ret == false && chk_result == 0)
            {
                txt_Xstpsv.Text = "0.0";
            }

            chk_ret = double.TryParse(txt_Ystpsv.Text, out chk_result);
            if (chk_ret == false && chk_result == 0)
            {
                txt_Ystpsv.Text = "0.0";
            }

            double dset_Xstp = Convert.ToDouble(txt_Xstpsv.Text);
            double dset_Ystp = Convert.ToDouble(txt_Ystpsv.Text);
            if (Current_MeasPointX + dset_Xstp < Measure_MinLimitX || Current_MeasPointX + dset_Xstp > Measure_MaxLimitX ||
                Current_MeasPointY + dset_Ystp < Measure_MinLimitY || Current_MeasPointY + dset_Ystp > Measure_MaxLimitY)
            {
                MessageBox.Show("X or Y point is out of range." + Environment.NewLine +
                                   "X range : " + Measure_MinLimitX + "≦ X ≦ " + Measure_MaxLimitX + Environment.NewLine +
                                   "Y range : " + Measure_MinLimitY + "≦ Y ≦ " + Measure_MaxLimitY, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Next_MeasPointX = Current_MeasPointX + dset_Xstp;
            Next_MeasPointY = Current_MeasPointY + dset_Ystp;

            //double dset_PointX = Stage_LimitPointX / 2 + Measure_HomePointX + (Current_MeasPointX + dset_Xstp);
            //double dset_PointY = Stage_LimitPointY / 2 - Measure_HomePointY - (Current_MeasPointY + dset_Ystp) ;
            double dset_PointX = Math.Round(Stage_LimitPointX / 2 - Measure_HomePointX - (Current_MeasPointX + dset_Xstp), 3);
            double dset_PointY = Math.Round(Stage_LimitPointY / 2 + Measure_HomePointY + (Current_MeasPointY + dset_Ystp), 3);

            Next_MechPointX = Math.Round(dset_PointX, 3);
            Next_MechPointY = Math.Round(dset_PointY, 3);

            string cmd = "A:W+P" + dset_PointX * 1000 + "+P" + dset_PointY * 1000;
            //StagePort.Write("A:W+P100000+P100000" + "\r\n");
            StagePort.Write(cmd + "\r\n");
            StagePort.Write("G:" + "\r\n");

            Label_information.Text = "Stage Moving...";
            workerForStgMove.RunWorkerAsync();
            currentStatus = (int)OperatingStateEnumeration.Busy;
        }

        private void singleMeasureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<double[]> list = new List<double[]>();
            DataGrid.Rows.Clear();

            list.Add(new double[] { Convert.ToDouble(lbl_XPV2.Text), Convert.ToDouble(lbl_Ypv2.Text) });
            DataGrid.Rows.Add(new string[] { lbl_XPV2.Text, lbl_Ypv2.Text, "", "" });
            DataGrid.Refresh();

            order = list.ToArray();

            DataGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            if (currentStatus == (int)OperatingStateEnumeration.Measuring || currentStatus == (int)OperatingStateEnumeration.Busy) return;
            if (order == null || order.Length == 0 || order[0].Length != 2)
            {
                MessageBox.Show("Please check the coordinates, there should be 2 columns (X and Y).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            b_MeasAuto = false;
            MeasErrCnt = 0;
            Err_pasttime = 0;
            b_MeasStop = false;

            Set_AutoFilmType(Properties.Settings.Default.recipeName);

            ReadReferenceTxt();
            for (int i = 0; i < order.Length; i++)
            {
                DataGrid.Rows[i].Cells[2].Value = null;
                DataGrid.Rows[i].Cells[3].Value = null;
                DataGrid.Rows[i].Cells[4].Value = null;
            }
            refList.Clear();
            darkList.Clear();
            samList.Clear();
            reflectanceList.Clear();
            fittingList.Clear();
            IntensityGraphControl.AxisChange();
            IntensityGraphControl.Invalidate();
            ReflectanceGraphControl.AxisChange();
            ReflectanceGraphControl.Invalidate();

            string filePath = "";
            DialogResult dr = MessageBox.Show("Current selected recipe: " + Properties.Settings.Default.recipeName + System.Environment.NewLine +
                "(Refractive index @" + Properties.Settings.Default.refractiveWL + "nm will be saved.)" + System.Environment.NewLine +
                "Save as", "Measurement data storage destination", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.Cancel) return;
            else if (dr == DialogResult.OK)
            {
                filePath = InitialShowSaveFileDialog();
            }

            if (filePath == null || filePath.Length == 0) return;
            else
            {
                thicknessWriter = new StreamWriter(filePath);
            }
            //thicknessWriter.WriteLine("X".PadRight(8) + "Y".PadRight(8) + "Thickness(nm)".PadRight(16) + "FitQuality".PadRight(16) + "RefractiveIndex@" + Properties.Settings.Default.refractiveWL + "nm");
            //thicknessWriter.WriteLine("X" + "\t" + "Y" + "\t" + "Thickness(nm)" + "\t" + "FitQuality" + "\t" + "RefractiveIndex@" + Properties.Settings.Default.refractiveWL + "nm");
            thicknessWriter.WriteLine("X" + "," + "Y" + "," + "Thickness(nm)" + "," + "FitQuality" + ","
                                        + "RefractiveIndex@" + Properties.Settings.Default.refractiveWL + "nm");
            thicknessWriter.Flush();

            currentStatus = (int)OperatingStateEnumeration.Measuring;
            DisableBtn();
            Menu.Enabled = false;

            startDateTime = DateTime.Now;
            StageManager.folderPath = System.IO.Path.Combine(Root_path, startDateTime.ToString("yyyyMMdd_HHmmss"));
            Directory.CreateDirectory(StageManager.folderPath);

            string rawDataFileName = StageManager.folderPath + "\\RawData_" + startDateTime.ToString("yyyyMMdd'_'HHmmss") + ".csv";
            CreateRawDataFile(rawDataFileName);

            //Pattern Alignment
            if (b_RecipewithAlignment == true)
            {
                Label_information.Text = "During Pattern Alignment.";
                //20240924-1
                DeviceParaPreGet();

                //if (true)//CConfig.GetBool(E_MacCfg.DebugMode))//20240804-1YZQ
                //AutoPatternAlignment(false, 2, true);
                //20240924-1 
                if (cbxUseAngleCorrection.Checked && !(Pattern_Num < 1))
                {
                    double Pat_elapsedtime = 0;
                    Pat_StartTime = DateTime.Now;

                    AutoAngleCorrection();
                    if (chk_UseFocusSearch.Checked && FVA.b_Alignment_NG == true)
                    {
                        double Fstep = 0.01;
                        double step_FP = 0;
                        b_FocMove = false;

                        step_FP = dinitFocPos + 0.15;

                        do
                        {
                            DeviceParaReset();
                            step_FP = step_FP - Fstep;
                            Focus_Moving(step_FP);
                            Thread.Sleep(100);
                            b_FocMove = true;
                            AutoAngleCorrection();

                            Pat_elapsedtime = DateTime.Now.Subtract(Pat_StartTime).TotalSeconds;

                            System.Windows.Forms.Application.DoEvents();
                            if (b_MeasStop == true || Pat_elapsedtime > 600)
                                break;

                        } while (FVA.b_Alignment_NG == true && step_FP > dinitFocPos - 0.15);
                    }
                }
                else
                    AutoPatternAlignment(false, 1, true);
                //else
                //    AutoPatternAlignment(false, 1, true);
                if (FVA.b_Alignment_NG == true)
                {
                    //MessageBox.Show("Auto Alinment is failed." + Environment.NewLine + "Stop Measurment.", "Alignment NG", MessageBoxButtons.OK);
                    //currentStatus = (int)OperatingStateEnumeration.Error;
                    DeviceParaReset();
                    thicknessWriter.Close();
                    StagePort.Write("L:E" + "\r\n");
                    Thread.Sleep(100);

                    b_MeasStop = true;
                    Label_information.Text = "Stop Measurement.";
                    if (b_FocMove == true)
                    {
                        Focus_Moving(dinitFocPos);
                        b_FocMove = false;
                    }

                    if (b_MeasAuto == true)
                    {
                        currentStatus = (int)OperatingStateEnumeration.Error;
                    }
                    else
                    {
                        currentStatus = (int)OperatingStateEnumeration.Unknown;
                    }
                    Menu.Enabled = true;
                    EnableBtn();
                    b_MeasFinished = true;
                    return;
                }
                else
                {
                    PreAlign_InitX = Convert.ToDouble(txt_C1_Xinit.Text);
                    PreAlign_InitY = Convert.ToDouble(txt_C1_Yinit.Text);
                    //20240924-1 chg
                    //Align_InitX = Convert.ToDouble(lbl_XPV2.Text);
                    //Align_InitY = Convert.ToDouble(lbl_Ypv2.Text);
                    Align_InitX = Convert.ToDouble(dInitAlignX);
                    Align_InitY = Convert.ToDouble(dInitAlignY);

                    if (CConfig.GetBool(E_MacCfg.DebugMode))
                    {
                        X_AlignCorrection = dInitGapX;
                        Y_AlignCorrection = dInitGapY;
                    }
                    else
                    {
                        X_AlignCorrection = Convert.ToDouble(lbl_DispXGap.Text);
                        Y_AlignCorrection = Convert.ToDouble(lbl_DispGapY.Text);
                    }
                    X_AngleCorrection = Convert.ToDouble(txt_XGrad.Text);
                    Y_AngleCorrection = Convert.ToDouble(txt_YGrad.Text);
                    X_Devicepitch = Convert.ToDouble(txt_Xpitch.Text);
                    Y_Devicepitch = Convert.ToDouble(txt_Ypitch.Text);
                    X_Deviceshot = Convert.ToInt32(txt_XShot.Text);
                    Y_Deviceshot = Convert.ToInt32(txt_YShot.Text);
                }
            }

            PrepareSpectrometer();
            SetXYaxisScale(m_Lambda.Value[startPixel], m_Lambda.Value[stopPixel]);

            orderIndex = 0;
            measurementIndex = 0;

            int l_Res = Avaspec.AVS_Measure(deviceHandle, hMainWnd, 1);
            //if (Avaspec.ERR_SUCCESS != l_Res)
            if (l_Res != Avaspec.ERR_SUCCESS)
            {
                MessageBox.Show("Error in AVS_Measure: " + l_Res.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Label_information.Text = "Measuring...";

            double dDark_X, dDark_Y;
            switch (Properties.Settings.Default.Dark_Point)
            {
                case 1:
                    dDark_X = 178;
                    dDark_Y = 178;
                    break;
                case 2:
                    dDark_X = 22;
                    dDark_Y = 178;
                    break;
                case 3:
                    dDark_X = 178;
                    dDark_Y = 22;
                    break;
                case 4:
                    dDark_X = 22;
                    dDark_Y = 22;
                    break;
                default:
                    dDark_X = 0;
                    dDark_Y = 0;
                    break;
            }

            Next_MechPointX = dDark_X;
            Next_MechPointY = dDark_Y;
            //Next_MeasPointX = dDark_X - Stage_LimitPointX / 2 - Measure_HomePointX;
            //Next_MeasPointY = Stage_LimitPointY / 2 - dDark_Y - Measure_HomePointY;
            Next_MeasPointX = Stage_LimitPointX / 2 - dDark_X - Measure_HomePointX;
            Next_MeasPointY = dDark_Y - Stage_LimitPointY / 2 - Measure_HomePointY;
            //First for the dark measurement
            //StagePort.Write("A:W+P" + 0 * 1000 + "+P" + 100 * 1000 + "\r\n");
            StagePort.Write("A:W+P" + dDark_X * 1000 + "+P" + dDark_Y * 1000 + "\r\n");
            StagePort.Write("G:" + "\r\n");
            measurementIndex++;

            DebugLog("WorkStart6");

            workerForMeasure.RunWorkerAsync();
        }

        public void Update_FVATPCRcvdata(string str_rcv)
        {
            txt_rcvdata.Text = txt_rcvdata.Text + Environment.NewLine + str_rcv;
        }

        public void Update_FVATPCSenddata(string str_send)
        {
            txt_senddata.Text = txt_senddata.Text + Environment.NewLine + str_send;
        }

        private void btn_AlignerRst_Click(object sender, EventArgs e)
        {
            useControlPatternBtn(false);
            FVA.ResetFVA();
            lbl_DispXGap.Text = "0.000";
            lbl_DispGapY.Text = "0.000";
            //lbl_PatternRecipe.Text = "No Pattern";
            //lbl_PatternNum.Text = "0";
            useControlPatternBtn(true);

        }
        public bool AutoPatternAlignment(bool bMsg = true, int nCountDown = 0, bool bCenterCorrection = false)
        {
            bool bResult = false;//20240803-2YZQ
            double d_elapsedtime = 0;

            if (currentStatus == (int)OperatingStateEnumeration.Busy || b_operateAlignment == true)
            {
                b_operateAlignment = false;
                return false;
            }
            //Reset FVA Error
            //FVA.ResetFVA();
            //Thread.Sleep(500);

            Pattern_Num = Convert.ToInt32(lbl_PatternNum.Text);
            if (Pattern_Num < 0) { return true; }//20240812-1YZQ

            b_operateAlignment = true;
            b_AlignmentInitialPos = true;

            bool chk_ret = false;
            double chk_result = 0;

            chk_ret = double.TryParse(txt_C1_Xinit.Text, out chk_result);
            if (chk_ret == false && chk_result == 0)
            {
                txt_C1_Xinit.Text = Current_MeasPointX.ToString();
            }

            chk_ret = double.TryParse(txt_C1_Yinit.Text, out chk_result);
            if (chk_ret == false && chk_result == 0)
            {
                txt_C1_Yinit.Text = Current_MeasPointY.ToString();
            }

            //double dset_PointX = Convert.ToDouble(txt_C1_Xinit.Text);
            //double dset_PointY = Convert.ToDouble(txt_C1_Yinit.Text);
            double dset_PointX = dX_Init;
            double dset_PointY = dY_Init;
            double init_X = dset_PointX;
            double init_Y = dset_PointY;

            if (
                dset_PointX < Measure_MinLimitX || dset_PointX > Measure_MaxLimitX ||
                dset_PointY < Measure_MinLimitY || dset_PointY > Measure_MaxLimitY)
            {
                if (bMsg)
                    MessageBox.Show("X or Y point is out of range." + Environment.NewLine +
                                       "X range : " + Measure_MinLimitX + "≦ X ≦ " + Measure_MaxLimitX + Environment.NewLine +
                                       "Y range : " + Measure_MinLimitY + "≦ Y ≦ " + Measure_MaxLimitY, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                b_operateAlignment = false;
                return false;
            }

            if (b_MeasStop == true)
            {
                b_operateAlignment = false;
                return false;
            }

            Next_MeasPointX = dset_PointX;
            Next_MeasPointY = dset_PointY;

            //dset_PointX = Stage_LimitPointX / 2 + dset_PointX + Measure_HomePointX;
            //dset_PointY = Stage_LimitPointY / 2 - dset_PointY - Measure_HomePointY;
            dset_PointX = Stage_LimitPointX / 2 - dset_PointX - Measure_HomePointX;
            dset_PointY = Stage_LimitPointY / 2 + dset_PointY + Measure_HomePointY;
            Next_MechPointX = dset_PointX;
            Next_MechPointY = dset_PointY;

            dset_PointX = Math.Round(dset_PointX, 3);
            dset_PointY = Math.Round(dset_PointY, 3);
            string cmd = "A:W+P" + dset_PointX * 1000 + "+P" + dset_PointY * 1000 + "\r\n";
            //StagePort.Write("A:W+P100000+P100000" + "\r\n");
            //StagePort.Write(cmd + "\r\n");
            //StagePort.Write("G:" + "\r\n");
            StageWrite(cmd + "\r\n");
            StageWrite("G:" + "\r\n");

            //if (true)//20240805-1YZQ CConfig.GetBool(E_MacCfg.DebugMode))
            //    TryRunStageMoveThread(3);
            //else
            workerForStgMove.RunWorkerAsync();
            Label_information.Text = "Stage moving...";

            if (currentStatus != (int)OperatingStateEnumeration.Measuring)
                currentStatus = (int)OperatingStateEnumeration.Busy;

            bool timeout = false;
            do
            {
                Thread.Sleep(10);
                d_elapsedtime = DateTime.Now.Subtract(Pat_StartTime).TotalSeconds;
                if (d_elapsedtime > 300)
                {
                    timeout = true;
                    break;
                }
                System.Windows.Forms.Application.DoEvents();
            } while (b_AlignmentInitialPos == true);

            if (timeout == true || b_MeasStop == true)
            {
                b_operateAlignment = false;
                return false;
            }
            X_AlignTol = Convert.ToDouble(txt_AlXtol.Text);
            Y_AlignTol = Convert.ToDouble(txt_AlYtol.Text);
            Thread.Sleep(3000);
            if (FVA.AlignmentProcess(Pattern_Num, X_AlignTol, Y_AlignTol) == true)
            {
                bResult = true;//20240803-2YZQ
                if (currentStatus != (int)OperatingStateEnumeration.Measuring)
                {
                    if (bMsg) MessageBox.Show("Autoalign success!", "OK", MessageBoxButtons.OK);
                }

                double X_diff = Current_MeasPointX - init_X;
                double Y_diff = Current_MeasPointY - init_Y;

                lbl_DispXGap.Text = X_diff.ToString("F3");
                lbl_DispGapY.Text = Y_diff.ToString("F3");
            }
            else
            {
                if (currentStatus != (int)OperatingStateEnumeration.Measuring)
                    if (bMsg) MessageBox.Show("Autoalign failed", "NG", MessageBoxButtons.OK);
            }

            if (ParaUpdate() && --nCountDown > 0)//20240803-2YZQ:Auto angle correction
            {
                b_operateAlignment = false;
                AutoPatternAlignment(false, nCountDown);
            }
            if (CConfig.GetBool(E_MacCfg.UseCenterCorrection) && bCenterCorrection && !bResult)
            {
                bResult = CenterCorrection();//20240914-1YZQ:Add result from CenterCorrection
            }
            b_operateAlignment = false;

            return bResult;
        }

        private void btn_PatternAlign_Click(object sender, EventArgs e)
        {
            useControlPatternBtn(false);
            Pat_StartTime = DateTime.Now;

            AutoPatternAlignment(true);
            useControlPatternBtn(true);
        }


        public void StageGapMove(double CorX, double CorY)
        {
            //if (bStageMoving)
            //{
            //    return;
            //}

            CorX = Math.Round(CorX, 3);
            CorY = Math.Round(CorY, 3);

            b_AlignmentCorPos = true;
            double dSetX, dSetY;
            dSetX = Current_MeasPointX + CorX;
            dSetY = Current_MeasPointY + CorY;

            Next_MeasPointX = dSetX;
            Next_MeasPointY = dSetY;

            dSetX = Stage_LimitPointX / 2 - dSetX - Measure_HomePointX;
            dSetY = Stage_LimitPointY / 2 + dSetY + Measure_HomePointY;
            Next_MechPointX = dSetX;
            Next_MechPointY = dSetY;

            string cmd = "A:W+P" + dSetX * 1000 + "+P" + dSetY * 1000;
            //StagePort.Write("A:W+P100000+P100000" + "\r\n");
            StagePort.Write(cmd + "\r\n");
            StagePort.Write("G:" + "\r\n");

            Label_information.Text = "Stage moving...";
            TryRunStageMoveThread(3);
            if (currentStatus != (int)OperatingStateEnumeration.Measuring)
                currentStatus = (int)OperatingStateEnumeration.Busy;
        }

        public void StagePosMove(double PosX, double PosY)//20240804-1YZQ
        {
            //if (bStageMoving)
            //{
            //    return;
            //}

            PosX = Math.Round(PosX, 3);
            PosY = Math.Round(PosY, 3);

            b_AlignmentCorPos = true;
            double dSetX, dSetY;
            dSetX = PosX;
            dSetY = PosY;

            Next_MeasPointX = dSetX;
            Next_MeasPointY = dSetY;

            dSetX = Stage_LimitPointX / 2 - dSetX - Measure_HomePointX;
            dSetY = Stage_LimitPointY / 2 + dSetY + Measure_HomePointY;
            Next_MechPointX = dSetX;
            Next_MechPointY = dSetY;

            string cmd = "A:W+P" + dSetX * 1000 + "+P" + dSetY * 1000;
            //StagePort.Write("A:W+P100000+P100000" + "\r\n");
            StagePort.Write(cmd + "\r\n");
            StagePort.Write("G:" + "\r\n");

            Label_information.Text = "Stage moving...";
            bMoving = true;
            TryRunStageMoveThread(3);
            if (currentStatus != (int)OperatingStateEnumeration.Measuring)
                currentStatus = (int)OperatingStateEnumeration.Busy;
        }


        private void btn_updateTGMark_Click(object sender, EventArgs e)
        {
            useControlPatternBtn(false);
            FVA.make_TGMark();
            useControlPatternBtn(true);
        }

        //20240506-1 20240429-1YZQ Merge
        frmRcpSetting frm = new frmRcpSetting();
        private void devicePatternToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecipeLoadSave();
        }

        void RecipeLoadSave()
        {
            frm.ShowDialog();
            try
            {
                frmRcpSetting.GetRcpData(out List<string> data);
                //Properties.Settings.Default.recipeName = data[0];
                lbl_PatternRecipe.Text = data[0];
                txt_C1_Xinit.Text = data[1];
                dX_Init = double.Parse(data[1]);
                txt_C1_Yinit.Text = data[2];
                dY_Init = double.Parse(data[2]);
                txt_AlXtol.Text = data[3];
                txt_AlYtol.Text = data[4];
                lbl_PatternNum.Text = data[5];
                //Properties.Settings.Default.refractiveWL = Model.modelList[int.Parse(data[6])];
                txt_Xpitch.Text = data[7];
                X_Devicepitch = double.Parse(data[7]);
                dX_Pitch = X_Devicepitch;
                txt_Ypitch.Text = data[8];
                Y_Devicepitch = double.Parse(data[8]);
                dY_Pitch = Y_Devicepitch;
                txt_XShot.Text = data[9];
                txt_YShot.Text = data[10];
                txt_XGrad.Text = data[11];
                txt_YGrad.Text = data[12];
                txt_ShotIntX.Text = data[13];//20241012 6inchpattern wafer 
                txt_ShotIntY.Text = data[14];

                for (int i = 0; i < data.Count; i++)//20240812-2YZQ
                {
                    DebugLog($"Recipe,{data[i]}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load recipe failed.");
            }

        }
        //20240506-1 Merge fin

        #region 20240803-1YZQ


        public static object DebugLog1Object = new object();
        public static void DebugLog(string message)
        {
            if (!CConfig.GetBool(E_MacCfg.DebugMode))//20240805-1YZQ
                return;
            Console.WriteLine("DebugLog1:" + message);
            string logFolderPath = @"DebugLog";//20240104-1YZQ: C:\DebugLog
            string today = DateTime.Today.ToString("yyyyMMdd");
            today = today + "-Log1";
            string logFilePath = Path.Combine(logFolderPath, $"{today}.csv");

            // ログフォルダが存在しない場合は作成する
            if (!Directory.Exists(logFolderPath))
            {
                Directory.CreateDirectory(logFolderPath);
            }

            // ログファイルが存在しない場合は作成する
            if (!File.Exists(logFilePath))
            {
                File.Create(logFilePath).Close();
            }

            // ファイルへのアクセスを排他的に行う
            lock (DebugLog1Object)
            {
                // ログをファイルに追記する
                using (StreamWriter writer = File.AppendText(logFilePath))
                {
                    string logEntry = $"{DateTime.Now:HH:mm:ss:fff},{message}";
                    writer.WriteLine(logEntry);

                }
            }
        }
        public static void CmdLog(string message)
        {
            if (!CConfig.GetBool(E_MacCfg.DebugMode))//20240805-1YZQ
                return;
            Console.WriteLine("DebugLog1:" + message);
            string logFolderPath = @"DebugLog";//20240104-1YZQ: C:\DebugLog
            string today = DateTime.Today.ToString("yyyyMMdd");
            today = today + "-Log1";
            string logFilePath = Path.Combine(logFolderPath, $"{today}.csv");

            // ログフォルダが存在しない場合は作成する
            if (!Directory.Exists(logFolderPath))
            {
                Directory.CreateDirectory(logFolderPath);
            }

            // ログファイルが存在しない場合は作成する
            if (!File.Exists(logFilePath))
            {
                File.Create(logFilePath).Close();
            }

            // ファイルへのアクセスを排他的に行う
            lock (DebugLog1Object)
            {
                // ログをファイルに追記する
                using (StreamWriter writer = File.AppendText(logFilePath))
                {
                    string logEntry = $"{DateTime.Now:HH:mm:ss:fff},{message}";
                    writer.WriteLine(logEntry);

                }
            }
        }

        private void logTestDebugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DebugLog("LogTest");
            //cLog.CreateLog(@".\Log", $"TestLog.txt");
            //cLog.MeasureLog("LogTest");
        }

        private void cmdTestDebugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCmdTest frm = new frmCmdTest();
            frm.TopMost = true;
            frm.Show();
        }

        public void FVAlingerReset()
        {
            //if(TPCforMPC.IsTCPConnectFinish)
            //    TPCforMPC.CloseTCPServer();

        }

        private void btnReConnect_Click(object sender, EventArgs e)
        {
            //20231229 TCP Server Communicatiion(MPS)
            //Server_TCP TPCforMPC;
            TPCforMPC = new Server_TCP();   //これは必須
            TPCforMPC.initTCPServer();      //これも必須

            //20240420 FVAlingner connect
            FVA = new FVA_Main();
            FVA.initFVA();
            //FVAlingerReset();
        }

        #endregion

        private void txt_XGrad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dX_Grad = double.Parse(txt_XGrad.Text);
            }
            catch
            { }
        }

        private void txt_YGrad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dY_Grad = double.Parse(txt_YGrad.Text);
            }
            catch
            { }
        }

        private void txt_Xpitch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dX_Pitch = double.Parse(txt_Xpitch.Text);
            }
            catch
            { }
        }

        private void txt_Ypitch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dY_Pitch = double.Parse(txt_Ypitch.Text);
            }
            catch
            { }
        }

        private void txt_XShot_TextChanged(object sender, EventArgs e)
        {
            try
            {
                nX_Shot = int.Parse(txt_XShot.Text);
            }
            catch
            { }
        }

        private void txt_YShot_TextChanged(object sender, EventArgs e)
        {
            try
            {
                nY_Shot = int.Parse(txt_YShot.Text);
            }
            catch
            { }
        }

        private void txt_C1_Xinit_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dX_Init = double.Parse(txt_C1_Xinit.Text);
            }
            catch
            { }
        }

        private void txt_C1_Yinit_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dY_Init = double.Parse(txt_C1_Yinit.Text);
            }
            catch
            { }
        }

        private void txt_XPV2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DebugLog($"StagePos_X,X,{lbl_XPV2.Text},Y,{lbl_Ypv2.Text}");//20240812-2YZQ
            }
            catch
            { }
        }

        private void txt_YPV2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DebugLog($"StagePos_Y,X,{lbl_XPV2.Text},Y,{lbl_Ypv2.Text}");//20240812-2YZQ
            }
            catch
            { }
        }


        private void btnAngleCorrection_Click(object sender, EventArgs e)
        {
            useControlPatternBtn(false);
            //AutoPatternAlignment(false,2);
            DeviceParaPreGet();
            AutoAngleCorrection();
            useControlPatternBtn(true);
        }

        private void btn_RowsMove_Click(object sender, EventArgs e)
        {
            //if (currentStatus != (int)OperatingStateEnumeration.Idle) return;
            if (currentStatus == (int)OperatingStateEnumeration.Measuring || currentStatus == (int)OperatingStateEnumeration.Busy) return;
            bool chk_ret = false;
            double chk_result = 0;
            double dset_Xstp = 0;
            double dset_Ystp = 0;

            chk_ret = double.TryParse(txbxStepLength.Text, out chk_result);
            if (chk_ret == false && chk_result == 0)
            {
                txbxStepLength.Text = "0.0";
            }

            Button btn = sender as Button;

            switch (btn.Text)
            {
                case "↑":
                    dset_Ystp = chk_result;
                    break;
                case "↓":
                    dset_Ystp = -chk_result;
                    break;
                case "←":
                    dset_Xstp = -chk_result;
                    break;
                case "→":
                    dset_Xstp = chk_result;
                    break;

                default:
                    return;
            }

            if (Current_MeasPointX + dset_Xstp < Measure_MinLimitX || Current_MeasPointX + dset_Xstp > Measure_MaxLimitX ||
                Current_MeasPointY + dset_Ystp < Measure_MinLimitY || Current_MeasPointY + dset_Ystp > Measure_MaxLimitY)
            {
                MessageBox.Show("X or Y point is out of range." + Environment.NewLine +
                                   "X range : " + Measure_MinLimitX + "≦ X ≦ " + Measure_MaxLimitX + Environment.NewLine +
                                   "Y range : " + Measure_MinLimitY + "≦ Y ≦ " + Measure_MaxLimitY, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Next_MeasPointX = Current_MeasPointX + dset_Xstp;
            Next_MeasPointY = Current_MeasPointY + dset_Ystp;

            //double dset_PointX = Stage_LimitPointX / 2 + Measure_HomePointX + (Current_MeasPointX + dset_Xstp);
            //double dset_PointY = Stage_LimitPointY / 2 - Measure_HomePointY - (Current_MeasPointY + dset_Ystp) ;
            double dset_PointX = Stage_LimitPointX / 2 - Measure_HomePointX - (Current_MeasPointX + dset_Xstp);
            double dset_PointY = Stage_LimitPointY / 2 + Measure_HomePointY + (Current_MeasPointY + dset_Ystp);

            Next_MechPointX = dset_PointX;
            Next_MechPointY = dset_PointY;

            string cmd = "A:W+P" + dset_PointX * 1000 + "+P" + dset_PointY * 1000;
            //StagePort.Write("A:W+P100000+P100000" + "\r\n");
            StagePort.Write(cmd + "\r\n");
            StagePort.Write("G:" + "\r\n");

            Label_information.Text = "Stage Moving...";
            workerForStgMove.RunWorkerAsync();
            currentStatus = (int)OperatingStateEnumeration.Busy;
        }

        bool CenterCorrection()
        {
            //20240924-1 Angle Correction chg 0.1 → 0.15
            double dCorStep = 0.15;
            double delapsedtime = 0;
            double[,] dPosList;
            if (chk_SerchAreaSpread.Checked == true)
            {
                dPosList = new double[,]
                {
                    {-1,1},
                    {0,1},
                    {1,1},
                    {1,0},
                    {-1,0},
                    {1,-1},
                    {0,-1},
                    {-1,-1},
                    //20240924-1 Angle Correction
                    {1,2},
                    {0,2},
                    {-1,2},
                    {-2,2},
                    {-2,1},
                    {-2,0},
                    {-2,-1},
                    {-2,-2},
                    {-1,-2},
                    {0,-2},
                    {1,-2},
                    {2,-2},
                    {2,-1},
                    {2,0},
                    {2,1},
                    {2,2},
                };
            }
            else
            {
                dPosList = new double[,]
                {
                    {-1,1},
                    {0,1},
                    {1,1},
                    {1,0},
                    {-1,0},
                    {1,-1},
                    {0,-1},
                    {-1,-1},
                };
            }

            //double dOrgX = double.Parse(lbl_XPV2.Text);
            //double dOrgY = double.Parse(lbl_Ypv2.Text);
            double dbOrgX = Current_MeasPointX;
            double dbOrgY = Current_MeasPointY;

            int f = dPosList.GetLength(0);
            for (int i = 0; i < dPosList.GetLength(0); i++)
            {

                dPosList[i, 0] *= dCorStep;
                dPosList[i, 1] *= dCorStep;
                txt_C1_Xinit.Text = (dbOrgX + dPosList[i, 0]).ToString();
                txt_C1_Yinit.Text = (dbOrgY + dPosList[i, 1]).ToString();
                //b_AlignmentInitialPos = false;
                //StageManager.EntryFormRef.StagePosMove(dOrgX + dPosList[i, 0], dOrgY + dPosList[i, 1]);
                //do
                //{

                //} while (b_AlignmentInitialPos == true);

                if (AutoPatternAlignment(false, 2))
                {
                    //20240924-1 del
                    //txt_C1_Xinit.Text = (dOrgX + dPosList[i, 0]).ToString();
                    //txt_C1_Yinit.Text = (dOrgY + dPosList[i, 1]).ToString();
                    return true;
                }
                System.Windows.Forms.Application.DoEvents();
                delapsedtime = DateTime.Now.Subtract(Pat_StartTime).TotalSeconds;

                if (b_MeasStop == true || delapsedtime > 600) return false;
            }

            //20240924-1 add
            txt_C1_Xinit.Text = dbOrgX.ToString();
            txt_C1_Yinit.Text = dbOrgY.ToString();

            return false;
        }

        bool ParaUpdate()
        {
            bool bContinue = true;
            //double newX = double.Parse(lbl_XPV2.Text);
            //double newY = double.Parse(lbl_Ypv2.Text);
            double newX = Current_MeasPointX;
            double newY = Current_MeasPointY;
            double oldX = double.Parse(txt_C1_Xinit.Text);
            double oldY = double.Parse(txt_C1_Yinit.Text);
            //if (Math.Abs(newX - oldX) < 0.005
            //    && Math.Abs(newY - oldY) < 0.005)
            //{
            //bContinue = false;
            //}
            X_AlignCorrection = newX - dOrgX;
            Y_AlignCorrection = newY - dOrgY;

            //20240924-1
            //txt_C1_Xinit.Text = lbl_XPV2.Text;
            //txt_C1_Yinit.Text = lbl_Ypv2.Text;
            txt_C1_Xinit.Text = Current_MeasPointX.ToString();
            txt_C1_Yinit.Text = Current_MeasPointY.ToString();
            return bContinue;
        }

        //20240924-1 add
        double dInitAlignX = 0;
        double dInitAlignY = 0;
        double dInitGapX = 0;
        double dInitGapY = 0;
        double dinitstepX = 0;
        double dinitstepY = 0;
        double dinitGradX = 0;
        double dinitGradY = 0;

        int iinitXshot = 0;
        int iinitYshot = 0;
        int iXshotInterval = 0;
        int iYshotInterval = 0;

        double dinitFocPos = 0;
        bool b_FocMove = false;
        //20240924-1
        private void DeviceParaReset()
        {
            txt_C1_Xinit.Text = dInitAlignX.ToString();
            txt_C1_Yinit.Text = dInitAlignY.ToString();
            txt_Xpitch.Text = dinitstepX.ToString();
            txt_Ypitch.Text = dinitstepY.ToString();
            txt_XGrad.Text = dinitGradX.ToString();
            txt_YGrad.Text = dinitGradY.ToString();
            txt_XShot.Text = iinitXshot.ToString();
            txt_YShot.Text = iinitYshot.ToString();
            txt_ShotIntX.Text = iXshotInterval.ToString();
            txt_ShotIntY.Text = iYshotInterval.ToString();
        }
        //20240924-1
        private void DeviceParaPreGet()
        {
            dInitAlignX = double.Parse(txt_C1_Xinit.Text);
            dInitAlignY = double.Parse(txt_C1_Yinit.Text);
            dinitstepX = double.Parse(txt_Xpitch.Text);
            dinitstepY = double.Parse(txt_Ypitch.Text);
            dinitGradX = double.Parse(txt_XGrad.Text);
            dinitGradY = double.Parse(txt_YGrad.Text);
            iinitXshot = int.Parse(txt_XShot.Text);
            iinitYshot = int.Parse(txt_YShot.Text);
            iXshotInterval = int.Parse(txt_ShotIntX.Text);
            iYshotInterval = int.Parse(txt_ShotIntY.Text);
        }

        private void btnPatternAlign_C_Click(object sender, EventArgs e)
        {
            useControlPatternBtn(false);
            double dCur_FocPos = MCM_Ctrl.Get_curMCM_Pos();
            Pat_StartTime = DateTime.Now;
            b_MeasStop = false;
            DeviceParaPreGet();
            AutoPatternAlignment(false, 2, true);
            if (FVA.b_Alignment_NG == true)
            {
                double Fstep = 0.01;
                double step_FP = 0;
                b_FocMove = false;

                step_FP = dCur_FocPos + 0.07;

                do
                {
                    DeviceParaReset();
                    step_FP = step_FP - Fstep;
                    Focus_Moving(step_FP);
                    Thread.Sleep(100);
                    b_FocMove = true;
                    AutoPatternAlignment(false, 2, true);

                    System.Windows.Forms.Application.DoEvents();
                    if (b_MeasStop == true) break;
                } while (FVA.b_Alignment_NG == true && step_FP > dCur_FocPos - 0.07);

                if (b_FocMove == true)
                {
                    Focus_Moving(dCur_FocPos);
                    b_FocMove = false;
                }
            }


            if (FVA.b_Alignment_NG == true)
                MessageBox.Show("Pattern was not.", "Pattern failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                MessageBox.Show("Pattern Alignment is succeeded.", "Pattern success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            useControlPatternBtn(true);
        }

        void TryRunStageMoveThread(int nMaxTimesRetry)
        {
            try
            {
                if (nMaxTimesRetry > 0)
                    workerForStgMove.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                Thread.Sleep(3000);
                TryRunStageMoveThread(--nMaxTimesRetry);
            }

        }

        bool TryRunStageRefThread(int nMaxTimesRetry)
        {
            try
            {
                if (nMaxTimesRetry > 0)
                    workerForMeasure.RunWorkerAsync();
                return true;
            }
            catch (Exception ex)
            {
                Thread.Sleep(2000);
                return TryRunStageRefThread(--nMaxTimesRetry);

            }
        }

        #region AngleCorrection
        double dYMoveXGrad = 0;
        double dYMoveYGrad = 0;
        double dXMoveXGrad = 0;
        double dXMoveYGrad = 0;

        void AutoAngleCorrection()//20240803-2YZQ//20240807-1YZQ Must be start after alignment
        {

            double aimX1 = 0, aimY1 = 0, aimX2 = 0, aimY2 = 0, orgX = 0, orgY = 0, steplengthX, steplengthY, gradX, gradY
                          , CorY = 0, gradX_cal = 0, gradY_cal = 0, preorgX = 0, preorgY = 0;   //20240924-1_Angle correction_add

            //int nStep = 20;
            int nStepX = 20, nStepY = 20;

            double d_elapsedtime = 0;

            //20241012 6inch pattern wafer
            if (cbuseshotnum.Checked)
            {
                nStepX = iXshotInterval * iinitXshot;
                nStepY = iYshotInterval * iinitYshot;
            }

            gradX = double.Parse(txt_XGrad.Text);
            gradY = double.Parse(txt_YGrad.Text);
            steplengthX = double.Parse(txt_Xpitch.Text);
            steplengthY = double.Parse(txt_Ypitch.Text);

            //20240924-1_Angle correction_add
            preorgX = double.Parse(txt_C1_Xinit.Text);
            preorgY = double.Parse(txt_C1_Yinit.Text);

            if (AutoPatternAlignment(false, 2, true) == false)
            {
                FVA.b_Alignment_NG = true;
                return;
            }

            //20240924-1_Angle correction_chg
            //orgX = double.Parse(txt_C1_Xinit.Text);
            //orgY = double.Parse(txt_C1_Yinit.Text);
            //orgX = double.Parse(lbl_XPV2.Text);
            //orgY = double.Parse(lbl_Ypv2.Text);
            orgX = Current_MeasPointX;
            orgY = Current_MeasPointY;
            dInitAlignX = orgX;
            dInitAlignY = orgY;
            dInitGapX = orgX - preorgX;
            dInitGapY = orgY - preorgY;

            //X-pos1(angle correction)
            txt_C1_Xinit.Text = (orgX - nStepX * steplengthX).ToString();
            txt_C1_Yinit.Text = (orgY - nStepX * gradY + CorY).ToString();
            if (AutoPatternAlignment(false, 1, true))
            {
                //aimX1 = double.Parse(lbl_XPV2.Text);
                //aimY1 = double.Parse(lbl_Ypv2.Text);
                aimX1 = Current_MeasPointX;
                aimY1 = Current_MeasPointY;
                steplengthX = (aimX1 - orgX) / -nStepX;
                gradY_cal = (aimY1 - orgY) / -nStepX; //20240924-1_Angle correction_add 
                txt_YGrad.Text = gradY_cal.ToString();
                dXMoveXGrad = steplengthX - X_Devicepitch;
                txt_Xpitch.Text = steplengthX.ToString();
                DebugLog($"-x,gradY,{gradY},{gradY_cal},steplengthX,{steplengthX}");
            }
            else
            {
                FVA.b_Alignment_NG = true;
                return;
            }


            //X-pos2(angle correction)
            txt_C1_Xinit.Text = (orgX + nStepX * steplengthX).ToString();
            txt_C1_Yinit.Text = (orgY + nStepX * gradY + CorY).ToString();
            if (AutoPatternAlignment(false, 1, true))
            {
                //aimX2 = double.Parse(lbl_XPV2.Text);
                //aimY2 = double.Parse(lbl_Ypv2.Text);
                aimX2 = Current_MeasPointX;
                aimY2 = Current_MeasPointY;
                steplengthX = (steplengthX + ((aimX2 - orgX) / nStepX)) / 2; //20240924-1_Angle correction_chg
                gradY_cal = (gradY_cal + ((aimY2 - orgY) / nStepX)) / 2;  //20240924-1_Angle correction_chg 
                //steplengthX = (aimX2 - aimX1) / 40;
                //gradY = (aimY2 - aimY1) / 40;
                txt_YGrad.Text = gradY_cal.ToString();
                dXMoveXGrad = (dXMoveXGrad + (steplengthX - X_Devicepitch)) / 2;
                txt_Xpitch.Text = steplengthX.ToString();
                DebugLog($"+x,gradY,{gradY},{gradY_cal},steplengthX,{steplengthX}");
            }
            else
            {
                FVA.b_Alignment_NG = true;
                return;
            }

            //Y-pos1(angle correction)
            txt_C1_Xinit.Text = (orgX - nStepY * gradX).ToString();
            txt_C1_Yinit.Text = (orgY - nStepY * steplengthY + CorY).ToString();
            if (AutoPatternAlignment(false, 1, true))
            {
                //aimX1 = double.Parse(lbl_XPV2.Text);
                //aimY1 = double.Parse(lbl_Ypv2.Text);
                aimX1 = Current_MeasPointX;
                aimY1 = Current_MeasPointY;
                steplengthY = (aimY1 - orgY) / -nStepY;
                gradX_cal = (aimX1 - orgX) / -nStepY;
                txt_XGrad.Text = gradX_cal.ToString();
                dYMoveYGrad = steplengthY - Y_Devicepitch;
                txt_Ypitch.Text = steplengthY.ToString();
                DebugLog($"-y,gradX,{gradX},{gradX_cal},steplengthY,{steplengthY}");
            }
            else
            {
                FVA.b_Alignment_NG = true;
                return;
            }

            //Y-pos2(angle correction)
            txt_C1_Xinit.Text = (orgX + nStepY * gradX).ToString();
            txt_C1_Yinit.Text = (orgY + nStepY * steplengthY + CorY).ToString();
            if (AutoPatternAlignment(false, 1, true))
            {
                //aimX2 = double.Parse(lbl_XPV2.Text);
                //aimY2 = double.Parse(lbl_Ypv2.Text);
                aimX2 = Current_MeasPointX;
                aimY2 = Current_MeasPointY;
                steplengthY = (steplengthY + ((aimY2 - orgY) / nStepY)) / 2; //20240924-1 Angle Correction chg
                gradX_cal = (gradX_cal + ((aimX2 - orgX) / nStepY)) / 2; //20240924-1 Angle Correction chg
                //steplengthY = (aimY2 - aimY1) / 40;
                //gradX = (aimX2 - aimX1) / 40;
                //txt_XGrad.Text = gradX.ToString();
                txt_XGrad.Text = gradX_cal.ToString();
                dYMoveYGrad = (dYMoveYGrad + (steplengthY - Y_Devicepitch)) / 2;
                txt_Ypitch.Text = steplengthY.ToString();
                DebugLog($"+y,gradX,{gradX},{gradX_cal},steplengthY,{steplengthY}");
            }
            else
            {
                FVA.b_Alignment_NG = true;
                txt_C1_Xinit.Text = preorgX.ToString();
                txt_C1_Yinit.Text = preorgY.ToString();
                return;
            }

            txt_C1_Xinit.Text = preorgX.ToString();
            txt_C1_Yinit.Text = preorgY.ToString();
            //AutoPatternAlignment(false, 1, true);
        }

        void MappingReCalc()
        {
            double stepX, stepY, dnx, dny, angleCorrectionX, angleCorrectionY, stepCorrectX = 0, stepCorrectY = 0;
            stepX = order[orderIndex][0];
            stepY = order[orderIndex][1];

            dnx = ((stepX + X_AlignCorrection - Align_InitX) / (dinitstepX));
            dny = ((stepY + Y_AlignCorrection - Align_InitY) / (dinitstepY));

            //angleCorrectionX = (X_AngleCorrection) * ((stepY + Y_AlignCorrection - Align_InitY) / (dinitstepY));
            //angleCorrectionY = (Y_AngleCorrection) * ((stepX + X_AlignCorrection - Align_InitX) / (dinitstepX));
            angleCorrectionX = (X_AngleCorrection) * dny;
            angleCorrectionY = (Y_AngleCorrection) * dnx;

            if (cbxUseAngleCorrection.Checked)
            {
                //20240924-1 AutoAngleCorrection chg
                //stepCorrectX = dXMoveXGrad * ((stepX + X_AlignCorrection - Align_InitX) / (X_Devicepitch));
                //stepCorrectY = dYMoveYGrad * ((stepY + Y_AlignCorrection - Align_InitY) / (Y_Devicepitch));
                stepCorrectX = dXMoveXGrad * dnx;
                stepCorrectY = dYMoveYGrad * dny;
            }

            order[orderIndex][0] = stepX + X_AlignCorrection + angleCorrectionX + stepCorrectX;
            order[orderIndex][1] = stepY + Y_AlignCorrection + angleCorrectionY + stepCorrectY;
            order[orderIndex][0] = Math.Round(order[orderIndex][0], 3);
            order[orderIndex][1] = Math.Round(order[orderIndex][1], 3);

        }

        #endregion
        //20241012 6inchwafer
        private void cbxUseAngleCorrection_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxUseAngleCorrection.Checked == true)
            {
                cbuseshotnum.Enabled = true;
            }
            else
            {
                cbuseshotnum.Enabled = false;
            }
        }

        // 20241105 Focus Unit
        private void btn_FcsZero_Click(object sender, EventArgs e)
        {
            if (currentStatus == (int)OperatingStateEnumeration.Measuring || currentStatus == (int)OperatingStateEnumeration.Busy) return;
            btn_FcsZero.Enabled = false;
            double set_Pos = 0;

            Focus_Moving(set_Pos);
            btn_FcsZero.Enabled = true;
        }

        private void btn_FcsSetNum_Click(object sender, EventArgs e)
        {
            if (currentStatus == (int)OperatingStateEnumeration.Measuring || currentStatus == (int)OperatingStateEnumeration.Busy) return;

            btn_FcsSetNum.Enabled = false;
            int set_Num = cmb_FcsPosNum.SelectedIndex;
            double set_Pos = ReadFcsProperty(set_Num);

            Focus_Moving(set_Pos);

            btn_FcsSetNum.Enabled = true;
        }
        private void btn_SetFcsPos_Click(object sender, EventArgs e)
        {
            if (currentStatus == (int)OperatingStateEnumeration.Measuring || currentStatus == (int)OperatingStateEnumeration.Busy) return;

            btn_SetFcsPos.Enabled = false;

            bool chk_ret = false;
            double chk_result = 0;
            double set_pos = 0;

            chk_ret = double.TryParse(txt_setFcsPos.Text, out chk_result);
            if (chk_ret == false && chk_result == 0)
            {
                txt_setFcsPos.Text = "0.0";
            }
            set_pos = chk_result;
            Focus_Moving(set_pos);
            btn_SetFcsPos.Enabled = true;
        }
        private void btn_FcsStep_Click(object sender, EventArgs e)
        {
            if (currentStatus == (int)OperatingStateEnumeration.Measuring || currentStatus == (int)OperatingStateEnumeration.Busy) return;

            btn_FcsStepUp.Enabled = false;
            btn_FcsStepdown.Enabled = false;

            bool chk_ret = false;
            double chk_result = 0;
            double dset_stp = 0;
            double dset_pos = 0;

            chk_ret = double.TryParse(txt_FcsStep.Text, out chk_result);
            if (chk_ret == false && chk_result == 0)
            {
                txt_FcsStep.Text = "0.0";
            }

            dset_stp = double.Parse(txt_FcsStep.Text);
            dset_pos = double.Parse(lbl_CurFcsPos.Text);
            if (dset_stp > 1)
                dset_stp = 1;
            Button btn = sender as Button;

            switch (btn.Text)
            {
                case "↑":
                    dset_stp = chk_result;
                    break;
                case "↓":
                    dset_stp = -chk_result;
                    break;

                default:
                    return;
            }

            dset_pos = dset_pos + dset_stp;

            Focus_Moving(dset_pos);

            btn_FcsStepUp.Enabled = true;
            btn_FcsStepdown.Enabled = true;
        }

        private void Focus_Moving(double set_Pos)
        {
            if (set_Pos > 5)
            {
                set_Pos = 5.000;
            }
            else if (set_Pos < -5)
            {
                set_Pos = -5.000;
            }
            bool set_ok = MCM_Ctrl.MoveMCM_setPos(set_Pos);

            if (set_ok)
            {
                double cur_pos = MCM_Ctrl.Get_curMCM_Pos();
                lbl_CurFcsPos.Text = cur_pos.ToString("F3");
                lbl_CurFcsPos.BackColor = System.Drawing.Color.Transparent;
            }
            else
            {
                if (b_MeasAuto == false)
                    MessageBox.Show("Move Failed.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //lbl_CurFcsPos.Text = "Error!";
                lbl_CurFcsPos.BackColor = System.Drawing.Color.Pink;
            }
        }

        private void btn_FcsRec_Click(object sender, EventArgs e)
        {
            int Rec_Num = cmb_FcsPosNum.SelectedIndex;
            double post_rec = 0;
            double rec_pos = double.Parse(lbl_CurFcsPos.Text);

            post_rec = ReadFcsProperty(Rec_Num);

            if (post_rec != 0)
            {
                DialogResult resu = MessageBox.Show("Update Current Focus point?", "Check", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resu == DialogResult.No)
                    return;
            }

            setFcsProperty(Rec_Num, rec_pos);
        }

        private double ReadFcsProperty(int num)
        {
            double pos = 0;
            switch (num)
            {
                case 1:
                    pos = Properties.StageSettings.Default.Fcs_Set1;
                    break;
                case 2:
                    pos = Properties.StageSettings.Default.Fcs_Set2;
                    break;
                case 3:
                    pos = Properties.StageSettings.Default.Fcs_Set3;
                    break;
                case 4:
                    pos = Properties.StageSettings.Default.Fcs_Set4;
                    break;
                case 5:
                    pos = Properties.StageSettings.Default.Fcs_Set5;
                    break;
                case 6:
                    pos = Properties.StageSettings.Default.Fcs_Set6;
                    break;
                case 7:
                    pos = Properties.StageSettings.Default.Fcs_Set7;
                    break;
                case 8:
                    pos = Properties.StageSettings.Default.Fcs_Set8;
                    break;
                case 9:
                    pos = Properties.StageSettings.Default.Fcs_Set9;
                    break;
                default:
                    pos = 0;
                    break;
            }
            return pos;
        }
        private void setFcsProperty(int num, double pos)
        {
            switch (num)
            {
                case 1:
                    Properties.StageSettings.Default.Fcs_Set1 = pos;
                    break;
                case 2:
                    Properties.StageSettings.Default.Fcs_Set2 = pos;
                    break;
                case 3:
                    Properties.StageSettings.Default.Fcs_Set3 = pos;
                    break;
                case 4:
                    Properties.StageSettings.Default.Fcs_Set4 = pos;
                    break;
                case 5:
                    Properties.StageSettings.Default.Fcs_Set5 = pos;
                    break;
                case 6:
                    Properties.StageSettings.Default.Fcs_Set6 = pos;
                    break;
                case 7:
                    Properties.StageSettings.Default.Fcs_Set7 = pos;
                    break;
                case 8:
                    Properties.StageSettings.Default.Fcs_Set8 = pos;
                    break;
                case 9:
                    Properties.StageSettings.Default.Fcs_Set9 = pos;
                    break;
                default:
                    pos = 0;
                    break;
            }
        }
        private void useControlPatternBtn(bool b_onoff)
        {
            if(b_onoff)
            {
                btnAngleCorrection.Enabled = true;
                btnPatternAlign_C.Enabled = true;
                btn_PatternAlign.Enabled = true;
                btn_AlignerRst.Enabled = true;
                btn_updateTGMark.Enabled = true;
            }
            else 
            {
                btnAngleCorrection.Enabled = false;
                btnPatternAlign_C.Enabled = false;
                btn_PatternAlign.Enabled = false;
                btn_AlignerRst.Enabled = false;
                btn_updateTGMark.Enabled = false;
            }
        }
        private void cbuseshotnum_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void fVASettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (FVASettingsForm settingsForm = new FVASettingsForm())
                {
                    DialogResult result = settingsForm.ShowDialog(this);

                    if (result == DialogResult.OK)
                    {
                        // Settings have been saved in the FVASettingsForm
                        DebugLog($"FVA Settings updated: Pitch X = {FVA_Main.pitch_X}, Pitch Y = {FVA_Main.pitch_Y}");

                        // Optionally save to application settings
                        Properties.StageSettings.Default.FVA_Pitch_X = FVA_Main.pitch_X;
                        Properties.StageSettings.Default.FVA_Pitch_Y = FVA_Main.pitch_Y;
                        Properties.StageSettings.Default.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                DebugLog($"Error opening FVA Settings: {ex.Message}");
                MessageBox.Show($"Error opening FVA Settings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
