using SmartSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.ViewModel
{
    public class ComplaintVM
    {
        public string ParentId { get; set; } = string.Empty;

        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}
