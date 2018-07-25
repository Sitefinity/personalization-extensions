Personalization extensions
==========================

>**Latest supported version**: Sitefinity CMS 11.0.6700.0

>**Documentation articles**: [Tutorial: Create custom personalization criteria](http://docs.sitefinity.com/tutorial-create-custom-personalization-criteria)

### Overview

The Sitefinity CMS personalization feature gives developers the power to create separate experiences for users grouped by a certain criteria. As a developer, you can deliver different content to be served to different types of users (user segments). You can build set of rules on which the user segments are based. You can then create different versions of the pages of your website. 

This Personalization extensions sample demonstrates a scenario where you need to personalize the content of a page depending on what day of the week users open the page. To achieve this, you need to extend the default functionality of the Sitefinity CMS personalization module to fit your requirements.

#### Prerequisites
- You must have a Sitefinity CMS license.
- Your setup must comply with the system requirements.  
 For more information, see the [System requirements](https://docs.sitefinity.com/system-requirements) for the  respective Sitefinity CMS version.

### Installation

1. Clone the sample repository.
2. Clear the NuGet cache files.  
 a. Open the solution file in Visual Studio.  
 b. In the toolbar, navigate to _Tools >> NuGet Package Manager >> Package Manager Settings_.  
 c. In the left pane, navigate to _NuGet Package Manager >> General_.  
 d. Click _Clear All NuGet Cache(s)_.  
3. Restore the NuGet packages in the solution.  
   
   >**NOTE**: The solution in this repository relies on NuGet packages with automatic package restore while the build procedure takes place.   
   >For a full list of the referenced packages and their versions see the [packages.config](https://github.com/Sitefinity-SDK/personalization-extensions/blob/master/DayOfWeekPersonalization/packages.config) file.    
   >For a history and additional information related to package versions on different releases of this repository, see the [Releases page](https://github.com/Sitefinity-SDK/personalization-extensions/releases).
   >  
   a. Navigate to _Tools >> NuGet Package Manager >> Package Manager Console_.  
   b. In _Source_, select Sitefinity CMS NuGet Repository.  
   c. Click _Restore_ button.
4. Build the solution.
5. Open your project and add a reference to the `DayOfWeekPersonalization.dll`.
6. Build your project and run your Sitefinity CMS application.

### Additional resources

Progress Sitefinity CMS documentation: [Overview: Personalization](http://docs.sitefinity.com/overview-personalization)
