namespace currus.Models;

public struct Coordinates
{
    public double lat, lng;

    public Coordinates(double lat, double lng)
    {
        this.lat = lat;
        this.lng = lng;
    }
}