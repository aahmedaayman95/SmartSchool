using SmartSchool.BL.ViewModel;
using SmartSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.Interface
{
    public interface IScheduleRepo
    {
        #region AdminRole
        public Schedule Create(ScheduleVM obj);

        public IEnumerable<ScheduleVM> GetAll();

        public ScheduleVM GetById(int id);

        public void Delete(int id);

        public Schedule Edit(ScheduleVM obj);
        #endregion
        
        #region ParentRole & StudentRole
        public IEnumerable<SessionVM> GetStudentSchedule(int classid, DateTime start, DateTime end);
        #endregion

        #region TeacherRole
        public IEnumerable<SessionVM> GetTeacherSchedule(string identity, DateTime start, DateTime end); 
        #endregion


    }
}
