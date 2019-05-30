using CQELight.Abstractions.CQS.Interfaces;
using CQELight.Abstractions.DDD;
using CQELight.Abstractions.EventStore.Interfaces;
using CQELight.Abstractions.IoC.Interfaces;
using Geneao.Commands;
using Geneao.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Geneao.Handlers.Commands
{
    class AjouterPersonneCommandHandler : ICommandHandler<AjouterPersonne>, IAutoRegisterType
    {
        private readonly IAggregateEventStore _aggregateEventStore;

        public AjouterPersonneCommandHandler(IAggregateEventStore aggregateEventStore)
        {
            _aggregateEventStore = aggregateEventStore;
        }
        public async Task<Result> HandleAsync(AjouterPersonne command, ICommandContext context = null)
        {
            Famille famille = await _aggregateEventStore.GetRehydratedAggregateAsync<Famille>(command.NomFamille);
            famille.AjouterPersonne(
                command.Prenom,
                new InformationsDeNaissance(command.LieuNaissance, command.DateNaissance));
            await famille.PublishDomainEventsAsync();
            return Result.Ok();
        }
    }
}
