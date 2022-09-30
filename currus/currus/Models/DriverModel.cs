using currus.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace currus.Models
{
    public class DriverModel
    {
        public string name { get; set; }
        public string surname { get; set; }
        public DateTime birthday { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string licenseNumber { get; set; }
        public VehicleTypeEnum Vehicles { get; set; }
        //public List<SelectListItem> VehicleTypes { get; set; }

        public DriverModel()
        {
            /*VehicleTypes = new List<SelectListItem>();

            // <options value="0">name</options> VALUE REPRESENTED BY INTEGERS
            // HARD CODED, PAKEISIU VELIAU KAD BUTU SU LOOPAIS
            // 
            VehicleTypes.Add(new SelectListItem
            {
                Value = ((int)VehicleTypeEnum.SEDAN).ToString(),
                Text = VehicleTypeEnum.SEDAN.ToString()
            });

            VehicleTypes.Add(new SelectListItem
            {
                Value = ((int)VehicleTypeEnum.EV).ToString(),
                Text = VehicleTypeEnum.EV.ToString()
            });

            VehicleTypes.Add(new SelectListItem
            {
                Value = ((int)VehicleTypeEnum.SUV).ToString(),
                Text = VehicleTypeEnum.SUV.ToString()
            });

            VehicleTypes.Add(new SelectListItem
            {
                Value = ((int)VehicleTypeEnum.MINIVAN).ToString(),
                Text = VehicleTypeEnum.MINIVAN.ToString()
            });

            VehicleTypes.Add(new SelectListItem
            {
                Value = ((int)VehicleTypeEnum.VAN).ToString(),
                Text = VehicleTypeEnum.VAN.ToString()
            });*/
        }
    }
}
