using Microsoft.AspNetCore.Mvc;
using Panda.Data;
using Panda.Models;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;

namespace Panda.Services
{
    public class PackageService
    {
        private readonly PandaContext _pandaContext;

        public PackageService(PandaContext pandaContext)
        {
            _pandaContext = pandaContext;
        }
        [HttpPost]
        public async Task<Package> CreatePackage(string description, double weight, string addressShipping, User user) 
        {
            Package package = new Package() 
            {
                Description = description,
                Weight = weight,
                ShippingAddress = addressShipping,
                Recipient = user,
                Status=StatusType.Pending,
            };
            _pandaContext.Packages.Add(package);
            await _pandaContext.SaveChangesAsync();
            return package;
        }
    }
}
