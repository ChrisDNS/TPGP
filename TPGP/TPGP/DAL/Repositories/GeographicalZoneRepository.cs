using TPGP.Context;
using TPGP.DAL.Interfaces;
using TPGP.Models.Jobs;

namespace TPGP.DAL.Repositories
{
    public class GeographicalZoneRepository : Repository<GeographicalZone>, IGeographicalZoneRepository
    {
        public GeographicalZoneRepository(TPGPContext ctx) : base(ctx)
        {
        }
    }
}