using CQELight.DAL.Attributes;
using CQELight.DAL.Common;
using CQELight.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geneao.Data.Models
{
    [Table]
    public class Famille : CustomKeyPersistableEntity
    {
        [PrimaryKey]
        public virtual string Nom { get; set; }
        public virtual ICollection<Personne> Personnes { get; set; }
    }
}
