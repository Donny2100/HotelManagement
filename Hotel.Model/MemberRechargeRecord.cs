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
    /// 会员充值记录
    /// </summary>
    [TableName("member_recharge_record"), PrimaryKey("Id")]
    public class MemberRechargeRecord : BaseModel
    {
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
        /// 支付方式
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long PayTypeId { set; get; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public string PayTypeName { set; get; }

        /// <summary>
        /// 收款金额
        /// </summary>
        public int SkMoney { set; get; }

        /// <summary>
        /// 充值金额
        /// </summary>
        public int CzMoney { set; get; }

        /// <summary>
        /// 赠送金额
        /// </summary>
        public int ZsMoney { set; get; }

        /// <summary>
        /// 赠送积分
        /// </summary>
        public int ZsExp { set; get; }

        /// <summary>
        /// 操作员
        /// </summary>
        [Display(Name = "操作员")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long HandlerId { set; get; }

        /// <summary>
        /// 操作员
        /// </summary>
        [Display(Name = "操作员")]
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
    }
}
