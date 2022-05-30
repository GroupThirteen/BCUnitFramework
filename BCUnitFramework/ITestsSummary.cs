using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCUnitFramework
{
    public interface ITestsSummary
    {
        void TestResults(string xmlResult);
    }

    // parse and make 
    // result prject name, name of classes, name of class , pass or fail
    // building the xml as  we loop
}
