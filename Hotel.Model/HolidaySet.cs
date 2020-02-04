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
    /// 节假日设置
    /// </summary>
    [TableName("holiday_set"), PrimaryKey("Id")]
    public class HolidaySet:BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 节假日名称
        /// </summary>
        [Display(Name = "节假日名称")]
        public string Name { set; get; }

        /// <summary>
        /// 开始日期 00:00:00
        /// </summary>
        [Display(Name = "开始日期")]
        public int StartDate { set; get; }

        /// <summary>
        /// 结束日期 23:59:59
        /// </summary>
        [Display(Name = "结束时间")]
        public int EndDate { set; get; }

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
