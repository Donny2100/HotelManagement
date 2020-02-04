using MC.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{
    /// <summary>
    /// 楼栋楼层模型
    /// </summary>
    [TableName("area"), PrimaryKey("accid")]
    public class Area
    {
        public string Accid { set; get; }

        public string Name { set; get; }

        public string ShortName { set; get; }

        public string Pid { set; get; }
    }
}
