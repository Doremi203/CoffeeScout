using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace CoffeeScoutBackend.Dal;

public class GpsLocationProvider : ILocationProvider
{
    private readonly GeometryFactory _geometryFactory = 
        NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
    
    public Point CreatePoint(double latitude, double longitude)
    {
        return _geometryFactory.CreatePoint(new Coordinate(longitude, latitude));
    }
}