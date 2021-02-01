namespace Nouwan.Smeuj.Domain
{
    public abstract class Inspiration
    {
        protected Inspiration(Smeu smeu)
        {
            Smeu = smeu;
        }

        public Smeu Smeu { get; }
    }
}