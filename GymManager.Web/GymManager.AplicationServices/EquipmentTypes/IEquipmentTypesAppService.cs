using GymManager.Core.EquipmentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager.AplicationServices.EquipmentTypes
{
    public interface IEquipmentTypesAppService
    {
        Task<List<EquipmentType>> GetEquipmentTypesAsync();

        Task<int> AddEquipmentTypesAsync(EquipmentType equipmentType);

        Task DeleteEquipmentTypesAsync(int equipmentTypeId);

        Task<EquipmentType> GetEquipmentTypeAsync(int equipmentTypeId);

        Task EditEquipmentTypesAsync(EquipmentType equipmentType);
    }
}
