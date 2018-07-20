using ForJobAtWirelessMedia.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForJobAtWirelessMedia.Controllers
{
    public class JsonController : Controller
    {

        [HttpGet]
        public ActionResult Index()
        {
            List<Product> ProductList = new List<Product>();
            using (WirelessMediaDBModel db = new WirelessMediaDBModel())
            {
                ProductList = db.Products.ToList();
            }
            return View(ProductList);
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase jsonFile)
        {
            using (WirelessMediaDBModel db = new WirelessMediaDBModel())
            {
                if (!jsonFile.FileName.EndsWith(".json"))
                {
                    ViewBag.Error = "Invalid file type";
                }
                else
                {
                    jsonFile.SaveAs(Server.MapPath("~/JSON/" + Path.GetFileName(jsonFile.FileName)));
                    StreamReader streamReader = new StreamReader(Server.MapPath("~/JSON/" + Path.GetFileName(jsonFile.FileName)));
                    string data = streamReader.ReadToEnd();
                    List<Product> products = JsonConvert.DeserializeObject<List<Product>>(data);

                    products.ForEach(p => {
                        Product product = new Product()
                        {
                            Name = p.Name,
                            Description = p.Description,
                            Category = p.Category,
                            Manufacturer = p.Manufacturer,
                            Supplier = p.Supplier,
                            Price = p.Price,
                           
                        };
                        db.Products.Add(product);
                        db.SaveChanges();
                    });
                    ViewBag.Success = "Success";
                }
            }
            return View("Index");
        }
    }
}  
    
