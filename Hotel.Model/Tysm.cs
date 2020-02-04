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
    /// 特要说明
    /// </summary>
    [TableName("tysm"), PrimaryKey("Id")]
    public class Tysm : BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        public string Name { set; get; }

        /// <summary>
        /// 图标Id
        /// </summary>
        [Display(Name = "图标")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long IconId { set; get; }

        /// <summary>
        /// 图标
        /// </summary>
        [Display(Name = "图标")]
        public string IconClass { set; get; }

        /// <summary>
        /// 排序号
        /// </summary>
        [Display(Name = "排序号")]
        public int Seq { set; get; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public int CDate { set; get; }

        [JsonConverter(typeof(LongToStringConverter))]
        public long HotelId { set; get; }

        #endregion

        #region 扩展属性

        /// <summary>
        /// 是否选中
        /// </summary>
        [Ignore]
        public bool IsCheck { set; get; }

        #endregion

        #region 方法


        #endregion
    }
}
