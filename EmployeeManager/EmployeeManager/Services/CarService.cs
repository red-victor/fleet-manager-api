using EmployeeManager.Data;
using EmployeeManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Services
{
    public class CarService : ICarService
    {
        //private readonly ICarDao _carDao;
        private readonly ApplicationDbContext _db;

        public CarService(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add(Car car)
        {
            _db.Cars.Add(car);
            _db.SaveChanges();
        }

        public Car Get(int id)
        {
            return _db.Cars.Find(id);
        }

        public IEnumerable<Car> GetAll()
        {
            return _db.Cars;
        }


        public Car GetCarByChassis(string chassisSeries)
        {
            return _db.Cars.Where(x => x.ChassisSeries == chassisSeries).FirstOrDefault();
            //return _db.Cars.Where(x => x.ChassisSeries == chassisSeries).Include(c => c.User).FirstOrDefault();
        }

        public void Remove(int id)
        {
            _db.Cars.Remove(this.Get(id));
        }
    }
}
