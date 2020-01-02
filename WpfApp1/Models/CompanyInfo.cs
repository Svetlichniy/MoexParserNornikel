using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WpfApp1.Models
{
    [DataContract]
    public class CompanyInfo
    {
        [DataMember(Name = "fullName")]
        public string fullName { get; set; }

        [DataMember(Name = "ogrn")]
        public string ogrn { get; set; }

        [DataMember(Name = "inn")]
        public string inn { get; set; }

        [DataMember(Name = "kpp")]
        public string kpp { get; set; }

        [DataMember(Name = "orgAddress")]
        public string orgAddress { get; set; }

        [DataMember(Name = "factualAddress")]
        public factualAddress factualAddress { get; set; }

        [DataMember(Name = "authorityFactualAddress")]
        public authorityFactualAddress authorityFactualAddress { get; set; }

        [DataMember(Name = "orgEmail")]
        public string orgEmail { get; set; }

        [DataMember(Name = "url")]
        public string url { get; set; }

    }

    [DataContract]
    public class authorityFactualAddress
    {
        [DataMember(Name = "formattedAddress")]
        public string formattedAddress { get; set; }
    }

    [DataContract]
    public class factualAddress
    {
        [DataMember(Name = "formattedAddress")]
        public string formattedAddress { get; set; }
    }

    public class ResultData
    {
        public string fullName { get; set; }

        public string ogrn { get; set; }

        public string inn { get; set; }

        public string kpp { get; set; }

        public string orgAddress { get; set; }

        public string factualAddress { get; set; }

        public string orgEmail { get; set; }

        public string url { get; set; }

    }
}
