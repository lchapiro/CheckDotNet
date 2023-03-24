# WINDOWS APPLICATION: `CheckDotNet` Project Overview

## Note

Users can install and run multiple versions of the .NET Framework on their computers. When you develop or deploy your app, you might need to know which .NET Framework versions are installed on the target's computer. This article manages the rare (but possible) situation if there aren't any .NET Frameworks installed. For this purpose, it starts with a pure C++ application (Starter) that doesn't need any .NET components in order to check whether at least anyone .NET Framework is available. If yes, the main application starts and shows a GUI with located components. Now the user has a possibility, to select a directory to check all *.EXEs, *.DLLs and *.OCXs in it for fulfillment of .NET requirements.

## Background

To get an accurate list of the .NET Framework versions installed on a computer, we need to view the registry. This feature is accompanied by Microsoft's document here. To find the .NET Framework 1 - 4, we need to use the following subkey:
HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\NET Framework Setup\NDP

### Using the Tool

After starting the CheckDotNet.exe, you will see the message box if absolutely no .NET Framework is available on your system.
However, this should be a very rare situation.

Most commonly, it will be found that one or more components and the CheckDotNet.exe will start the GUI application written in C#.

## History

    Current version 1.0.0
    Added the command-line Version Win32Analyser.zip Image 5
    Updated to Version 2.0.0 by adding recognition of .NET 4.6.2, 4.7, 4.7.1, 4.7.2
    Updated to Version 3.2.0
    Updated to Version 10.1.0