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
    /// 房型模型
    /// </summary>
    [TableName("room_type"), PrimaryKey("id")]
    public class RoomType : BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 房型名称
        /// </summary>
        [Display(Name = "房型名称")]
        public string Name { set; get; }

        /// <summary>
        /// 挂牌价
        /// </summary>
        [Display(Name = "挂牌价")]
        public decimal Price { set; get; }

        /// <summary>
        /// 凌晨价
        /// </summary>
        [Display(Name = "凌晨价")]
        public decimal MorningPrice { set; get; }

        /// <summary>
        /// 月租价
        /// </summary>
        [Display(Name = "月租价")]
        public decimal MonthlyPrice { set; get; }

        /// <summary>
        /// 是否允许周末房
        /// </summary>
        [Display(Name = "是否允许周末房")]
        public bool IsAllowWeekendRoom { set; get; }

        /// <summary>
        /// 周末挂牌价价
        /// </summary>
        [Display(Name = "周末挂牌价")]
        public decimal WeekendPrice { set; get; }

        /// <summary>
        /// 是否允许假日房
        /// </summary>
        [Display(Name = "是否允许假日房")]
        public bool IsAllowHolidayRoom { set; get; }

        /// <summary>
        /// 假日价
        /// </summary>
        [Display(Name = "假日价")]
        public decimal HolidayPrice { set; get; }

        /// <summary>
        /// 超预定，存的是int，注意除以100
        /// </summary>
        [Display(Name = "超预定（%）")]
        public int OverRate { set; get; }

        /// <summary>
        /// 预设押金
        /// </summary>
        [Display(Name = "预设押金")]
        public decimal Deposit { set; get; }

        /// <summary>
        /// 床位数
        /// </summary>
        [Display(Name = "床位数")]
        public decimal BedCount { set; get; }

        /// <summary>
        /// 早餐数
        /// </summary>
        [Display(Name = "早餐数")]
        public decimal BreakfastCount { set; get; }

        /// <summary>
        /// 是否有效
        /// </summary>
        [Display(Name = "是否有效")]
        public bool IsActive { set; get; }

        /// <summary>
        /// 制卡数量
        /// </summary>
        [Display(Name = "制卡数量")]
        public int CardCount { set; get; }

        /// <summary>
        /// 是否允许钟点房
        /// </summary>
        [Display(Name = "是否允许钟点房")]
        public bool IsAllowHourRoom { set; get; }

        /// <summary>
        /// 钟点房转正常入住房价
        /// </summary>
        [Display(Name = "钟点房转正常入住房价")]
        public string HourRoomToQTPrice { set; get; }

        /// <summary>
        /// 钟点房
        /// </summary>
        [Display(Name = "钟点房")]
        public string HourRoomIds { set; get; }

        /// <summary>
        /// 是否允许时段房
        /// </summary>
        [Display(Name = "是否允许时段房")]
        public bool IsAllowPeriodRoom { set; get; }

        /// <summary>
        /// 时段房转正常入住房价
        /// </summary>
        [Display(Name = "时段房转正常入住房价")]
        public string PeriodRoomToQTPrice { set; get; }

        /// <summary>
        /// 时段房
        /// </summary>
        [Display(Name = "时段房")]
        public string PeriodRoomIds { set; get; }

        /// <summary>
        /// 全天房是否单独设置超时费，true：是自定义  false：使用全局
        /// </summary>
        [Display(Name = "全天房是否单独设置超时费")]
        public bool IsQtSetBySelf { set; get; }

        /// <summary>
        /// 超时后收费类型---1：不加收房费\2：按半天房费加收\3：按分钟加收\4：先按分钟后按半天房费
        /// </summary>
        [Display(Name = "超时后收费类型")]
        public int QTOverTimeFeeType { set; get; }

        /// <summary>
        /// 超时后每多少分钟---超时后收费类型---按分钟
        /// </summary>
        [Display(Name = "超时后每多少分钟")]
        public int QTPerNMinByMin { set; get; }

        /// <summary>
        /// 超时后每多少分钟收取多少元---超时后收费类型---按分钟
        /// </summary>
        [Display(Name = "超时后每多少分钟收取多少元")]
        public decimal QTPerNMinPriceByMin { set; get; }

        /// <summary>
        /// 超时后不足多少分钟---超时后收费类型---按分钟
        /// </summary>
        [Display(Name = "超时后不足多少分钟")]
        public int QTLessThanNMinByMin { set; get; }

        /// <summary>
        /// 超时后超过多少分钟---超时后收费类型---按分钟
        /// </summary>
        [Display(Name = "超时后超过多少分钟")]
        public int QTMoreThanNMinByMin { set; get; }

        /// <summary>
        /// 超时后不足多少分钟，超过多少分钟，另外加收费用---超时后收费类型---按分钟
        /// </summary>
        [Display(Name = "超时后不足多少分钟，超过多少分钟，另外加收费用")]
        public decimal QTLessMoreThanNMinPriceByMin { set; get; }

        /// <summary>
        /// 超时后每多少分钟---超时后收费类型---先按分钟后按半天
        /// </summary>
        [Display(Name = "超时后每多少分钟")]
        public int QTPerNMinByMinHalfDay { set; get; }

        /// <summary>
        /// 超时后每多少分钟收取多少元---超时后收费类型---先按分钟后按半天
        /// </summary>
        [Display(Name = "超时后每多少分钟收取多少元")]
        public decimal QTPerNMinPriceByMinHalfDay { set; get; }

        /// <summary>
        /// 超时后不足多少分钟---超时后收费类型---先按分钟后按半天
        /// </summary>
        [Display(Name = "超时后不足多少分钟")]
        public int QTLessThanNMinByMinHalfDay { set; get; }

        /// <summary>
        /// 超时后超过多少分钟---超时后收费类型---先按分钟后按半天
        /// </summary>
        [Display(Name = "超时后超过多少分钟")]
        public int QTMoreThanNMinByMinHalfDay { set; get; }

        /// <summary>
        /// 超时后不足多少分钟，超过多少分钟，另外加收费用---超时后收费类型---先按分钟后按半天
        /// </summary>
        [Display(Name = "超时后不足多少分钟，超过多少分钟，另外加收费用")]
        public decimal QTLessMoreThanNMinPriceByMinHalfDay { set; get; }

        /// <summary>
        /// 是否启用凌晨房
        /// </summary>
        [Display(Name = "是否启用凌晨房")]
        public bool IsUseLC { set; get; }

        /// <summary>
        /// 凌晨房转正常入住房价
        /// </summary>
        [Display(Name = "凌晨房转正常入住房价")]
        public string LCRoomToQTPrice { set; get; }

        /// <summary>
        /// 凌晨房是否单独设置超时费，true：是自定义  false：使用全局
        /// </summary>
        [Display(Name = "凌晨房是否单独设置超时费")]
        public bool IsLCSetBySelf { set; get; }

        /// <summary>
        /// 超时后收费类型---1：不加收房费\2：按半天房费加收\3：按分钟加收\4：先按分钟后按半天房费
        /// </summary>
        [Display(Name = "超时后收费类型")]
        public int LCOverTimeFeeType { set; get; }

        ///// <summary>
        ///// 直到当日/次日---1：当日   2：次日
        ///// </summary>
        //[Display(Name = "直到当日/次日")]
        //public int LCUntilDrOrCrType { set; get; }

        ///// <summary>
        ///// 直到当日/次日几点
        ///// </summary>
        //[Display(Name = "直到当日/次日几点")]
        //public int LCUntilDrOrCrPoint { set; get; }

        ///// <summary>
        ///// 直到直到当日/次日几点  加收全天房费\转为全天房费   ---1：加收全天房费 2：转为全天房费
        ///// </summary>
        //[Display(Name = "直到直到当日/次日几点")]
        //public int LCUntilPointToFeeType { set; get; }

        /// <summary>
        /// 超时后每多少分钟---超时后收费类型---按分钟
        /// </summary>
        [Display(Name = "超时后每多少分钟")]
        public int LCPerNMinByMin { set; get; }

        /// <summary>
        /// 超时后每多少分钟收取多少元---超时后收费类型---按分钟
        /// </summary>
        [Display(Name = "超时后每多少分钟收取多少元")]
        public decimal LCPerNMinPriceByMin { set; get; }

        /// <summary>
        /// 超时后不足多少分钟---超时后收费类型---按分钟
        /// </summary>
        [Display(Name = "超时后不足多少分钟")]
        public int LCLessThanNMinByMin { set; get; }

        /// <summary>
        /// 超时后超过多少分钟---超时后收费类型---按分钟
        /// </summary>
        [Display(Name = "超时后超过多少分钟")]
        public int LCMoreThanNMinByMin { set; get; }

        /// <summary>
        /// 超时后不足多少分钟，超过多少分钟，另外加收费用---超时后收费类型---按分钟
        /// </summary>
        [Display(Name = "超时后不足多少分钟，超过多少分钟，另外加收费用")]
        public decimal LCLessMoreThanNMinPriceByMin { set; get; }

        /// <summary>
        /// 超时后每多少分钟---超时后收费类型---先按分钟后按半天
        /// </summary>
        [Display(Name = "超时后每多少分钟")]
        public int LCPerNMinByMinHalfDay { set; get; }

        /// <summary>
        /// 超时后每多少分钟收取多少元---超时后收费类型---先按分钟后按半天
        /// </summary>
        [Display(Name = "超时后每多少分钟收取多少元")]
        public decimal LCPerNMinPriceByMinHalfDay { set; get; }

        /// <summary>
        /// 超时后不足多少分钟---超时后收费类型---先按分钟后按半天
        /// </summary>
        [Display(Name = "超时后不足多少分钟")]
        public int LCLessThanNMinByMinHalfDay { set; get; }

        /// <summary>
        /// 超时后超过多少分钟---超时后收费类型---先按分钟后按半天
        /// </summary>
        [Display(Name = "超时后超过多少分钟")]
        public int LCMoreThanNMinByMinHalfDay { set; get; }

        /// <summary>
        /// 超时后不足多少分钟，超过多少分钟，另外加收费用---超时后收费类型---先按分钟后按半天
        /// </summary>
        [Display(Name = "超时后不足多少分钟，超过多少分钟，另外加收费用")]
        public decimal LCLessMoreThanNMinPriceByMinHalfDay { set; get; }

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
        [Display(Name = "更新时间")]
        public int CDate { set; get; }

        [Display(Name = "额定人数")]
        public int ManNumer { get; set; }

        #endregion

        #region 扩展属性

        [Ignore]
        public string HourRoomNames { set; get; }

        [Ignore]
        public string PeriodRoomNames { set; get; }

        /// <summary>
        /// 房价方案
        /// </summary>
        [Ignore]
        public List<RoomPrice> RoomPriceList { set; get; }

        /// <summary>
        /// 预订时的修改
        /// </summary>
        [Ignore]
        public List<Room> RoomList { set; get; }

        /// <summary>
        /// 预订时的修改
        /// </summary>
        [Ignore]
        public int RoomCount { set; get; }

        /// <summary>
        /// 预订时的修改
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        [Ignore]
        public long RoomPriceId{ set; get; }

        /// <summary>
        /// 预订时的修改
        /// </summary>
        [Ignore]
        public decimal RoomPrice { set; get; }

        /// <summary>
        /// 预订时的修改
        /// </summary>
        [Ignore]
        public decimal RoomSale { set; get; }

        /// <summary>
        /// 预订时的修改
        /// </summary>
        [Ignore]
        public decimal RoomSalePrice { set; get; }

        /// <summary>
        /// 预定转入住
        /// </summary>
        [Ignore]
        public List<RoomYdRoom> YdRoomList { set; get; }

        #endregion

        #region 方法


        #endregion
    }
}
