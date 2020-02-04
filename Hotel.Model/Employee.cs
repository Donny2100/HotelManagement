using MC.ORM;
using Newtonsoft.Json;
using NIU.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{
    [Serializable]
    [TableName("employee"), PrimaryKey("id")]
    public class Employee : BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 头像 
        /// </summary>
        [Display(Name = "头像")]
        public string Portrait { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        [Display(Name = "服务员工号")]
        public string JobNum { set; get; }

        /// <summary>
        /// 入职日期
        /// </summary>
        [Display(Name = "入职日期")]
        public string JobDate { set; get; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Display(Name = "服务员姓名")]
        public string Name { set; get; }

        /// <summary>
        /// 拼音简码
        /// </summary>
        [Display(Name = "拼音简码")]
        public string Spell { set; get; }

        /// <summary>
        /// 性别
        /// </summary>
        [Display(Name = "性别")]
        public int Sex { set; get; }


        [Display(Name = "是否有效")]
        public bool IsEnabled { set; get; }

        /// <summary>
        /// 员工生日
        /// </summary>
        [Display(Name = "员工生日")]
        public string BirthDate { set; get; }

        /// <summary>
        /// 所属部门
        /// </summary>
        [Display(Name = "所属部门")]
        public int DepartmentId { set; get; }

        /// <summary>
        /// 职务
        /// </summary>
        [Display(Name = "职务")]
        public string Duty { set; get; }

        /// <summary>
     
        /// <summary>
        /// 联系电话
        /// </summary>
        [Display(Name = "联系电话")]
        public string Tel { set; get; }

        /// <summary>
        /// 联系地址
        /// </summary>
        [Display(Name = "联系地址")]
        public string Address { set; get; }

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
        /// 所属酒店
        /// </summary>
        [Ignore]
        public HotelModel Hotel { set; get; }

        /// <summary>
        /// 所属酒店
        /// </summary>
        [Ignore]
        public string HotelName { set; get; }

        /// <summary>
        /// 用户组
        /// </summary>
        [Ignore]
        public Group Group { set; get; }

      

        /// <summary>
        /// 确认密码
        /// </summary>
        [Ignore]
        [Display(Name ="确认密码")]
        public string CfmPwd { set; get; }

        #endregion

        #region 扩展属性

        /// <summary>
        /// 所属部门
        /// </summary>
        [Ignore]
        public string DepartmentName { set; get; }

        /// <summary>
        /// 用户组
        /// </summary>
        [Ignore]
        public string GroupName { set; get; }

        #endregion

        #region 方法


        #endregion
    }
}
