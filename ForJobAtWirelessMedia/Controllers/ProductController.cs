using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ForJobAtWirelessMedia.Models;
using System.Data.Entity;
using System.IO;
using Newtonsoft.Json;

namespace ForJobAtWirelessMedia.Controllers
{
    public class ProductController : Controller
    {
         WirelessMediaDBModel db = new WirelessMediaDBModel();
        // GET: Product
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }
        [HttpGet]
       public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpGet]
        public ActionResult Edit(int Id = 0)
        {
            Product product = db.Products.Find(Id);
            if(product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(product);
        }


    }
}