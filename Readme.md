Disqus for EPiServer
=====================================
Disqus for EPiServer is [Disqus platform](http://disqus.com/for-websites/) integration for [EPiServer](http://episerver.com) based websites. It consists of several add-ons which enable Disqus comments on pages and provide user interface for configuration.

Add-ons are free.

Available localization: English and Swedish.

Development environment
------------
Create msbuild folder in the solution root directory. Create 2 subfolders inside msbuild directory: Dependencies and Tools.

Copy required EPiServer assemblies to msbuild\Dependencies folder:
- EPiServer.BaseLibrary
- EPiServer.Configuration
- EPiServer.Data
- EPiServer
- EPiServer.Framework
- EPiServer.Packaging
- EPiServer.Shell
- StructureMap

Download [NuGet.exe command line](http://nuget.codeplex.com/releases) and put NuGet.exe tool to msbuild\Tools folder. Add-on package will be created automatically after build.
