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
    /// 钟点房
    /// </summary>
    [TableName("hour_room"), PrimaryKey("Id")]
    public class HourRoom:BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 钟点房方案名称
        /// </summary>
        [Display(Name = "钟点房方案名称")]
        public string Name { set; get; }

        /// <summary>
        /// 分钟数
        /// </summary>
        [Display(Name = "分钟数")]
        public int Minute { set; get; }

        /// <summary>
        /// 价格
        /// </summary>
        [Display(Name = "价格")]
        public decimal Price { set; get; }

        /// <summary>
        /// 超时后每多少分钟
        /// </summary>
        [Display(Name = "超时后每多少分钟")]
        public int PerNMin { set; get; }

        /// <summary>
        /// 超时后每多少分钟收取多少元
        /// </summary>
        [Display(Name = "超时后每多少分钟收取多少元")]
        public decimal PerNMinPrice { set; get; }

        /// <summary>
        /// 超时后不足多少分钟
        /// </summary>
        [Display(Name = "超时后不足多少分钟")]
        public int LessThanNMin { set; get; }

        /// <summary>
        /// 超时后超过多少分钟
        /// </summary>
        [Display(Name = "超时后超过多少分钟")]
        public int MoreThanNMin { set; get; }

        /// <summary>
        /// 超时后不足多少分钟，超过多少分钟，另外加收费用
        /// </summary>
        [Display(Name = "超时后不足多少分钟，超过多少分钟，另外加收费用")]
        public decimal LessMoreThanNMinPrice { set; get; }

        /// <summary>
        /// 封顶费用--0:不封顶
        /// </summary>
        [Display(Name = "封顶费用")]
        public decimal UpperPirce { set; get; }

        /// <summary>
        /// 超过多少分钟则转为全天房--0：不转为全天房
        /// </summary>
        [Display(Name = "超过多少分钟则转为全天房")]
        public int ToBeDayRoomMin { set; get; }

        /// <summary>
        /// 酒店Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long HotelId { set; get; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public int CDate { set; get; }


        public bool IsEnabled { get; set; }
        #endregion

        #region 扩展属性

        [Ignore]
        public bool IsChecked { set; get; }

        [Ignore]
        public string Desc { get { return Name; } }

        #endregion

        #region 方法


        #endregion
    }
}
