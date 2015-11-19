using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2015_Pre_build_week_project.Team_Code.Utility
{
    /// <summary>
    /// Simpler form of PID logic
    /// </summary>
    public class PID
    {
        private double P, I, D;
        private double accumulatedIntegral;
        private double currentPointFeedback;

        public bool Enabled { get; set; }
        public string Name { get; set; }

        public double Max { get; set; }
        public double Min { get; set; }
        public double setpoint { get; set; }

        public PID(double p, double i, double d, double min, double max)
        {
            if(max < min)
                throw new Exception("Invalid Arguments: " + max + " Is less than " + min);

            P = p; I = i; D = d;
            accumulatedIntegral = 0;
            currentPointFeedback = 0;
            Max = max;
            Min = min;
        }

        public PID(double p, double i, double d) : this(p, i, d, double.MinValue, double.MaxValue) { }

        public double Get(double currentPoint)
        {
            return limit(((setpoint - currentPoint) * P) + ((currentPoint - currentPointFeedback) * -D) + accumulatedIntegral);
        }

        public void Update(double currentPoint)
        {
            if (I != 0) accumulatedIntegral += (currentPoint - currentPointFeedback) * I;
            if (D != 0) currentPointFeedback = currentPoint;
        }

        private double limit(double limitIn)
        {
            limitIn = limitIn > Max ? Max : limitIn;
            limitIn = limitIn < Min ? Min : limitIn;
            return limitIn;
        }
    }
}
