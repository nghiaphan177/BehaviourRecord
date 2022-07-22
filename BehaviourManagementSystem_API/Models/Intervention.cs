using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BehaviourManagementSystem_API.Models
{
    public class Intervention
    {
        public Guid Id { get; set; }
        [Column(TypeName = "date")]
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

        public Guid? AssesetmentId { get; set; }
        public Guid? ProfileMildId { get; set; }
        public Guid? ProfileModerateId { get; set; }
        public Guid? ProfileExtremeId { get; set; }
        public Guid? ProfileRecoveryId { get; set; }

        public virtual Assessment Assesetment { get; set; } = null!;
        public virtual ProfileMild ProfileMild { get; set; } = null!;
        public virtual ProfileModerate ProfileModerate { get; set; } = null!;
        public virtual ProfileExtreme ProfileExtreme { get; set; } = null!;
        public virtual ProfileRecovery ProfileRecovery { get; set; } = null!;
    }
}