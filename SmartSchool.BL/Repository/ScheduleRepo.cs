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
    public class ScheduleRepo : IScheduleRepo
    {
        public SmartSchoolContext Db { get; }
        public ScheduleRepo(SmartSchoolContext db)
        {
            Db = db;
        }
        #region AdminRole
        public IEnumerable<ScheduleVM> GetAll()
        {
            var allSchedules = Db.Schedules.Include(a => a.Class).Select(obj => new ScheduleVM()
            {
                Id = obj.Id,
                Day = obj.Day,
                ClassId = obj.ClassId,
                ClassRoomName = obj.Class.Name,
            });
            return allSchedules;
        }

        public ScheduleVM GetById(int id)
        {
            var mySchedule = Db.Schedules.Where(r => r.Id == id).Select(obj => new ScheduleVM()
            {
                Id = obj.Id,
                Day = obj.Day,
                ClassId = obj.ClassId,
                ClassRoomName = obj.Class.Name
            }).FirstOrDefault();

            return mySchedule;
        }

        public Schedule Create(ScheduleVM obj)
        {
            Schedule Schedule = new Schedule()
            {
                Day = obj.Day,
                ClassId = obj.ClassId,
            };

            Db.Schedules.Add(Schedule);
            Db.SaveChanges();
            return Schedule;
        }

        public void Delete(int id)
        {
            var mySchedule = Db.Schedules.Find(id);
            Db.Schedules.Remove(mySchedule);
            Db.SaveChanges();
        }

        public Schedule Edit(ScheduleVM obj)
        {
            Schedule S = Db.Schedules.Find(obj.Id);
            S.Id = obj.Id;
            S.Day = obj.Day;
            S.ClassId = obj.ClassId;
            Db.SaveChanges();
            return S;
        }

        #endregion

        #region ParentRole & StudentRole
        public IEnumerable<SessionVM> GetStudentSchedule(int classid, DateTime start, DateTime end)
        {

            var x = from s in Db.Schedules
                    join c in Db.ClassRooms
                    on s.ClassId equals c.Id
                    join e in Db.Sessions
                    on s.Id equals e.ScheduleID
                    join t in Db.Teachers
                    on e.TeacherID equals t.Id
                    join b in Db.Subjects
                    on t.SubjectId equals b.Id
                    where c.Id == classid && s.Day >= start && s.Day <= end
                    select new SessionVM()
                    {
                        Id = e.Id,
                        TeacherID = t.Id,
                        SubjectName = b.Name,
                        TeacherName = t.FullName,
                        SessionNo = e.SessionNo,
                        ScheduleDay = s.Day.ToString(),
                        ClassName = c.Name
                    };
            return x;

            #region another way

            //    var schedule = Db.Schedules.Where(s => s.ClassId == classid && s.Day >= start && s.Day <= end).Include(s=>s.Class).Include(s=>s.Sessions).ToList();

            //    List<SessionVM> sessions = new List<SessionVM>();

            //    foreach (var item in schedule)
            //    {
            //        foreach (var s in item.Sessions)
            //        {
            //            var x = Db.Sessions.Where(c=>c.Id==s.Id).Include(x=>x.Teacher).ThenInclude(a=>a.Subject).FirstOrDefault();

            //            SessionVM VMsession = new SessionVM()
            //            {
            //                Id = x.Id,
            //                TeacherID = x.TeacherID,
            //                SubjectName = x.Teacher.Subject.Name,
            //                TeacherName = x.Teacher.FullName,
            //                SessionNo = x.SessionNo,
            //                ScheduleDay = item.Day.ToString(),
            //                ClassName = item.Class.Name
            //            };

            //            sessions.Add(VMsession);
            //        }
            //    }  

            //    return sessions;

            #endregion

        }
        #endregion
        
        #region TeacherRole
        public IEnumerable<SessionVM> GetTeacherSchedule(string identity, DateTime start, DateTime end)
        {
            var teacher = Db.Teachers.Where(x => x.IdentityUserId == identity).FirstOrDefault();
            var teacherSessions = Db.Sessions.Where(s => s.TeacherID == teacher.Id && s.Schedule.Day >= start && s.Schedule.Day <= end).Select(x => new SessionVM()
            {
                Id = x.Id,
                TeacherID = x.TeacherID,
                SubjectName = x.Teacher.Subject.Name,
                TeacherName = x.Teacher.FullName,
                SessionNo = x.SessionNo,
                ScheduleDay = x.Schedule.Day.ToString(),
                ClassName = x.Schedule.Class.Name
            }
            );
            return teacherSessions;
        } 
        #endregion
    }
}
