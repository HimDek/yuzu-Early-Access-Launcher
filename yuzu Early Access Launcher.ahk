#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
; #Warn  ; Enable warnings to assist with detecting common errors.
#SingleInstance Force
#NoTrayIcon
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

versionnumber:="0.3.0-beta"
version:=StrReplace("vnumber", "number", versionnumber)
versionname:=StrReplace("Version number", "number", versionnumber)

FileInstall, 7za.exe, %A_temp%\7za.exe, 1
FileInstall, 7z.NET.dll, %A_temp%\7z.NET.dll, 1
FileInstall, jq.exe, %A_temp%\jq.exe, 1
FileInstall, logo.png, %A_temp%\logo.png, 1
FileInstall, load1.png, %A_temp%\load1.png, 1
FileInstall, load2.png, %A_temp%\load2.png, 1

yDir:=StrReplace(A_Temp, "Temp", "yuzu")
If (A_ScriptDir!=yDir) {
	If (!FileExist(yDir)) {
		FileCreateDir, %yDir%
		If (ErrorLevel) {
			MsgBox, % 16+262144, , Directory %yDir% Could not be Created.`nTry Running as Administrator.
			ExitApp
		}
	}
	FileCopy, %A_ScriptFullPath%, %yDir%, 1
	If (ErrorLevel) {
		MsgBox, % 16+262144, , Error 1.`nTry Running as Administrator.
		ExitApp
	}
	Path:=yDir . "\" . A_ScriptName
	Run, %Path%, %yDir%, UseErrorLevel
	If (ErrorLevel=="ERROR") {
		MsgBox, % 16+262144, , Error 2.`nTry Running as Administrator.
		ExitApp
	}
	FileDelete, %A_temp%\7za.exe
	FileDelete, %A_temp%\7z.NET.dll
	FileDelete, %A_temp%\jq.exe
	FileDelete, %A_temp%\logo.png
	FileDelete, %A_temp%\load1.png
	FileDelete, %A_temp%\load2.png
	ExitApp
}

FileCreateShortcut, %A_ScriptFullPath%, %A_Desktop%\yuzu Early Access Launcher.lnk, , , Launch yuzu Early Access
FileCreateShortcut, %A_ScriptFullPath%, %A_Programs%\yuzu Early Access Launcher.lnk, , , Launch yuzu Early Access
If (ErrorLevel) {
	MsgBox, % 16+262144, , Could not Create Desktop Shortcut.`nTry Running as Administrator.
}

Global DownloadTask:=0
GoSub MainGUI
GoSub, SysInfo
GoSub ControlGUI
OnExit("ExitFunc")

Return

UpdateLauncher:
	GUIControl, Main:, U, Checking for Launcher Updates!
	FileDelete, %A_temp%\launcher.ini
	FileDelete, %A_temp%\yuzulauncher.json
	FileDelete, %A_temp%\%A_ScriptName%
	
	UrlDownloadToFile, https://api.github.com/repos/HiDe-Techno-Tips/yuzu-Early-Access-Launcher/releases/latest, %A_temp%\yuzulauncher.json
	
	RunWait, cmd.exe /c echo [latest] > %A_temp%\launcher.ini,, Hide UseErrorLevel
	RunWait, cmd.exe /c for /f `%l in ('%A_temp%\jq.exe .tag_name %A_temp%\yuzulauncher.json') do echo version=`%l >> %A_temp%\launcher.ini,, Hide UseErrorLevel
	RunWait, cmd.exe /c for /f `%l in ('%A_temp%\jq.exe .assets[0].browser_download_url %A_temp%\yuzulauncher.json') do echo url=`%l >> %A_temp%\launcher.ini,, Hide UseErrorLevel
	RunWait, cmd.exe /c for /f `%l in ('%A_temp%\jq.exe .assets[0].size %A_temp%\yuzulauncher.json') do echo lsize=`%l >> %A_temp%\launcher.ini,, Hide UseErrorLevel
	
	If (ErrorLevel=="ERROR") {
		MsgBox, % 16+262144, , Error 3.`nTry Running as Administrator.
		FileDelete, %A_temp%\launcher.ini
		FileDelete, %A_temp%\yuzulauncher.json
		ExitApp
	}
	
	If (FileExist(A_temp . "\launcher.ini")) {
		IniRead, launcherlatest, %A_temp%\launcher.ini, latest, version
		IniRead, lurl, %A_temp%\launcher.ini, latest, url
		IniRead, lsize, %A_temp%\launcher.ini, latest, lsize
	}
	
	FileDelete, %A_temp%\launcher.ini
	FileDelete, %A_temp%\yuzulauncher.json

	If (launcherlatest!="" && launcherlatest!="ERROR" && launcherlatest!=ERROR && launcherlatest!="NULL" && launcherlatest!=NULL) {
		If (version!=launcherlatest) {
			tempPath:=A_Temp . "\" . A_ScriptName
			DownloadFile(lurl, A_ScriptName, lsize)
			Run, %tempPath%, %A_Temp%, UseErrorLevel
			If (ErrorLevel=="ERROR") {
				MsgBox, % 16+262144, , Error 4.`nTry Running as Administrator.
				FileDelete, %A_temp%\%A_ScriptName%
				ExitApp
			}
			FileDelete, %A_temp%\%A_ScriptName%
			ExitApp
		}
	}
Return

CheckUpdates:
	GoSub, UpdateLauncher
	GUIControl, Main:, U, Retrieving Metadata!

	FileDelete, %A_temp%\latest.ini
	FileDelete, %A_temp%\yuzuea.json
	FileDelete, %A_temp%\switch.json

	UrlDownloadToFile, https://api.github.com/repos/pineappleEA/pineapple-src/releases/latest, %A_temp%\yuzuea.json
	UrlDownloadToFile, https://hide-techno-tips.github.io/Nintendo-Switch-Files, %A_temp%\switch.json

	RunWait, cmd.exe /c echo [latest] > %A_temp%\latest.ini,, Hide UseErrorLevel
	RunWait, cmd.exe /c for /f `%l in ('%A_temp%\jq.exe .tag_name %A_temp%\yuzuea.json') do echo version=`%l >> %A_temp%\latest.ini,, Hide UseErrorLevel
	RunWait, cmd.exe /c for /f `%l in ('%A_temp%\jq.exe .assets[0].browser_download_url %A_temp%\yuzuea.json') do echo url=`%l >> %A_temp%\latest.ini,, Hide UseErrorLevel
	RunWait, cmd.exe /c for /f `%l in ('%A_temp%\jq.exe .assets[0].size %A_temp%\yuzuea.json') do echo ysize=`%l >> %A_temp%\latest.ini,, Hide UseErrorLevel

	RunWait, cmd.exe /c for /f `%l in ('%A_temp%\jq.exe .firmware.ver %A_temp%\switch.json') do echo firm=`%l >> %A_temp%\latest.ini,, Hide UseErrorLevel
	RunWait, cmd.exe /c for /f `%l in ('%A_temp%\jq.exe .firmware.url %A_temp%\switch.json') do echo furl=`%l >> %A_temp%\latest.ini,, Hide UseErrorLevel
	RunWait, cmd.exe /c for /f `%l in ('%A_temp%\jq.exe .firmware.size %A_temp%\switch.json') do echo fsize=`%l >> %A_temp%\latest.ini,, Hide UseErrorLevel
	RunWait, cmd.exe /c for /f `%l in ('%A_temp%\jq.exe .keys.url %A_temp%\switch.json') do echo kurl=`%l >> %A_temp%\latest.ini,, Hide UseErrorLevel

	If (ErrorLevel=="ERROR") {
		MsgBox, % 16+262144, , Error 5.`nTry Running as Administrator.
		FileDelete, %A_temp%\yuzuea.json
		FileDelete, %A_temp%\switch.json
		ExitApp
	}

	FileDelete, %A_temp%\yuzuea.json
	FileDelete, %A_temp%\switch.json
Return

GetInfo:
	GUIControl, Main:, U, Checking Files!
	FileDelete, %A_temp%\downloaded.ini
	FileDelete, %A_temp%\system.ini

	RunWait, cmd.exe /c echo [downloaded] > %A_temp%\downloaded.ini,, Hide UseErrorLevel
	RunWait, cmd.exe /c for /f "tokens=4 delims=-" `%b in ('where "Windows-Yuzu-EA-*.7z"') do echo version=`%~nb >> %A_temp%\downloaded.ini,, Hide UseErrorLevel
	RunWait, cmd.exe /c for /f "tokens=3 delims=-" `%b in ('where "Switch-Firmware-*.zip"') do echo firm=`%~nb >> %A_temp%\downloaded.ini,, Hide UseErrorLevel

	If (FileExist("installed.ini")) {
		IniRead, installed, installed.ini, installed, version
		IniRead, firm, installed.ini, installed, firm
	}
	If (installed=="" || installed=="ERROR"){
		If (FileExist("yuzu-windows-msvc-early-access\yuzu.exe")) {
			installed:="pre"
		}
	}
	Else {
		If (!FileExist("yuzu-windows-msvc-early-access\yuzu.exe")) {
			installed:="ERROR"
		}
	}
	If (firm!="" && firm!="ERROR") {
		infirm:=StrReplace("Installed Version: firm`n", "firm", firm)
		GUI, Font, Q5 s10 Norm
		GUIControl, Main:Font, Firmtxt
	}
	Else {
		infirm:="Firmware is not Installed. Some Games may not work.`n"
		GUI, Font, Q5 s10 Bold
		GUIControl, Main:Font, Firmtxt
	}

	GUIControl, Main:, U, Reading Metadata!

	If (FileExist(A_temp . "\latest.ini")) {
		IniRead, latest, %A_temp%\latest.ini, latest, version
		IniRead, url, %A_temp%\latest.ini, latest, url
		IniRead, ysize, %A_temp%\latest.ini, latest, ysize
		IniRead, lfirm, %A_temp%\latest.ini, latest, firm
		IniRead, furl, %A_temp%\latest.ini, latest, furl
		IniRead, fsize, %A_temp%\latest.ini, latest, fsize
		IniRead, kurl, %A_temp%\latest.ini, latest, kurl
	}

	FileDelete, %A_temp%\latest.ini
	
	latest:=StrReplace(latest, """")
	latest:=SubStr(latest, 4)
	size:=StrReplace(size, """")
	
	If (latest="" || latest==ERROR || latest=="ERROR" || latest==NULL || latest=="NULL" || lfirm=="" || lfirm==ERROR || lfirm=="ERROR" || lfirm==NULL || lfirm=="NULL") {
		Updates:=0
		IniRead, latest, %A_temp%\downloaded.ini, downloaded, version
		IniRead, lfirm, %A_temp%\downloaded.ini, downloaded, firm
	}
	Else {
		Updates:=1
		GUIControl, Main:, U, Updating prod.keys!
		GoSub, Dprod
	}
	FileDelete, %A_temp%\downloaded.ini
	file:=StrReplace("Windows-Yuzu-EA-latest.7z", "latest", latest)
	firfile:=StrReplace("Switch-Firmware-latest.zip", "latest", lfirm)
Return

SysInfo:
	RunWait, cmd.exe /c echo [system] | find /v "" > %A_Temp%\system.ini,, Hide UseErrorLevel
	RunWait, cmd.exe /c wmic OS get OSArchitecture /format:list | find /v "" >> %A_Temp%\system.ini,, Hide UseErrorLevel
	RunWait, cmd.exe /c wmic cpu get Name /format:list | find /v "" >> %A_Temp%\system.ini,, Hide UseErrorLevel
	RunWait, cmd.exe /c wmic cpu get NumberOfLogicalProcessors /format:list | find /v "" >> %A_Temp%\system.ini,, Hide UseErrorLevel
	RunWait, cmd.exe /c wmic cpu get MaxClockSpeed /format:list | find /v "" >> %A_Temp%\system.ini,, Hide UseErrorLevel
	RunWait, cmd.exe /c wmic memorychip get Capacity /format:list | find /v "" >> %A_Temp%\system.ini,, Hide UseErrorLevel

	If (ErrorLevel=="ERROR") {
		MsgBox, % 16+262144, , Error 6.`nTry Running as Administrator.
		FileDelete, %A_temp%\system.ini
		ExitApp
	}
	
	If (FileExist(A_temp . "\system.ini")) {
		IniRead, OSArchitecture, %A_Temp%\system.ini, system, OSArchitecture
		IniRead, RAMCapacity, %A_Temp%\system.ini, system, Capacity
		IniRead, MaxClockSpeed, %A_Temp%\system.ini, system, MaxClockSpeed
		IniRead, NumberOfLogicalProcessors, %A_Temp%\system.ini, system, NumberOfLogicalProcessors
		IniRead, CPUName, %A_Temp%\system.ini, system, Name
	}
	FileDelete, %A_temp%\system.ini
	If (OSArchitecture!="64-bit") {
		Global DownloadTask:=1
		GUIControl, Main:, U, 32-bit Windows detected. yuzu won't work at all.`nIf you have a 64-bit Processor, try after installing 64-bit Windows.
		GUIControl, Main:, Bs2, Ok
		GUIControl, Main:Show, Bs2
	}
	
	RAMAmount:=StrReplace("amount GiB", "amount", RAMCapacity // 1073741824)
	If (RAMCapacity<8000000000) {
		GUI, Font, Q5 s7 cOlive Bold
		GuiControl, Main:Font, RAM
		GUI, Font, Q5 s7 cBlack Norm
		RAMStatus:="is not enough for yuzu to load heavy games."
		If (RAMCapacity<6000000000) {
			GUI, Font, Q5 s7 cRed Bold
			GuiControl, Main:Font, RAM
			GUI, Font, Q5 s7 cBlack Norm
			RAMStatus:="is not enough for yuzu to load large games."
			If (RAMCapacity<4000000000) {
				RAMStatus:="is enough for yuzu to load only light or small games."
				If (RAMCapacity<2000000000) {
					RAMStatus:="is not enough for yuzu to load any game."
				}
			}
		}
	}
	Else {
		RAMStatus:="is enough for yuzu to load most games."
		If (RAMCapacity>12000000000) {
			RAMStatus:="is enough for yuzu to load even the heaviest game."
			If (RAMCapacity>16000000000) {
				RAMStatus:="is more than enough for yuzu."
			}
		}
	}
	If (NumberOfLogicalProcessors<2) {
		GUI, Font, Q5 s7 cRed Bold
		GuiControl, Main:Font, CPU
		GUI, Font, Q5 s7 cBlack Norm
		CPUStatus:="is not powerful enough to handle yuzu."
	}
	If (NumberOfLogicalProcessors<4) {
		If (MaxClockSpeed<2000) {
			GUI, Font, Q5 s7 cRed Bold
			GuiControl, Main:Font, CPU
			GUI, Font, Q5 s7 cBlack Norm
			CPUStatus:="is not powerful enough to handle yuzu."
		}
		Else {
			GUI, Font, Q5 s7 Bold
			GuiControl, Main:Font, CPU
			GUI, Font, Q5 s7 Norm
			CPUStatus:="might handle Single Core Emulation in yuzu."
			If (MaxClockSpeed>2700) {
				CPUStatus:="should handle Single Core Emulation in yuzu."
				If (MaxClockSpeed>3400) {
					CPUStatus:="will handle Single Core Emulation in yuzu."
				}
			}
		}
	}
	Else {
		If (MaxClockSpeed<2000) {
			GUI, Font, Q5 s7 Bold
			GuiControl, Main:Font, CPU
			GUI, Font, Q5 s7 Norm
			CPUStatus:="might handle Multi Core Emulation in yuzu."
			If (MaxClockSpeed<1500) {
			GUI, Font, Q5 s7 cRed Bold
			GuiControl, Main:Font, CPU
			GUI, Font, Q5 s7 cBlack Norm
				CPUStatus:="is not powerful enough to handle yuzu."
			}
		}
		Else {
			CPUStatus:="might be powerful enough to handle yuzu."
			If (MaxClockSpeed>2700) {
				CPUStatus:="should be powerful enough to handle yuzu."
				If (MaxClockSpeed>3400) {
					CPUStatus:="is powerful enough to handle yuzu."
				}
			}
		}
	}
	GuiControl, Main:, CPU, CPU: %CPUName%
	GuiControl, Main:, RAM, RAM: %RAMAmount%
	GuiControl, Main:, CPU2, This CPU %CPUStatus%
	GuiControl, Main:, RAM2, This amount of RAM %RAMStatus%
	GuiControl, Main:Show, Sys
	GuiControl, Main:Show, CPU
	GuiControl, Main:Show, RAM
	GuiControl, Main:Show, CPU2
	GuiControl, Main:Show, RAM2
Return

MainGUI:
	GUI, Main:New, -MinimizeBox, yuzu Early Access Launcher %versionname%

	GUI, Add, Picture, xm+150 ym+5 h216 w-1, %A_temp%\logo.png
	GUI, Font, Q5 s15 Bold
	GUI, Add, Text, xm ym+240 w710 h60 0x1 vI0, yuzu Early Access is not Installed!
	GUI, Add, Text, xm ym+265 w710 h30 0x1 vU, Processing!
	GUI, Font, Q5 s11 Norm

	GUI, Add, Button, xm ym+225 w350 h60 vB1 gB1,
	GUI, Add, Button, xm+360 yp w350 h60 vB2 gB2,
	GUI, Font, Q5 s14 Norm
	GUI, Add, Button, xm yp w710 h60 vB12 gB12,

	GUI, Font, Q5 s10 Bold
	GUI, Add, GroupBox, xm yp+70 w710 h60 vFirm, Firmware:
	GUI, Font, Q5 s10 Norm
	GUI, Add, Text, xm+10 yp+25 w470 h30 vFirmtxt,
	GUI, Font, Q5 s10 Norm
	GUI, Add, Button, xm+480 yp-7 w220 h30 vBs1 gDFirm,

	GUI, Font, Q5 s13 Norm
	GUI, Add, GroupBox, xm ym+225 w710 h130 vTask
	GUI, Add, Progress, Section xm+10 yp+30 w690 h15 vProgressBar -Smooth
	GUI, Add, Text, xm+10 yp+25 w280 h30 vProgressN
	GUI, Add, Text, xm+290 yp w400 h30 0x2 vKB
	GUI, Add, Text, xm+10 yp+40 w410 h30 vST
	ProgressGUI("Hide")

	GUI, Font, Q5 s11 Norm
	GUI, Add, Text, xm+5 ym+372 w470 h30 vU0,
	GUI, Font, Q5 s7 Norm
	GUI, Add, Button, xm+480 yp-7 w230 h30 vBs2 gBs2,

	GUI, Font, Q5 s10 Bold
	GUI, Add, GroupBox, xm yp+35 w710 h70 vSys, System Info:
	GUI, Font, Q5 s7 Norm
	GUI, Add, Text, xm+10 yp+25 w500 h15 vCPU,
	GUI, Add, Text, xm+320 yp w380 h15 0x2 vCPU2,
	GUI, Add, Text, xm+10 yp+20 w500 h15 vRAM,
	GUI, Add, Text, xm+320 yp w380 h15 0x2 vRAM2,

	GuiControl, Main:HiDe, Sys
	GuiControl, Main:HiDe, CPU
	GuiControl, Main:HiDe, RAM
	GuiControl, Main:HiDe, CPU2
	GuiControl, Main:HiDe, RAM2

	GUI, Font, Q5 s10 Bold
	GUI, Add, GroupBox, xm yp+35 w710 h100, Help and Support:
	GUI, Font, Q5 s7 Norm
	GUI, Add, Button, xm+10 yp+20 w220 h30 vHb1 gHb1, View Video Guide
	GUI, Add, Button, xm+240 yp w230 h30 vHb2 gHb2, Support Me
	GUI, Add, Button, xm+480 yp w220 h30 vHb3 gHb3, Report an Issue
	GUI, Add, Button, xm+10 yp+40 w220 h30 vHb4 gHb4, Check Compatibility of a Game
	GUI, Add, Button, xm+240 yp w230 h30 vHb5 gHb5, Ask about a Problem
	GUI, Add, Button, xm+480 yp w220 h30 vHb6 gHb6, More Information

	GUIButtons("Hide")
	GUIControl, Main:Hide, I0
	GUIControl, Main:Hide, U0
	GUIControl, Main:Show, U
	GUI, Main:Show
	Global OldOutputVarControl
	SetTimer, ButtonToolTip, 100
Return

ButtonToolTip:
	MouseGetPos,,,, OutputVarControl
	If (OutputVarControl==OldOutputVarControl) {
		Exit
	}
	OldOutputVarControl:=OutputVarControl
	SetTimer, ButtonToolTipOn, Off
	ToolTip,

	If (OutputVarControl=="Button12") {
		ToolTip:="Report an Issue with this Launcher."
		SetTimer, ButtonToolTipOn, -1000
	}
		If (OutputVarControl=="Button13") {
		ToolTip:="Check the Compatibiliy of different Games in yuzu as reported by Players using different hardwares from all over the World."
		SetTimer, ButtonToolTipOn, -1000
	}
		If (OutputVarControl=="Button14") {
		ToolTip:="Visit yuzu FAQ page to find the Solutions to Common Problems in yuzu."
		SetTimer, ButtonToolTipOn, -1000
	}
		If (OutputVarControl=="Button15") {
		ToolTip:="More Information about yuzu and this Launcher."
		SetTimer, ButtonToolTipOn, -1000
	}
	Exit

	ButtonToolTipOn:
		ToolTip, %ToolTip%
	Exit
Return

MainGUIClose:
	ExitApp
Return

Hb1:
	MsgBox, % 0+64+262144, , Not available yet!
Return

Hb2:
	SetTimer, Sub, -1
Return

Hb3:
	Run, "https://github.com/HiDe-Techno-Tips/yuzu-Early-Access-Launcher/issues/new"
Return

Hb4:
	Run, "https://yuzu-emu.org/game/"
Return

Hb5:
	Run, "https://yuzu-emu.org/wiki/faq/"
Return

Hb6:
	GUI, Info:New, -MinimizeBox, yuzu and yuzu Early Access Launcher %versionname% Info

	GUI, Add, Picture, xm+20 ym h216 w-1, %A_temp%\logo.png

	GUI, Font, Q5 s14 Bold
	GUI, Add, GroupBox, xm ym+210 w460 h140, yuzu
	GUI, Font, Q5 s13 Norm
	GUI, Add, Text, xm+10 yp+40, yuzu is an experimental open-source emulator for the`nNintendo Switch Licensed under GPLv2.0.
	GUI, Font, Q5 s7 Bold
	GUI, Add, Link, xm+10 yp+60, <a href=""></a><a href="https://yuzu-emu.org/">Website</a> | <a href="https://github.com/yuzu-emu">Source Code</a> | <a href="https://github.com/yuzu-emu/yuzu/graphs/contributors">Contributers</a> | <a href="https://github.com/yuzu-emu/yuzu/blob/master/license.txt">License</a>
	GUI, Font, Q5 s7 Norm
	GUI, Add, Text, xm+10 yp+15, "Nintendo Switch" is a trademark of Nintendo`, yuzu is not affiliated with Nintendo in any way.

	GUI, Font, Q5 s14 Bold
	GUI, Add, GroupBox, xm yp+40 w460 h200, yuzu Early Acces Launcher
	GUI, Font, Q5 s13 Norm
	GUI, Add, Text, xm+10 yp+40, %versionname%`n`nThis Launcher can install and keep yuzu Early Access`nalong with prod.key and Firmware updated for free.
	GUI, Font, Q5 s7 Bold
	GUI, Add, Link, xm+10 yp+100, <a href=""></a><a href="https://github.com/HiDe-Techno-Tips/yuzu-Early-Access-Launcher">Source Code</a> | <a href="https://github.com/pineappleEA/pineapple-src/releases">Early Access Source</a> | <a href="https://github.com/emuworld/aio/blob/master/prod.keys">prod.keys Source</a> | <a href="https://archive.org/download/nintendo-switch-global-firmwares/">Firmware Source</a>
	GUI, Font, Q5 s7 Norm
	GUI, Add, Text, xm+10 yp+15, "yuzu and yuzu Early Access" is developed by Team yuzu.`nThe maker of this Launcher is not affiliated with Nintendo or Team yuzu in any way.

	GUI, Info:Show
Return

B1:
	Run, yuzu-windows-msvc-early-access\yuzu.exe, , UseErrorLevel
	If (ErrorLevel=="ERROR") {
		MsgBox, % 16+262144, , Error 7.`nTry Running as Administrator.
	}
	ExitApp
Return

B2:
	GoSub Dyuzu
Return

B12:
	If (Updates==0) {
		If (installed=="" || installed=="ERROR") {
			GoSub, Dyuzu
		}
		Else {
			GoSub, B1
		}
	}
	If (Updates==1) {
		GoSub, Dyuzu
	}
Return

Sub:
	Run, "https://www.youtube.com/channel/UCy3fBVKd0RMY05CgiiuGqSA?sub_confirmation=1"
	Loop {
		CoordMode Pixel, screen
		ImageSearch, imageX, imageY, 0, 0, A_ScreenWidth, A_ScreenHeight, *100 load1.png
		if (imageY > 1) {
			Break
		}
		ImageSearch, imageX, imageY, 0, 0, A_ScreenWidth, A_ScreenHeight, *100 load2.png
		if (imageY > 1) {
			Break
		}
		sleep 100
	}
	SendInput, {Tab}
	Sleep, 100
	SendInput, {Tab}
	Sleep, 100
	SendInput, {Enter}
Return

Dyuzu:
	If (!FileExist(file)) {
		FileDelete, %A_temp%\Windows-Yuzu-EA-*.7z
		DownloadFile(url, file, ysize)
		FileDelete, Windows-Yuzu-EA-*.7z
		FileMove, %A_temp%\%file%, %file%, 1
	}
	Extract(file, "yuzu")
	IniWrite, %latest%, installed.ini, installed, version
	If (ErrorLevel) {
		MsgBox, % 16+262144, , Error 8.`nWe can still continue.
	}
	GUIControl, Main:, U, Refreshing! Please Wait...
	GoSub, ControlGUI
Return

Dprod:
	FileDelete, %A_temp%\prod.keys
	UrlDownloadToFile, %kurl%, %A_temp%\prod.keys
	FileDelete, prod.keys
	FileMove, %A_temp%\prod.keys, prod.keys, 1
	GoSub, MoveProd
Return

MoveProd:
	If (FileExist("prod.keys")) {
		FileCreateDir, %A_AppData%\yuzu\keys
		If (ErrorLevel) {
			MsgBox, % 16+262144, , Directory %A_AppData%\yuzu\keys Could not be Created.`nTry Running as Administrator.
			ExitApp
		}
		FileCopy, prod.keys, %A_AppData%\yuzu\keys ,1
		If (ErrorLevel) {
			MsgBox, % 16+262144, , Error Moving keys to %A_AppData%\yuzu\keys.`nTry Running as Administrator.
			ExitApp
		}
	}
Return

DFirm:
	If (Updates==1) {
		If (FileExist(firfile)) {
			GoSub ExFirm
		}
		If (!FileExist(firfile)){
			FileDelete, %A_temp%\Switch-Firmware*.zip
			DownloadFile(furl, firfile, fsize)
			FileDelete, Switch-Firmware*.zip
			FileMove, %A_temp%\%firfile%, %firfile%, 1
			GoSub, ExFirm
		}
	}
	If (Updates==0) {
		GoSub ExFirm
	}
Return

ExFirm:
	Extract(firfile, "firm")
	IniWrite, %lfirm%, installed.ini, installed, firm
	If (ErrorLevel) {
		MsgBox, % 16+262144, , Error 8.`nWe can still continue.
	}
	GUIControl, Main:, U, Refreshing! Please Wait...
	GoSub, ControlGUI
Return

Bs2:
	If (DownloadTask==1) {
		FileDelete, %A_temp%\%file%
		FileDelete, %A_temp%\%firfile%
	}
	If (DownloadTask==0) {
		GoSub, ControlGUI
		Return
	}
	Reload
Return

ControlGUI:
	GUIButtons("Hide")
	GUIControl, Main:Hide, I0
	GUIControl, Main:Hide, U0
	GUIControl, Main:Show, U
	GUIControl, Main:Hide, Firm
	GUIControl, Main:Hide, Firmtxt

	GoSub CheckUpdates
	GoSub GetInfo

	GUIControl, Main:Hide, U
	GUIControl, Main:, Bs2,  Refresh
	GUIControl, Main:, Firmtxt, %infirm%

	If (installed=="pre") {
		launch:="Launch yuzu Early Access"
		B1txt:="Launch yuzu Early Access"
	}
	Else {
		launch:=StrReplace("Launch yuzu Early Access installed", "installed", installed)
		B1txt:=StrReplace("Launch yuzu Early Access installed", "installed", installed)
	}

	If (Updates==1) {
		If (installed=="" || installed=="ERROR") {
			If (FileExist(file)) {
				B12txt:=StrReplace("Install yuzu Early Access latest", "latest", latest)
			}
			Else {
				B12txt:=StrReplace("Download yuzu Early Access latest", "latest", latest)
			}
		}
		Else {
			If(installed==latest) {
				If (FileExist(file)) {
					B2txt:=StrReplace("ReInstall yuzu Early Access latest", "latest", latest)
				}
				Else {
					B2txt:=StrReplace("ReDownload yuzu Early Access latest", "latest", latest)
				}
			}
			Else {
				If (FileExist(file)) {
					B2txt:=StrReplace("Update to yuzu Early Access latest", "latest", latest)
				}
				Else {
					B2txt:=StrReplace("Download yuzu Early Access latest", "latest", latest)
				}
			}
		}
	}
	If (Updates==0) {
		If (installed=="" || installed=="ERROR") {
			If (FileExist(file)) {
				B12txt:=StrReplace("Install yuzu Early Access latest", "latest", latest)
			}
		}
		Else {
			B2txt:=StrReplace("ReInstall yuzu Early Access installed", "installed", latest)
			B12txt:=launch
		}
	}
	
	GUIControl, Main:, B1, %B1txt%
	GUIControl, Main:, B2, %B2txt%
	GUIControl, Main:, B12, %B12txt%

	If (Updates==1) {
		If (installed=="" || installed=="ERROR") {
			GUIControl, Main:HiDe, I0
			GUIControl, Main:Show, B12
		}
		Else {
			GUIControl, Main:Show, B1
			GUIControl, Main:Show, B2
		}
	}
	If (Updates==0) {
		If (installed=="" || installed=="ERROR") {
			If (FileExist(file)) {
				GUIControl, Main:HiDe, I0
				GUIControl, Main:Show, B12
			}
			Else {
				GUIControl, Main:Show, I0
			}
		}
		Else {
			If (!FileExist(file)) {
				GUIControl, Main:Show, B12
			}
			If (FileExist(file)) {
				GUIControl, Main:Show, B1
				GUIControl, Main:Show, B2
			}
		}
	}

	If (Updates==1) {
		If (installed=="" || installed=="ERROR") {
			If (FileExist(file)) {
				GUI, Font, Q5 s12 Bold
				GUIControl, Main:Font, B12
			}
		}
		Else {
			If (latest==installed) {
				GUI, Font, Q5 s12 Norm
				GUIControl, Main:Font, B2
				GUI, Font, Q5 s12 Bold
				GUIControl, Main:Font, B1
			}
			Else {
				GUI, Font, Q5 s12 Bold
				GUIControl, Main:Font, B2
				GUI, Font, Q5 s12 Norm
				GUIControl, Main:Font, B1
			}
		}
		If (firm=="" || firm=="ERROR") {
			If (!FileExist(firfile)){
				GUIControl, Main:, Bs1, Download Firmware %lfirm%
				GUIControl, Main:Show, Bs1
			}
			If (FileExist(firfile)) {
				GUIControl, Main:, Bs1, Install Firmware %lfirm%
				GUIControl, Main:Show, Bs1
			}
		}
		Else {
			If (lfirm==firm) {
				If (!FileExist(firfile)){
					GUIControl, Main:, Bs1, ReDownload Firmware %lfirm%
					GUIControl, Main:Show, Bs1
				}
				If (FileExist(firfile)) {
					GUIControl, Main:, Bs1, ReInstall Firmware %lfirm%
					GUIControl, Main:Show, Bs1
				}
			}
			Else {
				GUIControl, Main:, Bs1, Update Firmware to %lfirm%
				GUIControl, Main:Show, Bs1
			}
		}
		GUIControl, Main:Show, Firm
		GUIControl, Main:Show, Firmtxt
		GUI, Font, Q5 s7 Norm
		GUIControl, Main:Font, Bs2
	}

	If (Updates==0) {
		If (installed=="" || installed=="ERROR") {
			GUIControl, Main:Show, I0
			GUIControl, Main:, U0, Error retrieving Updates! Check Your Internet and Refresh.
			GUI, Font, Q5 s11 Bold
			GUIControl, Main:Font, U0
		}
		Else {
			If (!FileExist("prod.keys")) {
				GUIControl, Main:, U0, Could not find prod.keys! Check Your Internet and Refresh.
				GUI, Font, Q5 s11 cRed Bold
				GUIControl, Main:Font, U0
				GUI, Font, Q5 s11 cBlack Bold
			}
			Else {
				GUIControl, Main:, U0, prod.keys may be old! Check Your Internet and Refresh.
				GUI, Font, Q5 s11 Bold
				GUIControl, Main:Font, U0
			}
			GUI, Font, Q5 s12 Norm
			GUIControl, Main:Font, B2
			GUI, Font, Q5 s12 Bold
			GUIControl, Main:Font, B1
		}
		If (firm=="" || firm=="ERROR") {
			If (FileExist(firfile)) {
				GUIControl, Main:, Bs1, Install Firmware %lfirm%
				GUIControl, Main:Show, Bs1
			}
		}
		Else {
			If (lfirm==firm) {
				If (FileExist(firfile)) {
					GUIControl, Main:, Bs1, ReInstall Firmware %lfirm%
					GUIControl, Main:Show, Bs1
				}
			}
			Else {
				If (FileExist(firfile)) {
					GUIControl, Main:, Bs1, Install Firmware %lfirm%
					GUIControl, Main:Show, Bs1
				}
			}
		}
		GUIControl, Main:Show, Firm
		GUIControl, Main:Show, Firmtxt
		GUIControl, Main:Show, U0
		GUI, Font, Q5 s11 Bold
		GUIControl, Main:Font, Bs2
	}
	GUIControl, Main:Show, Bs2
Return

Extract(file, id) {
	If (FileExist(file)) {
	GUIControl, Main:Hide, I0
	GUIControl, Main:Hide, U0
	GUIControl, Main:Hide, U
	GUIButtons("Hide")
	GUIControl, Main:Hide, KB
	GUIControl, Main:, Task, Extracting %file%
	GUIControl, Main:, ST, This may take some time.
	GUIControl, Main:, ProgressBar, 0
	GUIControl, Main:, ProgressN,

	ProgressGUI("Show")
	SetTimer, Post, 50

	If (id=="yuzu") {
		RunWait, cmd.exe /c %A_temp%\7za.exe x -bsp1 -y %file% > %A_temp%\log.txt, , Hide UseErrorLevel
		If (ErrorLevel=="ERROR") {
			MsgBox, % 16+262144, , Error 9.`nTry Running as Administrator.
			ExitApp
		}
		If (FileExist("yuzu-windows-msvc-early-access")) {
			FileDelete, yuzu-windows-msvc-early-access\yuzu-windows-msvc-source-*.tar.xz
			If (ErrorLevel) {
				MsgBox, % 16+262144, , Error 10.`nTry Running as Administrator.
				ExitApp
			}
		}
	}
	If (id=="firm") {
		RunWait, cmd.exe /c %A_temp%\7za.exe x -bsp1 -y -o%A_AppData%\yuzu\nand\system\Contents\registered %file% > %A_temp%\log.txt, , Hide UseErrorLevel
		If (ErrorLevel=="ERROR") {
			MsgBox, % 16+262144, , Error 11.`nTry Running as Administrator.
			ExitApp
		}
	}
	SetTimer, Post, Off
	FileDelete, %A_temp%\log.txt
	GUIControl, Main:, ProgressBar, 100
	GUIControl, Main:, ProgressN, 100`%
	Sleep, 500
	ProgressGUI("Hide")
	}
	Return

	Post:
		Loop Read, %A_temp%\log.txt
		progress = %A_LoopReadLine%
		progress := SubStr(progress, 1, 3)
		If (progress=="7-Z" || progress=="Sca" || progress=="0M" || progress=="1 f" || progress=="Ext" || progress=="--" || progress=="Pat" || progress=="Typ" || progress=="Phy" || progress=="Hea" || progress=="Met" || progress=="Sol" || progress=="Blo") {
			Exit
		}
		If (progress=="100" || progress=="Eve" || progress=="Fol" || progress=="Fil" || progress=="Siz" || progress=="Com") {
			SetTimer, Post, Off
			Exit
		}
		GUIControl, Main:, ProgressBar, %progress%
		GUIControl, Main:, ProgressN, %progress%`
	Exit
}

GUIButtons(str) {
	If (str=="Hide") {
		GUIControl, Main:Hide, B1
		GUIControl, Main:Hide, B2
		GUIControl, Main:Hide, B12
		GUIControl, Main:Hide, Bs1
		GUIControl, Main:Hide, Bs2
		GUIControl, Main:Hide, Firm
		GUIControl, Main:Hide, Firmtxt
	}
	If (str=="Show") {
		GUIControl, Main:Show, B1
		GUIControl, Main:Show, B2
		GUIControl, Main:Show, B12
		GUIControl, Main:Show, Bs1
		GUIControl, Main:Show, Bs2
		GUIControl, Main:Show, Firm
		GUIControl, Main:Show, Firmtxt
	}
	Return
}

ProgressGUI(str) {
	If (str=="Hide") {
		GUIControl, Main:Hide, ProgressBar
		GUIControl, Main:Hide, ProgressN
		GUIControl, Main:Hide, KB
		GUIControl, Main:Hide, Task
		GUIControl, Main:Hide, ST
	}
	If (str=="Show") {
		GUIControl, Main:Show, ProgressBar
		GUIControl, Main:Show, ProgressN
		GUIControl, Main:Show, KB
		GUIControl, Main:Show, Task
		GUIControl, Main:Show, ST
	}
	Return
}

DownloadFile(url, save, size) {
	Global DownloadTask:=1

	total:= Round(size / 1024)
	unit := "KiB"
	if (size>=1048576) {
		total := Round(size / 1048576)
		unit := "MiB"
	}

	GUIControl, Main:Hide, I0
	GUIControl, Main:Hide, U0
	GUIControl, Main:Hide, U
	GUIButtons("Hide")
	GUIControl, Main:, Bs2, Cancel
	GUIControl, Main:Show, Bs2
	GUIControl, Main:, Task, Downloading %save%
	GUIControl, Main:, ST, This will take time depending on your Network speed.
	GUIControl, Main:, ProgressBar, 0
	GUIControl, Main:, ProgressN,
	GUIControl, Main:, KB, (0 KiB of %total% %unit% Completed)
	ProgressGUI("Show")

	FileDelete, %save%
	Download(url, save, size)
	GUIControl, Main:, ProgressBar, 100
	GUIControl, Main:, ProgressN, 100`%
	GUIControl, Main:, KB
	Sleep, 500
	ProgressGUI("Hide")
	Global DownloadTask:=0
	Return
}

Download(url, save, total) {
	SetTimer, dlprocess, 500
	FileDelete, %A_temp%\%save%
	UrlDownloadToFile, %url%, %A_temp%\%save%
	If (ErrorLevel) {
		MsgBox, % 16+262144, , Error Downloading %save%.`nCheck your Internet Connection or try running as Administrator.
		ExitApp
	}
	SetTimer, dlprocess, Off
	Sleep, 1000
	Return, ErrorLevel

	dlprocess:
		GoSub, cprocess
		SetTimer, bprocess, -250
		SetCounter(current, total, before)
	Exit

	cprocess:
		FileGetSize, current, %A_temp%\%save%
	Return
	
	bprocess:
		FileGetSize, before, %A_temp%\%save%
	Exit
}

SetCounter(current, size, before) {
	progressBar := Round(current / size * 100)
	progressN := Round((current / size * 100), 2)
	GUIControl, Main:, ProgressBar, %progressBar%
	GUIControl, Main:, ProgressN, %progressN% `%
	GUIControl, Main:, KB, (%total% %unit% of %total% %unit% Completed)

	done := Round(current // 1024)
	dunit := "KiB"
	if (current>=1048576) {
		done := Round(current // 1048576)
		dunit := "MiB"
	}

	total := Round(size // 1024)
	tunit := "KiB"
	if (size>=1048576) {
		total := Round(size // 1048576)
		tunit := "MiB"
	}

	speed := (current - before) * 4
	uspeed := Round(speed // 1024)
	sunit := "KiB/s"
	if (speed>=1048576) {
		uspeed := Round((speed // 1048576), 1)
		sunit := "MiB/s"
	}
	GUIControl, Main:, KB, %uspeed% %sunit% (%done% %dunit% of %total% %tunit% Completed)
	Return
}

ExitFunc() {
	FileDelete, %A_temp%\7za.exe
	FileDelete, %A_temp%\7z.NET.dll
	FileDelete, %A_temp%\jq.exe
	FileDelete, %A_temp%\logo.png
	FileDelete, %A_temp%\load1.png
	FileDelete, %A_temp%\load2.png
	ExitApp
}

TipOff:
	ToolTip,
Return
