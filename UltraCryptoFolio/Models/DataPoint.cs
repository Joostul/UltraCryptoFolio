using System;
using System.Runtime.Serialization;

namespace UltraCryptoFolio.Models
{
    [DataContract]
    public class DataPoint
    {
        public DataPoint(double y, string name, double x = 0)
        {
            this.X = x;
            this.Y = y;
            this.name = name;
            this.legendText = name;
            this.indexLabel = name;
        }
        
        [DataMember(Name = "x")]
        public Nullable<double> X = null;
        
        [DataMember(Name = "y")]
        public Nullable<double> Y = null;

        [DataMember(Name = "name")]
        public string name = null;

        [DataMember(Name = "legendText")]
        public string legendText = null;

        [DataMember(Name = "indexLabel")]
        public string indexLabel = null;
    }
}
