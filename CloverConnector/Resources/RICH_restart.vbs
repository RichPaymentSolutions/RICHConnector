Set WshShell = CreateObject("WScript.Shell")
Set FSO = CreateObject("Scripting.FileSystemObject")

' Get the full path of the VBScript file
vbsPath = WScript.ScriptFullName

' Get the path of the batch file relative to the VBScript file
batchPath = FSO.BuildPath(FSO.GetParentFolderName(vbsPath), "RICH_restart.bat")

' Run the batch file with a hidden window
WshShell.Run """" & batchPath & """", 0, False

Set WshShell = Nothing
Set FSO = Nothing
