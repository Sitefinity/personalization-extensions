Personalization extensions
==========================

[![Build Status](http://sdk-jenkins-ci.cloudapp.net/buildStatus/icon?job=Telerik.Sitefinity.Samples.PersonalizationExtensions.CI)](http://sdk-jenkins-ci.cloudapp.net/job/Telerik.Sitefinity.Samples.PersonalizationExtensions.CI/)

Sitefinity’s personalization feature gives developers the power to create separate experiences for users grouped by a certain criteria. As a developer, you can deliver different content to be served to different types of users (user segments). You can build set of rules on which the user segments are based. You can then create different versions of the pages of your website. 

This Personalization extensions sample demonstrates a scenario where you need to personalize the content of a page depending on what day of the week the page is opened on. To achieve this, you need to extend the default functionality of Sitefinity's personalization module to fit your requirements.



### Requirements

* Sitefinity license

* .NET Framework 4

* Visual Studio 2012

* Microsoft SQL Server 2008R2 or later versions

* Windows Identity Foundation

   NOTE: Depending on the Microsoft OS version you are using, the method for downloading and installing or enabling the identity framework differs:

  * Windows 7 - download from [Windows Identity Foundation](http://www.microsoft.com/en-us/download/details.aspx?id=17331)

  * Windows 8 - in the Control Panel, turn on the relevant Windows feature Windows Identity Foundation 3.5* Windows Identity Foundation

### Prerequisites

Clear the NuGet cache files. To do this:

1. In Windows Explorer, open the **%localappdata%\NuGet\Cache** folder.
2. Select all files and delete them.


You must have a running Sitefinity project that with a personalization module installed. 


### Installation instructions

1. Open the personalization-extensions repository GitHub and clone the repository in your desktop.

2. Open your solution in Visual Studio and build the solution.

3. Open your project and add a reference to the *DayOfWeekPersonalization.dll*.

4. Build and run your Sitefinity application.


### Additional resources

[Persoanlization tutorial](http://www.sitefinity.com/documentation/documentationarticles/tutorials-personalization)

