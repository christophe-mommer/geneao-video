using System;
using System.Collections.Generic;
using System.Text;

namespace Geneao.Identity
{
    public class NomFamille
    {
        public string Value { get; private set; }

        public NomFamille(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("message", nameof(value));
            }
            if(value.Length < 3)
            {
                throw new ArgumentException("Nom de famille trop court");
            }

            Value = value;
        }
    }
}
