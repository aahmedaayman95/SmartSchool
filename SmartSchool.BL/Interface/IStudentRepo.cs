using SmartSchool.BL.ViewModel;
using SmartSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.Interface
{
    public interface IStudentRepo
    {
        #region Admin Role
        public IEnumerable<StudentVM> GetAll();
        public StudentVM GetbyId(string id);
        public Student Edit(StudentVM std);
        public void Delete(string id);
        public IEnumerable<StudentVM> GetStudentByClass(int classRoomId);
        public IEnumerable<StudentVM> GetStudentByGradeYear(int gradeYearId);
        public List<StudentVM> getAbsenceStudents();
        #endregion


        #region Student Role
        public StudentVM GetByIdentity(string identity);

        #endregion


        #region Parent Role
        public IEnumerable<StudentVM> GetByParentId(string id);

        #endregion

    }
}
