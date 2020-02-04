using System.Collections.Generic;

namespace MC.ORM
{
    /// <summary>
    /// 翻页列表中到的返回模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Page<T>
    {
        /// <summary>
        ///     当前页
        /// </summary>
        public long CurrentPage { get; set; }

        /// <summary>
        ///     总页数
        /// </summary>
        public long TotalPages { get; set; }

        /// <summary>
        ///    记录总数
        /// </summary>
        public long TotalItems { get; set; }

        /// <summary>
        ///     每个数
        /// </summary>
        public long ItemsPerPage { get; set; }

        /// <summary>
        ///    列表T
        /// </summary>
        public List<T> Items { get; set; }

        /// <summary>
        ///     User property to hold anything.
        /// </summary>
        public object Context { get; set; }
    }
}
