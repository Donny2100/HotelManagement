using MC.ORM;
using Newtonsoft.Json;
using NIU.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{
    /// <summary>
    /// 客户佣金记录   每日夜审时计算  需要另开一个定时任务  获取佣金计算命令   并执行计算
    /// </summary>
    [TableName("agree_comp_commision_record"), PrimaryKey("Id")]
    public class AgreeCompCommisionRecord : BaseModel
    {
        #region 属性

        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 账号
        /// </summary>
        public string AgreeCompId { set; get; }

        /// <summary>
        /// 全称
        /// </summary>
        [Display(Name = "全称")]
        public string AgreeCompName { set; get; }

        /// <summary>
        /// 简称
        /// </summary>
        [Display(Name = "简称")]
        public string AgreeCompShortName { set; get; }

        /// <summary>
        /// 需返佣金  --每日统计的佣金总额   并不是汇总，是每日的  每日夜审时统计的
        /// </summary>
        [Display(Name = "需返佣金")]
        public decimal XfCommision { set; get; }

        /// <summary>
        /// 实际返佣
        /// </summary>
        [Display(Name = "实际返佣")]
        public decimal SjfCommision { set; get; }

        /// <summary>
        /// 返佣状态  0：未返佣   1：已返佣   
        /// </summary>
        [Display(Name = "返佣状态")]
        public int State { set; get; }

        /// <summary>
        /// 单据号
        /// </summary>
        [Display(Name = "单据号")]
        public string DjNum { set; get; }

        /// <summary>
        /// 发票号
        /// </summary>
        [Display(Name = "发票号")]
        public string FpNum { set; get; }

        /// <summary>
        /// 支付方式
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long PayTypeId { set; get; }

        /// <summary>
        /// 支付方式
        /// </summary>
        [Display(Name = "支付方式")]
        public string PayTypeName { set; get; }

        /// <summary>
        /// 返佣时间
        /// </summary>
        [Display(Name = "返佣时间")]
        public int FyDate { set; get; }

        /// <summary>
        /// 是否每日记拥  --冗余客户协议主项数据
        /// </summary>
        [Display(Name = "是否每日记拥")]
        public bool IsMrjy { set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { set; get; }

        /// <summary>
        /// 佣金计算操作人
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long YjjsHandlerId { set; get; }

        /// <summary>
        /// 佣金计算操作人
        /// </summary>
        [Display(Name = "佣金计算操作人")]
        public string YjjsHandlerName { set; get; }

        /// <summary>
        /// 佣金返还操作人
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long YjFhHandlerId { set; get; }

        /// <summary>
        /// 佣金返还操作人
        /// </summary>
        [Display(Name = "佣金返还操作人")]
        public string YjFhHandlerName { set; get; }

        /// <summary>
        /// 酒店Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long HotelId { set; get; }

        /// <summary>
        /// 记拥日期
        /// </summary>
        public int CDate { set; get; }

        #endregion

        #region 扩展属性



        #endregion

        #region 方法


        #endregion 
    }
}
