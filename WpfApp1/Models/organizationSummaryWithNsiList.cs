using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WpfApp1.Models
{
    [DataContract]
    public class organizationSummaryWithNsiList
    {
        [DataMember(Name = "organizationSummaryWithNsiList")]
        public List<Model> Models { get; set; }
        [DataMember]
        public int total { get; set; }
    }
}
