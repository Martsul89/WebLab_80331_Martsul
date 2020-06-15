using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LR1Project.Extensions;
using LR1Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LR1Project
{
    public class CartViewComponent : ViewComponent
    {
        private Cart _cart;
        public CartViewComponent(Cart cart)
        {
            _cart = cart;
        }
        public IViewComponentResult Invoke()
        {
           // var cart = HttpContext.Session.Get<Cart>("cart") ?? new Cart();
            return View(_cart);
        }
    }
}
