using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Udem.LlamaClothingCo.Business;
using Udem.LlamaClothingCo.Entities;

namespace ClothingStoreWeb.Controllers
{
    public class SaleController : Controller
    {
        //
        // GET: /Sale/
        protected SaleLogic saleLogic = new SaleLogic();
        protected ClientLogic clientLogic = new ClientLogic();
       
        public ActionResult Index(int id)
        {
            ViewBag.Message = "My Sales";
            // Retrieve Genre and its Associated items from database
            var sales = saleLogic.GetSalesOfAClient(clientLogic.GetClientByID(id));
            return View(sales);
        }

        //
        // GET: /Sale/Details/5

        public ActionResult Details(int id)
        {
            var details = saleLogic.GetDetailsofaSale(saleLogic.GetSaleByID(id));
            return View(details);
        }

        //
        // GET: /Sale/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Sale/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Sale/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Sale/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Sale/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Sale/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public RedirectToRouteResult RemoveFromSales(Sale sale, string returnUrl)
        {
            saleLogic.DeleteSale(sale);
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}
