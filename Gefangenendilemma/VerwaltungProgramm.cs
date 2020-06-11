using System;
using System.Collections.Generic;
using Gefangenendilemma.Basis;
using GefangenenDilemma;

namespace Gefangenendilemma
{
    /// <summary>
    /// Diese Klasse können Sie beliebig umschreiben, jenachdem welche Tasks sie erledigen.
    /// </summary>
    class VerwaltungProgramm
    {
        /// <summary>
        /// Diese Liste(Collection) enthält alle Gefangene/Strategien
        /// </summary>
        private static List<BasisStrategie> _strategien;

        static void Main(string[] args)
        {
            //bekannt machen der ganzen strategien
            _strategien = new List<BasisStrategie>();
            _strategien.Add(new GrollStrategie());
            _strategien.Add(new VerrateImmerStrategie());
            _strategien.Add(new Tit_for_Tat());
            _strategien.Add(new periodisch_und_unfreundlich());
            _strategien.Add(new Zufall());
            _strategien.Add(new Aleksej_Sebastian_TobiTaktik());

            string eingabe;
            do
            {
                // Begrüßung
                Console.WriteLine("Willkommen zum Gefangenendilemma");
                Console.WriteLine("0 - Verhör zwischen 2 Strategien");
                Console.WriteLine("1 - Spiele selbst gegen eine Strategie");
                Console.WriteLine("X - Beenden");

                // Eingabe
                Console.Write("Treffen Sie ihre Option: ");
                eingabe = Console.ReadLine();

                // Auswerten der Eingabe
                switch (eingabe.ToLower())
                {
                    case "0":
                        Gefangene2();
                        break;
                    case "1":
                        SpielerGegenStrategie();
                        break;
                    case "X":
                        break;
                    default:
                        Console.WriteLine($"Eingabe {eingabe} nicht erkannt.");
                        break;
                }
            } while (!"x".Equals(eingabe?.ToLower()));
        }

        /// <summary>
        /// Fragt 2 Strategien, Länge und Schwere ab.
        /// </summary>
        static void Gefangene2()
        {
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

            //strategie1.Start(runde, schwere);
            //strategie2.Start(runde, schwere);

            //Schwere
            strafe = VerbrechensModus.Set(verbrechen);
            Console.WriteLine($"Verhör zwischen {strategie1.Name()} und {strategie2.Name()} für {runde} Runden mit der Schwere {verbrechen}.");

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
                //VerhoerSchwerPunkte(aktReaktion1, aktReaktion2, ref punkte1, ref punkte2);

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
        static void SpielerGegenStrategie()
        {
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
                if (aktReaktion1 == -1 || spielerReaktion == -1)
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