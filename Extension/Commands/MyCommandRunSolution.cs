using BCUnitEngine;
using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Task = System.Threading.Tasks.Task;

namespace Extension
{
    [Command(PackageIds.MyCommandRunSolution)]
    internal sealed class MyCommandRunSolution : BaseCommand<MyCommandRunSolution>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {

            try {
                //await VS.MessageBox.ShowAsync("Solution");

                // This will get projects in the solution
                var solution = await VS.Solutions.GetAllProjectsAsync();


                // Building the solution
                bool buildStarted = await VS.Build.BuildSolutionAsync(BuildAction.Build);

                OutputWindowPane pane = await VS.Windows.CreateOutputWindowPaneAsync("BC Unit Test");

                if (buildStarted) {

                    //TOTO
                    // start the xml here and fill the xml here
                    // by the time i finish ethe loop will fill the xml

                    foreach (var project in solution) {
                        //await VS.MessageBox.ShowWarningAsync("projct" + project);
                        string projectName = project.Name;        // name of the project
                        string projectPath = project.FullPath;    // absolute file path on disk

                        //await pane.WriteLineAsync($"Results of {projectName} Project");


                        //await VS.MessageBox.ShowWarningAsync(projectName + "\n" + projectPath);
                        AssemblyLoader al = new(projectPath, projectName);

                        Engine engine = new(al.TestClasses);

                        //await pane.WriteLineAsync("1");

                        await pane.WriteLineAsync(projectName);
                        //await pane.WriteLineAsync("2");
                        await pane.WriteLineAsync(engine.GetOutputResult());

                        //await VS.MessageBox.ShowWarningAsync(projectPath);



                    }//foreach

                    //TODO
                    //checked for plug int the implemt xml
                    // spacify a location for someone to write their dll
                    // this directory will be created using the installer

                    //this is a second part for the xml
                    //BCUNITframeworkSDK folder
                    // wil have folder bin inside BCUNITFramework
                    // plugins folder inside the user will drop the plug 

                }


                // Close the 



            } catch (System.Exception ex) {
                await VS.MessageBox.ShowWarningAsync("error" + ex.Message);
            } finally {
                


            }

        }
    }
}
