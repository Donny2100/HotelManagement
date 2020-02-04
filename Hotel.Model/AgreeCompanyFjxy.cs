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
    /// 客户协议中的房价协议
    /// </summary>
    [TableName("agree_company_fjxy"), PrimaryKey("Id")]
    public class AgreeCompanyFjxy
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 账号
        /// </summary>
        public string AgreeCompId { set; get; }

        /// <summary>
        /// 房间类型id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long RoomTypeId { set; get; }

        /// <summary>
        /// 协议房价
        /// </summary>
        [Display(Name = "协议房价")]
        public decimal XyFj { set; get; }

        /// <summary>
        /// 月长包房价
        /// </summary>
        [Display(Name = "月长包房价")]
        public decimal Ycbfj { set; get; }

        /// <summary>
        /// 早餐份数
        /// </summary>
        [Display(Name = "早餐份数")]
        public int BreakfastCount { set; get; }

        /// <summary>
        /// 佣金
        /// </summary>
        [Display(Name = "佣金")]
        public decimal Commission { set; get; }

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
        /// 更新时间
        /// </summary>
        public int CDate { set; get; }

        #endregion

        #region 扩展属性

        /// <summary>
        /// 房间类型id
        /// </summary>
        [Ignore]
        public string RoomTypeName { set; get; }

        /// <summary>
        /// 额定房价
        /// </summary>
        [Ignore]
        public decimal EdfjPrice { set; get; }

        #endregion

        #region 方法


        #endregion
    }
}
