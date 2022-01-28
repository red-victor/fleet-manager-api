using EmployeeManager.Data;
using EmployeeManager.DTOs;
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
        private readonly ApplicationDbContext _db;

        public CarService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Car> AddAsync(Car car)
        {
            await _db.Cars.AddAsync(car);
            await _db.SaveChangesAsync();

            return car;
        }

        public async Task<List<Car>> AddAsync(List<Car> cars)
        {
            await _db.Cars.AddRangeAsync(cars);
            await _db.SaveChangesAsync();

            return cars;
        }

        public async Task<Car> UpdateAsync(Car car)
        {
            var carToUpdate = await GetAsync(car.Id);
            carToUpdate.Color = car.Color;
            carToUpdate.Mileage = car.Mileage;
            carToUpdate.LicencePlate = carToUpdate.LicencePlate;
            await _db.SaveChangesAsync();

            return car;
        }

        public async Task<Car> GetAsync(int id)
        {
            return await _db.Cars.Where(c => c.Id == id).Include(c => c.User).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            return await _db.Cars.Include(c => c.User).ToListAsync();
        }

        public async Task<IEnumerable<Car>> GetAllAssignedAsync()
        {
            return await _db.Cars.Where(c => c.User != null).Include(c => c.User).ToListAsync();
        }

        public async Task<IEnumerable<Car>> GetAllUnassignedAsync()
        {
            return await _db.Cars.Where(c => c.User == null).Include(c => c.User).ToListAsync();
        }

        public async Task RemoveAsync(int id)
        {
            _db.Cars.Remove(await GetAsync(id));
            await _db.SaveChangesAsync();
        }

        public async Task<PaginationDto<Car>> SearchCars(string str, int page, int pageSize)
        {
            var allCars = await _db.Cars.ToListAsync();
            var cars = new List<Car>();
            var searchTerms = str.ToLower().Split(' ');

            foreach (var car in allCars)
            {
                var isEligible = true;
               
                foreach (var term in searchTerms)
                {
                    if (!isEligible) break;
                    if (!car.Brand.ToLower().Contains(term) && !car.Model.ToLower().Contains(term)) isEligible = false;
                }
                if (isEligible) cars.Add(car); 
            }

            var count = cars.Count;

            return new PaginationDto<Car>
            {
                Items = cars.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                CurrentPage = page
            };
        }

        public async Task<PaginationDto<Car>> GetCarsByPageAsync(int page, int pageSize)
        {
            var query = _db.Cars.AsQueryable();
            var count = await query.CountAsync();
            return new PaginationDto<Car>
            {
                Items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(),
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                CurrentPage = page
            };
        }
    }
}