using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMS.WebApi.Controllers
{
    public class InbillController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
