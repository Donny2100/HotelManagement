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
    /// 一次性用品 洗漱用品 每个房型的使用数量
    /// </summary>
    [TableName("disposable_material_setting"), PrimaryKey("Id")]
    public class DisposableMaterialSetting : BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }


        [JsonConverter(typeof(LongToStringConverter))]
        public long MaterialId { set; get; }



        [JsonConverter(typeof(LongToStringConverter))]
        public long RoomTypeId { set; get; }


        public int Number { get; set; }

        #endregion

        #region 扩展属性



        #endregion

        #region 方法


        #endregion
    }
}
