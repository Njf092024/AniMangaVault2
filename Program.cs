﻿using System;
using static System.Console;
using System.Collections.Generic;
using Spectre.Console;
using AniMangaVault2.Services;
using AniMangaVault2.Models;

namespace AniMangaVault2
{
    class Program
    {
        static void Main(string[] args)
        {
            AnimeMangaService service = new AnimeMangaService();
            int idCounter = 1;
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                var prompt = new SelectionPrompt<string>()
                    .Title("[bold yellow]Anime/Manga Menu[/]")
                    .PageSize(10)
                    .AddChoices("Add new Anime/Manga", "List all Anime/Manga", "Update Rating", "Delete Anime/Manga", "Exit");

                var selectedOption = AnsiConsole.Prompt(prompt);

                switch (selectedOption)
                {
                    case "Add new Anime/Manga":
                        AddNewAnimeMangaItems(service, ref idCounter);
                        break;
                    case "List all Anime/Manga":
                        ListAnimeMangaItems(service);
                        break;
                    case "Update Rating":
                        UpdateAnimeMangaRating(service);
                        break;
                    case "Delete Anime/Manga":
                        DeleteAnimeMangaItem(service);
                        break;
                    case "Exit":
                        exit = true;
                        break;
                }
            }
        }

        static void AddNewAnimeMangaItems(AnimeMangaService service, ref int idCounter)
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[bold yellow]Add a new Anime/Manga[/]");
            string title = AnsiConsole.Ask<string>("Enter the title: ");
            string type = AnsiConsole.Ask<string>("Enter type (Anime/Manga): ");
            string description = AnsiConsole.Ask<string>("Enter description: ");
            int rating = AnsiConsole.Ask<int>("Enter rating (1-6): ");
            string notes = AnsiConsole.Ask<string>("Enter notes: ");

            var newItem = new AnimeMangaItem
            {
                Id = idCounter++,
                Title = title,
                Type = type,
                Description = description,
                Rating = rating,
                Notes = notes
            };

            service.AddNewAnimeMangaItem(newItem);

            AnsiConsole.MarkupLine("[green]Anime/Manga added successfully![/]");
            AnsiConsole.MarkupLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }

        static void ListAnimeMangaItems(AnimeMangaService service)
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[bold cyan]List of Anime/Manga:[/]");

            service.ListAnimeMangaItems();
            AnsiConsole.MarkupLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }

        static void UpdateAnimeMangaRating(AnimeMangaService service)
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[bold yellow]Update Rating[/]");

            int id = AnsiConsole.Ask<int>("Enter the ID of the item to update: ");
            int newRating = AnsiConsole.Ask<int>("Enter the new rating (1-6): ");

            service.UpdateRating(id, newRating);
            AnsiConsole.MarkupLine("[green]Rating updated successfully![/]");
            AnsiConsole.MarkupLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }

        static void DeleteAnimeMangaItem(AnimeMangaService service)
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[bold red]Delete Anime/Manga[/]");
            int id = AnsiConsole.Ask<int>("Enter the ID of the item to delete: ");
            service.DeleteAnimeMangaItem(id);

            AnsiConsole.MarkupLine("[green]Item deleted successfully![/]");
            AnsiConsole.MarkupLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }
    }
}
