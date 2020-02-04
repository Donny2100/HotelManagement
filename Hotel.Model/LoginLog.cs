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
    /// <summary>
    ///  登录日志
    /// </summary>
    [TableName("login_log"), PrimaryKey("id")]
    [DataContract]
    public class LoginLog : BaseModel
    {
        #region 字段

        /// <summary>
        /// Id
        /// </summary>
        [Display(Name = "Id")]
        [DataMember(Name = "id")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 用户id
        /// </summary>
        [Display(Name = "用户id")]
        [DataMember(Name = "userid")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long UserId { set; get; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "用户名")]
        [DataMember(Name = "username")]
        public string UserName { set; get; }

        /// <summary>
        /// 用户组id
        /// </summary>
        [Display(Name = "用户组id")]
        [DataMember(Name = "groupid")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long GroupId { set; get; }

        /// <summary>
        /// 用户组
        /// </summary>
        [Display(Name = "用户组")]
        [DataMember(Name = "groupname")]
        public string GroupName { set; get; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Display(Name = "手机号")]
        [DataMember(Name = "phone")]
        public string Phone { set; get; }

        /// <summary>
        /// IP
        /// </summary>
        [Display(Name = "IP")]
        [DataMember(Name = "ip")]
        public string Ip { set; get; }

        /// <summary>
        /// 设备---1：电脑  2：手机   3：平板
        /// </summary>
        [Display(Name = "设备")]
        [DataMember(Name = "device")]
        public int Device { set; get; }

        /// <summary>
        /// 用户操作系统
        /// </summary>
        [Display(Name = "用户操作系统")]
        [DataMember(Name = "os")]
        public string OS { set; get; }

        /// <summary>
        /// 浏览器
        /// </summary>
        [Display(Name = "浏览器")]
        [DataMember(Name = "agent")]
        public string Agent { set; get; }

        /// <summary>
        /// 系统类型  1：后台  2：前台
        /// </summary>
        [Display(Name = "系统类型")]
        [DataMember(Name = "systype")]
        public int SysType { set; get; }

        /// <summary>
        /// 是否登录成功
        /// </summary>
        [Display(Name = "是否登录成功")]
        [DataMember(Name = "issuccess")]
        public bool IsSuccess { set; get; }

        /// <summary>
        /// 令牌
        /// </summary>
        [Display(Name = "令牌")]
        [DataMember(Name = "token")]
        public string Token { set; get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        [DataMember(Name = "created")]
        public int CDate { set; get; }

        #endregion

        #region 扩展属性

        /// <summary>
        /// 地理位置
        /// </summary>
        [Display(Name = "地理位置")]
        [DataMember(Name = "address")]
        [Ignore]
        public string Address { set; get; }

        /// <summary>
        /// 登录类型  1：平台账号  2：微信  3：qq 4:手机号
        /// </summary>
        [Display(Name = "登录类型")]
        [DataMember(Name = "logtype")]
        [Ignore]
        public int LogType { set; get; }

        #endregion
    }
}
