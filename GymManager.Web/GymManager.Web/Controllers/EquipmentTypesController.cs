using GymManager.AplicationServices.EquipmentTypes;
using GymManager.Core.EquipmentTypes;
using GymManager.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymManager.Web.Controllers
{
    public class EquipmentTypesController : Controller
    {
        private readonly IEquipmentTypesAppService _equipmentTypesAppService;
        public EquipmentTypesController(IEquipmentTypesAppService equipmentTypesAppService)
        {
            _equipmentTypesAppService = equipmentTypesAppService;
        }

        public async Task<IActionResult> Index()
        {

            List<EquipmentType> equipmentTypes = await _equipmentTypesAppService.GetEquipmentTypesAsync();

            EquipmentTypeListViewModel viewModel = new EquipmentTypeListViewModel();

            viewModel.EquipmentTypesCount = equipmentTypes.Count;
            viewModel.EquipmentTypes = equipmentTypes;

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EquipmentTypeViewModel viewModel)
        {
            EquipmentType equipmentType = new EquipmentType
            {
                Name = viewModel.Name
            };

            await _equipmentTypesAppService.AddEquipmentTypesAsync(equipmentType);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int equipmentTypeId)
        {
            await _equipmentTypesAppService.DeleteEquipmentTypesAsync(equipmentTypeId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int equipmentTypeId)
        {
            EquipmentType equipmentType = await _equipmentTypesAppService.GetEquipmentTypeAsync(equipmentTypeId);

            EquipmentTypeViewModel viewModel = new EquipmentTypeViewModel
            {
                Id = equipmentType.Id,
                Name = equipmentType.Name
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EquipmentType equipmentType)
        {
            await _equipmentTypesAppService.EditEquipmentTypesAsync(equipmentType);
            return RedirectToAction("Index");
        }

    }
}
