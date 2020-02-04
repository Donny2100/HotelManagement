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
    /// 协议单位
    /// </summary>
    [TableName("agree_company"), PrimaryKey("Id")]
    public class AgreeCompany : BaseModel
    {
        #region 属性

        /// <summary>
        /// 账号  系统生成的唯一的id--账号
        /// </summary>
        [Display(Name = "账号")]
        public string Id { set; get; }

        /// <summary>
        /// 客户编号
        /// </summary>
        [Display(Name = "客户编号")]
        public string Num { set; get; }

        /// <summary>
        /// 客户全称
        /// </summary>
        [Display(Name = "客户全称")]
        public string Name { set; get; }

        public string Spell { set; get; }

        /// <summary>
        /// 客户简称
        /// </summary>
        [Display(Name = "客户简称")]
        public string ShortName { set; get; }

        /// <summary>
        /// 系统类型
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long XtlxId { set; get; }

        /// <summary>
        /// 系统类型
        /// </summary>
        [Display(Name = "系统类型")]
        public string XtlxName { set; get; }

        /// <summary>
        /// 客户类型
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long KhlxId { set; get; }

        /// <summary>
        /// 客户类型
        /// </summary>
        [Display(Name = "客户类型")]
        public string KhlxName { set; get; }

        /// <summary>
        /// 主联系人
        /// </summary>
        [Display(Name = "主联系人")]
        public string ContactName { set; get; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Display(Name = "手机号")]
        public string ContactTel { set; get; }

        /// <summary>
        /// 状态
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long KhztId { set; get; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        public string KhztName { set; get; }

        /// <summary>
        /// 销售人员
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long SalerId { set; get; }

        /// <summary>
        /// 销售人员
        /// </summary>
        [Display(Name = "销售人员")]
        public string SalerName { set; get; }

        /// <summary>
        /// 前台是否可查询
        /// </summary>
        [Display(Name = "前台是否可查询")]
        public bool IsAllowQtSearch { set; get; }

        /// <summary>
        /// 是否返佣单位
        /// </summary>
        [Display(Name = "是否返佣单位")]
        public bool IsFyCompany { set; get; }

        /// <summary>
        /// 地区
        /// </summary>
        [Display(Name = "地区")]
        public string Area { set; get; }

        /// <summary>
        /// 邮编
        /// </summary>
        [Display(Name = "邮编")]
        public string Zipcode { set; get; }

        /// <summary>
        /// 地址
        /// </summary>
        [Display(Name = "地址")]
        public string Address { set; get; }

        /// <summary>
        /// 公司电话
        /// </summary>
        [Display(Name = "公司电话")]
        public string GsTel { set; get; }

        /// <summary>
        /// 传真
        /// </summary>
        [Display(Name = "传真")]
        public string Fax { set; get; }

        /// <summary>
        /// Email
        /// </summary>
        [Display(Name = "Email")]
        public string Email { set; get; }

        /// <summary>
        /// 主页
        /// </summary>
        [Display(Name = "主页")]
        public string WebSite { set; get; }

        /// <summary>
        /// 客户来源
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long GuestSourceId { set; get; }

        /// <summary>
        /// 客户来源
        /// </summary>
        [Display(Name = "客户来源")]
        public string GuestSourceName { set; get; }

        /// <summary>
        /// 客户行业
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long KhhyId { set; get; }

        /// <summary>
        /// 客户行业
        /// </summary>
        [Display(Name = "客户行业")]
        public string KhhyName { set; get; }

        /// <summary>
        /// 是否不允许该单位记账--赊账
        /// </summary>
        [Display(Name = "不允许该单位记账")]
        public bool IsNotAllowJz { set; get; }

        /// <summary>
        /// 记账限额
        /// </summary>
        [Display(Name = "不允许该单位记账")]
        public decimal JzLimitMoney { set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { set; get; }

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
        /// 审核人
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long ConfirmId { set; get; }

        /// <summary>
        /// 审核人
        /// </summary>
        [Display(Name = "审核人")]
        public string ConfirmName { set; get; }

        /// <summary>
        /// 审核状态  0：未审核  1：已审核
        /// </summary>
        [Display(Name = "审核状态")]
        public int ConfirmState { set; get; }
        
        /// <summary>
        /// 应收账款
        /// </summary>
        [Display(Name = "应收账款")]
        public decimal Yszk { set; get; }

        /// <summary>
        /// 余额
        /// </summary>
        [Display(Name = "余额")]
        public decimal LeftMoney { set; get; }

        /// <summary>
        /// 已结算
        /// </summary>
        [Display(Name = "已结算")]
        public decimal Yjs { set; get; }

        /// <summary>
        /// 累计消费总额
        /// </summary>
        [Display(Name = "累计消费总额")]
        public decimal Ljxfze { set; get; }

        /// <summary>
        /// 累计入住天数
        /// </summary>
        [Display(Name = "累计入住天数")]
        public int Ljrzts { set; get; }

        /// <summary>
        /// 累计NoShow次数
        /// </summary>
        [Display(Name = "累计NoShow次数")]
        public int Ljnscs { set; get; }

        /// <summary>
        /// 累计取消预订
        /// </summary>
        [Display(Name = "累计取消预订")]
        public int Ljqxyd { set; get; }

        /// <summary>
        /// 同类客户排名
        /// </summary>
        [Display(Name = "同类客户排名")]
        public int Tlkhpm { set; get; }

        /// <summary>
        /// 未结算佣金
        /// </summary>
        [Display(Name = "未结算佣金")]
        public decimal Wjsyj { set; get; }

        /// <summary>
        /// 已结算佣金
        /// </summary>
        [Display(Name = "已结算佣金")]
        public decimal Yjsyj { set; get; }

        /// <summary>
        /// 酒店Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long HotelId { set; get; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public int CDate { set; get; }

        #endregion

        #region 扩展属性



        #endregion

        #region 方法


        #endregion
    }
}
