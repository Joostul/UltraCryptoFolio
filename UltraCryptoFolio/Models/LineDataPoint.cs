using System;
using System.Runtime.Serialization;

namespace UltraCryptoFolio.Models
{
    [DataContract]
    public class LineDataPoint
    {
        public LineDataPoint(double y, int x)
        {
            X = x;
            Y = y;
        }

        [DataMember(Name = "x")]
        public int? X = null;

        [DataMember(Name = "y")]
        public double? Y = null;
    }
}
