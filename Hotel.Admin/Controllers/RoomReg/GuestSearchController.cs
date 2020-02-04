using Hotel.Bll;
using Newtonsoft.Json;
using NIU.Common.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;

namespace Hotel.Admin.Controllers.RoomReg
{
    /// <summary>
    /// 客人查询
    /// </summary>
    public class GuestSearchController : AdminBaseController
    {
        // GET: GuestSearch
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取房单列表
        /// </summary>
        /// <param name="roomNO">房号</param>
        /// <param name="name">客人姓名</param>
        /// <param name="customerType">客人类型</param>
        /// <param name="rzlx">入住类型</param>
        /// <param name="sddsj">开始抵店时间</param>
        /// <param name="eddsj">结束抵店时间</param>
        /// <param name="sldsj">开始离店时间</param>
        /// <param name="eldsj">结束离店时间</param>
        /// <param name="yudNum">预订号</param>
        /// <param name="agreeComp">协议单位</param>
        /// <param name="tel">手机号</param>
        /// <param name="cph">车牌号</param>
        /// <returns></returns>
        public string GetFdList(int page, int rows, string roomNO = "", string name = "", int customerType = 0, int rzlx = 0,
            string sddsj = "", string eddsj = "", string sldsj = "", string eldsj = "", string yudNum = "",
            string agreeComp = "", string tel = "", string cph = "")
        {
            var datas = RoomRegBll.GetFdList(page, rows, roomNO, name, customerType, rzlx, sddsj, eddsj, sldsj, eldsj, yudNum, agreeComp, tel, cph);
            return JsonConvert.SerializeObject(datas);
        }

        public string GetZzfdPager(int page, int rows, string roomNO = "", string name = "", int customerType = 0, int rzlx = 0,
            string sddsj = "", string eddsj = "", string sldsj = "", string eldsj = "", string yudNum = "",
            string agreeComp = "", string tel = "", string cph = "")
        {
            var datas = RoomRegBll.GetZzfdPager(UserContext.CurrentUser.HotelId, page, rows, roomNO, name, customerType,
                rzlx, sddsj, eddsj, sldsj, eldsj, yudNum, agreeComp, tel, cph);
            return JsonConvert.SerializeObject(datas);
        }

        public string GetDrldPager(int page, int rows, string roomNO = "", string name = "", int customerType = 0, int rzlx = 0,
            string sddsj = "", string eddsj = "", string sldsj = "", string eldsj = "", string yudNum = "",
            string agreeComp = "", string tel = "", string cph = "")
        {
            var datas = RoomRegBll.GetDrldPager(UserContext.CurrentUser.HotelId, page, rows, roomNO, name, customerType,
                rzlx, sddsj, eddsj, sldsj, eldsj, yudNum, agreeComp, tel, cph);
            return JsonConvert.SerializeObject(datas);
        }

        public string GetLffdPager(int page, int rows, string roomNO = "", string name = "", int customerType = 0, int rzlx = 0,
            string sddsj = "", string eddsj = "", string sldsj = "", string eldsj = "", string yudNum = "",
            string agreeComp = "", string tel = "", string cph = "")
        {
            var datas = RoomRegBll.GetLffdPager(UserContext.CurrentUser.HotelId, page, rows, roomNO, name, customerType,
                rzlx, sddsj, eddsj, sldsj, eldsj, yudNum, agreeComp, tel, cph);
            return JsonConvert.SerializeObject(datas);
        }

        public string GetWjldPager(int page, int rows, string roomNO = "", string name = "", int customerType = 0, int rzlx = 0,
           string sddsj = "", string eddsj = "", string sldsj = "", string eldsj = "", string yudNum = "",
           string agreeComp = "", string tel = "", string cph = "")
        {
            //MessageBox.Show("123");
            var datas = RoomRegBll.GetWjldPager(UserContext.CurrentUser.HotelId, page, rows, roomNO, name, customerType,
                rzlx, sddsj, eddsj, sldsj, eldsj, yudNum, agreeComp, tel, cph);
            /*var datas = RoomRegBll.GetDrldPager(UserContext.CurrentUser.HotelId, page, rows, roomNO, name, customerType,
                rzlx, sddsj, eddsj, sldsj, eldsj, yudNum, agreeComp, tel, cph);*/

            return JsonConvert.SerializeObject(datas);
        }

        

        public string GetJztfPager(int page, int rows, string roomNO = "", string name = "", int customerType = 0, int rzlx = 0,
          string sddsj = "", string eddsj = "", string sldsj = "", string eldsj = "", string yudNum = "",
          string agreeComp = "", string tel = "", string cph = "")
        {
            var datas = RoomRegBll.GetYlwlPager(UserContext.CurrentUser.HotelId, page, rows, roomNO, name, customerType,
                rzlx, sddsj, eddsj, sldsj, eldsj, yudNum, agreeComp, tel, cph);
            return JsonConvert.SerializeObject(datas);
        }

        public void GetJbqkshPager(int page, int rows, string roomNO = "", string name = "", int customerType = 0, int rzlx = 0,
         string sddsj = "", string eddsj = "", string sldsj = "", string eldsj = "", string yudNum = "",
         string agreeComp = "", string tel = "", string cph = "")
        {
            
        }


        public string GetKfsptjPager(int page, int rows, string roomNO = "", string name = "", int customerType = 0, int rzlx = 0,
         string sddsj = "", string eddsj = "", string sldsj = "", string eldsj = "", string yudNum = "",
         string agreeComp = "", string tel = "", string cph = "")
        {
            
            var datas = RoomRegGoodsDetailsBll.GetListAll(UserContext.CurrentUser.HotelId, page, rows);
            //MessageBox.Show(datas.ToString());
            return JsonConvert.SerializeObject(datas);
        }

        public string GetYdfjPager(int page, int rows, string roomNO = "", string name = "", int customerType = 0, int rzlx = 0,
         string sddsj = "", string eddsj = "", string sldsj = "", string eldsj = "", string yudNum = "",
         string agreeComp = "", string tel = "", string cph = "")
        {
            var datas = RoomYdBll.GetYdfjPager(UserContext.CurrentUser.HotelId, page, rows, roomNO, name, customerType,
                rzlx, sddsj, eddsj, sldsj, eldsj, yudNum, agreeComp, tel, cph);
            return JsonConvert.SerializeObject(datas);
        }

        public string GetYqxydPager(int page, int rows, string roomNO = "", string name = "", int customerType = 0, int rzlx = 0,
         string sddsj = "", string eddsj = "", string sldsj = "", string eldsj = "", string yudNum = "",
         string agreeComp = "", string tel = "", string cph = "")
        {
            var datas = RoomYdBll.GetYqxydPager(UserContext.CurrentUser.HotelId, page, rows, roomNO, name, customerType,
                rzlx, sddsj, eddsj, sldsj, eldsj, yudNum, agreeComp, tel, cph);
            return JsonConvert.SerializeObject(datas);
        }

        public string GetNoshowPager(int page, int rows, string roomNO = "", string name = "", int customerType = 0, int rzlx = 0,
        string sddsj = "", string eddsj = "", string sldsj = "", string eldsj = "", string yudNum = "",
        string agreeComp = "", string tel = "", string cph = "")
        {
            var datas = RoomYdBll.GetNoshowPager(UserContext.CurrentUser.HotelId, page, rows, roomNO, name, customerType,
                rzlx, sddsj, eddsj, sldsj, eldsj, yudNum, agreeComp, tel, cph);
            return JsonConvert.SerializeObject(datas);
        }
    }
}