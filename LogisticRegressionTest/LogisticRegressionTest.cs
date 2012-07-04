using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logistic_Regression;
using System.Diagnostics;

namespace LogisticRegressionTest
{
    [TestClass]
    public class LogisticRegressionTest
    {
        [TestMethod]
        public void TestLearn()
        {
            double[][] inputs = new double[4][]{new double[]{1,2} , new double[]{1,3}, new double[]{1,4} , new double[]{1,5} };
            //double[][] inputs = LogisticRegression.ReadInputFromFile("exe1data1.txt");
            double[] outputs = new double[] { 1, 1,1,0 };
            //double[] outputs = LogisticRegression.ReadDataFromFile("
            double alpha = 0.03;
            double[] theta = new double[]{1,1};
            LogisticRegression lr = new LogisticRegression();
            double[] newtheta = lr.Learn(inputs, outputs, theta, alpha);           
            DisplayArray(newtheta);

        }

        [TestMethod]
        public void TestPrependingOnes()
        {
            double[][] input = new double[1][] { new double[] { 2, 2 } };
            double[][] output = LogisticRegression.PrependWithOnes(input);
            DisplayArray(output[0]);
            Assert.IsTrue(output[0][0] == 1);
        }

        [TestMethod]
        public void TestFileRead()
        {
            double[][] filevalues = LogisticRegression.ReadInputFromFile("ex1data1.txt");
            Debug.WriteLine(filevalues[0][0]);
            Assert.IsNotNull(filevalues, "File contents not Read Properly");
        }


        static void DisplayArray(double[] arr)
        {
            foreach (double elem in arr)
            {
                Console.WriteLine(elem + " ");                     
            }
        }
    }
}
