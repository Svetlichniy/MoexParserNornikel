using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Point
    {
        public string Date { get; set; }
        public double Value { get; set; }

        public Point(string date, double value)
        {
            Date = date;
            Value = value;
        }
    }
}
