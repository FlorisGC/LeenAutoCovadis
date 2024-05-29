using LeenAutoCovadis.Api.Data;
using LeenAutoCovadis.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            covadisContext.Cars.Where(a => a.Id == id)
                .ExecuteDelete();
            covadisContext.SaveChanges();
            return Ok();
        }
    }
}