using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using VDMJasminka.Core.Exceptions;

namespace VDMJasminka.Core.Extensions
{
    public static class MappingExtension
    {
        /// <summary>
        /// Maps the entity properties to the DB column names provided in the description attributes of the same entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public static void MapDbProperties<T>(this T entity) where T : class
        {
            try
            {
                var map = new CustomPropertyTypeMap(typeof(T), (type, columnName) =>
                    type.GetProperties().FirstOrDefault(prop =>
                    {
                        return GetDescriptionFromAttribute(prop) == columnName;
                    }
                    ));

                SqlMapper.SetTypeMap(typeof(T), map);
            }
            catch (Exception)
            {

                throw;
            }

        }
        static string GetDescriptionFromAttribute(MemberInfo member)
        {
            if (member == null) return null;

            var attrib = (DescriptionAttribute)Attribute.GetCustomAttribute(member, typeof(DescriptionAttribute), false);

            return attrib == null ? throw new PropertyMappingException(member) : attrib.Description;

        }
    }

}
