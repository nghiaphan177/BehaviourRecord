﻿using System;
using System.Collections.Generic;

namespace BehaviourManagementSystem_API.Models
{
    /// <summary>
    /// Create model Assesetment
    /// Writer: DuyLH4
    /// Desciption: use to interact with the database
    /// </summary>
    public class Assesetment
    {
        public Guid Id { get; set; }
        public DateTime? RecordDate { get; set; }
        public TimeSpan? RecordStart { get; set; }
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

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public Guid? IndividualId { get; set; }
        public Guid? AnalyzeAntecedentPerceivedId { get; set; }
        public Guid? AnalyzeAntecedentEnvironmentalId { get; set; }
        public Guid? AnalyzeAntecedentActivityId { get; set; }

        public virtual Individual Individual { get; set; } = null!;
        public virtual AnalyzeAntecedentPerceive AnalyzeAntecedentPerceive { get; set; } = null!;
        public virtual AnalyzeAntecedentEnvironmental AnalyzeAntecedentEnvironmental { get; set; } = null!;
        public virtual AnalyzeAntecedentActivity AnalyzeAntecedentActivity { get; set; } = null!;

        public virtual ICollection<Intervention> Interventions { get; set; } = null!;
    }
}