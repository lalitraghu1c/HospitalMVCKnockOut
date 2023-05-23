using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.RepoLayer
{
    interface IHospitalRepo
    {
        IEnumerable<Hospital> GetAll();
        Hospital Get(int id);
        Hospital Add(Hospital item);
        bool Update(Hospital item);
        bool Delete(int id);
    }
}
