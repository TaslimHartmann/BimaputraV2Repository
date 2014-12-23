using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace AdvancedReportViewer.Utils
{
    public static class Extensions
    {
        /// <summary>
        /// Get name of the data source that will satisfy ReportViewer.
        /// </summary>
        /// <remarks>
        /// ReportName was guessed by ReportViewer error messages.
        /// </remarks>
        public static string ToReportName( this Type type )
        {
            var isTypedDataTable =
                type.IsNested &&
                type.BaseType.FullName.StartsWith( "System.Data.TypedTableBase" );

            if( isTypedDataTable )
            {
                // in:  Some.Namespace.CategoryDataSet+CategoryDataTable
                // out: CategoryDataSet_Category
                var match = Regex.Match( type.FullName, @"^.+\.(\w+\+\w+)DataTable$" );
                return match.Groups[ 1 ].Value.Replace( "+", "_" );
            }
            else
            {
                // in:  Some.Namespace.TypeName
                // out: Some_Namespace_TypeName
                return type.FullName.Replace( ".", "_" );
            }
        }

        /// <summary>
        /// Do the action for all enum elements
        /// </summary>
        /// <param name="source">The enum</param>
        /// <param name="action">What to do</param>
        /// <returns>Reference to the enum</returns>
        /// <remarks>NOTE: Unfortunately there is no such extension in LINQ</remarks>
        public static IEnumerable<T> Action<T>( this IEnumerable<T> source, Action<T> action )
        {
            foreach( T element in source )
            {
                action( element );
            }

            return source;
        }
    }
}
