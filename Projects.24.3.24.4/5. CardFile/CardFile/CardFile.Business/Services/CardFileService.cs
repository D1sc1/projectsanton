using CardFile.Business.Infrastructure;
using CardFile.Business.Models;
using CardFile.Common.Infrastructure;
using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using CardFile.DataStore.FileDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Business.Services
{
    public class CardFileService
    {
        /// <summary>
        /// Срок, на который выдается книга, дней
        /// </summary>
        public const int LoanPeriodDays = 30;

        private CardCollection _collection = new CardCollection();

        public CardFileService()
        {
            MapperInitialization.PreRegister();
        }

        public IEnumerable<Card> GetAll()
        {
            var cardDtos = _collection.GetAll();
            return cardDtos.Select(FromDto).ToList();
        }

        public int Save(Card card)
        {
            return _collection.Save(ToDto(card));
        }

        public bool Delete(int cardId)
        {
            return _collection.Delete(cardId);
        }

        public void SaveToFile(string fileName)
        {
            var fileService = new FileDataService();
            fileService.SaveToFile(fileName, _collection);
        }

        public void OpenFromFile(string fileName)
        {
            var fileService = new FileDataService();
            fileService.OpenFromFile(fileName, _collection);
        }

        /// <summary>
        /// Книга выдана и не возвращена дольше срока выдачи
        /// </summary>
        public static bool IsOverdue(DateTime? issueDate, DateTime today)
        {
            return issueDate.HasValue && (today - issueDate.Value.Date).TotalDays > LoanPeriodDays;
        }

        /// <summary>
        /// Проверка карточки перед сохранением. Возвращает текст ошибки или null
        /// </summary>
        public static string Validate(Card card)
        {
            if (string.IsNullOrWhiteSpace(card.Title))
            {
                return "Не указано название книги";
            }

            if (string.IsNullOrWhiteSpace(card.Author))
            {
                return "Не указан автор";
            }

            if (card.PublishYear < 1450 || card.PublishYear > DateTime.Today.Year)
            {
                return $"Год издания должен быть в диапазоне 1450 - {DateTime.Today.Year}";
            }

            if (card.PageCount <= 0)
            {
                return "Количество страниц должно быть больше нуля";
            }

            if (card.Price < 0)
            {
                return "Стоимость не может быть отрицательной";
            }

            if (card.IssueDate.HasValue)
            {
                if (string.IsNullOrWhiteSpace(card.ReaderName))
                {
                    return "Для выданной книги нужно указать читателя";
                }

                if (card.IssueDate.Value.Date < card.ReceiptDate.Date)
                {
                    return "Дата выдачи не может быть раньше даты поступления";
                }
            }

            return null;
        }

        private Card FromDto(CardDto card)
        {
            return Mapping.Mapper.Map<Card>(card);
        }

        private CardDto ToDto(Card card)
        {
            return Mapping.Mapper.Map<CardDto>(card);
        }
    }
}
