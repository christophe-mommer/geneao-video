using System;
using System.Collections.Generic;
using System.Text;

namespace Geneao.Queries
{
    public class FamilleListItem
    {
        public string Nom { get; internal set; }
        public List<string> Membres { get; internal set; }
    }
}
