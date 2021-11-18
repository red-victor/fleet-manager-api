using AutoMapper;
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

        public async Task AddAsync(Car car)
        {
            await _db.Cars.AddAsync(car);
        }

        public async Task<Car> GetAsync(int id)
        {
            return await _db.Cars.Where(c => c.Id == id).Include(c => c.User).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            return await _db.Cars.Include(c => c.User).ToListAsync();
        }

        public async void RemoveAsync(int id)
        {
            _db.Cars.Remove(await GetAsync(id));
            await _db.SaveChangesAsync();
        }

        public async Task<Car> TransposeFromDtoAsync(CarDto dto)
        {
            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<CarDto, Car>()
                );

            var mapper = new Mapper(config);
            return mapper.Map<Car>(dto);
        }

        public async Task<List<Car>> TransposeFromDtoAsync(List<CarDto> dtos)
        {
            var list = new List<Car>();
            foreach (var dto in dtos)
                list.Add(await TransposeFromDtoAsync(dto));
            return list;
        }

        public CarDto TransposeToDto(Car car)
        {
            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<Car, CarDto>()
                );

            var mapper = new Mapper(config);
            return mapper.Map<CarDto>(car);
        }

        public List<CarDto> TransposeToDto(IEnumerable<Car> cars)
        {
            var list = new List<CarDto>();
            foreach (var car in cars)
                list.Add(TransposeToDto(car));
            return list;
        }
    }
}