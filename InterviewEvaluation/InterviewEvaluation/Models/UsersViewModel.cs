using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewEvaluation.Models
{
    public class UsersListViewModel
    {
        public List<EntityModels.AspNetUser> users { get; set; }
    }

    public class UsersEditViewModel
    {
        public string Email { get; set; }

        public int Id { get; set; }


        public int UserRole { get; set; }


        public string NewPassword { get; set; }
    }
}
