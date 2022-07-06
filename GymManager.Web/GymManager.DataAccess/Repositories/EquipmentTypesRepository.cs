﻿using GymManager.Core.EquipmentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager.DataAccess.Repositories
{
    public class EquipmentTypesRepository : Repository<int, EquipmentType>
    {
        public EquipmentTypesRepository(GymManagerContext context) : base(context)
        {
            
        }
    }
}
