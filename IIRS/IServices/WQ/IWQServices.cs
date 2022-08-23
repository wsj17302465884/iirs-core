using IIRS.IServices.Base;
using IIRS.Models.ViewModel.WQ;

namespace IIRS.IServices.WQ
{
    public interface IWQServices : IBaseServices
    {
        /// <summary>
        /// 获取网签数据
        /// </summary>
        /// <returns></returns>
        MessageResult GetResult(string xm,string sfzh,string htbh,string cxrxm,string cxrzjhm,string dw);
    }
}
