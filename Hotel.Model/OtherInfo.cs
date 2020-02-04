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
    /// 其他资料--基本作废
    /// </summary>
    [TableName("other_info"), PrimaryKey("Id")]
    public class OtherInfo:BaseModel
    {
        #region 属性

        /// <summary>
        /// Id  1：客人来源  2：商品单位（可不要） 3：房间特征  4：支付方式   5：证件类型   6：房价方案
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// Pid   1：客人来源  2：商品单位（可不要） 3：房间特征  4：支付方式   5：证件类型   6：房价方案
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Pid { set; get; }

        /// <summary>
        /// 资料名称
        /// </summary>
        [Display(Name = "资料名称")]
        public string Name { set; get; }

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


        /// <summary>
        /// treegrid属性
        /// </summary>
        [Ignore]
        public List<OtherInfo> children { set; get; }

        #endregion

        #region 方法


        #endregion
    }
}
