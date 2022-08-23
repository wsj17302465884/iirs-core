using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Utilities.Common
{
    public class SysBdcFlowConst
    {
        #region 抵押变更（注销）

        /// <summary>
        /// 抵押注销 - 待银行办理【不动产退回】
        /// 不动产中心受理退回银行
        /// </summary>
        public const int FLOW_DYZX_BANK = 116;

        /// <summary>
        /// 抵押注销 -  不动产中心受理银行
        /// 银行提交给不动产中心受理
        /// </summary>
        public const int FLOW_DYZX_SL_BANK = 117;

        /// <summary>
        /// 抵押注销 -  不动产中心受理
        /// 后续审核退回
        /// </summary>
        public const int FLOW_DYZX_SL = 118;

        /// <summary>
        /// 抵押注销 -  不动产中心审核
        /// 不动产中心受理退回银行
        /// </summary>
        public const int FLOW_DYZX_SH = 119;

        /// <summary>
        /// 抵押注销 -  不动产中心登薄
        /// </summary>
        public const int FLOW_DYZX_DB = 120;

        /// <summary>
        /// 抵押登记 -  不动产中心登薄
        /// </summary>
        public const int FLOW_DYZX_GROUP = 24;

        #endregion

        #region 抵押登记

        /// <summary>
        /// 抵押登记 - 待银行办理【不动产退回】
        /// 不动产中心受理退回银行
        /// </summary>
        public const int FLOW_DYDJ_BANK = 111;

        /// <summary>
        /// 抵押登记 -  不动产中心受理银行
        /// 银行提交给不动产中心受理
        /// </summary>
        public const int FLOW_DYDJ_SL_BANK = 112;

        /// <summary>
        /// 抵押登记 -  不动产中心受理
        /// 后续审核退回
        /// </summary>
        public const int FLOW_DYDJ_SL = 113;

        /// <summary>
        /// 抵押登记 -  不动产中心审核
        /// 不动产中心受理退回银行
        /// </summary>
        public const int FLOW_DYDJ_SH = 114;

        /// <summary>
        /// 抵押登记 -  不动产中心登薄
        /// </summary>
        public const int FLOW_DYDJ_DB = 115;

        /// <summary>
        /// 抵押登记 -  不动产中心登薄
        /// </summary>
        public const int FLOW_DYDJ_GROUP = 23;


        #endregion

        #region 预告抵押

        /// <summary>
        /// 预告抵押 - 待银行办理【不动产退回】
        /// 不动产中心受理退回银行
        /// </summary>
        public const int FLOW_YGDY_BANK = 126;

        /// <summary>
        /// 预告抵押 -  不动产中心受理银行
        /// 银行提交给不动产中心受理
        /// </summary>
        public const int FLOW_YGDY_SL_BANK = 127;

        /// <summary>
        /// 预告抵押 -  不动产中心受理
        /// 后续审核退回
        /// </summary>
        public const int FLOW_YGDY_SL = 128;

        /// <summary>
        /// 预告抵押 -  不动产中心审核
        /// 不动产中心受理退回银行
        /// </summary>
        public const int FLOW_YGDY_SH = 129;

        /// <summary>
        /// 预告抵押 -  不动产中心登薄
        /// </summary>
        public const int FLOW_YGDY_DB = 130;

        /// <summary>
        /// 预告抵押 -  不动产中心登薄
        /// </summary>
        public const int FLOW_YGDY_GROUP = 26;


        #endregion

        #region 预告登记

        /// <summary>
        /// 预告登记 - 待银行办理【不动产退回】
        /// 不动产中心受理退回银行
        /// </summary>
        public const int FLOW_YGDJ_BANK = 121;

        /// <summary>
        /// 预告登记 -  不动产中心受理银行
        /// 银行提交给不动产中心受理
        /// </summary>
        public const int FLOW_YGDJ_SL_BANK = 122;

        /// <summary>
        /// 预告登记 -  不动产中心受理
        /// 后续审核退回
        /// </summary>
        public const int FLOW_YGDJ_SL = 123;

        /// <summary>
        /// 预告登记 -  不动产中心审核
        /// 不动产中心受理退回银行
        /// </summary>
        public const int FLOW_YGDJ_SH = 124;

        /// <summary>
        /// 预告登记 -  不动产中心登薄
        /// </summary>
        public const int FLOW_YGDJ_DB = 125;

        /// <summary>
        /// 预告登记 -  不动产中心登薄
        /// </summary>
        public const int FLOW_YGDJ_GROUP = 25;


        #endregion

        #region 转移抵押合并办理

        /// <summary>
        /// 转移抵押 - 待银行办理【不动产退回】
        /// 不动产中心受理退回银行
        /// </summary>
        public const int FLOW_ZYDYHBBL_BANK = 100;

        /// <summary>
        /// 转移抵押 -  不动产中心受理银行
        /// 银行提交给不动产中心受理
        /// </summary>
        public const int FLOW_ZYDYHBBL_SL_BANK = 101;

        /// <summary>
        /// 转移抵押 -  不动产中心受理
        /// 后续审核退回
        /// </summary>
        public const int FLOW_ZYDYHBBL_SL = 102;

        /// <summary>
        /// 转移抵押 -  不动产中心审核
        /// 不动产中心受理退回银行
        /// </summary>
        public const int FLOW_ZYDYHBBL_SH = 103;

        /// <summary>
        /// 转移抵押 -  不动产中心登薄
        /// </summary>
        public const int FLOW_ZYDYHBBL_DB = 104;

        /// <summary>
        /// 转移抵押 -  不动产中心登薄
        /// </summary>
        public const int FLOW_ZYDYHBBL_GROUP = 21;

        #endregion
    }
}
