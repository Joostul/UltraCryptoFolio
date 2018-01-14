using System;
using System.Runtime.Serialization;

namespace UltraCryptoFolio.Models
{
    [DataContract]
    public class LineDataPoint
    {
        public LineDataPoint(double y, string label)
        {
            Label = label;
            Y = y;
        }

        [DataMember(Name = "label")]
        public string Label = null;

        [DataMember(Name = "y")]
        public double? Y = null;
    }
}
