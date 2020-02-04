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
    /// 客人资料--境外客人
    /// </summary>
    [TableName("Guest_Info_EN"), PrimaryKey("Id")]
    public class GuestInfoEN : BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 登记id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long LastRoomRegId { set; get; }

        /// <summary>
        /// 房间id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long LastRoomId { set; get; }

        /// <summary>
        /// 上次入住房号
        /// </summary>
        public string LastRoomNO { set; get; }

        /// <summary>
        /// 上次入住房价
        /// </summary>
        public Decimal LastRoomPrice { set; get; }

        /// <summary>
        /// 上次入住时间
        /// </summary>
        public string LastRegTime { set; get; }

        /// <summary>
        /// 上次离店时间
        /// </summary>
        public string LastLeaveTime { set; get; }

        /// <summary>
        /// 英文姓
        /// </summary>
        [Display(Name = "英文姓")]
        public string LastName { set; get; }

        /// <summary>
        /// 英文名
        /// </summary>
        [Display(Name = "英文名")]
        public string FirstName { set; get; }

        /// <summary>
        /// 中文名
        /// </summary>
        [Display(Name = "中文名")]
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
        public string Sex { set; get; }

        /// <summary>
        /// 出生日期
        /// </summary>
        [Display(Name = "出生日期")]
        public string Birth { set; get; }

        /// <summary>
        /// 国家
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long CountryId { set; get; }

        /// <summary>
        /// 国家
        /// </summary>
        public string CountryName { set; get; }

        /// <summary>
        /// 证件类型
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long CertificateTypeId { set; get; }

        /// <summary>
        /// 证件类型
        /// </summary>
        public string CertificateTypeName { set; get; }

        /// <summary>
        /// 证件号
        /// </summary>
        public string CertificateNO { set; get; }

        /// <summary>
        /// 签证有效期
        /// </summary>
        public string QzExpireDate { set; get; }

        /// <summary>
        /// 入境口岸
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long PortEntryId { set; get; }

        /// <summary>
        /// 入境口岸
        /// </summary>
        public string PortEntryName { set; get; }

        /// <summary>
        /// 入境日期
        /// </summary>
        public string PortEntryDate { set; get; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Tel { set; get; }

        /// <summary>
        /// 客人类型  1:一般散客  2:联房成员  3:酒店会员:4:协议单位 与登记处一致
        /// </summary>
        public int CustomerType { set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { set; get; }

        /// <summary>
        /// 入住次数
        /// </summary>
        public int RzCount { set; get; }

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
        /// 习好
        /// </summary>
        public string Xh { set; get; }

        /// <summary>
        /// 通讯
        /// </summary>
        public string Tx { set; get; }

        /// <summary>
        /// 社会
        /// </summary>
        public string Sh { set; get; }

        /// <summary>
        /// 账务
        /// </summary>
        public string Zw { set; get; }

        /// <summary>
        /// 失物
        /// </summary>
        public string Sw { set; get; }

        /// <summary>
        /// 投诉
        /// </summary>
        public string Ts { set; get; }

        /// <summary>
        /// 其他
        /// </summary>
        public string Qt { set; get; }

        /// <summary>
        /// 是否是黑名单
        /// </summary>
        public bool? IsDisable { get; set; }

        /// <summary>
        /// 拉黑原因
        /// </summary>
        public string DisableReason { get; set; }

        /// <summary>
        /// 拉黑日期
        /// </summary>
        public DateTime? DisableDate { get; set; }
        #endregion

        #region 扩展属性

        /// <summary>
        /// 客人类型  1:一般散客  2:联房成员  3:酒店会员:4:协议单位 与登记处一致
        /// </summary>
        [Ignore]
        public string CustomerTypeName
        {
            get
            {
                switch (CustomerType)
                {
                    case 1:
                        return "一般散客";
                    case 2:
                        return "联房成员";
                    case 3:
                        return "酒店会员";
                    case 4:
                        return "协议单位";
                    default:
                        return string.Empty;
                }
            }
        }

        /// <summary>
        /// 累计入住天数
        /// </summary>
        [Ignore]
        public int LjRzCount
        {
            get; set;
        }

        /// <summary>
        /// 历史消费金额--界面：客人档案编辑那块要显示
        /// </summary>
        [Ignore]
        public decimal LsXfPrice { get; set; }
        #endregion

        #region 方法


        #endregion
    }
}
