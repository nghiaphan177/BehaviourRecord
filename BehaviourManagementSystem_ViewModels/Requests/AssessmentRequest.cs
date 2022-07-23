using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TimeSpanConverter = BehaviourManagementSystem_MVC.Utilities.TimeSpanConverter;

namespace BehaviourManagementSystem_ViewModels.Requests
{
    public class AssessmentRequest
    {
        [Required]
        public string Id { get; set; }
        public DateTime? RecordDate { get; set; }
        [JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan? RecordStart { get; set; }
        [JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan? RecordEnd { get; set; }
        public string RecordDuring { get; set; } = null!;
        public string RecordWhere { get; set; } = null!;
        public string RecordWho { get; set; } = null!;
        public string AnalyzeBehaviour { get; set; } = null!;
        public string AnalyzeAntecedentPerceivedDescription { get; set; } = null!;
        public string AnalyzeAntecedentEnvironmentalDescription { get; set; } = null!;
        public string AnalyzeAntecedentActivityDescription { get; set; } = null!;
        public string AnalyzeConsequencesPerceive { get; set; } = null!;
        public string AnalyzeConsequenceEnvironmental { get; set; } = null!;
        public string AnalyzeConsequencesActivity { get; set; } = null!;
        public string FunctionAntecedent { get; set; } = null!;
        public string FunctionConsequece { get; set; } = null!;
        public bool? RecordIsCompeleted { get; set; }
        public bool? AnalyzeIsCompeleted { get; set; }
        public bool? FunctionIsCompeleted { get; set; }
        [Required]
        public string IndividualId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string AnalyzeAntecedentPerceivedId { get; set; }
        public string AnalyzeAntecedentEnvironmentalId { get; set; }
        public string AnalyzeAntecedentActivityId { get; set; }
    }
}
