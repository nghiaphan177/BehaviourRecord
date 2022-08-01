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
    class ProfileRecoveryServiceFakes : IProfileRecoveryService
    {
        private readonly List<ProfileRecovery> _profilerecovery;
        public ProfileRecoveryServiceFakes()
        {
            _profilerecovery = new List<ProfileRecovery>()
            {
                new ProfileRecovery() { Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                    Content = "UnitTest1", CreateDate=DateTime.Now, UpdateDate=DateTime.Now},
                new ProfileRecovery() { Id = new Guid("815accac-fd5b-478a-a9d6-f171a2f6ae7f"),
                    Content = "UnitTest2", CreateDate=DateTime.Now, UpdateDate=DateTime.Now},
                new ProfileRecovery() { Id = new Guid("33704c4a-5b87-464c-bfb6-51971b4d18ad"),
                    Content = "UnitTest3", CreateDate=DateTime.Now, UpdateDate=DateTime.Now}
            };
        }
        public ProfileRecoveryServiceFakes(List<ProfileRecovery> profilerecovery)
        {
            _profilerecovery = profilerecovery;
        }

        public async Task<ResponseResult<List<ProfileRecovery>>> Create(string content)
        {
            if (_profilerecovery.Any(prop => prop.Content == content))
                return new ResponseResultError<List<ProfileRecovery>>("Dữ liệu đã tồn tại");
            _profilerecovery.Add(new ProfileRecovery()
            {
                Id = Guid.NewGuid(),
                Content = content,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            });
            return new ResponseResultSuccess<List<ProfileRecovery>>(_profilerecovery.ToList());
        }

        public async Task<ResponseResult<List<ProfileRecovery>>> Delete(string id)
        {
            if (!_profilerecovery.Any(prop => prop.Id.ToString() == id))
                return new ResponseResultError<List<ProfileRecovery>>("Id không tồn tại");
            var obj = _profilerecovery.Find(a => a.Id.ToString() == id);
            _profilerecovery.Remove(obj);
            return new ResponseResultSuccess<List<ProfileRecovery>>(_profilerecovery.ToList());
        }

        public async Task<ResponseResult<List<OptionsRequest>>> GetAll()
        {
            if (!_profilerecovery.Any())
                return new ResponseResultError<List<OptionsRequest>>("Hiện tại không có dữ liệu");
            var mild = _profilerecovery.ToList();
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
            if (!_profilerecovery.Any(prop => prop.Id.ToString() == id))
                return new ResponseResultError<OptionsRequest>("Id không tồn tại");
            var obj = _profilerecovery.Find(a => a.Id.ToString() == id);
            return new ResponseResultSuccess<OptionsRequest>(new OptionsRequest()
            {
                Id = obj.Id.ToString(),
                Content = obj.Content,
                CreateDate = obj.CreateDate.Value,
                UpdateDate = obj.UpdateDate.GetValueOrDefault()
            });
        }

        public async Task<ResponseResult<List<ProfileRecovery>>> Update(string id, string content)
        {
            if (!_profilerecovery.Any(prop => prop.Id.ToString() == id))
                return new ResponseResultError<List<ProfileRecovery>>("Id không tồn tại");
            if (_profilerecovery.Any(prop => prop.Content == content))
                return new ResponseResultError<List<ProfileRecovery>>("Dữ liệu đã tồn tại");
            var obj = _profilerecovery.Find(a => a.Id.ToString() == id);
            obj.Content = content;
            obj.UpdateDate = DateTime.Now;
            return new ResponseResultSuccess<List<ProfileRecovery>>(_profilerecovery.ToList());
        }
    }
}
