using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Udem.LlamaClothingCo.Entities;

namespace ClothingStoreWeb.Models
{
    /// <summary>
    /// This class contains the information to return the user back to continue shopping.
    /// </summary>
    public class CartIndexViewModel
    {
        public Sale Cart { get; set; }

        public string ReturnUrl { get; set; }
    }
}