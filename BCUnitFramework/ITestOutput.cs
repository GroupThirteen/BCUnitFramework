using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCUnitFramework
{
    public interface ITestOutput
    {
        void Print(string str);
        string GetResultString();
    }
}
