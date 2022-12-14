namespace currus.Models.DTOs
{
    public class TripAddDto
    {
        public double SLatitude { get; set; } // S - start
        public double SLongitude { get; set; }
        public double DLatitude { get; set; } // D - destination
        public double DLongitude { get; set; }
        public string StartingPoint { get; set; } = "";
        public string Destination { get; set; } = "";
        public decimal EstimatedTripPrice { get; set; }
        public int Seats { get; set; }
        public DateTime TripDate { get; set; }

        public TripAddDto(double sLatitude, double sLongitude, double dLatitude, 
            double dLongitude, string startingPoint, string destination,
            decimal estimatedTripPrice, int seats, DateTime tripDate)
        {
            SLatitude = sLatitude;
            SLongitude = sLongitude;
            DLatitude = dLatitude;
            DLongitude = dLongitude;
            StartingPoint = startingPoint;
            Destination = destination;
            EstimatedTripPrice = estimatedTripPrice;
            Seats = seats;
            TripDate = tripDate;
        }
    }
}
