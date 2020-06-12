using Gefangenendilemma.Basis;
using GefangenenDilemma;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gefangenendilemma.Spielmodi
{
    class SpielerVsStrategie
    {
        private static List<BasisStrategie> _strategien;
        public static void Start(List<BasisStrategie> strategien)
        {
            _strategien = strategien;
            int st1;
            int runde;
            string playername;
            Verbrechen verbrechen;

            Console.WriteLine($"\nWillkommen zum Verhör\nWähle deinen Namen: ");
            playername = Console.ReadLine();
            Console.WriteLine("\nNun beginnt das Verhör zwischen Spieler und Strategie.\nGegen welche Strategie möchtest du spielen?");
            for (int i = 0; i < _strategien.Count; i++)
            {
                Console.WriteLine($"{i} - {_strategien[i].Name()}");
            }
            st1 = VerwaltungKram.EingabeZahlMinMax(string.Empty, 0, _strategien.Count);
            runde = VerwaltungKram.EingabeZahlMinMax("Wie viele Runden sollen diese verhört werden?", 1, 101);
            verbrechen = (Verbrechen)VerwaltungKram.EingabeZahlMinMax("Wie schwer sind die Verstöße? (2=schwer)", 0, 3);

            VerhoerSGS(st1, playername, runde, verbrechen);
        }
        static void VerhoerSGS(int st1, string playername, int runde, Verbrechen verbrechen)
        {
            IVerbrechensModus strafe;
            //holt die gegnerische Strategie aus der Collection.
            BasisStrategie strategie1 = _strategien[st1];

            //setzt Startwerte
            int reaktion1 = BasisStrategie.NochNichtVerhoert;
            int spielerReaktion = BasisStrategie.NochNichtVerhoert;
            int punkte1 = 0, punkte2 = 0;

            //beide Strategien über den Start informieren (Also es wird die Startmethode aufgerufen)

            //strategie1.Start(runde, schwere);
            //strategie2.Start(runde, schwere);

            //Schwere
            strafe = VerbrechensModus.Set(verbrechen);
            Console.WriteLine($"Verhör zwischen {playername} und {strategie1.Name()} für {runde} Runden mit der Schwere {verbrechen}.");

            //start
            for (int i = 0; i < runde; i++)
            {
                int aktReaktion1 = strategie1.Verhoer(spielerReaktion);
                if (i > 0)
                {
                    switch (VerwaltungKram.EingabeZahlMinMax($"{strategie1.Name()} hat {(reaktion1 == 1 ? "dich " : string.Empty)}letzte Runde {(reaktion1 == 0 ? "kooperiert" : "verraten")}, wie möchtest du reagieren?\n[0] Kooperieren\n[1] Verraten", 0, 2))
                    {
                        case 0:
                            spielerReaktion = BasisStrategie.Kooperieren;
                            break;
                        case 1:
                            spielerReaktion = BasisStrategie.Verrat;
                            break;
                    }
                }
                else
                {
                    //Console.WriteLine("Erste Runde, womit moechtest du anfangen?\n[0] Verraten\n[1] Kooperieren");
                    switch (VerwaltungKram.EingabeZahlMinMax("Erste Runde, womit möchtest du anfangen?\n[0] Kooperieren\n[1] Verraten", 0, 2))
                    {
                        case 0:
                            spielerReaktion = BasisStrategie.Kooperieren;
                            break;
                        case 1:
                            spielerReaktion = BasisStrategie.Verrat;
                            break;
                    }
                }
                int aktSpielerReaktion = spielerReaktion;
                if (aktReaktion1 == -1 || aktSpielerReaktion == -1)
                {
                    Console.WriteLine(@"Fehler, Methode ""Verhör"" hat Fehlercode -1 zurückgegeben");
                    //Hier soll das Programm restartet werden
                }
                //punkte berechnen
                strafe.VerhoerSchwerpunkt(aktReaktion1, aktSpielerReaktion, ref punkte1, ref punkte2);
                Console.WriteLine($"Der Punktestand beträgt:\n\t{playername} - {punkte2}\n\t{strategie1.Name()} - {punkte1}\nWobei du{(aktSpielerReaktion == 1 ? " ihn" : string.Empty)} {(aktSpielerReaktion == 1 ? "verraten" : "kooperiert")} hast und {(aktReaktion1 == aktSpielerReaktion ? (aktReaktion1 == 1 ? "er dich auch" : "er auch") : aktReaktion1 == 1 ? "er dich verraten hat" : "er kooperiert hat")}.\n");
                //reaktion für den nächsten durchlauf merken
                reaktion1 = aktReaktion1;
                spielerReaktion = aktSpielerReaktion;
            }

            //ausgabe
            Console.WriteLine($"{strategie1.Name()} hat {punkte1} Punkte erhalten.");
            Console.WriteLine($"{playername} hat {punkte2} Punkte erhalten.");
            if (punkte1 < punkte2)
            {
                Console.WriteLine($"Somit hat {strategie1.Name()} gewonnen.");
            }
            else if (punkte1 == punkte2)
                Console.WriteLine($"Somit gibt es ein unentschieden zwischen {strategie1.Name()} und {playername}.");
            else
            {
                Console.WriteLine($"Somit hat {playername} gewonnen.\n");
            }
        }
    }
}
