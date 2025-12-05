using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panda.Models;
using Panda.Services;
using System.Threading.Tasks;

namespace Panda.Controllers
{
    public class ReceiptsController : Controller
    {
        private readonly ReceiptsService _receiptsService;
        public ReceiptsController(ReceiptsService receiptsService)
        {
            _receiptsService = receiptsService;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            List<Receipt> receipts = await _receiptsService.GetReceiptsForUser();
            return View(receipts);
        }
        [Authorize]
        public async Task<IActionResult> OnAcquire(int packageId) 
        {
            await _receiptsService.CreateReceipt(packageId);
            return Redirect("/Home/Index");
        }
        public async Task<IActionResult> Details(int id) 
        {
            Receipt receipt = await _receiptsService.GetReceiptById(id);
            return View(receipt);
        }
    }
}
