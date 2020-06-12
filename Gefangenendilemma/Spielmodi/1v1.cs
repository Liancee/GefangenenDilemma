using Gefangenendilemma.Basis;
using GefangenenDilemma;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Gefangenendilemma.Spielmodi
{
    class _1v1
    {
        private static List<BasisStrategie> _strategien;
        public static void Start(List<BasisStrategie> strategien)
        {
            _strategien = strategien;
            int st1, st2;
            int runde;
            Verbrechen verbrechen;

            Console.WriteLine("Willkommen zum Verhör zwischen 2 Strategien");
            for (int i = 0; i < _strategien.Count; i++)
            {
                Console.WriteLine($"{i} - {_strategien[i].Name()}");
            }
            Console.WriteLine("Wählen Sie ihre 2 Strategien:");
            st1 = VerwaltungKram.EingabeZahlMinMax("Wählen Sie die 1. Strategie", 0, _strategien.Count);
            st2 = VerwaltungKram.EingabeZahlMinMax("Wählen Sie die 2. Strategie", 0, _strategien.Count);
            while (st1 == st2)
            {
                Console.WriteLine("Du hast die gleichen Strategien gewählt, dies kann zu Problemen führen,\nwenn die Strategien zum Beispiel auf den gleichen Rundenzähler zugreifen.\nAuf Grund dessen haben wir die Möglichkeit ausgeschaltet, bitte fahre fort..\n");
                st1 = VerwaltungKram.EingabeZahlMinMax("Wählen Sie die 1. Strategie", 0, _strategien.Count);
                st2 = VerwaltungKram.EingabeZahlMinMax("Wählen Sie die 2. Strategie", 0, _strategien.Count);
            }
            runde = VerwaltungKram.EingabeZahlMinMax("Wie viele Runden sollen diese verhört werden?", 1, 101);
            verbrechen = (Verbrechen)VerwaltungKram.EingabeZahlMinMax("Wie schwer sind die Verstöße? (2=schwer)", 0, 3);

            Verhoer(st1, st2, runde, verbrechen);
        }
        static void Verhoer(int st1, int st2, int runde, Verbrechen verbrechen)
        {
            IVerbrechensModus strafe;
            //holt die beiden Strategien aus der Collection.
            BasisStrategie strategie1 = _strategien[st1];
            BasisStrategie strategie2 = _strategien[st2];

            //setzt Startwerte
            int reaktion1 = BasisStrategie.NochNichtVerhoert;
            int reaktion2 = BasisStrategie.NochNichtVerhoert;
            int punkte1 = 0, punkte2 = 0;

            //beide Strategien über den Start informieren (Also es wird die Startmethode aufgerufen)

            strategie1.Start(runde, (int)verbrechen);
            strategie2.Start(runde, (int)verbrechen);

            //Schwere
            strafe = VerbrechensModus.Set(verbrechen);
            Console.WriteLine($"\nVerhör zwischen {strategie1.Name()} und {strategie2.Name()} für {runde} Runden mit der Schwere {verbrechen}.");

            //start
            for (int i = 0; i < runde; i++)
            {
                //beide verhören
                int aktReaktion1 = strategie1.Verhoer(reaktion2);
                int aktReaktion2 = strategie2.Verhoer(reaktion1);
                if (aktReaktion1 == -1 || aktReaktion2 == -1)
                {
                    Console.WriteLine(@"Fehler, Methode ""Verhoer"" hat Fehlercode -1 zurückgegeben");
                    //Hier soll das Programm restartet werden
                }
                //punkte berechnen
                strafe.VerhoerSchwerpunkt(aktReaktion1, aktReaktion2, ref punkte1, ref punkte2);

                //reaktion für den nächsten durchlauf merken
                reaktion1 = aktReaktion1;
                reaktion2 = aktReaktion2;
            }

            //ausgabe
            Console.WriteLine($"{strategie1.Name()} hat {punkte1} Punkte erhalten.");
            Console.WriteLine($"{strategie2.Name()} hat {punkte2} Punkte erhalten.");
            if (punkte1 < punkte2)
            {
                Console.WriteLine($"Somit hat {strategie1.Name()} gewonnen.");
            }
            else if (punkte1 == punkte2)
                Console.WriteLine($"Somit gibt es ein unentschieden zwischen {strategie1.Name()} und {strategie2.Name()}.");
            else
            {
                Console.WriteLine($"Somit hat {strategie2.Name()} gewonnen.");
            }
        }
    }
}
