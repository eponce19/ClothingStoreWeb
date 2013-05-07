using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Udem.LlamaClothingCo.Entities;
using ClothingStoreWeb.Models;
using Udem.LlamaClothingCo.Business;
using ClothingStoreWeb.Interfaces;


namespace ClothingStoreWeb.Controllers
{
    public class CartController : Controller
    {
        protected ItemLogic itemLogic= new ItemLogic();
        protected ClientLogic clientLogic = new ClientLogic();
        protected SaleLogic saleLogic = new SaleLogic();
        private readonly List<CartLine> _items = new List<CartLine>();


        [HttpPost]
        public ViewResult Checkout(Cart cart, Client client)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty.");
            }

            if (ModelState.IsValid)
            {
                
                Sale sale=new Sale();
                sale.Client=client;
                sale.ShippingAddress=client.ShippingAddress;
                sale.SaleTotal=cart.ComputeTotalValue();
                sale.Date = DateTime.Today;
                ICollection<SaleDetail> icollection = new ICollection<SaleDetail>();
                
                foreach (var line in cart.Lines)
                {
                    SaleDetail saledetail = new SaleDetail();
                    saledetail.Item = line.Product;
                    saledetail.Quantity = line.Quantity;
                    icollection.Add(saledetail);
                }
                saleLogic.AddSale(sale, icollection);
                cart.Clear();
                return View("Completed");
            }

            return View(client);
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            var cartIndexViewModel = new CartIndexViewModel()
            {
                Cart = cart,
                ReturnUrl = returnUrl
            };

            return View(cartIndexViewModel);
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            
            var product = itemLogic.GetItemByID(productId);

            if (product != null)
            {
                cart.AddItem(product, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }


        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            var productLine = cart.Lines
                .FirstOrDefault(prod => prod.Product.ItemId == productId);

            if (productLine != null)
            {
                cart.RemoveItem(productLine.Product);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public ViewResult Summary(Cart cart)
        {
            return View(cart);
        }

        public ViewResult Checkout()
        {
            var client = clientLogic.GetClientByEmail(User.Identity.Name);
            return View(client);
        }
    }
}
