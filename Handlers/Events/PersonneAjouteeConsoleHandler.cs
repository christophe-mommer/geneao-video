using CQELight.Abstractions.DDD;
using CQELight.Abstractions.Events.Interfaces;
using CQELight.Abstractions.IoC.Interfaces;
using Geneao.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Geneao.Handlers
{
    class PersonneAjouteeConsoleHandler : IDomainEventHandler<PersonneAjoutee>, IAutoRegisterType
    {
        public Task<Result> HandleAsync(PersonneAjoutee domainEvent, IEventContext context = null)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"{domainEvent.Prenom} correctement ajouté(e)" +
                $" à la famille {domainEvent.NomFamille.Value}");
            Console.ResetColor();
            return Result.Ok();
        }
    }
}
