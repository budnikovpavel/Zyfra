using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebZyfra.Models;
using Zyfra.DbModel;
using Zyfra.Models;
using Zyfra.Repositories.Interfaces;

namespace Zyfra.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IDirectionsRepository _directionRepository;
        public HomeController(ILogger<HomeController> logger, IDirectionsRepository directionRepository)
        {
            _logger = logger;
            _directionRepository = directionRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> LoadDirections()
        {
            IList<TreeViewNode> nodes = new List<TreeViewNode>();
            var directions = await _directionRepository.GetAllAsync();
            foreach (var item in directions)
            {
                var parent = await _directionRepository.GetAsync(item.ParentDirectionId);
                nodes.Add(new TreeViewNode { id = item.Id.ToString(), parent = parent != null ? parent.Id.ToString() : "#", text = item.Name });
            }

            //сериализуем в JSON

            return Json(new { jsonvar = JsonConvert.SerializeObject(nodes) });
        }
        [HttpGet]
        public IActionResult CreateDirection()=>View(new Direction());
        
        // POST: Home/CreateDirection
        [HttpPost]
        public async Task<IActionResult> CreateDirection([Bind("Id,Name,DateCreated")] Direction direction)
        {
            if (ModelState.IsValid)
            {
               await _directionRepository.CreateAsync(direction);
                return RedirectToAction(nameof(Index)); 
            }
            return View(direction);
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
