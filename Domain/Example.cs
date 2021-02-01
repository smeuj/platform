using System;

namespace Nouwan.Smeuj.Domain
{
    public class Example
    {
        public Example(Smeu smeu, string content, Author author)
        {
            Smeu = smeu;
            Content = content;
            Author = author;
            AddedOn = DateTimeOffset.Now;
        }

        public Smeu Smeu { get;}
        
        public string Content { get; private set; }
        
        public DateTimeOffset AddedOn { get;}
        
        public Author Author { get; }

        public void UpdateContent(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException($"{nameof(content)} should not be null");
            Content = content;
        }
    }
}