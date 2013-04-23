using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Udem.LlamaClothingCo.Entities;
using ClothingStoreWeb.Models;


namespace ClothingStoreWeb.Views.Cart
{
    public class CartController : Controller
    {
        //
        // GET: /Cart/

        public ActionResult Index()
        {
            return View();
        }

        //public ViewResult Index(Sale cart, string returnUrl)
        //{
        //    var cartIndexViewModel = new CartIndexViewModel()
        //    {
        //        Cart = cart,
        //        ReturnUrl = returnUrl
        //    };

        //    return View(cartIndexViewModel);
        //}

    }
}
