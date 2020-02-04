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
    /// 房价方案
    /// </summary>
    [TableName("room_price"), PrimaryKey("Id")]
    public class RoomPrice : BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 折扣名
        /// </summary>
        [Display(Name = "折扣名")]
        public string SaleName { set; get; }

        /// <summary>
        /// 折扣值
        /// </summary>
        [Display(Name = "折扣值")]
        public decimal Sale { set; get; }

        /// <summary>
        /// 房型   0:所有房型
        /// </summary>
        [Display(Name = "房型")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long RoomTypeId { set; get; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Display(Name = "开始时间")]
        public int StartDate { set; get; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Display(Name = "结束时间")]
        public int EndDate { set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { set; get; }

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
        /// 房型
        /// </summary>
        [Ignore]
        public string RoomTypeName { set; get; }

        /// <summary>
        /// 原始房价
        /// </summary>
        [Display(Name = "原始房价")]
        [Ignore]
        public string Price { set; get; }

        /// <summary>
        /// 折后价
        /// </summary>
        [Ignore]
        [Display(Name = "折后价")]
        public string SalePrice { set; get; }

        #endregion

        #region 方法


        #endregion
    }
}
