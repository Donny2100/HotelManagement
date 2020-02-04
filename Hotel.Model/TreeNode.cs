using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{
    public class TreeNode
    {
        public long id { get; set; }

        public string text { get; set; }

        public string state { get; set; }

        public List<TreeNode> children { get; set; }
    }
}
