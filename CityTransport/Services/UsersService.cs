using AutoMapper;
using CityTransport.Data;
using CityTransport.Data.Models;
using CityTransport.Data.Repository;
using CityTransport.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CityTransport.Services
{
    public class UsersService : IUsersService
    {
       
        private readonly IRepository<User, string> usersRepo;
       
        private readonly IMapper mapper;

        public UsersService(IRepository<User, string> usersRepo,
            IMapper mapper){
            
            this.usersRepo = usersRepo;
            this.mapper = mapper;
        }

        public User GetUserById(string userId)
        {
            return usersRepo.GetById(userId);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return usersRepo.GetAll();
        }

       

        public void Edit(User user)
        {
            usersRepo.Update(user);
            usersRepo.Save();
        }
       

        // To be used only by Admin!
        public void Delete(User user)
        {
            usersRepo.Delete(user);
            usersRepo.Save();
        }
    }
}