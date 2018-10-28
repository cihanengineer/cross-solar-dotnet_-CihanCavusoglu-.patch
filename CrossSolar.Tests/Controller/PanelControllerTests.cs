using System.Threading.Tasks;
using CrossSolar.Controllers;
using CrossSolar.Exceptions;
using CrossSolar.Models;
using CrossSolar.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System;
using CrossSolar.Domain;

namespace CrossSolar.Tests.Controller
{
    public class PanelControllerTests
    {
        public PanelControllerTests()
        {
            _panelController = new PanelController(_panelRepositoryMock.Object);
        }

        private readonly PanelController _panelController;

        private readonly Mock<IPanelRepository> _panelRepositoryMock = new Mock<IPanelRepository>();

        [Fact]
        public async Task RegisterTest()
        {
            var panel = new PanelModel
            {
                Brand = "Areva",
                Latitude = 12.345678,
                Longitude = 98.7655432,
                Serial = "AAAA1111BBBB2222"
            };

            // Arrange
      

            // Act
            var result = await _panelController.Register(panel);

            // Assert
            Assert.NotNull(result);

            var createdResult = result as CreatedResult;
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
        }


        [Fact]
        public void TestModels()
        {

            OneHourElectricityModel oneHourElectricityModel = new OneHourElectricityModel();
            oneHourElectricityModel.DateTime = DateTime.Now;
            oneHourElectricityModel.Id = 1;
            oneHourElectricityModel.KiloWatt = 234;

            OneHourElectricityListModel oneHourElectricityListModel = new OneHourElectricityListModel();
            oneHourElectricityListModel.OneHourElectricitys.Add(oneHourElectricityModel);

            Assert.NotNull(oneHourElectricityListModel.OneHourElectricitys);
            Assert.NotNull(oneHourElectricityModel);




            OneDayElectricityModel oneDayElectricityModel = new OneDayElectricityModel();
            oneDayElectricityModel.Average = 23;
            oneDayElectricityModel.DateTime = DateTime.UtcNow;
            oneDayElectricityModel.Maximum = 344;
            oneDayElectricityModel.Minimum = 24255;
            oneDayElectricityModel.Sum = 234446;

            Assert.NotNull(oneDayElectricityModel);


        }

        [Fact]
        public void TestPanelRepository()
        {

            IPanelRepository panelRepository = new PanelRepository(new CrossSolarDbContext());

            var panel = new Panel
            {
                Brand = "Areva",
                Latitude = 12.345678,
                Longitude = 98.7655432,
                Serial = "AAAA1111BBBB27657"
            };

            panelRepository.InsertAsync(panel);

            var result = panelRepository.GetBySerialNumAsync("AAAA1111BBBB27657");

            Assert.NotNull(result);

        }

    }   
}