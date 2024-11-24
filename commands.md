# Site management commands
Add-Site -Key "keyname" -Url "http://site.com" [-Username "admin"] [-Password "pass"]
Use-Site -Key "keyname" | -Url "http://site.com" [-Username "admin"] [-Password "pass"] 
Get-Site
List-Sites

# Page management commands
New-Page -PageName "pagename" [-ParentId 123] [-PageTitle "title"] [-Url "url"] [-Description "desc"] [-Keywords "keys"] [-Visible $true]  # Creates a new page
Get-Page [-PageId 123] [-PageName "pagename"] [-ParentId 123]  # Gets page details
List-Pages [-ParentId 123] [-Deleted $false] [-PageName "pagename"] [-PageTitle "title"] [-Path "path"] [-Skin "skin"] [-Visible $true] [-Page 1] [-Max 10]  # Lists pages with filters
Set-Page -PageId 123 [-ParentId 123] [-PageTitle "title"] [-PageName "pagename"] [-Url "url"] [-Description "desc"] [-Keywords "keys"] [-Visible $true]  # Modifies an existing page
Delete-Page [-PageId 123] [-PageName "pagename"] [-ParentId 123]  # Deletes a page
Get-PageUrl [-PageId 123] [-PageName "pagename"] [-ParentId 123]  # Gets the complete URL of a page

# Module management commands
Add-DnnModule -ModuleName "modname" -PageId 123 [-PaneName "pane"] [-Title "title"]  # Adds a new module to a page
Get-DnnModule -ModuleId 123 -PageId 123  # Gets module details
List-DnnModules [-ModuleName "modname"] [-ModuleTitle "title"] [-PageId 123] [-Deleted $false] [-Page 1] [-Max 10]  # Lists modules with filters
Copy-DnnModule -ModuleId 123 -PageId 123 -ToPageId 456 [-PaneName "pane"] [-IncludeSettings $true]  # Copies a module to another page
Move-DnnModule -ModuleId 123 -PageId 123 -ToPageId 456 [-PaneName "pane"]  # Moves a module to another page
Delete-DnnModule -ModuleId 123 -PageId 123  # Deletes a module from a page

# User management commands
New-User -Email "email" -Username "username" -Firstname "firstname" -Lastname "lastname" [-Password "pass"] [-Approved $true] [-Notify $true]  # Creates a new user
Get-User [-UserId 123] [-Email "email"] [-Username "username"]  # Gets user details
List-Users [-Email "email"] [-Username "username"] [-Role "role"] [-Page 1] [-Max 10]  # Lists users with filters
Set-User -UserId 123 [-Email "email"] [-Username "username"] [-Displayname "display"] -Firstname "firstname" -Lastname "lastname" [-Password "pass"] [-Approved $true]  # Modifies a user
Delete-User -UserId 123 -Notify $true  # Deletes a user
Reset-Password -UserId 123 -Notify $true  # Resets a user's password
Add-Roles -UserId 123 -Roles "role1,role2" [-Start "date"] [-End "date"]  # Adds roles to a user

# Role management commands
New-Role -RoleName "rolename" [-Description "desc"] [-Public $true] [-AutoAssign $true] [-Status "Approved"]  # Creates a new role
Get-Role -RoleId 123  # Gets role details
List-Roles  # Lists all portal roles
Set-Role -RoleId 123 -RoleName "rolename" [-Description "desc"] [-Public $true] [-AutoAssign $true] [-Status "Approved"]  # Modifies a role
Delete-Role -RoleId 123  # Deletes a role

# Portal management commands
Get-Portal [-PortalId 123]  # Gets details of a specific portal or current portal
List-Portals                # Lists all available portals in the DNN instance

# Recycle bin commands
Purge-DnnModule -ModuleId 123 -PageId 123  # Permanently deletes a module from recycle bin
Purge-Page -PageId 123 [-DeleteChildren $true]  # Permanently deletes a page from recycle bin
Purge-User -UserId 123  # Permanently deletes a user from recycle bin
Restore-DnnModule -ModuleId 123 -PageId 123  # Restores a module from recycle bin
Restore-Page [-PageId 123] [-PageName "pagename"] [-ParentId 123]  # Restores a page from recycle bin
Restore-User -UserId 123  # Restores a user from recycle bin

# System commands
Clear-Cache
Get-Host
Restart-Application
List-Commands

# Notes:
# Parameters in [] are optional
# Parameters without [] are mandatory
# Values are given as examples
# Role status can be: Approved, Pending, Disabled