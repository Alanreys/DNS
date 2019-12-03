using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Assets.Develop.Scripts.Types
{
    public static class EnumExtensions
    {
        public static TAttribute GetAttribute<TAttribute>(this Enum value)
            where TAttribute : Attribute
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            return type.GetField(name)
                .GetCustomAttributes(false)
                .OfType<TAttribute>()
                .SingleOrDefault();
        }
    
        public static string GetCategory(this Enum value) => GetAttribute<CategoryAttribute>(value).Category;
    }


    public enum ObjectTypes
    {
        /// <summary>
        /// Витрина
        /// </summary>
        [Category("Витрина")]
        Showcase = 0,

        /// <summary>
        /// Железо
        /// </summary>
        [Category("Железо")]
        Iron = 1,

        /// <summary>
        /// Товар
        /// </summary>
        [Category("Товар")]
        Product = 2,

        /// <summary>
        /// Мебель
        /// </summary>
        [Category("Мебель")]
        Furniture = 3,

        /// <summary>
        /// Другое
        /// </summary>
        [Category("Другое")]
        Other = 4
    }
}
