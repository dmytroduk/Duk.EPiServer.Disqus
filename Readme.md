Disqus for EPiServer
=====================================
[Disqus for EPiServer](http://dmytroduk.com/projects/disqus-for-episerver) is [Disqus platform](http://disqus.com/for-websites/) integration for [EPiServer](http://episerver.com) based websites. It consists of several add-ons which enable Disqus comments on pages and provide user interface for configuration.

Please refer documentation on the [project page](http://dmytroduk.com/projects/disqus-for-episerver) for more information about add-ons installation, discussion settings and how Disqus comments can be added on pages.

Basic installation scenario
------------
1.	Signup on [Disqus](https://disqus.com/admin/signup/) and choose a shortname for your site
2.	Install add-ons on your site
3.	Set shortname and enable discussions using Disqus configuration UI 
4.	Start adding comments on pages

Usage scenarios
------------
- Adding comments block on a page (editors)
- Adding comments as dynamic content (editors)
- Displaying comments in specified rendering areas on a page (developers)
- Using comments block as a page property and rendering it in a template (geeky developers)

Development environment
------------
Create 2 subfolders inside msbuild directory: Dependencies and Tools.

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
