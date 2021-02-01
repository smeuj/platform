using System;

namespace Nouwan.Smeuj.Domain
{
    public record Message(long AuthorId, DateTime SendOn, long MessageId, long Id = 0)
    {
        public bool IsSaved => Id != 0;
    }
}
