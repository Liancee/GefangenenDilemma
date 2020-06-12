using Gefangenendilemma.Basis;
using System;

namespace Gefangenendilemma
{
    public class Aleksej_Sebastian_TobiTaktik : BasisStrategie
    {
        private bool kopierer;
        private bool reaktion;
        Random random = new Random();
        public override string Name()
        {
            return "Zufälliger Kopierer";
        }
        public override string Autor()
        {
            return "Aleksej Demtschuk";
        }
        public override void Start(int runde, int schwere)
        {
            reaktion = false;
        }
        public override int Verhoer(int letzteReaktion)
        {
            kopierer = random.Next(0, 2) != 0;

            if (letzteReaktion != Verrat)
            {
                reaktion = false;
            }
            else if (kopierer && letzteReaktion == Verrat)
            {
                reaktion = true;
            }
            else if (!kopierer && letzteReaktion == Verrat)
            {
                reaktion = false;
            }
            return reaktion ? Verrat : Kooperieren;
        }
    }
}