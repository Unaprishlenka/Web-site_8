using System.Text.Json;
using LR_CiT8.Models;

namespace LR_CiT8.Services
{
    public class HistoryService
    {
        private readonly string _filePath = "history.json";
        private List<ConversionHistory> _history;

        public HistoryService()
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
                _history = JsonSerializer.Deserialize<List<ConversionHistory>>(json) ?? new List<ConversionHistory>();
            }
            else
            {
                _history = new List<ConversionHistory>();
            }
        }

        public void Add(ConversionHistory record)
        {
            _history.Add(record);
            SaveToFile();
        }

        public List<ConversionHistory> GetAll()
        {
            return _history;
        }

        private void SaveToFile()
        {
            string json = JsonSerializer.Serialize(_history, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
    }
}