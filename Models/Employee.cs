using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Models
{
    public class Employee
    {
        public int? id { get; set; }
        public string Firstname { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public int? Age { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public string Joineddate { get; set; }
        public string View { get; set; }
        public string Edit { get; set; }
        public string Delete { get; set; }
    }
}
