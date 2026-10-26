using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class JsonCard
    {
        private const string DateFormat = "dd.MM.yyyy";

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
        [JsonIgnore]
        public DateTime ReceiptDate { get; set; }

        [JsonProperty("ReceiptDate")]
        public string ReceiptDateText
        {
            get => ReceiptDate.ToString(DateFormat, CultureInfo.InvariantCulture);
            set => ReceiptDate = DateTime.ParseExact(value, DateFormat, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Дата выдачи
        /// </summary>
        [JsonIgnore]
        public DateTime? IssueDate { get; set; }

        [JsonProperty("IssueDate")]
        public string IssueDateText
        {
            get => IssueDate?.ToString(DateFormat, CultureInfo.InvariantCulture) ?? "-";
            set => IssueDate = (value == "-") ? (DateTime?)null : DateTime.ParseExact(value, DateFormat, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Читатель
        /// </summary>
        public string ReaderName { get; set; }

        /// <summary>
        /// Стоимость
        /// </summary>
        public decimal Price { get; set; }
    }
}
