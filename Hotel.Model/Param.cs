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
    /// 参数设置
    /// </summary>
    [TableName("param"), PrimaryKey("Id")]
    public class Param : BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        #region 常规参数

        /// <summary>
        /// 是否自动过夜审
        /// </summary>
        [Display(Name = "是否自动过夜审")]
        public bool IsAutoNightAudit { set; get; }

        /// <summary>
        /// 夜审时间--几点
        /// </summary>
        [Display(Name = "夜审时间")]
        public int NightAuditTime { set; get; }

        /// <summary>
        /// 开房必须输入身份证号
        /// </summary>
        [Display(Name = "开房必须输入身份证号")]
        public bool IsIdCardForRegRoom { set; get; }

        /// <summary>
        /// 会员注册必须输入身份证号和手机号
        /// </summary>
        [Display(Name = "会员注册必须输入身份证号和手机号")]
        public bool IsIdCardTelForMemberReg { set; get; }

        /// <summary>
        /// 限制一个身份证只能开一个房间
        /// </summary>
        [Display(Name = "限制一个身份证只能开一个房间")]
        public bool IsOneIdCardForOneRoom { set; get; }

        /// <summary>
        /// 叫醒服务是否有声音
        /// </summary>
        [Display(Name = "叫醒服务是否有声音")]
        public bool IsVoiceForWake { set; get; }

        /// <summary>
        /// 是否自动刷新房态
        /// </summary>
        [Display(Name = "是否自动刷新房态")]
        public bool IsAutoRefreshForRoom { set; get; }

        /// <summary>
        /// 每隔多少秒刷新房态
        /// </summary>
        [Display(Name = "每隔多少秒刷新房态")]
        public int RefreshRoomSeconds { set; get; }

        /// <summary>
        /// 结账时零头计算方式---1：自动抹零到个位   2：自动抹零到十位:   3：四舍五入到个位:   4：个位直接加1 
        /// </summary>
        [Display(Name = "结账时零头计算方式")]
        public int SettleAccountOddType { set; get; }

        /// <summary>
        /// 入住登记时允许手动调价
        /// </summary>
        [Display(Name = "入住登记时允许手动调价")]
        public bool IsAdjustPriceForRegRoom { set; get; }

        #endregion

        #region 预定参数

        /// <summary>
        /// 新增散客预定是否可以删除
        /// </summary>
        [Display(Name = "新增散客预定是否可以删除")]
        public bool IsCanDelFitCustomer { set; get; }

        /// <summary>
        /// 新增散客预定后多少分钟内可以删除
        /// </summary>
        [Display(Name = "新增散客预定后多少分钟内可以删除")]
        public int DelFitCustomerMin { set; get; }

        /// <summary>
        /// 新增团体预定后是否可以删除
        /// </summary>
        [Display(Name = "新增团体预定后是否可以删除")]
        public bool IsCanDelGroupCustomer { set; get; }

        /// <summary>
        /// 新增团体预定后多少分钟内可以删除
        /// </summary>
        [Display(Name = "新增团体预定后多少分钟内可以删除")]
        public int DelGroupCustomerMin { set; get; }

        /// <summary>
        /// 散客超预定时多少分钟后自动转为noshow状态
        /// </summary>
        [Display(Name = "散客超预定时多少分钟后自动转为noshow状态")]
        public int MinuteForFitCustomerToNoshow { set; get; }

        /// <summary>
        /// 散客预定超时时，收取定金的方式  1：夜审时自动处理，产生一笔定金收入，并删除该预定  2：需前台手动处理，退定金或生成定金收入
        /// </summary>
        [Display(Name = "散客预定超时时，收取定金的方式")]
        public int DepositTypeForFitCustomerOvertime { set; get; }

        /// <summary>
        /// 团体预定超时时，收取定金的方式  1：夜审时自动处理，产生一笔定金收入，并删除该预定  2：需前台手动处理，退定金或生成定金收入
        /// </summary>
        [Display(Name = "团体预定超时时，收取定金的方式")]
        public int DepositTypeForGroupCustomerOvertime { set; get; }

        #endregion

        #region 登记参数

        /// <summary>
        /// 会员价格是否固定不可更改---开房时
        /// </summary>
        [Display(Name = "会员价格是否固定不可更改")]
        public bool IsMemberRoomPriceFixed { set; get; }

        /// <summary>
        /// 协议单位价格是否固定不可更改---开房时
        /// </summary>
        [Display(Name = "协议单位价格是否固定不可更改")]
        public bool IsAgreeCompanyRoomPriceFixed { set; get; }

        /// <summary>
        /// 散客登记后是否可以撤单
        /// </summary>
        [Display(Name = "散客登记后是否可以撤单")]
        public bool IsCanFitCustomerCancelOrder { set; get; }

        /// <summary>
        /// 散客登记后多少分钟内可以撤单
        /// </summary>
        [Display(Name = "散客登记后多少分钟内可以撤单")]
        public int MinuteForFitCustomerCancelOrder { set; get; }

        /// <summary>
        /// 团体登记后是否可以撤单
        /// </summary>
        [Display(Name = "团体登记后是否可以撤单")]
        public bool IsCanGroupCustomerCancelOrder { set; get; }

        /// <summary>
        /// 团体登记后多少分钟内可以撤单
        /// </summary>
        [Display(Name = "团体登记后多少分钟内可以撤单")]
        public int MinuteForGroupCustomerCancelOrder { set; get; }

        /// <summary>
        /// 交班模式   1;按收退款差额交班（只交接备用金）   2：按当班产生的营业额交款
        /// </summary>
        [Display(Name = "交班模式")]
        public int ShiftType { set; get; }

        #endregion

        #region 客房参数

        /// <summary>
        /// 客房清理后是否记录服务员姓名
        /// </summary>
        [Display(Name = "客房清理后是否记录服务员姓名")]
        public bool IsRemarkWaiterNameAfterRoomClear { set; get; }

        /// <summary>
        /// 干净状态的房间是否需要打扫
        /// </summary>
        [Display(Name = "干净状态的房间是否需要打扫")]
        public bool IsNeedClearForCleanRoom { set; get; }

        /// <summary>
        /// 客房清理时是否需要记录每个日耗品的具体数量
        /// </summary>
        [Display(Name = "客房清理时是否需要记录每个日耗品的具体数量")]
        public bool IsRemarkGoodsQuntityWhenClearRoom { set; get; }

        /// <summary>
        /// 是否有早餐
        /// </summary>
        [Display(Name = "是否有早餐")]
        public bool IsHasBreakfast { set; get; }

        /// <summary>
        /// 早餐开始时间
        /// </summary>
        [Display(Name = "早餐开始时间")]
        public string BreakfastTimeStart { set; get; }

        /// <summary>
        /// 早餐结束时间
        /// </summary>
        [Display(Name = "早餐结束时间")]
        public string BreakfastTimeEnd { set; get; }

        /// <summary>
        /// 早餐费用
        /// </summary>
        [Display(Name = "早餐费用")]
        public decimal BreakfastFee { set; get; }

        /// <summary>
        /// 早餐费用是否登记入账
        /// </summary>
        [Display(Name = "早餐费用是否登记入账")]
        public bool IsBreakfastFeeRegAccount { set; get; }

        /// <summary>
        /// 是否有午餐
        /// </summary>
        [Display(Name = "是否有午餐")]
        public bool IsHasLunch { set; get; }

        /// <summary>
        /// 午餐开始时间
        /// </summary>
        [Display(Name = "午餐开始时间")]
        public string LunchTimeStart { set; get; }

        /// <summary>
        /// 午餐结束时间
        /// </summary>
        [Display(Name = "午餐结束时间")]
        public string LunchTimeEnd { set; get; }

        /// <summary>
        /// 午餐费用
        /// </summary>
        [Display(Name = "午餐费用")]
        public decimal LunchFee { set; get; }

        /// <summary>
        /// 午餐费用是否登记入账
        /// </summary>
        [Display(Name = "午餐费用是否登记入账")]
        public bool IsLunchFeeRegAccount { set; get; }

        /// <summary>
        /// 是否有晚餐
        /// </summary>
        [Display(Name = "是否有晚餐")]
        public bool IsHasDinner { set; get; }

        /// <summary>
        /// 晚餐开始时间
        /// </summary>
        [Display(Name = "晚餐开始时间")]
        public string DinnerTimeStart { set; get; }

        /// <summary>
        /// 晚餐结束时间
        /// </summary>
        [Display(Name = "晚餐结束时间")]
        public string DinnerTimeEnd { set; get; }

        /// <summary>
        /// 晚餐费用
        /// </summary>
        [Display(Name = "晚餐费用")]
        public decimal DinnerFee { set; get; }

        /// <summary>
        /// 晚餐费用是否登记入账
        /// </summary>
        [Display(Name = "晚餐费用是否登记入账")]
        public bool IsDinnerFeeRegAccount { set; get; }

        /// <summary>
        /// 前台优惠限额
        /// </summary>
        [Display(Name = "前台优惠限额")]
        public decimal QtYhMaxMoney { set; get; }

        #endregion

        #region 公式参数

        /// <summary>
        /// 出租率基准房间数方式（计算出租率时使用）---1：酒店实际总房间数   2：总房间数—维修房数   3：总房间数—自用房数
        ///   4：总房间数—维修房数—自用房数
        /// </summary>
        [Display(Name = "出租率基准房间数")]
        public int RentRateBaseRoomQauntityType { set; get; }

        /// <summary>
        /// 免费房是否参与计算平均房价   平均房价计算方法按当晚在住客人（包括当日入住当日离店客人）房价的平均值计算
        /// 注意：钟点房、时段房、长包房不参与平均房价的计算
        /// </summary>
        [Display(Name = "免费房是否参与计算平均房价")]
        public bool IsFreeRoomForAverageRoomPrice { set; get; }

        #endregion

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
