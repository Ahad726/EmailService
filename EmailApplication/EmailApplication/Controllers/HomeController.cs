using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmailApplication.Models;
using EmailService;

namespace EmailApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IEmailSender _emailSender;
        private readonly IViewRenderService _viewRenderService;

        public HomeController(ILogger<HomeController> logger, IEmailSender emailSender, IViewRenderService viewRenderService)
        {
            _logger = logger;
            _emailSender = emailSender;
            _viewRenderService = viewRenderService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var emailModel = new WelcomeEmailModel("Mr. Teammate");
            var emailBody = await _viewRenderService.RenderToStringAsync("Email/Welcome", emailModel);
            _emailSender.SendEmail(new List<Message>
            {
                new Message
                {
                    Receiver = "Receiver Mail",
                    Subject = "Stay Home during pandemic",
                    Body = emailBody
                }
            });
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
