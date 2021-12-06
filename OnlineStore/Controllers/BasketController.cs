using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace OnlineStore.WebUI.Controllers
{
    public class BasketController : Controller
    {
        // GET: BasketController
        [Authorize(Policy = "OnlyForAuthenticateUser")]
        public ActionResult Index()
        {
            return View();
        }

    }
    
}
