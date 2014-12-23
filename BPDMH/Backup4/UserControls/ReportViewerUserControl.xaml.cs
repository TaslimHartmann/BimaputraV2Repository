using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using AdvancedReportViewer.Utils;
using Microsoft.Reporting.WinForms;
using Microsoft.Win32;

namespace AdvancedReportViewer.UserControls
{
    /// <summary>
    /// Interaction logic for ReportViewerUserControl.xaml
    /// </summary>
    public partial class ReportViewerUserControl: UserControl
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ReportViewerUserControl( )
        {
            InitializeComponent( );
            InitializeReportViewer( );
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Setting up ReportViewer
        /// </summary>
        /// <remarks>
        /// Render extensions must be initialized first
        /// </remarks>
        private void InitializeReportViewer( )
        {
            InitializeRenderExtensions( );
            InitializeIcons( );
            InitializeWordExport( );
            InitializeLocalization( );

            reportViewer.DocumentMapCollapsed = true;
            reportViewer.RefreshReport( );
        }

        #region Render Extensions

        /// <summary>
        /// Enable render to defined formats.
        /// Fix names for export options.
        /// </summary>
        /// <remarks>
        /// Localized names are used in other initialization procedures
        /// </remarks>
        private void InitializeRenderExtensions( )
        {
            EnableRenderExtension( "HTML4.0", "MS Word" );
            EnableRenderExtension( "Excel", "MS Excel" );
            EnableRenderExtension( "PDF", "Adobe PDF" );
        }

        /// <summary>
        /// Enable render extension for ReportViewer.LocalReport.
        /// </summary>
        /// <param name="extensionName">
        /// Extension name. Is taken from Microsoft.Reporting.ControlService.ListRenderingExtensions
        /// - HTML4.0
        /// - Excel
        /// - RGDI
        /// - IMAGE
        /// - PDF
        /// </param>
        /// <remarks>
        /// http://www.codeproject.com/KB/reporting-services/report-viewer-reflection.aspx
        /// http://beaucrawford.net/post/Enable-HTML-in-ReportViewer-LocalReport.aspx
        /// </remarks>
        private void EnableRenderExtension( string extensionName, string localizedExtensionName )
        {
            foreach( var extension in RenderingExtensions )
            {
                // name = extension.Name;
                var name = extension
                    .GetType( )
                    .GetProperty( "Name" )
                    .GetValue( extension, null )
                    .ToString( );

                if( name == extensionName )
                {
                    // extension.m_isVisible = true;
                    extension
                        .GetType( )
                        .GetField( "m_isVisible", BindingFlags.NonPublic | BindingFlags.Instance )
                        .SetValue( extension, true );

                    // extension.m_isExposedExternally = true;
                    extension
                        .GetType( )
                        .GetField( "m_isExposedExternally", BindingFlags.NonPublic | BindingFlags.Instance )
                        .SetValue( extension, true );

                    // extension.m_localizedName = localizedExtensionName;
                    extension
                        .GetType( )
                        .GetField( "m_localizedName", BindingFlags.NonPublic | BindingFlags.Instance )
                        .SetValue( extension, localizedExtensionName );
                }
            }
        }

        #endregion

        #region New Icons

        /// <summary>
        /// Fix icons on ReportViewer's toolbar
        /// </summary>
        private void InitializeIcons( )
        {
            // Fix find icons
            ToolStrip.Items[ "find" ].Image = Properties.Resources.Report_Find;
            ToolStrip.Items[ "findNext" ].Image = Properties.Resources.Report_FindNext;

            // Fix export icons
            var exportButton = ToolStrip.Items[ "export" ] as System.Windows.Forms.ToolStripDropDownButton;

            // Buttons are created on DropDownOpened so we can't assign Icon before it
            exportButton.DropDownOpened += delegate( object sender, EventArgs e )
            {
                var button = sender as System.Windows.Forms.ToolStripDropDownButton;
                if( button == null ) return;

                foreach( System.Windows.Forms.ToolStripItem item in button.DropDownItems )
                {
                    var extension = (RenderingExtension) item.Tag;

                    switch( extension.LocalizedName )
                    {
                    case "MS Word":
                        item.Image = Properties.Resources.Report_Word;
                        break;

                    case "MS Excel":
                        item.Image = Properties.Resources.Report_Excel;
                        break;

                    case "Adobe PDF":
                        item.Image = Properties.Resources.Report_PDF;
                        break;
                    }
                }
            };
        }

        #endregion

        #region Word Export

        /// <summary>
        /// Modify export to html so it will use our custom handler
        /// </summary>
        private void InitializeWordExport( )
        {
            // Suppress native export dialog
            reportViewer.ReportExport += ( sender, args ) => args.Cancel = args.Extension.LocalizedName == "MS Word";

            // Change export button handler
            var exportButton = ToolStrip.Items[ "export" ] as System.Windows.Forms.ToolStripDropDownButton;

            // Buttons are created on DropDownOpened so we can't assign handler before it
            exportButton.DropDownOpened += delegate( object sender, EventArgs e )
            {
                var button = sender as System.Windows.Forms.ToolStripDropDownButton;
                if( button == null ) return;

                foreach( System.Windows.Forms.ToolStripItem item in button.DropDownItems )
                {
                    var extension = (RenderingExtension) item.Tag;
                    if( extension.LocalizedName == "MS Word" )
                    {
                        item.Click += MSWordExport_Handler;
                    }
                }
            };
        }

        /// <summary>
        /// Html renderer is used to save in .doc file
        /// </summary>
        /// <remarks>
        /// DeviceInfo - http://msdn2.microsoft.com/en-us/library/ms155397.aspx
        /// </remarks>
        private void MSWordExport_Handler( object sender, EventArgs args )
        {
            // Ask user where to save
            var saveDialog = new SaveFileDialog
            {
                FileName = ReflectionHelper.GetPropertyValue( reportViewer.LocalReport, "DisplayNameForUse" ) + ".doc",
                DefaultExt = "doc",
                Filter = "MS Word (*.doc)|*.doc|All files (*.*)|*.*",
                FilterIndex = 0,
            };
            if( saveDialog.ShowDialog( ) != true ) return;

            // Create a report
            Warning[] warnings;
            using( var stream = File.Create( saveDialog.FileName ) )
            {
                reportViewer.LocalReport.Render(
                    "HTML4.0",
                    @"<DeviceInfo><ExpandContent>True</ExpandContent></DeviceInfo>",
                    (CreateStreamCallback) delegate { return stream; },
                    out warnings );
            }

            // Show user all warnings
            // NOTE: Default export handler doesn't do that
            if( warnings.Length > 0 )
            {
                var builder = new StringBuilder( );
                builder.AppendLine( "Please take notice that:" );

                warnings.Action( warning => builder.AppendLine( "- " + warning.Message ) );

                MessageBox.Show(
                    builder.ToString( ),
                    "Warnings",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning );
            }

            // Open created report
            // Process.Start( saveDialog.FileName );
        }

        #endregion

        #region Localization control

        /// <summary>
        /// Initialize localization control inside ReportViewer
        /// </summary>
        /// <remarks>
        /// Messages can be set only after supplying a data source
        /// </remarks>
        private void InitializeLocalization( )
        {
            var separator = new System.Windows.Forms.ToolStripSeparator( );
            ToolStrip.Items.Add( separator );

            var language = new System.Windows.Forms.ToolStripComboBox( );
            language.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            language.Items.Add( "English" );
            language.Items.Add( "Русский" );
            language.SelectedIndex = 0;
            language.SelectedIndexChanged += delegate
            {
                switch( (string) language.SelectedItem )
                {
                case "English":
                    reportViewer.Messages = null;
                    break;

                case "Русский":
                    reportViewer.Messages = new RussianReportViewerMessages( );
                    break;

                default:
                    Debug.Assert( false, "Unknown language: " + (string) language.SelectedItem );
                    break;
                }
            };
            ToolStrip.Items.Add( language );
        }

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Refresh ReportViewer
        /// </summary>
        /// <remarks>
        /// SetDisplayMode can be set only after supplying a data source
        /// </remarks>
        private void Refresh( )
        {
            if( !CanBeRefreshed ) return;

            reportViewer.SetDisplayMode( DisplayMode.PrintLayout );
            reportViewer.RefreshReport( );
        }

        /// <summary>
        /// Find subcontrol of defined type
        /// </summary>
        /// <param name="control">Start point of the search</param>
        private T FindControl<T>( System.Windows.Forms.Control control )
            where T: System.Windows.Forms.Control
        {
            if( control == null ) return null;

            if( control is T )
            {
                return (T) control;
            }

            foreach( System.Windows.Forms.Control subControl in control.Controls )
            {
                var result = FindControl<T>( subControl );
                if( result != null ) return result;
            }

            return null;
        }

        /// <summary>
        /// Create new data source for a report
        /// </summary>
        /// <remarks>
        /// There are some constraint on a data source type.
        /// This method incapsulates them.
        /// </remarks>
        private static ReportDataSource CreateReportDataSource( object originalDataObject )
        {
            string name = originalDataObject.GetType( ).ToReportName( );
            object value = originalDataObject;

            // DataTable
            if( originalDataObject is IListSource )
            {
            }
            // Collection
            if( originalDataObject is IEnumerable )
            {
                name = GetCollectionElementType( originalDataObject ).ToReportName( );
            }   
            // Just an object 
            else
            {
                value = new ArrayList { originalDataObject };
            }

            Debug.Assert( !string.IsNullOrEmpty( name ), "Data source's name must be defined " );
            Debug.Assert( value != null, "Data source must be defined" );
            return new ReportDataSource( name, value );
        }

        /// <summary>
        /// Get collection element type
        /// </summary>
        /// <param name="objectDataSource">Array or generic collection</param>
        /// <returns>Type of an element</returns>
        private static Type GetCollectionElementType( object objectDataSource )
        {
            Type elementType = typeof( object );

            foreach( var item in objectDataSource.GetType( ).GetInterfaces( ) )
            {
                // TODO: should only check collection's interfaces
                // NOTE: Dictionsry<T,S> will be processed wrong

                // If this is an array
                elementType = item.GetElementType( );
                if( elementType != null ) break;

                // If this is a generic colletion
                if( item.GetGenericArguments( ).Count( ) == 0 ) continue;
                elementType = item.GetGenericArguments( )[ 0 ];
                if( elementType != null ) break;
            }
            return elementType;
        }

        #endregion

        #region Dependency Properties

        /// <summary>
        /// Source for report data
        /// </summary>
        public static readonly DependencyProperty DataSourceProperty = DependencyProperty.Register(
            "DataSource",
            typeof( object ),
            typeof( ReportViewerUserControl ),
            new UIPropertyMetadata( DataSourceProperty_Changed ) );

        /// <summary>
        /// RDLC report added to project as Embedded Resource
        /// </summary>
        public static readonly DependencyProperty EmbeddedReportProperty = DependencyProperty.Register(
            "EmbeddedReport",
            typeof( string ),
            typeof( ReportViewerUserControl ),
            new UIPropertyMetadata( EmbeddedReport_Changed ) );


        /// <summary>
        /// Change ReportViewer's data source on DataSourceProperty change
        /// </summary>
        private static void DataSourceProperty_Changed( DependencyObject sender, DependencyPropertyChangedEventArgs args )
        {
            var control = (ReportViewerUserControl) sender;
            control.reportViewer.LocalReport.DataSources.Clear( );
            if( args.NewValue == null ) return;

            var reportDataSource = CreateReportDataSource( args.NewValue );
            control.reportViewer.LocalReport.DataSources.Add( reportDataSource );
            control.Refresh( );
        }

        /// <summary>
        /// Change ReportViewer's report on EmbeddedReport change
        /// </summary>
        private static void EmbeddedReport_Changed( DependencyObject sender, DependencyPropertyChangedEventArgs args )
        {
            if( args.NewValue == null ) return;
            var control = (ReportViewerUserControl) sender;

            var reportEmbeddedPath = (string) args.NewValue;
            control.reportViewer.LocalReport.ReportEmbeddedResource = reportEmbeddedPath;
            control.Refresh( );
        }


        /// <summary>
        /// Source for report data
        /// </summary>
        public object DataSource
        {
            get { return GetValue( DataSourceProperty ); }
            set { SetValue( DataSourceProperty, value ); }
        }

        /// <summary>
        /// RDLC report added to project as Embedded Resource
        /// </summary>
        public string EmbeddedReport
        {
            get { return (string) GetValue( EmbeddedReportProperty ); }
            set { SetValue( EmbeddedReportProperty, value ); }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Extension list that is used for report rendring
        /// </summary>
        /// <remarks>
        /// viewer.LocalReport.m_previewService.ListRenderingExtensions( )
        /// </remarks>
        private IList RenderingExtensions
        {
            get
            {
                var service = reportViewer.LocalReport
                    .GetType( )
                    .GetField( "m_previewService", BindingFlags.NonPublic | BindingFlags.Instance )
                    .GetValue( reportViewer.LocalReport );

                var extensions = service
                    .GetType( )
                    .GetMethod( "ListRenderingExtensions" )
                    .Invoke( service, null );

                return (IList) extensions;
            }
        }

        /// <summary>
        /// If the control can be refreshed
        /// </summary>
        private bool CanBeRefreshed
        {
            get
            {
                return reportViewer.LocalReport.DataSources.Count > 0
                    && !string.IsNullOrEmpty( reportViewer.LocalReport.ReportEmbeddedResource );
            }
        }

        /// <summary>
        /// ToolStrip of a ReportViewer
        /// </summary>
        /// <remarks>
        /// Here's list of control names that can be found on ReportViewer's toolbar:
        /// (Was taken from Microsoft.Reporting.WinForms.ReportToolBar.InitializeComponent)
        /// - back
        /// - currentPage
        /// - docMap
        /// - export
        /// - find
        /// - findNext
        /// - firstPage
        /// - labelOf
        /// - lastPage
        /// - nextPage
        /// - pageSetup
        /// - previousPage
        /// - print
        /// - printPreview
        /// - refresh
        /// - showParams
        /// - stop
        /// - textToFind
        /// - totalPages
        /// - zoom
        /// </remarks>
        private System.Windows.Forms.ToolStrip ToolStrip
        {
            get { return FindControl<System.Windows.Forms.ToolStrip>( reportViewer ); }
        }

        #endregion
    }
}
