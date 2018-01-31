using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPGPServices.View_Model
{
    public class GeographicalZone_VM
    {
        public long ID { get; set; }
        public string Label { get; set; }
        [JsonIgnore]
        public List<string>  ParentLabel { get; set; }
        [JsonIgnore]
        public List<string> ChildrenLabel { get; set; }
    }
}