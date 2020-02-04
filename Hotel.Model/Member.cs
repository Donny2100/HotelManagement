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
    /// 会员
    /// </summary>
    [TableName("member"), PrimaryKey("Id")]
    public class Member : BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 会员卡号 
        /// </summary>
        [Display(Name = "会员卡号")]
        public string CardNO { set; get; }

        /// <summary>
        /// 卡内码 
        /// </summary>
        [Display(Name = "卡内码")]
        public string CNM { set; get; }

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
        /// 头像
        /// </summary>
        [Display(Name = "头像")]
        public string Portrait { set; get; }

        /// <summary>
        /// 会员类型 
        /// </summary>
        [Display(Name = "会员类型")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long MemberTypeId { set; get; }

        /// <summary>
        /// 会员类型 
        /// </summary>
        [Display(Name = "会员类型")]
        public string MemberTypeName { set; get; }

        /// <summary>
        /// 消费密码 
        /// </summary>
        [Display(Name = "消费密码")]
        public string ConsumePwd { set; get; }

        /// <summary>
        /// 性别
        /// </summary>
        [Display(Name = "性别")]
        public string Sex { set; get; }

        /// <summary>
        /// 出生日期
        /// </summary>
        [Display(Name = "出生日期")]
        public DateTime Birth { set; get; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Display(Name = "手机号")]
        public string Tel { set; get; }

        /// <summary>
        /// 证件类型Id
        /// </summary>
        [Display(Name = "证件类型")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long CertificateTypeId { set; get; }

        /// <summary>
        /// 证件类型
        /// </summary>
        [Display(Name = "证件类型")]
        public string CertificateTypeName { set; get; }

        /// <summary>
        /// 证件号码
        /// </summary>
        [Display(Name = "证件号码")]
        public string CertificateTypeNO { set; get; }

        /// <summary>
        /// 地址
        /// </summary>
        [Display(Name = "地址")]
        public string Address { set; get; }

        /// <summary> 
        /// 营销人员 0：无营销人员
        /// </summary>
        [Display(Name = "营销人员")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long YxryId { set; get; }

        /// <summary>
        /// 营销人员  
        /// </summary>
        [Display(Name = "营销人员")]
        public string YxryName { set; get; }

        /// <summary>
        /// 是否不接收营销短信
        /// </summary>
        [Display(Name = "不接收营销短信")]
        public bool IsNotYxSms { set; get; }

        /// <summary>
        /// 是否长期有效
        /// </summary>
        [Display(Name = "长期有效")]
        public bool IsCqyx { set; get; }

        /// <summary>
        /// 到期日期
        /// </summary>
        [Display(Name = "到期日期")]
        public DateTime ExpireDate { set; get; }

        /// <summary>
        /// 卡内积分
        /// </summary>
        [Display(Name = "卡内积分")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long Exp { set; get; }

        /// <summary>
        /// 余额
        /// </summary>
        [Display(Name = "余额")]
        public decimal Money { set; get; }

        /// <summary>
        /// 操作员
        /// </summary>
        [Display(Name = "操作员")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long HandlerId { set; get; }

        /// <summary>
        /// 操作员
        /// </summary>
        [Display(Name = "操作员")]
        public string HandlerName { set; get; }

        /// <summary>
        /// 状态 1：正常   2：挂失   3：过期   4：作废   5：退卡
        /// </summary>
        [Display(Name = "状态")]
        public int State { set; get; }

        /// <summary>
        /// 入住次数--消费次数
        /// </summary>
        [Display(Name = "入住次数")]
        public int Times { set; get; }

        /// <summary>
        /// 酒店Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long HotelId { set; get; }

        /// <summary>
        /// 总储值
        /// </summary>
        [Display(Name = "总储值")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long TotalMoney { set; get; }

        /// <summary>
        /// 消费金额
        /// </summary>
        [Display(Name = "消费金额")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long XfMoney { set; get; }

        /// <summary>
        /// 总积分
        /// </summary>
        [Display(Name = "总积分")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long TotalExp { set; get; }

        /// <summary>
        /// 兑换积分
        /// </summary>
        [Display(Name = "兑换积分")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long DhExp { set; get; }

        /// <summary>
        /// 冻结积分
        /// </summary>
        [Display(Name = "冻结积分")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long DjExp { set; get; }

        /// <summary>
        /// 发卡时间
        /// </summary>
        [Display(Name = "发卡时间")]
        public int CDate { set; get; }

        #endregion

        #region 扩展属性

        /// <summary>
        /// 消费密码 
        /// </summary>
        [Display(Name = "消费密码")]
        [Ignore]
        public string ConsumePwdConfirm { set; get; }

        /// <summary>
        /// 支付方式 
        /// </summary>
        [Display(Name = "支付方式")]
        [Ignore]
        [JsonConverter(typeof(LongToStringConverter))]
        public long PayTypeId { set; get; }

        /// <summary>
        /// 支付方式
        /// </summary>
        [Display(Name = "支付方式")]
        [Ignore]
        public string PayTypeName { set; get; }

        /// <summary>
        /// 收款金额
        /// </summary>
        [Ignore]
        [Display(Name = "收款金额")]
        public int SkMoney { set; get; }

        /// <summary>
        /// 充值金额
        /// </summary>
        [Ignore]
        [Display(Name = "充值金额")]
        public int CzMoney { set; get; }

        /// <summary>
        /// 赠送积分
        /// </summary>
        [Ignore]
        [Display(Name = "赠送积分")]
        public int ZsExp { set; get; }

        [Ignore]
        public string StateName
        {
            get
            {
                switch (State)
                {
                    case 1:
                        return "正常";
                    case 2:
                        return "挂失";
                    case 3:
                        return "过期";
                    case 4:
                        return "作废";
                    case 5:
                        return "退卡";
                    default:
                        return "未知";
                }
            }
        }

        #endregion

        #region 方法


        #endregion
    }
}
