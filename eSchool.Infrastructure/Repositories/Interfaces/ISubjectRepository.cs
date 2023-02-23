using eSchool.Domain.Models;
using Onion.Infrastructures.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSchool.Infrastructure.Repositories.Interfaces
{
    public interface ISubjectRepository:IRepository<Subject>
    {
    }
}
