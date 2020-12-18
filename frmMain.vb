''
'' This program is free software; you can redistribute it and/or modify
'' it under the terms of the GNU General Public License as published by
'' the Free Software Foundation; either version 2 of the License, or
'' (at your option) any later version.
''
'' This program is distributed in the hope that it will be useful,
'' but WITHOUT ANY WARRANTY; without even the implied warranty of
'' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'' GNU General Public License for more details.
''
'' You should have received a copy of the GNU General Public License
'' along with this program; if not, write to the Free Software
'' Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
'' 

Imports SharpSvn
Imports vss5_2 = Microsoft.VisualStudio.SourceSafe.Interop
Imports Microsoft.Win32
Imports System.Text.RegularExpressions



Public Class frmMain

    'Reference to VSS top-level path.
    Dim topItem As SSItem = Nothing

	'Reference to the VSS database.
	Dim db As vss5_2.VSSDatabase

	'Determines whether a checked out item was found.
	Dim checkedOutFlag As Boolean

    'Used to keep track of work in progress
    'so user won't be allowed to close the form.
    Dim working As Boolean = False

    'Counters
    Dim fileCounter As Integer, versionCounter As Integer, pathCounter As Integer

    'List of file extensions to ignore
	Dim filesToIgnore As SortedList(Of String, String)

	Const ESS_VS_NO_DELTA As Integer = &HD691
	Const ESS_VS_NO_VERSION As Integer = &HD689

#Region "Form events"

	Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		' grab the list of ignored files from your svn install.
		Dim regVersion As RegistryKey
		Dim keyValue As String
		keyValue = "Software\\Tigris.org\\Subversion\\Config\\miscellany"
		regVersion = Registry.CurrentUser.OpenSubKey(keyValue, False)
		Dim sVersion As String
		If (Not regVersion Is Nothing) Then
			sVersion = regVersion.GetValue("global-ignores", 0).ToString
			regVersion.Close()

			txtFilesToIgnore.Text += sVersion
		End If
	End Sub

	' Don't allow user to close form
	' if we're in the middle of something.
    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If working Then
            e.Cancel = True
        End If
    End Sub

#End Region

#Region "Simple button handlers"

    'Get the working directory.
    Private Sub cmdGetWorkingDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetWorkingDir.Click
        FBD.SelectedPath = ""
        FBD.Description = "Select the working directory"
        FBD.ShowDialog(Me)
        If FBD.SelectedPath <> "" Then txtWorkingDir.Text = FBD.SelectedPath
    End Sub

    'Get the full path to scrsafe.ini.
    Private Sub cmdGetScrsafeDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGetScrsafeDir.Click
        OFD.Title = "Select scrsafe.ini"
        OFD.FileName = ""
        OFD.ShowDialog(Me)
        If OFD.FileName <> "" Then txtScrsafe.Text = OFD.FileName
    End Sub

    'Rearrange our GUI to start over.
    Private Sub cmdStartOver_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStartOver.Click
		txtLog.Text = ""
		txtException.Text = ""
        cmdMigrate.Enabled = False
        cmdSourceSafeFiles.Enabled = True
        grpGeneral.Enabled = False
        grpVSS.Enabled = True
        grpSVN.Enabled = False
        topItem = Nothing
        If db IsNot Nothing Then
            db.Close()
            db = Nothing
        End If
    End Sub

#End Region

#Region "Main functions"

    'Get source safe files.
    Private Sub cmdSourceSafeFiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSourceSafeFiles.Click
        'Sanity
        If txtScrsafe.Text = "" Then
            MessageBox.Show(Me, "Select the scrsafe.ini file", "Scrsafe.ini ???", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtScrsafe.Focus()
            Exit Sub
        End If

        If txtUserName.Text = "" Then
            MessageBox.Show(Me, "Enter a user name to connect to source safe", "User name", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtUserName.Focus()
            Exit Sub
        End If

        If txtVSSPath.Text = "" Then
            MessageBox.Show(Me, "Enter the source safe path to the project to migrate", "VSS path", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtVSSPath.Focus()
            Exit Sub
        End If

        If db IsNot Nothing Then db.Close()
		db = New vss5_2.VSSDatabase

		EnablePrimaryGUI(False)
        working = True

        Try
            ''
            '' 1. Open a connection to the VSS database.
            ''
            doLog("Opening connection to VSS...")
            db.Open(txtScrsafe.Text, txtUserName.Text, txtPassword.Text)
        Catch ex As Exception
            EnablePrimaryGUI(True)
            working = False
            MessageBox.Show(Me, "Error while connecting to source safe" + vbCrLf + vbCrLf + ex.ToString, "VSS error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

		Dim vssProject As vss5_2.VSSItem = Nothing

		Try
            ''
            '' 2. Find the project specified by the user.
            ''
            doLog("Searching for VSS project...")
            vssProject = db.VSSItem(txtVSSPath.Text, False)
        Catch ex As Exception
            EnablePrimaryGUI(True)
            working = False
            MessageBox.Show(Me, "Error while searching for source safe project" + vbCrLf + vbCrLf + ex.ToString, "VSS error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Try
            ''
            '' 3. Get all project files.
            ''
            doLog("Searching project contents...")
            topItem = Nothing
            checkedOutFlag = False
            fileCounter = 0
            pathCounter = 0
            processItem(vssProject, topItem)
        Catch ex As Exception
            EnablePrimaryGUI(True)
            working = False
            MessageBox.Show(Me, "Error while searching source safe project contents" + vbCrLf + vbCrLf + ex.ToString, "VSS error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        doLog("Done, " + pathCounter.ToString + " paths with " + fileCounter.ToString + " files were found.")

        If chkWarnCheckedOut.Checked Then
            'Positively identify our findings.
            If checkedOutFlag Then
                MessageBox.Show(Me, "At least one item in the source safe project is checked out", "Checked out item present", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                MessageBox.Show(Me, "No item in the source safe project is checked out", "All checked in.", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If

        'Rearrange the GUI to indicate next action.
        working = False
        EnablePrimaryGUI(True)
        cmdSourceSafeFiles.Enabled = False
        grpVSS.Enabled = False
        grpGeneral.Enabled = True
        grpSVN.Enabled = True
        cmdMigrate.Enabled = True

    End Sub

    'Migrate from VSS to SVN.
    Private Sub cmdMigrate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMigrate.Click
        'Sanity
        If txtWorkingDir.Text = "" Then
            MessageBox.Show(Me, "Select the working directory", "Working dir ???", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtWorkingDir.Focus()
            Exit Sub
        End If

        If txtSVNPath.Text = "" Then
            MessageBox.Show(Me, "Select the subversion path where the project will be added", "SVN path", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtSVNPath.Focus()
            Exit Sub
        End If

		' clear the existing logs.
		txtLog.Text = ""
		txtException.Text = ""

		'Append a / to the SVN path if one isn't there.
        If Not txtSVNPath.Text.EndsWith("/") Then
            txtSVNPath.Text += "/"
        End If

        'Find the maximum number of revisions.
        'We'll loop for this number.
        doLog("Finding maximum number of revisions...")
        Dim maxVer As Integer = getMaxVersion(topItem)
        doLog("Max revisions: " + maxVer.ToString)

        working = True
        EnableSecondaryGUI(False)

		Dim client As SvnClient = Nothing

        Try
            'Initiatize the SVN client.
			client = New SvnClient

			'Get notifications.
			If chkLogSVNResponses.Checked Then
				AddHandler client.Notify, AddressOf svnNotify
			End If

			fileCounter = 0
			versionCounter = 0
			pathCounter = 0

			filesToIgnore = New SortedList(Of String, String)
			If chkIgnoreFileExtensions.Checked Then
				Dim lst() As String = txtFilesToIgnore.Text.Split(" "c)
				For Each s As String In lst
					' convert to regex
					s = Regex.Escape(s)
					s = s.Replace("\*", ".*")
					s = s.Replace("\\?", ".")
					s = "^" + s + "$"
					filesToIgnore.Add(s.ToUpper, s.ToUpper)
				Next
			End If

			If chkOnlyLatestVersion.Checked Then
				BuildVSSPathOnePass(txtWorkingDir.Text, topItem)
				DeleteIgnoredFiles(txtWorkingDir.Text + "\" + topItem.Name)
				doLog("Importing version to svn...")
				Dim importArgs As New SvnImportArgs
				importArgs.LogMessage = "Importing " + topItem.Name
				client.Import(txtWorkingDir.Text + "\" + topItem.Name, New Uri(txtSVNPath.Text), importArgs)
			Else
				''
				'' The idea here is to recreate the VSS structure once for every revision.
				'' Once the VSS directory structure is created the first time, everything is 
				'' imported to SVN. For each next run where the VSS directory structure is
				'' updated with the next revisions, the SVN contents are updated via a new
				'' commit operation.
				''
				For i As Integer = 1 To maxVer
					txtLog.Text = ""
					doLog("====== Pass #" + i.ToString + " of " + maxVer.ToString + " ======")
					If i = 1 Then
						' import the initial directories to SVN so we have a set of working copies to work with
						doLog("...Importing initial version to svn...")
						Dim importArgs As New SvnImportArgs
						importArgs.LogMessage = "Importing " + topItem.Name
						Dim importRes As SvnCommitResult
						BuildVSSDirectories(txtWorkingDir.Text, topItem)
						client.Import(txtWorkingDir.Text + "\" + topItem.Name, New Uri(txtSVNPath.Text), importArgs, importRes)
					End If

					doLog("...Migrating versions to svn...")
					' go through fetching revisions, committing changes and optionally setting properties
					UpdateVSSPath(client, "", topItem, i)
					DeleteIgnoredFiles(txtWorkingDir.Text + "\" + topItem.Name)
				Next
			End If

			db.Close()

			If chkDeleteWorkingDir.Checked Then
				'Delete the temporary directory.
				'Unfortunately, that means we have to reset all attributes first.
				doLog("...Removing working directory...")
				ResetAttributes(txtWorkingDir.Text + "\" + topItem.Name)
				IO.Directory.Delete(txtWorkingDir.Text + "\" + topItem.Name, True)
			End If

			MessageBox.Show(Me, "Done." + vbCrLf + _
			 "Paths processed: " + pathCounter.ToString + vbCrLf + _
			 "Files processed: " + fileCounter.ToString + vbCrLf + _
			 "Versions processed: " + versionCounter.ToString, "Done", MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			EnableSecondaryGUI(True)
			working = False
			MessageBox.Show(Me, "Error while migrating project to subversion" + vbCrLf + vbCrLf + ex.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
			Exit Sub
		Finally
			If client IsNot Nothing Then
				client.Dispose()
				client = Nothing
			End If
		End Try

        working = False
        EnablePrimaryGUI(True)

    End Sub

#End Region

#Region "VSS utilities"

	'Recursively create the list of VSS items.
	Private Sub processItem(ByVal item As vss5_2.VSSItem, ByRef sitem As SSItem)
		If sitem Is Nothing Then
			sitem = New SSItem(item)
		End If
		If item.Type = 0 Then
			doLog("Found folder " + item.Name)
			pathCounter += 1
			For Each itm As vss5_2.VSSItem In item.Items
				Dim o As SSItem = sitem.AddSubItem(itm)
				processItem(itm, o)
			Next
		Else
			If item.IsCheckedOut <> vss5_2.VSSFileStatus.VSSFILE_NOTCHECKEDOUT Then
				checkedOutFlag = True
				doLog("Found file " + item.Name + "  <<< Checked out")
			Else
				doLog("Found file " + item.Name)
			End If
			fileCounter += 1
			sitem.AddSubItem(item)
		End If
	End Sub

	'Recursively find out the maximum number of revisions there are in the VSS project.
	Private Function getMaxVersion(ByVal sItem As SSItem) As Integer
        Dim topVer As Integer = 0
        If sItem.Type = SSItemType.File Then
            Return sItem.Versions
        Else
            For Each item As SSItem In sItem.SubItems
                Dim otherVer As Integer = getMaxVersion(item)
                If otherVer > topVer Then topVer = otherVer
            Next
            Return topVer
        End If
    End Function

    'Recursively get the contents of files from VSS.
	Private Sub UpdateVSSPath(ByRef client As SvnClient, ByVal topDir As String, ByVal sItem As SSItem, ByVal currentVersion As Integer)
		'If the item is a file, we need to get it from source safe.
		If sItem.Type = SSItemType.File Then

			'Ignore this file?
			If Not FileMatches(filesToIgnore, sItem.Name) Then
				' first, "cache" the current full path to the file so we don't over-exercise the GC's string handling :)
				Dim currentFilePath, currentFileName As String
				currentFileName = topDir + "\" + sItem.Name
				currentFilePath = txtWorkingDir.Text + currentFileName


				'Is the item's version greater or equal to the current version?
				If sItem.Versions >= currentVersion Then

					'Yes, so we need to fetch it. First, delete the previous version on disk.
					If currentVersion > 1 Then
						Try
							IO.File.SetAttributes(currentFilePath, IO.FileAttributes.Archive)
							'IO.File.Delete(currentFilePath)
						Catch ex As System.IO.FileNotFoundException
							doLogExcept(ex.Message + " - " + currentFileName + " (" + currentVersion.ToString() + ")")
						Catch ex As System.IO.IOException
							doLogExcept(ex.Message + " - " + currentFileName + " (" + currentVersion.ToString() + ")")
						End Try
						versionCounter += 1
					Else
						fileCounter += 1
						versionCounter += 1
					End If

					' ignore this file if its a store-only-latest type
					If sItem.IsSingleVersionFile = False Then

						Dim vItem As vss5_2.VSSItem
						Dim versionok As Boolean = True

						' fetch the relevant VSS version of the file, force replace of the local file
						Try
							vItem = sItem.VSS.Version(currentVersion.ToString)
							vItem.Get(currentFilePath, vss5_2.VSSFlags.VSSFLAG_REPREPLACE Or vss5_2.VSSFlags.VSSFLAG_TIMEUPD)
						Catch ex As System.Runtime.InteropServices.COMException When (ex.ErrorCode And &HFFFF) = ESS_VS_NO_DELTA
							'If we tried to retrieve the history of a single-version file, we got an exception.
							'Rightmost 2 bytes contain the VSS API error code. If it's the specific error
							'we're looking for, just get the latest version of the file and mark the sItem
							'instance so we don't try again (item #6255).
							sItem.IsSingleVersionFile = True

							vItem = sItem.VSS
							vItem.Get(currentFilePath, vss5_2.VSSFlags.VSSFLAG_REPREPLACE Or vss5_2.VSSFlags.VSSFLAG_TIMEUPD)
						Catch ex As System.Runtime.InteropServices.COMException
							doLogExcept(ex.Message + " - " + currentFileName + " (" + currentVersion.ToString() + ")")
							Exit Sub
						End Try

						''
						'' now add the file to SVN
						''
						Dim commitRes As SvnCommitResult
						If currentVersion = 1 Then
							' as this is the first revision, add it to the repository, then commit it. 
							Try
								doLog("Adding: " + currentFileName)
								client.Add(currentFilePath)

								Dim commitArgs As New SvnCommitArgs
								commitArgs.LogMessage = vItem.VSSVersion.Comment
								client.Commit(currentFilePath, commitArgs, commitRes)

							Catch ex As System.Runtime.InteropServices.COMException When (ex.ErrorCode And &HFFFF) = ESS_VS_NO_VERSION
								' Occasionally VSS doesn't return version info!
								' so if we get this error, skip it. 
								doLogExcept(ex.Message + " - " + currentFileName + " (" + currentVersion.ToString() + ")")
								versionok = False
							Catch ex As System.Runtime.InteropServices.COMException
								doLogExcept(ex.Message + " " + ex.ErrorCode.ToString() + " (" + currentFileName + ")" + " (" + currentVersion.ToString() + ")")
								versionok = False
							Catch ex As SvnException
								doLogExcept(ex.Message + " " + ex.SubversionErrorCode.ToString() + " (" + currentFileName + ")" + " (" + currentVersion.ToString() + ")")
								versionok = False
							End Try
						ElseIf sItem.Versions > 1 Then
							Dim commitArgs As New SvnCommitArgs
							Try
								' check for only updating latest versions
								If chkUpdateSince.Checked = False Or (chkUpdateSince.Checked And sItem.VSS.Version(currentVersion.ToString).VSSVersion.Date > CDate(dtUpdateSince.Text)) Then
									doLog("Updating: " + currentFileName)
									commitArgs.LogMessage = vItem.VSSVersion.Comment
									client.Commit(currentFilePath, commitArgs, commitRes)
								End If
							Catch ex As System.Runtime.InteropServices.COMException When (ex.ErrorCode And &HFFFF) = ESS_VS_NO_VERSION
								' Occasionally VSS doesn't return version info!
								' so if we get this error, skip it. 
								doLogExcept(ex.Message + " - " + currentFileName + " (" + currentVersion.ToString() + ")")
								commitArgs.LogMessage = " "
								versionok = False
							Catch ex As System.Runtime.InteropServices.COMException
								doLogExcept(ex.Message + " " + ex.ErrorCode.ToString() + " (" + currentFileName + ")" + " (" + currentVersion.ToString() + ")")
								versionok = False
							Catch ex As SvnException
								doLogExcept(ex.Message + " " + ex.SubversionErrorCode.ToString() + " (" + currentFileName + ")" + " (" + currentVersion.ToString() + ")")
								versionok = False
							End Try
						End If ' versions > 1


						' if the file was actually committed, change its revprops
						' ie. some files do not change in VSS but still show as versions (eg branches)
						If (Not (commitRes Is Nothing)) And versionok Then
							Dim uri As New SharpSvn.SvnUriTarget(New Uri(txtSVNPath.Text + currentFileName), commitRes.Revision)

							' if we want revision properties updated, then set them
							' NOTE: you will have to set the pre-revision hook in the SVN Server:
							'a simple "exit /b 0" will be sufficient to allow the import to run.
							If chkSetProps.Checked Then
								' already done as part of commit
								'If vItem.VSSVersion.Comment.Length > 0 Then
								'client.SetRevisionProperty(uri, "svn:log", vItem.Comment)
								'End If

								' change user if necessary
								If (vItem.VSSVersion.Username <> txtUserName.Text) Then
									client.SetRevisionProperty(uri, "svn:author", vItem.VSSVersion.Username)
								End If
								' set date to VSS date, in format 2008-10-30T17:46:06.0000000Z
								client.SetRevisionProperty(uri, "svn:date", vItem.VSSVersion.Date.ToString("O") + "Z")

								' just to maintain history, set a revprop if the VSS item was labelled. 
								If Not (vItem.VSSVersion.Label = Nothing) Then
									client.SetRevisionProperty(uri, "vss:label", vItem.VSSVersion.Label)
								End If
							End If
						End If ' not commitres
					End If ' singleversion = false

				End If
			End If

		Else
			'Item is a directory with possible subitems.
			'Create the directory and process subitems.
			'IO.Directory.CreateDirectory(txtWorkingDir.Text + topDir + "\" + sItem.Name)	- no need to create, its already done to get the working directories.
			If currentVersion = 1 Then pathCounter += 1
			For Each item As SSItem In sItem.SubItems
				UpdateVSSPath(client, topDir + "\" + sItem.Name, item, currentVersion)
			Next
		End If
	End Sub

	'Same as above but only gets the initial version.
	Private Sub BuildVSSPathInitialPass(ByVal topDir As String, ByVal sItem As SSItem)
		'If the item is a file, we need to get it from source safe.
		If sItem.Type = SSItemType.File Then
			fileCounter += 1
			versionCounter += 1
			doLog("Retrieving " + topDir + "\" + sItem.Name)

			If sItem.Versions = 1 Then
				'This special case is separate because VSS is much faster if we
				'retrieve the latest version instead of going into history.
				sItem.VSS.Get(topDir + "\" + sItem.Name, vss5_2.VSSFlags.VSSFLAG_KEEPNO Or vss5_2.VSSFlags.VSSFLAG_TIMEUPD)
			Else
				'We need a specific version.
				Try
					sItem.VSS.Version(1).Get(topDir + "\" + sItem.Name, vss5_2.VSSFlags.VSSFLAG_KEEPNO Or vss5_2.VSSFlags.VSSFLAG_TIMEUPD)
				Catch ex As Exception
					' if getting the historical version failed, just get the latest one.
					sItem.VSS.Get(topDir + "\" + sItem.Name, vss5_2.VSSFlags.VSSFLAG_KEEPNO Or vss5_2.VSSFlags.VSSFLAG_TIMEUPD)
				End Try
			End If

		Else
			'Item is a directory with possible subitems.
			'Create the directory and process subitems.
			IO.Directory.CreateDirectory(topDir + "\" + sItem.Name)
			pathCounter += 1
			For Each item As SSItem In sItem.SubItems
				BuildVSSPathInitialPass(topDir + "\" + sItem.Name, item)
			Next
		End If
	End Sub

	'Same as above but only gets the latest version.
	Private Sub BuildVSSPathOnePass(ByVal topDir As String, ByVal sItem As SSItem)
		'If the item is a file, we need to get it from source safe.
		If sItem.Type = SSItemType.File Then
			fileCounter += 1
			versionCounter += 1
			doLog("Retrieving " + topDir + "\" + sItem.Name)
			sItem.VSS.Get(topDir + "\" + sItem.Name, vss5_2.VSSFlags.VSSFLAG_KEEPNO Or vss5_2.VSSFlags.VSSFLAG_TIMEUPD)
		Else
			'Item is a directory with possible subitems.
			'Create the directory and process subitems.
			IO.Directory.CreateDirectory(topDir + "\" + sItem.Name)
			pathCounter += 1
			For Each item As SSItem In sItem.SubItems
				BuildVSSPathOnePass(topDir + "\" + sItem.Name, item)
			Next
		End If
	End Sub

	'Same as above but only creates the directory tree.
	Private Sub BuildVSSDirectories(ByVal topDir As String, ByVal sItem As SSItem)
		If sItem.Type = SSItemType.Path Then
			'Item is a directory with possible subitems.
			'Create the directory and process subitems.
			IO.Directory.CreateDirectory(topDir + "\" + sItem.Name)
			pathCounter += 1
			For Each item As SSItem In sItem.SubItems
				BuildVSSDirectories(topDir + "\" + sItem.Name, item)
			Next
		End If
	End Sub

#End Region

#Region "SVN events"

	'Receive SVN notifications: 
	'for initial adds, this will be Add, CommitAdded, CommitSendData.
	'for subsequent commits, this will be CommitModified, CommitSendData
	Private Sub svnNotify(ByVal sender As Object, ByVal e As SvnNotifyEventArgs)
		doLog("SVN " + e.Action.ToString + " for " + e.FullPath)
	End Sub

#End Region

#Region "GUI and other helpers"

    Private Sub EnableSecondaryGUI(ByVal flag As Boolean)
        grpSVN.Enabled = flag
        grpGeneral.Enabled = flag
        cmdMigrate.Enabled = flag
        cmdStartOver.Enabled = flag
        If flag Then
            Cursor = Cursors.Default
        Else
            Cursor = Cursors.WaitCursor
        End If
    End Sub

    Private Sub EnablePrimaryGUI(ByVal flag As Boolean)
        grpVSS.Enabled = flag
        cmdSourceSafeFiles.Enabled = flag
        cmdStartOver.Enabled = flag
        If flag Then
            Cursor = Cursors.Default
        Else
            Cursor = Cursors.WaitCursor
        End If
    End Sub

    Private Sub doLog(ByVal s As String)
        txtLog.AppendText(s + vbCrLf)
        txtLog.ScrollToCaret()
        Application.DoEvents()
    End Sub
	Private Sub doLogExcept(ByVal s As String)
		txtException.AppendText(s + vbCrLf)
		txtException.ScrollToCaret()
		Application.DoEvents()
	End Sub

    Private Sub ResetAttributes(ByVal topDir As String)
        Dim files() As String = IO.Directory.GetFiles(topDir, "*.*", IO.SearchOption.AllDirectories)
        For Each file As String In files
            IO.File.SetAttributes(file, IO.FileAttributes.Archive)
        Next
        'Dim dirs() As String = IO.Directory.GetDirectories(topDir, "*.*")
        'For Each dir As String In dirs
        '    ResetAttributes(dir)
        'Next
    End Sub

    Private Sub ResetFileAttributes(ByVal fileName As String)
        IO.File.SetAttributes(fileName, IO.FileAttributes.Archive)
    End Sub

    Private Sub DeleteIgnoredFiles(ByVal topDir As String)
        If filesToIgnore.Count = 0 Then Exit Sub
        Dim files() As String = IO.Directory.GetFiles(topDir, "*.*", IO.SearchOption.AllDirectories)
        For Each file As String In files
			If FileMatches(filesToIgnore, file) Then
				ResetFileAttributes(file)
				IO.File.Delete(file)
			End If
        Next
    End Sub

	Private Function FileMatches(ByRef list As SortedList(Of String, String), ByVal fileName As String) As Boolean
		' use regex matching
		For Each m As String In list.Values
			If Regex.IsMatch(fileName.ToUpper, m) Then
				Return True
			End If
		Next

		Return False
	End Function



	Private Sub chkUpdateSince_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUpdateSince.CheckedChanged
		chkOnlyLatestVersion.Enabled = Not chkUpdateSince.Checked
	End Sub

	Private Sub chkOnlyLatestVersion_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOnlyLatestVersion.CheckedChanged
		chkUpdateSince.Enabled = Not chkOnlyLatestVersion.Checked
	End Sub
#End Region

End Class