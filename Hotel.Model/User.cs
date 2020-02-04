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
    [TableName("user"), PrimaryKey("id")]
    public class User : BaseModel
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
        [Display(Name = "工号")]
        public string JobNum { set; get; }

        /// <summary>
        /// 入职日期
        /// </summary>
        [Display(Name = "入职日期")]
        public string JobDate { set; get; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Display(Name = "姓名")]
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
        /// 可打折扣--1-10之间  10为不可打折--
        /// </summary>
        [Display(Name = "房价可打折扣")]
        public decimal CanSale { set; get; }

        /// <summary>
        /// 结账可优惠金额
        /// </summary>
        [Display(Name = "结账可优惠金额")]
        public int CanDiscountMoney { set; get; }

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
        /// 用户组
        /// </summary>
        [Display(Name = "用户组")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long GId { set; get; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "用户名")]
        public string UserName { set; get; }

        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "密码")]
        public string Pwd { set; get; }

        /// <summary>
        /// 用户类型  1：平台用户  2：总店用户  3：分店用户
        /// </summary>
        public int UserType { set; get; }

        /// <summary>
        /// 酒店Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long HotelId { set; get; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public int CDate { set; get; }

        /// <summary>
        /// 最后登录Ip
        /// </summary>
        [Display(Name = "最后登录Ip")]
        public string LastIP { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        [Display(Name = "最后登录时间")]
        public int LastLDate { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        [Display(Name = "登录次数")]
        public int LoginCount { get; set; }

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
