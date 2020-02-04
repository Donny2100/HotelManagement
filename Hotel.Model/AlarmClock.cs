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
    /// 叫醒(闹钟)模型
    /// </summary>
    [TableName("alarm_clock"), PrimaryKey("Id")]
    public class AlarmClock
    {
        #region 属性

        [Display(Name = "编号")]
        public string Id { get; set; }
        /// <summary>
        /// 房间(pk)
        /// </summary>
        [Display(Name = "房间编号")]
        public string RoomId { get; set; }
        /// <summary>
        /// 客人姓名
        /// </summary>
        [Display(Name = "客人姓名")]
        public string Custom { get; set; }
        /// <summary>
        /// 唤醒时间
        /// </summary>
        [Display(Name = "唤醒时间")]
        public string AlarmDate { get; set; }
        /// <summary>
        /// 是否每日同时间唤醒(默认0 = 否 or 1 = 是)
        /// </summary>
        [Display(Name = "是否每日同时间唤醒")]
        public int IsAlarm { get; set; }

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

        #region 方法

        #endregion
    }
}
