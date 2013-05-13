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
        static TestContext context = (TestContext) System.Web.HttpContext.Current.ApplicationInstance.Application["Context"];
        protected ItemLogic itemLogic= new ItemLogic(context);
        protected ClientLogic clientLogic = new ClientLogic(context);
        protected SaleLogic saleLogic = new SaleLogic(context);
        private readonly List<CartLine> _items = new List<CartLine>();


        [HttpPost]
        public ViewResult Checkout(Cart cart)
        {
            var client = clientLogic.GetClientByEmail(User.Identity.Name);
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty.");
            }

            if (ModelState.IsValid)
            {
                
                Sale sale= Sale.CreateSale(0,client.ClientId,cart.ComputeTotalValue(),DateTime.Today, client.ShippingAddressId);
                /*
                sale.Client = client;
                sale.ShippingAddress=client.ShippingAddress;
                sale.SaleTotal=cart.ComputeTotalValue();
                sale.Date = DateTime.Today;*/
                saleLogic.AddSale(sale);
                ICollection<SaleDetail> icollection = new List<SaleDetail>();
                
                foreach (var line in cart.Lines)
                {
                    SaleDetail saledetail = SaleDetail.CreateSaleDetail(sale.SaleId,line.Product.ItemId, line.Quantity);
                    saledetail.Item = line.Product;
                    saledetail.Quantity = line.Quantity;
                    icollection.Add(saledetail);
                }
               
                saleLogic.AddSale(icollection);
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
