using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class AddressBusiness:IAddressbusiness
    {
        private readonly IAddressRepository addressRepository;

        public AddressBusiness(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        public AddressModel AddAddress(AddressModel model)
        {
            return addressRepository.AddAddress(model);
        }


    }
}
