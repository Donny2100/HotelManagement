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
    /// 转账记录
    /// </summary>
    [TableName("agree_comp_zz_record"), PrimaryKey("Id")]
    public class AgreeCompZzRecord : BaseModel
    {
        #region 属性

        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 转出账号
        /// </summary>
        public string FromAgreeCompId { set; get; }

        /// <summary>
        /// 转出单位全称
        /// </summary>
        [Display(Name = "转出单位全称")]
        public string FromAgreeCompName { set; get; }

        /// <summary>
        /// 转出单位简称
        /// </summary>
        [Display(Name = "转出单位简称")]
        public string FromAgreeCompShortName { set; get; }

        /// <summary>
        /// 转入账号
        /// </summary>
        public string ToAgreeCompId { set; get; }

        /// <summary>
        /// 转入单位全称
        /// </summary>
        [Display(Name = "转入单位全称")]
        public string ToAgreeCompName { set; get; }

        /// <summary>
        /// 转入单位简称
        /// </summary>
        [Display(Name = "转入单位简称")]
        public string ToAgreeCompShortName { set; get; }

        /// <summary>
        /// 明细Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long DetalId { set; get; }

        /// <summary>
        /// 类型  1：收款  2:退款   3：未结算明细
        /// </summary>
        public int ZType { set; get; }

        /// <summary>
        /// 操作人
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long HandlerId { set; get; }

        /// <summary>
        /// 操作人
        /// </summary>
        [Display(Name = "操作人")]
        public string HandlerName { set; get; }

        /// <summary>
        /// 转账时间
        /// </summary>
        public int CDate { set; get; }

        /// <summary>
        /// 酒店Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long HotelId { set; get; }

        #endregion

        #region 扩展属性

        /// <summary>
        /// 类型  1：收款  2:退款   3：未结算明细
        /// </summary>
        [Ignore]
        public string ZTypeName
        {
            get
            {
                if (ZType == 1)
                    return "收款";
                else if (ZType == 2)
                    return "退款";
                else if (ZType == 3)
                    return "未结算";
                return "未知";
            }
        }

        [Ignore]
        public string RoomNO { set; get; }

        [Ignore]
        public decimal Money { set; get; }

        [Ignore]
        public int FsDate { set; get; }

        [Ignore]
        public string Remark { set; get; }

        #endregion

        #region 方法


        #endregion 
    }
}
