using SmartSchool.BL.ViewModel;
using SmartSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.Interface
{
    public interface IParentRepo
    {


        #region Admin Role
        public IEnumerable<ParentVM> GetAll();
        public ParentVM GetbyId(string id);
        public Parent Edit(ParentVM pnt);
        public void Delete(string id);
        #endregion


        #region Parent Role

        public ParentVM GetByIdentity(string identity);

        #endregion
    }
}
