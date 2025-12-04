using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Panda.Data;
using Panda.Models;

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
        public async Task<List<Package>> GetAllPackagesForUser(AppUser user) 
        {
            List<Package> packages = _pandaContext.Packages.Include(el=>el.Recipient).Where(el=>el.Recipient.Username==user.UserName).ToList();
            return packages;
        }
        public async Task<List<Package>> GetPendingPackages()
        {
            List<Package> packages = _pandaContext.Packages.Include(el => el.Recipient).Where(el => el.Status == StatusType.Pending).ToList();
            return packages;
        }
        public async Task<List<Package>> GetShippedPackages()
        {
            List<Package> packages = _pandaContext.Packages.Include(el => el.Recipient).Where(el => el.Status == StatusType.Shipped).ToList();
            return packages;
        }
        public async Task<List<Package>> GetDeliveredPackages()
        {
            List<Package> packages = _pandaContext.Packages.Include(el => el.Recipient).Where(el => el.Status == StatusType.Delivered).ToList();
            return packages;
        }
        public async Task ChangeToShipped(int id) 
        {
            Package package = await GetPackageById(id);
            package.Status = StatusType.Shipped;
            package.EstimateDeliveryDate = DateTime.Now;
            await _pandaContext.SaveChangesAsync();
        }
        public async Task ChangeToDelivered(int id)
        {
            Package package = await GetPackageById(id);
            package.Status = StatusType.Delivered;
            await _pandaContext.SaveChangesAsync();
        }
        public async Task<Package> GetPackageById(int id) 
        {
            Package package = await _pandaContext.Packages.Include(p => p.Recipient).FirstOrDefaultAsync(p => p.Id == id);
            return package;
        }
    }
}
