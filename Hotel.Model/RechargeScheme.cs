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
    /// 充值方案--会员充值方案
    /// </summary>
    [TableName("recharge_scheme"), PrimaryKey("Id")]
    public class RechargeScheme : BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 充值金额
        /// </summary>
        [Display(Name = "充值金额")]
        public int Money { set; get; }

        /// <summary>
        /// 赠送金额
        /// </summary>
        [Display(Name = "赠送金额")]
        public int ZSMoney { set; get; }

        /// <summary>
        /// 赠送积分
        /// </summary>
        [Display(Name = "赠送积分")]
        public int ZSExp { set; get; }

        /// <summary>
        /// 状态   0：启用   1：停用
        /// </summary>
        [Display(Name = "状态")]
        public int State { set; get; }

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
