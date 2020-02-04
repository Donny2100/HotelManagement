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
    [TableName("room_maintain"), PrimaryKey("Id")]
    public class MaintainRoom : BaseModel
    {

        #region 属性
        [JsonConverter(typeof(LongToStringConverter))]
        public int Id { get; set; }

        [Display(Name = "房间id")]
        [JsonConverter(typeof(LongToStringConverter))]
        public long RoomId { set; get; }


        [Display(Name = "单据号--系统生成")]
        public string DjNumber { set; get; }


        [Display(Name = "维修原因")]
        public string MaintainCause { set; get; }
        [Display(Name = "开始时间")]
        public string StatrTimer { set; get; }
        [Display(Name = "结束时间")]
        public string YjEndTimer { set; get; }

        [Display(Name = "实际结束时间")]
        public string EndTime { set; get; }

        [Display(Name = "添加日期")]
        public string Addtimer { set; get; }


        [Display(Name = "维修结果")]
        public string MAresult { get; set; }

        [Display(Name = "备注")]
        public string Remark { set; get; }

        
        [Display(Name = "手工单号")]
        public string SdNum { set; get; }
        /// <summary>
        /// 1为通过审核
        /// </summary>
        [Display(Name = "审核状态")]
        public int ConfirmState { set; get; }
        [Display(Name = "修改人")]
        public string MModifier { set; get; }


        [Display(Name = "审核人")]
        public string MAudit { set; get; }



        [Display(Name = "酒店id")]
        public long HotelId { set; get; }

        [Display(Name = "操作人")]
        public string MOperator { set; get; }







        #endregion

        #region 扩展属性

        [Ignore]
        public string RoomNO { set; get; }
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
