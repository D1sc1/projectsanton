using CardFile.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataStore.Dtos
{
    public class CardDto
    {
        // DTO = Data transfer object

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
        /// Дата поступления в библиотеку
        /// </summary>
        public DateTime ReceiptDate { get; set; }

        /// <summary>
        /// Дата выдачи читателю (null - книга в наличии)
        /// </summary>
        public DateTime? IssueDate { get; set; }

        /// <summary>
        /// Читатель, которому выдана книга
        /// </summary>
        public string ReaderName { get; set; }

        /// <summary>
        /// Стоимость
        /// </summary>
        public decimal Price { get; set; }

        internal CardDto Clone()
        {
            return Mapping.Mapper.Map<CardDto>(this);
        }
    }
}
