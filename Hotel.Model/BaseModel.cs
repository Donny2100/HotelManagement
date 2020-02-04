using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{
    [Serializable]
    public class BaseModel
    {
        public override string ToString()
        {
            string ret = string.Empty;
            Type type = this.GetType();
            PropertyInfo[] list = type.GetProperties();
            if (list == null || !list.Any())
                return ret;
            foreach (var item in list)
            {
                if (item.Name == "Created")
                    continue;
                var obj = item.GetValue(this);
                if (obj == null)
                    ret += item.Name + ":null-";
                else
                    ret += item.Name + ":" + item.GetValue(this).ToString() + "-";
            }
            return ret;
        }
    }
}
