using System;
using System.Runtime.Serialization;

namespace UltraCryptoFolio.Models
{
    public class DoughnutDataPoint
    {
        public DoughnutDataPoint(double value)
        {
            Value = value;
        }
        
        public double? Value = null;
    }
}
