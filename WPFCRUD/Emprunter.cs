//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WPFCRUD
{
    using System;
    using System.Collections.Generic;
    
    public partial class Emprunter
    {
        public int id { get; set; }
        public Nullable<int> idLivre { get; set; }
        public Nullable<int> idLecteur { get; set; }
        public Nullable<System.DateTime> dateEmprunt { get; set; }
        public Nullable<System.DateTime> dateRetour { get; set; }
    
        public virtual Personne Personne { get; set; }
        public virtual Livre Livre { get; set; }
    }
}