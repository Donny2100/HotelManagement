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
    /// 全局计费设置
    /// </summary>
    [TableName("global_fee_set"), PrimaryKey("Id")]
    public class GlobalFeeSet : BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 进店后多少分钟开始计费
        /// </summary>
        [Display(Name = "进店后多少分钟开始计费")]
        public int QTStartFeeMin { set; get; }

        /// <summary>
        /// 进店时间在几点之后的算第二天退房
        /// </summary>
        [Display(Name = "进店时间在几点之后的算第二天退房")]
        public int QTAfterPointToNextDay { set; get; }

        /// <summary>
        /// 中午退房时间
        /// </summary>
        [Display(Name = "中午退房时间")]
        public int QTExitPoint { set; get; }

        /// <summary>
        /// 超时后收费类型---1：不加收房费\2：按半天房费加收\3：按分钟加收\4：先按分钟后按半天房费
        /// </summary>
        [Display(Name = "超时后收费类型")]
        public int QTOverTimeFeeType { set; get; }

        /// <summary>
        /// 直到几点加收全天房费
        /// </summary>
        [Display(Name = "直到几点加收全天房费")]
        public int QTUntilPointToOneDayFee { set; get; }

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
        /// 直到几点加收全天房费---超时后收费类型---先按分钟后按半天
        /// </summary>
        [Display(Name = "直到几点加收半天房费")]
        public int QTUntilPointToHalfDayFeeByMinHalfDay { set; get; }

        /// <summary>
        /// 计费宽限分钟数（缓冲分钟数）
        /// </summary>
        [Display(Name = "计费宽限分钟数")]
        public int QTBufferMinByMinHalfDay { set; get; }

        /// <summary>
        /// 凌晨房开始点
        /// </summary>
        [Display(Name = "凌晨房开始点")]
        public int LCStartFeePoint { set; get; }

        /// <summary>
        /// 凌晨房结束点
        /// </summary>
        [Display(Name = "凌晨房结束点")]
        public int LCEndFeePoint { set; get; }

        /// <summary>
        /// 进店时间在几点之后的算第二天退房
        /// </summary>
        [Display(Name = "进店时间在几点之后的算第二天退房")]
        public int LCAfterPointToNextDay { set; get; }

        /// <summary>
        /// 中午退房时间
        /// </summary>
        [Display(Name = "中午退房时间")]
        public int LCExitPoint { set; get; }

        /// <summary>
        /// 超时后收费类型---1：不加收房费\2：按半天房费加收\3：按分钟加收\4：先按分钟后按半天房费
        /// </summary>
        [Display(Name = "超时后收费类型")]
        public int LCOverTimeFeeType { set; get; }

        /// <summary>
        /// 直到当日/次日---1：当日   2：次日
        /// </summary>
        [Display(Name = "直到当日/次日")]
        public int LCUntilDrOrCrType { set; get; }

        /// <summary>
        /// 直到当日/次日几点
        /// </summary>
        [Display(Name = "直到当日/次日几点")]
        public int LCUntilDrOrCrPoint { set; get; }

        /// <summary>
        /// 直到直到当日/次日几点  加收全天房费\转为全天房费   ---1：加收全天房费 2：转为全天房费
        /// </summary>
        [Display(Name = "直到直到当日/次日几点")]
        public int LCUntilPointToFeeType { set; get; }

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
        /// 直到几点加收全天房费---超时后收费类型---先按分钟后按半天
        /// </summary>
        [Display(Name = "直到几点加收半天房费")]
        public int LCUntilPointToHalfDayFeeByMinHalfDay { set; get; }

        /// <summary>
        /// 计费宽限分钟数（缓冲分钟数）
        /// </summary>
        [Display(Name = "计费宽限分钟数")]
        public int LCBufferMinByMinHalfDay { set; get; }

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
