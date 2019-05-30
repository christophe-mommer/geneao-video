using CQELight.DAL.EFCore;
using Geneao.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geneao.Data.Repositories
{
    internal class FamilleRepository : EFRepository<Famille>, IFamilleRepository
    {
        public FamilleRepository(GeneaoDbContext context)
            : base(context)
        {
        }
    }
}
