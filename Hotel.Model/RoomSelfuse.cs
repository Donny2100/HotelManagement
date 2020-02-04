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
    [TableName("room_selfuse"), PrimaryKey("Id")]
  public  class RoomSelfuse: BaseModel
    {

        #region 属性
        [JsonConverter(typeof(LongToStringConverter))]
        public int Id { get; set; }

        [Display(Name = "房间id")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long RoomId { set; get; }


        [Display(Name = "房间号")]
        public string RoomNO { set; get; }

        [Display(Name = "单据号--系统生成")]
        public string DjNum { set; get; }

        [Display(Name = "用于记录最大单据号的年月日")]
        public long Nyr { set; get; }

        [Display(Name = "用于记录最大单据号")]
        public long MaxDjNum { set; get; }

        [Display(Name = "创建日期")]
        public string CDate { set; get; }

        [Display(Name = "创建人")]
        public long CreateHanlderId { set; get; }
        [Display(Name = "创建人")]
        public string CreateHanlderName { set; get; }
        [Display(Name = "自用原因")]
        public string ZyReason { set; get; }
        [Display(Name = "起始日期")]
        public string StartDate { set; get; }
        [Display(Name = "预计结束日期")]
        public string YEndDate { set; get; }


        [Display(Name = "结束时间")]
        public string EndTime { set; get; }

        [Display(Name = "手工单号")]
        public string Sgdh { set; get; }

        [Display(Name = "备注")]
        public string Remark { set; get; }

        [Display(Name = "更新人")]
        public long UpdateHanlderId { set; get; }

        [Display(Name = "更新人")]
        public string UpdateHanlderName { set; get; }

        [Display(Name = "更新时间")]
        public string UpdateTime        { set; get; }


     
        /// <summary>
        /// 审核状态 0位代审核 1为审核通过
        /// </summary>
        [Display(Name = "审核状态")]
        public int ConfirmState { set; get; }

        [Display(Name = "审核人")]
        public int ConfirmHanlderId { set; get; }

        [Display(Name = "审核人")]
        public string ConfirmHanlderName { set; get; }

        [Display(Name = "审核时间")]
        public string ConfirmTime { set; get; }

        [Display(Name = "酒店id")]
        public long HotelId { set; get; }

    






        #endregion

        #region 扩展属性


        [Ignore]
        public string StateStr
        {
            get
            {
                return string.IsNullOrEmpty(this.EndTime) ? "正常" : "已结束";
            }
        }

        #endregion

        #region 方法


        #endregion
    }
}
