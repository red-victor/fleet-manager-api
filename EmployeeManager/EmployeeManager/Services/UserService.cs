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

        public async Task AddAsync(ApplicationUser item)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(ApplicationUser user)
        {
            throw new NotImplementedException();

            //var userToUpdate = await GetAsync(user.Id);
            //userToUpdate.Adress = user.Adress;
            //userToUpdate.Email = user.Email;
            //await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<ApplicationUser> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> GetAsync(string id)
        {
            return await _db.Users.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public UserDto GetUserDto(ApplicationUser user)
        {
            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<ApplicationUser, UserDto>()
                );

            var mapper = new Mapper(config);
            return mapper.Map<UserDto>(user);
        }

        public IEnumerable<UserDto> GetUsersDto(IEnumerable<ApplicationUser> users)
        {
            var usersDtos = new List<UserDto>();

            foreach (var user in users)
                usersDtos.Add(GetUserDto(user));

            return usersDtos;
        }
    }
}
