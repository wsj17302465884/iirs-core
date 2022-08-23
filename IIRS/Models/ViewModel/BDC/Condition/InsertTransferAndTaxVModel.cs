using IIRS.Models.ViewModel.TAX;

namespace IIRS.Models.ViewModel.BDC.Condition
{
    public class InsertTransferAndTaxVModel
    {
        /// <summary>
        /// 不动产业务实体类
        /// </summary>
        public InsertTransferVModel transferModel { get; set; } = new InsertTransferVModel();
        /// <summary>
        /// 税务实体类
        /// </summary>
        public InsertTaxVModel taxModel { get; set; } = new InsertTaxVModel();
    }
}
