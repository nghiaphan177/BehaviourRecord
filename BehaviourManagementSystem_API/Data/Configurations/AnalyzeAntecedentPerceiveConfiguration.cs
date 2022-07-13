﻿using BehaviourManagementSystem_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BehaviourManagementSystem_API.Data.Configurations
{
    /// <summary>
    /// Writer: DuyLh4
    /// </summary>
    public class AnalyzeAntecedentPerceiveConfiguration : IEntityTypeConfiguration<AnalyzeAntecedentPerceive>
    {
        public void Configure(EntityTypeBuilder<AnalyzeAntecedentPerceive> builder)
        {
            builder.HasKey(prop => prop.Id);
        }
    }
}
