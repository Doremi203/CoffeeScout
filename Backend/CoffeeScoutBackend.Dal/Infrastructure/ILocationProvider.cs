using NetTopologySuite.Geometries;
using Location = CoffeeScoutBackend.Domain.Models.Location;

namespace CoffeeScoutBackend.Dal.Infrastructure;

public interface ILocationProvider
{
    Point CreatePoint(double latitude, double longitude);
    Geometry CreateArea(Location location, double radiusInMetres);
}