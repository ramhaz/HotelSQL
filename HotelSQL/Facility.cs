using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace HotelSQL
{
     public class Facility
     { 
        
        public int FacilityNo { get; set; }
        public int FacilityId { get; set; }
        public string Name { get; set; }
      

    
        public override string ToString()
        {
            return $"ID: {FacilityId}, Name: {Name}";
        }
    }

}
