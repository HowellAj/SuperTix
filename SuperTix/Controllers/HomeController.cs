using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SuperTix.Models;


namespace SuperTix.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //Create list for tickets.. myabe change to events later
            List<Ticket> tickets = new List<Ticket>();

            Ticket HockeyGame = new Ticket();
            HockeyGame.TicketID = 1;
            HockeyGame.EventName = "HOCKEY HOCKEY HOCKEY";
            HockeyGame.SeatNumber = "34A";
            HockeyGame.EventDate = new DateTime(2025, 9, 19, 17, 0, 0); //Sep 19th 5pm //for current time DateTime.now



            Ticket FootballGame = new Ticket();
            FootballGame.TicketID = 2;
            FootballGame.EventName = "FOOOOOOOOOOTBALLLLLLLLL";
            FootballGame.SeatNumber = "12C";
            FootballGame.EventDate = new DateTime(2025, 9, 19, 17, 0, 0); //Sep 19th 5pm //for current time DateTime.now


            tickets.Add(HockeyGame);
            tickets.Add(FootballGame);

            _logger.Log(LogLevel.Information, "number of events: " +tickets.Count)

            return View();
        }

        public IActionResult Details(int id)
        {
            int photoId = id;

            return View();
        }


    }
}
