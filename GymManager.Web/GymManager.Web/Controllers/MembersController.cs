using GymManager.AplicationServices.Members;
using GymManager.AplicationServices.MembershipTypes;
using GymManager.Core.Members;
using GymManager.Core.MembershipTypes;
using GymManager.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymManager.Web.Controllers
{
    [Authorize]
    public class MembersController : Controller
    {
        private readonly IMembersAppService _membersAppService;
        private readonly IMembershipTypesAppService _membershipTypesAppService;
        private readonly ILogger _logger;
        public MembersController(IMembersAppService membersAppService, 
            IMembershipTypesAppService membershipTypesAppService,
            ILogger logger)
        {
            _membershipTypesAppService = membershipTypesAppService;
            _membersAppService = membersAppService;
            _logger = logger;
        }


        public async Task<IActionResult> Index()
        {

            List<Member> members = await _membersAppService.GetMembersAsync();

            MemberListViewModel viewModel = new MemberListViewModel();

            viewModel.NewMembersCount = 2;
            viewModel.Members = members;

            _logger.Information("Index in Members executed");

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Delete(int memberId)
        {
            await _membersAppService.DeleteMemberAsync(memberId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int memberId)
        {

            Member member = await _membersAppService.GetMemberAsync(memberId);

            MemberViewModel viewModel = new MemberViewModel
            {
                AllowNewsletter = member.AllowNewsLetter,
                BirthDay = member.BirthDay,
                CityId = member.City.Id,
                Email = member.Email,
                Id = member.Id,
                LastName = member.LastName,
                Name = member.Name
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MemberViewModel viewModel)
        {
            Member member = new Member
            {
                Name = viewModel.Name,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                City = new City
                {
                    Id = viewModel.CityId
                },
                BirthDay = viewModel.BirthDay,
                DateRegistration = DateTime.Now,
                AllowNewsLetter = viewModel.AllowNewsletter
            };

            await _membersAppService.AddMemberAsync(member);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Member member)
        {
            await _membersAppService.EditMemberAsync(member);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Renewal()
        {
            List<Member> members = await _membersAppService.GetMembersAsync();

            MemberListViewModel viewModel = new MemberListViewModel();

            viewModel.Members = members;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Renewal(string filter)
        {
            List<Member> members = await _membersAppService.GetMembersFilterAsync(filter);

            MemberListViewModel viewModel = new MemberListViewModel();

            viewModel.Members = members;

            return View(viewModel);
        }

        public async Task<IActionResult> EditRenewal(int memberId)
        {
            Member member = await _membersAppService.GetMemberAsync(memberId);
            List<MembershipType> membershipTypes = await _membershipTypesAppService.GetMembershipTypesAsync();

            RenewalViewModel viewModel = new RenewalViewModel
            {
                IdMember = memberId,
                Member = member,
                MembershipTypes = membershipTypes
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditRenewal(RenewalViewModel renewalViewModel)
        {
            Member member = await _membersAppService.GetMemberAsync(renewalViewModel.IdMember);
            MembershipType membershipType = await _membershipTypesAppService.GetMembershipTypeAsync(renewalViewModel.IdMembershipType);
            member.MembershipType = membershipType;
            membershipType.Members.Add(member);
            member.DateBeginMembership = DateTime.Now;

            await _membersAppService.EditMemberAsync(member);
            await _membershipTypesAppService.EditMembershipTypeAsync(membershipType);
            return RedirectToAction("Renewal");
        }


        public IActionResult Test()
        {
            return View();
        }
    }
}
