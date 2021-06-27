using AutoMapper;
using CityTransport.Data.Models;
using CityTransport.Data.Repository;
using CityTransport.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTransport.Services
{
    public class ParentService : IParentService
    {

        private readonly IRepository<User, string> usersRepo;
        private readonly IRepository<Parent, string> parentRepo;
        private readonly IMapper mapper;

        public ParentService(IRepository<User, string> usersRepo,
            IRepository<Parent, string> parentRepo,
            IMapper mapper)
        {

            this.usersRepo = usersRepo;
            this.parentRepo = parentRepo;
            this.mapper = mapper;
        }

        public Parent GetChildrenById(string userId)
        {
            return parentRepo.GetById(userId);
        }

        public IEnumerable<Parent> GetAllParents()
        {
            return parentRepo.GetAll();
        }
       /* public string GetChildrenName(string ChildrenFirstName, string ChildrenLastName)
        {
            return parentRepo.u

        }*/


        public void Edit(Parent parent)
        {
            parentRepo.Update(parent);
            parentRepo.Save();
        }
        public void Add(Parent parent)
        {
            parentRepo.Add(parent);
            parentRepo.Save();
        }


        // To be used only by Admin!
        public void Delete(Parent parent)
        {
            parentRepo.Delete(parent);
            parentRepo.Save();
        }
    }
}