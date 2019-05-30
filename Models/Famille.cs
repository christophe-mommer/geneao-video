using CQELight.Abstractions.DDD;
using CQELight.Abstractions.Events.Interfaces;
using CQELight.Abstractions.EventStore.Interfaces;
using Geneao.Events;
using Geneao.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geneao.Models
{
    public enum FamilleNonCreeeCar
    {
        ExisteDeja
    }

    public class Famille : AggregateRoot<NomFamille>, IEventSourcedAggregate
    {
        private class FamilleState : AggregateState
        {
            public List<Personne> Personnes { get; private set; } = new List<Personne>();
            public NomFamille NomFamille { get; private set; }

            public FamilleState()
            {
                AddHandler<FamilleCreee>(OnFamilleCreee);
                AddHandler<PersonneAjoutee>(OnPersonneAjoutee);
            }

            private void OnFamilleCreee(FamilleCreee obj)
            {
                NomFamille = obj.NomFamille;
            }

            private void OnPersonneAjoutee(PersonneAjoutee obj)
            {
                Personnes.Add(new Personne
                {
                    Prenom = obj.Prenom,
                    InformationsDeNaissance =
                        new InformationsDeNaissance(
                            obj.LieuNaissance,
                            obj.DateNaissance)
                });
            }
        }
        private FamilleState _state = new FamilleState();
        public IEnumerable<Personne> Personnes => _state.Personnes.AsEnumerable();

        internal static List<string> _nomFamilles = new List<string>();

        private Famille()
        {

        }
        public Famille(NomFamille nomFamille)
        {
            Id = nomFamille;
        }

        internal void AjouterPersonne(string prenom, InformationsDeNaissance informationsDeNaissance)
        {
            if (!_state.Personnes.Any(pers => pers.Prenom == prenom
                             && pers.InformationsDeNaissance == informationsDeNaissance))
            {
                //_state.Personnes.Add(new Personne()
                //{
                //    Prenom = prenom,
                //    InformationsDeNaissance = informationsDeNaissance
                //});
                var @event =
                    new PersonneAjoutee(this.Id,
                    prenom,
                    informationsDeNaissance.LieuDeNaissance,
                    informationsDeNaissance.DateDeNaissance);
                AddDomainEvent(@event);
                _state.Apply(@event);
            }
        }

        public static Result CreerFamille(NomFamille nomFamille)
        {
            if (!_nomFamilles.Any(f => f == nomFamille.Value))
            {
                var famille = new Famille(nomFamille);
                _nomFamilles.Add(nomFamille.Value);
                return Result.Ok(famille);
            }
            return Result.Fail(FamilleNonCreeeCar.ExisteDeja);
        }

        public void RehydrateState(IEnumerable<IDomainEvent> events)
        {
            _state.ApplyRange(events);
            Id = _state.NomFamille;
        }
    }
}
