using System.Text.Json.Serialization;

namespace currus.Models;

public struct Coordinates
{
    [JsonInclude]
    public double lat { get; set; }
    [JsonInclude]
    public double lng { get; set; }

    public Coordinates(double lat, double lng)
    {
        this.lat = lat;
        this.lng = lng;
    }
}