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
    /// 单据设置
    /// </summary>
    [TableName("bill_set"), PrimaryKey("Id")]
    public class BillSet : BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 入住单结尾自定义信息
        /// </summary>
        [Display(Name = "入住单结尾自定义信息")]
        public string CheckInEndInfo { set; get; }

        /// <summary>
        /// 结帐单结尾自定义信息
        /// </summary>
        [Display(Name = "结帐单结尾自定义信息")]
        public string BillingEndInfo { set; get; }

        /// <summary>
        /// 预订单结尾自定义信息
        /// </summary>
        [Display(Name = "预订单结尾自定义信息")]
        public string PreOrderEndInfo { set; get; }

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
