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
    /// 商品单位
    /// </summary>
    [TableName("goods_unit"), PrimaryKey("Id")]
    public class GoodsUnit : BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 单位名称
        /// </summary>
        [Display(Name = "单位名称")]
        public string Name { set; get; }

        /// <summary>
        /// 排序号
        /// </summary>
        [Display(Name = "排序号")]
        public int Seq { set; get; }

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
