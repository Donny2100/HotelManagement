using MC.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{
    /// <summary>
    ///  菜单模型
    /// </summary>
    [TableName("menu"), PrimaryKey("id")]
    public class Menu : BaseModel
    {
        #region 属性

        public long Id { set; get; }

        public string MenuController { set; get; }

        public long Pid { set; get; }

        public bool HasChild { set; get; }

        public string Name { set; get; }

        public string Url { set; get; }

        public string Icon { set; get; }

        public int Seq { set; get; }

        public int CDate { set; get; }

        #endregion

        #region 扩展属性

        /// <summary>
        /// treegrid属性
        /// </summary>
        public List<Menu> children { set; get; }

        #endregion

        #region 方法

        public Dictionary<string, string> GetHash()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            Type type = this.GetType();
            PropertyInfo[] list = type.GetProperties();
            if (list == null || !list.Any())
                return dic;
            foreach (var item in list)
            {
                if (item.Name == "children")
                    continue;
                dic.Add(item.Name, item.GetValue(this) == null ? string.Empty : item.GetValue(this).ToString());
            }
            return dic;
        }

        #endregion

    }
}
