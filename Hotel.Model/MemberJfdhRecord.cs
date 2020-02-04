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
    /// 商品类别
    /// </summary>
    [TableName("member_jfdh_record"), PrimaryKey("Id")]
    public class MemberJfdhRecord : BaseModel
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
        /// 兑换类型   1：普通   2：房间积分优惠
        /// </summary>
        public int RType { set; get; }

        /// <summary>
        /// rtype为2时代表积分优惠的id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long TargetId { set; get; }

        /// <summary>
        /// 商品id
        /// </summary>
        public long GoodsId { set; get; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { set; get; }

        /// <summary>
        /// 单个商品所需积分
        /// </summary>
        public int GoodsExp { set; get; }

        /// <summary>
        /// 兑换数量
        /// </summary>
        public int Qunatity { set; get; }

        /// <summary>
        /// 总积分
        /// </summary>
        public int TotalExp { set; get; }

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
