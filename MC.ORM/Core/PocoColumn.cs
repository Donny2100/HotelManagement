using System;
using System.Reflection;

namespace MC.ORM.Core
{
    public class PocoColumn
    {
        public string ColumnName;
        public bool ForceToUtc;
        public PropertyInfo PropertyInfo;
        public bool ResultColumn;

        public virtual void SetValue(object target, object val)
        {
            PropertyInfo.SetValue(target, val, null);
        }

        public virtual object GetValue(object target)
        {
            return PropertyInfo.GetValue(target, null);
            //return null;
        }

        public virtual object ChangeType(object val)
        {
            var t = PropertyInfo.PropertyType;
            if (val.GetType().IsValueType && PropertyInfo.PropertyType.IsGenericType && PropertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                t = t.GetGenericArguments()[0];

            return Convert.ChangeType(val, t);
        }
    }
}
