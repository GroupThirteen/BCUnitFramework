using BCUnitEngine;
using BCUnitFramework;
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


                //XmlTestOutput.CreateInitialXml();

                // This will get projects in the solution
                var solution = await VS.Solutions.GetAllProjectsAsync();

                bool isPathSet = false;


                // Building the solution
                bool buildStarted = await VS.Build.BuildSolutionAsync(BuildAction.Build);

                OutputWindowPane pane = await VS.Windows.CreateOutputWindowPaneAsync("BC Unit Test");

                if (buildStarted) {


                    foreach (var project in solution) {

                        string projectName = project.Name;        // name of the project
                        string projectPath = project.FullPath;    // absolute file path on disk

                        if (!isPathSet) {
                            string path = Directory.GetParent(Directory.GetParent(projectPath).FullName) + "\\SolutionTestSummary.xml";
                            XmlTestOutput.CreateInitialXml(path);
                            isPathSet = true;
                        }


                        AssemblyLoader al = new(projectPath, projectName);
                        Engine engine = new(al.TestClasses, projectName);

                        await pane.WriteLineAsync(projectName);
                        await pane.WriteLineAsync(engine.GetOutputResult());

                    }//foreach

                }

            } catch (System.Exception ex) {
                await VS.MessageBox.ShowWarningAsync("error" + ex.Message);
            } finally {



            }

        }
    }
}
