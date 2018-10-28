using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrossSolar.Domain;
using CrossSolar.Models;
using CrossSolar.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrossSolar.Controllers
{
    [Route("[controller]")]
    public class AnalyticsController : Controller
    {
        private readonly IAnalyticsRepository _analyticsRepository;

        private readonly IPanelRepository _panelRepository;

        private readonly IDayAnalyticsRepository _dayAnalyticsRepository;

        public AnalyticsController(IAnalyticsRepository analyticsRepository, IPanelRepository panelRepository, IDayAnalyticsRepository dayAnalyticsRepository)
        {
            _analyticsRepository = analyticsRepository;
            _panelRepository = panelRepository;
            _dayAnalyticsRepository = dayAnalyticsRepository;
        }

        
        [HttpGet("{panelId}/[controller]")]
        public async Task<IActionResult> Get([FromRoute] string panelId)
        {
            var panel = await _panelRepository.GetBySerialNumAsync(panelId);

            if (panel == null) return NotFound();

            var analytics = await _analyticsRepository.GetByPanelIdAsync(panelId);

            var result = new OneHourElectricityListModel
            {
                OneHourElectricitys = analytics.Select(c => new OneHourElectricityModel
                {
                    Id = c.Id,
                    KiloWatt = c.KiloWatt,
                    DateTime = c.DateTime
                }).ToList()
            };

            return Ok(result);
        }

       
        [HttpGet("{panelId}/[controller]/day")]
        public async Task<IActionResult> DayAnalytics([FromRoute] string panelId)
        {
            var result = await _dayAnalyticsRepository.GetBySerialAsync(panelId);

            if (result == null) return NotFound();


            return Ok(result);
        }

       
        [HttpPost]
        public async Task<IActionResult> Post([FromRoute] string panelId, [FromBody] OneHourElectricityModel value)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var oneHourElectricityContent = new OneHourElectricity
            {
                PanelId = panelId,
                KiloWatt = value.KiloWatt,
                DateTime = DateTime.UtcNow
            };

            await _analyticsRepository.InsertAsync(oneHourElectricityContent);

            var result = new OneHourElectricityModel
            {
                Id = oneHourElectricityContent.Id,
                KiloWatt = oneHourElectricityContent.KiloWatt,
                DateTime = oneHourElectricityContent.DateTime
            };

            return Created($"panel/{panelId}/analytics/{result.Id}", result);
        }
    }
}