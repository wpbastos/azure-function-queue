using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SendEmail.AzureQueueLibrary.Messages;
using SendEmail.AzureQueueLibrary.QueueConnection;
using SendEmail.WebApp.Models;

namespace SendEmail.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQueueCommunicator _queueCommunicator;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IQueueCommunicator queueCommunicator)
        {
            _logger = logger;
            _queueCommunicator = queueCommunicator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ContactUS()
        {
            ViewBag.Message = "";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ContactUS(string contactName, string emailAddress)
        {
            // Send Thank you e-mail to Contact
            var thankYouEmail = new SendEmailCommand()
            {
                To = emailAddress,
                Subject = "Thank you for reaching out!",
                Body = "We'll contact you shortly."
            };
            await _queueCommunicator.SendAsync(thankYouEmail);

            // Send New Contact e-mail to Admin
            var adminEmail = new SendEmailCommand()
            {
                To = "admin@test.com",
                Subject = "New contact!",
                Body = $"{contactName} has reached out via contact form. Please respond back at {emailAddress}."
            };
            await _queueCommunicator.SendAsync(adminEmail);

            ViewBag.Message = "Thank you we've received your message =)";
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}