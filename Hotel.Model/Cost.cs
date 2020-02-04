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
    /// 费用
    /// </summary>
    [TableName("cost"), PrimaryKey("Id")]
    public class Cost:BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 费用类别
        /// </summary>
        [Display(Name = "费用类别")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long CostCatId { set; get; }

        /// <summary>
        /// 费用名称
        /// </summary>
        [Display(Name = "费用名称")]
        public string Name { set; get; }

        /// <summary>
        /// 拼音简码
        /// </summary>
        [Display(Name = "拼音简码")]
        public string Spell { set; get; }

        /// <summary>
        /// 价格
        /// </summary>
        [Display(Name = "价格")]
        public decimal Price { set; get; }


        /// <summary>
        /// 状态   0：正常使用   1：已停用
        /// </summary>
        [Display(Name = "状态")]
        public int State { set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { set; get; }

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
        /// 费用类别
        /// </summary>
        [Display(Name = "商品类别")]
        [Ignore]
        public string CostCatName { set; get; }

        #endregion

        #region 方法


        #endregion
    }
}
