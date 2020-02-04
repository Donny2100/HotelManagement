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
    /// 协议单位收退款记录
    /// </summary>
    [TableName("agree_comp_stk_record"), PrimaryKey("Id")]
    public class AgreeCompStkRecord : BaseModel
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
        /// 金额
        /// </summary>
        [Display(Name = "金额")]
        public decimal Money { set; get; }

        /// <summary>
        /// 单据号--人工录入或系统生成  若人工没有录入，则系统生成
        /// </summary>
        [Display(Name = "单据号")]
        public string DjNum { set; get; }

        /// <summary>
        /// 用于记录最大单据号的年月日
        /// </summary>
        public long Nyr { set; get; }

        /// <summary>
        /// 用于记录最大单据号
        /// </summary>
        public long MaxDjNum { set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { set; get; }

        /// <summary>
        /// 发生时间
        /// </summary>
        [Display(Name = "发生时间")]
        public DateTime FsDate { set; get; }

        /// <summary>
        /// 类型  1：收款   2：退款
        /// </summary>
        public int RType { set; get; }

        /// <summary>
        /// 操作人
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long HandlerId { set; get; }

        /// <summary>
        /// 操作人
        /// </summary>
        [Display(Name = "操作人")]
        public string HandlerName { set; get; }

        /// <summary>
        /// 酒店Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long HotelId { set; get; }

        /// <summary>
        /// 更新日期
        /// </summary>
        public int CDate { set; get; }

        #endregion

        #region 扩展属性



        #endregion

        #region 方法


        #endregion 
    }
}
