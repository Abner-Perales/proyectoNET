using GymManager.AplicationServices.Attendance;
using GymManager.AplicationServices.Members;
using GymManager.Core.Attendance;
using GymManager.Core.Members;
using GymManager.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymManager.Web.Controllers
{
    [Authorize]
    public class AttendanceController : Controller
    {
        private readonly IAttendanceAppService _attendanceAppService;
        private readonly IMembersAppService _membersAppService;
        public AttendanceController(IAttendanceAppService attendanceAppService,
            IMembersAppService membersAppService)
        {
            _attendanceAppService = attendanceAppService;
            _membersAppService = membersAppService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MemberIn()
        {
            AttendanceViewModel viewModel = new AttendanceViewModel
            {
                Message = "",
                CheckType = 1
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> MemberIn(AttendanceViewModel viewModel)
        {
            var checkType = "";
            if (viewModel.CheckType == 1)
            {
                checkType = "CheckIn";
            } else
            {
                checkType = "CheckOut";
            }

            Member member = await _membersAppService.GetMemberAsync(viewModel.IdMember);

            if (member == null)
            {
                viewModel.Message = "Miembro inválido, ingrese nuevamente";
                return View(viewModel);
            }

            Check check = new Check
            {
                DateCheck = DateTime.Now,
                CheckType = checkType,
                Member = member
            };

            await _attendanceAppService.AddCheckAsync(check);
            member.Checks.Add(check);                 
            

            return RedirectToAction("MemberIn");
        }

        public IActionResult MemberOut()
        {
            AttendanceViewModel viewModel = new AttendanceViewModel
            {
                Message = "",
                CheckType = 2
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> MemberOut(AttendanceViewModel viewModel)
        {
            var checkType = "";
            if (viewModel.CheckType == 1)
            {
                checkType = "CheckIn";
            }
            else
            {
                checkType = "CheckOut";
            }

            Member member = await _membersAppService.GetMemberAsync(viewModel.IdMember);

            if (member == null)
            {
                viewModel.Message = "Miembro inválido, ingrese nuevamente";
                return View(viewModel);
            }

            Check check = new Check
            {
                DateCheck = DateTime.Now,
                CheckType = checkType,
                Member = member
            };

            await _attendanceAppService.AddCheckAsync(check);
            member.Checks.Add(check);


            return RedirectToAction("MemberOut");
        }
    }
}
