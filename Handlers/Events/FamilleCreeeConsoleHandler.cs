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
    class FamilleCreeeConsoleHandler : IDomainEventHandler<FamilleCreee>, IAutoRegisterType
    {
        public Task<Result> HandleAsync(FamilleCreee domainEvent, IEventContext context = null)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Famille {domainEvent.NomFamille.Value} correctement créée");
            Console.ResetColor();
            return Result.Ok();
        }
    }
}
