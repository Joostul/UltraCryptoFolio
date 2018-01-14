using System;
using System.Runtime.Serialization;

namespace UltraCryptoFolio.Models
{
    [DataContract]
    public class DoughnutDataPoint
    {
        public DoughnutDataPoint(double y, string name, double x = 0)
        {
            X = x;
            Y = y;
            this.name = name;
            legendText = name;
            indexLabel = name;
        }
        
        [DataMember(Name = "x")]
        public double? X = null;
        
        [DataMember(Name = "y")]
        public double? Y = null;

        [DataMember(Name = "name")]
        public string name = null;

        [DataMember(Name = "legendText")]
        public string legendText = null;

        [DataMember(Name = "indexLabel")]
        public string indexLabel = null;
    }
}
