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
    /// 入住记录表
    /// </summary>
    [TableName("Guest_Info_CN_Rz_Record"), PrimaryKey("Id")]
    public class GuestInfoCNRzRecord
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 客人id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long LskrId { set; get; }

        /// <summary>
        /// 登记id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long RoomRegId { set; get; }

        /// <summary>
        /// 房间id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long RoomId { set; get; }

        /// <summary>
        /// 房号
        /// </summary>
        public string RoomNO { set; get; }

        /// <summary>
        /// 房价
        /// </summary>
        public decimal RoomPrice { set; get; }

        /// <summary>
        /// 入住时间
        /// </summary>
        public string RegTime { set; get; }

        /// <summary>
        /// 离店时间
        /// </summary>
        public string LeaveTime { set; get; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Display(Name = "姓名")]
        public string Name { set; get; }

        /// <summary>
        /// 客人类型  1:一般散客  2:联房成员  3:酒店会员:4:协议单位 与登记处一致
        /// </summary>
        public int CustomerType { set; get; }

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
        /// 客人类型  1:一般散客  2:联房成员  3:酒店会员:4:协议单位 与登记处一致
        /// </summary>
        [Ignore]
        public string CustomerTypeName
        {
            get
            {
                switch (CustomerType)
                {
                    case 1:
                        return "一般散客";
                    case 2:
                        return "联房成员";
                    case 3:
                        return "酒店会员";
                    case 4:
                        return "协议单位";
                    default:
                        return string.Empty;
                }
            }
        }

        #endregion
    }
}
