using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_ViewModels.Requests
{
    public class InterventionRequest
    {
        public string Id { get; set; }
        public DateTime? ProfileDate { get; set; }
        public string Summary { get; set; } = null!;
        public string ProfileMildDesciption { get; set; } = null!;
        public string ProfileModerateDesciption { get; set; } = null!;
        public string ProfileExtremeDesciption { get; set; } = null!;
        public string ProfileRecoveryDesciption { get; set; } = null!;
        public string ManageMild { get; set; } = null!;
        public string ManageModerate { get; set; } = null!;
        public string ManageExtreme { get; set; } = null!;
        public string ManageRecovery { get; set; } = null!;
        public string PreventStatus { get; set; } = null!;
        public string PreventActivity { get; set; } = null!;
        public string PreventInteraction { get; set; } = null!;
        public string PreventInvironmental { get; set; } = null!;

        public bool? ProfileIsCompeleted { get; set; }
        public bool? ManageIsCompleted { get; set; }
        public bool? PreventIsCompleted { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public string AssesetmentId { get; set; }
        public string ProfileMildId { get; set; }
        public string ProfileModerateId { get; set; }
        public string ProfileExtremeId { get; set; }
        public string ProfileRecoveryId { get; set; }
    }
}
