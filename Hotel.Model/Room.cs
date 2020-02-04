using MC.ORM;
using Newtonsoft.Json;
using NIU.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NIU.Forum.Common;

namespace Hotel.Model
{
    /// <summary>
    /// 房间模型
    /// </summary>
    [TableName("room"), PrimaryKey("id")]
    public class Room : BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long Id { set; get; }

        /// <summary>
        /// 楼栋Id
        /// </summary>
        [Display(Name = "楼栋")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long BuildId { set; get; }

        /// <summary>
        /// 楼层Id
        /// </summary>
        [Display(Name = "楼层")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long FloorId { set; get; }

        /// <summary>
        /// 房号
        /// </summary>
        [Display(Name = "房号")]
        public string RoomNO { set; get; }

        /// <summary>
        /// 房型Id
        /// </summary>
        [Display(Name = "房型")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long RoomTypeId { set; get; }

        /// <summary>
        /// 特征描述
        /// </summary>
        [Display(Name = "特征描述")]
        public string Remark { set; get; }

        [Display(Name = "自用房编号")]
        public string RoomSelfuseId { get; set; }

        [Display(Name = "房间锁定")]
        public int IsLock { get; set; }

        [Display(Name = "锁定备注")]
        public string LockBz { get; set; }

        /// <summary>
        /// 房态   0：干净房  1：预定房   2：在住房  3：脏住房（夜审后的在住房的全天房）  4：脏房（退房后）   5：维修房   6：自用房
        /// </summary>
        public FjStateEnum FjState { set; get; }

        [Ignore]
        public MaintainRoom MaintainRoom { get; set; }
        [Ignore]
        public RoomSelfuse RoomSelfuse { get; set; }

        [Ignore]
        public string FjStateName
        {
            get {
                switch (FjState)
                {
                    case FjStateEnum.干净房:
                        return FjStateEnum.干净房.ToString();
                    //case FjStateEnum.预定房:
                    //    return FjStateEnum.预定房.ToString();
                    case FjStateEnum.在住房:
                        return FjStateEnum.在住房.ToString();
                    case FjStateEnum.脏住房:
                        return FjStateEnum.脏住房.ToString();
                    case FjStateEnum.脏房:
                        return FjStateEnum.脏房.ToString();
                    case FjStateEnum.维修房:
                        return FjStateEnum.维修房.ToString();
                    case FjStateEnum.自用房:
                        return FjStateEnum.自用房.ToString();
                    default:
                        return "未知";
                }
            }
        }

        /// <summary>
        /// 房间登记id
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long RoomRegId { set; get; }

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
        /// 最后一次清理时间
        /// </summary>
        public int LastCleanDate { set; get; }
        
        /// <summary>
        /// 最后一次设置脏房的时间
        /// </summary>
        public int LastStartCleanDate { set; get; }

        /// <summary>
        /// 最后一次清理房间员工id
        /// </summary>
        public long LastCleanEmployeeId{get;set;}



        public bool IsMaterialChangeEveryday { get; set; }

        public bool IsMaterialChangeWash { get; set; }


        public int LastChangeRoomDate { get; set; }


        #endregion

        #region 扩展属性
        [Ignore]
        public decimal BedCount { get; set; }
        [Ignore]
        public int ManNumber { get; set; }

        [Ignore]
        public string[] LFRoomNos { get; set; }

        [Ignore]
        public bool IsBuZu { get; set; }
        [Ignore]
        public bool IsQianfei { get; set; }
        [Ignore]
        public bool IsLC { get; set; }

        /// <summary>
        /// 是否包含租赁物品
        /// </summary>
        [Ignore]
        public bool HasMaterialLease { get; set; }
        
        [Ignore]
        public string GuestInfo { get; set; }
        [Ignore]
        public string FirstGuestName { get; set; }
        [Ignore]
        public string FirstGuestSex { get; set; }
        [Ignore]
        public List<string> GuestNames { get; set; }
        [Ignore]
        public List<string> GuestCertificateNOs { get; set; }
        [Ignore]
        public string MemberInfo { get; set; }

        [Ignore]
        public decimal SalePrice2 { get; set; }

        /// <summary>
        /// PS:房态使用
        /// </summary>
        [Ignore]
        public string SimpleJson
        {
            get
            {
                var InFullDateStr = ""; 
                if (this.RoomReg != null && this.RoomReg.KaiFangFangShiName == "钟点房")
                {
                    var span = DateTime.Now - TypeConvert.IntToDateTime(this.RoomReg.RegTime);

                    InFullDateStr = span.Hours.ToString().PadLeft(2,'0') + ":" + span.Minutes.ToString().PadLeft(2, '0');


                }
 

                return JsonConvert.SerializeObject(new
                { 
                    this.IsBuZu, //是否余额不足
                    
                    this.IsLC,//是否凌晨房
                    this.FirstGuestName,
                    this.FirstGuestSex,
                    this.GuestNames,
                    this.GuestCertificateNOs,
                    this.HasMaterialLease,
                    this.IsQianfei,
                    this.RoomNO,
                    this.IsLock,
                    this.LockBz,
                    this.FjState,
                    this.FjStateName, 
                    RoomTypeId = this.RoomTypeId.ToString(),
                    this.RoomTypeName,
                    this.LeavelDays,
                    this.LastChangeRoomDate,
                    this.GuestInfo,
                    InFullDateStr = InFullDateStr,
                    this.IsXZToday,
                    this.IsXZ,
                    this.RoomReg_Jy,
                    this.RoomReg_XykYsqMoney,
                    this.RoomReg_Money,
                    this.RoomReg_Yjs,
                    this.RoomReg_Yszk,
                    this.RoomReg_Yuszk,
                    this.LastCleanDateStr,
                    this.YDDays,
                    this.SalePrice2,
                    RoomRemark = this.Remark,
                    CardCount = this.RoomReg == null ? 0 : this.RoomReg.CardCount, 
                    Cph = this.RoomReg == null ? "" : this.RoomReg.Cph,
                    IsZDF = this.RoomReg == null ? false : this.RoomReg.KaiFangFangShiName == "钟点房", //是否钟点房
                    IsAddToday = this.RoomReg == null ? false : (TypeConvert.IntToDateTime(this.RoomReg.RegTime).ToString("yyy-MM-dd") == DateTime.Now.ToString("yyy-MM-dd")),
                    VipLevel = this.RoomReg == null ? 0 : this.RoomReg.VipLevel,
                    MemberCardNO = this.RoomReg == null ? "" : this.RoomReg.MemberCardNO,
                    KaiFangFangShi = this.RoomReg == null ? 0 : this.RoomReg.KaiFangFangShi,
                    KaiFangFangShiName = this.RoomReg == null ? "" : this.RoomReg.KaiFangFangShiName,
                    AgreeCompanyName = this.RoomReg == null ? "" : this.RoomReg.AgreeCompanyName,
                    LFRoomNos = this.LFRoomNos == null ? "" : string.Join(",", LFRoomNos),
                    LFZfDjId = (this.LFRoomNos == null || this.LFRoomNos.Count() <= 1) ? "0" : this.RoomReg.ZfDjId.ToString(), //联房主房登记Id，没有联房的时候为0
                    CustomerType = this.RoomReg == null ? 0 :  this.RoomReg.CustomerType,
                    this.MemberInfo,


                   
                    IsSecrecy = this.RoomReg == null ? false : this.RoomReg.IsSecrecy,
                    Name = this.RoomReg == null ? "" : this.RoomReg.Name,
                    Sex = this.RoomReg == null ? "" : this.RoomReg.Sex,
                    CertificateTypeName = this.RoomReg == null ? "" : this.RoomReg.CertificateTypeName,
                    CertificateNO = this.RoomReg == null ? "" : this.RoomReg.CertificateNO,
                    Address = this.RoomReg == null ? "" : this.RoomReg.Address,
                    Tel = this.RoomReg == null ? "" : this.RoomReg.Tel,

                    RegTime_Str = this.RoomReg == null || this.RoomReg.RegTime == 0 ? "" : TypeConvert.IntToDateTime(this.RoomReg.RegTime).ToString("yyyy-MM-dd HH:mm"),

                    RegTimeStr = this.RoomReg == null || this.RoomReg.RegTime == 0 ? "" : this.RoomReg.RegTimeStr,
                    LeaveTimeStr = this.RoomReg == null || this.RoomReg.LeaveTime == 0 ? "" : this.RoomReg.LeaveTimeStr,

                    RegTimeStr2 = this.RoomReg == null || this.RoomReg.RegTime == 0 ? "" : this.RoomReg.RegTimeStr2,
                    LeaveTimeStr2 = this.RoomReg == null || this.RoomReg.LeaveTime == 0 ? "" : this.RoomReg.LeaveTimeStr2,

                    this.Price,

                   
                   SalePrice = this.RoomReg == null ? 0 : this.RoomReg.SalePrice,
                   Remark = this.RoomReg == null ? "" : this.RoomReg.Remark,
                   BedCount  = this.RoomType == null ? "" : this.RoomType.BedCount.ToString(),
                 
                   MAresult = this.MaintainRoom == null ? "" : this.MaintainRoom.MAresult,
                   YjEndTimer = this.MaintainRoom == null ? "" : this.MaintainRoom.YjEndTimer,
                   MOperator = this.MaintainRoom == null ? "" : this.MaintainRoom.MOperator,
                   YEndDate = this.RoomSelfuse == null ? "" : this.RoomSelfuse.YEndDate,
                   ZyReason = this.RoomSelfuse == null ? "" : this.RoomSelfuse.ZyReason,

                   this.IsYD,
                   YD_Name = this.RoomYd == null ? "" : this.RoomYd.Name,
                    YD_YudaoTime2 = this.RoomYdRecord == null ? "" : this.RoomYdRecord.YudaoTimeStr2,
                    YD_YudaoTime = this.RoomYdRecord == null ? "" : this.RoomYdRecord.YudaoTimeStr,
                   YD_YuliTime = this.RoomYdRecord == null ? "" : this.RoomYdRecord.YuliTimeStr,
                    YD_YuliTime2 = this.RoomYdRecord == null ? "" : this.RoomYdRecord.YuliTimeStr2,
                    YD_Tel = this.RoomYd == null ? "" : this.RoomYd.Tel,
                   YD_Remark = this.RoomYd == null ? "" : this.RoomYd.Remark,

                    MaintainCause = this.MaintainRoom == null ? "" : this.MaintainRoom.MaintainCause
                });
            }
        }

        [Ignore]
        public RoomReg RoomReg { set; get; }
         
        /// <summary>
        /// 是否已经预定
        /// </summary>
        [Ignore]
        public bool IsYD { set; get; }
 
        [Ignore]
        public string YDDays { get; set; }

        [Ignore]
        public RoomYd RoomYd { get; set; }
        [Ignore]
        public RoomYdRecord RoomYdRecord { get; set; }
        [Ignore]
        public string RoomReg_Money
        {
            get
            {
                if (this.RoomReg == null) return "";
                return this.RoomReg.Money.ToString();

            }
        }
        [Ignore]
        public string RoomReg_Yuszk
        {
            get
            {
                if (this.RoomReg == null) return "";
                return this.RoomReg.Yuszk.ToString();

            }
        }
        [Ignore]
        public string RoomReg_Yjs
        {
            get
            {
                if (this.RoomReg == null) return "";
                return this.RoomReg.Yjs.ToString();

            }
        }
        [Ignore]
        public decimal RoomReg_XykYsqMoney
        {
            get
            {
                if (this.RoomReg == null) return 0;
                return this.RoomReg.XykYsqMoney;

            }
        }

        [Ignore]
        public decimal RoomReg_Jy
        {
            get
            {
                if (this.RoomReg == null) return 0;
                return this.RoomReg.Jy;

            }
        }
        [Ignore]
        public string RoomReg_Yszk
        {
            get
            {
                if (this.RoomReg == null) return "";
                return this.RoomReg.Yszk.ToString();

            }
        }
        /// <summary>
        /// 楼栋
        /// </summary>
        [Ignore]
        public BuildFloor Build { set; get; }


        [Ignore]
        public bool IsXZToday
        {
            get
            {
                if (this.RoomReg == null) return false;
                if (this.RoomReg.XZDate == 0) return false;
                var date = NIU.Forum.Common.TypeConvert.IntToDateTime(this.RoomReg.XZDate);

                DateTime today = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                if (date > today) return true;
                return false;
            }
        }
        [Ignore]
        public bool IsXZ
        {
            get
            {
                if (this.RoomReg == null) return false;
                return this.RoomReg.XZDate != 0;
            }
        }

        [Ignore]
        public bool IsLeavelToday
        {
            get
            {
                if (this.RoomReg == null) return false;
                if (this.RoomReg.LeaveTime == 0) return false;
                var date = NIU.Forum.Common.TypeConvert.IntToDateTime(this.RoomReg.LeaveTime);

                DateTime start = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                DateTime end = Convert.ToDateTime(date.ToShortDateString());
                TimeSpan sp = end.Subtract(start);
                int days = sp.Days;
                if (days == 0) return true;
                return false;
            }
        }
        [Ignore]
        public string LeavelDays
        {
            get
            {
                if (this.RoomReg == null) return "";
                if (this.RoomReg.LeaveTime == 0) return "";
                var date = NIU.Forum.Common.TypeConvert.IntToDateTime(this.RoomReg.LeaveTime);

                DateTime start = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                DateTime end = Convert.ToDateTime(date.ToShortDateString());
                TimeSpan sp = end.Subtract(start);
                int days = sp.Days;
                if (days == 0) return "";
                return days.ToString();
            }
        }

        [Ignore]
        public string LastCleanDateStr
        {
            get
            {
                if (LastCleanDate == 0) return "";
                return NIU.Forum.Common.TypeConvert.IntToDateTime(LastCleanDate).ToString("yyy-MM-dd hh:mm");
            }
        }


       
        /// <summary>
        /// 楼栋
        /// </summary>
        [Ignore]
        public string BuildName { set; get; }

        /// <summary>
        /// 楼层
        /// </summary>
        [Ignore]
        public BuildFloor Floor { set; get; }

        /// <summary>
        /// 楼层
        /// </summary>
        [Ignore]
        public string FloorName { set; get; }

        /// <summary>
        /// 房型
        /// </summary>
        [Ignore]
        public RoomType RoomType { set; get; }

        /// <summary>
        /// 房型
        /// </summary>
        [Ignore]
        public string RoomTypeName { set; get; }

        /// <summary>
        /// 价格
        /// </summary>
        [Ignore]
        [Display(Name = "价格")]
        public decimal Price { set; get; }



        #endregion

        #region 方法


        #endregion
    }

    /// <summary>
    /// 房间状态枚举 0：干净房  1：预定房   2：在住房  3：脏住房（夜审后的在住房的全天房）  4：脏房（退房后）   5：维修房   6：自用房
    /// </summary>
    public enum FjStateEnum
    {
        干净房 = 0,
        预定房_移除 = 1,
        在住房 = 2,

        /// <summary>
        /// 夜审后的在住房的全天房
        /// </summary>
        脏住房 = 3,

        /// <summary>
        /// 退房后
        /// </summary>
        脏房 = 4,
        维修房 = 5,
        自用房 = 6
    }
}
