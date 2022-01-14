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
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;

        public UserService(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task<ApplicationUser> AddAsync(ApplicationUser item)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> UpdateAsync(ApplicationUser user)
        {
            var userToUpdate = await GetAsync(user.Id);
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.Adress = user.Adress;
            userToUpdate.Email = user.Email;
            userToUpdate.PhoneNumber = user.PhoneNumber;
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            return await _db.Users
                .Include(u => u.Car)
                .ToListAsync();
        }

        public Task<ApplicationUser> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> GetAsync(string id)
        {
            return await _db.Users
                .Where(u => u.Id == id)
                .Include(u => u.Car)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersWithoutCarAsync()
        {
            return await _db.ApplicationUsers.Where(u => u.Car == null).ToListAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var user = await GetAsync(id);
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
        }

        public ApplicationUser TransposeFromDto(UserDto dto)
        {
            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<UserDto, ApplicationUser>()
                );

            var mapper = new Mapper(config);
            return mapper.Map<ApplicationUser>(dto);
        }

        public UserDto TransposeToDtoAsync(ApplicationUser user)
        {
            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<ApplicationUser, UserDto>()
                );

            var mapper = new Mapper(config);
            return mapper.Map<UserDto>(user);
        }

        public IEnumerable<UserDto> TransposeToDtoAsync(IEnumerable<ApplicationUser> users)
        {
            var usersDtos = new List<UserDto>();

            foreach (var user in users)
                usersDtos.Add(TransposeToDtoAsync(user));

            return usersDtos;
        }

        public async Task<ApplicationUser> GetByUsernameAsync(string username)
        {
            return await _db.Users
                .Where(u => u.UserName == username)
                .Include(u => u.Car)
                .FirstOrDefaultAsync();
        }
    }
}
