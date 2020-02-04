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
    [TableName("Room_message"), PrimaryKey("Id")]
    public class RoomMessage : BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

     

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

        [JsonConverter(typeof(LongToStringConverter))]
        public long RoomId { set; get; }

        /// <summary>
        /// 是否特要留言
        /// </summary>
        public bool IsSpecial { set; get; }

        public int MsgDate { set; get; }

        public int EnabledDate { set; get; }
        public int AutoCallDate { set; get; }

        public string MsgContent { get; set; }
        public string Remark { get; set; }

        [Display(Name = "留言人")]
        public string MsgName { get; set; }
        public bool IsEnabled { set; get; }


        public bool IsSended { set; get; }


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
