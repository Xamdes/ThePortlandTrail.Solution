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
        [HttpGet("/player/home")]
        public ActionResult PlayerHome()
        {
            return View("Index", Player.GetAll());
        }

        [HttpPost("/player/home")]
        public ActionResult NewPlayer(string name)
        {
            Player newPlayer = new Player(name);
            newPlayer.Save();
            return RedirectToAction("PlayerHome");
        }

        [HttpGet("/player/{id}/actions")]
        public ActionResult Actions()
        {
            return View();
        }

        [HttpGet("player/{id}/delete")]
        public ActionResult DeletePlayer(int id)
        {
            Player thisPlayer = Player.Find(id);
            thisPlayer.Delete();
            return RedirectToAction("Index");
        }
        
        [HttpGet("/player/{id}/details")]
        public ActionResult Details(int id)
        {
            return View(Player.Find(id));
        }

    }
}
