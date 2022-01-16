using EmployeeManager.Data;
using EmployeeManager.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Services
{
    public class CarHistoryService : ICarHistoryService
    {
        private readonly ApplicationDbContext _db;

        public CarHistoryService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<CarHistory> AddAsync(CarHistory carHistory)
        {
            await _db.CarHistory.AddAsync(carHistory);
            await _db.SaveChangesAsync();
            return carHistory;
        }

        public async Task<IEnumerable<CarHistory>> GetAllAsync()
        {
            return await _db.CarHistory
                .Include(h => h.Car)
                .ToListAsync();
        }

        public async Task<IEnumerable<CarHistory>> GetAllForCar(int id)
        {
            return await _db.CarHistory
                .Where(h => h.CarId == id)
                .Include(h => h.Car)
                .ToListAsync();
        }

        public async Task<IEnumerable<CarHistory>> GetAllForUser(string id)
        {
            return await _db.CarHistory
                .Where(h => h.UserId == id)
                .Include(h => h.Car)
                .ToListAsync();
        }


        public async Task<CarHistory> GetAsync(int id)
        {
            return await _db.CarHistory.FindAsync(id);
        }

        public async Task RemoveAsync(int id)
        {
            _db.CarHistory.Remove(await GetAsync(id));
            await _db.SaveChangesAsync();
        }

        public async Task<CarHistory> UpdateAsync(CarHistory carHistory)
        {
            var historyToUpdate = await GetAsync(carHistory.Id);
            historyToUpdate.CarId = carHistory.CarId;
            historyToUpdate.Cost = carHistory.Cost;
            historyToUpdate.Details = carHistory.Details;
            historyToUpdate.ExecutionDate = carHistory.ExecutionDate;
            historyToUpdate.MileageAtExecution = carHistory.MileageAtExecution;
            historyToUpdate.RenewDate = carHistory.RenewDate;
            historyToUpdate.ServiceType = carHistory.ServiceType;
            historyToUpdate.IsPayed = carHistory.IsPayed;
            await _db.SaveChangesAsync();

            return historyToUpdate;
        }
    }
}
