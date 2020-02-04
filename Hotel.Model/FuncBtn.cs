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
    /// 功能按钮模型
    /// </summary>
    [TableName("func_btn"), PrimaryKey("id")]
    public class FuncBtn : BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 按钮名称
        /// </summary>
        [Display(Name = "按钮名称")]
        public string Name { set; get; }

        /// <summary>
        /// 标识码
        /// </summary>
        [Display(Name = "标识码")]
        public string Code { set; get; }

        /// <summary>
        /// 排序号
        /// </summary>
        [Display(Name = "排序号")]
        public int Seq { set; get; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public int CDate { set; get; }

        #endregion

        #region 扩展属性

        [Ignore]
        public bool IsChecked { set; get; }

        [Ignore]
        public string Desc { get { return Name; } }

        #endregion

        #region 方法


        #endregion
    }
}
