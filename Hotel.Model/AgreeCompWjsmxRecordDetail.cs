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
    /// 协议单位未结算明细 的明细   保存商品入账的商品消费明细
    /// </summary>
    [TableName("agree_comp_wjsmx_record_detail"), PrimaryKey("Id")]
    public class AgreeCompWjsmxRecordDetail : BaseModel
    {
        #region 属性

        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 协议单位未结算明细Id
        /// </summary>
        public long AgreeCompWjsmxRecordId { set; get; }

        /// <summary>
        /// 项目id 与RType对应  结账时的单位记账对应的明细id
        /// </summary>
        public long ProjectId { set; get; }

        /// <summary>
        ///结账时的单位记账对应的明细的类别  1：房费  2：商品费用   3：损物赔偿   4：其他费用  1、2、3、4是结账时的单位记账，其他地方的转账与RType无关
        /// </summary>
        public int RType { set; get; }

        public long CatId { set; get; }

        public string CatName { set; get; }
       
        public long GoodsId { set; get; }

        public string GoodsName { set; get; }

        public decimal Price { set; get; }

        public int Quantity { set; get; }

        public decimal Money { set; get; }

        public string Remark { set; get; }

        /// <summary>
        /// 酒店Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long HotelId { set; get; }

        #endregion

        #region 扩展属性

        [Ignore]
        public string GoodsNO { set; get; }

        #endregion

        #region 方法


        #endregion 
    }

    public class AgreeCompWjsmxRecordDetailViewHelp
    {
        public long CatId { set; get; }

        public string CatName { set; get; }

        public List<AgreeCompWjsmxRecordDetail> List { set; get; }
    }
}
