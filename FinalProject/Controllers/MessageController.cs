using Identity.DAL.Entities;
using FinalProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Identity.DAL.Context;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.Controllers
{
    
    public class MessageController : Controller
    {
        private readonly ILogger<MessageController> _logger;
        private readonly ISendMessageService _messageService;
        private readonly UserManager<User> userManager;

        public MessageController(ILogger<MessageController> logger, ISendMessageService messageService, UserManager<User> userManager)
        {
            _logger = logger;            
            _messageService = messageService;
            this.userManager = userManager;
        }
        
        public async Task<IActionResult> SendMessage(string userName) 
        {
            var temp = await userManager.FindByNameAsync(userName);
            
            await _messageService.SendReportAsync(temp);

            _logger.LogInformation($"{DateTime.UtcNow} Report send");

            return View("SendMessage");
        }
    }
}
