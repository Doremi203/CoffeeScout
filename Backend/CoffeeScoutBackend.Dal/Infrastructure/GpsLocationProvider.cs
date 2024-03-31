using NetTopologySuite;
using NetTopologySuite.Geometries;
using Location = CoffeeScoutBackend.Domain.Models.Location;

namespace CoffeeScoutBackend.Dal.Infrastructure;

public class GpsLocationProvider : ILocationProvider
{
    private readonly GeometryFactory _geometryFactory =
        NtsGeometryServices.Instance.CreateGeometryFactory(4326);

    public Point CreatePoint(double latitude, double longitude)
    {
        return _geometryFactory.CreatePoint(new Coordinate(longitude, latitude));
    }

    public Geometry CreateArea(Location location, double radiusInMetres)
    {
        const double degreesToMeters = 111139.0;
        var degrees = radiusInMetres / degreesToMeters;
        var locationPoint = CreatePoint(location.Latitude, location.Longitude);
        var area = locationPoint.Buffer(degrees);
        area.SRID = 4326;
        
        return area;
    }
}