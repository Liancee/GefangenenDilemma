using Gefangenendilemma.Basis;
using System;

namespace Gefangenendilemma
{
    class periodisch_und_unfreundlich : BasisStrategie
    {
        private int Round { get; set; }
        public override string Name()
        {
            return "periodisch und unfreundlich";
        }
        public override string Autor()
        {
            return "Paul Schüler";
        }
        public override void Start(int runde, int schwere)
        {
            this.Round = 0;
        }
        public override int Verhoer(int letzteReaktion)
        {
            Round++;
            if (Round != 3)
                return BasisStrategie.Verrat;
            else
            {
                Round = 0;
                return BasisStrategie.Kooperieren;
            }
        }
    }
}