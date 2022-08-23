using IIRS.IRepository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Utilities.FlowHelper
{
    public class FlowHelper
    {
        //public void CreateFlowInstance(FlowType flowType)
        //{
        //    // 这里使用事务处理

        //    var data = new MessageModel<string>();
        //    try
        //    {
        //        _dBTransManagement.BeginTran();

        //        if (userInfo != null && userInfo.Id != Guid.Empty)
        //        {
        //            if (userInfo.RIDs.Count > 0)
        //            {
        //                // 无论 Update Or Add , 先删除当前用户的全部 U_R 关系
        //                var usreroles = (await _userRoleRepository.Query(d => d.UserId == userInfo.Id)).Select(d => d.Id.ToString()).ToArray();
        //                if (usreroles.Count() > 0)
        //                {
        //                    var isAllDeleted = await _userRoleRepository.DeleteByIds(usreroles);
        //                }

        //                // 然后再执行添加操作
        //                var userRolsAdd = new List<UserRole>();
        //                userInfo.RIDs.ForEach(rid =>
        //                {
        //                    userRolsAdd.Add(new UserRole(userInfo.Id, rid));
        //                });

        //                await _userRoleRepository.Add(userRolsAdd);

        //            }

        //            data.success = await _userInfoRepository.Update(userInfo);

        //            _dBTransManagement.CommitTran();

        //            if (data.success)
        //            {
        //                data.msg = "更新成功";
        //                data.response = userInfo?.Id.ObjToString();
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        _dBTransManagement.RollbackTran();
        //        _logger.LogError(e, e.Message);
        //    }

        //    return data;
        //}
    }

    public enum FlowInfoEnum : int
    {
        DemoFlow = 0
            //DemoFlow=0,
            //讲师,
            //副教授,
    }

    //public class FlowEnum
    //{
        
    //}

}
