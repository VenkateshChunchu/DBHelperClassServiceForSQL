using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Checking
{
    [DataContract]
    public class NewsArticle
    {
        [DataMember(Order=1,Name="Article ID")]
        public int ID { get; set; }
        [DataMember(Order = 4, Name="Title Of Article")]
        public string Title { get; set; }
        [DataMember(Order = 2,Name="Description")]
        public string Description { get; set; }
        [DataMember(Order = 3,Name="Image Source")]
        public string ImageURL { get; set; }
    }
}
