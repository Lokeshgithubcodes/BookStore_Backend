using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IAddressbusiness
    {
        public AddressModel AddAddress(AddressModel model);

        public List<AddressModel> GetAddresses(int UserId);

        public AddressUpdateModel UpdateAddress(AddressUpdateModel model);


    }
}
