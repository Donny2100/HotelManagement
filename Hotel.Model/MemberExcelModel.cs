using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{
    /// <summary>
    /// 测验会员导入模版表头
    /// </summary>
    public class MemberExcelModel
    {
        #region 属性
        /// <summary>
        /// 会员卡号
        /// </summary>
        public string 会员卡号 { get; set; }

        /// <summary>
        ///  姓名
        /// </summary>
        public string 姓名 { get; set; }

        /// <summary>
        /// 会员类型
        /// </summary>
        public string 会员类型 { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string 手机号 { get; set; }

        #endregion
    }
}
