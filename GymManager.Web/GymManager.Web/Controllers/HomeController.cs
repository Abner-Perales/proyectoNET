using GymManager.AplicationServices.Members;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymManager.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly IMembersAppService _membersAppService;
        public HomeController(IMembersAppService membersAppService)
        {
            _membersAppService = membersAppService;
        }


        public IActionResult Index()
        {
            //var members = _membersAppService.GetMembers();

            return View();
        }
    }
}
