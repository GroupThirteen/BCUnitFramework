
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;


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


        public static void SetTestOutput(ITestOutput test)
        {
            testOutput = test;
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
