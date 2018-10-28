using CrossSolar.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrossSolar.Repository
{
    public class DayAnalyticsRepository : GenericRepository<OneDayElectricityModel>, IDayAnalyticsRepository
    {
        public DayAnalyticsRepository(CrossSolarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<OneHourElectricity>> GetBySerialAsync(string panelId)
        {
            return await _dbContext.OneHourElectricitys
                 .Where(
                     e => e.PanelId == panelId)
                .ToListAsync();
        }

}
}