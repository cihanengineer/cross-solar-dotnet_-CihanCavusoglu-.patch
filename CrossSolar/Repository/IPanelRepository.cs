using CrossSolar.Domain;
using System.Threading.Tasks;

namespace CrossSolar.Repository
{
    public interface IPanelRepository : IGenericRepository<Panel>
    {
        Task<Panel> GetBySerialNumAsync(string panelId);
    }
}