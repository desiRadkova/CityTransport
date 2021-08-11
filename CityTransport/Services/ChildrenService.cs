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
    public class ChildrenService : IChildrenService
    {

        private readonly IRepository<User, string> usersRepo;
        private readonly IRepository<Children, string> childrenRepo;
        private readonly IMapper mapper;

        public ChildrenService(IRepository<User, string> usersRepo,
            IRepository<Children, string> childrenRepo,
            IMapper mapper)
        {

            this.usersRepo = usersRepo;
            this.childrenRepo = childrenRepo;
            this.mapper = mapper;
        }

        public Children GetChildrenById(string userId)
        {
            return childrenRepo.GetById(userId);
        }

        public IEnumerable<Children> GetAllParents()
        {
            return childrenRepo.GetAll();
        }
       /* public string GetChildrenName(string ChildrenFirstName, string ChildrenLastName)
        {
            return parentRepo.u

        }*/


        public void Edit(Children children)
        {
            childrenRepo.Update(children);
            childrenRepo.Save();
        }
        public void Add(Children children)
        {
            childrenRepo.Add(children);
            childrenRepo.Save();
        }


        // To be used only by Admin!
        public void Delete(Children children)
        {
            childrenRepo.Delete(children);
            childrenRepo.Save();
        }
    }
}