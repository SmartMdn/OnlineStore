using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.WebUI.Controllers
{
    public class BasketController : Controller
    {
        // GET: BasketController
        public ActionResult Index()
        {
            return View();
        }

    }
    
}
