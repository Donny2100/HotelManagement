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
    /// 协议单位未结算明细 数据来源有  1：商品入账  2：费用入账  3：记账   4：转账
    /// </summary>
    [TableName("agree_comp_wjsmx_record"), PrimaryKey("Id")]
    public class AgreeCompWjsmxRecord : BaseModel
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
        /// 房间号  只有RType为  记账  的时候才会有房号
        /// </summary>
        [Display(Name = "房间号")]
        public string RoomNO { set; get; }

        /// <summary>
        /// 费用名称
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long CostId { set; get; }

        /// <summary>
        /// 费用名称
        /// </summary>
        [Display(Name = "费用名称")]
        public string CostName { set; get; }

        /// <summary>
        /// 金额
        /// </summary>
        [Display(Name = "金额")]
        public decimal Money { set; get; }

        /// <summary>
        /// 红冲金额
        /// </summary>
        [Display(Name = "红冲金额")]
        public decimal HcMoney { set; get; }

        /// <summary>
        /// 单据号--人工录入
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
        /// 类型  1：商品入账  2：费用入账  3：记账   4：转账  5：部分结账记账    6：结账记账
        /// </summary>
        public int RType { set; get; }

        /// <summary>
        /// 类型
        /// </summary>
        [Ignore]
        public string RTypeName
        {
            get
            {
                switch (RType)
                {
                    case 1:
                        return "商品入账";
                    case 2:
                        return "费用入账";
                    case 3:
                        return "记账";
                    case 4:
                        return "转账";
                    default:
                        return string.Empty;
                }
            }
        }

        /// <summary>
        /// 部分结账单id或结账单id，RType为5（记账时才有），方便撤销
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long BfjzdOrJzdId { set; get; }

        /// <summary>
        /// 记录操作人
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long RecordHandlerId { set; get; }

        /// <summary>
        /// 记录操作人
        /// </summary>
        [Display(Name = "记录操作人")]
        public string RecordHandlerName { set; get; }

        /// <summary>
        /// 是否已结算  0：未结算   1：已结算
        /// </summary>
        public int IsJs { set; get; }

        /// <summary>
        /// 结算操作人
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long JsHandlerId { set; get; }

        /// <summary>
        /// 结算操作人
        /// </summary>
        [Display(Name = "结算操作人")]
        public string JsHandlerName { set; get; }

        /// <summary>
        /// 结算时间
        /// </summary>
        public int JsDate { set; get; }

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

        /// <summary>
        /// 如果是商品入账才会有
        /// </summary>
        [Ignore]
        public List<AgreeCompWjsmxRecordDetailViewHelp> Details { set; get; }

        /// <summary>
        /// 剩余应收
        /// </summary>
        [Ignore]
        public decimal SyysMoney
        {
            get
            {
                return Money - HcMoney;
            }
        }

        #endregion

        #region 方法


        #endregion 
    }
}
