using System;
using System.Collections.Generic;
using System.Text;

namespace Geneao.Identity
{
    public class PersonneId
    {
        public Guid Value { get; private set; }

        public PersonneId(Guid value)
        {
            Value = value;
        }

        public static PersonneId Generate()
            => new PersonneId(Guid.NewGuid());
    }
}
