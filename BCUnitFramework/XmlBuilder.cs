using System;
using System.Xml.Linq;
using System.Linq;
using System.Text;



namespace BCUnitFramework
{
    public class XmlBuilder
    {
        private string PATH = @"C:\Users\jaffa\Desktop\XML_data.xml";
        //private XDocument xmlDocument;
        public XmlBuilder()
        {
            CreateInitialXml();
        }

        private void CreateInitialXml()
        {
            XDocument xmlDocument = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment("Comments skldjflkdsaf"),

                // root element constructor
                new XElement("BCUnitTestResults"));

            xmlDocument.Save(PATH);
        }


        // project name
        public void AddRootElement(string rootName)
        {
            XDocument xmlDoc = XDocument.Load(PATH);
            xmlDoc.Element("BCUnitTestResults").Add(new XElement(rootName));
            xmlDoc.Save(PATH);
        }

        public void AddNode(string nodeName, string nodeChild)
        {
            XDocument xmlDoc = XDocument.Load(PATH);
            xmlDoc.Element(nodeName).Add(new XElement(nodeChild));
            xmlDoc.Save(PATH);
        }
    }
}
