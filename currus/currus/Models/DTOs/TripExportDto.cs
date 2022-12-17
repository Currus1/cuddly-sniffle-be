namespace currus.Models.DTOs
{
    public class TripExportDto
    {
        public int Id { get; set; }
        public double SLatitude { get; set; } // S - start
        public double SLongitude { get; set; }
        public double DLatitude { get; set; } // D - destination
        public double DLongitude { get; set; }
        public string StartingPoint { get; set; } = "";
        public string Destination { get; set; } = "";
        public int Occupied { get; set; }
        public int Seats { get; set; }
        public string DriverId { get; set; } = "";
        public string DriverName { get; set; }
        public string DriverSurname { get; set; }
        public string VehicleType { get; set; } = "";
        public decimal EstimatedTripPrice { get; set; }
        public string TripStatus { get; set; } = "";
        public DateTime TripDate { get; set; }

        public TripExportDto(int id, double sLatitude, double sLongitude, 
            double dLatitude, double dLongitude, string startingPoint, 
            string destination, int occupied, int seats, string driverId, 
            string driverName, string driverSurname, string vehicleType,
            decimal estimatedTripPrice, string tripStatus, DateTime tripDate)
        {
            Id = id;
            SLatitude = sLatitude;
            SLongitude = sLongitude;
            DLatitude = dLatitude;
            DLongitude = dLongitude;
            StartingPoint = startingPoint;
            Destination = destination;
            Occupied = occupied;
            Seats = seats;
            DriverId = driverId;
            DriverName = driverName;
            DriverSurname = driverSurname;
            VehicleType = vehicleType;
            EstimatedTripPrice = estimatedTripPrice;
            TripStatus = tripStatus;
            TripDate = tripDate;
        }
    }
}
