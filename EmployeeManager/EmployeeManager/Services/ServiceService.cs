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
    public class ServiceService : IServiceService
    {
        private readonly ApplicationDbContext _db;

        public ServiceService(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add(ServiceToProcess service)
        {
            _db.ServicesToProcess.Add(service); 
            _db.SaveChanges();
        }

        public ServiceToProcess Get(int id)
        {
            return _db.ServicesToProcess.Where(s => s.Id == id)
                .Include(s => s.User)
                .Include(s => s.Car)
                .FirstOrDefault();
        }

        public IEnumerable<ServiceToProcess> GetAll()
        {
            return _db.ServicesToProcess;
        }

        public void Remove(int id)
        {
            _db.ServicesToProcess.Remove(Get(id));
        }

        public ServiceToProcess TransposeFromDto(ServiceToProcessDto dto)
        {
            return new ServiceToProcess
            {
                Id = dto.Id,
                User = _db.Users.Find(dto.UserId),
                Car = _db.Cars.Find(dto.CarId),
                Title = dto.Title,
                ImagePath = dto.ImagePath,
                Details = dto.Details,
                Date = dto.Date
            };
        }

        public ServiceToProcessDto TransposeToDto(ServiceToProcess service)
        {
            var dto =  new ServiceToProcessDto
            {
                Id = service.Id,
                UserId = service.User.Id,
                CarId = service.Car.Id,
                Title = service.Title,
                ImagePath = service.ImagePath,
                Details = service.Details,
                Date = service.Date
            };

            return dto;
        }
    }
}
