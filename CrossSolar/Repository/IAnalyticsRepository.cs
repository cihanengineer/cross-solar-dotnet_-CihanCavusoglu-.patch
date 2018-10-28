using CrossSolar.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrossSolar.Repository
{
    public interface IAnalyticsRepository : IGenericRepository<OneHourElectricity>
    {

        Task<List<OneHourElectricity>> GetByPanelIdAsync(string panelId);
    }
}