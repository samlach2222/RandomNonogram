@ECHO OFF
cd "%~dp0"
"C:\Program Files\doxygen\bin\doxygen" Doxyfile
TIMEOUT 3
