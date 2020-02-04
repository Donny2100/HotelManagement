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
    /// 钟点房基本数据
    /// </summary>
    [TableName("hour_room_base"), PrimaryKey("Id")]
    public class HourRoomBase
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Display(Name = "开始时间")]
        public string StartTime { set; get; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Display(Name = "结束时间")]
        public string ToTime { set; get; }

        /// <summary>
        /// 缓冲时间（分）
        /// </summary>
        [Display(Name = "缓冲时间（分）")]
        public int BufferTime { set; get; }

        /// <summary>
        /// 提前通知时间（分）
        /// </summary>
        [Display(Name = "提前通知时间（分）")]
        public int AdvanceTime { set; get; }

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
