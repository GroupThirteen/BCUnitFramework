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
            //await VS.MessageBox.ShowConfirmAsync((new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString());

            await VS.MessageBox.ShowErrorAsync("before try");

            try {

                XmlTestOutput.CreateInitialXml();



                //await VS.MessageBox.ShowConfirmAsync((new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString());

                // This will get the active project ( highlighted with a mouse click)
                Project activeProject = await VS.Solutions.GetActiveProjectAsync();



                //await VS.MessageBox.ShowConfirmAsync((new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString());

                //Project project = await VS.Solutions.GetActiveProjectAsync();
                await activeProject.BuildAsync(BuildAction.Rebuild);


                //await VS.MessageBox.ShowConfirmAsync((new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString());

                OutputWindowPane pane = await VS.Windows.CreateOutputWindowPaneAsync("BC Unit Test");


                //await VS.MessageBox.ShowConfirmAsync((new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString());
               


                    string projectName = activeProject.Name;        // name of the project
                    string projectPath = activeProject.FullPath;    // absolute file path on disk


                //await VS.MessageBox.ShowAsync($"Running on {projectName} Project");
                //pane = await VS.Windows.CreateOutputWindowPaneAsync($"Results of {projectName} Project:");


                // TODO: try to find a way to return the full path
                //string filePath = await activeProject.GetAttributeAsync("TargetPath");

                await VS.MessageBox.ShowErrorAsync("before ass loader");
                //await VS.MessageBox.ShowConfirmAsync((new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString());

                AssemblyLoader al = new AssemblyLoader(projectPath, projectName);
                //await VS.MessageBox.ShowConfirmAsync((new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString());

                List<Type> listOfTestAttribute = al.TestClasses;
                //await VS.MessageBox.ShowConfirmAsync((new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString());

                Engine engine = new Engine(listOfTestAttribute, projectName);
                //await VS.MessageBox.ShowConfirmAsync((new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString());


                await VS.MessageBox.ShowErrorAsync("after ass");
                //await VS.MessageBox.ShowConfirmAsync((new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString());

                await pane.WriteLineAsync($"Results of {projectName} Project");
                //await VS.MessageBox.ShowConfirmAsync((new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString());

                string line = engine.GetOutputResult();
                //await VS.MessageBox.ShowConfirmAsync((new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString());


                await pane.WriteLineAsync(engine.GetOutputResult());
                //await VS.MessageBox.ShowConfirmAsync((new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString());
                //al.UnoladDomain();













                //await VS.MessageBox.ShowErrorAsync("No Project has been selected");
                //pane = await VS.Windows.CreateOutputWindowPaneAsync("No project is selected");


                await VS.MessageBox.ShowErrorAsync("before catch");

            } catch (Exception ex) {
                await VS.MessageBox.ShowAsync(ex.Message.ToString());
            
            
            
            } finally {

                

            }



            await VS.MessageBox.ShowErrorAsync("before the function ends");
        
        }
    }


}
