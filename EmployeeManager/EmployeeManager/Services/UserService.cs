using AutoMapper;
using EmployeeManager.Data;
using EmployeeManager.DTOs;
using EmployeeManager.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }

        public async Task<ApplicationUser> ChangeEmail(ApplicationUser userToChange, string newEmail)
        {
            var user = await GetAsync(userToChange.Id);
            user.Email = newEmail;
            user.UserName = newEmail;
            user.NormalizedEmail = newEmail.ToUpper();
            user.NormalizedUserName = newEmail.ToUpper();
            user.UnConfirmedEmail = null;

            if (await _db.SaveChangesAsync() > 0) return user;

            return null;
        }

        public Task<ApplicationUser> AddAsync(ApplicationUser item)
        {
            throw new NotImplementedException();
        }

        public async Task<(List<string>, List<string>)> RegisterUsers(List<RegisterDto> userDtos)
        {
            var successful = new List<string>();
            var failed = new List<string>();

            foreach (var toRegister in userDtos)
            {
                var user = new ApplicationUser
                {
                    UserName = toRegister.Email,
                    Email = toRegister.Email,
                    FirstName = toRegister.FirstName,
                    LastName = toRegister.LastName,
                    CNP = toRegister.Cnp,
                    Adress = toRegister.Address,
                    PhoneNumber = toRegister.PhoneNumber,
                    PhoneNumberConfirmed = false,
                    ImgName = null,
                    ImgSrc = null,
                };

                var result = await _userManager.CreateAsync(user, toRegister.Password);

                // Send email to user with generated password

                if (result.Succeeded)
                {
                    successful.Add(toRegister.Email);
                    await _userManager.AddToRoleAsync(user, toRegister.Role);
                }
                else
                {
                    failed.Add(toRegister.Email);
                }
            }

            return (successful, failed);
        }

        public async Task<ApplicationUser> UpdateAsync(ApplicationUser user)
        {
            var userToUpdate = await GetAsync(user.Id);
            if (userToUpdate != null)
            {
                userToUpdate.FirstName = user.FirstName;
                userToUpdate.LastName = user.LastName;
                userToUpdate.Adress = user.Adress;
                userToUpdate.Email = user.Email;
                userToUpdate.PhoneNumber = user.PhoneNumber;
                userToUpdate.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
                userToUpdate.CNP = user.CNP;
                userToUpdate.ImgName = user.ImgName;
                userToUpdate.ImgSrc = user.ImgSrc;
                await _db.SaveChangesAsync(); 
                return user;
            }

            return null;
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

        public async Task<ApplicationUser> TransposeFromDtoAsync(UserDto dto)
        {
            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<UserDto, ApplicationUser>()
                );

            var mapper = new Mapper(config);
            var mappedObj = mapper.Map<ApplicationUser>(dto);

            // If user simply deleted actual photo
            if (dto.ImgSrc == null && dto.ImgFile == null)
            {
                DeleteImage(mappedObj.ImgName);
                mappedObj.ImgName = null;
                return mappedObj;
            }

            // If user changed photo
            if (dto.ImgFile != null)
            {
                DeleteImage(mappedObj.ImgName);
                mappedObj.ImgName = await SaveProfileImageAsync(dto.ImgFile);
            }

            return mappedObj;
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

        public async Task<PaginationDto<ApplicationUser>> SearchUsers(string str, int page, int pageSize)
        {
            var allUsers = await _db.Users.Include(u => u.Car).ToListAsync();
            var usersToReturn = new List<ApplicationUser>();
            var searchTerms = str.ToLower().Split(' ');

            foreach (var user in allUsers)
            {
                var isEligible = true;

                if (user.Email.ToLower().Contains(str.ToLower()))
                {
                    usersToReturn.Add(user);
                    isEligible = false;
                }

                if (isEligible)
                {
                    foreach (var term in searchTerms)
                    {
                        if (!isEligible) break;
                        if (!user.FirstName.ToLower().Contains(term) && !user.LastName.ToLower().Contains(term)) isEligible = false;
                    }
                    if (isEligible) usersToReturn.Add(user);
                }
            }

            var count = usersToReturn.Count;

            return new PaginationDto<ApplicationUser>
            {
                Items = usersToReturn.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                CurrentPage = page
            };
        }

        public async Task<List<ApplicationUser>> SearchUsersWithNoCar(string str)
        {
            var allUsers = await _db.Users.Where(u => u.Car == null).ToListAsync();
            var usersToReturn = new List<ApplicationUser>();
            var searchTerms = str.ToLower().Split(' ');

            foreach (var user in allUsers)
            {
                var isEligible = true;

                if (user.Email.ToLower().Contains(str.ToLower()))
                {
                    usersToReturn.Add(user);
                    isEligible = false;
                }

                if (isEligible)
                {
                    foreach (var term in searchTerms)
                    {
                        if (!isEligible) break;
                        if (!user.FirstName.ToLower().Contains(term) && !user.LastName.ToLower().Contains(term)) isEligible = false;
                    }
                    if (isEligible) usersToReturn.Add(user);
                }
            }

            return usersToReturn;
        }

        public async Task<PaginationDto<ApplicationUser>> GetUsersByPageAsync(int page, int pageSize)
        {
            var query = _db.Users.AsQueryable();
            var count = await query.CountAsync();
            return new PaginationDto<ApplicationUser>
            {
                Items = await query.Skip((page - 1) * pageSize).Take(pageSize).Include(u => u.Car).ToListAsync(),
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                CurrentPage = page
            };
        }

        public async Task<string> SaveProfileImageAsync(IFormFile imgFile)
        {
            if (imgFile == null) return null;

            string imgName = Guid.NewGuid().ToString().Substring(0, 8) + Path.GetExtension(imgFile.FileName);
            var imgPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", imgName);

            using (var fileStream = new FileStream(imgPath, FileMode.Create))
            {
                await imgFile.CopyToAsync(fileStream);
            }

            return imgName;
        }

        public void DeleteImage(string imgName)
        {
            if (imgName == null || imgName == "") return;
            var imgPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", imgName);
            if (File.Exists(imgPath)) File.Delete(imgPath);
        }
    }
}
