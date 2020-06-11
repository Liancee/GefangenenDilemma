using Gefangenendilemma.Basis;

namespace GefangenenDilemma
{
    class MittlererVerstoß : IVerbrechensModus
    {
        public void VerhoerSchwerpunkt(int aktReaktion1, int aktReaktion2, ref int punkte1, ref int punkte2)
        {
            if (aktReaktion1 == BasisStrategie.Kooperieren && aktReaktion2 == BasisStrategie.Kooperieren)
            {
                punkte1 += 10;
                punkte2 += 10;
                return;
            }
            if (aktReaktion1 == BasisStrategie.Verrat && aktReaktion2 == BasisStrategie.Kooperieren)
            {
                punkte1 += 8;
                punkte2 += 0;
                return;
            }
            if (aktReaktion1 == BasisStrategie.Kooperieren && aktReaktion2 == BasisStrategie.Verrat)
            {
                punkte1 += 0;
                punkte2 += 8;
                return;
            }
            punkte1 += 4;
            punkte2 += 4;
        }
    }
}
