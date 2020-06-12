using Gefangenendilemma.Basis;
using GefangenenDilemma;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gefangenendilemma.Spielmodi
{
    class Tunier
    {
        private static List<BasisStrategie> _strategien;
        private static int Pos;
        private static int[] Punkte;

        public static void Start(List<BasisStrategie> strategien)
        {
            _strategien = strategien;
            Punkte = new int[_strategien.Count];
            var verbrechen = (Verbrechen)VerwaltungKram.EingabeZahlMinMax("Wie schwer sind die Verstöße? (2=schwer)", 0, 3);
            Console.WriteLine("\nStarte Spiellog:\n------------------------------------------------------------------------------------------------------------------------\n");
            _strategien.ForEach(x => Verhoer(Pos, 9, verbrechen));
            Pos = 0;
            Ergebnis();
        }
        static void Verhoer(int st1, int runde, Verbrechen verbrechen)
        {
            IVerbrechensModus strafe;
            //holt die beiden Strategien aus der Collection.
            BasisStrategie strategie1 = _strategien[st1];
            foreach (var strat in _strategien)
            {
                BasisStrategie strategie2 = strat;

                if (strategie1 == strategie2)
                    break;

                //setzt Startwerte
                int reaktion1 = BasisStrategie.NochNichtVerhoert;
                int reaktion2 = BasisStrategie.NochNichtVerhoert;
                int punkte1 = 0, punkte2 = 0;

                //beide Strategien über den Start informieren (Also es wird die Startmethode aufgerufen)

                strategie1.Start(runde, (int)verbrechen);
                strategie2.Start(runde, (int)verbrechen);

                //Schwere
                strafe = VerbrechensModus.Set(verbrechen);
                //Console.WriteLine($"Verhör zwischen {strategie1.Name()} und {strategie2.Name()} für {runde} Runden mit der Schwere {verbrechen}.");

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
                    Console.WriteLine($"Somit hat {strategie1.Name()} gewonnen.\n");
                }
                else if (punkte1 == punkte2)
                    Console.WriteLine($"Somit gibt es ein unentschieden zwischen {strategie1.Name()} und {strategie2.Name()}.\n");
                else
                {
                    Console.WriteLine($"Somit hat {strategie2.Name()} gewonnen.\n");
                }
                Punkte[_strategien.IndexOf(strategie1)] += punkte1;
                Punkte[_strategien.IndexOf(strategie2)] += punkte2;
            }
            Pos++;
        }
        static void Ergebnis()
        {
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------\nLogende\n\nErgebnis:\n");
            var resultatList = new List<KeyValuePair<BasisStrategie, int>>();
            for (int i = 0; i < _strategien.Count; i++)
            {
                resultatList.Add(new KeyValuePair<BasisStrategie, int>(_strategien[i], Punkte[i]));
            }
            resultatList = resultatList.OrderBy(x => x.Value).ToList();
            resultatList.ForEach(x => Console.WriteLine($"[{resultatList.IndexOf(x) + 1}]\t{x.Key.Name()} mit {x.Value} Punkten"));
            Console.WriteLine("\n");
        }
    }
}
