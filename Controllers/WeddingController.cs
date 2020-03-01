using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WP.Models;

namespace WP.Controllers
{
    public class WeddingController : Controller
    {
        private MyContext dbContext;
        private int? _uid
        {
            get { return HttpContext.Session.GetInt32("UserId"); }
        }

        public WeddingController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("Wedding/Dashboard")]
        public IActionResult Dashboard()
        {
            var uid = _uid;
            if (_uid != null)
            {
                var allInfo = dbContext.Wedding.Include(guest => guest.Guests).ThenInclude(u => u.user).ToList();
                User user = dbContext.Users.FirstOrDefault(u => u.UserId == _uid);
                ViewBag.user = user;
                return View("Dashboard", allInfo);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Wedding/create")]
        public IActionResult create()
        {

            if (_uid == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost("Wedding/AddNewWedding")]
        public IActionResult AddNewWedding(Wedding newWedding)
        {
            if (ModelState.IsValid)
            {
                User user = dbContext.Users.FirstOrDefault(u => u.UserId == _uid);
                newWedding.Creator = user;
                newWedding.CreatorId = user.UserId;
                dbContext.Add(newWedding);
                dbContext.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            return View("create");
        }

        [HttpGet("Wedding/delete/{weddingId}")]
        public IActionResult delete(int weddingId)
        {
            if (_uid == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Wedding wedding = dbContext.Wedding.FirstOrDefault(w => w.WeddingId == weddingId);
            dbContext.Remove(wedding);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet("Wedding/rsvp/{weddingId}")]
        public IActionResult rsvp(int weddingId)
        {
            if (_uid == null)
            {
                return RedirectToAction("Index", "Home");
            }
            User user = dbContext.Users.FirstOrDefault(u => u.UserId == _uid);
            Wedding wedding = dbContext.Wedding.FirstOrDefault(w => w.WeddingId == weddingId);
            RSVP rsvp = new RSVP();
            rsvp.WeddingId = wedding.WeddingId;
            rsvp.UserId = user.UserId;
            dbContext.RSVP.Add(rsvp);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet("Wedding/unrsvp/{weddingId}")]
        public IActionResult unrsvp(int weddingId){
            if (_uid == null)
            {
                return RedirectToAction("Index", "Home");
            }
            RSVP rsvp = dbContext.RSVP.Include(a => a.wedding).FirstOrDefault(d => d.UserId==_uid);
            dbContext.RSVP.Remove(rsvp);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        
    }
}