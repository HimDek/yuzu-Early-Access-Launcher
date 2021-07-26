#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
; #Warn  ; Enable warnings to assist with detecting common errors.
#SingleInstance Force
#NoTrayIcon
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

version:="v0.1.0-beta"

FileInstall, 7za.exe, %A_temp%\7za.exe, 1
FileInstall, 7z.NET.dll, %A_temp%\7z.NET.dll, 1
FileInstall, jq.exe, %A_temp%\jq.exe, 1
FileInstall, logo.png, %A_temp%\logo.png, 1

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
	ExitApp
}

FileCreateShortcut, %A_ScriptFullPath%, %A_Desktop%\yuzu Early Access Launcher.lnk, , , Launch yuzu Early Access
FileCreateShortcut, %A_ScriptFullPath%, %A_Programs%\yuzu Early Access Launcher.lnk, , , Launch yuzu Early Access
If (ErrorLevel) {
	MsgBox, % 16+262144, , Could not Create Desktop Shortcut.`nTry Running as Administrator.
}

Global DownloadTask:=0
GoSub MainGUI
GoSub ControlGUI
OnExit("ExitFunc")

Return

UpdateLauncher:
	GUIControl, Main:, U, Updating Launcher!
	FileDelete, %A_temp%\launcher.ini
	
	UrlDownloadToFile, https://api.github.com/repos/HiDe-Techno-Tips/pineapple-src/releases/latest, %A_temp%\yuzulauncher.json
	
	RunWait, cmd.exe /c echo [latest] > %A_temp%\launcher.ini,, Hide UseErrorLevel
	RunWait, cmd.exe /c for /f `%l in ('%A_temp%\jq.exe .tag_name %A_temp%\yuzulauncher.json') do echo version=`%l >> %A_temp%\launcher.ini,, Hide UseErrorLevel
	RunWait, cmd.exe /c for /f `%l in ('%A_temp%\jq.exe .assets[0].browser_download_url %A_temp%\yuzulauncher.json') do echo url=`%l >> %A_temp%\launcher.ini,, Hide UseErrorLevel
	RunWait, cmd.exe /c for /f `%l in ('%A_temp%\jq.exe .assets[0].size %A_temp%\yuzulauncher.json') do echo lsize=`%l >> %A_temp%\launcher.ini,, Hide UseErrorLevel
	
	If (ErrorLevel=="ERROR") {
		MsgBox, % 16+262144, , Error 3.`nTry Running as Administrator.
		ExitApp
	}
	
	If (FileExist(A_temp . "\launcher.ini")) {
		IniRead, launcherlatest, %A_temp%\launcher.ini, latest, version
		IniRead, lurl, %A_temp%\launcher.ini, latest, url
		IniRead, lsize, %A_temp%\launcher.ini, latest, lsize
	}
	
	FileDelete, %A_temp%\launcher.ini
	
	If (version!=launcherlatest) {
		tempPath:=A_Temp . "\" . A_ScriptName
		DownloadFile(lurl, tempPath, lsize)
		Run, %tempPath%, %A_Temp%, UseErrorLevel
		If (ErrorLevel=="ERROR") {
			MsgBox, % 16+262144, , Error 2.`nTry Running as Administrator.
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
	UrlDownloadToFile, https://hide-techno-tips.github.io/Nintendo-Switch-Files/api.json, %A_temp%\switch.json

	RunWait, cmd.exe /c echo [latest] > %A_temp%\latest.ini,, Hide UseErrorLevel
	RunWait, cmd.exe /c for /f `%l in ('%A_temp%\jq.exe .tag_name %A_temp%\yuzuea.json') do echo version=`%l >> %A_temp%\latest.ini,, Hide UseErrorLevel
	RunWait, cmd.exe /c for /f `%l in ('%A_temp%\jq.exe .assets[0].browser_download_url %A_temp%\yuzuea.json') do echo url=`%l >> %A_temp%\latest.ini,, Hide UseErrorLevel
	RunWait, cmd.exe /c for /f `%l in ('%A_temp%\jq.exe .assets[0].size %A_temp%\yuzuea.json') do echo ysize=`%l >> %A_temp%\latest.ini,, Hide UseErrorLevel

	RunWait, cmd.exe /c for /f `%l in ('%A_temp%\jq.exe .firmware.ver %A_temp%\switch.json') do echo firm=`%l >> %A_temp%\latest.ini,, Hide UseErrorLevel
	RunWait, cmd.exe /c for /f `%l in ('%A_temp%\jq.exe .firmware.url %A_temp%\switch.json') do echo furl=`%l >> %A_temp%\latest.ini,, Hide UseErrorLevel
	RunWait, cmd.exe /c for /f `%l in ('%A_temp%\jq.exe .firmware.size %A_temp%\switch.json') do echo fsize=`%l >> %A_temp%\latest.ini,, Hide UseErrorLevel

	If (ErrorLevel=="ERROR") {
		MsgBox, % 16+262144, , Error 3.`nTry Running as Administrator.
		ExitApp
	}

	FileDelete, %A_temp%\yuzuea.json
	FileDelete, %A_temp%\switch.json
Return

GetInfo:
	GUIControl, Main:, U, Checking Files!
	FileDelete, %A_temp%\downloaded.ini
	RunWait, cmd.exe /c echo [downloaded] > %A_temp%\downloaded.ini,, Hide UseErrorLevel
	RunWait, cmd.exe /c for /f "tokens=4 delims=-" `%b in ('where "Windows-Yuzu-EA-*.7z"') do echo version=`%~nb >> %A_temp%\downloaded.ini,, Hide UseErrorLevel
	RunWait, cmd.exe /c for /f "tokens=3 delims=-" `%b in ('where "Switch-Firmware-*.zip"') do echo firm=`%~nb >> %A_temp%\downloaded.ini,, Hide UseErrorLevel

	If (ErrorLevel=="ERROR") {
		MsgBox, % 16+262144, , Error 4.`nTry Running as Administrator.
		ExitApp
	}

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

MainGUI:
	GUI, Main:New, -MinimizeBox, yuzu Early Access Launcher

	GUI, Add, Picture, xm+105 ym+5 h216 w-1, %A_temp%\logo.png
	GUI, Font, Q5 s15 Bold
	GUI, Add, Text, xm ym+240 w620 h60 0x1 vI0, yuzu Early Access is not Installed!
	GUI, Add, Text, xm ym+265 w620 h30 0x1 vU, Checking for Latest Version! Please Wait.
	GUI, Font, Q5 s11 Norm

	GUI, Add, Button, xm ym+225 w305 h60 vB1 gB1,
	GUI, Add, Button, xm+315 yp w305 h60 vB2 gB2,
	GUI, Font, Q5 s14 Norm
	GUI, Add, Button, xm yp w620 h60 vB12 gB12,

	GUI, Font, Q5 s10 Bold
	GUI, Add, GroupBox, xm yp+70 w620 h60 vFirm, Firmware:
	GUI, Font, Q5 s10 Norm
	GUI, Add, Text, xm+10 yp+25 w420 h30 vFirmtxt,
	GUI, Font, Q5 s10 Norm
	GUI, Add, Button, xm+420 yp-7 w190 h30 vBs1 gDFirm,

	GUI, Font, Q5 s13 Norm
	GUI, Add, GroupBox, xm ym+225 w620 h130 vTask
	GUI, Add, Progress, Section xm+10 yp+30 w600 h15 vProgressBar -Smooth
	GUI, Add, Text, xm+10 yp+25 w200 h30 vProgressN
	GUI, Add, Text, xm+210 yp w400 h30 0x2 vKB
	GUI, Add, Text, xm+10 yp+40 w410 h30 vST
	
	ProgressGUI("Hide")

	GUI, Font, Q5 s11 Norm
	GUI, Add, Text, xm+5 ym+372 w510 h30 vU0, Error Checking for Updates! Check Your Internet and Refresh.
	GUI, Font, Q5 s7 Norm
	GUI, Add, Button, xm+525 yp-7 w95 h30 vBs2 gBs2,

	GUI, Font, Q5 s10 Bold
	GUI, Add, GroupBox, xm yp+40 w620 h75, Help and Support:
	GUI, Font, Q5 s7 Norm
	GUI, Add, Link, xm+10 yp+25, <a href="https://youtu.be/fPdPDgNGKI4">View Video Tutorial</a> by the Maker of This Tool, HiDe Techno Tips.
	GUI, Add, Link, xp yp+20, <a href="https://www.youtube.com/channel/UCy3fBVKd0RMY05CgiiuGqSA?sub_confirmation=1">Please Support me by Subscribing to my Youtube Channel.</a>

	GUIButtons("Hide")
	GUIControl, Main:Hide, I0
	GUIControl, Main:Hide, U0
	GUIControl, Main:Show, U

	GUI, Main: Show
Return

MainGUIClose:
	ExitApp
Return

B1:
	Run, yuzu-windows-msvc-early-access\yuzu.exe, , UseErrorLevel
	If (ErrorLevel=="ERROR") {
		MsgBox, % 16+262144, , Error 5.`nTry Running as Administrator.
	}
	ExitApp
Return

B2:
	GoSub Dyuzu
Return

B12:
	If (Updates==0) {
		GoSub, B1
	}
	If (Updates==1) {
		GoSub, Dyuzu
	}
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
		MsgBox, % 16+262144, , Error 6.`nWe can still continue.
	}
	GUIControl, Main:, U, Refreshing! Please Wait...
	GoSub, ControlGUI
Return

Dprod:
	FileDelete, %A_temp%\prod.keys
	UrlDownloadToFile, https://hide-techno-tips.github.io/Nintendo-Switch-Files/prod.keys, %A_temp%\prod.keys
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
		MsgBox, % 16+262144, , Error 7.`nWe can still continue.
	}
	GUIControl, Main:, U, Refreshing! Please Wait...
	GoSub, ControlGUI
Return

Bs2:
	If (DownloadTask==1) {
		FileDelete, %file%
		FileDelete, %A_temp%\%file%
		FileDelete, %firfile%
		FileDelete, %A_temp%\%firfile%
	}
	If (DownloadTask==0) {
		GUIControl, , U, Checking for Latest Version! Please Wait...
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
			}
			If (FileExist(firfile)) {
				GUIControl, Main:, Bs1, Install Firmware %lfirm%
			}
		}
		Else {
			If (lfirm==firm) {
				If (!FileExist(firfile)){
					GUIControl, Main:, Bs1, ReDownload Firmware %lfirm%
				}
				If (FileExist(firfile)) {
					GUIControl, Main:, Bs1, ReInstall Firmware %lfirm%
				}
			}
			Else {
				GUIControl, Main:, Bs1, Update Firmware to %lfirm%
			}
		}
		GUIControl, Main:Show, Firm
		GUIControl, Main:Show, Firmtxt
		GUIControl, Main:Show, Bs1
		GUI, Font, Q5 s7 Norm
		GUIControl, Main:Font, Bs2
	}

	If (Updates==0) {
		If (installed=="" || installed=="ERROR") {
			GUIControl, Main:Show, I0
			GUIControl, Main:, U0, Error Finding the Latest Version! Check Your Internet and Refresh.
		}
		Else {
			If (!FileExist("prod.keys")) {
				GUIControl, Main:, U0, prod.keys not found! Check Your Internet and Refresh.
				GUI, Font, Q5 s11 cRed Bold
				GUIControl, Main:Font, U0
			}
			Else {
				GUIControl, Main:, U0, Could not verify if prod.keys are latest! Check Your Internet and Refresh.
				GUI, Font, Q5 s11 Bold
				GUIControl, Main:Font, U0
				GUIControl, Main:Show, U0
			}
			GUI, Font, Q5 s12 Norm
			GUIControl, Main:Font, B2
			GUI, Font, Q5 s12 Bold
			GUIControl, Main:Font, B1
		}
		If (firm=="" || firm=="ERROR") {
			If (FileExist(firfile)) {
				GUIControl, Main:, Bs1, Install Firmware %lfirm%
			}
		}
		Else {
			If (lfirm==firm) {
				If (FileExist(firfile)) {
					GUIControl, Main:, Bs1, ReInstall Firmware %lfirm%
				}
			}
			Else {
				If (FileExist(firfile)) {
					GUIControl, Main:, Bs1, Install Firmware %lfirm%
				}
			}
		}
		GUIControl, Main:Show, Firm
		GUIControl, Main:Show, Firmtxt
		GUIControl, Main:Show, Bs1
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
	GUIControl, Main:, ProgressN, 0`%
	
	Sleep, 1000
	ProgressGUI("Show")
	SetTimer, Post, 50

	If (id=="yuzu") {
		If (FileExist("yuzu-windows-msvc-early-access")) {
			FileRemoveDir, yuzu-windows-msvc-early-access, 1
			If (ErrorLevel) {
				MsgBox, % 16+262144, , Error 8.`nTry Running as Administrator.
				ExitApp
			}
		}
		RunWait, cmd.exe /c %A_temp%\7za.exe x -bsp1 -y %file% > %A_temp%\log.txt, , Hide UseErrorLevel
		If (ErrorLevel=="ERROR") {
			MsgBox, % 16+262144, , Error 9.`nTry Running as Administrator.
			ExitApp
		}
	}
	If (id=="firm") {
		RunWait, cmd.exe /c %A_temp%\7za.exe x -bsp1 -y -o%A_AppData%\yuzu\nand\system\Contents\registered %file% > %A_temp%\log.txt, , Hide UseErrorLevel
		If (ErrorLevel=="ERROR") {
			MsgBox, % 16+262144, , Error 10.`nTry Running as Administrator.
			ExitApp
		}
	}
	SetTimer, Post, Off
	FileDelete, %A_temp%\log.txt
	GUIControl, Main:, ProgressBar, 100
	GUIControl, Main:, ProgressN, 100`%
	Sleep, 1000
	ProgressGUI("Hide")
	}
	Return

	Post:
		Loop Read, %A_temp%\log.txt
		progress = %A_LoopReadLine%
		progress := SubStr(progress, 1, 3)
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
	GUIControl, Main:, ProgressN, 0`%
	GUIControl, Main:, KB, (0 KiB of %total% %unit% Completed)
	ProgressGUI("Show")

	FileDelete, %save%
	Download(url, save, size)
	GUIControl, Main:, ProgressBar, 100
	GUIControl, Main:, ProgressN, 100`%
	GUIControl, Main:, KB
	Sleep, 1000
	ProgressGUI("Hide")
	Global DownloadTask:=0
	Return
}

Download(url, save, total) {
	SetTimer, dlprocess, 500
	FileDelete, %A_temp%\%save%
	UrlDownloadToFile, %url%, %A_temp%\%save%
	If (ErrorLevel) {
		MsgBox, % 16+262144, , Error Downloading %save%.`nCheck your Internet Connection ot try running as Administrator.
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
	ExitApp
}

TipOff:
	ToolTip,
Return
