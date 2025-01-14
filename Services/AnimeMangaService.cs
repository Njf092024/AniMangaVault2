using AniMangaVault2.Models;
using Spectre.Console;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace AniMangaVault2.Services
{
    public class AnimeMangaService
    {
        private List<AnimeMangaItem> items = new List<AnimeMangaItem>();
        private const string FileName = "animeMangaData.json";
        private int idCounter = 1;

        public AnimeMangaService()
        {
            LoadData();
        }

        public void AddNewAnimeMangaItem(AnimeMangaItem item)
        {
            item.Id = idCounter++;
            items.Add(item);
            SaveData();
        }

        public void ListAnimeMangaItems()
        {
            if (items.Count == 0)
            {
                AnsiConsole.MarkupLine("[yellow]No items available.[/]");
                return;
            }

            foreach (var item in items)
            {
                AnsiConsole.MarkupLine($"[cyan]ID:[/] {item.Id}, [bold]{item.Title}[/], [green]Rating:[/] {item.Rating}, [blue]Type:[/] {item.Type}");
            }
        }

        public void UpdateRating(int id, int newRating)
        {
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                item.Rating = newRating;
                SaveData();
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Item not found.[/]");
            }
        }

        public void DeleteAnimeMangaItem(int id)
        {
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                items.Remove(item);
                SaveData();
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Item not found.[/]");
            }
        }

        private void SaveData()
        {
            var data = new
            {
                items = items,
                idCounter = idCounter
            };

            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(FileName, json);
        }

        private void LoadData()
        {
            if (File.Exists(FileName))
            {
                var json = File.ReadAllText(FileName);
                var data = JsonConvert.DeserializeObject<dynamic>(json);

                if (data != null)
                {
                    items = data.items.ToObject<List<AnimeMangaItem>>() ?? new List<AnimeMangaItem>();
                    idCounter = data.idCounter ?? 1;
                }

            }
        }
         public List<AnimeMangaItem> GetAllItems()
        {
                    return items;
        }
    }
}