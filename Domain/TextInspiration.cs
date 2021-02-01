using System;

namespace Nouwan.Smeuj.Domain
{
    public class TextInspiration : Inspiration
    {
        public string Reason { get; private set; }

        public void SetReason(string newReason)
        {
            if (string.IsNullOrWhiteSpace(newReason)) throw new InvalidOperationException();
            Reason = newReason;
        }

        public TextInspiration(string reason, Smeu smeu) : base(smeu)
        {
            if (string.IsNullOrWhiteSpace(Reason)) throw new InvalidOperationException();
            Reason = reason;
        }
    }
}