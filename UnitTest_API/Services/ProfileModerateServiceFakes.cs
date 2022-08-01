using BehaviourManagementSystem_API.Models;
using BehaviourManagementSystem_API.Services;
using BehaviourManagementSystem_ViewModels.Requests;
using BehaviourManagementSystem_ViewModels.Responses.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest_API.Services
{
    class ProfileModerateServiceFakes : IProfileModerateService
    {
        private readonly List<ProfileModerate> _profilemoderate;

        public ProfileModerateServiceFakes()
        {
            _profilemoderate = new List<ProfileModerate>()
            {
                new ProfileModerate() { Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                    Content = "UnitTest1", CreateDate=DateTime.Now, UpdateDate=DateTime.Now},
                new ProfileModerate() { Id = new Guid("815accac-fd5b-478a-a9d6-f171a2f6ae7f"),
                    Content = "UnitTest2", CreateDate=DateTime.Now, UpdateDate=DateTime.Now},
                new ProfileModerate() { Id = new Guid("33704c4a-5b87-464c-bfb6-51971b4d18ad"),
                    Content = "UnitTest3", CreateDate=DateTime.Now, UpdateDate=DateTime.Now}
            };
        }

        public ProfileModerateServiceFakes(List<ProfileModerate> profilemoderate)
        {
            _profilemoderate = profilemoderate;
        }

        public async Task<ResponseResult<List<ProfileModerate>>> Create(string content)
        {
            if (_profilemoderate.Any(prop => prop.Content == content))
                return new ResponseResultError<List<ProfileModerate>>("Dữ liệu đã tồn tại");
            _profilemoderate.Add(new ProfileModerate()
            {
                Id = Guid.NewGuid(),
                Content = content,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            });
            return new ResponseResultSuccess<List<ProfileModerate>>(_profilemoderate.ToList());
        }

        public async Task<ResponseResult<List<ProfileModerate>>> Delete(string id)
        {
            if (!_profilemoderate.Any(prop => prop.Id.ToString() == id))
                return new ResponseResultError<List<ProfileModerate>>("Id không tồn tại");
            var obj = _profilemoderate.Find(a => a.Id.ToString() == id);
            _profilemoderate.Remove(obj);
            return new ResponseResultSuccess<List<ProfileModerate>>(_profilemoderate.ToList());
        }

        public async Task<ResponseResult<List<OptionsRequest>>> GetAll()
        {
            if (!_profilemoderate.Any())
                return new ResponseResultError<List<OptionsRequest>>("Hiện tại không có dữ liệu");
            var mild = _profilemoderate.ToList();
            var result = new List<OptionsRequest>();
            int stt = 0;
            foreach (var item in mild)
            {
                result.Add(new OptionsRequest()
                {
                    STT = stt += 1,
                    Id = item.Id.ToString(),
                    Content = item.Content,
                    CreateDate = item.CreateDate.Value,
                    UpdateDate = item.UpdateDate.GetValueOrDefault()
                });
            }
            return new ResponseResultSuccess<List<OptionsRequest>>(result.ToList());
        }

        public async Task<ResponseResult<OptionsRequest>> GetById(string id)
        {
            if (!_profilemoderate.Any(prop => prop.Id.ToString() == id))
                return new ResponseResultError<OptionsRequest>("Id không tồn tại");
            var obj = _profilemoderate.Find(a => a.Id.ToString() == id);
            return new ResponseResultSuccess<OptionsRequest>(new OptionsRequest()
            {
                Id = obj.Id.ToString(),
                Content = obj.Content,
                CreateDate = obj.CreateDate.Value,
                UpdateDate = obj.UpdateDate.GetValueOrDefault()
            });
        }

        public async Task<ResponseResult<List<ProfileModerate>>> Update(string id, string content)
        {
            if (!_profilemoderate.Any(prop => prop.Id.ToString() == id))
                return new ResponseResultError<List<ProfileModerate>>("Id không tồn tại");
            if (_profilemoderate.Any(prop => prop.Content == content))
                return new ResponseResultError<List<ProfileModerate>>("Dữ liệu đã tồn tại");
            var obj = _profilemoderate.Find(a => a.Id.ToString() == id);
            obj.Content = content;
            obj.UpdateDate = DateTime.Now;
            return new ResponseResultSuccess<List<ProfileModerate>>(_profilemoderate.ToList());
        }
    }
}
