using CQELight.Abstractions.DDD;
using Geneao.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geneao.Models
{
    public enum DeclarationNaissanceImpossibleCar
    {
        AbsenceDePrenom,
        AbsenceInformationNaissance,
    }
    public class Personne : Entity<PersonneId>
    {
        public string Prenom { get; internal set; }
        public InformationsDeNaissance InformationsDeNaissance { get; internal set; }

        internal Personne()
        {
        }

    }
}
