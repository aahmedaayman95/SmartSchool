using SmartSchool.BL.ViewModel;
using SmartSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.Interface
{
    public interface ITeacherRepo
    {
        #region AdminRole
        public string SaveInDb(TeacherVM obj, string TeacherId);
        public Teacher Edit(TeacherVMEdit obj);
        public IEnumerable<TeacherVM> GetAll();
        public TeacherVM GetById(string id);
        public void Delete(string id);
        #endregion

        #region TeacherRole
        public TeacherVM GetByIdentity(string identity); 
        #endregion

    }
}
