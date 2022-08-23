using IIRS.IServices.Base;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.IServices
{
    public interface IConstructionServices : IBaseServices
    {
        Task<List<DJ_QLR>> GetDyrInfo(string slbh);

        Task<List<FC_Z_QSDC>> GetFwbhList(string zdtybm);

        Task<int> Construction(REGISTRATION_INFO DjInfo, IFLOW_DO_ACTION flowInfo, List<TSGL_INFO> TsglInfo, DY_INFO ParcelInfo, List<XGDJGL_INFO> XgdjglInfos, List<QLRGL_INFO> qlrglInfos);

        Task<int> Certification(REGISTRATION_INFO DjInfo, IFLOW_DO_ACTION flowInfo, List<TSGL_INFO> TsglInfo, List<OrderHouseAssociation> ationList, DY_INFO ParcelInfo,List<XGDJGL_INFO> XgdjglInfos, List<QLRGL_INFO> qlrglInfos, List<PUB_ATT_FILE> attFileInfos);

        /// <summary>
        /// 获取系统受理编号
        /// </summary>
        /// <returns>受理编号</returns>
        string GetSLBH();

        Task<List<DJ_TSGL>> GetZdTstybmByZjhm(string zjhm, string bdczh, string qlrmc);
        
    }
}
