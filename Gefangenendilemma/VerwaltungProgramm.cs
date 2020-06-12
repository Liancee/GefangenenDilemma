using System;
using System.Collections.Generic;
using Gefangenendilemma.Basis;
using Gefangenendilemma.Spielmodi;
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
            //_strategien.Add(new periodisch_und_unfreundlich());
            _strategien.Add(new Zufall());
            _strategien.Add(new Aleksej_Sebastian_TobiTaktik());

            string eingabe;
            do
            {
                // Begrüßung
                Console.WriteLine("\nWillkommen zum Gefangenendilemma");
                Console.WriteLine("0 - Verhör zwischen 2 Strategien");
                Console.WriteLine("1 - Spiele selbst gegen eine Strategie");
                Console.WriteLine("2 - Lass alle Strategien in einem Tunier mit Neun Runden gegeneinander spielen");
                Console.WriteLine("X - Beenden");

                // Eingabe
                Console.Write("Treffen Sie ihre Option: ");
                eingabe = Console.ReadLine();

                // Auswerten der Eingabe
                switch (eingabe.ToLower())
                {
                    case "0":
                        _1v1.Start(_strategien);
                        break;
                    case "1":
                        SpielerVsStrategie.Start(_strategien);
                        break;
                    case "2":
                        Tunier.Start(_strategien);
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
        
        
    }
}