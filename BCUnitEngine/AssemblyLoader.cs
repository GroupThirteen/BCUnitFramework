using BCUnitFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;


namespace BCUnitEngine
{
    public class AssemblyLoader
    {
        private string projectPath;
        private string projectName;

        private string projectDirectory;            // To use it for a default directory
        private List<Type> listOfTestAttribute;     // List of Dll files that has the Test Class Attribute
        private Assembly assemblyFile;              //  Assembly file of the first index  of file path      files[0] = path containing the bin folder 
                                                    //                                                      files[1] = path containing the obj folder 
                                                    //private DirectoryInfo info;



        
        public AssemblyLoader(string projectPath, string projectName)
        {
            this.projectPath = projectPath;
            this.projectName = projectName;

            // make a method to run all these three
            SetCurrentDirectory(projectPath);
            AddToList();
            ReadAllAttribute();

          
            


        }





        // set the current working directory
        private void SetCurrentDirectory(string path)
        {
            // the parent directory
            DirectoryInfo info = Directory.GetParent(path);
            // set the current working directory
            Directory.SetCurrentDirectory(info.ToString());
            this.projectDirectory = Directory.GetCurrentDirectory();
        }



        private void AddToList()
        {
            // An array of the full names (including paths) for the files 
            //string temp = GetProjectName();
            string searchPatten = projectName + (".dll");

            // array will contain 2 files of .dll...
            // one inside bin folder and second one inside obj folder
            var files = Directory.GetFiles(projectDirectory, searchPatten, SearchOption.AllDirectories);

            // TODO: try to find a way to return the full path


            // assembly file of the bin folder  
            assemblyFile = Assembly.LoadFrom(files[0]);




            TimeStamp(files[0]);








        }

        public void TimeStamp(string newFile)
        {

            string projectNamePDP = projectName + (".pdb");
            var rename = Directory.GetFiles(projectDirectory, projectNamePDP, SearchOption.AllDirectories);
            foreach (var file in rename) {
                Guid myuuid = Guid.NewGuid();
                string myuuidAsString = myuuid.ToString();
                System.IO.File.Move(file, file + myuuidAsString);
            }
            Guid myuuid2 = Guid.NewGuid();
            string myuuidAsString2 = myuuid2.ToString();
            System.IO.File.Move(newFile, newFile + myuuidAsString2);
        }



        //TestAttributes list not used in c# no get and set

        public List<Type> TestClasses
        {
            get
            {
                return listOfTestAttribute;

            }
        }

    // list has types of Test class Attributes
    private void ReadAllAttribute()
    {
        listOfTestAttribute = new List<Type>();

        Type[] allTypes = assemblyFile.GetTypes();
        foreach (Type type in allTypes) {
            if ((type.GetCustomAttribute(typeof(TestClassAttribute))) is TestClassAttribute) {
                listOfTestAttribute.Add(type);
            } 
        }

    }


    public void ShowMessage(string s)
    {
        System.Windows.Forms.MessageBox.Show($"Message:\n{s}");
    }




}




}










