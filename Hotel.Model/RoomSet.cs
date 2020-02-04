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
    /// 房态设置
    /// </summary>
    [TableName("room_set"), PrimaryKey("Id")]
    public class RoomSet : BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 房态图区域背景颜色
        /// </summary>
        [Display(Name = "房态图区域背景颜色")]
        public string BodyBkg { set; get; }

        /// <summary>
        /// 房态图区域边框颜色
        /// </summary>
        [Display(Name = "房态图区域边框颜色")]
        public string BodyBorderColor { set; get; }

        /// <summary>
        /// 将到房背景颜色
        /// </summary>
        [Display(Name = "将到房背景颜色")]
        public string ComingRoomBkg { set; get; }

        /// <summary>
        /// 在住房背景颜色
        /// </summary>
        [Display(Name = "在住房背景颜色")]
        public string AlreadyRoomBkg { set; get; }

        /// <summary>
        /// 干净房背景颜色
        /// </summary>
        [Display(Name = "干净房背景颜色")]
        public string CleanRoomBkg { set; get; }

        /// <summary>
        /// 脏房背景颜色--未住
        /// </summary>
        [Display(Name = "脏房背景颜色")]
        public string DirtyRoomBkg { set; get; }

        /// <summary>
        /// 长包房背景颜色
        /// </summary>
        [Display(Name = "长包房背景颜色")]
        public string LongtimeRoomBkg { set; get; }

        /// <summary>
        /// 免费房
        /// </summary>
        [Display(Name = "免费房背景颜色")]
        public string FreeRoomBkg { set; get; }
        /// <summary>
        /// 维修房背景颜色
        /// </summary>
        [Display(Name = "维修房背景颜色")]
        public string RepairRoomBkg { set; get; }

        /// <summary>
        /// 脏住房背景颜色--已住
        /// </summary>
        [Display(Name = "脏住房背景颜色")]
        public string DirtyAlreadyRoomBkg { set; get; }

        /// <summary>
        /// 自用房背景颜色
        /// </summary>
        [Display(Name = "自用房背景颜色")]
        public string SelfRoomBkg { set; get; }

        /// <summary>
        /// 房号字体大小
        /// </summary>
        [Display(Name = "房号字体大小")]
        public int RoomNoFontSize { set; get; }

        /// <summary>
        /// 房号颜色
        /// </summary>
        [Display(Name = "房号颜色")]
        public string RoomNoColor { set; get; }

        /// <summary>
        /// 房型字体大小
        /// </summary>
        [Display(Name = "房型字体大小")]
        public int RoomTypeFontSize { set; get; }

        /// <summary>
        /// 房型颜色
        /// </summary>
        [Display(Name = "房型颜色")]
        public string RoomTypeColor { set; get; }

        /// <summary>
        /// 余额字体大小
        /// </summary>
        [Display(Name = "余额字体大小")]
        public int LeftMoneyFontSize { set; get; }

        /// <summary>
        /// 余额颜色
        /// </summary>
        [Display(Name = "余额颜色")]
        public string LeftMoneyColor { set; get; }

        /// <summary>
        /// 图标宽度
        /// </summary>
        [Display(Name = "图标宽度")]
        public int RoomWidth { set; get; }

        /// <summary>
        /// 图标高度
        /// </summary>
        [Display(Name = "图标高度")]
        public int RoomHeight { set; get; }

        /// <summary>
        /// 图标字体--房间图标和快捷图标
        /// </summary>
        [Display(Name = "图标字体")]
        public string IconFontFamily { set; get; }

        /// <summary>
        /// 图标字号
        /// </summary>
        [Display(Name = "图标字号")]
        public int IconFontSize { set; get; }

        /// <summary>
        /// 图标间距
        /// </summary>
        [Display(Name = "图标间距")]
        public int IconMargin { set; get; }

        /// <summary>
        /// 图标大小
        /// </summary>
        [Display(Name = "图标大小")]
        public int IconSize { set; get; }

        /// <summary>
        /// 姓名字体大小
        /// </summary>
        [Display(Name = "姓名字体大小")]
        public int NameFontSize { set; get; }

        /// <summary>
        /// 姓名颜色
        /// </summary>
        [Display(Name = "姓名颜色")]
        public string NameColor { set; get; }

        /// <summary>
        /// 房价字体大小
        /// </summary>
        [Display(Name = "房价字体大小")]
        public int RoomPriceFontSize { set; get; }

        /// <summary>
        /// 房价颜色
        /// </summary>
        [Display(Name = "房价颜色")]
        public string RoomPriceColor { set; get; }

        /// <summary>
        /// 来店时间字体大小
        /// </summary>
        [Display(Name = "来店时间字体大小")]
        public int RegTimeFontSize { set; get; }

        /// <summary>
        /// 来店时间颜色
        /// </summary>
        [Display(Name = "来店时间颜色")]
        public string RegTimeColor { set; get; }

        /// <summary>
        /// 钟点时间字体大小
        /// </summary>
        [Display(Name = "钟点时间字体大小")]
        public int HourTimeFontSize { set; get; }

        /// <summary>
        /// 钟点时间颜色
        /// </summary>
        [Display(Name = "钟点时间颜色")]
        public string HourTimeColor { set; get; }

        /// <summary>
        /// 空房是否显示房间类型
        /// </summary>
        [Display(Name = "空房是否显示房间类型")]
        public bool IsEmptyShowRoomType { set; get; }

        /// <summary>
        /// 空房是否显示房间价格
        /// </summary>
        [Display(Name = "空房是否显示房间价格")]
        public bool IsEmptyShowRoomPrice { set; get; }

        /// <summary>
        /// 是否按楼层排序
        /// </summary>
        [Display(Name = "是否按楼层排序")]
        public bool IsOrderByFloor { set; get; }

        /// <summary>
        /// 在住房是否显示房间类型
        /// </summary>
        [Display(Name = "在住房是否显示房间类型")]
        public bool IsAlreadyRoomShowRoomType { set; get; }

        /// <summary>
        /// 在住房是否显示房间价格
        /// </summary>
        [Display(Name = "在住房是否显示房间价格")]
        public bool IsAlreadyRoomShowRoomPrice { set; get; }

        /// <summary>
        /// 是否显示到店时间
        /// </summary>
        [Display(Name = "是否显示到店时间")]
        public bool IsShowRegTime { set; get; }

        /// <summary>
        /// 是否显示连房图标
        /// </summary>
        [Display(Name = "是否显示连房图标")]
        public bool IsShowEvenRoomIcon { set; get; }

        /// <summary>
        /// 是否显示保密图标
        /// </summary>
        [Display(Name = "是否显示保密图标")]
        public bool IsShowSecretIcon { set; get; }

        /// <summary>
        /// 是否显示协议单位名称
        /// </summary>
        [Display(Name = "是否显示协议单位名称")]
        public bool IsShowAgreeCompanyName { set; get; }

        


        /// <summary>
        /// 是否显示押金不足图标
        /// </summary>
        [Display(Name = "是否显示押金不足图标")]
        public bool IsShowDepositNotEnoughIcon { set; get; }

        /// <summary>
        /// 当押金不足时的显示类型   1：当押金不足几天房费时显示   2：当押金不足多少元时显示
        /// </summary>
        [Display(Name = "当押金不足时的显示类型")]
        public bool IsShowWhenDepositNotEnoughType1 { set; get; }

        /// <summary>
        /// 当押金不足时的显示类型   1：当押金不足几天房费时显示   2：当押金不足多少元时显示
        /// </summary>
        [Display(Name = "当押金不足时的显示类型")]
        public bool IsShowWhenDepositNotEnoughType2 { set; get; }

        /// <summary>
        /// 押金不足时显示类型对应的具体值   ShowWhenDepositNotEnoughType为1时存的是不足多少天房费   
        /// ShowWhenDepositNotEnoughType为2时存的是不足多少元
        /// </summary>
        [Display(Name = "押金不足时显示类型对应的具体值")]
        public int DepositNotEnoughType1Value { set; get; }

        /// <summary>
        /// 余额不足时显示类型对应的具体值   ShowWhenDepositNotEnoughType为1时存的是不足多少天房费   
        /// ShowWhenDepositNotEnoughType为2时存的是不足多少元
        /// </summary>
        [Display(Name = "余额不足时显示类型对应的具体值")]
        public int DepositNotEnoughType2Value { set; get; }

        /// <summary>
        /// 是否显示欠费图标
        /// </summary>
        [Display(Name = "是否显示欠费图标")]
        public bool IsShowArrearageIcon { set; get; }

        /// <summary>
        /// 是否显示团队图标
        /// </summary>
        [Display(Name = "是否显示团队图标")]
        public bool IsShowGroupIcon { set; get; }

        /// <summary>
        /// 团队文字
        /// </summary>
        [Display(Name = "团队文字")]
        public string GroupWord { set; get; }

        /// <summary>
        /// 是否显示会员图标
        /// </summary>
        [Display(Name = "是否显示会员图标")]
        public bool IsShowMemberIcon { set; get; }

        /// <summary>
        /// 是否显示贵宾图标
        /// </summary>
        [Display(Name = "是否显示贵宾图标")]
        public bool IsShowVipIcon { set; get; }

        /// <summary>
        /// 会员文字
        /// </summary>
        [Display(Name = "会员文字")]
        public string MemberWord { set; get; }

        /// <summary>
        /// 是否显示预离图标
        /// </summary>
        [Display(Name = "是否显示预离图标")]
        public bool IsShowDepartureIcon { set; get; }

        /// <summary>
        /// 是否显示预离
        /// </summary>
        [Display(Name = "是否显示预离")]
        public bool IsShowDeparture { set; get; }

        /// <summary>
        /// 预离方式   1：只显示当日预离   2：显示多少天内预离的
        /// </summary>
        [Display(Name = "预离方式")]
        public bool DepartureType1 { set; get; }

        /// <summary>
        /// 预离方式   1：只显示当日预离   2：显示多少天内预离的
        /// </summary>
        [Display(Name = "预离方式")]
        public bool DepartureType2 { set; get; }

        /// <summary>
        /// 预离方式对应的具体数值   只有当DepartureType为2时才有值，表示显示多少天内预离的
        /// </summary>
        [Display(Name = "预离方式对应的具体数值")]
        public int DepartureType2Value { set; get; }

        [Display(Name = "每行显示房间数量")]
        public int RoomNumberEveryRow { set; get; }


        /// <summary>
        /// 是否显示续住图标
        /// </summary>
        [Display(Name = "是否显示续住图标")]
        public bool IsShowContinueLiveIcon { set; get; }

        /// <summary>
        /// 是否显示锁房图标
        /// </summary>
        [Display(Name = "是否显示锁房图标")]
        public bool IsShowLockRoomIcon { set; get; }

        /// <summary>
        /// 是否显示余额
        /// </summary>
        [Display(Name = "是否显示余额")]
        public bool IsShowLeftMoney { set; get; }

        /// <summary>
        /// 是否显示余额图标
        /// </summary>
        [Display(Name = "是否显示余额图标")]
        public bool IsShowLeftMoneyIcon { set; get; }
        
        /// <summary>
        /// 是否显示钟点房时间
        /// </summary>
        [Display(Name = "是否显示钟点房时间")]
        public bool IsShowHourRoomTime { set; get; }

        /// <summary>
        /// 是否显示预定房到店时间
        /// </summary>
        [Display(Name = "是否显示预定房到店时间")]
        public bool IsShowReserveRoomComingTime { set; get; }

        /// <summary>
        /// 是否显示脏住房图标
        /// </summary>
        [Display(Name = "是否显示脏住房图标")]
        public bool IsShowDirtyAlreadyRoomIcon { set; get; }

        /// <summary>
        /// 是否显示凌晨房图标
        /// </summary>
        [Display(Name = "是否显示凌晨房图标")]
        public bool IsShowLCRoomIcon { set; get; }

        /// <summary>
        /// 是否显示人数图标
        /// </summary>
        [Display(Name = "是否显示人数图标")]
        public bool IsShowCustomerQuantityIcon { set; get; }

        [Display(Name = "是否显示客人名称")]
        public bool IsShowGuestName { set; get; }
        
        /// <summary>
        /// 是否显示房卡数量
        /// </summary>
        [Display(Name = "是否显示房卡数量")]
        public bool IsShowRoomCardQuantity { set; get; }

        [Display(Name = "是否显示特征描述")]
        public bool IsShowRoomRemark { set; get; }
        
        /// <summary>
        /// 是否显示物品租赁图标
        /// </summary>
        [Display(Name = "是否显示物品租赁图标")]
        public bool IsShowGoodsRentIcon { set; get; }

        /// <summary>
        /// 是否显示月租房图标
        /// </summary>
        [Display(Name = "是否显示月租房图标")]
        public bool IsShowMonthlyRentRoomIcon { set; get; }

        /// <summary>
        /// 是否显示免费房图标
        /// </summary>
        [Display(Name = "是否显示免费房图标")]
        public bool IsShowFreeRoomIcon { set; get; }

        /// <summary>
        /// 是否显示钟点房图标
        /// </summary>
        [Display(Name = "是否显示钟点房图标")]
        public bool IsShowHourRoomIcon { set; get; }

        /// <summary>
        /// 是否显示换房图标
        /// </summary>
        [Display(Name = "是否显示换房图标")]
        public bool IsShowChangeRoomIcon { set; get; }
        /// <summary>
        /// 是否显示租赁物品图标
        /// </summary>
        [Display(Name = "是否显示租赁物品图标")]
        public bool IsShowMaterialLeaseIcon { set; get; }

        /// <summary>
        /// 是否显示特要说明图标
        /// </summary>
        [Display(Name = "是否显示特要说明图标")]
        public bool IsShowRoomMessageIcon { set; get; }
        
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
