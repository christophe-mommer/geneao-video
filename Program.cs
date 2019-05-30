using Autofac;
using CQELight;
using CQELight.Abstractions.DDD;
using CQELight.Dispatcher;
using CQELight.EventStore;
using CQELight.EventStore.EFCore.Common;
using CQELight.IoC;
using Geneao.Commands;
using Geneao.Data;
using Geneao.Data.Repositories;
using Geneao.Handlers;
using Geneao.Models;
using Geneao.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Geneao
{
    public class EventStoreDbContextCreator : IDesignTimeDbContextFactory<EventStoreDbContext>
    {
        public EventStoreDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<EventStoreDbContext>();
            Program.ApplyOptions(builder);
            return new EventStoreDbContext(builder.Options);
        }
    }
    static class Program
    {
        public static void ApplyOptions(DbContextOptionsBuilder builder)
        {
            builder.UseSqlite("Filename=test.db",
                opt => opt.MigrationsAssembly(typeof(Program).Assembly.GetName().Name));
        }

        static async Task Main(string[] args)
        {

            Console.WriteLine("Bienvenue dans la gestion de votre arbre généalogique");

            var b = new Bootstrapper()
                .UseInMemoryEventBus()
                .UseInMemoryCommandBus()
                .UseEFCoreAsMainRepository(ApplyOptions)
                .UseAutofacAsIoC(c =>
                {
                    c.RegisterType<FamilleRepository>().AsImplementedInterfaces();
                    c.Register(_ => new LoggerFactory().AddDebug(LogLevel.Debug)).AsSelf();
                    c.Register(_ => new LoggerFactory().AddDebug(LogLevel.Debug).CreateLogger("General")).As<ILogger>();
                })
                .UseEFCoreAsEventStore(new CQELight.EventStore.EFCore.EFEventStoreOptions(ApplyOptions));
            b.Bootstrapp();

            var optionsBuilder = new DbContextOptionsBuilder();
            ApplyOptions(optionsBuilder);
            using (var ctx = new GeneaoDbContext(optionsBuilder.Options))
            {
                await ctx.Database.MigrateAsync();
            }
            var eventStoreOptionsBuilder = new DbContextOptionsBuilder<EventStoreDbContext>();
            ApplyOptions(eventStoreOptionsBuilder);
            using (var ctx = new EventStoreDbContext(eventStoreOptionsBuilder.Options))
            {
                await ctx.Database.MigrateAsync();
            }

            await DisplayMainMenuAsync();
        }

        private static async Task DisplayMainMenuAsync()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Choisissez votre commande");
                    Console.WriteLine("1. Lister les familles du logiciel");
                    Console.WriteLine("2. Créer une nouvelle famille");
                    Console.WriteLine("3. Ajouter une personne à une famille");
                    Console.WriteLine("Ou tapez q pour quitter");
                    Console.WriteLine();
                    var result = Console.ReadKey();
                    Console.WriteLine();
                    switch (result.Key)
                    {
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1:
                            using (var scope = DIManager.BeginScope())
                            {
                                var query = scope.Resolve<RecupererListeFamilleQuery>();
                                var familles = await query.ExecuteQueryAsync();
                                foreach (var item in familles)
                                {
                                    Console.WriteLine($"Famille {item.Nom}");
                                    foreach (var personne in item.Membres)
                                    {
                                        Console.WriteLine($"\t {personne}");
                                    }
                                }
                            }
                            break;
                        case ConsoleKey.D2:
                        case ConsoleKey.NumPad2:
                            Console.WriteLine("Entrez le nom de la famille à créer et appuyez sur entrée");
                            var famille = Console.ReadLine();
                            var creationResult = await CoreDispatcher
                                .DispatchCommandAsync(new CreerFamille(new Identity.NomFamille(famille)));
                            if (!creationResult.IsSuccess
                                && creationResult is Result<FamilleNonCreeeCar> resultEnum)
                            {
                                if (resultEnum.Value == FamilleNonCreeeCar.ExisteDeja)
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine("La famille n'a pas pu être créée car elle existe déjà");
                                    Console.ResetColor();
                                }
                            }
                            break;
                        case ConsoleKey.D3:
                        case ConsoleKey.NumPad3:
                            Console.WriteLine("Entrez le nom de la famille dans laquelle insérer cette personne");
                            var nomFamillePourAjout = Console.ReadLine();
                            using (var scope = DIManager.BeginScope())
                            {
                                var query = scope.Resolve<RecupererListeFamilleQuery>();
                                var famillePourAjout = (await query.ExecuteQueryAsync())
                                    .FirstOrDefault(f => f.Nom.Equals(nomFamillePourAjout, StringComparison.OrdinalIgnoreCase));

                                if (famillePourAjout != null)
                                {
                                    Console.WriteLine("Saisir le prénom");
                                    var prenom = Console.ReadLine();
                                    Console.WriteLine("Saisir le lieu de naissance");
                                    var lieuNaissance = Console.ReadLine();
                                    Console.WriteLine("Saisir la date de naissance (dd/MM/yyyy)");
                                    var dateNaissance = DateTime.Parse(Console.ReadLine());

                                    await CoreDispatcher.DispatchCommandAsync(new AjouterPersonne(
                                        new Identity.NomFamille(nomFamillePourAjout),
                                        prenom,
                                        lieuNaissance,
                                        dateNaissance));
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine($"La famille {nomFamillePourAjout} n'existe pas dans le système");
                                    Console.ResetColor();
                                }
                            }
                            break;
                        case ConsoleKey.Q:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Choix incorrect, merci de faire un choix dans la liste");
                            break;
                    }
                }
                catch (Exception e)
                {
                    var color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkRed;

                    Console.WriteLine(e.ToString());

                    Console.ForegroundColor = color;
                }
            }
        }

    }
}
