using MC.ORM;
using Newtonsoft.Json;
using NIU.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{
    /// <summary>
    /// 客户协议
    /// </summary>
    [TableName("agree_company_khxy"), PrimaryKey("Id")]
    public class AgreeCompanyKhxy : BaseModel
    {
        #region 属性

        /// <summary>
        /// Id
        /// </summary>
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
        /// 协议主题
        /// </summary>
        [Display(Name = "协议主题")]
        public string Theme { set; get; }

        /// <summary>
        /// 协议类型
        /// </summary>
        [Display(Name = "协议类型")]
        public int XyType { set; get; }

        /// <summary>
        /// 协议单号
        /// </summary>
        [Display(Name = "协议单号")]
        public string XyNum { set; get; }

        /// <summary>
        /// 是否按房类记佣
        /// </summary>
        [Display(Name = "是否按房类记佣")]
        public bool IsAfljy { set; get; }

        /// <summary>
        /// 佣金
        /// </summary>
        [Display(Name = "佣金")]
        public decimal Commission { set; get; }

        /// <summary>
        /// 是否每日记佣
        /// </summary>
        [Display(Name = "是否每日记佣")]
        public bool IsMrjy { set; get; }

        /// <summary>
        /// 前台是否显示佣金金额
        /// </summary>
        [Display(Name = "前台是否显示佣金金额")]
        public bool IsQtxsyjje { set; get; }

        /// <summary>
        /// 是否使用固定房价折扣
        /// </summary>
        [Display(Name = "是否使用固定房价折扣")]
        public bool IsSygdfjzk { set; get; }

        /// <summary>
        /// 折扣
        /// </summary>
        [Display(Name = "折扣")]
        public decimal Sale { set; get; }

        /// <summary>
        /// 早餐份数
        /// </summary>
        [Display(Name = "早餐份数")]
        public int BreakfastCount { set; get; }

        /// <summary>
        /// 客户签约人
        /// </summary>
        [Display(Name = "客户签约人")]
        public string KhQyr { set; get; }

        /// <summary>
        /// 本司签约人
        /// </summary>
        [Display(Name = "本司签约人")]
        public string BsQyr { set; get; }

        /// <summary>
        /// 房间号---用户输入--只是备注
        /// </summary>
        [Display(Name = "房间号")]
        public string RoomNO { set; get; }

        /// <summary>
        ///签约日期
        /// </summary>
        [Display(Name = "签约日期")]
        public DateTime QyDate { set; get; }

        /// <summary>
        ///有效期
        /// </summary>
        [Display(Name = "有效期")]
        public DateTime ExpireDate { set; get; }

        /// <summary>
        ///备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { set; get; }

        /// <summary>
        ///协议内容--最多1000字
        /// </summary>
        [Display(Name = "协议内容")]
        public string XyContent { set; get; }

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
        /// 审核人--数据冗余
        /// </summary>
        [JsonConverter(typeof(LongToStringConverter))]
        public long ConfirmId { set; get; }

        /// <summary>
        /// 审核人--数据冗余
        /// </summary>
        [Display(Name = "审核人")]
        public string ConfirmName { set; get; }

        /// <summary>
        /// 审核状态  0：未审核  1：已审核
        /// </summary>
        [Display(Name = "审核状态")]
        public int ConfirmState { set; get; }

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

        /// <summary>
        /// 协议类型
        /// </summary>
        [Ignore]
        public string XyTypeName
        {
            get
            {
                if (XyType <= 0)
                    return string.Empty;
                else
                {
                    List<XyType> list = new List<XyType>();
                    string result = string.Empty;
                    try
                    {
                        string path = System.Web.HttpContext.Current.Server.MapPath("/") + "Config/xytype.json";
                        result = File.ReadAllText(path);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("xytype.json配置错误:" + ex.Message);
                    }

                    if (!string.IsNullOrEmpty(result))
                    {
                        try
                        {
                            list = JsonConvert.DeserializeObject<List<XyType>>(result);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("反序列化xytype.json出错:" + ex.Message);
                        }
                    }
                    return list.SingleOrDefault(m => m.Id == XyType)?.Name;
                }
            }

            #endregion
          
        }

        #region 方法


        #endregion

    }
}
