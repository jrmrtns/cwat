# cellent WPF Application Template

## Build solution
After building the solution there is a file called "cellent Template.vsix" in the output of the VSIX project.
Double click this file and the template will be installed in Visual Studio. 

## Create solution
In Visual Studio create a new project and select the "cellent Template" as template.

## First steps after running the template 
- Adjust connection string in "projectname".Service/app.config
- Build solution
- Open Package Manager Console and select "projectname".Repository
- Run update-database -StartupProjectName "projectname".Service from Package Manager Console in Visual Studio
- Go to solution properties and set multiple startup projects. Set Action for "projectname"Client and "projectname"Service to "Start"
- To run "projectname".Service as console go to the properties for "projectname".Service and enter "/c" under Debug-->Command line Arguments
- Press "F5"
