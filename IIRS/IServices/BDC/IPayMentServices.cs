using IIRS.Models.ViewModel.IIRS;
using System.Threading.Tasks;

namespace IIRS.IServices.BDC
{
    public interface IPayMentServices
    {
        /// <summary>
        /// 生成电子缴款码
        /// </summary>
        /// <param name="slbh">受理编号</param>
        /// <returns></returns>
        Task<string> GenerateTollCode(string slbh);
        /// <summary>
        /// 生成缴款书支付二维码URL
        /// </summary>
        /// <param name="slbh">受理编号</param>
        /// <returns></returns>
        Task<string> GetPayQrCode(string slbh);

        /// <summary>
        /// 根据缴款码获取缴款情况
        /// </summary>
        /// <param name="slbh">受理编号</param>
        /// <returns></returns>
        Task<string> GetPayResult(string slbh, string userName);
        /// <summary>
        /// 根据缴款码获取电子票据信息
        /// </summary>
        /// <param name="slbh"></param>
        /// <returns></returns>
        Task<string> GetGenerateTicket(string slbh);
        int SubmitPayHome();
    }
}
