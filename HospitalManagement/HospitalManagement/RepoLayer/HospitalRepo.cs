using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagement.RepoLayer
{
    public class HospitalRepo : IHospitalRepo
    {
        TestEntities HospitalDB = new TestEntities();

        public IEnumerable<Hospital> GetAll()
        {
            return HospitalDB.Hospitals;
        }

        public Hospital Get(int HospitalId)
        {
            return HospitalDB.Hospitals.Find(HospitalId);
        }

        public Hospital Add(Hospital item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            // TO DO : Code to save record into database
            HospitalDB.Hospitals.Add(item);
            HospitalDB.SaveChanges();
            return item;
        }

        public bool Update(Hospital item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            // TO DO : Code to update record into database

            var products = HospitalDB.Hospitals.Single(a => a.HospitalId == item.HospitalId);
            products.HospitalName = item.HospitalName;
            products.HospitalSpeciality = item.HospitalSpeciality;
            products.HospitalAddress = item.HospitalAddress;
            products.HospialCity = item.HospialCity;
            products.HospitalContactNumber = item.HospitalContactNumber;

            HospitalDB.SaveChanges();
            return true;

        }

        public bool Delete(int id)
        {
            Hospital products = HospitalDB.Hospitals.Find(id);
            HospitalDB.Hospitals.Remove(products);
            HospitalDB.SaveChanges();

            return true;
        }
    }
}