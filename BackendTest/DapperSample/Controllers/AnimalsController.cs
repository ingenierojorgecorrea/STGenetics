using STGenetics.Core.Entities;
using STGenetics.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System;
using STGenetics.Business;

namespace STGenetics.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimalsController : ControllerBase
    {
        private readonly ILogger<AnimalsController> _logger;
        private readonly IAnimalRepository _animalRepository;
        private IConfiguration _configuration;
        private JwtAuthentication jwtAuthentication;

        public AnimalsController(ILogger<AnimalsController> logger, IAnimalRepository animalRepository, IConfiguration configuration)
        {
            _logger = logger;
            _animalRepository = animalRepository;
            _configuration = configuration;
            jwtAuthentication = new JwtAuthentication();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var animals = await _animalRepository.GetAll();

            return Ok(animals);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            jwtAuthentication.GenerateToken(_configuration);

            var animal = await _animalRepository.GetById(id);

            return Ok(animal);
        }

        [HttpGet("{filter}/{value}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByFilter(string filter, string value)
        {
            jwtAuthentication.GenerateToken(_configuration);

            var animal = await _animalRepository.GetByFilter(filter, value);

            return Ok(animal);
        }

        [HttpGet("AnimalSexByQuantity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AnimalSexByQuantity()
        {
            jwtAuthentication.GenerateToken(_configuration);

            var animalSexByQuantity = await _animalRepository.AnimalSexByQuantity();

            return Ok(animalSexByQuantity);
        }

        [HttpGet("GetByOlderThanTwoYearsAndFemale")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByOlderThanTwoYearsAndFemale()
        {
            jwtAuthentication.GenerateToken(_configuration);

            var animal = await _animalRepository.GetByOlderThanTwoYearsAndFemale();

            return Ok(animal);
        }        

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(Animal animal)
        {
            jwtAuthentication.GenerateToken(_configuration);

            _ = await _animalRepository.Create(animal);
            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Animal animal)
        {
            jwtAuthentication.GenerateToken(_configuration);

            var currentanimal = await _animalRepository.GetById(animal.AnimalId);
            if (currentanimal == null)
            {
                return BadRequest("Animal not found");
            }

            _ = await _animalRepository.Update(animal);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            jwtAuthentication.GenerateToken(_configuration);

            var currentAnimal = await _animalRepository.GetById(id);
            if (currentAnimal == null)
            {
                return BadRequest("Animal not found");
            }

            _ = await _animalRepository.Delete(id);
            return Ok();
        }

    }
}
