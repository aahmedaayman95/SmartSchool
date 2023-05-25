using SmartSchool.BL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.Interface
{
    public interface IExamResultRepo
    {
        public void generateStudentsGrades();
        public IEnumerable<ExamResultVM> getResultsByClassRoom(int classid);
        public void Edit(List<ExamResultVM> Result);

        public void upgradeStudent();


        #region Parent and Student Role
        public List<ExamResultVM> getStudentExamResults(string studentId, int gradeYearId);

        #endregion
    }
}
