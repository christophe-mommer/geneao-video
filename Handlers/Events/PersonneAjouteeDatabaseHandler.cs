using CQELight.Abstractions.DDD;
using CQELight.Abstractions.Events.Interfaces;
using CQELight.Abstractions.IoC.Interfaces;
using CQELight.DAL.Interfaces;
using Geneao.Data.Models;
using Geneao.Data.Repositories;
using Geneao.Events;
using System.Linq;
using System.Threading.Tasks;

namespace Geneao.Handlers.Events
{
    class PersonneAjouteeDatabaseHandler : IDomainEventHandler<PersonneAjoutee>, IAutoRegisterType
    {
        private readonly IDatabaseRepository<Personne> _personneRepository;

        public PersonneAjouteeDatabaseHandler(
            IDatabaseRepository<Personne> personneRepository)
        {
            _personneRepository = personneRepository;
        }
        public async Task<Result> HandleAsync(PersonneAjoutee domainEvent, IEventContext context = null)
        {
            var p = new Personne
            {
                DateNaissance = domainEvent.DateNaissance,
                LieuNaissance = domainEvent.LieuNaissance,
                Prenom = domainEvent.Prenom,
                NomFamille = domainEvent.NomFamille.Value
            };
            _personneRepository.MarkForInsert(p);
            if (await _personneRepository.SaveAsync() > 0)
            {
                return Result.Ok();
            }
            return Result.Fail();
        }
    }
}
