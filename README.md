# VSS2SVN
Copy from https://archive.codeplex.com/?p=VSS2SVN

## Project Description

VSS2SVN is a simple utility project that aims to help migrate the contents of a source safe database to subversion. To do that, VSS2SVN uses two key assemblies:

* The Microsoft sourcesafe interop assembly, which allows for fast access to VSS, version 5.2.0.0.
* [SharpSVN](http://sharpsvn.open.collab.net/). This is statically compiled with support for SVN 1.5.5.

VSS2SVN migrates the complete contents of a VSS project, including the various versions of the project files. It works like this:

* The VSS project contents are being scanned to determine the maximum number of versions in all included files.
* The initial version of the VSS contents is downloaded to a temporary location and is imported to SVN.
* Subsequent VSS versions are repeatedly dowloaded to the temporary location and SVN is updated with these contents via a commit.

VSS2SVN has been tested in the following environment:

* VSS 2005, client version 8.0.50727.1551. If you don't have [this](http://www.microsoft.com/downloads/details.aspx?familyid=8A1A68D8-DB11-417C-91AD-02AAB484776B&displaylang=en) update, you should probably get it.
* SVN server used is VisualSVN, version 1.6.3.
* SVN windows client used is TortoiseSVN vesion 1.5.6, build 14908.
* SharpSVN version is 1.5005.984.35067.
* Sourcesafe interop assembly version is 5.2.0.0.

Currently, VSS2SVN does not implement any SVN dialogue functionality. That means that you should use TortoiseSVN to log on to the SVN repository and then use VSS2SVN.
