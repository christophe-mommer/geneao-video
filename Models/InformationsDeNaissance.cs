using CQELight.Abstractions.DDD;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geneao.Models
{
    public class InformationsDeNaissance : ValueObject<InformationsDeNaissance>
    {
        public string LieuDeNaissance { get; private set; }
        public DateTime DateDeNaissance { get; private set; }

        public InformationsDeNaissance(string lieuNaissance, DateTime dateDeNaissance)
        {
            if (string.IsNullOrWhiteSpace(lieuNaissance))
            {
                throw new ArgumentException("message", nameof(lieuNaissance));
            }

            LieuDeNaissance = lieuNaissance;
            DateDeNaissance = dateDeNaissance;
        }

        protected override bool EqualsCore(InformationsDeNaissance other)
            => other.LieuDeNaissance == LieuDeNaissance
            && other.DateDeNaissance == DateDeNaissance;

        protected override int GetHashCodeCore()
            => (LieuDeNaissance + DateDeNaissance.ToString("yyyyMMdd")).GetHashCode();
    }
}
