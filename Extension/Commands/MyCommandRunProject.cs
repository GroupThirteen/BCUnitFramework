using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Shell;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Task = System.Threading.Tasks.Task;
using BCUnitEngine;
using System.Collections.Generic;

namespace Extension
{
    [Command(PackageIds.MyCommandRunProject)]
    internal sealed class MyCommandRunProject : BaseCommand<MyCommandRunProject>
    {
        protected async override Task ExecuteAsync(OleMenuCmdEventArgs e)
        {


            try {

                
                // This will get the active project ( highlighted with a mouse click)
                Project activeProject = await VS.Solutions.GetActiveProjectAsync();
               


                await activeProject.BuildAsync(BuildAction.Rebuild);

                OutputWindowPane pane = await VS.Windows.CreateOutputWindowPaneAsync("BC Unit Test");




                if (activeProject != null) {


                    string projectName = activeProject.Name;        // name of the project
                    string projectPath = activeProject.FullPath;    // absolute file path on disk


                    //await VS.MessageBox.ShowAsync($"Running on {projectName} Project");
                    //pane = await VS.Windows.CreateOutputWindowPaneAsync($"Results of {projectName} Project:");


                    // TODO: try to find a way to return the full path
                    //string filePath = await activeProject.GetAttributeAsync("TargetPath");


                    AssemblyLoader al = new AssemblyLoader(projectPath, projectName);
                    List<Type> listOfTestAttribute = al.TestClasses;
                    Engine engine = new Engine(listOfTestAttribute);


                    await pane.WriteLineAsync($"Results of {projectName} Project");
                    string line = engine.GetOutputResult();


                    await pane.WriteLineAsync(engine.GetOutputResult());



                    





                } else {

                    await VS.MessageBox.ShowErrorAsync("No Project has been selected");
                    pane = await VS.Windows.CreateOutputWindowPaneAsync("No project is selected");

                }


            } catch (Exception ex) {
                await VS.MessageBox.ShowAsync(ex.Message.ToString());
            } finally {

                

            }
        }
    }


}
