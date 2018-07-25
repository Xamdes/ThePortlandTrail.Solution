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
        [HttpGet("/Player/Home")]
        public ActionResult PlayerHome()
        {
            return View("Index", Player.GetAll());
        }

        [HttpPost("/Player/Home")]
        public ActionResult NewPlayer()
        {
            Player newPlayer = new Player(Request.Form["new-name"]);
            newPlayer.Save();
            return RedirectToAction("PlayerHome");
        }
        [HttpGet("/Player/{id}/Actions")]
        public ActionResult Actions()
        {
            return View();
        }
        [HttpPost("/Player/{id}/Food")]
        public ActionResult Food(int id)
        {
            Player thisPlayer = Player.Find(id);
            thisPlayer.GiveFood();
            return RedirectToAction("Actions");
        }
        [HttpPost("/Player/{id}/Fix")]
        public ActionResult Fix(int id)
        {
            Player thisPlayer = Player.Find(id);
            thisPlayer.GiveFix();
            return RedirectToAction("Actions");
        }
        [HttpPost("/Player/{id}/Rest")]
        public ActionResult Rest(int id)
        {
            Player thisPlayer = Player.Find(id);
            thisPlayer.GiveRest();
            return RedirectToAction("Actions");
        }
        [HttpGet("Player/{id}/Delete")]
        public ActionResult DeletePlayer(int id)
        {
            Player thisPlayer = Player.Find(id);
            thisPlayer.Delete();
            return RedirectToAction("Index");
        }

    }
}