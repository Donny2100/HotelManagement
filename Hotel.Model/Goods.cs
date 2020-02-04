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
    /// 商品
    /// </summary>
    [TableName("goods"), PrimaryKey("Id")]
    public class Goods : BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 商品类别
        /// </summary>
        [Display(Name = "商品类别")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long CatId { set; get; }

        /// <summary>
        /// 商品编号
        /// </summary>
        [Display(Name = "商品编号")]
        public string GoodsNO { set; get; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [Display(Name = "商品名称")]
        public string Name { set; get; }

        /// <summary>
        /// 拼音简码
        /// </summary>
        [Display(Name = "拼音简码")]
        public string Spell { set; get; }

        /// <summary>
        /// 单价
        /// </summary>
        [Display(Name = "单价")]
        public decimal Price { set; get; }

        /// <summary>
        /// 积分
        /// </summary>
        [Display(Name = "积分")]
        public int Exp { set; get; }

        /// <summary>
        /// 商品单位
        /// </summary>
        [Display(Name = "商品单位")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long GoodsUnitId { set; get; }

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
        /// 商品类别
        /// </summary>
        [Display(Name = "商品类别")]
        [Ignore]
        public string CatName { set; get; }

        /// <summary>
        /// 商品单位
        /// </summary>
        [Display(Name = "商品单位")]
        [Ignore]
        public string GoodsUnitName { set; get; }

        #endregion

        #region 方法


        #endregion
    }
}
