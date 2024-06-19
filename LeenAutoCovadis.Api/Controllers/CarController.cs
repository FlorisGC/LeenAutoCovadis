using LeenAutoCovadis.Api.Data;
using LeenAutoCovadis.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LeenAutoCovadis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CovadisContext covadisContext;

        public CarController(CovadisContext covadisContext)
        {
            this.covadisContext = covadisContext;
        }

        [HttpGet]
        public IEnumerable<Car> GetCar()
        {
            return covadisContext.Cars.ToArray();
        }

        [HttpPost]
        public ActionResult<Car> AddCar(Car car)
        {
            covadisContext.Cars.Add(car);
            covadisContext.SaveChanges();
            return Ok(car);
        }

        [HttpPut]
        public ActionResult UpdateCar(int id, Car cars)
        {
            Car prevCar = covadisContext.Cars.SingleOrDefault(a => a.Id == id);

            if (prevCar == null)
            {
                return NotFound();
            }

            prevCar.Model = cars.Model;
            prevCar.Type = cars.Type;
            prevCar.Kilometers = cars.Kilometers;
            prevCar.StartKilometers = cars.StartKilometers;
            prevCar.EndKilometers = cars.EndKilometers;
            prevCar.Available = cars.Available;
            prevCar.User = cars.User;

            covadisContext.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public ActionResult DeleteCar(int id)
        {
            var car = covadisContext.Cars.SingleOrDefault(a => a.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            covadisContext.Cars.Remove(car);
            covadisContext.SaveChanges();
            return Ok();
        }

        [HttpPost("check-and-add")]
        public ActionResult CheckAndAddTestCar()
        {
            if (!covadisContext.Cars.Any())
            {
                var testCar = new Car
                {
                    Model = "Test Model",
                    Type = "Test Type",
                    StartKilometers = 0,
                    EndKilometers = 0,
                    Kilometers = 0,
                    StartAddress = "Test Start Address",
                    EndAddress = "Test End Address",
                    Available = true,
                    User = covadisContext.Users.FirstOrDefault() // Assign to the first user
                };

                covadisContext.Cars.Add(testCar);
                covadisContext.SaveChanges();
                return Ok(testCar);
            }
            return Ok("Cars already exist");
        }
    }
}
