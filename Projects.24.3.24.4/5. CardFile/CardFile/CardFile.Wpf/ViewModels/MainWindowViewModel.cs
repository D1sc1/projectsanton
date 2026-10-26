using CardFile.Business.Models;
using CardFile.Business.Services;
using CardFile.Common.Infrastructure;
using CardFile.Wpf.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CardFile.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private CardFileService _service = new CardFileService();

        private string _searchText = string.Empty;

        private bool _isOnlyIssuedShown;

        public ObservableCollection<CardViewModel> Cards { get; } = new ObservableCollection<CardViewModel>();

        public CardViewModel SelectedCard { get; set; }

        public bool IsEditButtonEnabled => SelectedCard != null;

        public bool IsDeleteButtonEnabled => SelectedCard != null;

        public string FileName { get; private set; }

        public string WindowTitle => string.IsNullOrEmpty(FileName) ? "Картотека библиотеки" : $"Картотека библиотеки: {Path.GetFileName(FileName)}";

        /// <summary>
        /// Строка поиска по названию, автору, жанру и читателю
        /// </summary>
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                RefreshFilter();
            }
        }

        /// <summary>
        /// Показывать только выданные книги
        /// </summary>
        public bool IsOnlyIssuedShown
        {
            get => _isOnlyIssuedShown;
            set
            {
                _isOnlyIssuedShown = value;
                RefreshFilter();
            }
        }

        public string SummaryText
        {
            get
            {
                var issued = Cards.Count(c => c.IssueDate.HasValue);
                var overdue = Cards.Count(c => c.IsOverdue);
                var shown = CollectionViewSource.GetDefaultView(Cards).Cast<object>().Count();
                return $"Книг: {Cards.Count}, в наличии: {Cards.Count - issued}, выдано: {issued}, просрочено: {overdue}. Показано: {shown}";
            }
        }

        public MainWindowViewModel()
        {
            MapperInitialization.PreRegister();
        }

        public void WindowLoaded()
        {
            Mapping.Initialize();

            CollectionViewSource.GetDefaultView(Cards).Filter = FilterCard;
            LoadAllData();
        }

        public CardViewModel GetSelectedCard()
        {
            return SelectedCard;
        }

        public CardViewModel GetNewCard()
        {
            return new CardViewModel
            {
                PublishYear = DateTime.Today.Year,
                PageCount = 100,
                ReceiptDate = DateTime.Today,
                IssueDate = null,
                ReaderName = string.Empty,
            };
        }

        public void SaveEditedCard(CardViewModel card)
        {
            var index = Cards.ToList().FindIndex(c => c.Id == card.Id);

            if (index < 0)
            {
                throw new Exception("Карточка с таким Id не существует");
            }

            var id = _service.Save(ToBusiness(card));

            if (id < 0)
            {
                Cards.RemoveAt(index);
            }
            else
            {
                Cards[index].LoadViewModel(card);
            }

            RefreshFilter();
        }

        public void SaveNewCard(CardViewModel card)
        {
            var newCard = new CardViewModel();
            newCard.LoadViewModel(card);

            var id = _service.Save(ToBusiness(card));

            if (id < 0)
            {
                return;
            }

            newCard.Id = id;
            Cards.Add(newCard);
            RefreshFilter();
        }

        public void DeleteSelectedCard()
        {
            if (SelectedCard == null)
            {
                return;
            }

            _service.Delete(SelectedCard.Id);
            var index = Cards.ToList().FindIndex(c => c.Id == SelectedCard.Id);

            if (index < 0)
            {
                throw new Exception("Карточка с таким Id не существует");
            }

            Cards.RemoveAt(index);
            SelectedCard = null;

            OnPropertyChanged(nameof(SelectedCard));
            OnPropertyChanged(nameof(SummaryText));
        }

        /// <summary>
        /// Возврат выбранной книги в библиотеку
        /// </summary>
        public void ReturnSelectedCard()
        {
            if (SelectedCard == null || !SelectedCard.IssueDate.HasValue)
            {
                return;
            }

            var card = new CardViewModel();
            card.LoadViewModel(SelectedCard);
            card.IssueDate = null;
            card.ReaderName = string.Empty;

            SaveEditedCard(card);
            SelectionChanged();
        }

        public bool IsReturnButtonEnabled => SelectedCard != null && SelectedCard.IssueDate.HasValue;

        public void SelectionChanged()
        {
            OnPropertyChanged(nameof(IsDeleteButtonEnabled));
            OnPropertyChanged(nameof(IsEditButtonEnabled));
            OnPropertyChanged(nameof(IsReturnButtonEnabled));
        }

        public void SaveToFile(string fileName)
        {
            _service.SaveToFile(fileName);

            FileName = fileName;
            OnPropertyChanged(nameof(WindowTitle));
        }

        public void SaveToFile()
        {
            try
            {
                _service.SaveToFile(FileName);
            }
            catch (Exception)
            {
                FileName = null;
                OnPropertyChanged(nameof(WindowTitle));
                throw;
            }
        }

        public void OpenFromFile(string fileName)
        {
            _service.OpenFromFile(fileName);
            LoadAllData();

            FileName = fileName;
            OnPropertyChanged(nameof(WindowTitle));
        }

        private void LoadAllData()
        {
            var cards = _service.GetAll();
            Cards.Clear();
            foreach (var card in cards)
            {
                Cards.Add(ToViewModel(card));
            }

            RefreshFilter();
        }

        private bool FilterCard(object item)
        {
            var card = (CardViewModel)item;

            if (IsOnlyIssuedShown && !card.IssueDate.HasValue)
            {
                return false;
            }

            return card.Matches(SearchText);
        }

        private void RefreshFilter()
        {
            CollectionViewSource.GetDefaultView(Cards).Refresh();
            OnPropertyChanged(nameof(SummaryText));
        }

        private CardViewModel ToViewModel(Card card)
        {
            return Mapping.Mapper.Map<CardViewModel>(card);
        }

        private Card ToBusiness(CardViewModel card)
        {
            return Mapping.Mapper.Map<Card>(card);
        }
    }
}
