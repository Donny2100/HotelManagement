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
    /// 物品租赁
    /// </summary>
    [TableName("Material_Lease"), PrimaryKey("Id")]
    public class MaterialLease : BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 房间登记id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long RoomRegId { set; get; }

        /// <summary>
        /// 房间id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long RoomId { set; get; }

        /// <summary>
        /// 物品
        /// </summary>
        [Display(Name = "物品")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long MaterialId { set; get; }

        /// <summary>
        /// 物品
        /// </summary>
        [Display(Name = "物品")]
        public string MaterialName { set; get; }

        /// <summary>
        /// 数量
        /// </summary>
        [Display(Name = "数量")]
        public int Quantity { set; get; }

        /// <summary>
        /// 押金
        /// </summary>
        [Display(Name = "押金")]
        public decimal Deposit { set; get; }

        /// <summary>
        /// 租借时间
        /// </summary>
        [Display(Name = "租借时间")]
        public string LeaseTime { set; get; }

        /// <summary>
        /// 状态  0：未还    1：已还
        /// </summary>
        public int State { set; get; }

        /// <summary>
        /// 归还时间
        /// </summary>
        [Display(Name = "归还时间")]
        public string BackTime { set; get; }

        /// <summary>
        /// 单据号
        /// </summary>
        [Display(Name = "单据号")]
        public string DjNum { set; get; }

        /// <summary>
        /// 用于记录最大单据号的年月日
        /// </summary>
        public long Nyr { set; get; }

        /// <summary>
        /// 用于记录最大单据号
        /// </summary>
        public long MaxDjNum { set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { set; get; }

        /// <summary>
        /// 操作人
        /// </summary>
        [Display(Name = "操作人")]
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
