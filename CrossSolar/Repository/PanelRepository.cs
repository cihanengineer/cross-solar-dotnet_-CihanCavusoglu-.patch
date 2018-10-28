using CrossSolar.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CrossSolar.Repository
{
    public class PanelRepository : GenericRepository<Panel>, IPanelRepository
    {
        public PanelRepository(CrossSolarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Panel> GetBySerialNumAsync(string panelId)
        {
            return  await _dbContext.Panels.FirstOrDefaultAsync(x => x.Serial.Equals(panelId, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}