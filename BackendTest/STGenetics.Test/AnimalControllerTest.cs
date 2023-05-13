using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using STGenetics.Controllers;
using STGenetics.Core.Entities;
using STGenetics.Core.Repositories;
using STGenetics.DataModel;
using System;

namespace STGenetics.Test
{
    public class AnimalControllerTest
    {
        ILogger<AnimalsController> _logger;
        IConfiguration _configuration;

        [Fact]
        public void GetAllTest()
        {
            //Arrange
            Mock<IAnimalRepository> _animalRepository = new Mock<IAnimalRepository>();
            var animalMock = new Mock<DbSet<Animal>>();
            var animal = new Animal(1, "Dog", "Yorkey", DateTime.Now, "M", 500, true);

            var controller = new AnimalsController(_logger, (IAnimalRepository)_animalRepository, _configuration);

            //Act
            var result = controller.Get();

            //Assert
            Assert.NotNull(result);
        }
    }
}