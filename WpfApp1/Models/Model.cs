using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WpfApp1
{
    [DataContract]
    public class Model
    {
        [DataMember(Name = "guid")]
        public string guid { get; set; }

        [DataMember(Name = "shortName")]
        public string shortName { get; set; }

        [DataMember(Name = "fullName")]
        public string fullName { get; set; }


        [DataMember(Name = "phone")]
        public string phone { get; set; }

        [DataMember(Name = "orgEmail")]
        public string orgEmail { get; set; }

        [DataMember(Name = "url")]
        public string url { get; set; }

        [DataMember(Name = "organizationType")]
        public string organizationType { get; set; }

        /*[DataMember(Name = "factualAddress")]
        public Address factualAddress { get; set; }*/





        public Model( string name )
        {
            this.guid = name;
        }
    }

    public class Address
    {
        [DataMember(Name = "formattedAddress")]
        public string formattedAddress { get; set; }
    }
}
