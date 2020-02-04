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
    /// 未结算明细-红冲明细
    /// </summary>
    [TableName("agree_comp_wjsmx_hch_detail"), PrimaryKey("Id")]
    public class AgreeCompWjsmxHchDetail : BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 未结算明细Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long WjsmxId { set; get; }

        /// <summary>
        /// 红冲
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long HchId { set; get; }

        /// <summary>
        /// 红冲名称
        /// </summary>
        [Display(Name = "红冲名称")]
        public string HchName { set; get; }

        /// <summary>
        /// 红冲金额
        /// </summary>
        [Display(Name = "红冲金额")]
        public decimal Money { set; get; }

        /// <summary>
        /// 单据号
        /// </summary>
        [Display(Name = "单据号")]
        public string DjNum { set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { set; get; }

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
        /// 更新时间
        /// </summary>
        [Display(Name = "更新时间")]
        public int CDate { set; get; }

        [JsonConverter(typeof(LongToStringConverter))]
        public long HotelId { set; get; }

        #endregion

        #region 扩展属性



        #endregion

        #region 方法


        #endregion
    }
}
