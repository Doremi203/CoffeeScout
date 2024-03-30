using NetTopologySuite.Geometries;

namespace CoffeeScoutBackend.Dal;

public interface ILocationProvider
{
    Point CreatePoint(double latitude, double longitude);
}