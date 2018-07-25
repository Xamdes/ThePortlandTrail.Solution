using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThePortlandTrail;
using ThePortlandTrail.Models;

namespace ThePortlandTrail.Controllers
{
    public class PlayersController : Controller
    {
        [HttpGet("/players")]
        public ActionResult PlayerHome()
        {
            return View("Index", Player.GetAll());
        }

        [HttpPost("/players")]
        public ActionResult NewPlayer(string name)
        {
            Player newPlayer = new Player(name);
            newPlayer.Save();
            return RedirectToAction("PlayerHome");
        }

        [HttpGet("/players/create")]
        public ActionResult CreatePlayer()
        {
            return View();
        }

        [HttpGet("/players/{id}/actions")]
        public ActionResult Actions()
        {
            return View();
        }

        [HttpPost("players/{id}/delete")]
        public ActionResult DeletePlayer(int id)
        {
            Player.Delete(id);
            return RedirectToAction("PlayerHome");
        }

        [HttpGet("/players/{id}/details")]
        public ActionResult Details(int id)
        {
            return View(Player.Find(id));
        }

    }
}
