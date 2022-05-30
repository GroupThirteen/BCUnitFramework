using BCUnitFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;

namespace BCUnitEngine
{





    //public interface ILoader
    //{
    //    string Execute();
    //}

    //public class Loader : MarshalByRefObject, ILoader
    //{
    //    public string Execute()
    //    {
    //        System.Windows.Forms.MessageBox.Show("in execute");
    //        const string path = @"C:\Users\jaffa\source\repos\Capstone Project\Testing user\TEST\bin\Debug\TEST.dll";
    //        //assembly = Assembly.LoadFrom(path);
    //        //return assembly.FullName;
    //        return "ass";
    //    }
    //}



    //class Sandboxer : MarshalByRefObject
    //{
    //    const string pathToUntrusted = @"C:\Users\jaffa\source\repos\Capstone Project\Testing user\TEST\bin\Debug\";
    //    const string untrustedAssembly = "TEST";
    //    const string untrustedClass = "UntrustedCode.UntrustedClass";
    //    const string entryPoint = "IsFibonacci";
    //    private static Object[] parameters = { 45 };
    //    private AppDomain domain;
    //    private Assembly assembly;

    //    public Assembly getAssembly()
    //    {
    //        return assembly;
    //    }

    //    public static Sandboxer Create()
    //    {
    //        //Setting the AppDomainSetup. It is very important to set the ApplicationBase to a folder   
    //        //other than the one in which the sandboxer resides.  
    //        AppDomainSetup adSetup = new AppDomainSetup();
    //        adSetup.ApplicationBase = Path.GetFullPath(pathToUntrusted);

    //        //Setting the permissions for the AppDomain. We give the permission to execute and to   
    //        //read/discover the location where the untrusted code is loaded.  
    //        PermissionSet permSet = new PermissionSet(PermissionState.None);
    //        permSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
    //        permSet.AddPermission(new IsolatedStorageFilePermission(PermissionState.Unrestricted));
    //        permSet.AddPermission(new FileIOPermission(PermissionState.Unrestricted));

    //        //We want the sandboxer assembly's strong name, so that we can add it to the full trust list.  
            
            
            
    //        StrongName fullTrustAssembly = typeof(Sandboxer).Assembly.Evidence.GetHostEvidence<StrongName>();

    //        //Now we have everything we need to create the AppDomain, so let's create it.  
    //        AppDomain newDomain = AppDomain.CreateDomain("Sandbox", null, adSetup, permSet, fullTrustAssembly);

    //        //Use CreateInstanceFrom to load an instance of the Sandboxer class into the  
    //        //new AppDomain.   
    //        ObjectHandle handle = Activator.CreateInstanceFrom(
    //            newDomain, typeof(Sandboxer).Assembly.ManifestModule.FullyQualifiedName,
    //            typeof(Sandboxer).FullName
    //            );
    //        //Unwrap the new domain instance into a reference in this domain and use it to execute the   
    //        //untrusted code.  
    //        Sandboxer newDomainInstance = (Sandboxer)handle.Unwrap();
    //        newDomainInstance.domain = newDomain;
    //        newDomainInstance.ExecuteUntrustedCode(untrustedAssembly, untrustedClass, entryPoint, parameters);
    //        return newDomainInstance;
    //    }

    //    public void ExecuteUntrustedCode(string assemblyName, string typeName, string entryPoint, Object[] parameters)
    //    {
    //        //Load the MethodInfo for a method in the new Assembly. This might be a method you know, or   
    //        //you can use Assembly.EntryPoint to get to the main function in an executable.  
    //        //MethodInfo target = Assembly.Load(assemblyName).GetType(typeName).GetMethod(entryPoint);
    //        //try {
    //        //    //Now invoke the method.  
    //        //    bool retVal = (bool)target.Invoke(null, parameters);
    //        //} catch (Exception ex) {
    //        //    // When we print informations from a SecurityException extra information can be printed if we are   
    //        //    //calling it with a full-trust stack.  
    //        //    new PermissionSet(PermissionState.Unrestricted).Assert();
    //        //    Console.WriteLine("SecurityException caught:\n{0}", ex.ToString());
    //        //    CodeAccessPermission.RevertAssert();
    //        //    Console.ReadLine();
    //        //}
    //        assembly = Assembly.Load(assemblyName);
    //    }

    //}


    public class AssemblyLoader
    {
        private string projectPath;
        private string projectName;

        private string projectDirectory;            // To use it for a default directory
        private List<Type> listOfTestAttribute;     // List of Dll files that has the Test Class Attribute
        private static Assembly assemblyFile;              //  Assembly file of the first index  of file path      files[0] = path containing the bin 

        private AppDomain dom;
        private static string filePath;


        //private Sandboxer sob;




        public AssemblyLoader(string projectPath, string projectName)
        {
            this.projectPath = projectPath;
            this.projectName = projectName;

            // make a method to run all these three
            SetCurrentDirectory(projectPath);
            ShowMessage("AddToList");
            AddToList();

            ShowMessage("ReadAllAttribute");

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
            try {
                ShowMessage(files[0]);
                filePath = files[0];
                assemblyFile = Assembly.LoadFrom(files[0]);


                //sob = Sandboxer.Create();
                //assembly = sob.getAssembly();


                //AppDomain domain = AppDomain.CreateDomain("child");


                //var loader = (ILoader)domain.CreateInstanceAndUnwrap(typeof(Loader).Assembly.FullName, typeof(Loader).FullName);

                //ShowMessage(loader.Execute());


                //MyCallBack();
                //ShowMessage((new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber().ToString());

                //otherDomain.DoCallBack(new CrossAppDomainDelegate(MyCallBack));


                //ShowMessage("anus " + assemblyFile.FullName);
                //dom = AppDomain.CreateDomain("newDomain");
                //AssemblyName assemblyName = new AssemblyName();
                //assemblyName.CodeBase = files[0];
                //ShowMessage("Ass name" + assemblyName.Name);
                //assemblyFile = dom.Load("TEST2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
                //Type[] types = assemblyFile.GetTypes();




            } catch (FileNotFoundException e) {
                ShowMessage("error " + e.Message + " " + e.FileName);
            }

        }


        
        public void UnoladDomain()
        {
            AppDomain.Unload(AppDomain.CurrentDomain);


        }

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


        public string GetProjectName()
        {
            return projectName;
        }

        public void ShowMessage(string s)
        {
            System.Windows.Forms.MessageBox.Show($"Message:\n{s}");
        }




    }




}










