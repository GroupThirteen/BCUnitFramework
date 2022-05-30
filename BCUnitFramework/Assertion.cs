
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;


// The fail will not throw an exception it will print only
namespace BCUnitFramework
{


    //test that fails
    // log the fails results
    // create a plug in 
    // create xml of test result and look for a plug ing (anybody want s that file)
    // get data will take a string the pub in will be the dll
    // 
    //interface 


    public class Assertions
    {

        private static ITestOutput testOutput = new TestOutput();
        private static XmlTestOutput xmlTestOutput;


        public static void SetTestOutput(ITestOutput test)
        {
            testOutput = test;
        }

        public static void SetXmlTestOutput(XmlTestOutput test)
        {
            xmlTestOutput = test;
        }



        public static void AssertArrayEquals(int[] expected, int[] actual)
        {
            if (expected == null || actual == null || (expected.Length != actual.Length) ) {
                printResult($"FAIL: Arrays are not Equal!");
                return;
            }

            else if (expected.Length == actual.Length) {
                for (int i = 0; i < expected.Length; i++) {
                    if (expected[i] != actual[i]) {
                        printResult($"FAIL: Expected: {expected[i]} Actual: {actual[i]} at index {i}");
                        return;
                    }
                }

                printResult($"PASS");

            } 




        }

        public static void printResult(string message) {
            StackFrame sf = new StackFrame(2);
            var method = sf.GetMethod();
            var type = method.DeclaringType;
            string methodName = method.Name;
            string className = type.Name;

            //if (File.Exists(@"C:\Users\jaffa\Desktop\testSummary.xml")) {
            //    XDocument myDoc = XDocument.Load(@"C:\Users\jaffa\Desktop\testSummary.xml");
            //    XElement serviceTicket = myDoc.Root;

            //    serviceTicket.Element("projectName").Add(
            //          new XElement($"{className}",
            //               new XElement($"{methodName}", $"{message}")

            //    ));


            //    myDoc.Save(@"C:\Users\jaffa\Desktop\testSummary.xml");
            //}
            //else 
            //{
            //XmlDocument doc = new XmlDocument();
            //XmlElement root = doc.CreateElement("testsResults");
            //XmlElement ClassNameTag = doc.CreateElement($"{className}");

            //XmlElement methodNameTag = doc.CreateElement($"{methodName}");
            //methodNameTag.InnerText = $"{message}";


            //ClassNameTag.AppendChild(methodNameTag);

            //projectNameTag.AppendChild(ClassNameTag);

            //root.AppendChild(projectNameTag);

            //doc.AppendChild(root);


            //doc.Save(@"C:\Users\jaffa\Desktop\testSummary.xml");



            xmlTestOutput.AddChildToRootElement(className, methodName, message);
            
            

            testOutput.Print($"{className}.{methodName} {message}");
        }


        public static void AssertEquals(int expected, int actual)
        {
            
             
            if (expected != actual) {
                printResult($"FAIL: Expected: {expected} Actual: {actual}");
                return;
            }

            printResult($"PASS");
        }


        public static void AssertNotEquals(int expected, int actual)
        {
            if (expected == actual) {
                printResult($"FAIL: Expected: {expected} Actual: {actual}");
                return;
            }


            //testOutput.Print("PASS");
            printResult($"PASS");
        }


        public static void AssertNotEquals(double expected, double acutal)
        {
            if (expected == acutal) {
                printResult($"FAIL: Expected: {expected} Actual: {acutal}");
                return;
            }
            printResult($"PASS");
        }



    }
}
