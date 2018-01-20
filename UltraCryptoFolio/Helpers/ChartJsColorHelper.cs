using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UltraCryptoFolio.Helpers
{
    public static class ChartJsColorHelper
    {
        public static List<string> GetRandomColors(int amount)
        {
            var rnd = new Random();
            var colors = new List<string>();

            for (int i = 0; i < amount; i++)
            {
                var r = rnd.Next(0, 175);
                var g = rnd.Next(75, 255);
                var b = rnd.Next(0, 175);
                colors.Add($"rgba({r}, {g}, {b}, 0.7)");
            }

            return colors;
        }
    }
}
