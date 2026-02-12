using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin.SkinClass;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office.Word;
using Thin_Film_Thickness.Forms;//20240803-1YZQ
using static Thin_Film_Thickness.StageForm;

namespace Thin_Film_Thickness
{
    public class FVA_Main
    {
        //FVA Client
        FVATcpclient TPCforFVA;
        public bool b_Alignment;
        public bool b_operateAlignment;
        public bool b_AlignmentInitialPos;
        private bool b_Res_Align;
        public bool b_Alignment_NG;
        private bool b_ObjCmd;
        private bool b_FANSCmd;
        private bool b_TgCmd;
        private bool b_FSSVCmd;
        private bool b_FSPCCmd;
        private bool b_FRSTCmd;

        private int i_Pattern_Num;
        public double X_CorPos;
        public double Y_CorPos;
        public double X_Gap;
        public double Y_Gap;
        public double X_Tol;
        public double Y_Tol;


        //20240803-1YZQ
        public double theta_Para;

        // Pitch parameters for coordinate transformation
        public static double pitch_X = 994.787;
        public static double pitch_Y = 995.766;

        public static string strFVARcv = "";



        public void initFVA()
        {
            TPCforFVA = new FVATcpclient();
            TPCforFVA.Connect("192.168.55.110", 8637);

            //受信時にコールバックさせる関数設定
            TPCforFVA.StrReceiveCB += TcpStrReceived;
        }

        public bool Alignment(int ipattern)
        {
            bool ret = false;
            int timeoutcount = 0;
            int alignTime = 0;

            i_Pattern_Num = ipattern;

            //Pattern_Num Kind setting
            string cmd = "FSPC " + ipattern.ToString() + " 1";
            Pattern_KindSet(ipattern);
            do
            {
                Thread.Sleep(100);
                Application.DoEvents();
                timeoutcount++;
                if (timeoutcount > 10) break;
            } while (b_FSPCCmd == true);

            do
            {
                //TPCforFVA.Send("FAAL 1");
                make_OBJMark();
                timeoutcount = 0;
                do
                {
                    Thread.Sleep(100);
                    Application.DoEvents();
                    timeoutcount++;
                    if (timeoutcount > 30)
                        break;
                } while (b_ObjCmd == true);
                Thread.Sleep(100);
                Get_Alginment_Info();
                timeoutcount = 0;
                do
                {
                    Thread.Sleep(100);
                    Application.DoEvents();
                    timeoutcount++;
                    if (timeoutcount > 30) break;
                } while (b_FANSCmd == true);
                alignTime++;
                //if (X_Gap > -0.02 && X_Gap < 0.02 && Y_Gap > -0.02 && Y_Gap < 0.02)
                if (alignTime > 3)
                {
                    break;
                }
            } while (b_Res_Align == false);

            if (b_Res_Align == true && b_Alignment_NG == false)
            {
                ret = true;
                Thread.Sleep(100);
                make_OBJMark();
                timeoutcount = 0;
                do
                {
                    Thread.Sleep(100);
                    Application.DoEvents();
                    timeoutcount++;
                    if (timeoutcount > 100) break;
                } while (b_ObjCmd == true);
            }
            else//20240803-1YZQ
            {
                ResetFVA();
            }

            return ret;
        }

        public bool AlignmentProcess(int ipattern , double dtol_X , double dtol_Y)
        {
            bool ret = false;
            int timeoutcount = 0;
            b_operateAlignment = true;
            int alignTime = 0;

            i_Pattern_Num = ipattern;
            X_Tol = dtol_X;
            Y_Tol = dtol_Y;

            //Pattern_Num Kind setting
            string cmd = "FSPC " + ipattern.ToString() + " 1";
            Pattern_KindSet(ipattern);
            do
            {
                Thread.Sleep(100);
                Application.DoEvents();
                timeoutcount++;
                if (timeoutcount > 10) break;
            } while (b_FSPCCmd == true);

            do
            {
                //TPCforFVA.Send("FAAL 1");
                make_OBJMark();
                timeoutcount = 0;
                do
                {
                    Thread.Sleep(100);
                    Application.DoEvents();
                    timeoutcount++;
                    if (timeoutcount > 30) 
                        break;
                } while (b_ObjCmd == true);
                Thread.Sleep(100);
                Get_Alginment_Info();
                timeoutcount = 0;
                do
                {
                    Thread.Sleep(100);
                    Application.DoEvents();
                    timeoutcount++;
                    if (timeoutcount > 30) break;
                } while (b_FANSCmd == true);
                alignTime ++;
                //if (X_Gap > -0.02 && X_Gap < 0.02 && Y_Gap > -0.02 && Y_Gap < 0.02)
                if (Math.Abs(X_Gap)  < X_Tol && Math.Abs(Y_Gap) < Y_Tol)
                {
                    DebugLog($"X_Gap,{X_Gap},Y_Gap,{Y_Gap}");
                    b_Res_Align = true;
                }
                if (alignTime > 5)
                {
                    break;
                }
            } while (b_Res_Align == false);
            
            if (b_Res_Align == true && b_Alignment_NG == false)
            {
                ret = true;
                Thread.Sleep(100);
                make_OBJMark();
                timeoutcount = 0;
                do
                {
                    Thread.Sleep(100);
                    Application.DoEvents();
                    timeoutcount++;
                    if (timeoutcount > 100) break;
                } while (b_ObjCmd == true);
            }
            else//20240803-1YZQ
            {
                ResetFVA();
            }

            b_operateAlignment = false;
            return ret;
        }
        public void Pattern_KindSet(int iPattern)
        {
            string cmd = "FSPC " + iPattern.ToString() + " 1";
            TPCforFVA.Send(cmd);
            b_FSPCCmd = true;
        }
        public void ResetFVA()
        {
            TPCforFVA.Send("FRST");
            Thread.Sleep(100);
            b_FRSTCmd = true;
        }

        public void FVAMMVA()
        {
            TPCforFVA.Send("MMVA 0");
        }

        public void make_TGMark()
        {
            b_TgCmd = true;
            TPCforFVA.Send("FTGT 1");
            int timeoutcount = 0;
            do
            {
                Thread.Sleep(100);
                Application.DoEvents();
                timeoutcount++;
                if (timeoutcount > 100) break;
            } while (b_TgCmd == true);

            b_FSSVCmd = true;
            TPCforFVA.Send("FSSV 0 1"); //20240924-1 cmd parameter chg FSSV 1 1 → FSSV 0 1
        }
        private void make_OBJMark()
        {
            b_ObjCmd = true;
            TPCforFVA.Send("FOBJ 1");
            
        }
        private void Get_Alginment_Info()
        {
            b_FANSCmd = true;
            TPCforFVA.Send("FANS");
        }

        //コマンド受信時の処理
        public void TcpStrReceived(string strData)
        {
            string rdata = strData + "\r\n";
            StageManager.EntryFormRef.Update_FVATPCRcvdata(rdata);

            string retcmd = "";
            string[] lines = strData.Split(' ');
            string cmd = lines[0].Substring(1, 4);

            bWaitRecv = false;
            DebugLog($"CameraCmd,Recv,{strData}");//20240812-2YZQ
            switch (cmd)
            {
                //MNPS
                case "MNPS":
                    //Current StagePosition send
                    retcmd = "MNPS 0" + ' ';
                    double current_X, current_Y;

                    current_X = StageManager.EntryFormRef.Current_MeasPointX * 10000;
                    current_Y = StageManager.EntryFormRef.Current_MeasPointY * 10000;
                    retcmd = retcmd + current_X.ToString() + ' ' + current_Y.ToString() + ' ' + "0";
                    TPCforFVA.Send(retcmd);
                    break;
                //MMVA
                case "MMVA":

                    X_CorPos = Convert.ToDouble(lines[1]) / 10000;
                    X_CorPos = Math.Round(X_CorPos,3);
                    Y_CorPos = Convert.ToDouble(lines[2]) / 10000;
                    Y_CorPos = Math.Round(Y_CorPos, 3);
                    X_Gap = X_CorPos;
                    Y_Gap = Y_CorPos;
                    //if (X_CorPos > -0.02 && X_CorPos < 0.02)
                    if (Math.Abs(X_CorPos) < X_Tol)
                            X_CorPos = 0;
                    //if (Y_CorPos > -0.02 && Y_CorPos < 0.02)
                    if (Math.Abs(Y_CorPos) < Y_Tol)
                        Y_CorPos = 0;
                    StageManager.EntryFormRef.StageGapMove(X_CorPos, Y_CorPos);
                    break;

                //FANS
                case "FANS":
                    if (lines[1].Substring(0, 1) == "0")
                    {
                        X_Gap = Convert.ToDouble(lines[2])/10000;
                        Y_Gap = Convert.ToDouble(lines[3])/10000;
                        X_Gap = Math.Round(X_Gap, 3);
                        Y_Gap = Math.Round(Y_Gap, 3);
                        X_CorPos = Convert.ToDouble(lines[2]) / 10000;
                        //X_CorPos = Math.Round(X_CorPos,3);
                        Y_CorPos = Convert.ToDouble(lines[3]) / 10000;
                        //Y_CorPos = Math.Round(Y_CorPos, 3); ;
                        //if (X_CorPos > -0.02 && X_CorPos < 0.02)
                        //if (Math.Abs(X_CorPos) < X_Tol)
                        //X_CorPos = 0;
                        //if (Y_CorPos > -0.02 && Y_CorPos < 0.02)
                        //if (Math.Abs(Y_CorPos) < Y_Tol)
                        //Y_CorPos = 0;

                        //20240803-1YZQ
                        //X_CorPos = X_CorPos + Y_CorPos / dY_Pitch * StageForm.dX_Grad;
                        //Y_CorPos = Y_CorPos + X_CorPos / dX_Pitch * StageForm.dY_Grad;

                        //X_CorPos = X_CorPos + Y_CorPos / 981.2 * 183.4;
                        //Y_CorPos = Y_CorPos + X_CorPos / -1965.4 * 374.8;

                        //X_CorPos = X_CorPos + Y_CorPos / 994.787 * -101.974;
                        //Y_CorPos = Y_CorPos + X_CorPos / 995.766 * 91.925;
                        X_CorPos = X_CorPos + Y_CorPos / pitch_X * -191.974;
                        Y_CorPos = Y_CorPos + X_CorPos / pitch_Y * 181.925;
                        //X_CorPos *= Math.Cos(2*Math.PI/360*5.5636);
                        //Y_CorPos*=


                        StageManager.EntryFormRef.StageGapMove(X_CorPos , Y_CorPos);
                    }
                    else
                    {
                        b_Alignment_NG = true;
                    }
                    b_FANSCmd = false;
                    break;

                //FAAL
                case "FAAL":
                    if (lines[1] == "0")
                        b_Res_Align = true;
                    else
                        b_Res_Align = false;
                    
                    break;
                //FOBJ
                case "FOBJ":
                    if (lines[1].Substring(0,1) == "0")
                        b_Alignment_NG = false;
                    else
                        b_Alignment_NG = true;
                    b_ObjCmd = false;
                    break;
                //FTGT
                case "FTGT":
                    if (lines[1].Substring(0, 1) == "0")
                        b_Alignment = false;
                    else
                        b_Alignment_NG = true;
                    b_TgCmd = false;
                    break;
                //FSSV
                case "FSSV":
                    if (lines[1].Substring(0, 1) == "0")
                        b_Alignment = false;
                    else
                        b_Alignment_NG = true;
                    b_FSSVCmd = false;
                    break;
                //FSPC
                case "FSPC":
                    if (lines[1].Substring(0, 1) == "0")
                        b_Alignment = false;
                    else
                        b_Alignment_NG = true;
                    b_FSPCCmd = false;
                    break;
                //FRST
                case "FRST":
                    //if (lines[1].Substring(0, 1) == "0")
                    b_Alignment = false;
                    //else
                    //    b_Alignment_NG = true;
                    b_FRSTCmd = false;
                    break;
                default:
                    //FreeStrReceiveCB();//コールバック
                    break;
            }
        }

        public void SendCmd(string cmd)
        {
            bWaitRecv = true;
            TPCforFVA.Send(cmd);
        }

    }
}
