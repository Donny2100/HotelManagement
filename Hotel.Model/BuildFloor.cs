using MC.ORM;
using Newtonsoft.Json;
using NIU.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{
    /// <summary>
    /// 楼栋楼层模型
    /// </summary>
    [TableName("build_floor"), PrimaryKey("id")]
    public class BuildFloor : BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 上级Id  楼栋的上级id为0   楼层的上级id为楼栋id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        [Display(Name="楼栋")]
        public long Pid { set; get; }

        /// <summary>
        /// 楼栋/楼层名称
        /// </summary>
        [Display(Name = "楼栋/楼层名称")]
        public string Name { set; get; }

        /// <summary>
        /// 楼栋/楼层类型 1:楼栋   2：楼层
        /// </summary>
        public int BuildFloorType { set; get; }

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
        public List<BuildFloor> children { set; get; }

        #endregion

        #region 方法


        #endregion
    }
}
