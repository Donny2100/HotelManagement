using MC.ORM;
using Newtonsoft.Json;
using NIU.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{
    /// <summary>
    /// 菜单功能按钮模型
    /// </summary>
    [TableName("menu_func_btn"), PrimaryKey("Id")]
    public class MenuFuncBtn : BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 菜单id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long MenuId { set; get; }

        /// <summary>
        /// 功能按钮id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long FuncBtnId { set; get; }

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
