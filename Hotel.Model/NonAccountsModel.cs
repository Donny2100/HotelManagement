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
    /// 
    /// </summary>
    [TableName("non_accounts"), PrimaryKey("Id")]
    public class NonAccountsModel : BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [Display(Name = "Id")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 时间
        /// </summary>
        [Display(Name = "时间")]
        public int CDate { set; get; }
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "单号")]
        public string Sgdh { set; get; }
        [Display(Name = "数量")]
        public int Number { set; get; }
        [Display(Name = "金额")]
        public int Money { set; get; }
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "支付方式")]
        public string PayTyepeName { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "操作员")]
        public string HandlerName { set; get; }

        /// <summary>
        /// 图标
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { set; get; }
        public int PayTyepeID { get; set; }
        public long HandlerID { get; set; }
        public string GoodsName { get; set; }
        public string GoodsID { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
         
        #endregion

        #region 扩展属性

        [Ignore]
        public decimal Price { set; get; }
        [Ignore]
        public string GoodsUnitName { get; set; }
        [Ignore]
        public long GoodsUnitId { set; get; }
        #endregion
        //jack
        #region 方法


        #endregion
    }
}
