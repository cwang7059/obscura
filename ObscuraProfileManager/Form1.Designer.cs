namespace ObscuraProfileManager;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;
    private SplitContainer splitContainer;
    private Label lblProfiles;
    private ListBox lstProfiles;
    private FlowLayoutPanel pnlLeftButtons;
    private Button btnCreateProfile;
    private Button btnRefreshProfiles;
    private Button btnOpenConfigsFolder;
    private Label lblRepoRootCaption;
    private Label lblRepoRoot;
    private TableLayoutPanel pnlDetails;
    private TextBox txtProfileId;
    private ComboBox cboBrowser;
    private TextBox txtStartUrl;
    private TextBox txtUserDataDir;
    private TextBox txtProxy;
    private TextBox txtExecutablePath;
    private CheckBox chkHeadless;
    private TextBox txtArgs;
    private TextBox txtConfigPath;
    private FlowLayoutPanel pnlActions;
    private Button btnSaveProfile;
    private Button btnStartProfile;
    private Button btnOpenProfileFolder;
    private Label lblLog;
    private TextBox txtLog;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.splitContainer = new System.Windows.Forms.SplitContainer();
        this.lblProfiles = new System.Windows.Forms.Label();
        this.lstProfiles = new System.Windows.Forms.ListBox();
        this.pnlLeftButtons = new System.Windows.Forms.FlowLayoutPanel();
        this.btnCreateProfile = new System.Windows.Forms.Button();
        this.btnRefreshProfiles = new System.Windows.Forms.Button();
        this.btnOpenConfigsFolder = new System.Windows.Forms.Button();
        this.lblRepoRootCaption = new System.Windows.Forms.Label();
        this.lblRepoRoot = new System.Windows.Forms.Label();
        this.pnlDetails = new System.Windows.Forms.TableLayoutPanel();
        this.txtProfileId = new System.Windows.Forms.TextBox();
        this.cboBrowser = new System.Windows.Forms.ComboBox();
        this.txtStartUrl = new System.Windows.Forms.TextBox();
        this.txtUserDataDir = new System.Windows.Forms.TextBox();
        this.txtProxy = new System.Windows.Forms.TextBox();
        this.txtExecutablePath = new System.Windows.Forms.TextBox();
        this.txtConfigPath = new System.Windows.Forms.TextBox();
        this.chkHeadless = new System.Windows.Forms.CheckBox();
        this.txtArgs = new System.Windows.Forms.TextBox();
        this.pnlActions = new System.Windows.Forms.FlowLayoutPanel();
        this.btnSaveProfile = new System.Windows.Forms.Button();
        this.btnStartProfile = new System.Windows.Forms.Button();
        this.btnOpenProfileFolder = new System.Windows.Forms.Button();
        this.lblLog = new System.Windows.Forms.Label();
        this.txtLog = new System.Windows.Forms.TextBox();
        ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
        this.splitContainer.Panel1.SuspendLayout();
        this.splitContainer.Panel2.SuspendLayout();
        this.splitContainer.SuspendLayout();
        this.pnlLeftButtons.SuspendLayout();
        this.pnlDetails.SuspendLayout();
        this.pnlActions.SuspendLayout();
        this.SuspendLayout();
        // 
        // splitContainer
        // 
        this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
        this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
        this.splitContainer.Location = new System.Drawing.Point(0, 0);
        this.splitContainer.Name = "splitContainer";
        // 
        // splitContainer.Panel1
        // 
        this.splitContainer.Panel1.Controls.Add(this.lblRepoRoot);
        this.splitContainer.Panel1.Controls.Add(this.lblRepoRootCaption);
        this.splitContainer.Panel1.Controls.Add(this.pnlLeftButtons);
        this.splitContainer.Panel1.Controls.Add(this.lstProfiles);
        this.splitContainer.Panel1.Controls.Add(this.lblProfiles);
        this.splitContainer.Panel1.Padding = new System.Windows.Forms.Padding(12);
        // 
        // splitContainer.Panel2
        // 
        this.splitContainer.Panel2.Controls.Add(this.txtLog);
        this.splitContainer.Panel2.Controls.Add(this.lblLog);
        this.splitContainer.Panel2.Controls.Add(this.pnlActions);
        this.splitContainer.Panel2.Controls.Add(this.pnlDetails);
        this.splitContainer.Panel2.Padding = new System.Windows.Forms.Padding(12);
        this.splitContainer.Size = new System.Drawing.Size(1184, 761);
        this.splitContainer.SplitterDistance = 300;
        this.splitContainer.TabIndex = 0;
        // 
        // lblProfiles
        // 
        this.lblProfiles.AutoSize = true;
        this.lblProfiles.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        this.lblProfiles.Location = new System.Drawing.Point(12, 12);
        this.lblProfiles.Name = "lblProfiles";
        this.lblProfiles.Size = new System.Drawing.Size(167, 28);
        this.lblProfiles.TabIndex = 0;
        this.lblProfiles.Text = "Browser Profiles";
        // 
        // lstProfiles
        // 
        this.lstProfiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
        this.lstProfiles.FormattingEnabled = true;
        this.lstProfiles.ItemHeight = 20;
        this.lstProfiles.Location = new System.Drawing.Point(12, 52);
        this.lstProfiles.Name = "lstProfiles";
        this.lstProfiles.Size = new System.Drawing.Size(276, 524);
        this.lstProfiles.TabIndex = 1;
        // 
        // pnlLeftButtons
        // 
        this.pnlLeftButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
        this.pnlLeftButtons.Controls.Add(this.btnCreateProfile);
        this.pnlLeftButtons.Controls.Add(this.btnRefreshProfiles);
        this.pnlLeftButtons.Controls.Add(this.btnOpenConfigsFolder);
        this.pnlLeftButtons.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
        this.pnlLeftButtons.Location = new System.Drawing.Point(12, 587);
        this.pnlLeftButtons.Name = "pnlLeftButtons";
        this.pnlLeftButtons.Size = new System.Drawing.Size(276, 110);
        this.pnlLeftButtons.TabIndex = 2;
        this.pnlLeftButtons.WrapContents = false;
        // 
        // btnCreateProfile
        // 
        this.btnCreateProfile.AutoSize = true;
        this.btnCreateProfile.Location = new System.Drawing.Point(3, 3);
        this.btnCreateProfile.Name = "btnCreateProfile";
        this.btnCreateProfile.Size = new System.Drawing.Size(150, 30);
        this.btnCreateProfile.TabIndex = 0;
        this.btnCreateProfile.Text = "Create New Profile";
        this.btnCreateProfile.UseVisualStyleBackColor = true;
        // 
        // btnRefreshProfiles
        // 
        this.btnRefreshProfiles.AutoSize = true;
        this.btnRefreshProfiles.Location = new System.Drawing.Point(3, 39);
        this.btnRefreshProfiles.Name = "btnRefreshProfiles";
        this.btnRefreshProfiles.Size = new System.Drawing.Size(150, 30);
        this.btnRefreshProfiles.TabIndex = 1;
        this.btnRefreshProfiles.Text = "Refresh";
        this.btnRefreshProfiles.UseVisualStyleBackColor = true;
        // 
        // btnOpenConfigsFolder
        // 
        this.btnOpenConfigsFolder.AutoSize = true;
        this.btnOpenConfigsFolder.Location = new System.Drawing.Point(3, 75);
        this.btnOpenConfigsFolder.Name = "btnOpenConfigsFolder";
        this.btnOpenConfigsFolder.Size = new System.Drawing.Size(150, 30);
        this.btnOpenConfigsFolder.TabIndex = 2;
        this.btnOpenConfigsFolder.Text = "Open Configs Folder";
        this.btnOpenConfigsFolder.UseVisualStyleBackColor = true;
        // 
        // lblRepoRootCaption
        // 
        this.lblRepoRootCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.lblRepoRootCaption.AutoSize = true;
        this.lblRepoRootCaption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        this.lblRepoRootCaption.Location = new System.Drawing.Point(12, 708);
        this.lblRepoRootCaption.Name = "lblRepoRootCaption";
        this.lblRepoRootCaption.Size = new System.Drawing.Size(76, 20);
        this.lblRepoRootCaption.TabIndex = 3;
        this.lblRepoRootCaption.Text = "Repo root";
        // 
        // lblRepoRoot
        // 
        this.lblRepoRoot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
        this.lblRepoRoot.Location = new System.Drawing.Point(12, 728);
        this.lblRepoRoot.Name = "lblRepoRoot";
        this.lblRepoRoot.Size = new System.Drawing.Size(276, 21);
        this.lblRepoRoot.TabIndex = 4;
        this.lblRepoRoot.Text = "-";
        // 
        // pnlDetails
        // 
        this.pnlDetails.AutoSize = true;
        this.pnlDetails.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        this.pnlDetails.ColumnCount = 2;
        this.pnlDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 145F));
        this.pnlDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.pnlDetails.Controls.Add(new System.Windows.Forms.Label() { AutoSize = true, Text = "Profile ID", Anchor = System.Windows.Forms.AnchorStyles.Left, Margin = new System.Windows.Forms.Padding(0, 8, 8, 8) }, 0, 0);
        this.pnlDetails.Controls.Add(this.txtProfileId, 1, 0);
        this.pnlDetails.Controls.Add(new System.Windows.Forms.Label() { AutoSize = true, Text = "Browser", Anchor = System.Windows.Forms.AnchorStyles.Left, Margin = new System.Windows.Forms.Padding(0, 8, 8, 8) }, 0, 1);
        this.pnlDetails.Controls.Add(this.cboBrowser, 1, 1);
        this.pnlDetails.Controls.Add(new System.Windows.Forms.Label() { AutoSize = true, Text = "Start URL", Anchor = System.Windows.Forms.AnchorStyles.Left, Margin = new System.Windows.Forms.Padding(0, 8, 8, 8) }, 0, 2);
        this.pnlDetails.Controls.Add(this.txtStartUrl, 1, 2);
        this.pnlDetails.Controls.Add(new System.Windows.Forms.Label() { AutoSize = true, Text = "User Data Dir", Anchor = System.Windows.Forms.AnchorStyles.Left, Margin = new System.Windows.Forms.Padding(0, 8, 8, 8) }, 0, 3);
        this.pnlDetails.Controls.Add(this.txtUserDataDir, 1, 3);
        this.pnlDetails.Controls.Add(new System.Windows.Forms.Label() { AutoSize = true, Text = "Proxy", Anchor = System.Windows.Forms.AnchorStyles.Left, Margin = new System.Windows.Forms.Padding(0, 8, 8, 8) }, 0, 4);
        this.pnlDetails.Controls.Add(this.txtProxy, 1, 4);
        this.pnlDetails.Controls.Add(new System.Windows.Forms.Label() { AutoSize = true, Text = "Executable Path", Anchor = System.Windows.Forms.AnchorStyles.Left, Margin = new System.Windows.Forms.Padding(0, 8, 8, 8) }, 0, 5);
        this.pnlDetails.Controls.Add(this.txtExecutablePath, 1, 5);
        this.pnlDetails.Controls.Add(new System.Windows.Forms.Label() { AutoSize = true, Text = "Config Path", Anchor = System.Windows.Forms.AnchorStyles.Left, Margin = new System.Windows.Forms.Padding(0, 8, 8, 8) }, 0, 6);
        this.pnlDetails.Controls.Add(this.txtConfigPath, 1, 6);
        this.pnlDetails.Controls.Add(this.chkHeadless, 1, 7);
        this.pnlDetails.Controls.Add(new System.Windows.Forms.Label() { AutoSize = true, Text = "Extra Args", Anchor = System.Windows.Forms.AnchorStyles.Left, Margin = new System.Windows.Forms.Padding(0, 8, 8, 8) }, 0, 8);
        this.pnlDetails.Controls.Add(this.txtArgs, 1, 8);
        this.pnlDetails.Dock = System.Windows.Forms.DockStyle.Top;
        this.pnlDetails.Location = new System.Drawing.Point(12, 12);
        this.pnlDetails.Name = "pnlDetails";
        this.pnlDetails.RowCount = 9;
        this.pnlDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
        this.pnlDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
        this.pnlDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
        this.pnlDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
        this.pnlDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
        this.pnlDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
        this.pnlDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
        this.pnlDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
        this.pnlDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
        this.pnlDetails.Size = new System.Drawing.Size(856, 357);
        this.pnlDetails.TabIndex = 0;
        // 
        // txtProfileId
        // 
        this.txtProfileId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right))));
        this.txtProfileId.Location = new System.Drawing.Point(148, 3);
        this.txtProfileId.Name = "txtProfileId";
        this.txtProfileId.ReadOnly = true;
        this.txtProfileId.Size = new System.Drawing.Size(705, 27);
        this.txtProfileId.TabIndex = 0;
        // 
        // cboBrowser
        // 
        this.cboBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right))));
        this.cboBrowser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cboBrowser.FormattingEnabled = true;
        this.cboBrowser.Location = new System.Drawing.Point(148, 41);
        this.cboBrowser.Name = "cboBrowser";
        this.cboBrowser.Size = new System.Drawing.Size(705, 28);
        this.cboBrowser.TabIndex = 1;
        // 
        // txtStartUrl
        // 
        this.txtStartUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right))));
        this.txtStartUrl.Location = new System.Drawing.Point(148, 80);
        this.txtStartUrl.Name = "txtStartUrl";
        this.txtStartUrl.Size = new System.Drawing.Size(705, 27);
        this.txtStartUrl.TabIndex = 2;
        // 
        // txtUserDataDir
        // 
        this.txtUserDataDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right))));
        this.txtUserDataDir.Location = new System.Drawing.Point(148, 119);
        this.txtUserDataDir.Name = "txtUserDataDir";
        this.txtUserDataDir.Size = new System.Drawing.Size(705, 27);
        this.txtUserDataDir.TabIndex = 3;
        // 
        // txtProxy
        // 
        this.txtProxy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right))));
        this.txtProxy.Location = new System.Drawing.Point(148, 158);
        this.txtProxy.Name = "txtProxy";
        this.txtProxy.Size = new System.Drawing.Size(705, 27);
        this.txtProxy.TabIndex = 4;
        // 
        // txtExecutablePath
        // 
        this.txtExecutablePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right))));
        this.txtExecutablePath.Location = new System.Drawing.Point(148, 197);
        this.txtExecutablePath.Name = "txtExecutablePath";
        this.txtExecutablePath.Size = new System.Drawing.Size(705, 27);
        this.txtExecutablePath.TabIndex = 5;
        // 
        // txtConfigPath
        // 
        this.txtConfigPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right))));
        this.txtConfigPath.Location = new System.Drawing.Point(148, 236);
        this.txtConfigPath.Name = "txtConfigPath";
        this.txtConfigPath.ReadOnly = true;
        this.txtConfigPath.Size = new System.Drawing.Size(705, 27);
        this.txtConfigPath.TabIndex = 6;
        // 
        // chkHeadless
        // 
        this.chkHeadless.AutoSize = true;
        this.chkHeadless.Location = new System.Drawing.Point(148, 269);
        this.chkHeadless.Name = "chkHeadless";
        this.chkHeadless.Size = new System.Drawing.Size(215, 24);
        this.chkHeadless.TabIndex = 7;
        this.chkHeadless.Text = "Headless (not recommended)";
        this.chkHeadless.UseVisualStyleBackColor = true;
        // 
        // txtArgs
        // 
        this.txtArgs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right))));
        this.txtArgs.Location = new System.Drawing.Point(148, 307);
        this.txtArgs.Multiline = true;
        this.txtArgs.Name = "txtArgs";
        this.txtArgs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        this.txtArgs.Size = new System.Drawing.Size(705, 47);
        this.txtArgs.TabIndex = 8;
        // 
        // pnlActions
        // 
        this.pnlActions.AutoSize = true;
        this.pnlActions.Controls.Add(this.btnSaveProfile);
        this.pnlActions.Controls.Add(this.btnStartProfile);
        this.pnlActions.Controls.Add(this.btnOpenProfileFolder);
        this.pnlActions.Dock = System.Windows.Forms.DockStyle.Top;
        this.pnlActions.Location = new System.Drawing.Point(12, 369);
        this.pnlActions.Name = "pnlActions";
        this.pnlActions.Padding = new System.Windows.Forms.Padding(0, 12, 0, 12);
        this.pnlActions.Size = new System.Drawing.Size(856, 54);
        this.pnlActions.TabIndex = 1;
        // 
        // btnSaveProfile
        // 
        this.btnSaveProfile.AutoSize = true;
        this.btnSaveProfile.Location = new System.Drawing.Point(3, 15);
        this.btnSaveProfile.Name = "btnSaveProfile";
        this.btnSaveProfile.Size = new System.Drawing.Size(106, 30);
        this.btnSaveProfile.TabIndex = 0;
        this.btnSaveProfile.Text = "Save Profile";
        this.btnSaveProfile.UseVisualStyleBackColor = true;
        // 
        // btnStartProfile
        // 
        this.btnStartProfile.AutoSize = true;
        this.btnStartProfile.Location = new System.Drawing.Point(115, 15);
        this.btnStartProfile.Name = "btnStartProfile";
        this.btnStartProfile.Size = new System.Drawing.Size(133, 30);
        this.btnStartProfile.TabIndex = 1;
        this.btnStartProfile.Text = "Start In Browser";
        this.btnStartProfile.UseVisualStyleBackColor = true;
        // 
        // btnOpenProfileFolder
        // 
        this.btnOpenProfileFolder.AutoSize = true;
        this.btnOpenProfileFolder.Location = new System.Drawing.Point(254, 15);
        this.btnOpenProfileFolder.Name = "btnOpenProfileFolder";
        this.btnOpenProfileFolder.Size = new System.Drawing.Size(141, 30);
        this.btnOpenProfileFolder.TabIndex = 2;
        this.btnOpenProfileFolder.Text = "Open Profile Folder";
        this.btnOpenProfileFolder.UseVisualStyleBackColor = true;
        // 
        // lblLog
        // 
        this.lblLog.AutoSize = true;
        this.lblLog.Dock = System.Windows.Forms.DockStyle.Top;
        this.lblLog.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
        this.lblLog.Location = new System.Drawing.Point(12, 423);
        this.lblLog.Name = "lblLog";
        this.lblLog.Padding = new System.Windows.Forms.Padding(0, 10, 0, 8);
        this.lblLog.Size = new System.Drawing.Size(89, 41);
        this.lblLog.TabIndex = 2;
        this.lblLog.Text = "Launcher Log";
        // 
        // txtLog
        // 
        this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
        this.txtLog.Location = new System.Drawing.Point(12, 464);
        this.txtLog.Multiline = true;
        this.txtLog.Name = "txtLog";
        this.txtLog.ReadOnly = true;
        this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        this.txtLog.Size = new System.Drawing.Size(856, 285);
        this.txtLog.TabIndex = 3;
        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1184, 761);
        this.Controls.Add(this.splitContainer);
        this.MinimumSize = new System.Drawing.Size(1100, 720);
        this.Name = "Form1";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Obscura Profile Manager";
        this.splitContainer.Panel1.ResumeLayout(false);
        this.splitContainer.Panel1.PerformLayout();
        this.splitContainer.Panel2.ResumeLayout(false);
        this.splitContainer.Panel2.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
        this.splitContainer.ResumeLayout(false);
        this.pnlLeftButtons.ResumeLayout(false);
        this.pnlLeftButtons.PerformLayout();
        this.pnlDetails.ResumeLayout(false);
        this.pnlDetails.PerformLayout();
        this.pnlActions.ResumeLayout(false);
        this.pnlActions.PerformLayout();
        this.ResumeLayout(false);
    }

    #endregion
}
