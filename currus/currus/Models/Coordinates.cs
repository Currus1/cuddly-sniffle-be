using System.Text.Json.Serialization;

namespace currus.Models;

public struct Coordinates
{
    [JsonInclude]
    public double lat;
    [JsonInclude]
    public double lng;

    public Coordinates(double lat, double lng)
    {
        this.lat = lat;
        this.lng = lng;
    }
}