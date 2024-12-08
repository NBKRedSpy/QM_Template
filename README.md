# Quasimorph Project Template

A Quasimorph dotnet style template to create a new mod.
This creates a Quasimorph Mod project which already includes Harmony, Publicizer, game references, and some basic scaffolding.


# Installation

The template can either be installed via a NuGet package or by installing from the source code.
Only one install method is needed.  See the [Install Via NuGet Package](#install-via-nuget-package) and [Install Via Source Code](#install-via-source-code) sections below.

## Old Versions of the dotnet Utility

**Important**: If the template install fails with the message `Couldn't find an installed template that matches the input, searching online for one that does...`, 
the dotnet utility is most likely an old version.  The latest .NET SDK with the latest tools can be found here: https://dotnet.microsoft.com/en-us/download/dotnet

## Install Via NuGet Package

This registers the dotnet template via the `NBK_RedSpy.QM_Template.Template.<version>.nupkg` in the releases folder.

Go to the releases page and download the latest *.nupkg file.
Open a command prompt in the directory that the nupkg was downloaded to.
type `dotnet new install <downloaded file name>` and press enter.

Example:
`dotnet new install .\NBK_RedSpy.QM_Template.Template.1.1.0.nupkg`

## Install Via Source Code
Download the source code from this repository.

Change to the template directory.
run `dotnet new install ./` in that directory.

The template is now installed.

# Template Usage

After the template is installed (as per the [Installation](#installation) section), a new project can be created with Visual Studio or the `dotnet` CLI tool.
The layout of the project is described in the [Folder Structure](#folder-structure) section.

## Visual Studio 
Visual Studio is the easier of the two methods.

For Visual Studio, create a new project via the main page UI.  There will be a Quasimorph template available in the project type.  
A parameter screen will ask for the UserName, which is the name that will be used for the GitHub link (optional), mod's manifest, and the Harmony unique identifier prefix.

## dotnet Command line
Open a console window to the desired root directory (Ex: c:/src)

type `dotnet new QM_Template -o <some project name> --UserName <some user name>` where `<some project name>` is the name of the project, and `<some user name>` is the username to use as a username identifier.

Example: `dotnet new QM_Template -o QM_AmazingMod --UserName NBKRedSpy`

In the example above, there will now be a QM_AmazingMod folder in the current directory with the new project's files.  

*Important*: If the -o parameter is not provided, the project will be created in the current directory, rather than a sub folder.

# Uninstall
At a command prompt, type 
`dotnet new uninstall QM_Template`

The output will indicate the uninstall command for the template.
Example:
```
The template 'QM_template' is included to the packages:
   NBK_RedSpy.QM_Template.Template::1.0.0 (contains 1 templates)
To uninstall the template package use:
   dotnet new uninstall NBK_RedSpy.QM_Template.Template
```

In the example above, type `dotnet new uninstall NBK_RedSpy.QM_Template.Template` and press enter.

# Folder Structure

```
FooProject
│   .gitignore
│   LICENSE.md
│   modmanifest.json   -- QM mod manifest.  
│   README.md
│   
├───media  -- folder to place the thumbnail.png
│       .gitkeep
│       
└───src -- holds the project's files.
    │   .gitignore
    │   ExamplePatch.cs     	-- An example Harmony Patch.  Commented out
    │   ConfigDirectories.cs	-- The file and configuration paths
    │   FooProject.csproj
    │   FooProject.sln
    │   LICENSE.md
    │   Logger.cs				-- Unity.Debug logger that includes the mod's name
    │   ModConfig.cs        	-- Optional Mod config file
    │   Plugin.cs           	-- Bootstrap 
    │       
    └───Properties
            AssemblyInfo.cs		-- Version Information
```

To use the project:
* Make the changes to the files in the src folder.
* Update the modmanifest.json in the root if necessary.
* Add a thumbnail.png to the media folder.

* Compile the project
* In the Quasimorph game, invoke the `mod_createworkshopitem` command, with the second parameter being this project's output bin folder.
    * Get the returned SteamId from that command.
* In Steam Workshop, subscribe to your mod.
* In the *.csproj file, set the SteamId entry to the mod's Steam id.  Example:`<SteamId>12345678</SteamId>`

    * From now on, all builds will be copied to the Steam Workshop folder.  Generally located at `<Steam Install Directory>\steamapps\workshop\content\2059170\<Mod's Steam Workshop ID>`
* Build the project.
* Remove any Unneeded items.  For example, ModConfig and the reference to the NewtonSoft.Json package.
* The project's build events (found in the Project's details page) handles copying over the files to the Workshop folder.
    * If not using ModConfig or Newtonsoft, remove the System.* and Newtonsoft* lines.
* Edit the README.md in the root:
    * Remove my Kofi link.
    * Edit as desired.

Update the mod on Steam:
* Open the game.
* Use the console command `mod_updateworkshopitem <Steam Workshop ID> <Workshop Folder> true`, pointing to the mod's Steam Workshop folder.  Example: `mod_updateworkshopitem 1234567 "C:\Program Files\steamapps\workshop\content\2059170\12345678" true`
* Change the Mod's Steam Workshop page to public.




