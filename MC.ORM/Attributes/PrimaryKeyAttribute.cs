using System;


namespace MC.ORM
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PrimaryKeyAttribute : Attribute
    {
        /// <summary>
        ///     The column name.
        /// </summary>
        /// <returns>
        ///     The column name.
        /// </returns>
        public string Value { get; private set; }

        /// <summary>
        ///     The sequence name.
        /// </summary>
        /// <returns>
        ///     The sequence name.
        /// </returns>
        public string SequenceName { get; set; }

        /// <summary>
        ///     A flag which specifies if the primary key is auto incrementing.
        /// </summary>
        /// <returns>
        ///     True if the primary key is auto incrementing; else, False.
        /// </returns>
        public bool AutoIncrement { get; set; }

        /// <summary>
        ///     Constructs a new instance of the <seealso cref="PrimaryKeyAttribute" />.
        /// </summary>
        /// <param name="primaryKey">The name of the primary key column.</param>
        public PrimaryKeyAttribute(string primaryKey)
        {
            Value = primaryKey;
            AutoIncrement = true;
        }
    }
}
