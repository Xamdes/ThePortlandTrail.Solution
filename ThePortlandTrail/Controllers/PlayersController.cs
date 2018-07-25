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
<<<<<<< HEAD
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
=======

        [HttpGet("player/{id}/delete")]
>>>>>>> d4fe753961604dcfcc9edbc66683d19aee0eef00
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
