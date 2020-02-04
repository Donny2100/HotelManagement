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
  
    [TableName("room_change_record"), PrimaryKey("Id")]
    public class RoomChangeRecord : BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

      
        /// <summary>
        /// 酒店Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long HotelId { set; get; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public int CDate { set; get; }

        [JsonConverter(typeof(LongToStringConverter))]
        public long RoomRegId { get; set; }

        [JsonConverter(typeof(LongToStringConverter))]
        public long OldRoomId { get; set; }
        [JsonConverter(typeof(LongToStringConverter))]
        public long NewRoomId { get; set; }

 
        public decimal OldSalePrice { get; set; }
        public decimal OldSaleRate { get; set; }
        public decimal NewSalePrice { get; set; }
        public decimal NewSaleRate { get; set; }

        public int ChangeType { get; set; }
        public long RoomPriceFaId { get; set; }

        public string Remark { get; set; }


        public string NewRoomNo { get; set; }
        public string OldRoomNo { get; set; }

        #endregion
    }
}
