﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthMVC.Models;

namespace NorthMVC.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View(new NorthwindEntities().Products.ToList());
        }
        public ActionResult Detay(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");
            NorthwindEntities db = new NorthwindEntities();
            var kategoriler = new List<SelectListItem>();
            kategoriler.Add(new SelectListItem()
            {
                Text = "Kategorisi Yok",
                Value = "null"
            });
            db.Categories.ToList().ForEach(x =>
                kategoriler.Add(new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.CategoryID.ToString()
                })
            );
            ViewBag.Kategoriler = kategoriler;


            return View(db.Products.Find(id));
        }
        [HttpPost]
        public ActionResult Duzenle(Product urun)
        {
            NorthwindEntities db = new NorthwindEntities();
            var guncellenecek = db.Products.Find(urun.ProductID);
            guncellenecek.ProductName = urun.ProductName;
            guncellenecek.UnitPrice = urun.UnitPrice;
            guncellenecek.QuantityPerUnit = urun.QuantityPerUnit;
            guncellenecek.UnitsInStock = urun.UnitsInStock;
            guncellenecek.CategoryID = urun.CategoryID;
            db.SaveChanges();
            return RedirectToAction("Detay", new { id = urun.ProductID });
        }
    }
}