using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Voodoo.Maths;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            STest();
        }

        public static void STest()
        {
            var x = new double[] { 1, 2, 2, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 8, 9, 6, 6, 6, 6, 6, 2, 5, 5, 5, 5, 5, 5, 5, 7, 9, 0, 3, 2, 4, 7, 9 };

            Statistic s = new Statistic(x);
            Console.WriteLine("上限:"+s.UCL(3).ToString());
            Console.WriteLine("下限:" + s.LCL(3).ToString());
            Console.WriteLine("平均:" + s.Average().ToString());
            Console.WriteLine("中值:" + s.MidNumber().ToString());
            Console.WriteLine("方差:" + s.Sigma().ToString());
            Console.WriteLine("标准平均数:" + s.StandardAverage(3).ToString());
            Console.WriteLine("标准差:" + s.StandardDev().ToString());
            Console.ReadLine();
        }
    }
}
