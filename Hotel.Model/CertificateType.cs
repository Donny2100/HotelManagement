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
    /// 证件类型
    /// </summary>
    [TableName("certificate_type"), PrimaryKey("Id")]
    public class CertificateType : BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 境内/境外 0:境内   1：境外
        /// </summary>
        [Display(Name = "境内/境外")]
        public int Rtype{ set; get; }

        /// <summary>
        /// 证件编号
        /// </summary>
        [Display(Name = "证件编号")]
        public string CertificateNum { set; get; }

        /// <summary>
        /// 证件类型
        /// </summary>
        [Display(Name = "证件类型")]
        public string Name { set; get; }

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



        #endregion

        #region 方法


        #endregion
    }
}
