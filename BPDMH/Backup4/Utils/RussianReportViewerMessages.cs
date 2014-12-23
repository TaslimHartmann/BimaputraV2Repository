using Microsoft.Reporting.WinForms;

namespace AdvancedReportViewer.Utils
{
    /// <summary>
    /// Russian localization of all ReportViewer messages
    /// </summary>
    public class RussianReportViewerMessages: IReportViewerMessages
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public RussianReportViewerMessages( )
        {
            BackButtonToolTip = @"Назад";
            BackMenuItemText = @"Назад";
            ChangeCredentialsText = @"Изменить учетные данные";
            CurrentPageTextBoxToolTip = @"Текущая страница";
            DocumentMapButtonToolTip = @"Схема отчета";
            DocumentMapMenuItemText = @"Схема отчета";
            ExportButtonToolTip = @"Сохранить как...";
            ExportMenuItemText = @"Сохранить как...";
            FalseValueText = @"Ложь";
            FindButtonText = "Найти";
            FindButtonToolTip = "Найти";
            FindNextButtonText = "Найти далее";
            FindNextButtonToolTip = "Найти далее";
            FirstPageButtonToolTip = @"Первая страница";
            LastPageButtonToolTip = @"Последняя страница";
            NextPageButtonToolTip = @"Следующая страница";
            NoMoreMatches = @"Поиск завершен";
            NullCheckBoxText = @"";
            NullCheckBoxToolTip = @"";
            NullValueText = @"";
            PageOf = @"из";
            PageSetupButtonToolTip = @"Параметры страницы...";
            PageSetupMenuItemText = @"Параметры страницы...";
            ParameterAreaButtonToolTip = @"Параметры отчета";
            PasswordPrompt = @"Пароль";
            PreviousPageButtonToolTip = @"Предыдущая страница";
            PrintButtonToolTip = @"Печать...";
            PrintLayoutButtonToolTip = @"Предпросмотр печати";
            PrintLayoutMenuItemText = @"Предпросмотр печати";
            PrintMenuItemText = @"Печать...";
            ProgressText = @"Подождите, пожалуйста. Отчет создается.";
            RefreshButtonToolTip = @"Обновить";
            RefreshMenuItemText = @"Обновить";
            SearchTextBoxToolTip = @"Введите текст для поиска";
            SelectAll = @"Выделить все";
            SelectAValue = @"Выберите";
            StopButtonToolTip = @"Прекратить загрузку";
            StopMenuItemText = @"Прекратить загрузку";
            TextNotFound = @"Текст не найден";
            TotalPagesToolTip = @"Всего страниц";
            TrueValueText = @"Правда";
            UserNamePrompt = @"Имя пользователя";
            ViewReportButtonText = @"Показать отчет";
            ViewReportButtonToolTip = @"Показать отчет";
            ZoomControlToolTip = @"Масштаб";
            ZoomMenuItemText = @"Масштаб";
            ZoomToPageWidth = @"По ширине";
            ZoomToWholePage = @"По высоте";
        }

        #endregion

        #region IReportViewerMessages Members

        public string BackButtonToolTip { get; set; }
        public string BackMenuItemText { get; set; }
        public string ChangeCredentialsText { get; set; }
        public string CurrentPageTextBoxToolTip { get; set; }
        public string DocumentMapButtonToolTip { get; set; }
        public string DocumentMapMenuItemText { get; set; }
        public string ExportButtonToolTip { get; set; }
        public string ExportMenuItemText { get; set; }
        public string FalseValueText { get; set; }
        public string FindButtonText { get; set; }
        public string FindButtonToolTip { get; set; }
        public string FindNextButtonText { get; set; }
        public string FindNextButtonToolTip { get; set; }
        public string FirstPageButtonToolTip { get; set; }
        public string LastPageButtonToolTip { get; set; }
        public string NextPageButtonToolTip { get; set; }
        public string NoMoreMatches { get; set; }
        public string NullCheckBoxText { get; set; }
        public string NullCheckBoxToolTip { get; set; }
        public string NullValueText { get; set; }
        public string PageOf { get; set; }
        public string PageSetupButtonToolTip { get; set; }
        public string PageSetupMenuItemText { get; set; }
        public string ParameterAreaButtonToolTip { get; set; }
        public string PasswordPrompt { get; set; }
        public string PreviousPageButtonToolTip { get; set; }
        public string PrintButtonToolTip { get; set; }
        public string PrintLayoutButtonToolTip { get; set; }
        public string PrintLayoutMenuItemText { get; set; }
        public string PrintMenuItemText { get; set; }
        public string ProgressText { get; set; }
        public string RefreshButtonToolTip { get; set; }
        public string RefreshMenuItemText { get; set; }
        public string SearchTextBoxToolTip { get; set; }
        public string SelectAll { get; set; }
        public string SelectAValue { get; set; }
        public string StopButtonToolTip { get; set; }
        public string StopMenuItemText { get; set; }
        public string TextNotFound { get; set; }
        public string TotalPagesToolTip { get; set; }
        public string TrueValueText { get; set; }
        public string UserNamePrompt { get; set; }
        public string ViewReportButtonText { get; set; }
        public string ViewReportButtonToolTip { get; set; }
        public string ZoomControlToolTip { get; set; }
        public string ZoomMenuItemText { get; set; }
        public string ZoomToPageWidth { get; set; }
        public string ZoomToWholePage { get; set; }

        #endregion
    }
}
