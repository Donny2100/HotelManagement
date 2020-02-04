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
    /// 会员消费记录
    /// </summary>
    [TableName("member_fee_record"), PrimaryKey("Id")]
    public class MemberFeeRecord : BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 会员Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long MemberId { set; get; }

        /// <summary>
        /// 会员姓名
        /// </summary>
        public string MemberName { set; get; }

        /// <summary>
        /// 消费金额
        /// </summary>
        public decimal Money { set; get; }

        /// <summary>
        /// 消费类型   1：会员正常消费   2、部分结账   3、结账
        /// </summary>
        public int XfType{ set; get; }

        /// <summary>
        /// 部分结账单id或结账单id，XfType为2或3时才有，方便撤销
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long BfjzdOrJzdId { set; get; }

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
