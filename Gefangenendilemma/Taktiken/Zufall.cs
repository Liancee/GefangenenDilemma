using Gefangenendilemma.Basis;
using System;

namespace Gefangenendilemma
{
    public class Zufall : BasisStrategie
    {
        public override string Name()
        {
            return "Zufall";
        }
        public override string Autor()
        {
            return "Paul Preis";
        }
        public override void Start(int runde, int schwere)
        {
            //Vorbereitungen für Start
        }
        public override int Verhoer(int letzteReaktion)
        {
            return new Random().Next(0, 2) == 0 ? BasisStrategie.Verrat : BasisStrategie.Kooperieren;
        }
    }
}