using GymManager.AplicationServices.MembershipTypes;
using GymManager.Core.MembershipTypes;
using GymManager.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymManager.Web.Controllers
{
    public class MembershipTypesController : Controller
    {
        private readonly IMembershipTypesAppService _membershipTypesAppService;
        public MembershipTypesController(IMembershipTypesAppService membershipTypesAppService)
        {
            _membershipTypesAppService = membershipTypesAppService;
        }

        public async Task<IActionResult> Index()
        {

            List<MembershipType> membershipTypes = await _membershipTypesAppService.GetMembershipTypesAsync();

            MembershipTypeListViewModel viewModel = new MembershipTypeListViewModel();

            viewModel.NewMembershipTypesCount = membershipTypes.Count;
            viewModel.MembershipTypes = membershipTypes;

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Delete(int membershipTypeId)
        {
            await _membershipTypesAppService.DeleteMembershipTypeAsync(membershipTypeId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int membershipTypeId)
        {
            MembershipType membershipType = await _membershipTypesAppService.GetMembershipTypeAsync(membershipTypeId);

            MembershipTypesViewModel viewModel = new MembershipTypesViewModel
            {
                Id = membershipType.Id,
                Name = membershipType.Name,
                Cost = membershipType.Cost,
                Duration = membershipType.Duration
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MembershipTypesViewModel viewModel)
        {
            MembershipType membershipType = new MembershipType
            {
                Name = viewModel.Name,
                Cost = viewModel.Cost,
                CreatedOn = DateTime.Now,
                Duration = viewModel.Duration
            };

            await _membershipTypesAppService.AddMembershipTypeAsync(membershipType);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MembershipType membershipType)
        {
            await _membershipTypesAppService.EditMembershipTypeAsync(membershipType);
            return RedirectToAction("Index");
        }
    }
}
