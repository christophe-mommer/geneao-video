using CQELight.Abstractions.DDD;
using CQELight.Abstractions.Events.Interfaces;
using CQELight.Abstractions.IoC.Interfaces;
using Geneao.Data.Models;
using Geneao.Data.Repositories;
using Geneao.Events;
using System.Threading.Tasks;

namespace Geneao.Handlers.Events
{
    class FamilleCreeeDatabaseHandler : IDomainEventHandler<FamilleCreee>, IAutoRegisterType
    {
        private readonly IFamilleRepository _familleRepository;

        public FamilleCreeeDatabaseHandler(
            IFamilleRepository familleRepository)
        {
            _familleRepository = familleRepository;
        }
        public async Task<Result> HandleAsync(FamilleCreee domainEvent, IEventContext context = null)
        {
            var famille = new Famille
            {
                Nom = domainEvent.NomFamille.Value
            };
            _familleRepository.MarkForInsert(famille);
            if (await _familleRepository.SaveAsync() > 0)
            {
                return Result.Ok();
            }
            return Result.Fail();
        }
    }
}
