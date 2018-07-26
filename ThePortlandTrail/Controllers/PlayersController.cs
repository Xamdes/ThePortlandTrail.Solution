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
        [HttpGet("/players/home")]
        public ActionResult PlayerHome()
        {
            return View("Index", Player.GetAll());
        }

        [HttpPost("/players/home")]
        public ActionResult NewPlayer(string name)
        {
            Player newPlayer = new Player(name);
            newPlayer.Save();
            return RedirectToAction("PlayerHome");
        }

        [HttpGet("/players/{id}/actions")]
        public ActionResult Actions(int id)
        {
            Player thisPlayer = Player.Find(id);
            return View(thisPlayer);
        }
        [HttpPost("/players/{id}/food")]
        public ActionResult Food(int id)
        {
            Player thisPlayer = Player.Find(id);
            thisPlayer.GiveFood();
            thisPlayer.UpdatePlayerFood(thisPlayer.GetFood());
            return RedirectToAction("Actions", thisPlayer);
        }
        [HttpPost("/players/{id}/fix")]
        public ActionResult Fix(int id)
        {
            Player thisPlayer = Player.Find(id);
            thisPlayer.GiveFix();
            thisPlayer.UpdatePlayerFix(thisPlayer.GetFix());
            return RedirectToAction("Actions", thisPlayer);
        }
        [HttpPost("/players/{id}/rest")]
        public ActionResult Rest(int id)
        {
            Player thisPlayer = Player.Find(id);
            thisPlayer.GiveRest();
            thisPlayer.UpdatePlayerRest(thisPlayer.GetRest());
            return RedirectToAction("Actions", thisPlayer);
        }
        [HttpPost("/players/{id}/explore")]
        public ActionResult Explore(int id)
        {
            Player thisPlayer = Player.Find(id);
            thisPlayer.PassTime();
            thisPlayer.UpdatePlayerFix(thisPlayer.GetFix());
            thisPlayer.UpdatePlayerFood(thisPlayer.GetFood());
            thisPlayer.UpdatePlayerRest(thisPlayer.GetRest());
            return RedirectToAction("Actions", thisPlayer);
        }

        [HttpGet("players/{id}/delete")]
        public ActionResult DeletePlayer(int id)
        {
            Player thisPlayer = Player.Find(id);
            thisPlayer.Delete();
            return RedirectToAction("Index");
        }
        
        [HttpGet("/players/{id}/details")]
        public ActionResult Details(int id)
        {
            return View(Player.Find(id));
        }

    }
}
