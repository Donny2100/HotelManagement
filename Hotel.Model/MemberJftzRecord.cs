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
    /// 积分调整
    /// </summary>
    [TableName("member_jftz_record"), PrimaryKey("Id")]
    public class MemberJftzRecord : BaseModel
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
        /// 调整积分  整数为加，负数为减
        /// </summary>
        public int TzExp { set; get; }

        /// <summary>
        /// 原因
        /// </summary>
        public string Remark { set; get; }

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

        #endregion

        #region 扩展属性



        #endregion

        #region 方法


        #endregion
    }
}
