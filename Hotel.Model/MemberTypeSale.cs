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
    /// 会员折扣设置
    /// </summary>
    [TableName("member_type_sale"), PrimaryKey("Id")]
    public class MemberTypeSale
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 会员类型id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long MemberTypeId { set; get; }

        /// <summary>
        /// 折扣类型    1：客房折扣    2：商品折扣    3：费用折扣
        /// </summary>
        public int SaleType { set; get; }

        /// <summary>
        /// 折扣目标Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long TargetId { set; get; }

        /// <summary>
        /// 折扣值
        /// </summary>
        public decimal Sale { set; get; }

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
        /// 目标名称
        /// </summary>
        [Ignore]
        public string TargetName { set; get; }

        /// <summary>
        /// 预设单价
        /// </summary>
        [Ignore]
        public decimal Price { set; get; }

        /// <summary>
        /// 折后单价
        /// </summary>
        [Ignore]
        public decimal SalePrice { set; get; }

        #endregion

        #region 方法


        #endregion
    }
}
