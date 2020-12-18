<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
		Me.Label1 = New System.Windows.Forms.Label
		Me.txtWorkingDir = New System.Windows.Forms.TextBox
		Me.cmdGetWorkingDir = New System.Windows.Forms.Button
		Me.Label2 = New System.Windows.Forms.Label
		Me.txtSVNPath = New System.Windows.Forms.TextBox
		Me.grpGeneral = New System.Windows.Forms.GroupBox
		Me.chkDeleteWorkingDir = New System.Windows.Forms.CheckBox
		Me.grpSVN = New System.Windows.Forms.GroupBox
		Me.grpVSS = New System.Windows.Forms.GroupBox
		Me.dtUpdateSince = New System.Windows.Forms.DateTimePicker
		Me.chkUpdateSince = New System.Windows.Forms.CheckBox
		Me.chkSetProps = New System.Windows.Forms.CheckBox
		Me.txtFilesToIgnore = New System.Windows.Forms.TextBox
		Me.chkIgnoreFileExtensions = New System.Windows.Forms.CheckBox
		Me.chkOnlyLatestVersion = New System.Windows.Forms.CheckBox
		Me.chkWarnCheckedOut = New System.Windows.Forms.CheckBox
		Me.txtVSSPath = New System.Windows.Forms.TextBox
		Me.Label6 = New System.Windows.Forms.Label
		Me.txtPassword = New System.Windows.Forms.TextBox
		Me.Label5 = New System.Windows.Forms.Label
		Me.txtUserName = New System.Windows.Forms.TextBox
		Me.Label4 = New System.Windows.Forms.Label
		Me.txtScrsafe = New System.Windows.Forms.TextBox
		Me.Label3 = New System.Windows.Forms.Label
		Me.cmdGetScrsafeDir = New System.Windows.Forms.Button
		Me.txtLog = New System.Windows.Forms.TextBox
		Me.cmdSourceSafeFiles = New System.Windows.Forms.Button
		Me.cmdMigrate = New System.Windows.Forms.Button
		Me.FBD = New System.Windows.Forms.FolderBrowserDialog
		Me.OFD = New System.Windows.Forms.OpenFileDialog
		Me.cmdStartOver = New System.Windows.Forms.Button
		Me.txtException = New System.Windows.Forms.TextBox
		Me.Label7 = New System.Windows.Forms.Label
		Me.chkLogSVNResponses = New System.Windows.Forms.CheckBox
		Me.grpGeneral.SuspendLayout()
		Me.grpSVN.SuspendLayout()
		Me.grpVSS.SuspendLayout()
		Me.SuspendLayout()
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(11, 23)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(97, 13)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "Working Directory:"
		'
		'txtWorkingDir
		'
		Me.txtWorkingDir.Location = New System.Drawing.Point(114, 20)
		Me.txtWorkingDir.Name = "txtWorkingDir"
		Me.txtWorkingDir.Size = New System.Drawing.Size(397, 21)
		Me.txtWorkingDir.TabIndex = 1
		Me.txtWorkingDir.Text = "c:\temp"
		'
		'cmdGetWorkingDir
		'
		Me.cmdGetWorkingDir.Location = New System.Drawing.Point(517, 19)
		Me.cmdGetWorkingDir.Name = "cmdGetWorkingDir"
		Me.cmdGetWorkingDir.Size = New System.Drawing.Size(30, 23)
		Me.cmdGetWorkingDir.TabIndex = 2
		Me.cmdGetWorkingDir.Text = "..."
		Me.cmdGetWorkingDir.UseVisualStyleBackColor = True
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(11, 23)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(88, 13)
		Me.Label2.TabIndex = 3
		Me.Label2.Text = "SVN path to use:"
		'
		'txtSVNPath
		'
		Me.txtSVNPath.Location = New System.Drawing.Point(114, 20)
		Me.txtSVNPath.Name = "txtSVNPath"
		Me.txtSVNPath.Size = New System.Drawing.Size(433, 21)
		Me.txtSVNPath.TabIndex = 4
		Me.txtSVNPath.Text = "http://server/svn/repo/trunk"
		'
		'grpGeneral
		'
		Me.grpGeneral.Controls.Add(Me.chkDeleteWorkingDir)
		Me.grpGeneral.Controls.Add(Me.txtWorkingDir)
		Me.grpGeneral.Controls.Add(Me.Label1)
		Me.grpGeneral.Controls.Add(Me.cmdGetWorkingDir)
		Me.grpGeneral.Enabled = False
		Me.grpGeneral.Location = New System.Drawing.Point(12, 12)
		Me.grpGeneral.Name = "grpGeneral"
		Me.grpGeneral.Size = New System.Drawing.Size(560, 77)
		Me.grpGeneral.TabIndex = 5
		Me.grpGeneral.TabStop = False
		Me.grpGeneral.Text = "General paramaters"
		'
		'chkDeleteWorkingDir
		'
		Me.chkDeleteWorkingDir.AutoSize = True
		Me.chkDeleteWorkingDir.Location = New System.Drawing.Point(14, 47)
		Me.chkDeleteWorkingDir.Name = "chkDeleteWorkingDir"
		Me.chkDeleteWorkingDir.Size = New System.Drawing.Size(188, 17)
		Me.chkDeleteWorkingDir.TabIndex = 3
		Me.chkDeleteWorkingDir.Text = "Delete temporary files when done"
		Me.chkDeleteWorkingDir.UseVisualStyleBackColor = True
		'
		'grpSVN
		'
		Me.grpSVN.Controls.Add(Me.txtSVNPath)
		Me.grpSVN.Controls.Add(Me.Label2)
		Me.grpSVN.Enabled = False
		Me.grpSVN.Location = New System.Drawing.Point(12, 95)
		Me.grpSVN.Name = "grpSVN"
		Me.grpSVN.Size = New System.Drawing.Size(560, 55)
		Me.grpSVN.TabIndex = 6
		Me.grpSVN.TabStop = False
		Me.grpSVN.Text = "SVN parameters"
		'
		'grpVSS
		'
		Me.grpVSS.Controls.Add(Me.dtUpdateSince)
		Me.grpVSS.Controls.Add(Me.chkUpdateSince)
		Me.grpVSS.Controls.Add(Me.chkSetProps)
		Me.grpVSS.Controls.Add(Me.txtFilesToIgnore)
		Me.grpVSS.Controls.Add(Me.chkIgnoreFileExtensions)
		Me.grpVSS.Controls.Add(Me.chkOnlyLatestVersion)
		Me.grpVSS.Controls.Add(Me.chkWarnCheckedOut)
		Me.grpVSS.Controls.Add(Me.txtVSSPath)
		Me.grpVSS.Controls.Add(Me.Label6)
		Me.grpVSS.Controls.Add(Me.txtPassword)
		Me.grpVSS.Controls.Add(Me.Label5)
		Me.grpVSS.Controls.Add(Me.txtUserName)
		Me.grpVSS.Controls.Add(Me.Label4)
		Me.grpVSS.Controls.Add(Me.txtScrsafe)
		Me.grpVSS.Controls.Add(Me.Label3)
		Me.grpVSS.Controls.Add(Me.cmdGetScrsafeDir)
		Me.grpVSS.Location = New System.Drawing.Point(12, 156)
		Me.grpVSS.Name = "grpVSS"
		Me.grpVSS.Size = New System.Drawing.Size(560, 230)
		Me.grpVSS.TabIndex = 7
		Me.grpVSS.TabStop = False
		Me.grpVSS.Text = "VSS parameters"
		'
		'dtUpdateSince
		'
		Me.dtUpdateSince.Location = New System.Drawing.Point(354, 197)
		Me.dtUpdateSince.Name = "dtUpdateSince"
		Me.dtUpdateSince.Size = New System.Drawing.Size(200, 21)
		Me.dtUpdateSince.TabIndex = 18
		'
		'chkUpdateSince
		'
		Me.chkUpdateSince.AutoSize = True
		Me.chkUpdateSince.Location = New System.Drawing.Point(195, 197)
		Me.chkUpdateSince.Name = "chkUpdateSince"
		Me.chkUpdateSince.Size = New System.Drawing.Size(157, 17)
		Me.chkUpdateSince.TabIndex = 17
		Me.chkUpdateSince.Text = "Update files modified since:"
		Me.chkUpdateSince.UseVisualStyleBackColor = True
		'
		'chkSetProps
		'
		Me.chkSetProps.AutoSize = True
		Me.chkSetProps.Checked = True
		Me.chkSetProps.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkSetProps.Location = New System.Drawing.Point(14, 174)
		Me.chkSetProps.Name = "chkSetProps"
		Me.chkSetProps.Size = New System.Drawing.Size(212, 17)
		Me.chkSetProps.TabIndex = 16
		Me.chkSetProps.Text = "Update date,log and author properties"
		Me.chkSetProps.UseVisualStyleBackColor = True
		'
		'txtFilesToIgnore
		'
		Me.txtFilesToIgnore.Location = New System.Drawing.Point(231, 149)
		Me.txtFilesToIgnore.Name = "txtFilesToIgnore"
		Me.txtFilesToIgnore.Size = New System.Drawing.Size(323, 21)
		Me.txtFilesToIgnore.TabIndex = 15
		'
		'chkIgnoreFileExtensions
		'
		Me.chkIgnoreFileExtensions.AutoSize = True
		Me.chkIgnoreFileExtensions.Checked = True
		Me.chkIgnoreFileExtensions.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkIgnoreFileExtensions.Location = New System.Drawing.Point(14, 151)
		Me.chkIgnoreFileExtensions.Name = "chkIgnoreFileExtensions"
		Me.chkIgnoreFileExtensions.Size = New System.Drawing.Size(211, 17)
		Me.chkIgnoreFileExtensions.TabIndex = 14
		Me.chkIgnoreFileExtensions.Text = "Ignore files with the following patterns"
		Me.chkIgnoreFileExtensions.UseVisualStyleBackColor = True
		'
		'chkOnlyLatestVersion
		'
		Me.chkOnlyLatestVersion.AutoSize = True
		Me.chkOnlyLatestVersion.Location = New System.Drawing.Point(14, 197)
		Me.chkOnlyLatestVersion.Name = "chkOnlyLatestVersion"
		Me.chkOnlyLatestVersion.Size = New System.Drawing.Size(153, 17)
		Me.chkOnlyLatestVersion.TabIndex = 13
		Me.chkOnlyLatestVersion.Text = "Get the latest version only"
		Me.chkOnlyLatestVersion.UseVisualStyleBackColor = True
		'
		'chkWarnCheckedOut
		'
		Me.chkWarnCheckedOut.AutoSize = True
		Me.chkWarnCheckedOut.Checked = True
		Me.chkWarnCheckedOut.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkWarnCheckedOut.Location = New System.Drawing.Point(14, 128)
		Me.chkWarnCheckedOut.Name = "chkWarnCheckedOut"
		Me.chkWarnCheckedOut.Size = New System.Drawing.Size(350, 17)
		Me.chkWarnCheckedOut.TabIndex = 12
		Me.chkWarnCheckedOut.Text = "Warn me if there are any checked-out files in the project to migrate"
		Me.chkWarnCheckedOut.UseVisualStyleBackColor = True
		'
		'txtVSSPath
		'
		Me.txtVSSPath.Location = New System.Drawing.Point(115, 101)
		Me.txtVSSPath.Name = "txtVSSPath"
		Me.txtVSSPath.Size = New System.Drawing.Size(397, 21)
		Me.txtVSSPath.TabIndex = 11
		Me.txtVSSPath.Text = "$\Project"
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.Location = New System.Drawing.Point(11, 104)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(99, 13)
		Me.Label6.TabIndex = 10
		Me.Label6.Text = "VSS project to use:"
		Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'txtPassword
		'
		Me.txtPassword.Location = New System.Drawing.Point(115, 74)
		Me.txtPassword.Name = "txtPassword"
		Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
		Me.txtPassword.Size = New System.Drawing.Size(397, 21)
		Me.txtPassword.TabIndex = 9
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Location = New System.Drawing.Point(47, 78)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(57, 13)
		Me.Label5.TabIndex = 8
		Me.Label5.Text = "Password:"
		Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'txtUserName
		'
		Me.txtUserName.Location = New System.Drawing.Point(115, 47)
		Me.txtUserName.Name = "txtUserName"
		Me.txtUserName.Size = New System.Drawing.Size(397, 21)
		Me.txtUserName.TabIndex = 7
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Location = New System.Drawing.Point(46, 50)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(62, 13)
		Me.Label4.TabIndex = 6
		Me.Label4.Text = "User name:"
		Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'txtScrsafe
		'
		Me.txtScrsafe.Location = New System.Drawing.Point(115, 20)
		Me.txtScrsafe.Name = "txtScrsafe"
		Me.txtScrsafe.Size = New System.Drawing.Size(397, 21)
		Me.txtScrsafe.TabIndex = 4
		Me.txtScrsafe.Text = "\\server\share\srcsafe.ini"
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(11, 23)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(99, 13)
		Me.Label3.TabIndex = 3
		Me.Label3.Text = "Path to Scrsafe.ini:"
		'
		'cmdGetScrsafeDir
		'
		Me.cmdGetScrsafeDir.Location = New System.Drawing.Point(517, 19)
		Me.cmdGetScrsafeDir.Name = "cmdGetScrsafeDir"
		Me.cmdGetScrsafeDir.Size = New System.Drawing.Size(30, 23)
		Me.cmdGetScrsafeDir.TabIndex = 5
		Me.cmdGetScrsafeDir.Text = "..."
		Me.cmdGetScrsafeDir.UseVisualStyleBackColor = True
		'
		'txtLog
		'
		Me.txtLog.Location = New System.Drawing.Point(578, 19)
		Me.txtLog.Multiline = True
		Me.txtLog.Name = "txtLog"
		Me.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both
		Me.txtLog.Size = New System.Drawing.Size(416, 282)
		Me.txtLog.TabIndex = 8
		Me.txtLog.WordWrap = False
		'
		'cmdSourceSafeFiles
		'
		Me.cmdSourceSafeFiles.Location = New System.Drawing.Point(52, 411)
		Me.cmdSourceSafeFiles.Name = "cmdSourceSafeFiles"
		Me.cmdSourceSafeFiles.Size = New System.Drawing.Size(142, 42)
		Me.cmdSourceSafeFiles.TabIndex = 9
		Me.cmdSourceSafeFiles.Text = "Find files in source safe"
		Me.cmdSourceSafeFiles.UseVisualStyleBackColor = True
		'
		'cmdMigrate
		'
		Me.cmdMigrate.Enabled = False
		Me.cmdMigrate.Location = New System.Drawing.Point(217, 411)
		Me.cmdMigrate.Name = "cmdMigrate"
		Me.cmdMigrate.Size = New System.Drawing.Size(142, 42)
		Me.cmdMigrate.TabIndex = 10
		Me.cmdMigrate.Text = "Migrate to subversion"
		Me.cmdMigrate.UseVisualStyleBackColor = True
		'
		'FBD
		'
		Me.FBD.ShowNewFolderButton = False
		'
		'OFD
		'
		Me.OFD.Filter = "Scrsafe INI file|srcsafe.ini"
		Me.OFD.RestoreDirectory = True
		'
		'cmdStartOver
		'
		Me.cmdStartOver.Location = New System.Drawing.Point(382, 411)
		Me.cmdStartOver.Name = "cmdStartOver"
		Me.cmdStartOver.Size = New System.Drawing.Size(142, 42)
		Me.cmdStartOver.TabIndex = 11
		Me.cmdStartOver.Text = "Start over"
		Me.cmdStartOver.UseVisualStyleBackColor = True
		'
		'txtException
		'
		Me.txtException.Location = New System.Drawing.Point(578, 330)
		Me.txtException.Multiline = True
		Me.txtException.Name = "txtException"
		Me.txtException.ScrollBars = System.Windows.Forms.ScrollBars.Both
		Me.txtException.Size = New System.Drawing.Size(416, 123)
		Me.txtException.TabIndex = 12
		Me.txtException.WordWrap = False
		'
		'Label7
		'
		Me.Label7.AutoSize = True
		Me.Label7.Location = New System.Drawing.Point(578, 313)
		Me.Label7.Name = "Label7"
		Me.Label7.Size = New System.Drawing.Size(63, 13)
		Me.Label7.TabIndex = 13
		Me.Label7.Text = "Exceptions:"
		'
		'chkLogSVNResponses
		'
		Me.chkLogSVNResponses.AutoSize = True
		Me.chkLogSVNResponses.Location = New System.Drawing.Point(578, 2)
		Me.chkLogSVNResponses.Name = "chkLogSVNResponses"
		Me.chkLogSVNResponses.Size = New System.Drawing.Size(117, 17)
		Me.chkLogSVNResponses.TabIndex = 14
		Me.chkLogSVNResponses.Text = "Log SVN responses"
		Me.chkLogSVNResponses.UseVisualStyleBackColor = True
		'
		'frmMain
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(1003, 465)
		Me.Controls.Add(Me.chkLogSVNResponses)
		Me.Controls.Add(Me.Label7)
		Me.Controls.Add(Me.txtException)
		Me.Controls.Add(Me.cmdStartOver)
		Me.Controls.Add(Me.cmdMigrate)
		Me.Controls.Add(Me.cmdSourceSafeFiles)
		Me.Controls.Add(Me.txtLog)
		Me.Controls.Add(Me.grpVSS)
		Me.Controls.Add(Me.grpSVN)
		Me.Controls.Add(Me.grpGeneral)
		Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.MaximizeBox = False
		Me.Name = "frmMain"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "VSS2SVN"
		Me.grpGeneral.ResumeLayout(False)
		Me.grpGeneral.PerformLayout()
		Me.grpSVN.ResumeLayout(False)
		Me.grpSVN.PerformLayout()
		Me.grpVSS.ResumeLayout(False)
		Me.grpVSS.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents txtWorkingDir As System.Windows.Forms.TextBox
	Friend WithEvents cmdGetWorkingDir As System.Windows.Forms.Button
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents txtSVNPath As System.Windows.Forms.TextBox
	Friend WithEvents grpGeneral As System.Windows.Forms.GroupBox
	Friend WithEvents chkDeleteWorkingDir As System.Windows.Forms.CheckBox
	Friend WithEvents grpSVN As System.Windows.Forms.GroupBox
	Friend WithEvents grpVSS As System.Windows.Forms.GroupBox
	Friend WithEvents txtVSSPath As System.Windows.Forms.TextBox
	Friend WithEvents Label6 As System.Windows.Forms.Label
	Friend WithEvents txtPassword As System.Windows.Forms.TextBox
	Friend WithEvents Label5 As System.Windows.Forms.Label
	Friend WithEvents txtUserName As System.Windows.Forms.TextBox
	Friend WithEvents Label4 As System.Windows.Forms.Label
	Friend WithEvents txtScrsafe As System.Windows.Forms.TextBox
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents cmdGetScrsafeDir As System.Windows.Forms.Button
	Friend WithEvents chkWarnCheckedOut As System.Windows.Forms.CheckBox
	Friend WithEvents txtLog As System.Windows.Forms.TextBox
	Friend WithEvents cmdSourceSafeFiles As System.Windows.Forms.Button
	Friend WithEvents cmdMigrate As System.Windows.Forms.Button
	Friend WithEvents FBD As System.Windows.Forms.FolderBrowserDialog
	Friend WithEvents OFD As System.Windows.Forms.OpenFileDialog
	Friend WithEvents cmdStartOver As System.Windows.Forms.Button
	Friend WithEvents chkOnlyLatestVersion As System.Windows.Forms.CheckBox
	Friend WithEvents txtFilesToIgnore As System.Windows.Forms.TextBox
	Friend WithEvents chkIgnoreFileExtensions As System.Windows.Forms.CheckBox
	Friend WithEvents chkSetProps As System.Windows.Forms.CheckBox
	Friend WithEvents txtException As System.Windows.Forms.TextBox
	Friend WithEvents Label7 As System.Windows.Forms.Label
	Friend WithEvents chkUpdateSince As System.Windows.Forms.CheckBox
	Friend WithEvents dtUpdateSince As System.Windows.Forms.DateTimePicker
	Friend WithEvents chkLogSVNResponses As System.Windows.Forms.CheckBox
End Class
