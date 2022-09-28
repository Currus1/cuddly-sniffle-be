using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace currus.Models
{
    class TripModel
    {
        public static DriverModel Driver { get; set; } 

        public static List<UserModel> Commuters = new List<UserModel>();
        public int SeatCount { get; set; }
        public int CurrentSeatCount = 0;

        public TripModel(DriverModel driver, int seatCount) 
        {
            Driver = driver;
            SeatCount = seatCount;
        } 

        public void AddCommuter (UserModel commuter)
        {
            if (seatCount > CurrentSeatCount)
            {
                Commuters.Add(commuter);
            }
        }
    }
}
