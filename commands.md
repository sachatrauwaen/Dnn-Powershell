# DNN PowerShell Commands Documentation

## Table of Contents
- [Site Management](#site-management)
- [Page Management](#page-management)
- [Module Management](#module-management)
- [User Management](#user-management)
- [Role Management](#role-management)
- [Portal Management](#portal-management)
- [Recycle Bin Management](#recycle-bin-management)
- [System Commands](#system-commands)

## Site Management

### Add-Site
Adds a new site to DNN installation.
```powershell
Add-Site -Key "keyname" -Url "http://site.com" [-Username "admin"] [-Password "pass"]
```

### Use-Site
Switches to a different site context.
```powershell
Use-Site -Key "keyname" | -Url "http://site.com" [-Username "admin"] [-Password "pass"]
```

### Get-Site
Retrieves information about the current site.
```powershell
Get-Site
```

### List-Sites
Lists all available sites.
```powershell
List-Sites
```

## Page Management

### New-Page
Creates a new page in the portal.
```powershell
New-Page -PageName "pagename" [-ParentId 123] [-PageTitle "title"] [-Url "url"] [-Description "desc"] [-Keywords "keys"] [-Visible $true]
```

### Get-Page
Retrieves page details.
```powershell
Get-Page [-PageId 123] [-PageName "pagename"] [-ParentId 123]
```

### List-Pages
Lists pages based on specified filters.
```powershell
List-Pages [-ParentId 123] [-Deleted $false] [-PageName "pagename"] [-PageTitle "title"] [-Path "path"] [-Skin "skin"] [-Visible $true] [-Page 1] [-Max 10]
```

### Set-Page
Modifies an existing page.
```powershell
Set-Page -PageId 123 [-ParentId 123] [-PageTitle "title"] [-PageName "pagename"] [-Url "url"] [-Description "desc"] [-Keywords "keys"] [-Visible $true]
```

### Delete-Page
Deletes a page.
```powershell
Delete-Page [-PageId 123] [-PageName "pagename"] [-ParentId 123]
```

### Get-PageUrl
Retrieves the complete URL of a page.
```powershell
Get-PageUrl [-PageId 123] [-PageName "pagename"] [-ParentId 123]
```

## Module Management

### Add-DnnModule
Adds a new module to a page.
```powershell
Add-DnnModule -ModuleName "modname" -PageId 123 [-PaneName "pane"] [-Title "title"]
```

### Get-DnnModule
Retrieves module details.
```powershell
Get-DnnModule -ModuleId 123 -PageId 123
```

### List-DnnModules
Lists modules based on specified filters.
```powershell
List-DnnModules [-ModuleName "modname"] [-ModuleTitle "title"] [-PageId 123] [-Deleted $false] [-Page 1] [-Max 10]
```

### Copy-DnnModule
Copies a module to another page.
```powershell
Copy-DnnModule -ModuleId 123 -PageId 123 -ToPageId 456 [-PaneName "pane"] [-IncludeSettings $true]
```

### Move-DnnModule
Moves a module to another page.
```powershell
Move-DnnModule -ModuleId 123 -PageId 123 -ToPageId 456 [-PaneName "pane"]
```

### Delete-DnnModule
Deletes a module from a page.
```powershell
Delete-DnnModule -ModuleId 123 -PageId 123
```

## User Management

### New-User
Creates a new user.
```powershell
New-User -Email "email" -Username "username" -Firstname "firstname" -Lastname "lastname" [-Password "pass"] [-Approved $true] [-Notify $true]
```

### Get-User
Retrieves user details.
```powershell
Get-User [-UserId 123] [-Email "email"] [-Username "username"]
```

### List-Users
Lists users based on specified filters.
```powershell
List-Users [-Email "email"] [-Username "username"] [-Role "role"] [-Page 1] [-Max 10]
```

### Set-User
Modifies an existing user.
```powershell
Set-User -UserId 123 [-Email "email"] [-Username "username"] [-Displayname "display"] -Firstname "firstname" -Lastname "lastname" [-Password "pass"] [-Approved $true]
```

### Delete-User
Deletes a user.
```powershell
Delete-User -UserId 123 -Notify $true
```

### Reset-Password
Resets a user's password.
```powershell
Reset-Password -UserId 123 -Notify $true
```

### Add-Roles
Adds roles to a user.
```powershell
Add-Roles -UserId 123 -Roles "role1,role2" [-Start "date"] [-End "date"]
```

## Role Management

### New-Role
Creates a new role.
```powershell
New-Role -RoleName "rolename" [-Description "desc"] [-Public $true] [-AutoAssign $true] [-Status "Approved"]
```

### Get-Role
Retrieves role details.
```powershell
Get-Role -RoleId 123
```

### List-Roles
Lists all portal roles.
```powershell
List-Roles
```

### Set-Role
Modifies an existing role.
```powershell
Set-Role -RoleId 123 -RoleName "rolename" [-Description "desc"] [-Public $true] [-AutoAssign $true] [-Status "Approved"]
```

### Delete-Role
Deletes a role.
```powershell
Delete-Role -RoleId 123
```

## Portal Management

### Get-Portal
Retrieves portal details.
```powershell
Get-Portal [-PortalId 123]
```

### List-Portals
Lists all available portals.
```powershell
List-Portals
```

## Recycle Bin Management

### Purge Commands
Permanently delete items from recycle bin:
```powershell
Purge-DnnModule -ModuleId 123 -PageId 123
Purge-Page -PageId 123 [-DeleteChildren $true]
Purge-User -UserId 123
```

### Restore Commands
Restore items from recycle bin:
```powershell
Restore-DnnModule -ModuleId 123 -PageId 123
Restore-Page [-PageId 123] [-PageName "pagename"] [-ParentId 123]
Restore-User -UserId 123
```

## System Commands

### Clear-Cache
Clears the DNN cache.
```powershell
Clear-Cache
```

### Get-Host
Retrieves host/system information.
```powershell
Get-Host
```

### Restart-Application
Restarts the DNN application.
```powershell
Restart-Application
```

### List-Commands
Lists all available commands.
```powershell
List-Commands
```

## Notes
- Parameters in square brackets `[]` are optional
- Parameters without square brackets are mandatory
- Role status options: Approved, Pending, Disabled
- All examples show typical usage patterns with sample values