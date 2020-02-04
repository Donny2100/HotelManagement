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
    /// 一次性用品 洗漱用品
    /// </summary>
    [TableName("disposable_material"), PrimaryKey("Id")]
    public class DisposableMaterial : BaseModel
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

 
        [Display(Name = "编码")]
        public string Num { set; get; }
 
        /// <summary>
        /// 酒店Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long HotelId { set; get; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public int CDate { set; get; }

        [Display(Name = "是否有效")]
        public bool IsEnabled { get; set; }

        [Display(Name = "是否每日更换")]
        public bool IsChangeEveryday { get; set; }

        #endregion

        #region 扩展属性


        [Ignore]
        public int Quantity { get; set; }


        [Ignore]
        public int RoomQuantity { get; set; }
        #endregion

        #region 方法


        #endregion
    }
}
