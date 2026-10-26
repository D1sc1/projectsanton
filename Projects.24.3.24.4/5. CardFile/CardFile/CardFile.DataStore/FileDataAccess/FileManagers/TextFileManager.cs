using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataStore.FileDataAccess.FileManagers
{
    public class TextFileManager : IFileManager
    {
        private const int FieldCount = 11;

        private const char Separator = ';';

        public void OpenFromFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(fs))
                {
                    var records = new List<CardDto>();
                    var lineNumber = 0;

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        lineNumber++;

                        if (string.IsNullOrEmpty(line))
                        {
                            continue;
                        }

                        var lineParts = line.Split(Separator);
                        if (lineParts.Length != FieldCount)
                        {
                            throw new Exception($"В строке {lineNumber} файла {fileName} неверное количество полей");
                        }

                        var newCard = new CardDto();

                        newCard.Id = ParseInt(lineParts[0], "Id", lineNumber, fileName);
                        newCard.Title = lineParts[1];
                        newCard.Author = lineParts[2];
                        newCard.Genre = lineParts[3];
                        newCard.PublishYear = ParseInt(lineParts[4], "PublishYear", lineNumber, fileName);
                        newCard.Publisher = lineParts[5];
                        newCard.PageCount = ParseInt(lineParts[6], "PageCount", lineNumber, fileName);

                        if (DateTime.TryParse(lineParts[7], out var receiptDate))
                        {
                            newCard.ReceiptDate = receiptDate;
                        }
                        else
                        {
                            throw new Exception($"В строке {lineNumber} файла {fileName} неверное значение ReceiptDate");
                        }

                        if (lineParts[8] == "-")
                        {
                            newCard.IssueDate = null;
                        }
                        else if (DateTime.TryParse(lineParts[8], out var issueDate))
                        {
                            newCard.IssueDate = issueDate;
                        }
                        else
                        {
                            throw new Exception($"В строке {lineNumber} файла {fileName} неверное значение IssueDate");
                        }

                        newCard.ReaderName = lineParts[9];

                        if (decimal.TryParse(lineParts[10], out var price))
                        {
                            newCard.Price = price;
                        }
                        else
                        {
                            throw new Exception($"В строке {lineNumber} файла {fileName} неверное значение Price");
                        }

                        records.Add(newCard);
                    }

                    collection.ReplaceAll(records);
                }
            }
        }

        public void SaveToFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new StreamWriter(fs))
                {
                    foreach (var item in collection.GetAll())
                    {
                        writer.WriteLine($"{item.Id};{Clean(item.Title)};{Clean(item.Author)};{Clean(item.Genre)};" +
                            $"{item.PublishYear};{Clean(item.Publisher)};{item.PageCount};" +
                            $"{item.ReceiptDate.ToShortDateString()};{item.IssueDate?.ToShortDateString() ?? "-"};" +
                            $"{Clean(item.ReaderName)};{item.Price}");
                    }
                }
            }
        }

        private static int ParseInt(string text, string fieldName, int lineNumber, string fileName)
        {
            if (int.TryParse(text, out var value))
            {
                return value;
            }

            throw new Exception($"В строке {lineNumber} файла {fileName} неверное значение {fieldName}");
        }

        /// <summary>
        /// Разделитель внутри текстового поля сломал бы формат файла, поэтому заменяем его
        /// </summary>
        private static string Clean(string text)
        {
            return (text ?? string.Empty).Replace(Separator, ',');
        }
    }
}
