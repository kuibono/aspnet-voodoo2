using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Voodoo;
namespace Voodoo.Maths
{
    public class Statistic
    {

        #region Initialize
        public double[] Source { get; set; }

        public Statistic(double[] source)
        {
            this.Source = source;
        }
        public Statistic(long[] source)
        {
            this.Source = source.Select(x => Convert.ToDouble(x)).ToArray();
        }
        public Statistic(int[] source)
        {
            this.Source = source.Select(x => Convert.ToDouble(x)).ToArray();
        }
        public Statistic(decimal[] source)
        {
            this.Source = source.Select(x => Convert.ToDouble(x)).ToArray();
        }
        #endregion

        public double Count()
        {
            return this.Source.Count();
        }

        public double Average()
        {
            return Source.Average();
        }

        public double Max()
        {
            return Source.Max();
        }

        public double Min()
        {
            return Source.Min();
        }

        public double Sigma()
        {
            //^2=∑(X-μ) ^2/ N
            var avg = this.Average();
            return Source.Sum(x => System.Math.Pow(x - avg, 2)) / Source.Length;
        }

        public double StandardDev()
        {
            return System.Math.Sqrt(Sigma());
        }

        public double MidNumber()
        {
            var orderedSource = Source.OrderBy(x=>x).ToArray();
            if (Count() % 2 == 0)
            {
                return orderedSource[(Source.Length + 2) / 2];
            }
            else
            {
                return orderedSource[(Source.Length + 1) / 2];
            }
        }

        public double UCL(int sigma)
        {
            return MidNumber() + sigma * StandardDev();
        }

        public double LCL(int sigma)
        {
            return MidNumber() - sigma * StandardDev();
        }

        public double StandardAverage(int sigma)
        {
            return this.Source.Where(x => x > LCL(sigma) && x < UCL(sigma)).Average();
        }
    }
}
