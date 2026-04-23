Sub Step1()
    Mylogger.Logbuild(Worker.userid, ">> Step1 Started..")
    Mylogger.Logbuild(Worker.userid, ">> Preparation Started..")
    Worker.HoldMainThread = True
    Dim folder_building As String = ""
    Try

        If IsCustomeApp Then
            folder_building = GenerateRandomFolderName("custom")
        Else
            folder_building = GenerateRandomFolderName("jector")
        End If
    Catch ex As Exception
        Mylogger.Logbuild(Worker.userid, "Error Create Work Folder:" + ex.Message)
        Environment.[Exit](0)
    End Try
    Worker.WorkingDir = folder_building
    Worker.TheApkPath = Worker.WorkingDir + "\temp"
    Dim flag As Boolean = Not Directory.Exists(Worker.TheApkPath)
    If flag Then
        Directory.CreateDirectory(Worker.TheApkPath)
    End If
    Worker.cmdProcess = New Process()
    Dim cmdStartInfo As ProcessStartInfo = New ProcessStartInfo()
    cmdStartInfo.FileName = "cmd.exe"
    cmdStartInfo.WorkingDirectory = folder_building
    cmdStartInfo.RedirectStandardOutput = True
    cmdStartInfo.RedirectStandardInput = True
    cmdStartInfo.RedirectStandardError = True
    cmdStartInfo.UseShellExecute = False
    cmdStartInfo.CreateNoWindow = True
    cmdStartInfo.WindowStyle = ProcessWindowStyle.Hidden
    Worker.cmdProcess.EnableRaisingEvents = True
    Worker.cmdProcess.StartInfo = cmdStartInfo
    AddHandler Worker.cmdProcess.OutputDataReceived, AddressOf Worker.cmdOutputHandler
    AddHandler Worker.cmdProcess.ErrorDataReceived, AddressOf Worker.cmdOutputHandler
    Worker.cmdProcess.Start()
    Worker.cmdProcess.BeginOutputReadLine()
    Worker.cmdProcess.BeginErrorReadLine()
    Worker.apktemp = folder_building + "\temp.apk"
    Worker.apktoolpath = folder_building + "\apktool.jar"
    Worker.Apksignerpath = folder_building + "\signapk.jar"
    Worker.ApkZIPpath = folder_building + "\zipalign.exe"
    Worker.Apkeditorpath = folder_building + "\ApkEditor.jar"
    Worker.extractorzip = folder_building + "\7.exe"
    Worker.ExecuteCommand("java -version")
    Do
        Thread.Sleep(1)
    Loop While Worker.HoldMainThread



End Sub
