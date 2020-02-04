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
    [TableName("pay_type"), PrimaryKey("Id")]
    public class PayType:BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 支付方式
        /// </summary>
        [Display(Name = "支付方式")]
        public string Name { set; get; }

        /// <summary>
        /// 是否用于押金付款
        /// </summary>
        [Display(Name = "是否用于押金付款")]
        public bool IsYyyjfk { set; get; }

        /// <summary>
        /// 是否用于结账付款
        /// </summary>
        [Display(Name = "是否用于结账付款")]
        public bool IsYyjzfk { set; get; }

        /// <summary>
        /// 排序号
        /// </summary>
        [Display(Name = "排序号")]
        public int Seq { set; get; }

        /// <summary>
        /// 启用状态   true：启用   false：未启用
        /// </summary>
        [Display(Name = "启用状态")]
        public bool State { set; get; }

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
