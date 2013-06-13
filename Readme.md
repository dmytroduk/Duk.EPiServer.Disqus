Disqus for EPiServer
=====================================
Disqus for EPiServer is [Disqus platform](http://disqus.com/for-websites/) integration for [EPiServer](http://episerver.com) based websites. It consists of several add-ons which enable Disqus comments on pages and provide user interface for configuration.

Add-ons are free.

Available localization: English and Swedish.

Basic installation scenario
------------
1.	Signup on [Disqus](https://disqus.com/admin/signup/) and choose a shortname for your site
2.	Install add-ons on your site
3.	In Disqus configuration UI set shortname and enable Disqus 
4.	Start adding comments on pages

Usage scenarios
------------
- Adding comments block on a page (editors, admins)
- Adding comments as dynamic content (editors, admins)
- Displaying comments in specified rendering areas on a page (developers)
- Using comments block as a page property and rendering it in a template (geeky developers)

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
