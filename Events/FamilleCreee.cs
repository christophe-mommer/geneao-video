using CQELight.Abstractions.Events;
using Geneao.Identity;
using Geneao.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geneao.Events
{
    public sealed class FamilleCreee : BaseDomainEvent
    {
        public NomFamille NomFamille { get; private set; }

        private FamilleCreee()
        {

        }
        internal FamilleCreee(NomFamille nomFamille)
        {
            NomFamille = nomFamille;
            this.AggregateType = typeof(Famille);
            this.AggregateId = nomFamille;
        }
    }
}
