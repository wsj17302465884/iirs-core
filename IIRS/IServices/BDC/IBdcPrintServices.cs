using IIRS.IServices.Base;
using IIRS.Models.ViewModel.BDC.print;
using IIRS.Models.ViewModel.Print;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.IServices.BDC
{
    public interface IBdcPrintServices : IBaseServices
    {
        Task<TransferSpbPrintVModel> GetTransferSpbPrint(string xid);
        //Task<QlrInfoVModel> GetPersonList(string xid);
        Task<TransferSjsjPrintVModel> TransferSjsjPrint(string xid);
        Task<TransferSqbPrintVModel> TransferSqbPrint(string xid);
        Task<TransferSfdPrintVModel> TransferSfdPrint(string xid,string slbh);
        /// <summary>
        /// 抵押申请表打印
        /// </summary>
        /// <param name="xid"></param>
        /// <returns></returns>
        Task<DySqbPrintVModel> GeneralMrgeSqbPrint(string xid);

        #region 抵押打印
        /// <summary>
        /// 一般抵押 - 收件收据打印
        /// </summary>
        /// <param name="xid"></param>
        /// <returns></returns>
        Task<BdcGeneralMrgeSjsjPrintVModel> GeneralMrgeSjsjPrint(string xid);
        /// <summary>
        /// 一般抵押 - 清单打印
        /// </summary>
        /// <param name="xid"></param>
        /// <returns></returns>
        Task<BdcGeneralMrgeQdPrintVModel> BdcGeneralMrgeQdPrint(string xid);
        /// <summary>
        /// 抵押审批表
        /// </summary>
        /// <param name="xid"></param>
        /// <param name="slbh"></param>
        /// <returns></returns>
        Task<DySpbPrintVModel> GeneralMrgeSpbPrint(string xid,string slbh);
        /// <summary>
        /// 抵押收费单
        /// </summary>
        /// <param name="xid"></param>
         /// <param name="slbh">抵押受理编号</param>
        /// <returns></returns>
        Task<GeneralMrgeSfdPrintVModel> GeneralMrgeSfdPrint(string xid,string slbh);
        /// <summary>
        /// 抵押注销审批表
        /// </summary>
        /// <param name="xid"></param>
        /// <returns></returns>
        Task<MrgeReleaseSpbVModel> GeneralMrgeZXSqbPrint(string xid);
        #endregion

        #region 抵押注销打印
        /// <summary>
        /// 抵押注销申请表打印
        /// </summary>
        /// <param name="xid"></param>
        /// <returns></returns>
        Task<MrgeReleaseSqbVModel> MrgeReleaseSqbPrint(string xid);
        /// <summary>
        /// 抵押注销审批表
        /// </summary>
        /// <param name="xid"></param>
        /// <returns></returns>
        Task<MrgeReleaseSpbVModel> MrgeReleaseSpbPrint(string xid);
        /// <summary>
        /// 抵押注销清单表
        /// </summary>
        /// <param name="xid"></param>
        /// <returns></returns>
        Task<MrgeReleaseQdPrintVmodel> MrgeReleaseQdPrint(string xid);
        /// <summary>
        /// 抵押注销收件收据表
        /// </summary>
        /// <param name="xid"></param>
        /// <returns></returns>
        Task<MrgeReleaseSjPrintVModel> MrgeReleaseSjPrint(string xid);

        #endregion

        #region 预告抵押打印
        #endregion
        Task<DyYgSqbPrintVModel> DyYgSqbPrintPrint(string xid);
    }
}
