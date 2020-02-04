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
    /// 会员类型
    /// </summary>
    [TableName("member_type"), PrimaryKey("Id")]
    public class MemberType : BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        public string Name { set; get; }

        /// <summary>
        /// 卡费--额外卡片的费用
        /// </summary>
        [Display(Name = "卡费")]
        public decimal CardFee { set; get; }

        /// <summary>
        /// 期限
        /// </summary>
        [Display(Name = "期限")]
        public DateTime ExpireDate { set; get; }

        /// <summary>
        /// 排序号
        /// </summary>
        [Display(Name = "排序号")]
        public int Seq { set; get; }

        /// <summary>
        /// 是否是积分卡
        /// </summary>
        public bool IsJFK { set; get; }

        /// <summary>
        /// 是否按消费金额积分
        /// </summary>
        public bool IsByXFJE { set; get; }

        /// <summary>
        /// 是否按消费金额积分--房租
        /// </summary>
        public bool IsFZ { set; get; }

        /// <summary>
        /// 是否按消费金额积分--消费
        /// </summary>
        public bool IsXF { set; get; }

        /// <summary>
        /// 每消费多少元
        /// </summary>
        public int MXFMoney { set; get; }

        /// <summary>
        /// 每消费多少元 可积多少分
        /// </summary>
        public int MXFMoneyExp { set; get; }

        /// <summary>
        /// 是否按入住天数积分
        /// </summary>
        public bool IsByRZTS { set; get; }

        /// <summary>
        /// 每入住多少天
        /// </summary>
        public int MRZDay { set; get; }

        /// <summary>
        /// 每入住多少天 可积多少分
        /// </summary>
        public int MRZDayExp { set; get; }

        /// <summary>
        /// 积分兑换房租时是否允许计算积分
        /// </summary>
        public bool IsExpDHFZ { set; get; }

        /// <summary>
        /// 每1积分抵扣多少元 /*每元抵扣多少积分*/   
        /// </summary>
        public int PerExpDKYuan { set; get; }

        //public int PerYuanDKExp { set; get; }

        /// <summary>
        /// 是否是储值卡
        /// </summary>
        public bool IsCZK { set; get; }

        /// <summary>
        /// 开卡时默认金额是多少元
        /// </summary>
        public decimal BaseMoney { set; get; }

        /// <summary>
        /// 是否启用储值卡赠送积分
        /// </summary>
        public bool IsCZKGiveExp { set; get; }

        /// <summary>
        /// 每充值多少元
        /// </summary>
        public int MczMoney { set; get; }

        /// <summary>
        /// 每充值多少元--赠送多少积分
        /// </summary>
        public int MczMoneyGiveExp { set; get; }

        /// <summary>
        /// 会员设置--积分超过是否设置
        /// </summary>
        public bool IsExpOverSet { set; get; }

        /// <summary>
        /// 积分超过多少时
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long OverExp { set; get; }

        /// <summary>
        /// 积分超过多少时--升级为什么会员
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long OverExpToMemTypeId { set; get; }


        /// <summary>
        /// 积分超过多少时--升级为什么会员
        /// </summary>
        public string OverExpToMemTypeName { set; get; }

        /// <summary>
        /// 升级是否扣除积分
        /// </summary>
        public bool IsSJKCExp { set; get; }

        /// <summary>
        /// 升级扣除的积分
        /// </summary>
        public int SJKCExp { set; get; }

        /// <summary>
        /// 会员设置--推迟时间是否设置
        /// </summary>
        public bool IsTCTimeSet { set; get; }

        /// <summary>
        /// 天房可推迟多少小时退房
        /// </summary>
        public int TFKTCHour { set; get; }

        /// <summary>
        /// 钟点房可推迟多少分钟退房
        /// </summary>
        public int ZDFKTCMinute { set; get; }

        /// <summary>
        /// 首次入住房价--0的话则按照房价方案
        /// </summary>
        public int FirstTimeRoomFee { set; get; }

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
