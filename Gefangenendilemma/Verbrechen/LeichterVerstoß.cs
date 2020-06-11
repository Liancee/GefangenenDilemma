using Gefangenendilemma.Basis;
using System;
using System.Collections.Generic;

namespace GefangenenDilemma
{
    class LeichterVerstoß : IVerbrechensModus
    {
        public void VerhoerSchwerpunkt(int aktReaktion1, int aktReaktion2, ref int punkte1, ref int punkte2)
        {
            if (aktReaktion1 == BasisStrategie.Kooperieren && aktReaktion2 == BasisStrategie.Kooperieren)
            {
                punkte1 += 3;
                punkte2 += 3;
                return;
            }
            if (aktReaktion1 == BasisStrategie.Verrat && aktReaktion2 == BasisStrategie.Kooperieren)
            {
                punkte1 += 0;
                punkte2 += 9;
                return;
            }
            if (aktReaktion1 == BasisStrategie.Kooperieren && aktReaktion2 == BasisStrategie.Verrat)
            {
                punkte1 += 9;
                punkte2 += 0;
                return;
            }
            punkte1 += 6;
            punkte2 += 6;
        }
    }
}
