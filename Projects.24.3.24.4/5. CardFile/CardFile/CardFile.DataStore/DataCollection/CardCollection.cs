using CardFile.DataStore.Dtos;
using CardFile.DataStore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataStore.DataCollection
{
    public class CardCollection
    {
        private readonly List<CardDto> _cards = new List<CardDto>();

        internal int NextId { get; set; } = 7;

        public CardCollection()
        {
            MapperInitialization.PreRegister();

            _cards.Add(new CardDto
            {
                Id = 1,
                Title = "Мастер и Маргарита",
                Author = "Михаил Булгаков",
                Genre = "Роман",
                PublishYear = 2019,
                Publisher = "АСТ",
                PageCount = 480,
                ReceiptDate = new DateTime(2020, 2, 10),
                IssueDate = null,
                ReaderName = string.Empty,
                Price = 650m,
            });

            _cards.Add(new CardDto
            {
                Id = 2,
                Title = "Преступление и наказание",
                Author = "Федор Достоевский",
                Genre = "Роман",
                PublishYear = 2017,
                Publisher = "Эксмо",
                PageCount = 608,
                ReceiptDate = new DateTime(2018, 9, 1),
                IssueDate = new DateTime(2026, 10, 12),
                ReaderName = "Иванов П.С.",
                Price = 540m,
            });

            _cards.Add(new CardDto
            {
                Id = 3,
                Title = "Пикник на обочине",
                Author = "Аркадий и Борис Стругацкие",
                Genre = "Фантастика",
                PublishYear = 2021,
                Publisher = "АСТ",
                PageCount = 256,
                ReceiptDate = new DateTime(2021, 11, 15),
                IssueDate = new DateTime(2026, 8, 20),
                ReaderName = "Смирнова А.В.",
                Price = 420m,
            });

            _cards.Add(new CardDto
            {
                Id = 4,
                Title = "Евгений Онегин",
                Author = "Александр Пушкин",
                Genre = "Поэзия",
                PublishYear = 2015,
                Publisher = "Детская литература",
                PageCount = 224,
                ReceiptDate = new DateTime(2016, 3, 3),
                IssueDate = null,
                ReaderName = string.Empty,
                Price = 310m,
            });

            _cards.Add(new CardDto
            {
                Id = 5,
                Title = "C# 10 и .NET 6. Современная кросс-платформенная разработка",
                Author = "Марк Прайс",
                Genre = "Программирование",
                PublishYear = 2023,
                Publisher = "Питер",
                PageCount = 848,
                ReceiptDate = new DateTime(2024, 1, 22),
                IssueDate = new DateTime(2026, 10, 1),
                ReaderName = "Кузнецов Д.А.",
                Price = 2900m,
            });

            _cards.Add(new CardDto
            {
                Id = 6,
                Title = "Война и мир. Том 1",
                Author = "Лев Толстой",
                Genre = "Роман",
                PublishYear = 2012,
                Publisher = "Азбука",
                PageCount = 704,
                ReceiptDate = new DateTime(2013, 5, 30),
                IssueDate = null,
                ReaderName = string.Empty,
                Price = 480m,
            });
        }

        public IEnumerable<CardDto> GetAll()
        {
            return _cards.Select(c => c.Clone()).ToList();
        }

        public int Save(CardDto card)
        {
            if (card.Id == 0)
            {
                var newCard = card.Clone();
                var id = NextId++;
                newCard.Id = id;
                _cards.Add(newCard);
                return id;
            }

            var index = _cards.FindIndex(c => c.Id == card.Id);
            if (index < 0)
            {
                return -1;
            }

            _cards[index] = card.Clone();
            return card.Id;
        }

        public bool Delete(int cardId)
        {
            var index = _cards.FindIndex(c => c.Id == cardId);
            if (index < 0)
            {
                return false;
            }

            _cards.RemoveAt(index);
            return true;
        }

        public void ReplaceAll(IEnumerable<CardDto> cards)
        {
            _cards.Clear();
            _cards.AddRange(cards);
            NextId = _cards.Count == 0 ? 1 : _cards.Max(c => c.Id) + 1;
        }

        public void ReplaceAll(IEnumerable<CardDto> cards, int nextId)
        {
            _cards.Clear();
            _cards.AddRange(cards);
            NextId = nextId;
        }
    }
}
