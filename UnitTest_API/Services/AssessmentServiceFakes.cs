using BehaviourManagementSystem_API.Models;
using BehaviourManagementSystem_API.Services;
using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTest_API.Services
{
    class AssessmentServiceFakes : IAssessmentService
    {
        private readonly List<Assessment> _assessment;
        private readonly List<Individual> _individual;
        private readonly List<Intervention> _intervention;

        public AssessmentServiceFakes()
        {
            _assessment = new List<Assessment>()
            {
                new Assessment()
                {
                    Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                    IndividualId = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c100"),
                    RecordDate = DateTime.Parse("3/3/2022"),
                    RecordStart = "7:00",
                    RecordEnd = "10:00",
                    RecordWhere = "Sân trường",
                    RecordWho = "Hoàng",
                    AnalyzeBehaviour = "Đánh nhau",
                    AnalyzeAntecedentPerceivedDescription = "Tức giận",
                    AnalyzeAntecedentEnvironmentalDescription = "Không có người",
                    AnalyzeAntecedentActivityDescription = "Đánh bạn",
                    AnalyzeConsequencesPerceive = "Kiểm soát",
                    AnalyzeConsequenceEnvironmental = "Đông người",
                    AnalyzeConsequencesActivity = "Bỏ trốn",
                    FunctionAntecedent = "Hoàng đánh bạn bị thương",
                    FunctionConsequece = "Sau đó chạy trốn",
                    CreateDate=DateTime.Now, 
                    UpdateDate=DateTime.Now
                }
            };

            _individual = new List<Individual>()
            {
                new Individual()
                {
                    Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c100"),
                }
            };

            _intervention = new List<Intervention>()
            {
                new Intervention()
                {
                    Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c300"),
                    AssesetmentId = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                }
            };
        }

        public async Task<ResponseResult<Assessment>> CreateRecord(string ind_id, DateTime r_date, string r_start, string r_end, string r_where, string r_who)
        {
            var a = new Assessment();
            if (! _individual.Any(prop => prop.Id.ToString() == ind_id))
                return new ResponseResultError<Assessment>("Id individual không tồn tại");
            if (r_date.ToString() == null || r_start == null || r_end == null)
            {
                return new ResponseResultError<Assessment>("Chưa có dữ liệu");
            }
            else
            {
                _assessment.Add(a = new Assessment()
                {
                    Id = Guid.NewGuid(),
                    RecordDate = r_date,
                    RecordStart = r_start,
                    RecordEnd = r_end,
                    RecordWhere = r_where,
                    RecordWho = r_who,
                    IndividualId = new Guid(ind_id),
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    RecordIsCompeleted = true

                });
            }

            return new ResponseResultSuccess<Assessment>(a);
        }

        public async Task<ResponseResult<List<Assessment>>> Delete(string ass_id)
        {
            if (!_assessment.Any(prop => prop.Id.ToString() == ass_id))
                return new ResponseResultError<List<Assessment>>("Id không tồn tại");
            var obj =  _assessment.Find(prop => prop.Id.ToString() == ass_id);
            var inter =  _intervention.Where(i => i.AssesetmentId == obj.Id).ToList();
            foreach (var item in inter)
            {
                _intervention.Remove(item);
            }
            _assessment.Remove(obj);
            return new ResponseResultSuccess<List<Assessment>>( _assessment.ToList());
        }

        public async Task<ResponseResult<string>> DeleteAllAssessWithInd(string id)
        {
            try
            {
                foreach (var item in _assessment
                .Where(prop => prop.IndividualId.ToString() == id)
                .ToList())
                {
                    item.IndividualId = null;
                }
                return new ResponseResultSuccess<string>("Xóa thành công");
            }
            catch (Exception ex)
            {
                return new ResponseResultError<string>(ex.Message);
            }
        }

        public async Task<ResponseResult<AssessmentRequest>> Detail(string ass_id)
        {
            if (! _assessment.Any(prop => prop.Id.ToString() == ass_id))
                return new ResponseResultError<AssessmentRequest>("Id assessment không tồn tại");
            var obj = _assessment.Find(prop => prop.Id.ToString() == ass_id);
            return new ResponseResultSuccess<AssessmentRequest>(new AssessmentRequest()
            {
                Id = obj.Id.ToString(),
                RecordDate = obj.RecordDate.GetValueOrDefault(),
                RecordStart = obj.RecordStart,
                RecordEnd = obj.RecordEnd,
                RecordDuring = obj.RecordDuring,
                RecordWhere = obj.RecordWhere,
                RecordWho = obj.RecordWho,
                AnalyzeAntecedentPerceivedDescription = obj.AnalyzeAntecedentPerceivedDescription,
                AnalyzeAntecedentEnvironmentalDescription = obj.AnalyzeAntecedentEnvironmentalDescription,
                AnalyzeAntecedentActivityDescription = obj.AnalyzeAntecedentActivityDescription,
                AnalyzeConsequencesPerceive = obj.AnalyzeConsequencesPerceive,
                AnalyzeConsequenceEnvironmental = obj.AnalyzeConsequenceEnvironmental,
                AnalyzeConsequencesActivity = obj.AnalyzeConsequencesActivity,
                AnalyzeBehaviour = obj.AnalyzeBehaviour,
                FunctionAntecedent = obj.FunctionAntecedent,
                FunctionConsequece = obj.FunctionConsequece,
                IndividualId = obj.IndividualId.ToString()
            });
        }

        public async Task<ResponseResult<List<AssessmentRequest>>> GetAll(string ind_id)
        {
            var find = _assessment.Where(p => p.IndividualId.ToString() == ind_id);
            var assessment = _assessment.Take(find.Count());
            if (assessment.Any() == false)
            {
                return new ResponseResultError<List<AssessmentRequest>>("Hiện tại không có dữ liệu");
            }
            var result = new List<AssessmentRequest>();
            foreach (var item in find)
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

        public Task<ResponseResult<Assessment>> UpdateAnalyzeAntecedent(string ass_id, string ana_ant_per, string ana_ant_envi, string ana_ant_act)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseResult<Assessment>> UpdateAnalyzeBehaviour(string ass_id, string ana_behaviour)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseResult<Assessment>> UpdateAnalyzeConsequence(string ass_id, string ana_con_per, string ana_con_envi, string ana_con_act)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseResult<Assessment>> UpdateFuntionAntecedent(string ass_id, string fun_ant)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseResult<Assessment>> UpdateFuntionConsequece(string ass_id, string fun_con)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseResult<Assessment>> UpdateRecord(string ass_id, DateTime r_date, string r_start, string r_end, string r_where, string r_who)
        {
            if (! _assessment.Any(prop => prop.Id.ToString() == ass_id))
                return new ResponseResultError<Assessment>("Id assessment không tồn tại");
            var obj = _assessment.Find(prop => prop.Id.ToString() == ass_id);
            if (r_date.ToString() == null || r_start == null || r_end == null)
            {
                return new ResponseResultError<Assessment>("Chưa có dữ liệu");
            }
            else
            {
                obj.RecordDate = r_date;
                obj.RecordStart = r_start;
                obj.RecordEnd = r_end;
                obj.RecordWhere = r_where;
                obj.RecordWho = r_who;
                obj.UpdateDate = DateTime.Now;
            }
            return new ResponseResultSuccess<Assessment>(obj);
        }
    }
}
