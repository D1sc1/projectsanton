using CardFile.Business.Models;
using CardFile.Business.Services;
using CardFile.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : ViewModelBase
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Автор
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Жанр
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Год издания
        /// </summary>
        public int PublishYear { get; set; }

        /// <summary>
        /// Издательство
        /// </summary>
        public string Publisher { get; set; }

        /// <summary>
        /// Количество страниц
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// Дата поступления
        /// </summary>
        public DateTime ReceiptDate { get; set; }

        public string ReceiptDateText => ReceiptDate.ToShortDateString();

        /// <summary>
        /// Дата выдачи
        /// </summary>
        public DateTime? IssueDate { get; set; }

        public string IssueDateText => IssueDate?.ToShortDateString() ?? "-";

        /// <summary>
        /// Читатель
        /// </summary>
        public string ReaderName { get; set; }

        /// <summary>
        /// Стоимость
        /// </summary>
        public decimal Price { get; set; }

        public string PriceText => Price.ToString("c");

        public bool IsOverdue => CardFileService.IsOverdue(IssueDate, DateTime.Today);

        public string StatusText
        {
            get
            {
                if (!IssueDate.HasValue)
                {
                    return "В наличии";
                }

                return IsOverdue ? "Просрочена" : "Выдана";
            }
        }

        /// <summary>
        /// Флажок "В наличии" в окне правки
        /// </summary>
        public bool IsAvailable { get; set; }

        public bool IsIssueFieldsEnabled => !IsAvailable;

        public void IsAvailableChecked()
        {
            IssueDate = null;
            ReaderName = string.Empty;

            OnPropertyChanged(nameof(IssueDate));
            OnPropertyChanged(nameof(ReaderName));
            OnPropertyChanged(nameof(IsIssueFieldsEnabled));
        }

        public void IsAvailableUnchecked()
        {
            IssueDate = DateTime.Today;

            OnPropertyChanged(nameof(IssueDate));
            OnPropertyChanged(nameof(IsIssueFieldsEnabled));
        }

        /// <summary>
        /// Строка для поиска: название, автор, жанр и читатель
        /// </summary>
        public bool Matches(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return true;
            }

            var text = searchText.Trim();
            return new[] { Title, Author, Genre, ReaderName }
                .Any(f => f != null && f.IndexOf(text, StringComparison.CurrentCultureIgnoreCase) >= 0);
        }

        /// <summary>
        /// Проверка введенных данных. Возвращает текст ошибки или null
        /// </summary>
        public string Validate()
        {
            return CardFileService.Validate(Mapping.Mapper.Map<Card>(this));
        }

        public void LoadViewModel(CardViewModel card)
        {
            Mapping.Mapper.Map(card, this);

            IsAvailable = !card.IssueDate.HasValue;
            UpdateAll();
        }

        private void UpdateAll()
        {
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Author));
            OnPropertyChanged(nameof(Genre));
            OnPropertyChanged(nameof(PublishYear));
            OnPropertyChanged(nameof(Publisher));
            OnPropertyChanged(nameof(PageCount));
            OnPropertyChanged(nameof(ReceiptDate));
            OnPropertyChanged(nameof(ReceiptDateText));
            OnPropertyChanged(nameof(IssueDate));
            OnPropertyChanged(nameof(IssueDateText));
            OnPropertyChanged(nameof(ReaderName));
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(PriceText));
            OnPropertyChanged(nameof(IsOverdue));
            OnPropertyChanged(nameof(StatusText));
            OnPropertyChanged(nameof(IsAvailable));
            OnPropertyChanged(nameof(IsIssueFieldsEnabled));
        }
    }
}
