# Quasimorph Visual Studio Template

This is a template to quickly create Quasimorph mods.

It comes configured with a config class, Newtonsoft.Json, and Harmony, .gitignore files.

The project build handles copying mod manifest and thumbnail to the output directory.
Handles finding and deploying to the workshop directory once the SteamId is set.

# Installation

Copy the Quasimorph Mod.zip file from the releases folder to the %UserProfile%\Documents\Visual Studio 2022\Templates\ProjectTemplates.

It is not necessary to unzip the file.  Visual Studio can use it directly.

# Usage
When creating a new project in Visual Studio, one of the templates will be called "Quasimorph Mod".  Use that template and create as normal.

## TODO:  Finish the docs here for usage
Items to flesh out.

* Use the template to create a project.
* Exit Visual Studio.
* Move project's code to the `Move To Root/src` folder.
* Move everything in `Move To Root` to the root folder, overwriting all files.
* Delete the `Move To Root` folder.


* Create a thumbnail.png and put it in the `media` folder.
* Edit the modmanifest.json in the root if needed.
* README.md: Fill out and remove unneeded sections.  For example, Configuration or key info.


* Open the project and build.
* Upload the project's outputs to Steam via Quasimorph's mod_workshopcreate command.  
    * The first time will need to be from the /bin/* folder.  
* Copy the SteamId number that is returned.
* At the top of the *.csproj file, put that number in the `SteamId` element.
    *Ex: `<SteamId>1234567</SteamId>`
* Subscribe to the mod in steam.
* With the SteamId set, building the project will also copy the required files to the mod's workshop folder.
* When done, update the mod with Quasimorph's update command.

* For the Steam description, use https://github.com/NBKRedSpy/MarkdownToSteam utility to convert the Markdown file to Steam's format.  
    * Run the utility, `MarkdownToSteam.exe" -i ReadMe.md -o SteamReadMe.txt -r`
    * Take the contents of the SteamReadme.txt and paste into the workshop page.

**TODO** Remove my Github link from the Readme.md file.

# Modify This Template
Close this repo and open the solution.

Make the changes as needed.  

Save the changes and then convert the project to a template by:

Menu -> Project -> Export Template...
On the popup, Project Template, Next.
Template Name:  This is what the name of the template will be in Visual Studio.  To match, set it to Quasimorph Mod.
