using CrossSolar.Controllers;
using CrossSolar.Domain;
using CrossSolar.Models;
using CrossSolar.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CrossSolar.Tests.Controller
{
   public class AnalyticsControllerTests
    {

        public AnalyticsControllerTests()
        {
            _analyticsController = new AnalyticsController(
                _analyticsRepositoryMock.Object,
               _panelRepositoryMock.Object,
                _dayAnalyticsRepositoryMock.Object
            );
        }

        private readonly AnalyticsController _analyticsController;

        private readonly Mock<IAnalyticsRepository> _analyticsRepositoryMock = new Mock<IAnalyticsRepository>();
        private readonly Mock<IPanelRepository> _panelRepositoryMock = new Mock<IPanelRepository>();
       private readonly Mock<IDayAnalyticsRepository> _dayAnalyticsRepositoryMock = new Mock<IDayAnalyticsRepository>();

    
        [Fact]
        public async Task GetTest()
        {
            var resultGet = await _analyticsController.Get("1");
            var objectResult = resultGet as ObjectResult;
            // Assert
            Assert.NotNull(resultGet);
            Assert.NotNull(objectResult);
            Assert.Equal(200, objectResult.StatusCode);

        }

        [Fact]
        public async Task DayResultsTest()
        {

           //// Act
            var result = await _analyticsController.DayAnalytics("1234");

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(result);

        }

        [Fact]
        public async Task Post_ShouldReturnBadRequestIfModelIsInvalid()
        {
            // Arrange
          

            var result = await _analyticsController.Post("1234", new OneHourElectricityModel
            {
                DateTime=DateTime.Now,
                Id=23,
                KiloWatt=12345
            });

            Assert.NotNull(result);

            var badRequest = result as BadRequestObjectResult;

            Assert.NotNull(badRequest);
        }


        [Fact]
        public void TestAnalyticsRepository()
        {

          IAnalyticsRepository analytics= new AnalyticsRepository(new CrossSolarDbContext());

            OneHourElectricity oneHourElectricity = new OneHourElectricity()
            {
                DateTime = DateTime.Now,
                KiloWatt=312,
                PanelId="12"
            };
            
            analytics.InsertAsync(oneHourElectricity);
            
            var result=  analytics.GetByPanelIdAsync("12");

            Assert.NotNull(result);

        }


        [Fact]
        public void TestDayAnalyticsAndGenericRepository()
        {

            IDayAnalyticsRepository dayAnalyticsRepository = new DayAnalyticsRepository(new CrossSolarDbContext());

            var oneDayElectricity = new OneDayElectricityModel
            {
               Average=13,
               DateTime=DateTime.Now,
               Maximum=2423,
               Minimum=324,
               Sum=34543
            };

            dayAnalyticsRepository.InsertAsync(oneDayElectricity);

            oneDayElectricity.Average = 1;

            dayAnalyticsRepository.UpdateAsync(oneDayElectricity);
            
           

            var result = dayAnalyticsRepository.GetBySerialAsync("123");

            var averageResult= dayAnalyticsRepository.Query().Where(x=>x.Average==1);
            var resultAnalytics = dayAnalyticsRepository.GetAsync("2");

            Assert.NotNull(result);
            Assert.NotNull(averageResult);
            Assert.NotNull(resultAnalytics);

        }
    }
}
