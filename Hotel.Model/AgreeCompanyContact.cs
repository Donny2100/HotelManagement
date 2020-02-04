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
    /// 协议单位联系人
    /// </summary>
    [TableName("agree_company_contact"), PrimaryKey("Id")]
    public class AgreeCompanyContact : BaseModel
    {
        #region 属性

        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 账号
        /// </summary>
        public string AgreeCompId { set; get; }

        /// <summary>
        /// 全称
        /// </summary>
        [Display(Name = "全称")]
        public string AgreeCompName { set; get; }

        /// <summary>
        /// 简称
        /// </summary>
        [Display(Name = "简称")]
        public string AgreeCompShortName { set; get; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Display(Name = "姓名")]
        public string Name { set; get; }

        /// <summary>
        /// 性别
        /// </summary>
        [Display(Name = "性别")]
        public string Sex { set; get; }

        /// <summary>
        /// 出生日期
        /// </summary>
        [Display(Name = "出生日期")]
        public string Birth { set; get; }

        /// <summary>
        /// 称呼
        /// </summary>
        [Display(Name = "称呼")]
        public string Chenghu { set; get; }

        /// <summary>
        /// 部门
        /// </summary>
        [Display(Name = "部门")]
        public string Depart { set; get; }

        /// <summary>
        /// 办公电话
        /// </summary>
        [Display(Name = "办公电话")]
        public string BgTel { set; get; }

        /// <summary>
        /// 手机
        /// </summary>
        [Display(Name = "手机")]
        public string Tel { set; get; }

        /// <summary>
        /// 家庭电话
        /// </summary>
        [Display(Name = "家庭电话")]
        public string JtTel { set; get; }

        /// <summary>
        /// 职务
        /// </summary>
        [Display(Name = "职务")]
        public string Position { set; get; }

        /// <summary>
        /// 邮编
        /// </summary>
        [Display(Name = "邮编")]
        public string Zipcode { set; get; }

        /// <summary>
        /// Email
        /// </summary>
        [Display(Name = "Email")]
        public string Email { set; get; }

        /// <summary>
        /// 地址
        /// </summary>
        [Display(Name = "地址")]
        public string Address { set; get; }

        /// <summary>
        /// 兴趣爱好
        /// </summary>
        [Display(Name = "兴趣爱好")]
        public string Hobby { set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { set; get; }

        /// <summary>
        /// 操作人
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long HandlerId { set; get; }

        /// <summary>
        /// 操作人
        /// </summary>
        [Display(Name = "操作人")]
        public string HandlerName { set; get; }

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



        #endregion

        #region 方法


        #endregion
    }
}
