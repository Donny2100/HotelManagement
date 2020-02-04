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
    /// 图标库  平台使用，酒店无权限
    /// </summary>
    [TableName("icon_model"), PrimaryKey("Id")]
    public class IconModel : BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 样式
        /// </summary>
        [Display(Name = "样式")]
        public string IconClass { set; get; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public int CDate { set; get; }

        #endregion

        #region 扩展属性

        [Ignore]
        public bool IsChecked { set; get; }

        [Ignore]
        public string Desc { get { return ""; } }

        #endregion

        #region 方法


        #endregion
    }
}
