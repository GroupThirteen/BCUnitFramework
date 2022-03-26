using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BCUnitEngine;

namespace MainMethod
{
    public class Class1
    {

        public static void Main(string [] args)
        {
            //AssemblyLoader al = new AssemblyLoader(@"C:\Users\jaffa\source\repos\Capstone Project\Testing user\TEST\bin\Debug\TEST.dll", "TEST");
            //List<Type> listOfTestAttribute = al.GetlistOfTestAttribute();
            //Engine engine = new Engine(listOfTestAttribute);

            Assembly assemblyFile = Assembly.LoadFrom(@"C:\Users\jaffa\source\repos\Capstone Project\Testing user\TEST\bin\Debug\TEST.dll");




            Console.WriteLine(assemblyFile);
            Console.ReadLine();
        }
    }
}
