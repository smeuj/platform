using System;
using System.Collections.Generic;

namespace Nouwan.Smeuj.Domain
{
    public class Smeu
    {
        public Smeu(string text, ICollection<Inspiration> inspirations, DateTimeOffset createdOn, ICollection<Example> examples, int authorId)
        {
            Text = text;
            Inspirations = inspirations;
            CreatedOn = createdOn;
            Examples = examples;
            AuthorId = authorId;
            Reference = Guid.NewGuid();
        }

        public Smeu(string text, ICollection<Inspiration> inspirations, DateTimeOffset createdOn, ICollection<Example> examples, int authorId, Guid reference)
        {
            Text = text;
            Inspirations = inspirations;
            CreatedOn = createdOn;
            Examples = examples;
            AuthorId = authorId;
            Reference = reference;
        }

        public string Text { get; set; }
        public Guid Reference { get; set; }

        public int AuthorId { get; set; }
        
        public DateTimeOffset CreatedOn { get; }
        
        public ICollection<Inspiration> Inspirations { get; set; }
        
        public ICollection<Example> Examples { get; set; }

        public int MessageId { get; set; }

        public bool Hidden { get; private set; }

        public void Hide()
        {
            Hidden = true;
        }
    }
}
