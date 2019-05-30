using CQELight.Abstractions.CQS.Interfaces;
using CQELight.Abstractions.DDD;
using CQELight.Abstractions.EventStore.Interfaces;
using CQELight.Abstractions.IoC.Interfaces;
using CQELight.Dispatcher;
using Geneao.Commands;
using Geneao.Events;
using Geneao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geneao.Handlers
{
    class CreerFamilleHandler : ICommandHandler<CreerFamille>, IAutoRegisterType
    {
        private readonly IEventStore _eventStore;

        public CreerFamilleHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }
        public async Task<Result> HandleAsync(CreerFamille command, ICommandContext context = null)
        {
            var tousLesNomsDeFamilles =
                (await _eventStore.GetAllEventsByEventType<FamilleCreee>().ToList())
                .Select(c => c.NomFamille.Value).ToList();
            Famille._nomFamilles = tousLesNomsDeFamilles;
            var result = Famille.CreerFamille(command.NomFamille);
            if(result.IsSuccess && result is Result<Famille> resultFamille)
            {
                await CoreDispatcher.PublishEventAsync(new FamilleCreee(resultFamille.Value.Id));
            }
            return result;
        }
    }
}
