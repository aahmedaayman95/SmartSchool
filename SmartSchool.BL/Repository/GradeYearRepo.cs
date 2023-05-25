using Microsoft.EntityFrameworkCore;
using SmartSchool.BL.Interface;
using SmartSchool.BL.ViewModel;
using SmartSchool.DAL.Context;
using SmartSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.Repository
{
    public class GradeYearRepo :IGradeYearRepo
    {
        public SmartSchoolContext Db { get; }
        public GradeYearRepo(SmartSchoolContext db)
        {
            Db = db; 
        }
        public IEnumerable<GradeYearVM> GetAll()
        {
            var allGradYears = Db.GradeYears.Select(obj => new GradeYearVM()
            {
                Id = obj.Id,
                Name = obj.Name,
                Fees = obj.Fees,
               

            });

            return allGradYears;
        }

        public GradeYearVM GetById(int id)
        {
            var myGradYear = Db.GradeYears.Where(r => r.Id == id).Select(obj => new GradeYearVM()
            {
                Id = obj.Id,
                Name = obj.Name,
                Fees = obj.Fees,

            }).FirstOrDefault();

            return myGradYear;
        }

        public GradeYear Create(GradeYearVM obj)
        {
            var gradeYear = Db.GradeYears.Where(s => s.Name.ToLower() == obj.Name.ToLower()).FirstOrDefault();

            if (gradeYear == null)
            {
                GradeYear gradYear = new GradeYear()
                {
                    Id = obj.Id,
                    Name = obj.Name,
                    Fees = obj.Fees,

                };

                Db.GradeYears.Add(gradYear);
                Db.SaveChanges();
                return gradYear;

            }
            return default;


        }

      
        public void Delete(int id)
        {
            var gradeYear = Db.GradeYears.Find(id);
            Db.GradeYears.Remove(gradeYear);
            Db.SaveChanges();
        }

      
    }
}
