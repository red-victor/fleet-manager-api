﻿using EmployeeManager.Data;
using EmployeeManager.Models;
using Microsoft.EntityFrameworkCore;
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
    }
}