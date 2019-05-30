using CQELight.DAL.Attributes;
using CQELight.DAL.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Geneao.Data.Models
{
    [Table]
    public class Personne : PersistableEntity
    {
        [ForeignKey, Required]
        public Famille Famille { get; set; }
        [KeyStorageOf(nameof(Famille))]
        public string NomFamille { get; set; }
        [Column]
        public string Prenom { get; set; }
        [Column]
        public string LieuNaissance { get; set; }
        [Column]
        public DateTime DateNaissance { get; set; }
    }
}
