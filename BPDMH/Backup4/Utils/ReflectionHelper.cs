using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;

namespace AdvancedReportViewer.Utils
{
    public static class ReflectionHelper
    {
        /// <summary>
        /// Get property value from object
        /// </summary>
        /// <param name="instance">Object instance that hosts the property</param>
        /// <param name="path">Path to the property</param>
        /// <returns>Value of the property</returns>
        /// <remarks>Works for pblic and private properties on the path</remarks>
        /// <example>GetPropertyValue( obj, "Item.SubItem.Name" )</example>
        public static object GetPropertyValue( object instance, string path )
        {
            if( string.IsNullOrEmpty( path ) ) return instance;

            // Walk all way down the path
            foreach( var element in path.Split( '.' ) )
            {
                var type = instance.GetType( );
                var property = type.GetProperty( element, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic );

                Debug.Assert(
                    property != null,
                    string.Format( "Can't find property {0} in object of type {1}.",
                        element,
                        type.FullName ) );

                instance = property.GetValue( instance, null );
            }

            return instance;
        }
    }
}
