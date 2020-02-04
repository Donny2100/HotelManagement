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
    /// 客人物品
    /// </summary>
    [TableName("guest_goods"), PrimaryKey("Id")]
    public class GuestGoods
    {
        #region 属性
        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 客人姓名
        /// </summary>
        [Display(Name = "客人姓名")]
        public string Name { set; get; }

        /// <summary>
        /// 电话
        /// </summary>
        [Display(Name = "电话")]
        public string Phone { set; get; }

        /// <summary>
        /// 房间号
        /// </summary>
        [Display(Name = "房间号")]
        public string RoomNo { set; get; }

        /// <summary>
        /// 订单号
        /// </summary>
        [Display(Name = "订单号")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long OrderId { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "物品名称")]
        public string GoodName { set; get; }

        /// <summary>
        /// 类型
        /// </summary>
        [Display(Name = "类型")]
        public string GoodType { set; get; }

        /// <summary>
        /// 参考价值
        /// </summary>
        [Display(Name = "参考价值")]
        public decimal GoodPrice { set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { set; get; }

        // <summary>
        /// 存放时间
        /// </summary>
        [Display(Name = "存放时间")]
        public string StorageTime { set; get; }

        // <summary>
        /// 存放操作员
        /// </summary>
        [Display(Name = "存放操作员")]
        public string StorageUserName { set; get; }


        // <summary>
        /// 存放操作员
        /// </summary>
        [Display(Name = "领取时间")]
        public string ReceiveTime { set; get; }

        // <summary>
        /// 领取操作员
        /// </summary>
        [Display(Name = "领取操作员")]
        public string ReceiveUserName { set; get; }

        /// <summary>
        /// 领取人
        /// </summary>
        [Display(Name = "领取人")]
        public string ReceiveUser { get; set; }

        /// <summary>
        /// 酒店Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long HotelId { set; get; }
        #endregion

        #region 扩展属性

        #endregion

        #region 方法


        #endregion
    }
}
