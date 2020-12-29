@echo off

%comspec% /k "d:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\VC\Auxiliary\Build\vcvars32.bat"

pause

cl Win32Loader.c


