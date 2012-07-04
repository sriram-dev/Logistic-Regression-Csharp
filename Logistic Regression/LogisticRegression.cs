using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Logistic_Regression
{
    public class LogisticRegression
    {
       /// <summary>
       /// Learning Algorithm of Logistic Regression
       /// </summary>
       /// <param name="inputs">Input parameters</param>
       /// <param name="outputs">Corresponding outputs</param>
       /// <param name="theta">Terms which forms the Hypothesis</param>
       /// <param name="alpha">To Control the Rate</param>      
        public double[] Learn(double[][] inputs , double[] outputs, double[] theta , double alpha )
        {
            
            double[] h = new double[inputs.Length];
            int k = 0;
            double cost = 0.0;
            double[] newtheta = new double[theta.Length];            
            while (true)
            {
                foreach(double[] input in inputs)
                {
                    h[k] = Normalise(VectorProduct(input, theta));
                    if (k + 1 < inputs.Length)
                    {
                        k++;
                    }
                }
                double newcost = CalculateCost(h, outputs);
                newtheta = CalculateTheta(inputs, h, outputs, theta ,  alpha);
                Console.WriteLine("cost: " + newcost);               
                //Console.WriteLine("cost Difference : " + (Math.Abs(newcost) - Math.Abs(cost)));
                if (Math.Abs(Math.Abs(newcost) - Math.Abs(cost)) < 0.001)
                    break;
                else
                    cost = newcost;             
                theta = newtheta;
            }
            return h;
        }
        
        static double CalculateCost(double[] h , double[] outputs)
        {
            double cost = 0.0;
            if (h.Length == outputs.Length)
            {
                for (int i = 0; i < h.Length; i++)
                {
                    cost += (-outputs[i] * Math.Log(h[i])) - ((1 - outputs[i]) * Math.Log(1 - h[i]));
                }
            }
            return cost / h.Length;
        }

        static double[] CalculateTheta(double[][] inputs , double[] h, double[] outputs, double[] theta, double alpha )
        {
            double[] newtheta = new double[theta.Length];
            double[] delta = new double[theta.Length];            
            for (int i = 0; i < theta.Length; i++)
            {
                for(int j=0;j< inputs.Length ;j++)
                {
                    delta[i] += (h[j] - outputs[j]) * inputs[j][i];                
                }
                delta[i] *= alpha;
            }
            for (int k = 0; k < delta.Length; k++)
            {
                newtheta[k] = theta[k] - delta[k];
            }

            return newtheta;
        }

#region helpers 

       public static double[][] ReadInputFromFile(string filePath)
        {            
            string[] lines= File.ReadAllLines(filePath);
            double[][] data = new double[lines.Length][];
            for (int i = 0; i < lines.Length;i++)
            {
                string[] entry = lines[i].Split(new char[] { ',' });
                data[i] = new double[entry.Length];
                for (int j = 0; j < entry.Length;j++)
                {
                    data[i][j] = double.Parse(entry[j]);
                }
            }
            return data;
        }

        public static double[] ReadDataFromFile(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            double[] data = new double[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                data[i] = double.Parse(lines[i]);
            }
            return data;
        }

        static double VectorProduct(double[] mul1, double[] mul2)
        {
            double product = 0.0;
            if (mul1.Length == mul2.Length)
            {
                for(int i=0;i<mul1.Length;i++){

                    product += mul1[i] * mul2[i];                
                }
            }
            return product;
        }

        static double Normalise(double val)
        {
            return (1/(1+ Math.Pow(Math.E,(-val))));
        }


        static void Display(double[] arr)
        {
            foreach (double elem in arr)
            {
                Console.WriteLine(elem + " ");
            }
        }

        public static double[][] PrependWithOnes(double[][] arr)
        {
            double[][] newarr = new double[arr.Length][];
            for (int i=0;i< arr.Length;i++)
            {
                newarr[i] = new double[arr[i].Length + 1];
                newarr[i][0] =1;
                for (int j = 0; j < arr[i].Length; j++)
                {
                    newarr[i][j + 1] = arr[i][j];
                }
            }
            return newarr;
        }
#endregion

    }
}
