using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class XmlCard
    {
        private const string DateFormat = "dd.MM.yyyy";

        /// <summary>
        /// Id
        /// </summary>
        [XmlAttribute("Id")]
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        [XmlAttribute("Title")]
        public string Title { get; set; }

        /// <summary>
        /// Автор
        /// </summary>
        [XmlAttribute("Author")]
        public string Author { get; set; }

        /// <summary>
        /// Жанр
        /// </summary>
        [XmlAttribute("Genre")]
        public string Genre { get; set; }

        /// <summary>
        /// Год издания
        /// </summary>
        [XmlAttribute("PublishYear")]
        public int PublishYear { get; set; }

        /// <summary>
        /// Издательство
        /// </summary>
        [XmlAttribute("Publisher")]
        public string Publisher { get; set; }

        /// <summary>
        /// Количество страниц
        /// </summary>
        [XmlAttribute("PageCount")]
        public int PageCount { get; set; }

        /// <summary>
        /// Дата поступления
        /// </summary>
        [XmlIgnore]
        public DateTime ReceiptDate { get; set; }

        [XmlAttribute("ReceiptDate")]
        public string ReceiptDateText
        {
            get => ReceiptDate.ToString(DateFormat, CultureInfo.InvariantCulture);
            set => ReceiptDate = DateTime.ParseExact(value, DateFormat, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Дата выдачи
        /// </summary>
        [XmlIgnore]
        public DateTime? IssueDate { get; set; }

        [XmlAttribute("IssueDate")]
        public string IssueDateText
        {
            get => IssueDate?.ToString(DateFormat, CultureInfo.InvariantCulture) ?? "-";
            set => IssueDate = (value == "-") ? (DateTime?)null : DateTime.ParseExact(value, DateFormat, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Читатель
        /// </summary>
        [XmlAttribute("ReaderName")]
        public string ReaderName { get; set; }

        /// <summary>
        /// Стоимость
        /// </summary>
        [XmlAttribute("Price")]
        public decimal Price { get; set; }
    }
}
