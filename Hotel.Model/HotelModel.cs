using MC.ORM;
using Newtonsoft.Json;
using NIU.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{
    /// <summary>
    /// 酒店模型
    /// </summary>
    [Serializable,TableName("hotel"), PrimaryKey("id")]
    public class HotelModel : BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 上级酒店id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Pid { set; get; }

        /// <summary>
        /// 酒店名称
        /// </summary>
        [Display(Name = "酒店名称")]
        public string Name { set; get; }

        /// <summary>
        /// 拼音简码
        /// </summary>
        [Display(Name = "拼音简码")]
        public string Spell { set; get; }

        /// <summary>
        /// 联系人
        /// </summary>
        [Display(Name = "联系人")]
        public string Handler { set; get; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [Display(Name = "联系电话")]
        public string Tel { set; get; }

        /// <summary>
        /// 传真
        /// </summary>
        [Display(Name = "传真")]
        public string Fax { set; get; }

        /// <summary>
        /// 管理员账号
        /// </summary>
        [Display(Name = "管理员账号")]
        public string UserName { set; get; }

        /// <summary>
        /// 省
        /// </summary>
        [Display(Name = "省")]
        public string Province { set; get; }

        /// <summary>
        /// 省
        /// </summary>
        [Display(Name = "省")]
        public string ProvinceName { set; get; }
      
        /// <summary>
        /// 市
        /// </summary>
        [Display(Name = "市")]
        public string City { set; get; }

        /// <summary>
        /// 市
        /// </summary>
        [Display(Name = "市")]
        public string CityName { set; get; }

        /// <summary>
        /// 区县
        /// </summary>
        [Display(Name = "区县")]
        public string Town { set; get; }

        /// <summary>
        /// 区县
        /// </summary>
        [Display(Name = "区县")]
        public string TownName { set; get; }

        /// <summary>
        /// 详细地址
        /// </summary>
        [Display(Name = "详细地址")]
        public string Address { set; get; }

        /// <summary>
        /// 酒店介绍
        /// </summary>
        [Display(Name = "酒店介绍")]
        public string Remark { set; get; }

        /// <summary>
        /// 经度
        /// </summary>
        [Display(Name = "经度")]
        public string Lng { set; get; }

        /// <summary>
        /// 纬度
        /// </summary>
        [Display(Name = "纬度")]
        public string Lat { set; get; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Display(Name = "更新时间")]
        public int CDate { set; get; }

        #endregion

        #region 扩展属性

        /// <summary>
        /// 密码
        /// </summary>
        [Ignore]
        [Display(Name = "密码")]
        public string Pwd { set; get; }

        /// <summary>
        /// 确认密码
        /// </summary>
        [Ignore]
        [Display(Name = "确认密码")]
        public string CfmPwd { set; get; }

        #endregion

        #region 方法

        public Dictionary<string, string> GetHash()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            Type type = this.GetType();
            PropertyInfo[] list = type.GetProperties();
            if (list == null || !list.Any())
                return dic;
            foreach (var item in list)
            {
                if (item.Name == "children")
                    continue;
                dic.Add(item.Name, item.GetValue(this) == null ? string.Empty : item.GetValue(this).ToString());
            }
            return dic;
        }

        #endregion
    }
}
