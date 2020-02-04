using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{
    /// <summary>
    /// 房太图实体类
    /// </summary>
  public  class RoomPattenView
    {
        /// <summary>
        /// 楼栋编号
        /// </summary>
        public int LouDongId { get; set; }

        /// <summary>
        /// 楼层编号
        /// </summary>
        public int LouCenID { get; set; }

        /// <summary>
        /// 房态状态
        /// </summary>
        public FjStateEnum RoomTyep{ get; set; }

        /// <summary>
        /// 入住房间类型
        /// </summary>
        public int RoomRegType { get; set; }

        /// <summary>
        /// 入住时间
        /// </summary>
        public string LiveDate { get; set; }

        /// <summary>
        /// 离开时间
        /// </summary>
        public int MyProperty { get; set; }

        /// <summary>
        /// 协议
        /// </summary>
        public string Xieyi { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        public int YuE { get; set; }



    }
}
