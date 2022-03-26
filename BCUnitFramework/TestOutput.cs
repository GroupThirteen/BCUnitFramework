using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BCUnitFramework
{

    // Call back design pattern
    public class TestOutput : ITestOutput
    {
        private  string result;
        

   
        
        public void Print(string str)
        {
            result = result + str + "\n";
        }

        public string GetResultString()
        {
            return result;
        }
        public void clearResult()
        {
            result = String.Empty;
        }
    }
}
