using CQELight.Abstractions.CQS.Interfaces;
using Geneao.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geneao.Commands
{
    public class CreerFamille : ICommand
    {
        public NomFamille NomFamille { get; private set; }

        private CreerFamille()
        {

        }

        public CreerFamille(NomFamille nomFamille)
        {
            if (nomFamille == null)
                throw new ArgumentNullException(nameof(nomFamille));

            NomFamille = nomFamille;
        }
    }
}
