using System.Text.Json.Serialization;

namespace currus.Models;

public struct Coordinates
{
    [JsonInclude]
    public double Latitude { get; set; }
    [JsonInclude]
    public double Longitude { get; set; }

    public Coordinates(double latitude, double longitude)
    {
        this.Latitude = latitude;
        this.Longitude = longitude;
    }
}