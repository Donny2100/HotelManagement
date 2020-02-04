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
    /// 会员升级记录
    /// </summary>
    [TableName("mem_uplevel_record"), PrimaryKey("Id")]
    public class MemUplevelRecord : BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 会员id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long MemberId { set; get; }

        /// <summary>
        /// 会员姓名
        /// </summary>
        [Display(Name = "会员姓名")]
        public string MemberName { set; get; }

        /// <summary>
        /// 原会员类型
        /// </summary>
        [Display(Name = "原会员类型")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long OldMemberTypeId { set; get; }

        /// <summary>
        /// 原会员类型
        /// </summary>
        [Display(Name = "原会员类型")]
        public string OldMemberTypeName { set; get; }

        /// <summary>
        /// 升级会员类型
        /// </summary>
        [Display(Name = "升级会员类型")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long MemberTypeId { set; get; }

        /// <summary>
        /// 升级会员类型
        /// </summary>
        [Display(Name = "升级会员类型")]
        public string MemberTypeName { set; get; }

        /// <summary>
        /// 扣除积分
        /// </summary>
        [Display(Name = "扣除积分")]
        public int KcExp { set; get; }

        /// <summary>
        /// 卡内剩余积分
        /// </summary>
        [Display(Name = "卡内剩余积分")]
        public int KnExp { set; get; }

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



        #endregion

        #region 方法


        #endregion
    }
}
