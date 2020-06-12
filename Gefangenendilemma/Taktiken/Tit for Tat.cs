using Gefangenendilemma.Basis;
using Gefangenendilemma.Spielmodi;

namespace Gefangenendilemma
{
    public class Tit_for_Tat : BasisStrategie
    {
        private int Runde { get; set; }
        public override string Name()
        {
            return "Tit for Tat";
        }
        public override string Autor()
        {
            return "Franz Linden";
        }
        public override void Start(int runde, int schwere)
        {
            Runde = 0;
        }
        public override int Verhoer(int letzteReaktion)
        {
            //return Aufgaben(letzteReaktion);
            return Test(letzteReaktion);
        }
        public int Test(int letzteReaktion)
        {
            Runde++;
            if (Runde > 2)
            {
                switch (letzteReaktion)
                {
                    case BasisStrategie.Kooperieren:
                        return BasisStrategie.Kooperieren;
                    case BasisStrategie.Verrat:
                        return BasisStrategie.Verrat;
                    default:
                        return -1;
                }
            }
            else
            {
                switch (Runde)
                {
                    case 1:
                        return BasisStrategie.Kooperieren;
                    case 2:
                        return BasisStrategie.Kooperieren;
                    default:
                        return -1;
                }
            }
        }
        public int Aufgaben(int letzteReaktion)
        {
            switch (letzteReaktion)
            {
                case BasisStrategie.NochNichtVerhoert:
                    return BasisStrategie.Verrat;

                case BasisStrategie.Verrat:
                    return BasisStrategie.Kooperieren;
                case BasisStrategie.Kooperieren:
                    return BasisStrategie.Verrat;
                default:
                    return -1;
            }
        }
    }
}