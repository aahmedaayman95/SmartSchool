using Microsoft.AspNetCore.Mvc;
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
    public class SubjectRepo : ISubjectRepo
    {
        public SmartSchoolContext Db { get; }
        public SubjectRepo(SmartSchoolContext db)
        {
            Db = db;
        }
        public IEnumerable<SubjectVM> GetAll()
        {
            var allSubjects = Db.Subjects.Include("GradeYear").Select(obj => new SubjectVM()
            {
                Id = obj.Id,
                Name = obj.Name,
                GradeYearId = obj.GradeYearId,
                GradeYearName = obj.GradeYear.Name,
                TotalMark= obj.TotalMark,
            });

            return allSubjects;
        }

        public SubjectVM GetById(int id)
        {
            var mySubject= Db.Subjects.Include("GradeYear").Where(r => r.Id == id).Select(obj => new SubjectVM()
            {
                Id = obj.Id,
                Name = obj.Name,
                GradeYearId = obj.GradeYearId,
                GradeYearName = obj.GradeYear.Name,
                TotalMark = obj.TotalMark,
            }).FirstOrDefault();

            return mySubject;
        }

        public Subject Create(SubjectVM obj)
        {
            var gradeYearSubjects = Db.Subjects.Where(s => s.Name.ToLower() == obj.Name.ToLower()&&s.GradeYearId==obj.GradeYearId).FirstOrDefault();

            if(gradeYearSubjects == null)
            {
                Subject subject = new Subject()
                {
                    Name = obj.Name,
                    GradeYearId = obj.GradeYearId,
                    TotalMark = obj.TotalMark,
                };

                Db.Subjects.Add(subject);
                Db.SaveChanges();
                return subject;
            }
                return default;
        }

        public void Delete(int id)
        {
            var subject = Db.Subjects.Find(id);
            Db.Subjects.Remove(subject);
            Db.SaveChanges();
        }
		public IEnumerable<SubjectVM> GetByClassRoom(int classroomId)
		{
			var mySubjects = from c in Db.ClassRooms
							 join g in Db.GradeYears
							 on c.gradeYearId equals g.Id
							 join s in Db.Subjects
							 on g.Id equals s.GradeYearId
							 where c.Id == classroomId
							 select new SubjectVM()
							 {
								 Id = s.Id,
								 Name = s.Name,
								 GradeYearId = g.Id,
								 GradeYearName = g.Name
							 };

			return mySubjects;
		}
	
        //public List<SubjectVM> GetByGradeYear(int gradeYearId)
		//{
		//    var mySubject = Db.Subjects.Include("GradeYear").Where(r => r.GradeYearId == gradeYearId ).Select(obj => new SubjectVM()
		//    {
		//        Id = obj.Id,
		//        Name = obj.Name,
		//        GradeYearId = obj.GradeYearId,
		//        GradeYearName = obj.GradeYear.Name,
		//        TotalMark = obj.TotalMark,
		//    }).ToList();

		//    return mySubject;
		//}

	}
}

