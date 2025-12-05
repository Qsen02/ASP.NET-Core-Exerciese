using Microsoft.EntityFrameworkCore;
using Panda.Data;
using Panda.Models;

namespace Panda.Services
{
    public class ReceiptsService
    {
        private readonly PandaContext _context;
        private readonly UserService _userService;
        public ReceiptsService(PandaContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }
        public async Task<Receipt> CreateReceipt(int packageId) 
        {
            Package package = await _context.Packages
                .Include(el=>el.Recipient)
                .FirstOrDefaultAsync(el=>el.Id == packageId);

            Receipt receipt = new Receipt() 
            {
                Recipient = package.Recipient,
                Package = package,
                IssuedOn = DateTime.UtcNow,
                Fee=package.Weight * 2.67
            };
            _context.Receipts.Add(receipt);
            package.Status = StatusType.Acquired;
            await _context.SaveChangesAsync();
            return receipt;
        }
        public async Task<List<Receipt>> GetReceiptsForUser() 
        {
            AppUser user = await _userService.GetCurrentUser();
            List<Receipt> receipts= await _context.Receipts
                .Include(el => el.Recipient)
                .Where(el => el.Recipient.Username == user.UserName)
                .ToListAsync();
            return receipts;
        }
        public async Task<Receipt> GetReceiptById(int id) 
        {
            Receipt receipt = await _context.Receipts
                .Include(el => el.Package)
                .Include(el => el.Recipient)
                .FirstOrDefaultAsync(el => el.Id == id); ;
            return receipt;
        }
    }
}
