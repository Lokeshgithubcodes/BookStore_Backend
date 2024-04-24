using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Models
{
    public class AddressUpdateModel
    {
        public string FullAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Type { get; set; }

        public int UserId { get; set; }

        public int AId {  get; set; }
    }
}
