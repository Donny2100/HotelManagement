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
    /// 周末设置
    /// </summary>
    [TableName("weekend_set"), PrimaryKey("Id")]
    public class WeekendSet : BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 星期一是否为节假日
        /// </summary>
        [Display(Name = "星期一")]
        public bool IsMonday { set; get; }

        /// <summary>
        /// 星期二是否为节假日
        /// </summary>
        [Display(Name = "星期二")]
        public bool IsTuesday { set; get; }

        /// <summary>
        /// 星期三是否为节假日
        /// </summary>
        [Display(Name = "星期三")]
        public bool IsWednesday { set; get; }

        /// <summary>
        /// 星期四是否为节假日
        /// </summary>
        [Display(Name = "星期四")]
        public bool IsThursday { set; get; }

        /// <summary>
        /// 星期五是否为节假日
        /// </summary>
        [Display(Name = "星期五")]
        public bool IsFriday { set; get; }

        /// <summary>
        /// 星期六是否为节假日
        /// </summary>
        [Display(Name = "星期六")]
        public bool IsSaturday { set; get; }

        /// <summary>
        /// 星期日是否为节假日
        /// </summary>
        [Display(Name = "星期日")]
        public bool IsSunday { set; get; }

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
