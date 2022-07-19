using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_ViewModels.Requests
{
    public class IndividualRequest
    {
        public string Id { get; set; }
        public int? RecordIncident { get; set; }
        public string Organization { get; set; } = null!;

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public string UserId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }
        public string PhoneIndividual { get; set; }

        public string Gender { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Teacher { get; set; }


    }
}
