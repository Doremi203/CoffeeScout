using NetTopologySuite.Geometries;

namespace CoffeeScoutBackend.Dal.Infrastructure;

public interface ILocationProvider
{
    Point CreatePoint(double latitude, double longitude);
}