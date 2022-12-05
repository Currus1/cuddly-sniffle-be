﻿using currus.Models;
using Microsoft.VisualBasic;

namespace currus.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            //InitializeUser(context);
            InitializeTrip(context);
            context.SaveChanges();
        }

        public static void InitializeUser(ApplicationDbContext context)
        {
            if (context.User.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
                new User { UserName = "Abel", Surname = "Morton", Birthdate = new DateTime(), Email = "AbelMorton@gmail.com", PhoneNumber = "868686861"},
                new User { UserName = "Fox", Surname = "Stevens", Birthdate = new DateTime(), Email = "FoxStevens@gmail.com", PhoneNumber = "868686862"},
                new User { UserName = "Ira", Surname = "Taylor", Birthdate = new DateTime(), Email = "IraTaylort@gmail.com", PhoneNumber = "868686863"},
            };

            context.User.AddRange(users);
        }

    

        public static void InitializeTrip(ApplicationDbContext context)
        {
            if (context.Trip.Any())
            {
                return;   // DB has been seeded
            }
        
            var trips = new Trip[]
            {
                new Trip {Seats = 4, StartingPoint = "Vilnius", Latitude = 54.6872, Longitude = 25.2797,
                    Destination = "Kaunas", Hours = 1, Minutes = 2, Distance = 100, VehicleType = "EV", TripStatus = "Planned"},
                new Trip {Seats = 2, StartingPoint = "Vilnius", Latitude = 54.6760, Longitude = 25.2738,
                    Destination = "Klaipeda", Hours = 3, Minutes = 6, Distance = 306, VehicleType = "Sedan", TripStatus = "Planned"},
                new Trip {Seats = 4, StartingPoint = "Vilnius", Latitude = 54.6861, Longitude = 25.2845,
                    Destination = "Vyziniai", Hours = 1, Minutes = 3, Distance = 79.3M, VehicleType = "Van", TripStatus = "Ended"},
            };

            context.Trip.AddRange(trips);
        }
    }
}
