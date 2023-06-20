using System;
using System.Collections.Generic;

namespace SignInTPMedia.Models
{
    public partial class Lienhe
    {
        public Lienhe()
        {
            Baocaos = new HashSet<Baocao>();
            Kehoaches = new HashSet<Kehoach>();
        }

        public int Idlh { get; set; }
        public int? SoKhlh { get; set; }

        public virtual ICollection<Baocao> Baocaos { get; set; }
        public virtual ICollection<Kehoach> Kehoaches { get; set; }
    }
}
