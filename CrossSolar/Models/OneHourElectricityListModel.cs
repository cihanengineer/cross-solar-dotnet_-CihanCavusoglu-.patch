﻿using System.Collections.Generic;

namespace CrossSolar.Models
{
    public class OneHourElectricityListModel
    {
        public List<OneHourElectricityModel> OneHourElectricitys { get; set; }
        public OneHourElectricityListModel()
        {
            OneHourElectricitys = new List<OneHourElectricityModel>();
        }
    }
}