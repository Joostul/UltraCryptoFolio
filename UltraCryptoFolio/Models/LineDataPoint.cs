using System;
using System.Runtime.Serialization;

namespace UltraCryptoFolio.Models
{
    [DataContract]
    public class LineDataPoint
    {
        public LineDataPoint(double y, string label, string name = null)
        {
            Label = label;
            Y = y;
            Name = name;
        }

        [DataMember(Name = "label")]
        public string Label = null;

        [DataMember(Name = "name")]
        public string Name = null;

        [DataMember(Name = "y")]
        public double? Y = null;
    }
}
