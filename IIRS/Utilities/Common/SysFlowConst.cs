using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Utilities.Common
{
    public class SysFlowConst
    {
        #region 一般抵押

        /// <summary>
        /// 一般抵押
        /// </summary>
        public const int FLOW_YBDY = 1;

        /// <summary>
        /// 一般抵押--待银行受理
        /// </summary>
        public const int FLOW_YBDY_DYHSL = 1;

        /// <summary>
        /// 一般抵押-- 待不动产中心抵押审批
        /// </summary>
        public const int FLOW_YBDY_DBDCZXDYSP = 2;

        #endregion


        #region 抵押变更（注销）

        /// <summary>
        /// 抵押变更（注销）
        /// </summary>
        public const int FLOW_DYBGZX = 4;

        /// <summary>
        /// 抵押变更（注销）-- 待银行受理
        /// </summary>
        public const int FLOW_DYBGZX_DYHSL = 31;

        /// <summary>
        /// 抵押变更（注销）-- 待不动产中心抵押审批
        /// </summary>
        public const int FLOW_DYBGZX_DBDCZXDYSP = 32;

        #endregion
    }
}
