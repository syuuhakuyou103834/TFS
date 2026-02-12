namespace Thin_Film_Thickness
{
    partial class StageForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StageForm));
            this.PortComboBox = new CCWin.SkinControl.SkinComboBox();
            this.BtnConnect = new CCWin.SkinControl.SkinButton();
            this.StagePort = new System.IO.Ports.SerialPort(this.components);
            this.LabelPort = new CCWin.SkinControl.SkinLabel();
            this.DataGrid = new CCWin.SkinControl.SkinDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RightPanel = new CCWin.SkinControl.SkinPanel();
            this.BottomRightPanel = new CCWin.SkinControl.SkinPanel();
            this.TopRightPanel = new CCWin.SkinControl.SkinPanel();
            this.TxtPath = new CCWin.SkinControl.SkinTextBox();
            this.TopLeftPanel = new CCWin.SkinControl.SkinPanel();
            this.btnReConnect = new CCWin.SkinControl.SkinButton();
            this.LabelTime = new CCWin.SkinControl.SkinLabel();
            this.LabelElapsedTime = new CCWin.SkinControl.SkinLabel();
            this.TopLeftPanel2 = new CCWin.SkinControl.SkinPanel();
            this.Label_information = new CCWin.SkinControl.SkinLabel();
            this.TopLeftPanel3 = new CCWin.SkinControl.SkinPanel();
            this.BtnStart = new CCWin.SkinControl.SkinButton();
            this.BtnStop = new CCWin.SkinControl.SkinButton();
            this.BtnEject = new CCWin.SkinControl.SkinButton();
            this.BtnHome = new CCWin.SkinControl.SkinButton();
            this.BottomLeftPanel = new CCWin.SkinControl.SkinPanel();
            this.BottomLeftPanel2 = new CCWin.SkinControl.SkinPanel();
            this.GraphTabControl = new CCWin.SkinControl.SkinTabControl();
            this.TabIntensity = new CCWin.SkinControl.SkinTabPage();
            this.IntensityGraphControl = new ZedGraph.ZedGraphControl();
            this.TabReflectance = new CCWin.SkinControl.SkinTabPage();
            this.ReflectanceGraphControl = new ZedGraph.ZedGraphControl();
            this.tab_MeasurePoint = new CCWin.SkinControl.SkinTabPage();
            this.pnl_FocusUnit = new System.Windows.Forms.Panel();
            this.cmb_FcsComPort = new CCWin.SkinControl.SkinComboBox();
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btn_SetFcsPos = new System.Windows.Forms.Button();
            this.label32 = new System.Windows.Forms.Label();
            this.txt_setFcsPos = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btn_FcsStepdown = new System.Windows.Forms.Button();
            this.btn_FcsStepUp = new System.Windows.Forms.Button();
            this.txt_FcsStep = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.btn_FcsZero = new System.Windows.Forms.Button();
            this.btn_FcsRec = new System.Windows.Forms.Button();
            this.btn_FcsSetNum = new System.Windows.Forms.Button();
            this.lbl_CurFcsPos = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.cmb_FcsPosNum = new System.Windows.Forms.ComboBox();
            this.label31 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chk_SerchAreaSpread = new System.Windows.Forms.CheckBox();
            this.chk_UseFocusSearch = new System.Windows.Forms.CheckBox();
            this.cbuseshotnum = new System.Windows.Forms.CheckBox();
            this.cbxUseAngleCorrection = new System.Windows.Forms.CheckBox();
            this.tlpOperate = new System.Windows.Forms.TableLayoutPanel();
            this.btnAngleCorrection = new System.Windows.Forms.Button();
            this.btn_updateTGMark = new System.Windows.Forms.Button();
            this.btnPatternAlign_C = new System.Windows.Forms.Button();
            this.btn_PatternAlign = new System.Windows.Forms.Button();
            this.btn_AlignerRst = new System.Windows.Forms.Button();
            this.lbl_PatternNum = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbl_PatternRecipe = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_DispGapY = new System.Windows.Forms.Label();
            this.lbl_DispXGap = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tab_Cassete1 = new System.Windows.Forms.TabPage();
            this.txt_AlYtol = new System.Windows.Forms.TextBox();
            this.txt_AlXtol = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_C1_Yinit = new System.Windows.Forms.TextBox();
            this.txt_C1_Xinit = new System.Windows.Forms.TextBox();
            this.tab_Device = new System.Windows.Forms.TabPage();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txt_YShot = new System.Windows.Forms.TextBox();
            this.txt_XShot = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txt_Ypitch = new System.Windows.Forms.TextBox();
            this.txt_Xpitch = new System.Windows.Forms.TextBox();
            this.tab_Angle = new System.Windows.Forms.TabPage();
            this.txt_ShotIntY = new System.Windows.Forms.TextBox();
            this.txt_ShotIntX = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txt_YGrad = new System.Windows.Forms.TextBox();
            this.txt_XGrad = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.lbl_Ypv2 = new System.Windows.Forms.Label();
            this.lbl_XPV2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbp_Point_Move = new System.Windows.Forms.TabPage();
            this.btn_XYMovePos = new System.Windows.Forms.Button();
            this.lbl_YMovepos = new System.Windows.Forms.Label();
            this.lbl_Xmove_pos = new System.Windows.Forms.Label();
            this.txt_YMovePos = new System.Windows.Forms.TextBox();
            this.txt_XMovepos = new System.Windows.Forms.TextBox();
            this.tbp_StepMove = new System.Windows.Forms.TabPage();
            this.btn_MoveStep = new System.Windows.Forms.Button();
            this.txt_Ystpsv = new System.Windows.Forms.TextBox();
            this.lbl_YStep = new System.Windows.Forms.Label();
            this.txt_Xstpsv = new System.Windows.Forms.TextBox();
            this.lbl_XStep = new System.Windows.Forms.Label();
            this.tab_RowsMove = new System.Windows.Forms.TabPage();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.txbxStepLength = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.tab_ComCheck = new CCWin.SkinControl.SkinTabPage();
            this.label20 = new System.Windows.Forms.Label();
            this.txt_AlinRcvCmd = new System.Windows.Forms.RichTextBox();
            this.txt_Alinsendcmd = new System.Windows.Forms.RichTextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.lbl_Recipiinfo = new System.Windows.Forms.Label();
            this.txt_Recipeinfo = new System.Windows.Forms.RichTextBox();
            this.lbl_Sendcmd = new System.Windows.Forms.Label();
            this.lbl_Rcvcmd = new System.Windows.Forms.Label();
            this.txt_senddata = new System.Windows.Forms.RichTextBox();
            this.txt_rcvdata = new System.Windows.Forms.RichTextBox();
            this.tab_MainConfig = new CCWin.SkinControl.SkinTabPage();
            this.pnl_Stgsize = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.btn_Limitset = new System.Windows.Forms.Button();
            this.txt_Ylimit = new System.Windows.Forms.TextBox();
            this.txt_Xlimit = new System.Windows.Forms.TextBox();
            this.lbl_YLimit = new System.Windows.Forms.Label();
            this.lbl_Xlimit = new System.Windows.Forms.Label();
            this.cmb_Stgsize = new System.Windows.Forms.ComboBox();
            this.lbl_StgSize = new System.Windows.Forms.Label();
            this.pnl_Measure_origin = new System.Windows.Forms.Panel();
            this.lbl_YHomepv = new System.Windows.Forms.Label();
            this.lbl_XHomepv = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.btn_MeasOrgSet = new System.Windows.Forms.Button();
            this.txt_Ymeasorg_sv = new System.Windows.Forms.TextBox();
            this.txt_Xmeasorg_sv = new System.Windows.Forms.TextBox();
            this.lbl_YMeasOrg = new System.Windows.Forms.Label();
            this.lbl_Xmeasorg = new System.Windows.Forms.Label();
            this.BottomLeftPanel1 = new CCWin.SkinControl.SkinPanel();
            this.Status = new System.Windows.Forms.StatusStrip();
            this.mStatusStripStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.mStatusStripTxtReceived = new System.Windows.Forms.ToolStripStatusLabel();
            this.TimerForRefresh = new System.Windows.Forms.Timer(this.components);
            this.Menu = new CCWin.SkinControl.SkinMenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openCoordinatesTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ThinFilmModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.singleMeasureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.spectrometerConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.devicePatternToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logTestDebugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdTestDebugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).BeginInit();
            this.RightPanel.SuspendLayout();
            this.BottomRightPanel.SuspendLayout();
            this.TopRightPanel.SuspendLayout();
            this.TopLeftPanel.SuspendLayout();
            this.TopLeftPanel2.SuspendLayout();
            this.TopLeftPanel3.SuspendLayout();
            this.BottomLeftPanel.SuspendLayout();
            this.BottomLeftPanel2.SuspendLayout();
            this.GraphTabControl.SuspendLayout();
            this.TabIntensity.SuspendLayout();
            this.TabReflectance.SuspendLayout();
            this.tab_MeasurePoint.SuspendLayout();
            this.pnl_FocusUnit.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tlpOperate.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tab_Cassete1.SuspendLayout();
            this.tab_Device.SuspendLayout();
            this.tab_Angle.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tbp_Point_Move.SuspendLayout();
            this.tbp_StepMove.SuspendLayout();
            this.tab_RowsMove.SuspendLayout();
            this.tab_ComCheck.SuspendLayout();
            this.tab_MainConfig.SuspendLayout();
            this.pnl_Stgsize.SuspendLayout();
            this.pnl_Measure_origin.SuspendLayout();
            this.BottomLeftPanel1.SuspendLayout();
            this.Status.SuspendLayout();
            this.Menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // PortComboBox
            // 
            this.PortComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.PortComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PortComboBox.FormattingEnabled = true;
            this.PortComboBox.Location = new System.Drawing.Point(92, 14);
            this.PortComboBox.Name = "PortComboBox";
            this.PortComboBox.Size = new System.Drawing.Size(75, 22);
            this.PortComboBox.TabIndex = 0;
            this.PortComboBox.WaterText = "";
            this.PortComboBox.SelectedIndexChanged += new System.EventHandler(this.PortComboBox_SelectedIndexChanged);
            // 
            // BtnConnect
            // 
            this.BtnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnConnect.BackColor = System.Drawing.Color.Transparent;
            this.BtnConnect.BaseColor = System.Drawing.SystemColors.Highlight;
            this.BtnConnect.BorderColor = System.Drawing.Color.Transparent;
            this.BtnConnect.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.BtnConnect.DownBack = null;
            this.BtnConnect.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnConnect.ForeColor = System.Drawing.Color.Black;
            this.BtnConnect.Location = new System.Drawing.Point(277, 11);
            this.BtnConnect.MouseBack = null;
            this.BtnConnect.Name = "BtnConnect";
            this.BtnConnect.NormlBack = null;
            this.BtnConnect.Size = new System.Drawing.Size(82, 23);
            this.BtnConnect.TabIndex = 1;
            this.BtnConnect.Text = "Connect";
            this.BtnConnect.UseVisualStyleBackColor = false;
            this.BtnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // StagePort
            // 
            this.StagePort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.StagePort_DataReceived);
            // 
            // LabelPort
            // 
            this.LabelPort.BackColor = System.Drawing.Color.Transparent;
            this.LabelPort.BorderColor = System.Drawing.Color.White;
            this.LabelPort.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabelPort.Location = new System.Drawing.Point(10, 14);
            this.LabelPort.Name = "LabelPort";
            this.LabelPort.Size = new System.Drawing.Size(73, 20);
            this.LabelPort.TabIndex = 2;
            this.LabelPort.Text = "Serial Port:";
            this.LabelPort.Visible = false;
            // 
            // DataGrid
            // 
            this.DataGrid.AllowUserToAddRows = false;
            this.DataGrid.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
            this.DataGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGrid.BackgroundColor = System.Drawing.SystemColors.Window;
            this.DataGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DataGrid.ColumnFont = null;
            this.DataGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(246)))), ((int)(((byte)(239)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.DataGrid.ColumnSelectForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(188)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGrid.DefaultCellStyle = dataGridViewCellStyle7;
            this.DataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGrid.EnableHeadersVisualStyles = false;
            this.DataGrid.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DataGrid.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.DataGrid.HeadFont = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DataGrid.HeadSelectForeColor = System.Drawing.SystemColors.HighlightText;
            this.DataGrid.Location = new System.Drawing.Point(0, 0);
            this.DataGrid.Name = "DataGrid";
            this.DataGrid.ReadOnly = true;
            this.DataGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.DataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.DataGrid.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.DataGrid.RowTemplate.Height = 50;
            this.DataGrid.Size = new System.Drawing.Size(460, 773);
            this.DataGrid.TabIndex = 4;
            this.DataGrid.TitleBack = null;
            this.DataGrid.TitleBackColorBegin = System.Drawing.Color.White;
            this.DataGrid.TitleBackColorEnd = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(242)))));
            // 
            // Column1
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column1.HeaderText = "X";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 75;
            // 
            // Column2
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column2.HeaderText = "Y";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 75;
            // 
            // Column3
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column3.HeaderText = "Thickness (nm)";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 150;
            // 
            // Column4
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column4.HeaderText = "Fit Quality";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 125;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Refractive Index";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // RightPanel
            // 
            this.RightPanel.BackColor = System.Drawing.Color.Transparent;
            this.RightPanel.Controls.Add(this.BottomRightPanel);
            this.RightPanel.Controls.Add(this.TopRightPanel);
            this.RightPanel.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.RightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightPanel.DownBack = null;
            this.RightPanel.Location = new System.Drawing.Point(707, 52);
            this.RightPanel.MouseBack = null;
            this.RightPanel.Name = "RightPanel";
            this.RightPanel.NormlBack = null;
            this.RightPanel.Size = new System.Drawing.Size(460, 803);
            this.RightPanel.TabIndex = 5;
            // 
            // BottomRightPanel
            // 
            this.BottomRightPanel.BackColor = System.Drawing.Color.Transparent;
            this.BottomRightPanel.Controls.Add(this.DataGrid);
            this.BottomRightPanel.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.BottomRightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BottomRightPanel.DownBack = null;
            this.BottomRightPanel.Location = new System.Drawing.Point(0, 30);
            this.BottomRightPanel.MouseBack = null;
            this.BottomRightPanel.Name = "BottomRightPanel";
            this.BottomRightPanel.NormlBack = null;
            this.BottomRightPanel.Size = new System.Drawing.Size(460, 773);
            this.BottomRightPanel.TabIndex = 1;
            // 
            // TopRightPanel
            // 
            this.TopRightPanel.BackColor = System.Drawing.Color.Transparent;
            this.TopRightPanel.Controls.Add(this.TxtPath);
            this.TopRightPanel.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.TopRightPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopRightPanel.DownBack = null;
            this.TopRightPanel.Location = new System.Drawing.Point(0, 0);
            this.TopRightPanel.MouseBack = null;
            this.TopRightPanel.Name = "TopRightPanel";
            this.TopRightPanel.NormlBack = null;
            this.TopRightPanel.Size = new System.Drawing.Size(460, 30);
            this.TopRightPanel.TabIndex = 0;
            // 
            // TxtPath
            // 
            this.TxtPath.BackColor = System.Drawing.Color.Transparent;
            this.TxtPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtPath.DownBack = null;
            this.TxtPath.Icon = null;
            this.TxtPath.IconIsButton = false;
            this.TxtPath.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.TxtPath.IsPasswordChat = '\0';
            this.TxtPath.IsSystemPasswordChar = false;
            this.TxtPath.Lines = new string[0];
            this.TxtPath.Location = new System.Drawing.Point(0, 0);
            this.TxtPath.Margin = new System.Windows.Forms.Padding(0);
            this.TxtPath.MaxLength = 32767;
            this.TxtPath.MinimumSize = new System.Drawing.Size(28, 28);
            this.TxtPath.MouseBack = null;
            this.TxtPath.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.TxtPath.Multiline = true;
            this.TxtPath.Name = "TxtPath";
            this.TxtPath.NormlBack = null;
            this.TxtPath.Padding = new System.Windows.Forms.Padding(5);
            this.TxtPath.ReadOnly = true;
            this.TxtPath.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.TxtPath.Size = new System.Drawing.Size(460, 30);
            // 
            // 
            // 
            this.TxtPath.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtPath.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtPath.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.TxtPath.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.TxtPath.SkinTxt.Multiline = true;
            this.TxtPath.SkinTxt.Name = "BaseText";
            this.TxtPath.SkinTxt.ReadOnly = true;
            this.TxtPath.SkinTxt.Size = new System.Drawing.Size(450, 20);
            this.TxtPath.SkinTxt.TabIndex = 0;
            this.TxtPath.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.TxtPath.SkinTxt.WaterText = "";
            this.TxtPath.TabIndex = 5;
            this.TxtPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TxtPath.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.TxtPath.WaterText = "";
            this.TxtPath.WordWrap = true;
            // 
            // TopLeftPanel
            // 
            this.TopLeftPanel.BackColor = System.Drawing.Color.SpringGreen;
            this.TopLeftPanel.Controls.Add(this.btnReConnect);
            this.TopLeftPanel.Controls.Add(this.LabelTime);
            this.TopLeftPanel.Controls.Add(this.LabelElapsedTime);
            this.TopLeftPanel.Controls.Add(this.BtnConnect);
            this.TopLeftPanel.Controls.Add(this.LabelPort);
            this.TopLeftPanel.Controls.Add(this.PortComboBox);
            this.TopLeftPanel.ControlState = CCWin.SkinClass.ControlState.Hover;
            this.TopLeftPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopLeftPanel.DownBack = null;
            this.TopLeftPanel.Location = new System.Drawing.Point(4, 52);
            this.TopLeftPanel.MouseBack = null;
            this.TopLeftPanel.Name = "TopLeftPanel";
            this.TopLeftPanel.NormlBack = null;
            this.TopLeftPanel.Size = new System.Drawing.Size(703, 47);
            this.TopLeftPanel.TabIndex = 6;
            // 
            // btnReConnect
            // 
            this.btnReConnect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReConnect.BackColor = System.Drawing.Color.Transparent;
            this.btnReConnect.BaseColor = System.Drawing.SystemColors.Highlight;
            this.btnReConnect.BorderColor = System.Drawing.Color.Transparent;
            this.btnReConnect.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnReConnect.DownBack = null;
            this.btnReConnect.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnReConnect.ForeColor = System.Drawing.Color.Black;
            this.btnReConnect.Location = new System.Drawing.Point(377, 11);
            this.btnReConnect.MouseBack = null;
            this.btnReConnect.Name = "btnReConnect";
            this.btnReConnect.NormlBack = null;
            this.btnReConnect.Size = new System.Drawing.Size(99, 23);
            this.btnReConnect.TabIndex = 5;
            this.btnReConnect.Text = "ReConnect";
            this.btnReConnect.UseVisualStyleBackColor = false;
            this.btnReConnect.Visible = false;
            this.btnReConnect.Click += new System.EventHandler(this.btnReConnect_Click);
            // 
            // LabelTime
            // 
            this.LabelTime.BackColor = System.Drawing.Color.Transparent;
            this.LabelTime.BorderColor = System.Drawing.Color.White;
            this.LabelTime.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabelTime.Location = new System.Drawing.Point(482, 11);
            this.LabelTime.Name = "LabelTime";
            this.LabelTime.Size = new System.Drawing.Size(93, 23);
            this.LabelTime.TabIndex = 4;
            this.LabelTime.Text = "Elapsed Time:";
            // 
            // LabelElapsedTime
            // 
            this.LabelElapsedTime.BackColor = System.Drawing.Color.Transparent;
            this.LabelElapsedTime.BorderColor = System.Drawing.Color.White;
            this.LabelElapsedTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LabelElapsedTime.Font = new System.Drawing.Font("游ゴシック", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LabelElapsedTime.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.LabelElapsedTime.Location = new System.Drawing.Point(581, 5);
            this.LabelElapsedTime.Name = "LabelElapsedTime";
            this.LabelElapsedTime.Size = new System.Drawing.Size(120, 32);
            this.LabelElapsedTime.TabIndex = 3;
            this.LabelElapsedTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TopLeftPanel2
            // 
            this.TopLeftPanel2.BackColor = System.Drawing.Color.White;
            this.TopLeftPanel2.Controls.Add(this.Label_information);
            this.TopLeftPanel2.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.TopLeftPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopLeftPanel2.DownBack = null;
            this.TopLeftPanel2.Location = new System.Drawing.Point(4, 99);
            this.TopLeftPanel2.MouseBack = null;
            this.TopLeftPanel2.Name = "TopLeftPanel2";
            this.TopLeftPanel2.NormlBack = null;
            this.TopLeftPanel2.Size = new System.Drawing.Size(703, 90);
            this.TopLeftPanel2.TabIndex = 7;
            // 
            // Label_information
            // 
            this.Label_information.BackColor = System.Drawing.SystemColors.Info;
            this.Label_information.BorderColor = System.Drawing.Color.White;
            this.Label_information.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Label_information.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label_information.Font = new System.Drawing.Font("游ゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label_information.Location = new System.Drawing.Point(0, 0);
            this.Label_information.Name = "Label_information";
            this.Label_information.Size = new System.Drawing.Size(703, 90);
            this.Label_information.TabIndex = 0;
            this.Label_information.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TopLeftPanel3
            // 
            this.TopLeftPanel3.BackColor = System.Drawing.Color.Pink;
            this.TopLeftPanel3.Controls.Add(this.BtnStart);
            this.TopLeftPanel3.Controls.Add(this.BtnStop);
            this.TopLeftPanel3.Controls.Add(this.BtnEject);
            this.TopLeftPanel3.Controls.Add(this.BtnHome);
            this.TopLeftPanel3.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.TopLeftPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopLeftPanel3.DownBack = null;
            this.TopLeftPanel3.Enabled = false;
            this.TopLeftPanel3.Location = new System.Drawing.Point(4, 189);
            this.TopLeftPanel3.MouseBack = null;
            this.TopLeftPanel3.Name = "TopLeftPanel3";
            this.TopLeftPanel3.NormlBack = null;
            this.TopLeftPanel3.Size = new System.Drawing.Size(703, 58);
            this.TopLeftPanel3.TabIndex = 8;
            // 
            // BtnStart
            // 
            this.BtnStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnStart.BackColor = System.Drawing.Color.Transparent;
            this.BtnStart.BaseColor = System.Drawing.Color.LimeGreen;
            this.BtnStart.BorderColor = System.Drawing.Color.Transparent;
            this.BtnStart.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.BtnStart.DownBack = null;
            this.BtnStart.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnStart.Location = new System.Drawing.Point(316, 7);
            this.BtnStart.MouseBack = null;
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.NormlBack = null;
            this.BtnStart.Size = new System.Drawing.Size(138, 46);
            this.BtnStart.TabIndex = 2;
            this.BtnStart.Text = "Start";
            this.BtnStart.UseVisualStyleBackColor = false;
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // BtnStop
            // 
            this.BtnStop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnStop.BackColor = System.Drawing.Color.Transparent;
            this.BtnStop.BaseColor = System.Drawing.Color.OrangeRed;
            this.BtnStop.BorderColor = System.Drawing.Color.Transparent;
            this.BtnStop.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.BtnStop.DownBack = null;
            this.BtnStop.Font = new System.Drawing.Font("MS UI Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnStop.Location = new System.Drawing.Point(485, 6);
            this.BtnStop.MouseBack = null;
            this.BtnStop.Name = "BtnStop";
            this.BtnStop.NormlBack = null;
            this.BtnStop.Size = new System.Drawing.Size(128, 49);
            this.BtnStop.TabIndex = 3;
            this.BtnStop.Text = "Emergency Stop";
            this.BtnStop.UseVisualStyleBackColor = false;
            this.BtnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // BtnEject
            // 
            this.BtnEject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnEject.BackColor = System.Drawing.Color.Transparent;
            this.BtnEject.BaseColor = System.Drawing.Color.Orange;
            this.BtnEject.BorderColor = System.Drawing.Color.Transparent;
            this.BtnEject.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.BtnEject.DownBack = null;
            this.BtnEject.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnEject.Location = new System.Drawing.Point(29, 11);
            this.BtnEject.MouseBack = null;
            this.BtnEject.Name = "BtnEject";
            this.BtnEject.NormlBack = null;
            this.BtnEject.Size = new System.Drawing.Size(103, 37);
            this.BtnEject.TabIndex = 1;
            this.BtnEject.Text = "Tray Eject";
            this.BtnEject.UseVisualStyleBackColor = false;
            this.BtnEject.Click += new System.EventHandler(this.BtnEject_Click);
            // 
            // BtnHome
            // 
            this.BtnHome.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnHome.BackColor = System.Drawing.Color.Transparent;
            this.BtnHome.BaseColor = System.Drawing.Color.Aqua;
            this.BtnHome.BorderColor = System.Drawing.Color.Transparent;
            this.BtnHome.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.BtnHome.DownBack = null;
            this.BtnHome.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnHome.ForeColor = System.Drawing.SystemColors.ControlText;
            this.BtnHome.Location = new System.Drawing.Point(162, 12);
            this.BtnHome.MouseBack = null;
            this.BtnHome.Name = "BtnHome";
            this.BtnHome.NormlBack = null;
            this.BtnHome.Size = new System.Drawing.Size(127, 37);
            this.BtnHome.TabIndex = 0;
            this.BtnHome.Text = "Home Position";
            this.BtnHome.UseVisualStyleBackColor = false;
            this.BtnHome.Click += new System.EventHandler(this.BtnHome_Click);
            // 
            // BottomLeftPanel
            // 
            this.BottomLeftPanel.BackColor = System.Drawing.Color.Transparent;
            this.BottomLeftPanel.Controls.Add(this.BottomLeftPanel2);
            this.BottomLeftPanel.Controls.Add(this.BottomLeftPanel1);
            this.BottomLeftPanel.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.BottomLeftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BottomLeftPanel.DownBack = null;
            this.BottomLeftPanel.Location = new System.Drawing.Point(4, 247);
            this.BottomLeftPanel.MouseBack = null;
            this.BottomLeftPanel.Name = "BottomLeftPanel";
            this.BottomLeftPanel.NormlBack = null;
            this.BottomLeftPanel.Size = new System.Drawing.Size(703, 608);
            this.BottomLeftPanel.TabIndex = 9;
            // 
            // BottomLeftPanel2
            // 
            this.BottomLeftPanel2.BackColor = System.Drawing.Color.Transparent;
            this.BottomLeftPanel2.Controls.Add(this.GraphTabControl);
            this.BottomLeftPanel2.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.BottomLeftPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BottomLeftPanel2.DownBack = null;
            this.BottomLeftPanel2.Location = new System.Drawing.Point(0, 0);
            this.BottomLeftPanel2.MouseBack = null;
            this.BottomLeftPanel2.Name = "BottomLeftPanel2";
            this.BottomLeftPanel2.NormlBack = null;
            this.BottomLeftPanel2.Size = new System.Drawing.Size(703, 581);
            this.BottomLeftPanel2.TabIndex = 1;
            // 
            // GraphTabControl
            // 
            this.GraphTabControl.AnimatorType = CCWin.SkinControl.AnimationType.HorizSlide;
            this.GraphTabControl.BackColor = System.Drawing.Color.Gold;
            this.GraphTabControl.CloseRect = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.GraphTabControl.Controls.Add(this.TabIntensity);
            this.GraphTabControl.Controls.Add(this.TabReflectance);
            this.GraphTabControl.Controls.Add(this.tab_MeasurePoint);
            this.GraphTabControl.Controls.Add(this.tab_ComCheck);
            this.GraphTabControl.Controls.Add(this.tab_MainConfig);
            this.GraphTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GraphTabControl.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GraphTabControl.HeadBack = null;
            this.GraphTabControl.ImgTxtOffset = new System.Drawing.Point(0, 0);
            this.GraphTabControl.ItemSize = new System.Drawing.Size(120, 36);
            this.GraphTabControl.Location = new System.Drawing.Point(0, 0);
            this.GraphTabControl.Name = "GraphTabControl";
            this.GraphTabControl.PageArrowDown = ((System.Drawing.Image)(resources.GetObject("GraphTabControl.PageArrowDown")));
            this.GraphTabControl.PageArrowHover = ((System.Drawing.Image)(resources.GetObject("GraphTabControl.PageArrowHover")));
            this.GraphTabControl.PageCloseHover = ((System.Drawing.Image)(resources.GetObject("GraphTabControl.PageCloseHover")));
            this.GraphTabControl.PageCloseNormal = ((System.Drawing.Image)(resources.GetObject("GraphTabControl.PageCloseNormal")));
            this.GraphTabControl.PageDown = ((System.Drawing.Image)(resources.GetObject("GraphTabControl.PageDown")));
            this.GraphTabControl.PageHover = ((System.Drawing.Image)(resources.GetObject("GraphTabControl.PageHover")));
            this.GraphTabControl.PageImagePosition = CCWin.SkinControl.SkinTabControl.ePageImagePosition.Left;
            this.GraphTabControl.PageNorml = null;
            this.GraphTabControl.SelectedIndex = 2;
            this.GraphTabControl.Size = new System.Drawing.Size(703, 581);
            this.GraphTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.GraphTabControl.TabIndex = 0;
            // 
            // TabIntensity
            // 
            this.TabIntensity.BackColor = System.Drawing.Color.White;
            this.TabIntensity.Controls.Add(this.IntensityGraphControl);
            this.TabIntensity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabIntensity.Location = new System.Drawing.Point(0, 36);
            this.TabIntensity.Name = "TabIntensity";
            this.TabIntensity.Size = new System.Drawing.Size(703, 545);
            this.TabIntensity.TabIndex = 0;
            this.TabIntensity.TabItemImage = null;
            this.TabIntensity.Text = "Intensity";
            // 
            // IntensityGraphControl
            // 
            this.IntensityGraphControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IntensityGraphControl.Location = new System.Drawing.Point(0, 0);
            this.IntensityGraphControl.Margin = new System.Windows.Forms.Padding(4);
            this.IntensityGraphControl.Name = "IntensityGraphControl";
            this.IntensityGraphControl.ScrollGrace = 0D;
            this.IntensityGraphControl.ScrollMaxX = 0D;
            this.IntensityGraphControl.ScrollMaxY = 0D;
            this.IntensityGraphControl.ScrollMaxY2 = 0D;
            this.IntensityGraphControl.ScrollMinX = 0D;
            this.IntensityGraphControl.ScrollMinY = 0D;
            this.IntensityGraphControl.ScrollMinY2 = 0D;
            this.IntensityGraphControl.Size = new System.Drawing.Size(703, 545);
            this.IntensityGraphControl.TabIndex = 0;
            this.IntensityGraphControl.UseExtendedPrintDialog = true;
            this.IntensityGraphControl.ContextMenuBuilder += new ZedGraph.ZedGraphControl.ContextMenuBuilderEventHandler(this.IntensityGraphControl_ContextMenuBuilder);
            // 
            // TabReflectance
            // 
            this.TabReflectance.BackColor = System.Drawing.Color.White;
            this.TabReflectance.Controls.Add(this.ReflectanceGraphControl);
            this.TabReflectance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabReflectance.Location = new System.Drawing.Point(0, 36);
            this.TabReflectance.Name = "TabReflectance";
            this.TabReflectance.Size = new System.Drawing.Size(703, 545);
            this.TabReflectance.TabIndex = 1;
            this.TabReflectance.TabItemImage = null;
            this.TabReflectance.Text = "Reflectance";
            // 
            // ReflectanceGraphControl
            // 
            this.ReflectanceGraphControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReflectanceGraphControl.Location = new System.Drawing.Point(0, 0);
            this.ReflectanceGraphControl.Margin = new System.Windows.Forms.Padding(4);
            this.ReflectanceGraphControl.Name = "ReflectanceGraphControl";
            this.ReflectanceGraphControl.ScrollGrace = 0D;
            this.ReflectanceGraphControl.ScrollMaxX = 0D;
            this.ReflectanceGraphControl.ScrollMaxY = 0D;
            this.ReflectanceGraphControl.ScrollMaxY2 = 0D;
            this.ReflectanceGraphControl.ScrollMinX = 0D;
            this.ReflectanceGraphControl.ScrollMinY = 0D;
            this.ReflectanceGraphControl.ScrollMinY2 = 0D;
            this.ReflectanceGraphControl.Size = new System.Drawing.Size(703, 545);
            this.ReflectanceGraphControl.TabIndex = 0;
            this.ReflectanceGraphControl.UseExtendedPrintDialog = true;
            this.ReflectanceGraphControl.ContextMenuBuilder += new ZedGraph.ZedGraphControl.ContextMenuBuilderEventHandler(this.ReflectanceGraphControl_ContextMenuBuilder);
            // 
            // tab_MeasurePoint
            // 
            this.tab_MeasurePoint.AutoScroll = true;
            this.tab_MeasurePoint.BackColor = System.Drawing.Color.White;
            this.tab_MeasurePoint.Controls.Add(this.pnl_FocusUnit);
            this.tab_MeasurePoint.Controls.Add(this.panel2);
            this.tab_MeasurePoint.Controls.Add(this.panel1);
            this.tab_MeasurePoint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab_MeasurePoint.Location = new System.Drawing.Point(0, 36);
            this.tab_MeasurePoint.Name = "tab_MeasurePoint";
            this.tab_MeasurePoint.Size = new System.Drawing.Size(703, 545);
            this.tab_MeasurePoint.TabIndex = 2;
            this.tab_MeasurePoint.TabItemImage = null;
            this.tab_MeasurePoint.Text = "Measure Point";
            // 
            // pnl_FocusUnit
            // 
            this.pnl_FocusUnit.BackColor = System.Drawing.SystemColors.HighlightText;
            this.pnl_FocusUnit.Controls.Add(this.cmb_FcsComPort);
            this.pnl_FocusUnit.Controls.Add(this.skinLabel1);
            this.pnl_FocusUnit.Controls.Add(this.tabControl3);
            this.pnl_FocusUnit.Controls.Add(this.btn_FcsZero);
            this.pnl_FocusUnit.Controls.Add(this.btn_FcsRec);
            this.pnl_FocusUnit.Controls.Add(this.btn_FcsSetNum);
            this.pnl_FocusUnit.Controls.Add(this.lbl_CurFcsPos);
            this.pnl_FocusUnit.Controls.Add(this.label29);
            this.pnl_FocusUnit.Controls.Add(this.label28);
            this.pnl_FocusUnit.Controls.Add(this.cmb_FcsPosNum);
            this.pnl_FocusUnit.Controls.Add(this.label31);
            this.pnl_FocusUnit.ForeColor = System.Drawing.Color.Black;
            this.pnl_FocusUnit.Location = new System.Drawing.Point(7, 422);
            this.pnl_FocusUnit.Name = "pnl_FocusUnit";
            this.pnl_FocusUnit.Size = new System.Drawing.Size(590, 117);
            this.pnl_FocusUnit.TabIndex = 32;
            // 
            // cmb_FcsComPort
            // 
            this.cmb_FcsComPort.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmb_FcsComPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_FcsComPort.FormattingEnabled = true;
            this.cmb_FcsComPort.Location = new System.Drawing.Point(110, 19);
            this.cmb_FcsComPort.Name = "cmb_FcsComPort";
            this.cmb_FcsComPort.Size = new System.Drawing.Size(105, 24);
            this.cmb_FcsComPort.TabIndex = 44;
            this.cmb_FcsComPort.WaterText = "";
            // 
            // skinLabel1
            // 
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel1.Location = new System.Drawing.Point(14, 23);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(86, 20);
            this.skinLabel1.TabIndex = 43;
            this.skinLabel1.Text = "Serial Port:";
            this.skinLabel1.Visible = false;
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPage1);
            this.tabControl3.Controls.Add(this.tabPage2);
            this.tabControl3.Location = new System.Drawing.Point(296, 10);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(287, 104);
            this.tabControl3.TabIndex = 42;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btn_SetFcsPos);
            this.tabPage1.Controls.Add(this.label32);
            this.tabPage1.Controls.Add(this.txt_setFcsPos);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(279, 74);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Point Move";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btn_SetFcsPos
            // 
            this.btn_SetFcsPos.ForeColor = System.Drawing.Color.Black;
            this.btn_SetFcsPos.Location = new System.Drawing.Point(178, 25);
            this.btn_SetFcsPos.Name = "btn_SetFcsPos";
            this.btn_SetFcsPos.Size = new System.Drawing.Size(68, 32);
            this.btn_SetFcsPos.TabIndex = 26;
            this.btn_SetFcsPos.Text = "Move";
            this.btn_SetFcsPos.UseVisualStyleBackColor = true;
            this.btn_SetFcsPos.Click += new System.EventHandler(this.btn_SetFcsPos_Click);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.ForeColor = System.Drawing.Color.Black;
            this.label32.Location = new System.Drawing.Point(6, 33);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(85, 16);
            this.label32.TabIndex = 24;
            this.label32.Text = "Point[mm]";
            // 
            // txt_setFcsPos
            // 
            this.txt_setFcsPos.Location = new System.Drawing.Point(92, 30);
            this.txt_setFcsPos.Name = "txt_setFcsPos";
            this.txt_setFcsPos.Size = new System.Drawing.Size(69, 23);
            this.txt_setFcsPos.TabIndex = 22;
            this.txt_setFcsPos.Text = "0.000";
            this.txt_setFcsPos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btn_FcsStepdown);
            this.tabPage2.Controls.Add(this.btn_FcsStepUp);
            this.tabPage2.Controls.Add(this.txt_FcsStep);
            this.tabPage2.Controls.Add(this.label34);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(279, 74);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Step Move";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btn_FcsStepdown
            // 
            this.btn_FcsStepdown.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_FcsStepdown.ForeColor = System.Drawing.Color.Black;
            this.btn_FcsStepdown.Location = new System.Drawing.Point(164, 37);
            this.btn_FcsStepdown.Name = "btn_FcsStepdown";
            this.btn_FcsStepdown.Size = new System.Drawing.Size(50, 31);
            this.btn_FcsStepdown.TabIndex = 34;
            this.btn_FcsStepdown.Text = "↓";
            this.btn_FcsStepdown.UseVisualStyleBackColor = true;
            this.btn_FcsStepdown.Click += new System.EventHandler(this.btn_FcsStep_Click);
            // 
            // btn_FcsStepUp
            // 
            this.btn_FcsStepUp.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_FcsStepUp.ForeColor = System.Drawing.Color.Black;
            this.btn_FcsStepUp.Location = new System.Drawing.Point(164, 3);
            this.btn_FcsStepUp.Name = "btn_FcsStepUp";
            this.btn_FcsStepUp.Size = new System.Drawing.Size(50, 31);
            this.btn_FcsStepUp.TabIndex = 33;
            this.btn_FcsStepUp.Text = "↑";
            this.btn_FcsStepUp.UseVisualStyleBackColor = true;
            this.btn_FcsStepUp.Click += new System.EventHandler(this.btn_FcsStep_Click);
            // 
            // txt_FcsStep
            // 
            this.txt_FcsStep.Location = new System.Drawing.Point(88, 18);
            this.txt_FcsStep.Name = "txt_FcsStep";
            this.txt_FcsStep.Size = new System.Drawing.Size(59, 23);
            this.txt_FcsStep.TabIndex = 28;
            this.txt_FcsStep.Text = "1.000";
            this.txt_FcsStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.ForeColor = System.Drawing.Color.Black;
            this.label34.Location = new System.Drawing.Point(3, 21);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(81, 16);
            this.label34.TabIndex = 27;
            this.label34.Text = "Step[mm]";
            // 
            // btn_FcsZero
            // 
            this.btn_FcsZero.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_FcsZero.ForeColor = System.Drawing.Color.Black;
            this.btn_FcsZero.Location = new System.Drawing.Point(227, 78);
            this.btn_FcsZero.Name = "btn_FcsZero";
            this.btn_FcsZero.Size = new System.Drawing.Size(63, 25);
            this.btn_FcsZero.TabIndex = 41;
            this.btn_FcsZero.Text = "Zero";
            this.btn_FcsZero.UseVisualStyleBackColor = true;
            this.btn_FcsZero.Click += new System.EventHandler(this.btn_FcsZero_Click);
            // 
            // btn_FcsRec
            // 
            this.btn_FcsRec.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_FcsRec.ForeColor = System.Drawing.Color.Red;
            this.btn_FcsRec.Location = new System.Drawing.Point(227, 10);
            this.btn_FcsRec.Name = "btn_FcsRec";
            this.btn_FcsRec.Size = new System.Drawing.Size(63, 28);
            this.btn_FcsRec.TabIndex = 40;
            this.btn_FcsRec.Text = "Rec";
            this.btn_FcsRec.UseVisualStyleBackColor = true;
            this.btn_FcsRec.Click += new System.EventHandler(this.btn_FcsRec_Click);
            // 
            // btn_FcsSetNum
            // 
            this.btn_FcsSetNum.BackColor = System.Drawing.Color.Transparent;
            this.btn_FcsSetNum.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_FcsSetNum.ForeColor = System.Drawing.Color.Black;
            this.btn_FcsSetNum.Location = new System.Drawing.Point(227, 47);
            this.btn_FcsSetNum.Name = "btn_FcsSetNum";
            this.btn_FcsSetNum.Size = new System.Drawing.Size(63, 25);
            this.btn_FcsSetNum.TabIndex = 39;
            this.btn_FcsSetNum.Text = "Set";
            this.btn_FcsSetNum.UseVisualStyleBackColor = false;
            this.btn_FcsSetNum.Click += new System.EventHandler(this.btn_FcsSetNum_Click);
            // 
            // lbl_CurFcsPos
            // 
            this.lbl_CurFcsPos.BackColor = System.Drawing.Color.Transparent;
            this.lbl_CurFcsPos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_CurFcsPos.ForeColor = System.Drawing.Color.Black;
            this.lbl_CurFcsPos.Location = new System.Drawing.Point(135, 56);
            this.lbl_CurFcsPos.Name = "lbl_CurFcsPos";
            this.lbl_CurFcsPos.Size = new System.Drawing.Size(80, 16);
            this.lbl_CurFcsPos.TabIndex = 35;
            this.lbl_CurFcsPos.Text = "0.000";
            this.lbl_CurFcsPos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.ForeColor = System.Drawing.Color.Black;
            this.label29.Location = new System.Drawing.Point(7, 56);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(127, 16);
            this.label29.TabIndex = 28;
            this.label29.Text = "Current [mm] : ";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(9, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(119, 16);
            this.label28.TabIndex = 25;
            this.label28.Text = "Focus Position";
            // 
            // cmb_FcsPosNum
            // 
            this.cmb_FcsPosNum.FormattingEnabled = true;
            this.cmb_FcsPosNum.Items.AddRange(new object[] {
            "0: Origin",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.cmb_FcsPosNum.Location = new System.Drawing.Point(135, 84);
            this.cmb_FcsPosNum.Name = "cmb_FcsPosNum";
            this.cmb_FcsPosNum.Size = new System.Drawing.Size(80, 24);
            this.cmb_FcsPosNum.TabIndex = 24;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.ForeColor = System.Drawing.Color.Black;
            this.label31.Location = new System.Drawing.Point(7, 87);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(133, 16);
            this.label31.TabIndex = 23;
            this.label31.Text = "Focus position : ";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gold;
            this.panel2.Controls.Add(this.chk_SerchAreaSpread);
            this.panel2.Controls.Add(this.chk_UseFocusSearch);
            this.panel2.Controls.Add(this.cbuseshotnum);
            this.panel2.Controls.Add(this.cbxUseAngleCorrection);
            this.panel2.Controls.Add(this.tlpOperate);
            this.panel2.Controls.Add(this.lbl_PatternNum);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.lbl_PatternRecipe);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.lbl_DispGapY);
            this.panel2.Controls.Add(this.lbl_DispXGap);
            this.panel2.Controls.Add(this.label22);
            this.panel2.Controls.Add(this.label21);
            this.panel2.Controls.Add(this.tabControl2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(7, 135);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(590, 281);
            this.panel2.TabIndex = 30;
            // 
            // chk_SerchAreaSpread
            // 
            this.chk_SerchAreaSpread.AutoSize = true;
            this.chk_SerchAreaSpread.Location = new System.Drawing.Point(212, 255);
            this.chk_SerchAreaSpread.Name = "chk_SerchAreaSpread";
            this.chk_SerchAreaSpread.Size = new System.Drawing.Size(186, 20);
            this.chk_SerchAreaSpread.TabIndex = 46;
            this.chk_SerchAreaSpread.Text = "Pattern Wide Search";
            this.chk_SerchAreaSpread.UseVisualStyleBackColor = true;
            // 
            // chk_UseFocusSearch
            // 
            this.chk_UseFocusSearch.AutoSize = true;
            this.chk_UseFocusSearch.Location = new System.Drawing.Point(17, 255);
            this.chk_UseFocusSearch.Name = "chk_UseFocusSearch";
            this.chk_UseFocusSearch.Size = new System.Drawing.Size(165, 20);
            this.chk_UseFocusSearch.TabIndex = 45;
            this.chk_UseFocusSearch.Text = "Use Focus Search";
            this.chk_UseFocusSearch.UseVisualStyleBackColor = true;
            // 
            // cbuseshotnum
            // 
            this.cbuseshotnum.AutoSize = true;
            this.cbuseshotnum.Checked = true;
            this.cbuseshotnum.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbuseshotnum.Location = new System.Drawing.Point(212, 226);
            this.cbuseshotnum.Name = "cbuseshotnum";
            this.cbuseshotnum.Size = new System.Drawing.Size(127, 20);
            this.cbuseshotnum.TabIndex = 44;
            this.cbuseshotnum.Text = "Use Shotnum";
            this.cbuseshotnum.UseVisualStyleBackColor = true;
            this.cbuseshotnum.CheckedChanged += new System.EventHandler(this.cbuseshotnum_CheckedChanged);
            // 
            // cbxUseAngleCorrection
            // 
            this.cbxUseAngleCorrection.AutoSize = true;
            this.cbxUseAngleCorrection.Checked = true;
            this.cbxUseAngleCorrection.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxUseAngleCorrection.Location = new System.Drawing.Point(17, 226);
            this.cbxUseAngleCorrection.Name = "cbxUseAngleCorrection";
            this.cbxUseAngleCorrection.Size = new System.Drawing.Size(189, 20);
            this.cbxUseAngleCorrection.TabIndex = 43;
            this.cbxUseAngleCorrection.Text = "Use Angle Correction";
            this.cbxUseAngleCorrection.UseVisualStyleBackColor = true;
            this.cbxUseAngleCorrection.CheckedChanged += new System.EventHandler(this.cbxUseAngleCorrection_CheckedChanged);
            // 
            // tlpOperate
            // 
            this.tlpOperate.ColumnCount = 1;
            this.tlpOperate.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpOperate.Controls.Add(this.btnAngleCorrection, 0, 4);
            this.tlpOperate.Controls.Add(this.btn_updateTGMark, 0, 0);
            this.tlpOperate.Controls.Add(this.btnPatternAlign_C, 0, 2);
            this.tlpOperate.Controls.Add(this.btn_PatternAlign, 0, 1);
            this.tlpOperate.Controls.Add(this.btn_AlignerRst, 0, 3);
            this.tlpOperate.Location = new System.Drawing.Point(449, 3);
            this.tlpOperate.Name = "tlpOperate";
            this.tlpOperate.RowCount = 5;
            this.tlpOperate.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpOperate.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpOperate.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpOperate.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpOperate.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpOperate.Size = new System.Drawing.Size(138, 275);
            this.tlpOperate.TabIndex = 42;
            // 
            // btnAngleCorrection
            // 
            this.btnAngleCorrection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAngleCorrection.ForeColor = System.Drawing.Color.Black;
            this.btnAngleCorrection.Location = new System.Drawing.Point(3, 223);
            this.btnAngleCorrection.Name = "btnAngleCorrection";
            this.btnAngleCorrection.Size = new System.Drawing.Size(132, 49);
            this.btnAngleCorrection.TabIndex = 41;
            this.btnAngleCorrection.Text = "Angle correction";
            this.btnAngleCorrection.UseVisualStyleBackColor = true;
            this.btnAngleCorrection.Click += new System.EventHandler(this.btnAngleCorrection_Click);
            // 
            // btn_updateTGMark
            // 
            this.btn_updateTGMark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_updateTGMark.ForeColor = System.Drawing.Color.Black;
            this.btn_updateTGMark.Location = new System.Drawing.Point(3, 3);
            this.btn_updateTGMark.Name = "btn_updateTGMark";
            this.btn_updateTGMark.Size = new System.Drawing.Size(132, 49);
            this.btn_updateTGMark.TabIndex = 32;
            this.btn_updateTGMark.Text = "Update Target Mark";
            this.btn_updateTGMark.UseVisualStyleBackColor = true;
            this.btn_updateTGMark.Click += new System.EventHandler(this.btn_updateTGMark_Click);
            // 
            // btnPatternAlign_C
            // 
            this.btnPatternAlign_C.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPatternAlign_C.ForeColor = System.Drawing.Color.Black;
            this.btnPatternAlign_C.Location = new System.Drawing.Point(3, 113);
            this.btnPatternAlign_C.Name = "btnPatternAlign_C";
            this.btnPatternAlign_C.Size = new System.Drawing.Size(132, 49);
            this.btnPatternAlign_C.TabIndex = 41;
            this.btnPatternAlign_C.Text = "Start Pattern Align(C)";
            this.btnPatternAlign_C.UseVisualStyleBackColor = true;
            this.btnPatternAlign_C.Click += new System.EventHandler(this.btnPatternAlign_C_Click);
            // 
            // btn_PatternAlign
            // 
            this.btn_PatternAlign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_PatternAlign.ForeColor = System.Drawing.Color.Black;
            this.btn_PatternAlign.Location = new System.Drawing.Point(3, 58);
            this.btn_PatternAlign.Name = "btn_PatternAlign";
            this.btn_PatternAlign.Size = new System.Drawing.Size(132, 49);
            this.btn_PatternAlign.TabIndex = 30;
            this.btn_PatternAlign.Text = "Start Pattern Align";
            this.btn_PatternAlign.UseVisualStyleBackColor = true;
            this.btn_PatternAlign.Click += new System.EventHandler(this.btn_PatternAlign_Click);
            // 
            // btn_AlignerRst
            // 
            this.btn_AlignerRst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_AlignerRst.ForeColor = System.Drawing.Color.Black;
            this.btn_AlignerRst.Location = new System.Drawing.Point(3, 168);
            this.btn_AlignerRst.Name = "btn_AlignerRst";
            this.btn_AlignerRst.Size = new System.Drawing.Size(132, 49);
            this.btn_AlignerRst.TabIndex = 29;
            this.btn_AlignerRst.Text = "Aligner Reset";
            this.btn_AlignerRst.UseVisualStyleBackColor = true;
            this.btn_AlignerRst.Click += new System.EventHandler(this.btn_AlignerRst_Click);
            // 
            // lbl_PatternNum
            // 
            this.lbl_PatternNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_PatternNum.Location = new System.Drawing.Point(379, 31);
            this.lbl_PatternNum.Name = "lbl_PatternNum";
            this.lbl_PatternNum.Size = new System.Drawing.Size(47, 16);
            this.lbl_PatternNum.TabIndex = 40;
            this.lbl_PatternNum.Text = "0";
            this.lbl_PatternNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(258, 31);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(115, 16);
            this.label10.TabIndex = 39;
            this.label10.Text = "Pattern Num :";
            // 
            // lbl_PatternRecipe
            // 
            this.lbl_PatternRecipe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_PatternRecipe.Location = new System.Drawing.Point(148, 31);
            this.lbl_PatternRecipe.Name = "lbl_PatternRecipe";
            this.lbl_PatternRecipe.Size = new System.Drawing.Size(104, 16);
            this.lbl_PatternRecipe.TabIndex = 38;
            this.lbl_PatternRecipe.Text = "No Pattern";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 16);
            this.label4.TabIndex = 37;
            this.label4.Text = "Pattern Recipe :";
            // 
            // lbl_DispGapY
            // 
            this.lbl_DispGapY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_DispGapY.Location = new System.Drawing.Point(362, 60);
            this.lbl_DispGapY.Name = "lbl_DispGapY";
            this.lbl_DispGapY.Size = new System.Drawing.Size(64, 16);
            this.lbl_DispGapY.TabIndex = 36;
            this.lbl_DispGapY.Text = "0.000";
            this.lbl_DispGapY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_DispXGap
            // 
            this.lbl_DispXGap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_DispXGap.Location = new System.Drawing.Point(141, 60);
            this.lbl_DispXGap.Name = "lbl_DispXGap";
            this.lbl_DispXGap.Size = new System.Drawing.Size(57, 16);
            this.lbl_DispXGap.TabIndex = 35;
            this.lbl_DispXGap.Text = "0.000";
            this.lbl_DispXGap.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(33, 60);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(102, 16);
            this.label22.TabIndex = 34;
            this.label22.Text = "Gap X[mm] :";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(255, 60);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(101, 16);
            this.label21.TabIndex = 33;
            this.label21.Text = "Gap Y[mm] :";
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tab_Cassete1);
            this.tabControl2.Controls.Add(this.tab_Device);
            this.tabControl2.Controls.Add(this.tab_Angle);
            this.tabControl2.Location = new System.Drawing.Point(17, 102);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(409, 116);
            this.tabControl2.TabIndex = 31;
            // 
            // tab_Cassete1
            // 
            this.tab_Cassete1.Controls.Add(this.txt_AlYtol);
            this.tab_Cassete1.Controls.Add(this.txt_AlXtol);
            this.tab_Cassete1.Controls.Add(this.label24);
            this.tab_Cassete1.Controls.Add(this.label23);
            this.tab_Cassete1.Controls.Add(this.label2);
            this.tab_Cassete1.Controls.Add(this.label3);
            this.tab_Cassete1.Controls.Add(this.txt_C1_Yinit);
            this.tab_Cassete1.Controls.Add(this.txt_C1_Xinit);
            this.tab_Cassete1.Location = new System.Drawing.Point(4, 26);
            this.tab_Cassete1.Name = "tab_Cassete1";
            this.tab_Cassete1.Padding = new System.Windows.Forms.Padding(3);
            this.tab_Cassete1.Size = new System.Drawing.Size(401, 86);
            this.tab_Cassete1.TabIndex = 0;
            this.tab_Cassete1.Text = "Init / Tol";
            this.tab_Cassete1.UseVisualStyleBackColor = true;
            // 
            // txt_AlYtol
            // 
            this.txt_AlYtol.Location = new System.Drawing.Point(328, 50);
            this.txt_AlYtol.Name = "txt_AlYtol";
            this.txt_AlYtol.Size = new System.Drawing.Size(69, 23);
            this.txt_AlYtol.TabIndex = 29;
            this.txt_AlYtol.Text = "0.02";
            this.txt_AlYtol.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_AlXtol
            // 
            this.txt_AlXtol.Location = new System.Drawing.Point(328, 18);
            this.txt_AlXtol.Name = "txt_AlXtol";
            this.txt_AlXtol.Size = new System.Drawing.Size(69, 23);
            this.txt_AlXtol.TabIndex = 28;
            this.txt_AlXtol.Text = "0.02";
            this.txt_AlXtol.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.ForeColor = System.Drawing.Color.Black;
            this.label24.Location = new System.Drawing.Point(221, 53);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(97, 16);
            this.label24.TabIndex = 27;
            this.label24.Text = "Y-Tol[mm] :";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.ForeColor = System.Drawing.Color.Black;
            this.label23.Location = new System.Drawing.Point(221, 21);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(98, 16);
            this.label23.TabIndex = 26;
            this.label23.Text = "X-Tol[mm] :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(20, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 16);
            this.label2.TabIndex = 25;
            this.label2.Text = "Y-init[mm] :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(20, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 16);
            this.label3.TabIndex = 24;
            this.label3.Text = "X-init[mm] :";
            // 
            // txt_C1_Yinit
            // 
            this.txt_C1_Yinit.Location = new System.Drawing.Point(125, 50);
            this.txt_C1_Yinit.Name = "txt_C1_Yinit";
            this.txt_C1_Yinit.Size = new System.Drawing.Size(69, 23);
            this.txt_C1_Yinit.TabIndex = 23;
            this.txt_C1_Yinit.Text = "0.35";
            this.txt_C1_Yinit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_C1_Yinit.TextChanged += new System.EventHandler(this.txt_C1_Yinit_TextChanged);
            // 
            // txt_C1_Xinit
            // 
            this.txt_C1_Xinit.Location = new System.Drawing.Point(125, 18);
            this.txt_C1_Xinit.Name = "txt_C1_Xinit";
            this.txt_C1_Xinit.Size = new System.Drawing.Size(69, 23);
            this.txt_C1_Xinit.TabIndex = 22;
            this.txt_C1_Xinit.Text = "3.3";
            this.txt_C1_Xinit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_C1_Xinit.TextChanged += new System.EventHandler(this.txt_C1_Xinit_TextChanged);
            // 
            // tab_Device
            // 
            this.tab_Device.Controls.Add(this.label17);
            this.tab_Device.Controls.Add(this.label18);
            this.tab_Device.Controls.Add(this.txt_YShot);
            this.tab_Device.Controls.Add(this.txt_XShot);
            this.tab_Device.Controls.Add(this.label15);
            this.tab_Device.Controls.Add(this.label16);
            this.tab_Device.Controls.Add(this.txt_Ypitch);
            this.tab_Device.Controls.Add(this.txt_Xpitch);
            this.tab_Device.Location = new System.Drawing.Point(4, 26);
            this.tab_Device.Name = "tab_Device";
            this.tab_Device.Size = new System.Drawing.Size(401, 86);
            this.tab_Device.TabIndex = 3;
            this.tab_Device.Text = "Device Size";
            this.tab_Device.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(241, 51);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(72, 16);
            this.label17.TabIndex = 33;
            this.label17.Text = "Y-Shot :";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.Black;
            this.label18.Location = new System.Drawing.Point(240, 25);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(73, 16);
            this.label18.TabIndex = 32;
            this.label18.Text = "X-Shot :";
            // 
            // txt_YShot
            // 
            this.txt_YShot.Location = new System.Drawing.Point(319, 49);
            this.txt_YShot.Name = "txt_YShot";
            this.txt_YShot.Size = new System.Drawing.Size(69, 23);
            this.txt_YShot.TabIndex = 31;
            this.txt_YShot.Text = "6";
            this.txt_YShot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_YShot.TextChanged += new System.EventHandler(this.txt_YShot_TextChanged);
            // 
            // txt_XShot
            // 
            this.txt_XShot.Location = new System.Drawing.Point(319, 20);
            this.txt_XShot.Name = "txt_XShot";
            this.txt_XShot.Size = new System.Drawing.Size(69, 23);
            this.txt_XShot.TabIndex = 30;
            this.txt_XShot.Text = "5";
            this.txt_XShot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_XShot.TextChanged += new System.EventHandler(this.txt_XShot_TextChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(20, 52);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(114, 16);
            this.label15.TabIndex = 29;
            this.label15.Text = "Y-Pitch[mm] :";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(20, 23);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(115, 16);
            this.label16.TabIndex = 28;
            this.label16.Text = "X-Pitch[mm] :";
            // 
            // txt_Ypitch
            // 
            this.txt_Ypitch.Location = new System.Drawing.Point(143, 49);
            this.txt_Ypitch.Name = "txt_Ypitch";
            this.txt_Ypitch.Size = new System.Drawing.Size(76, 23);
            this.txt_Ypitch.TabIndex = 27;
            this.txt_Ypitch.Text = "1.16";
            this.txt_Ypitch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Ypitch.TextChanged += new System.EventHandler(this.txt_Ypitch_TextChanged);
            // 
            // txt_Xpitch
            // 
            this.txt_Xpitch.Location = new System.Drawing.Point(143, 20);
            this.txt_Xpitch.Name = "txt_Xpitch";
            this.txt_Xpitch.Size = new System.Drawing.Size(76, 23);
            this.txt_Xpitch.TabIndex = 26;
            this.txt_Xpitch.Text = "1.5";
            this.txt_Xpitch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Xpitch.TextChanged += new System.EventHandler(this.txt_Xpitch_TextChanged);
            // 
            // tab_Angle
            // 
            this.tab_Angle.Controls.Add(this.txt_ShotIntY);
            this.tab_Angle.Controls.Add(this.txt_ShotIntX);
            this.tab_Angle.Controls.Add(this.label27);
            this.tab_Angle.Controls.Add(this.label26);
            this.tab_Angle.Controls.Add(this.label11);
            this.tab_Angle.Controls.Add(this.label14);
            this.tab_Angle.Controls.Add(this.txt_YGrad);
            this.tab_Angle.Controls.Add(this.txt_XGrad);
            this.tab_Angle.Location = new System.Drawing.Point(4, 26);
            this.tab_Angle.Name = "tab_Angle";
            this.tab_Angle.Size = new System.Drawing.Size(401, 86);
            this.tab_Angle.TabIndex = 2;
            this.tab_Angle.Text = "XY Angle";
            this.tab_Angle.UseVisualStyleBackColor = true;
            // 
            // txt_ShotIntY
            // 
            this.txt_ShotIntY.Location = new System.Drawing.Point(349, 46);
            this.txt_ShotIntY.Name = "txt_ShotIntY";
            this.txt_ShotIntY.Size = new System.Drawing.Size(30, 23);
            this.txt_ShotIntY.TabIndex = 33;
            this.txt_ShotIntY.Text = "5";
            this.txt_ShotIntY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_ShotIntX
            // 
            this.txt_ShotIntX.Location = new System.Drawing.Point(349, 14);
            this.txt_ShotIntX.Name = "txt_ShotIntX";
            this.txt_ShotIntX.Size = new System.Drawing.Size(30, 23);
            this.txt_ShotIntX.TabIndex = 32;
            this.txt_ShotIntX.Text = "5";
            this.txt_ShotIntX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.ForeColor = System.Drawing.Color.Black;
            this.label27.Location = new System.Drawing.Point(209, 49);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(142, 16);
            this.label27.TabIndex = 31;
            this.label27.Text = "Y-Shot interval : ";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.ForeColor = System.Drawing.Color.Black;
            this.label26.Location = new System.Drawing.Point(209, 17);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(143, 16);
            this.label26.TabIndex = 30;
            this.label26.Text = "X-Shot interval : ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(10, 49);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(117, 16);
            this.label11.TabIndex = 29;
            this.label11.Text = "Y-Grad[mm] : ";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(10, 17);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(118, 16);
            this.label14.TabIndex = 28;
            this.label14.Text = "X-Grad[mm] : ";
            // 
            // txt_YGrad
            // 
            this.txt_YGrad.Location = new System.Drawing.Point(134, 46);
            this.txt_YGrad.Name = "txt_YGrad";
            this.txt_YGrad.Size = new System.Drawing.Size(55, 23);
            this.txt_YGrad.TabIndex = 27;
            this.txt_YGrad.Text = "0.032";
            this.txt_YGrad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_YGrad.TextChanged += new System.EventHandler(this.txt_YGrad_TextChanged);
            // 
            // txt_XGrad
            // 
            this.txt_XGrad.Location = new System.Drawing.Point(134, 14);
            this.txt_XGrad.Name = "txt_XGrad";
            this.txt_XGrad.Size = new System.Drawing.Size(55, 23);
            this.txt_XGrad.TabIndex = 26;
            this.txt_XGrad.Text = "-0.024";
            this.txt_XGrad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_XGrad.TextChanged += new System.EventHandler(this.txt_XGrad_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(9, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 16);
            this.label1.TabIndex = 28;
            this.label1.Text = "Auto Aligner";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.lbl_Ypv2);
            this.panel1.Controls.Add(this.lbl_XPV2);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Location = new System.Drawing.Point(7, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(590, 126);
            this.panel1.TabIndex = 29;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 16);
            this.label7.TabIndex = 28;
            this.label7.Text = "Stage Move";
            // 
            // lbl_Ypv2
            // 
            this.lbl_Ypv2.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Ypv2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Ypv2.ForeColor = System.Drawing.Color.Black;
            this.lbl_Ypv2.Location = new System.Drawing.Point(85, 91);
            this.lbl_Ypv2.Name = "lbl_Ypv2";
            this.lbl_Ypv2.Size = new System.Drawing.Size(83, 16);
            this.lbl_Ypv2.TabIndex = 34;
            this.lbl_Ypv2.Text = "0";
            this.lbl_Ypv2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_Ypv2.TextChanged += new System.EventHandler(this.txt_YPV2_TextChanged);
            // 
            // lbl_XPV2
            // 
            this.lbl_XPV2.BackColor = System.Drawing.Color.Transparent;
            this.lbl_XPV2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_XPV2.ForeColor = System.Drawing.Color.Black;
            this.lbl_XPV2.Location = new System.Drawing.Point(85, 54);
            this.lbl_XPV2.Name = "lbl_XPV2";
            this.lbl_XPV2.Size = new System.Drawing.Size(83, 16);
            this.lbl_XPV2.TabIndex = 33;
            this.lbl_XPV2.Text = "0";
            this.lbl_XPV2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_XPV2.TextChanged += new System.EventHandler(this.txt_XPV2_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(19, 91);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 16);
            this.label8.TabIndex = 32;
            this.label8.Text = "Y[mm] : ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(19, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 16);
            this.label6.TabIndex = 30;
            this.label6.Text = "X[mm] : ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(3, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 16);
            this.label5.TabIndex = 29;
            this.label5.Text = "Current position";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbp_Point_Move);
            this.tabControl1.Controls.Add(this.tbp_StepMove);
            this.tabControl1.Controls.Add(this.tab_RowsMove);
            this.tabControl1.Location = new System.Drawing.Point(178, 7);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(409, 116);
            this.tabControl1.TabIndex = 27;
            // 
            // tbp_Point_Move
            // 
            this.tbp_Point_Move.Controls.Add(this.btn_XYMovePos);
            this.tbp_Point_Move.Controls.Add(this.lbl_YMovepos);
            this.tbp_Point_Move.Controls.Add(this.lbl_Xmove_pos);
            this.tbp_Point_Move.Controls.Add(this.txt_YMovePos);
            this.tbp_Point_Move.Controls.Add(this.txt_XMovepos);
            this.tbp_Point_Move.Location = new System.Drawing.Point(4, 26);
            this.tbp_Point_Move.Name = "tbp_Point_Move";
            this.tbp_Point_Move.Padding = new System.Windows.Forms.Padding(3);
            this.tbp_Point_Move.Size = new System.Drawing.Size(401, 86);
            this.tbp_Point_Move.TabIndex = 0;
            this.tbp_Point_Move.Text = "Point Move";
            this.tbp_Point_Move.UseVisualStyleBackColor = true;
            // 
            // btn_XYMovePos
            // 
            this.btn_XYMovePos.ForeColor = System.Drawing.Color.Black;
            this.btn_XYMovePos.Location = new System.Drawing.Point(242, 20);
            this.btn_XYMovePos.Name = "btn_XYMovePos";
            this.btn_XYMovePos.Size = new System.Drawing.Size(111, 48);
            this.btn_XYMovePos.TabIndex = 26;
            this.btn_XYMovePos.Text = "Move Pos";
            this.btn_XYMovePos.UseVisualStyleBackColor = true;
            this.btn_XYMovePos.Click += new System.EventHandler(this.btn_XYMovePos_Click);
            // 
            // lbl_YMovepos
            // 
            this.lbl_YMovepos.AutoSize = true;
            this.lbl_YMovepos.ForeColor = System.Drawing.Color.Black;
            this.lbl_YMovepos.Location = new System.Drawing.Point(34, 52);
            this.lbl_YMovepos.Name = "lbl_YMovepos";
            this.lbl_YMovepos.Size = new System.Drawing.Size(102, 16);
            this.lbl_YMovepos.TabIndex = 25;
            this.lbl_YMovepos.Text = "Y-point[mm]";
            // 
            // lbl_Xmove_pos
            // 
            this.lbl_Xmove_pos.AutoSize = true;
            this.lbl_Xmove_pos.ForeColor = System.Drawing.Color.Black;
            this.lbl_Xmove_pos.Location = new System.Drawing.Point(34, 23);
            this.lbl_Xmove_pos.Name = "lbl_Xmove_pos";
            this.lbl_Xmove_pos.Size = new System.Drawing.Size(103, 16);
            this.lbl_Xmove_pos.TabIndex = 24;
            this.lbl_Xmove_pos.Text = "X-point[mm]";
            // 
            // txt_YMovePos
            // 
            this.txt_YMovePos.Location = new System.Drawing.Point(143, 50);
            this.txt_YMovePos.Name = "txt_YMovePos";
            this.txt_YMovePos.Size = new System.Drawing.Size(69, 23);
            this.txt_YMovePos.TabIndex = 23;
            this.txt_YMovePos.Text = "100";
            this.txt_YMovePos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_XMovepos
            // 
            this.txt_XMovepos.Location = new System.Drawing.Point(143, 21);
            this.txt_XMovepos.Name = "txt_XMovepos";
            this.txt_XMovepos.Size = new System.Drawing.Size(69, 23);
            this.txt_XMovepos.TabIndex = 22;
            this.txt_XMovepos.Text = "100";
            this.txt_XMovepos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbp_StepMove
            // 
            this.tbp_StepMove.Controls.Add(this.btn_MoveStep);
            this.tbp_StepMove.Controls.Add(this.txt_Ystpsv);
            this.tbp_StepMove.Controls.Add(this.lbl_YStep);
            this.tbp_StepMove.Controls.Add(this.txt_Xstpsv);
            this.tbp_StepMove.Controls.Add(this.lbl_XStep);
            this.tbp_StepMove.Location = new System.Drawing.Point(4, 26);
            this.tbp_StepMove.Name = "tbp_StepMove";
            this.tbp_StepMove.Padding = new System.Windows.Forms.Padding(3);
            this.tbp_StepMove.Size = new System.Drawing.Size(401, 86);
            this.tbp_StepMove.TabIndex = 1;
            this.tbp_StepMove.Text = "Step Move";
            this.tbp_StepMove.UseVisualStyleBackColor = true;
            // 
            // btn_MoveStep
            // 
            this.btn_MoveStep.ForeColor = System.Drawing.Color.Black;
            this.btn_MoveStep.Location = new System.Drawing.Point(262, 22);
            this.btn_MoveStep.Name = "btn_MoveStep";
            this.btn_MoveStep.Size = new System.Drawing.Size(111, 48);
            this.btn_MoveStep.TabIndex = 31;
            this.btn_MoveStep.Text = "Move Step";
            this.btn_MoveStep.UseVisualStyleBackColor = true;
            this.btn_MoveStep.Click += new System.EventHandler(this.btn_MoveStep_Click);
            // 
            // txt_Ystpsv
            // 
            this.txt_Ystpsv.Location = new System.Drawing.Point(140, 52);
            this.txt_Ystpsv.Name = "txt_Ystpsv";
            this.txt_Ystpsv.Size = new System.Drawing.Size(87, 23);
            this.txt_Ystpsv.TabIndex = 30;
            this.txt_Ystpsv.Text = "1";
            this.txt_Ystpsv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbl_YStep
            // 
            this.lbl_YStep.AutoSize = true;
            this.lbl_YStep.ForeColor = System.Drawing.Color.Black;
            this.lbl_YStep.Location = new System.Drawing.Point(34, 52);
            this.lbl_YStep.Name = "lbl_YStep";
            this.lbl_YStep.Size = new System.Drawing.Size(100, 16);
            this.lbl_YStep.TabIndex = 29;
            this.lbl_YStep.Text = "Y-Step[mm]";
            // 
            // txt_Xstpsv
            // 
            this.txt_Xstpsv.Location = new System.Drawing.Point(141, 19);
            this.txt_Xstpsv.Name = "txt_Xstpsv";
            this.txt_Xstpsv.Size = new System.Drawing.Size(86, 23);
            this.txt_Xstpsv.TabIndex = 28;
            this.txt_Xstpsv.Text = "1";
            this.txt_Xstpsv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbl_XStep
            // 
            this.lbl_XStep.AutoSize = true;
            this.lbl_XStep.ForeColor = System.Drawing.Color.Black;
            this.lbl_XStep.Location = new System.Drawing.Point(34, 22);
            this.lbl_XStep.Name = "lbl_XStep";
            this.lbl_XStep.Size = new System.Drawing.Size(101, 16);
            this.lbl_XStep.TabIndex = 27;
            this.lbl_XStep.Text = "X-Step[mm]";
            // 
            // tab_RowsMove
            // 
            this.tab_RowsMove.Controls.Add(this.btnRight);
            this.tab_RowsMove.Controls.Add(this.btnLeft);
            this.tab_RowsMove.Controls.Add(this.btnDown);
            this.tab_RowsMove.Controls.Add(this.btnUp);
            this.tab_RowsMove.Controls.Add(this.txbxStepLength);
            this.tab_RowsMove.Controls.Add(this.label25);
            this.tab_RowsMove.Location = new System.Drawing.Point(4, 26);
            this.tab_RowsMove.Name = "tab_RowsMove";
            this.tab_RowsMove.Size = new System.Drawing.Size(401, 86);
            this.tab_RowsMove.TabIndex = 3;
            this.tab_RowsMove.Text = "RowsMove";
            this.tab_RowsMove.UseVisualStyleBackColor = true;
            // 
            // btnRight
            // 
            this.btnRight.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRight.ForeColor = System.Drawing.Color.Black;
            this.btnRight.Location = new System.Drawing.Point(316, 28);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(50, 33);
            this.btnRight.TabIndex = 35;
            this.btnRight.Text = "→";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btn_RowsMove_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLeft.ForeColor = System.Drawing.Color.Black;
            this.btnLeft.Location = new System.Drawing.Point(195, 28);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(50, 33);
            this.btnLeft.TabIndex = 34;
            this.btnLeft.Text = "←";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btn_RowsMove_Click);
            // 
            // btnDown
            // 
            this.btnDown.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDown.ForeColor = System.Drawing.Color.Black;
            this.btnDown.Location = new System.Drawing.Point(255, 45);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(50, 33);
            this.btnDown.TabIndex = 33;
            this.btnDown.Text = "↓";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btn_RowsMove_Click);
            // 
            // btnUp
            // 
            this.btnUp.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUp.ForeColor = System.Drawing.Color.Black;
            this.btnUp.Location = new System.Drawing.Point(255, 8);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(50, 33);
            this.btnUp.TabIndex = 32;
            this.btnUp.Text = "↑";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btn_RowsMove_Click);
            // 
            // txbxStepLength
            // 
            this.txbxStepLength.Location = new System.Drawing.Point(88, 29);
            this.txbxStepLength.Name = "txbxStepLength";
            this.txbxStepLength.Size = new System.Drawing.Size(86, 23);
            this.txbxStepLength.TabIndex = 30;
            this.txbxStepLength.Text = "0.1";
            this.txbxStepLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.ForeColor = System.Drawing.Color.Black;
            this.label25.Location = new System.Drawing.Point(3, 32);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(81, 16);
            this.label25.TabIndex = 29;
            this.label25.Text = "Step[mm]";
            // 
            // tab_ComCheck
            // 
            this.tab_ComCheck.BackColor = System.Drawing.Color.White;
            this.tab_ComCheck.Controls.Add(this.label20);
            this.tab_ComCheck.Controls.Add(this.txt_AlinRcvCmd);
            this.tab_ComCheck.Controls.Add(this.txt_Alinsendcmd);
            this.tab_ComCheck.Controls.Add(this.label19);
            this.tab_ComCheck.Controls.Add(this.lbl_Recipiinfo);
            this.tab_ComCheck.Controls.Add(this.txt_Recipeinfo);
            this.tab_ComCheck.Controls.Add(this.lbl_Sendcmd);
            this.tab_ComCheck.Controls.Add(this.lbl_Rcvcmd);
            this.tab_ComCheck.Controls.Add(this.txt_senddata);
            this.tab_ComCheck.Controls.Add(this.txt_rcvdata);
            this.tab_ComCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab_ComCheck.Location = new System.Drawing.Point(0, 36);
            this.tab_ComCheck.Name = "tab_ComCheck";
            this.tab_ComCheck.Size = new System.Drawing.Size(703, 545);
            this.tab_ComCheck.TabIndex = 3;
            this.tab_ComCheck.TabItemImage = null;
            this.tab_ComCheck.Text = "COM Check";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(337, 384);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(217, 16);
            this.label20.TabIndex = 9;
            this.label20.Text = "Recieve Command(Aligner )";
            // 
            // txt_AlinRcvCmd
            // 
            this.txt_AlinRcvCmd.Location = new System.Drawing.Point(330, 403);
            this.txt_AlinRcvCmd.Name = "txt_AlinRcvCmd";
            this.txt_AlinRcvCmd.Size = new System.Drawing.Size(357, 97);
            this.txt_AlinRcvCmd.TabIndex = 8;
            this.txt_AlinRcvCmd.Text = "";
            // 
            // txt_Alinsendcmd
            // 
            this.txt_Alinsendcmd.Location = new System.Drawing.Point(10, 403);
            this.txt_Alinsendcmd.Name = "txt_Alinsendcmd";
            this.txt_Alinsendcmd.Size = new System.Drawing.Size(310, 97);
            this.txt_Alinsendcmd.TabIndex = 7;
            this.txt_Alinsendcmd.Text = "";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(7, 384);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(195, 16);
            this.label19.TabIndex = 6;
            this.label19.Text = "Send Command(Aligner )";
            // 
            // lbl_Recipiinfo
            // 
            this.lbl_Recipiinfo.AutoSize = true;
            this.lbl_Recipiinfo.Location = new System.Drawing.Point(482, 15);
            this.lbl_Recipiinfo.Name = "lbl_Recipiinfo";
            this.lbl_Recipiinfo.Size = new System.Drawing.Size(174, 16);
            this.lbl_Recipiinfo.TabIndex = 5;
            this.lbl_Recipiinfo.Text = "Auto-Measure setting";
            // 
            // txt_Recipeinfo
            // 
            this.txt_Recipeinfo.Location = new System.Drawing.Point(475, 44);
            this.txt_Recipeinfo.Name = "txt_Recipeinfo";
            this.txt_Recipeinfo.Size = new System.Drawing.Size(212, 298);
            this.txt_Recipeinfo.TabIndex = 4;
            this.txt_Recipeinfo.Text = "";
            // 
            // lbl_Sendcmd
            // 
            this.lbl_Sendcmd.AutoSize = true;
            this.lbl_Sendcmd.Location = new System.Drawing.Point(7, 190);
            this.lbl_Sendcmd.Name = "lbl_Sendcmd";
            this.lbl_Sendcmd.Size = new System.Drawing.Size(125, 16);
            this.lbl_Sendcmd.TabIndex = 3;
            this.lbl_Sendcmd.Text = "Send Command";
            // 
            // lbl_Rcvcmd
            // 
            this.lbl_Rcvcmd.AutoSize = true;
            this.lbl_Rcvcmd.Location = new System.Drawing.Point(14, 15);
            this.lbl_Rcvcmd.Name = "lbl_Rcvcmd";
            this.lbl_Rcvcmd.Size = new System.Drawing.Size(147, 16);
            this.lbl_Rcvcmd.TabIndex = 2;
            this.lbl_Rcvcmd.Text = "Recieve Command";
            // 
            // txt_senddata
            // 
            this.txt_senddata.Location = new System.Drawing.Point(0, 209);
            this.txt_senddata.Name = "txt_senddata";
            this.txt_senddata.Size = new System.Drawing.Size(454, 133);
            this.txt_senddata.TabIndex = 1;
            this.txt_senddata.Text = "";
            // 
            // txt_rcvdata
            // 
            this.txt_rcvdata.Location = new System.Drawing.Point(0, 44);
            this.txt_rcvdata.Name = "txt_rcvdata";
            this.txt_rcvdata.Size = new System.Drawing.Size(454, 127);
            this.txt_rcvdata.TabIndex = 0;
            this.txt_rcvdata.Text = "";
            // 
            // tab_MainConfig
            // 
            this.tab_MainConfig.BackColor = System.Drawing.Color.White;
            this.tab_MainConfig.Controls.Add(this.pnl_Stgsize);
            this.tab_MainConfig.Controls.Add(this.pnl_Measure_origin);
            this.tab_MainConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab_MainConfig.Enabled = false;
            this.tab_MainConfig.Location = new System.Drawing.Point(0, 36);
            this.tab_MainConfig.Name = "tab_MainConfig";
            this.tab_MainConfig.Size = new System.Drawing.Size(703, 545);
            this.tab_MainConfig.TabIndex = 4;
            this.tab_MainConfig.TabItemImage = null;
            this.tab_MainConfig.Text = "Configuration";
            // 
            // pnl_Stgsize
            // 
            this.pnl_Stgsize.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.pnl_Stgsize.Controls.Add(this.label9);
            this.pnl_Stgsize.Controls.Add(this.btn_Limitset);
            this.pnl_Stgsize.Controls.Add(this.txt_Ylimit);
            this.pnl_Stgsize.Controls.Add(this.txt_Xlimit);
            this.pnl_Stgsize.Controls.Add(this.lbl_YLimit);
            this.pnl_Stgsize.Controls.Add(this.lbl_Xlimit);
            this.pnl_Stgsize.Controls.Add(this.cmb_Stgsize);
            this.pnl_Stgsize.Controls.Add(this.lbl_StgSize);
            this.pnl_Stgsize.ForeColor = System.Drawing.Color.Black;
            this.pnl_Stgsize.Location = new System.Drawing.Point(7, 159);
            this.pnl_Stgsize.Name = "pnl_Stgsize";
            this.pnl_Stgsize.Size = new System.Drawing.Size(282, 117);
            this.pnl_Stgsize.TabIndex = 29;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 16);
            this.label9.TabIndex = 25;
            this.label9.Text = "Stage Limit";
            // 
            // btn_Limitset
            // 
            this.btn_Limitset.ForeColor = System.Drawing.Color.Black;
            this.btn_Limitset.Location = new System.Drawing.Point(182, 54);
            this.btn_Limitset.Name = "btn_Limitset";
            this.btn_Limitset.Size = new System.Drawing.Size(95, 48);
            this.btn_Limitset.TabIndex = 29;
            this.btn_Limitset.Text = "Limit Set";
            this.btn_Limitset.UseVisualStyleBackColor = true;
            // 
            // txt_Ylimit
            // 
            this.txt_Ylimit.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txt_Ylimit.Enabled = false;
            this.txt_Ylimit.Location = new System.Drawing.Point(128, 82);
            this.txt_Ylimit.Name = "txt_Ylimit";
            this.txt_Ylimit.Size = new System.Drawing.Size(47, 23);
            this.txt_Ylimit.TabIndex = 28;
            this.txt_Ylimit.Text = "200";
            this.txt_Ylimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Xlimit
            // 
            this.txt_Xlimit.Enabled = false;
            this.txt_Xlimit.Location = new System.Drawing.Point(128, 50);
            this.txt_Xlimit.Name = "txt_Xlimit";
            this.txt_Xlimit.Size = new System.Drawing.Size(47, 23);
            this.txt_Xlimit.TabIndex = 27;
            this.txt_Xlimit.Text = "200";
            this.txt_Xlimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbl_YLimit
            // 
            this.lbl_YLimit.AutoSize = true;
            this.lbl_YLimit.ForeColor = System.Drawing.Color.Black;
            this.lbl_YLimit.Location = new System.Drawing.Point(15, 82);
            this.lbl_YLimit.Name = "lbl_YLimit";
            this.lbl_YLimit.Size = new System.Drawing.Size(102, 16);
            this.lbl_YLimit.TabIndex = 26;
            this.lbl_YLimit.Text = "Y-Limit[mm]";
            // 
            // lbl_Xlimit
            // 
            this.lbl_Xlimit.AutoSize = true;
            this.lbl_Xlimit.ForeColor = System.Drawing.Color.Black;
            this.lbl_Xlimit.Location = new System.Drawing.Point(15, 50);
            this.lbl_Xlimit.Name = "lbl_Xlimit";
            this.lbl_Xlimit.Size = new System.Drawing.Size(107, 16);
            this.lbl_Xlimit.TabIndex = 25;
            this.lbl_Xlimit.Text = "X-Liimit[mm]";
            // 
            // cmb_Stgsize
            // 
            this.cmb_Stgsize.FormattingEnabled = true;
            this.cmb_Stgsize.Items.AddRange(new object[] {
            "Select Stage Size",
            "8inch",
            "12inch",
            "other"});
            this.cmb_Stgsize.Location = new System.Drawing.Point(119, 16);
            this.cmb_Stgsize.Name = "cmb_Stgsize";
            this.cmb_Stgsize.Size = new System.Drawing.Size(121, 24);
            this.cmb_Stgsize.TabIndex = 24;
            // 
            // lbl_StgSize
            // 
            this.lbl_StgSize.AutoSize = true;
            this.lbl_StgSize.ForeColor = System.Drawing.Color.Black;
            this.lbl_StgSize.Location = new System.Drawing.Point(24, 19);
            this.lbl_StgSize.Name = "lbl_StgSize";
            this.lbl_StgSize.Size = new System.Drawing.Size(89, 16);
            this.lbl_StgSize.TabIndex = 23;
            this.lbl_StgSize.Text = "Stage Size";
            // 
            // pnl_Measure_origin
            // 
            this.pnl_Measure_origin.BackColor = System.Drawing.Color.Bisque;
            this.pnl_Measure_origin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnl_Measure_origin.Controls.Add(this.lbl_YHomepv);
            this.pnl_Measure_origin.Controls.Add(this.lbl_XHomepv);
            this.pnl_Measure_origin.Controls.Add(this.label12);
            this.pnl_Measure_origin.Controls.Add(this.label13);
            this.pnl_Measure_origin.Controls.Add(this.btn_MeasOrgSet);
            this.pnl_Measure_origin.Controls.Add(this.txt_Ymeasorg_sv);
            this.pnl_Measure_origin.Controls.Add(this.txt_Xmeasorg_sv);
            this.pnl_Measure_origin.Controls.Add(this.lbl_YMeasOrg);
            this.pnl_Measure_origin.Controls.Add(this.lbl_Xmeasorg);
            this.pnl_Measure_origin.ForeColor = System.Drawing.Color.Black;
            this.pnl_Measure_origin.Location = new System.Drawing.Point(311, 160);
            this.pnl_Measure_origin.Name = "pnl_Measure_origin";
            this.pnl_Measure_origin.Size = new System.Drawing.Size(302, 118);
            this.pnl_Measure_origin.TabIndex = 28;
            // 
            // lbl_YHomepv
            // 
            this.lbl_YHomepv.BackColor = System.Drawing.Color.Transparent;
            this.lbl_YHomepv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_YHomepv.ForeColor = System.Drawing.Color.Black;
            this.lbl_YHomepv.Location = new System.Drawing.Point(227, 66);
            this.lbl_YHomepv.Name = "lbl_YHomepv";
            this.lbl_YHomepv.Size = new System.Drawing.Size(68, 21);
            this.lbl_YHomepv.TabIndex = 36;
            this.lbl_YHomepv.Text = "0";
            this.lbl_YHomepv.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_XHomepv
            // 
            this.lbl_XHomepv.BackColor = System.Drawing.Color.Transparent;
            this.lbl_XHomepv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_XHomepv.ForeColor = System.Drawing.Color.Black;
            this.lbl_XHomepv.Location = new System.Drawing.Point(227, 34);
            this.lbl_XHomepv.Name = "lbl_XHomepv";
            this.lbl_XHomepv.Size = new System.Drawing.Size(68, 17);
            this.lbl_XHomepv.TabIndex = 35;
            this.lbl_XHomepv.Text = "0";
            this.lbl_XHomepv.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(168, 13);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(131, 16);
            this.label12.TabIndex = 12;
            this.label12.Text = "Current Home : ";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(157, 16);
            this.label13.TabIndex = 11;
            this.label13.Text = "Measurement Home";
            // 
            // btn_MeasOrgSet
            // 
            this.btn_MeasOrgSet.ForeColor = System.Drawing.Color.Black;
            this.btn_MeasOrgSet.Location = new System.Drawing.Point(82, 90);
            this.btn_MeasOrgSet.Name = "btn_MeasOrgSet";
            this.btn_MeasOrgSet.Size = new System.Drawing.Size(203, 26);
            this.btn_MeasOrgSet.TabIndex = 10;
            this.btn_MeasOrgSet.Text = "Measure Home Set";
            this.btn_MeasOrgSet.UseVisualStyleBackColor = true;
            // 
            // txt_Ymeasorg_sv
            // 
            this.txt_Ymeasorg_sv.Location = new System.Drawing.Point(112, 64);
            this.txt_Ymeasorg_sv.Name = "txt_Ymeasorg_sv";
            this.txt_Ymeasorg_sv.Size = new System.Drawing.Size(77, 23);
            this.txt_Ymeasorg_sv.TabIndex = 9;
            this.txt_Ymeasorg_sv.Text = "0";
            this.txt_Ymeasorg_sv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Xmeasorg_sv
            // 
            this.txt_Xmeasorg_sv.Location = new System.Drawing.Point(112, 32);
            this.txt_Xmeasorg_sv.Name = "txt_Xmeasorg_sv";
            this.txt_Xmeasorg_sv.Size = new System.Drawing.Size(77, 23);
            this.txt_Xmeasorg_sv.TabIndex = 8;
            this.txt_Xmeasorg_sv.Text = "0";
            this.txt_Xmeasorg_sv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbl_YMeasOrg
            // 
            this.lbl_YMeasOrg.AutoSize = true;
            this.lbl_YMeasOrg.ForeColor = System.Drawing.Color.Black;
            this.lbl_YMeasOrg.Location = new System.Drawing.Point(3, 64);
            this.lbl_YMeasOrg.Name = "lbl_YMeasOrg";
            this.lbl_YMeasOrg.Size = new System.Drawing.Size(106, 16);
            this.lbl_YMeasOrg.TabIndex = 7;
            this.lbl_YMeasOrg.Text = "Y-Home[mm]";
            // 
            // lbl_Xmeasorg
            // 
            this.lbl_Xmeasorg.AutoSize = true;
            this.lbl_Xmeasorg.ForeColor = System.Drawing.Color.Black;
            this.lbl_Xmeasorg.Location = new System.Drawing.Point(3, 35);
            this.lbl_Xmeasorg.Name = "lbl_Xmeasorg";
            this.lbl_Xmeasorg.Size = new System.Drawing.Size(107, 16);
            this.lbl_Xmeasorg.TabIndex = 6;
            this.lbl_Xmeasorg.Text = "X-Home[mm]";
            // 
            // BottomLeftPanel1
            // 
            this.BottomLeftPanel1.BackColor = System.Drawing.Color.Transparent;
            this.BottomLeftPanel1.Controls.Add(this.Status);
            this.BottomLeftPanel1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.BottomLeftPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomLeftPanel1.DownBack = null;
            this.BottomLeftPanel1.Location = new System.Drawing.Point(0, 581);
            this.BottomLeftPanel1.MouseBack = null;
            this.BottomLeftPanel1.Name = "BottomLeftPanel1";
            this.BottomLeftPanel1.NormlBack = null;
            this.BottomLeftPanel1.Size = new System.Drawing.Size(703, 27);
            this.BottomLeftPanel1.TabIndex = 0;
            // 
            // Status
            // 
            this.Status.AllowDrop = true;
            this.Status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Status.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.Status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mStatusStripStatus,
            this.mStatusStripTxtReceived});
            this.Status.Location = new System.Drawing.Point(0, 0);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(703, 27);
            this.Status.TabIndex = 0;
            this.Status.Text = "statusStrip1";
            // 
            // mStatusStripStatus
            // 
            this.mStatusStripStatus.AutoSize = false;
            this.mStatusStripStatus.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.mStatusStripStatus.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.mStatusStripStatus.ForeColor = System.Drawing.SystemColors.Highlight;
            this.mStatusStripStatus.Name = "mStatusStripStatus";
            this.mStatusStripStatus.Size = new System.Drawing.Size(400, 22);
            this.mStatusStripStatus.Text = "Status";
            this.mStatusStripStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mStatusStripTxtReceived
            // 
            this.mStatusStripTxtReceived.AutoSize = false;
            this.mStatusStripTxtReceived.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.mStatusStripTxtReceived.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.mStatusStripTxtReceived.ForeColor = System.Drawing.SystemColors.Highlight;
            this.mStatusStripTxtReceived.Name = "mStatusStripTxtReceived";
            this.mStatusStripTxtReceived.Size = new System.Drawing.Size(400, 22);
            this.mStatusStripTxtReceived.Text = "AnswerReceived";
            this.mStatusStripTxtReceived.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TimerForRefresh
            // 
            this.TimerForRefresh.Interval = 200;
            this.TimerForRefresh.Tick += new System.EventHandler(this.TimerForRefresh_Tick);
            // 
            // Menu
            // 
            this.Menu.Arrow = System.Drawing.Color.Black;
            this.Menu.Back = System.Drawing.Color.White;
            this.Menu.BackColor = System.Drawing.Color.LightSeaGreen;
            this.Menu.BackRadius = 4;
            this.Menu.BackRectangle = new System.Drawing.Rectangle(10, 10, 10, 10);
            this.Menu.Base = System.Drawing.Color.LightCoral;
            this.Menu.BaseFore = System.Drawing.Color.Black;
            this.Menu.BaseForeAnamorphosis = false;
            this.Menu.BaseForeAnamorphosisBorder = 4;
            this.Menu.BaseForeAnamorphosisColor = System.Drawing.Color.White;
            this.Menu.BaseHoverFore = System.Drawing.Color.White;
            this.Menu.BaseItemAnamorphosis = true;
            this.Menu.BaseItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.Menu.BaseItemBorderShow = true;
            this.Menu.BaseItemDown = ((System.Drawing.Image)(resources.GetObject("Menu.BaseItemDown")));
            this.Menu.BaseItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.Menu.BaseItemMouse = ((System.Drawing.Image)(resources.GetObject("Menu.BaseItemMouse")));
            this.Menu.BaseItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.Menu.BaseItemRadius = 4;
            this.Menu.BaseItemRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.Menu.BaseItemSplitter = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.Menu.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.Menu.Enabled = false;
            this.Menu.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Menu.Fore = System.Drawing.Color.Black;
            this.Menu.HoverFore = System.Drawing.Color.White;
            this.Menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.Menu.ItemAnamorphosis = true;
            this.Menu.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.Menu.ItemBorderShow = true;
            this.Menu.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.Menu.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.Menu.ItemRadius = 4;
            this.Menu.ItemRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.configurationToolStripMenuItem1});
            this.Menu.Location = new System.Drawing.Point(4, 28);
            this.Menu.Name = "Menu";
            this.Menu.RadiusStyle = CCWin.SkinClass.RoundStyle.All;
            this.Menu.Size = new System.Drawing.Size(1163, 24);
            this.Menu.SkinAllColor = true;
            this.Menu.TabIndex = 3;
            this.Menu.TitleAnamorphosis = true;
            this.Menu.TitleColor = System.Drawing.Color.LightSeaGreen;
            this.Menu.TitleRadius = 4;
            this.Menu.TitleRadiusStyle = CCWin.SkinClass.RoundStyle.All;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openCoordinatesTableToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openCoordinatesTableToolStripMenuItem
            // 
            this.openCoordinatesTableToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openCoordinatesTableToolStripMenuItem.Image")));
            this.openCoordinatesTableToolStripMenuItem.Name = "openCoordinatesTableToolStripMenuItem";
            this.openCoordinatesTableToolStripMenuItem.Size = new System.Drawing.Size(119, 26);
            this.openCoordinatesTableToolStripMenuItem.Text = "Open ...";
            this.openCoordinatesTableToolStripMenuItem.Click += new System.EventHandler(this.openCoordinatesTableToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ThinFilmModelToolStripMenuItem,
            this.singleMeasureToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.settingsToolStripMenuItem.Text = "Measure";
            // 
            // ThinFilmModelToolStripMenuItem
            // 
            this.ThinFilmModelToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ThinFilmModelToolStripMenuItem.Image")));
            this.ThinFilmModelToolStripMenuItem.Name = "ThinFilmModelToolStripMenuItem";
            this.ThinFilmModelToolStripMenuItem.Size = new System.Drawing.Size(166, 26);
            this.ThinFilmModelToolStripMenuItem.Text = "Thin Film Recipe";
            this.ThinFilmModelToolStripMenuItem.Click += new System.EventHandler(this.ThinFilmModelToolStripMenuItem_Click);
            // 
            // singleMeasureToolStripMenuItem
            // 
            this.singleMeasureToolStripMenuItem.Name = "singleMeasureToolStripMenuItem";
            this.singleMeasureToolStripMenuItem.Size = new System.Drawing.Size(166, 26);
            this.singleMeasureToolStripMenuItem.Text = "Single Measure";
            this.singleMeasureToolStripMenuItem.Click += new System.EventHandler(this.singleMeasureToolStripMenuItem_Click);
            // 
            // configurationToolStripMenuItem1
            // 
            this.configurationToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.spectrometerConfigurationToolStripMenuItem,
            this.devicePatternToolStripMenuItem,
            this.logTestDebugToolStripMenuItem,
            this.cmdTestDebugToolStripMenuItem});
            this.configurationToolStripMenuItem1.Name = "configurationToolStripMenuItem1";
            this.configurationToolStripMenuItem1.Size = new System.Drawing.Size(92, 20);
            this.configurationToolStripMenuItem1.Text = "Configuration";
            // 
            // spectrometerConfigurationToolStripMenuItem
            // 
            this.spectrometerConfigurationToolStripMenuItem.Name = "spectrometerConfigurationToolStripMenuItem";
            this.spectrometerConfigurationToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.spectrometerConfigurationToolStripMenuItem.Text = "Spectrometer";
            this.spectrometerConfigurationToolStripMenuItem.Click += new System.EventHandler(this.spectrometerConfigurationToolStripMenuItem_Click);
            // 
            // devicePatternToolStripMenuItem
            // 
            this.devicePatternToolStripMenuItem.Name = "devicePatternToolStripMenuItem";
            this.devicePatternToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.devicePatternToolStripMenuItem.Text = "Device Pattern";
            this.devicePatternToolStripMenuItem.Click += new System.EventHandler(this.devicePatternToolStripMenuItem_Click);
            // 
            // logTestDebugToolStripMenuItem
            // 
            this.logTestDebugToolStripMenuItem.Name = "logTestDebugToolStripMenuItem";
            this.logTestDebugToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.logTestDebugToolStripMenuItem.Text = "LogTest(Debug)";
            this.logTestDebugToolStripMenuItem.Click += new System.EventHandler(this.logTestDebugToolStripMenuItem_Click);
            // 
            // cmdTestDebugToolStripMenuItem
            // 
            this.cmdTestDebugToolStripMenuItem.Name = "cmdTestDebugToolStripMenuItem";
            this.cmdTestDebugToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cmdTestDebugToolStripMenuItem.Text = "CmdTest(Debug)";
            this.cmdTestDebugToolStripMenuItem.Click += new System.EventHandler(this.cmdTestDebugToolStripMenuItem_Click);
            // 
            // StageForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1171, 859);
            this.Controls.Add(this.BottomLeftPanel);
            this.Controls.Add(this.TopLeftPanel3);
            this.Controls.Add(this.TopLeftPanel2);
            this.Controls.Add(this.TopLeftPanel);
            this.Controls.Add(this.RightPanel);
            this.Controls.Add(this.Menu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.Menu;
            this.MaximizeBox = false;
            this.Name = "StageForm";
            this.Text = "Thin Film Thickness";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StageForm_FormClosing);
            this.Load += new System.EventHandler(this.StageForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).EndInit();
            this.RightPanel.ResumeLayout(false);
            this.BottomRightPanel.ResumeLayout(false);
            this.TopRightPanel.ResumeLayout(false);
            this.TopLeftPanel.ResumeLayout(false);
            this.TopLeftPanel2.ResumeLayout(false);
            this.TopLeftPanel3.ResumeLayout(false);
            this.BottomLeftPanel.ResumeLayout(false);
            this.BottomLeftPanel2.ResumeLayout(false);
            this.GraphTabControl.ResumeLayout(false);
            this.TabIntensity.ResumeLayout(false);
            this.TabReflectance.ResumeLayout(false);
            this.tab_MeasurePoint.ResumeLayout(false);
            this.pnl_FocusUnit.ResumeLayout(false);
            this.pnl_FocusUnit.PerformLayout();
            this.tabControl3.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tlpOperate.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tab_Cassete1.ResumeLayout(false);
            this.tab_Cassete1.PerformLayout();
            this.tab_Device.ResumeLayout(false);
            this.tab_Device.PerformLayout();
            this.tab_Angle.ResumeLayout(false);
            this.tab_Angle.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tbp_Point_Move.ResumeLayout(false);
            this.tbp_Point_Move.PerformLayout();
            this.tbp_StepMove.ResumeLayout(false);
            this.tbp_StepMove.PerformLayout();
            this.tab_RowsMove.ResumeLayout(false);
            this.tab_RowsMove.PerformLayout();
            this.tab_ComCheck.ResumeLayout(false);
            this.tab_ComCheck.PerformLayout();
            this.tab_MainConfig.ResumeLayout(false);
            this.pnl_Stgsize.ResumeLayout(false);
            this.pnl_Stgsize.PerformLayout();
            this.pnl_Measure_origin.ResumeLayout(false);
            this.pnl_Measure_origin.PerformLayout();
            this.BottomLeftPanel1.ResumeLayout(false);
            this.BottomLeftPanel1.PerformLayout();
            this.Status.ResumeLayout(false);
            this.Status.PerformLayout();
            this.Menu.ResumeLayout(false);
            this.Menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinComboBox PortComboBox;
        private CCWin.SkinControl.SkinButton BtnConnect;
        private System.IO.Ports.SerialPort StagePort;
        private CCWin.SkinControl.SkinLabel LabelPort;
        private CCWin.SkinControl.SkinMenuStrip Menu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openCoordinatesTableToolStripMenuItem;
        private CCWin.SkinControl.SkinDataGridView DataGrid;
        private CCWin.SkinControl.SkinPanel RightPanel;
        private CCWin.SkinControl.SkinPanel TopLeftPanel;
        private CCWin.SkinControl.SkinPanel TopLeftPanel2;
        private CCWin.SkinControl.SkinLabel Label_information;
        private CCWin.SkinControl.SkinTextBox TxtPath;
        private CCWin.SkinControl.SkinPanel TopLeftPanel3;
        private CCWin.SkinControl.SkinButton BtnStop;
        private CCWin.SkinControl.SkinButton BtnStart;
        private CCWin.SkinControl.SkinButton BtnEject;
        private CCWin.SkinControl.SkinButton BtnHome;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private CCWin.SkinControl.SkinTabControl GraphTabControl;
        private CCWin.SkinControl.SkinTabPage TabIntensity;
        private CCWin.SkinControl.SkinTabPage TabReflectance;
        private CCWin.SkinControl.SkinPanel BottomRightPanel;
        private CCWin.SkinControl.SkinPanel TopRightPanel;
        private ZedGraph.ZedGraphControl IntensityGraphControl;
        private ZedGraph.ZedGraphControl ReflectanceGraphControl;
        private CCWin.SkinControl.SkinPanel BottomLeftPanel;
        private CCWin.SkinControl.SkinPanel BottomLeftPanel2;
        private CCWin.SkinControl.SkinPanel BottomLeftPanel1;
        private System.Windows.Forms.StatusStrip Status;
        private System.Windows.Forms.ToolStripStatusLabel mStatusStripStatus;
        private System.Windows.Forms.ToolStripStatusLabel mStatusStripTxtReceived;
        private System.Windows.Forms.Timer TimerForRefresh;
        private CCWin.SkinControl.SkinLabel LabelTime;
        private CCWin.SkinControl.SkinLabel LabelElapsedTime;
        private System.Windows.Forms.ToolStripMenuItem ThinFilmModelToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem spectrometerConfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem devicePatternToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem singleMeasureToolStripMenuItem;
        private CCWin.SkinControl.SkinTabPage tab_MeasurePoint;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbl_Ypv2;
        private System.Windows.Forms.Label lbl_XPV2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbp_Point_Move;
        private System.Windows.Forms.Button btn_XYMovePos;
        private System.Windows.Forms.Label lbl_YMovepos;
        private System.Windows.Forms.Label lbl_Xmove_pos;
        private System.Windows.Forms.TextBox txt_YMovePos;
        private System.Windows.Forms.TextBox txt_XMovepos;
        private System.Windows.Forms.TabPage tbp_StepMove;
        private System.Windows.Forms.Button btn_MoveStep;
        private System.Windows.Forms.TextBox txt_Ystpsv;
        private System.Windows.Forms.Label lbl_YStep;
        private System.Windows.Forms.TextBox txt_Xstpsv;
        private System.Windows.Forms.Label lbl_XStep;
        private CCWin.SkinControl.SkinTabPage tab_ComCheck;
        private System.Windows.Forms.RichTextBox txt_senddata;
        private System.Windows.Forms.RichTextBox txt_rcvdata;
        private System.Windows.Forms.Label lbl_Rcvcmd;
        private System.Windows.Forms.Label lbl_Sendcmd;
        private System.Windows.Forms.Label lbl_Recipiinfo;
        private System.Windows.Forms.RichTextBox txt_Recipeinfo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_AlignerRst;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tab_Cassete1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_C1_Yinit;
        private System.Windows.Forms.TextBox txt_C1_Xinit;
        private System.Windows.Forms.Button btn_PatternAlign;
        private System.Windows.Forms.Button btn_updateTGMark;
        private System.Windows.Forms.TabPage tab_Angle;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_YGrad;
        private System.Windows.Forms.TextBox txt_XGrad;
        private System.Windows.Forms.TabPage tab_Device;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txt_YShot;
        private System.Windows.Forms.TextBox txt_XShot;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txt_Ypitch;
        private System.Windows.Forms.TextBox txt_Xpitch;
        private System.Windows.Forms.RichTextBox txt_AlinRcvCmd;
        private System.Windows.Forms.RichTextBox txt_Alinsendcmd;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lbl_DispXGap;
        private System.Windows.Forms.Label lbl_DispGapY;
        private System.Windows.Forms.Label lbl_PatternNum;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbl_PatternRecipe;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txt_AlYtol;
        private System.Windows.Forms.TextBox txt_AlXtol;
        private System.Windows.Forms.ToolStripMenuItem logTestDebugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fVASettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cmdTestDebugToolStripMenuItem;
        private CCWin.SkinControl.SkinButton btnReConnect;
        private System.Windows.Forms.Button btnAngleCorrection;
        private System.Windows.Forms.Button btnPatternAlign_C;
        private System.Windows.Forms.TableLayoutPanel tlpOperate;
        private System.Windows.Forms.TabPage tab_RowsMove;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.TextBox txbxStepLength;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.CheckBox cbxUseAngleCorrection;
        private System.Windows.Forms.CheckBox cbuseshotnum;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txt_ShotIntY;
        private System.Windows.Forms.TextBox txt_ShotIntX;
        private System.Windows.Forms.Label label27;
        private CCWin.SkinControl.SkinTabPage tab_MainConfig;
        private System.Windows.Forms.Panel pnl_Stgsize;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btn_Limitset;
        private System.Windows.Forms.TextBox txt_Ylimit;
        private System.Windows.Forms.TextBox txt_Xlimit;
        private System.Windows.Forms.Label lbl_YLimit;
        private System.Windows.Forms.Label lbl_Xlimit;
        private System.Windows.Forms.ComboBox cmb_Stgsize;
        private System.Windows.Forms.Label lbl_StgSize;
        private System.Windows.Forms.Panel pnl_Measure_origin;
        private System.Windows.Forms.Label lbl_YHomepv;
        private System.Windows.Forms.Label lbl_XHomepv;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btn_MeasOrgSet;
        private System.Windows.Forms.TextBox txt_Ymeasorg_sv;
        private System.Windows.Forms.TextBox txt_Xmeasorg_sv;
        private System.Windows.Forms.Label lbl_YMeasOrg;
        private System.Windows.Forms.Label lbl_Xmeasorg;
        private System.Windows.Forms.Panel pnl_FocusUnit;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.ComboBox cmb_FcsPosNum;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label lbl_CurFcsPos;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Button btn_FcsRec;
        private System.Windows.Forms.Button btn_FcsSetNum;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btn_SetFcsPos;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox txt_setFcsPos;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txt_FcsStep;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Button btn_FcsZero;
        private System.Windows.Forms.Button btn_FcsStepdown;
        private System.Windows.Forms.Button btn_FcsStepUp;
        private CCWin.SkinControl.SkinComboBox cmb_FcsComPort;
        private CCWin.SkinControl.SkinLabel skinLabel1;
        private System.Windows.Forms.CheckBox chk_UseFocusSearch;
        private System.Windows.Forms.CheckBox chk_SerchAreaSpread;
    }
}

