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
    public class BinaryFileManager : IFileManager
    {
        public void OpenFromFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(fs))
                {
                    var records = new List<CardDto>();

                    try
                    {
                        while (reader.BaseStream.Position < reader.BaseStream.Length)
                        {
                            var newCard = new CardDto();

                            newCard.Id = reader.ReadInt32();
                            newCard.Title = reader.ReadString();
                            newCard.Author = reader.ReadString();
                            newCard.Genre = reader.ReadString();
                            newCard.PublishYear = reader.ReadInt32();
                            newCard.Publisher = reader.ReadString();
                            newCard.PageCount = reader.ReadInt32();

                            var ticks = reader.ReadInt64();
                            newCard.ReceiptDate = new DateTime(ticks);

                            var isIssueDatePresent = reader.ReadBoolean();
                            if (isIssueDatePresent)
                            {
                                ticks = reader.ReadInt64();
                                newCard.IssueDate = new DateTime(ticks);
                            }
                            else
                            {
                                newCard.IssueDate = null;
                            }

                            newCard.ReaderName = reader.ReadString();
                            newCard.Price = reader.ReadDecimal();

                            records.Add(newCard);
                        }
                    }
                    catch
                    {
                        throw new Exception($"Неверные данные в файле {fileName}");
                    }

                    collection.ReplaceAll(records);
                }
            }
        }

        public void SaveToFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new BinaryWriter(fs))
                {
                    foreach (var item in collection.GetAll())
                    {
                        // BinaryWriter.Write(string) не принимает null, поэтому пустые поля пишем как ""
                        writer.Write(item.Id);
                        writer.Write(item.Title ?? string.Empty);
                        writer.Write(item.Author ?? string.Empty);
                        writer.Write(item.Genre ?? string.Empty);
                        writer.Write(item.PublishYear);
                        writer.Write(item.Publisher ?? string.Empty);
                        writer.Write(item.PageCount);
                        writer.Write(item.ReceiptDate.Ticks);

                        writer.Write(item.IssueDate.HasValue);
                        if (item.IssueDate.HasValue)
                        {
                            writer.Write(item.IssueDate.Value.Ticks);
                        }

                        writer.Write(item.ReaderName ?? string.Empty);
                        writer.Write(item.Price);
                    }
                }
            }
        }
    }
}
