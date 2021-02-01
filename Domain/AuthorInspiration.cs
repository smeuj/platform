namespace Nouwan.Smeuj.Domain
{
    public class AuthorInspiration : Inspiration
    {
        public AuthorInspiration(Author inspiredBy, Smeu smeu) : base(smeu)
        {
            InspiredBy = inspiredBy;
        }

        public Author InspiredBy { get; }
    }
}