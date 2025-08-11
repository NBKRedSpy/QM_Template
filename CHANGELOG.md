# Changelog

All notable changes to this project will be documented in this file.

## 1.1.5

### Removed
- Removed copying build artifacts that are no longer needed.  Specifically the System*.dlls, Newtonsoft.json.dll and 0Harmony.dll

### Changed
- The build no longer includes the unneeded references to the output folder.  For example, the BepInEx build artifacts.
- Moved the build events from the "Events" project property to tasks in the .csproj file.  
- Release build configuration no longer includes the pdb.
- Clean up of .csproj entries.
- Build now cleans the mod's Workshop folder to prevent removed items from being uploaded.
