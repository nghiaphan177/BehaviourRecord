using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_ViewModels.Requests
{
    public class IndAssessRequest
    {
        public string Ind_Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvtName { get; set; }
        public string Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string Address { get; set; }
        public string Classes { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string TeacherId { get; set; }
    }
}
