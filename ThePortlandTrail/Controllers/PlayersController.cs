using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThePortlandTrail;
using ThePortlandTrail.Models;

namespace ThePortlandTrail.Controllers
{
    public class PlayersController : Controller {
        [HttpGet("/Player/Home")]
        public ActionResult PlayerHome(){
            {
                return View("Index", Player.GetAll());
            }
        }

           [HttpPost("/Player/Home")]
            public ActionResult NewPlayer()
            {
            Player newPlayer = new Player(Request.Form["new-name"]);
            newPlayer.Save();
            return RedirectToAction("PlayerHome");
            }
    }
}