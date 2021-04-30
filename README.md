# STROOP
*SuperMario64 Technical Runtime Observer and Object Processor*  
[Original Program by Pannenkoek2012](https://github.com/SM64-TAS-ABC/STROOP)

  STROOP is a diagnostic tool for Super Mario 64 that displays and allows for simple editing of various game values and information. It can connect to a running emulator and update values in real time. Some core features include views of loaded/unloaded objects, Mario structure variables, camera + HUD values, an overhead map display, and many more.
 
  
       
## Downloading STROOP

This refactor is designed to make STROOP faster, less memory heavy and more extensible. Since this process is still going on, some features may not be available or unstable. You can get these recent builds [here](https://github.com/chaosBrick/STROOP/releases).
  
You can get more stable releases from the original [Releases Page](https://github.com/SM64-TAS-ABC/STROOP/releases).  
  
## Requirements

  As of the current build, STROOP has the following system requirements:
  * Windows 10 / Windows 8.1 / Windows 8 / Windows 7 64-bit or 32-bit
  * OpenGL 3.2 or greater (3.2 requirement for map tab, 1.0 requirement for model tab only) 
  * .NET Framework 4.6.1 (See [.NET Framework System Requirements](https://msdn.microsoft.com/en-us/library/8z6watww(v=vs.110).aspx) for more information)
  * [Mupen 0.5 rerecording](http://adelikat.tasvideos.org/emulatordownloads/mupen64-rr/Mupen64%20v8%20installer.zip) (0.5.1 will not work) (Bizhawk + Nemu partially supported on Development branch)
  * 64 Marios (Must be super)
  * Marios must be American, Japanese or PAL
  
## Status 
 
  STROOP is currently under a development phase; however, pre-releases have been made.
 
## Building

Requirements:
  * Visual Studio *(2017 recommended)*
  * OpenTK 1.1 *(Version 1.1.1589.5942 used, others may work)*
  
OpenTK is a prerequisite for building STROOP. This is easiest installed by using the NuGet package manager. STROOP can be easily built from the source code by opening up the solution file in Visual Studio and performing a build. 
