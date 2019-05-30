using CQELight.Abstractions.CQS.Interfaces;
using CQELight.Abstractions.IoC.Interfaces;
using Geneao.Data;
using Geneao.Data.Models;
using Geneao.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geneao.Queries
{
    class RecupererListeFamilleQuery : IQuery<IEnumerable<FamilleListItem>>, IAutoRegisterType
    {
        private readonly IFamilleRepository _repository;

        public RecupererListeFamilleQuery(
            IFamilleRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<FamilleListItem>> ExecuteQueryAsync()
        {
            
                return (await _repository.GetAsync(
                    includes: f => f.Personnes).ToList())
                    .Select(f => new FamilleListItem
                    {
                        Nom = f.Nom,
                        Membres = f.Personnes.Select(p => p.Prenom).ToList()
                    })
                    .ToList();
        }
    }
}
