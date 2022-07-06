using GymManager.Core.EquipmentTypes;
using GymManager.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager.AplicationServices.EquipmentTypes
{
    public class EquipmentTypesAppService : IEquipmentTypesAppService
    {
        private readonly IRepository<int, EquipmentType> _repository;

        public EquipmentTypesAppService(IRepository<int, EquipmentType> repository)
        {
            _repository = repository;
        }

        public async Task<int> AddEquipmentTypesAsync(EquipmentType equipmentType)
        {
            await _repository.AddAsync(equipmentType);

            return equipmentType.Id;
        }

        public async Task DeleteEquipmentTypesAsync(int equipmentTypeId)
        {
            await _repository.DeleteAsync(equipmentTypeId);
        }

        public async Task EditEquipmentTypesAsync(EquipmentType equipmentType)
        {
            await _repository.UpdateAsync(equipmentType);
        }

        public async Task<EquipmentType> GetEquipmentTypeAsync(int equipmentTypeId)
        {
            return await _repository.GetAsync(equipmentTypeId);
        }

        public async Task<List<EquipmentType>> GetEquipmentTypesAsync()
        {
            return await _repository.GetAll().ToListAsync();
        }

        
    }
}
