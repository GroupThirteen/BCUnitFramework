using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Shell;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Task = System.Threading.Tasks.Task;
using BCUnitEngine;
using System.Collections.Generic;
using BCUnitFramework;

namespace Extension
{
    [Command(PackageIds.MyCommandRunProject)]
    internal sealed class MyCommandRunProject : BaseCommand<MyCommandRunProject>
    {
        protected async override Task ExecuteAsync(OleMenuCmdEventArgs e)
        {

            try {

                //string path = @"C:\Users\jaffa\Desktop\Testing\TestSummary.xml";

                Project activeProject = await VS.Solutions.GetActiveProjectAsync();


                await activeProject.BuildAsync(BuildAction.Build);
                OutputWindowPane pane = await VS.Windows.CreateOutputWindowPaneAsync("BC Unit Test");


                if (activeProject != null) {

                    string projectName = activeProject.Name;        // name of the project
                    string projectPath = activeProject.FullPath;    // absolute file path on disk


                    string path = Directory.GetParent(projectPath).FullName + "\\ProjectTestSummary.xml";
                    XmlTestOutput.CreateInitialXml(path);

                    AssemblyLoader al = new AssemblyLoader(projectPath, projectName);
                    List<Type> listOfTestAttribute = al.TestClasses;
                    Engine engine = new Engine(listOfTestAttribute, projectName);


                    string line = engine.GetOutputResult();
                    await pane.WriteLineAsync(projectName);
                    await pane.WriteLineAsync(engine.GetOutputResult());
                }




            } catch (Exception ex) {
                await VS.MessageBox.ShowAsync(ex.Message.ToString());

            } finally {

            }

        }
    }


}
