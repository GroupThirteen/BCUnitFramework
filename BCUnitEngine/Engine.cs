using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using BCUnitFramework;


// Class Library (.NET Framework)
// .NET Framework  4.7.2

namespace BCUnitEngine
{

    public class Engine
    {

        private List<Type> listOfTestAttribute = null;                  // Will hold the 
        private List<string> invokedMethodNames = new List<string>();
        //private XmlDocument doc = new XmlDocument();
        private string projectName;



        private ITestOutput testOutput = new TestOutput();
        private XmlTestOutput xmlTestOutput;
        
        public Engine (List<Type> listOfTestAttribute, string projectName)
        {
            this.projectName = projectName;
            this.listOfTestAttribute = listOfTestAttribute;
            xmlTestOutput = new XmlTestOutput(projectName);

            if (listOfTestAttribute != null) {
                MethodsParser();
            } else {
                ShowMessage("The list of Test Attribute is null");
            }
        }



        // Will parse and find the methods that are decorated with test attributes
        private void MethodsParser()
        {


            //xml root
          
            //XmlElement root = doc.CreateElement("testsResults");

            foreach (Type t in listOfTestAttribute) {

                //XmlNode classElement = root.AppendChild(doc.CreateElement(t.Name));
         
                // Query to find the BeforeAllTestAttribute tag
                var BeforeAllTest = from m in t.GetMethods()
                                    where m.GetCustomAttributes(false).Any(a => a is BeforeAllTestsAttribute)
                                    select m;
             
                //InvokeMethod(t, BeforeAllTest, classElement);


                // Query to find the TestMethodsAttriubte tags
                var allTestMethods = from m in t.GetMethods()
                                     where m.GetCustomAttributes(false).Any(a => a is TestMethodAttribute)
                                     select m;

                // Re-ordering the founded methods

                var ordered = allTestMethods.OrderBy(p => p.GetCustomAttribute<TestMethodAttribute>().Order);

                // Two lists to store the TestMethodsAttriubte tags 
                List<MethodInfo> withOrder = new List<MethodInfo>();        // has Order = 1, 2, 3, ..

                List<MethodInfo> withoutOrder = new List<MethodInfo>();     // has default Order = 0


                // adding TestMethodsAttriubte methods to the lists
                foreach (var v in ordered) {
                    int order = v.GetCustomAttribute<TestMethodAttribute>().Order;


                    if (order != 0) {
                        withOrder.Add(v);
                    } else {
                        withoutOrder.Add(v);
                    }
                }

                // Concatenate the lists with Order of 1, 2, 3......, then 0, 0, 0
                List<MethodInfo> newList = withOrder.Concat(withoutOrder).ToList();

                //xml


                InvokeMethod(t, newList);


                // Checking for AfterAllTests Attribute tag
                var afterTest = from m in t.GetMethods()
                                where m.GetCustomAttributes(false).Any(a => a is AfterAllTestsAttribute)
                                select m;

                InvokeMethod(t, afterTest);

            }

        }


        //private void InvokeMethod(Type type, IEnumerable<MethodInfo> testList, XmlNode testClassNode)
        private void InvokeMethod(Type type, IEnumerable<MethodInfo> testList)

        {

            Assertions.SetTestOutput(testOutput);
            
            if (testList != null) {
                object instance = Activator.CreateInstance(type);
                foreach (MethodInfo mInfo in testList) {
                    Assertions.SetXmlTestOutput(xmlTestOutput);
                    //XmlNode methodNode = testClassNode.AppendChild(doc.CreateElement("method"));
                    //XmlAttribute methodNameAttribute = doc.CreateAttribute("method");
                    //methodNameAttribute.Value = mInfo.Name;
                    //methodNode.Attributes.Append(methodNameAttribute);

                    //set the iner
                    //methodNode.InnerText = 

                    mInfo.Invoke(instance, new object[0]);
                    invokedMethodNames.Add(mInfo.Name);
                }
            }
        }





        public void ShowMessage(string s)
        {
            System.Windows.Forms.MessageBox.Show($"Message:\n{s}");
        }

  

   
        public string GetOutputResult()
        {
            return testOutput.GetResultString();
        }


        public int GetCount()
        {
            return listOfTestAttribute.Count;
        }
    }
}
