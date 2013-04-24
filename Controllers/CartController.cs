using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Udem.LlamaClothingCo.Entities;
using ClothingStoreWeb.Models;
using Udem.LlamaClothingCo.Business;


namespace ClothingStoreWeb.Views.Cart
{
    public class CartController : Controller
    {
        //
        // GET: /Cart/
        protected ItemLogic itemLogic = new ItemLogic();

        public ViewResult Index()
        {
            //var item = itemLogic.GetItemByID(idItem);

            return View();
        }

        //public ViewResult Index()
        //{
        //    var item = itemLogic.GetItemByID(idItem);

        //    return View(item);
        //}

    }
}
