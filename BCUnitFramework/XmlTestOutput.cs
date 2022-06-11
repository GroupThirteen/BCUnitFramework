using System;
using System.Xml.Linq;

namespace BCUnitFramework
{





    public class XmlTestOutput : ITestOutput
    {
        private string projectName;
        //private static string PATH = @"C:\Users\jaffa\source\repos\Capstone Project\Testing user\TEST\TestSummary.xml";
        private static string PATH;

        public XmlTestOutput(string projectName)
        {
            this.projectName = projectName;

        }



        // send to the child class
        public string GetResultString()
        {
            throw new NotImplementedException();
        }


        // add to the xml
        public void Print(string str)
        {
            throw new NotImplementedException();
        }

        public static void CreateInitialXml(string newPath)
        {

            PATH = newPath;
            XDocument xmlDocument = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment("Test summary"),

                // root element constructor
                new XElement("BCUnitTestResults"));

            xmlDocument.Save(PATH);
        }


        // project name
        public void AddChildToRootElement(string className, string methodName, string message)
        {
            XDocument xmlDoc = XDocument.Load(PATH);

            XElement element = new XElement(projectName);

            if (xmlDoc.Element("BCUnitTestResults").Element(projectName) == null) {
                //XElement messageX = new XElement(message);
                XElement methodNameX = new XElement(methodName);
                XElement classNameX = new XElement(className);
                XElement projectNameX = new XElement(projectName);

                methodNameX.Add(message);
                classNameX.Add(methodNameX);
                projectNameX.Add(classNameX);

                xmlDoc.Element("BCUnitTestResults").Add(projectNameX);
            } else if (xmlDoc.Element("BCUnitTestResults").Element(projectName).Element(className) == null) {
                //XElement messageX = new XElement(message);
                XElement methodNameX = new XElement(methodName);
                XElement classNameX = new XElement(className);

                methodNameX.Add(message);
                classNameX.Add(methodNameX);

                xmlDoc.Element("BCUnitTestResults").Element(projectName).Add(classNameX);



            } else {

                //XElement messageX = new XElement(message);
                XElement methodNameX = new XElement(methodName);

                methodNameX.Add(message);

                xmlDoc.Element("BCUnitTestResults").Element(projectName).Element(className).Add(methodNameX);

            }

            xmlDoc.Save(PATH);
        }


    }

}

