﻿using BehaviourManagementSystem_API.Data.EF;
using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BehaviourManagementSystem_API.Services
{
    public class AssessmentService : IAssessmentService
    {
        private readonly ApplicationDbContext _context;

        public AssessmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseResult<AssessmentRequest>> Detail(string id)
        {
            if (!await _context.Assesetments.AnyAsync(prop => prop.Id.ToString() == id))
                return new ResponseResultError<AssessmentRequest>("Id không tồn tại");
            var obj = await _context.Assesetments.FindAsync(new Guid(id));
            return new ResponseResultSuccess<AssessmentRequest>(new AssessmentRequest()
            {
                Id = obj.Id.ToString(),
                RecordDate = obj.RecordDate.GetValueOrDefault(),
                RecordStart = obj.RecordStart,
                RecordEnd = obj.RecordEnd,
                RecordDuring = obj.RecordDuring,
                RecordWhere = obj.RecordWhere,
                RecordWho = obj.RecordWho,
                AnalyzeAntecedentActivityDescription = obj.AnalyzeAntecedentActivityDescription,
                AnalyzeAntecedentEnvironmentalDescription = obj.AnalyzeAntecedentEnvironmentalDescription,
                AnalyzeAntecedentPerceivedDescription = obj.AnalyzeAntecedentPerceivedDescription,
                AnalyzeBehaviour = obj.AnalyzeBehaviour,
                FunctionAntecedent = obj.FunctionAntecedent,
                FunctionConsequece = obj.FunctionConsequece,
            });
        }

        public async Task<ResponseResult<List<AssessmentRequest>>> GetAll(string individualId)
        {
            var find = _context.Assesetments.Where(p => p.IndividualId.ToString() == individualId);        
            var assessment = _context.Assesetments.Take(find.Count());
            if (await assessment.AnyAsync() == false)
            {
                return new ResponseResultError<List<AssessmentRequest>>("Hiện tại không có dữ liệu");
            }
            var result = new List<AssessmentRequest>();
            foreach (var item in assessment)
            {             
                result.Add(new AssessmentRequest()
                {
                    Id = item.Id.ToString(),
                    RecordDate = item.RecordDate.GetValueOrDefault(),
                    RecordStart = item.RecordStart,
                    RecordIsCompeleted = item.RecordIsCompeleted,
                    AnalyzeIsCompeleted = item.AnalyzeIsCompeleted,
                    FunctionIsCompeleted = item.FunctionIsCompeleted,
                    AnalyzeBehaviour = item.AnalyzeBehaviour,
                    IndividualId = item.IndividualId.ToString(),

                });
            }
            return new ResponseResultSuccess<List<AssessmentRequest>>(result);
        }
    }
}