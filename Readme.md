# Show Selected Blueprint

Shows the name of the selected blueprint in the sidebar.

Requires BepInEx Version
`5.4.2100` [Thunderstore Download](https://thunderstore.io/package/download/BepInEx/BepInExPack/5.4.2100/)

## Development

1. Install BepInEx
2. Create a file called `Environment.props` in the root folder and add the following content:
   ```xml
   <?xml version="1.0" encoding="utf-8"?>
   <Project ToolsVersion="Current" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
     <PropertyGroup>
       <SHAPES2_INSTALL>C:/Program Files/Steam/steamapps/common/shapez 2</SHAPES2_INSTALL>
       <MOD_DEPLOYPATH>$(SHAPES2_INSTALL)/BepInEx/plugins</MOD_DEPLOYPATH>
     </PropertyGroup>
   </Project>
   ```
3. Compile the project. This copies the resulting dll into `MOD_DEPLOYPATH`, if set
