using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.ViewModel;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.Repository
{
    public class DetailsExamineRepository : BaseRepository<DetailsExamineVModel>, IDetailsExamineRepository
    {
        private readonly ILogger<DetailsExamineRepository> _logger;
        public DetailsExamineRepository(IDBTransManagement dbTransManagement, ILogger<DetailsExamineRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }

        /// <summary>
        /// 获取房子登记种类tree
        /// </summary>
        /// <param name="tstybm"></param>
        /// <returns></returns>
        public async Task<List<DJZLViewTree>> GetDetailsExamineTreeList(string tstybm)
        {
            var bdczh = "";
            var list = await base.Query(a => a.tstybm == tstybm);
            List<DJZLViewTree> rtn = new List<DJZLViewTree>();

            #region tree父级菜单
            DJZLViewTree qs = new DJZLViewTree
            {
                Lable = "权属"
            };
            DJZLViewTree dy = new DJZLViewTree
            {
                Lable = "抵押"
            };
            DJZLViewTree cf = new DJZLViewTree
            {
                Lable = "查封"
            };
            DJZLViewTree yy = new DJZLViewTree
            {
                Lable = "异议"
            };
            #endregion

            #region 业务
            foreach (var item in list)
            {
                if(item.djzl.Contains("权属"))
                {
                    bdczh = item.bdczh;
                }else if(item.djzl.Contains("抵押"))
                {
                    bdczh = item.bdczmh;
                }
                else if (item.djzl.Contains("查封"))
                {
                    bdczh = item.cfwh;
                }
                else if (item.djzl.Contains("异议"))
                {
                    bdczh = item.yybdczmh;
                }
                DJZLViewTree pchild = new DJZLViewTree
                {
                    Lable = item.slbh + "(" + bdczh + ")"
                };
                switch (item.djzl.Substring(0,2))
                {
                    case "权属":
                        {
                            qs.Children.Add(pchild);
                            break;
                        }
                    case "抵押":
                        {
                            dy.Children.Add(pchild);
                            break;
                        }
                    case "查封":
                        {
                            cf.Children.Add(pchild);
                            break;
                        }
                    case "异议":
                        {
                            yy.Children.Add(pchild);
                            break;
                        }
                    default:
                        break;
                }
            }
            #endregion

            rtn.Add(qs);
            rtn.Add(dy);
            rtn.Add(cf);
            rtn.Add(yy);
            return rtn;
        }
    }
    
}
