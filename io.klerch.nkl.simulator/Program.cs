using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;

namespace io.klerch.nkl.simulator
{
    class MainClass
    {
        // Bitte die nachfolgenden Parameter bei Bedarf anpassen

        // Gib hier deine siebenstellige NKL-Losnummer an
		const string Losnummer = "1815426";
        // Gib ihr den Anteil an, den du am Los besitzt (1/1 oder 1/2 oder 1/4 oder 1/8 oder 1/16)
        const double Losanteil = 1/1;
        // Gib hier die Anzahl der Simulationen an, die durchgefuehrt werden sollen
		const int Anzahl_Runden = 1000;
        // Hat das Los den Millionen-Joker?
        const bool MillionenJoker = true;
        // Hat das Los den Renten-Joker?
        const bool RentenJoker = true;

		// Lospreise in Euro
		const int PreisVollesLos = 160;
		const int PreisMillionenJoker = 28;
		const int PreisRentenJoker = 5;















        // Nachfolgendes bitte nicht anfassen, es sei denn du weisst was du tust.

		static Dictionary<string, KeyValuePair<int, int>> Gewinne = new Dictionary<string, KeyValuePair<int, int>>();
		static HashSet<string> Geloste = new HashSet<string>();
		private static int Jackpot = 0;

		static void Main(string[] args)
		{
            const double intRundenEinsatz = (PreisVollesLos * Losanteil)
                                             + (MillionenJoker ? PreisMillionenJoker : 0) 
                                             + (RentenJoker ? PreisRentenJoker : 0) * 6;
			var intRunden = 0;
			var intBarwert = 0;
			var intNieten = 0;
			var intVerluste = 0;
			var intEinsatz = 0;
			var intGewinn = 0;
			var intHoherGewinn = 0;

			var oWatch = new Stopwatch();

			oWatch.Start();
			while (++intRunden <= Anzahl_Runden)
			{
				var intBarwertRunde = 0;
                Console.BackgroundColor = ConsoleColor.Black;
				Console.ForegroundColor = ConsoleColor.White;
				Console.Write("{0}. Runde:", intRunden);
				intBarwertRunde += Klasse1(strLosnummer: Losnummer);
				Console.ForegroundColor = ConsoleColor.White; Console.Write(".");
				intBarwertRunde += Klasse2(strLosnummer: Losnummer);
				Console.ForegroundColor = ConsoleColor.White; Console.Write(".");
				intBarwertRunde += Klasse3(strLosnummer: Losnummer);
				Console.ForegroundColor = ConsoleColor.White; Console.Write(".");
				intBarwertRunde += Klasse4(strLosnummer: Losnummer);
				Console.ForegroundColor = ConsoleColor.White; Console.Write(".");
				intBarwertRunde += Klasse5(strLosnummer: Losnummer);
				Console.ForegroundColor = ConsoleColor.White; Console.Write(".");
				intBarwertRunde += Klasse6(strLosnummer: Losnummer);
				intBarwert += intBarwertRunde;

				if (intBarwertRunde == 0) intNieten++;
				else if (intBarwertRunde < intRundenEinsatz) intVerluste++;
				else if (intBarwertRunde == intRundenEinsatz) intEinsatz++;
				else if (intBarwertRunde >= 5000) intHoherGewinn++;
				else intGewinn++;

				Console.ForegroundColor = ConsoleColor.White; Console.WriteLine(" ({0:G} Euro)", intBarwert);
			}
			oWatch.Stop();
			Console.WriteLine("===============================");
			Console.WriteLine("Laufzeit: {0} ms", oWatch.ElapsedMilliseconds);
			Console.WriteLine("===============================");
			Console.WriteLine("{0} Runden mit Nieten", intNieten);
			Console.WriteLine("{0} Runden mit Verlust", intVerluste);
			Console.WriteLine("{0} Runden mit Einsatz erspielt", intEinsatz);
			Console.WriteLine("{0} Runden mit Gewinn", intGewinn);
			Console.WriteLine("{0} Runden mit hohem Gewinn", intHoherGewinn);
			Console.WriteLine("===============================");
			Console.WriteLine("Geldeinsatz: {0} Euro", intRunden * intRundenEinsatz);
			Console.WriteLine("Gewinne: {0} Euro", intBarwert);
			Console.WriteLine("Saldo: {0} Euro", intBarwert - (intRunden * intRundenEinsatz));
			Console.WriteLine("Rundenschnitt: {0} Euro", intBarwert / intRunden);
			Console.WriteLine("===============================");
			foreach (var gewinn in Gewinne.OrderByDescending(x => x.Value.Key))
			{
				Console.ForegroundColor = ConsoleColor.White;
				Console.Write(gewinn.Value.Value + "x ");
				AusgabeGewinn(gewinn.Key, gewinn.Value.Key);
				Console.WriteLine(Math.Round(1.0000 * gewinn.Value.Value / intRunden * 100.00000, 5) + "%");
			}
			Console.ReadLine();
		}

		private static void AusgabeGewinn(string strGewinn, int intWert)
		{
			Console.ForegroundColor = intWert > 0 && intWert < 1000 ? ConsoleColor.Yellow :
									  intWert < 5000 ? ConsoleColor.DarkYellow :
									  intWert <= 10000 ? ConsoleColor.Green :
									  intWert <= 25000 ? ConsoleColor.DarkGreen :
									  intWert <= 50000 ? ConsoleColor.Magenta :
									  intWert <= 80000 ? ConsoleColor.DarkMagenta :
									  intWert <= 100000 ? ConsoleColor.Red :
									  intWert > 100000 ? ConsoleColor.DarkRed :
									  ConsoleColor.White;
			Console.Write("{0} ", strGewinn);
			Console.ForegroundColor = ConsoleColor.White;
		}

		static int MerkeGewinn(string strGewinn, int intWert)
		{
			if (Gewinne.ContainsKey(strGewinn)) Gewinne[strGewinn] = new KeyValuePair<int, int>(intWert, Gewinne[strGewinn].Value + 1);
			else Gewinne.Add(strGewinn, new KeyValuePair<int, int>(intWert, 1));

			if (intWert > 0)
			{
				AusgabeGewinn(strGewinn, intWert);
			}

			if (intWert > 10000)
			{

			}
			return intWert;
		}

		static int Klasse1(string strLosnummer)
		{
			Jackpot = 3000000;

			var intBarwert = 0;
			// 13 x 1 Mio.
			for (var i = 0; i < 13; i++)
			{
				if (Verlosung(strLosnummer, 1)) intBarwert += MerkeGewinn("1M", 1000000);
			}
			// 14 x 100 x 10.000 €
			for (var i = 0; i < 14; i++)
			{
				if (Verlosung(strLosnummer, 100)) intBarwert += MerkeGewinn("10k", 10000);
			}
			// Hauptziehung 1
			if (Verlosung(strLosnummer, 180000)) intBarwert += MerkeGewinn("160", 160);
			else if (Verlosung(strLosnummer, 300, true, false)) intBarwert += MerkeGewinn("1k", 1000);
			else if (Verlosung(strLosnummer, 30, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			else if (Verlosung(strLosnummer, 5, true, false)) intBarwert += MerkeGewinn("100k", 100000);
			else if (Verlosung(strLosnummer, 1, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			// Hauptziehung 2
			if (Verlosung(strLosnummer, 6000)) intBarwert += MerkeGewinn("160", 160);
			else if (Verlosung(strLosnummer, 300, true, false)) intBarwert += MerkeGewinn("1k", 1000);
			else if (Verlosung(strLosnummer, 30, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			else if (Verlosung(strLosnummer, 5, true, false)) intBarwert += MerkeGewinn("100k", 100000);
			else if (Verlosung(strLosnummer, 1, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			// Hauptziehung 3
			if (Verlosung(strLosnummer, 3000)) intBarwert += MerkeGewinn("160", 160);
			else if (Verlosung(strLosnummer, 300, true, false)) intBarwert += MerkeGewinn("1k", 1000);
			else if (Verlosung(strLosnummer, 30, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			else if (Verlosung(strLosnummer, 5, true, false)) intBarwert += MerkeGewinn("100k", 100000);
			else if (Verlosung(strLosnummer, 1, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			// Hauptziehung 4
			if (Verlosung(strLosnummer, 3000)) intBarwert += MerkeGewinn("160", 160);
			else if (Verlosung(strLosnummer, 300, true, false)) intBarwert += MerkeGewinn("1k", 1000);
			else if (Verlosung(strLosnummer, 30, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			else if (Verlosung(strLosnummer, 5, true, false)) intBarwert += MerkeGewinn("100k", 100000);
			else if (Verlosung(strLosnummer, 1, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			// Reise
			if (Verlosung(strLosnummer, 200, false)) intBarwert += MerkeGewinn("Reise", 5000);
			// Kreuzfahrt
			if (Verlosung(strLosnummer, 300, false)) intBarwert += MerkeGewinn("Kreuzfahrt", 12000);
			// Extra-Lohn
			if (Verlosung(strLosnummer, 20, false)) intBarwert += MerkeGewinn("EL-3000", 36000);
			// Millionen-Joker
			if (MillionenJoker && Verlosung(strLosnummer, 10)) intBarwert += MerkeGewinn("1M", 1000000);
			// Renten-Joker
			if (RentenJoker && Verlosung(strLosnummer, 1, false)) intBarwert += MerkeGewinn("SR-5000", (5000 * 120));
			else if (RentenJoker && Verlosung(strLosnummer, 4, false, false)) intBarwert += MerkeGewinn("SR-3000", (3000 * 120));
			else if (RentenJoker && Verlosung(strLosnummer, 45, false, false)) intBarwert += MerkeGewinn("SR-1000", (1000 * 120));
			// Jackpot
			if (GenerateRandomNumber(0, 2) == 0)
			{
				if (Verlosung(strLosnummer, 1, false)) intBarwert += MerkeGewinn("Jackpot " + Jackpot, Jackpot);
				Jackpot = 0;
			}
			return intBarwert;
		}

		static int Klasse2(string strLosnummer)
		{
			Jackpot += 2000000;

			var intBarwert = 0;
			// 13 x 1 Mio.
			for (var i = 0; i < 13; i++)
			{
				if (Verlosung(strLosnummer, 1)) intBarwert += MerkeGewinn("1M", 1000000);
			}
			// 13 x 100 x 10.000 €
			for (var i = 0; i < 13; i++)
			{
				if (Verlosung(strLosnummer, 100)) intBarwert += MerkeGewinn("10k", 10000);
			}
			// Hauptziehung 1
			if (Verlosung(strLosnummer, 5)) intBarwert += MerkeGewinn("100k", 100000);
			else if (Verlosung(strLosnummer, 210000, true, false)) intBarwert += MerkeGewinn("160", 160);
			else if (Verlosung(strLosnummer, 600, true, false)) intBarwert += MerkeGewinn("1k", 1000);
			else if (Verlosung(strLosnummer, 30, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			else if (Verlosung(strLosnummer, 1, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			// Hauptziehung 2
			if (Verlosung(strLosnummer, 6000)) intBarwert += MerkeGewinn("160", 160);
			else if (Verlosung(strLosnummer, 600, true, false)) intBarwert += MerkeGewinn("1k", 1000);
			else if (Verlosung(strLosnummer, 600, true, false)) intBarwert += MerkeGewinn("1k", 1000);
			else if (Verlosung(strLosnummer, 5, true, false)) intBarwert += MerkeGewinn("100k", 100000);
			else if (Verlosung(strLosnummer, 1, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			// Hauptziehung 3
			if (Verlosung(strLosnummer, 3000)) intBarwert += MerkeGewinn("160", 160);
			else if (Verlosung(strLosnummer, 600, true, false)) intBarwert += MerkeGewinn("1k", 1000);
			else if (Verlosung(strLosnummer, 30, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			else if (Verlosung(strLosnummer, 5, true, false)) intBarwert += MerkeGewinn("100k", 100000);
			else if (Verlosung(strLosnummer, 1, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			// Hauptziehung 4
			if (Verlosung(strLosnummer, 3000)) intBarwert += MerkeGewinn("160", 160);
			else if (Verlosung(strLosnummer, 600, true, false)) intBarwert += MerkeGewinn("1k", 1000);
			else if (Verlosung(strLosnummer, 30, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			else if (Verlosung(strLosnummer, 5, true, false)) intBarwert += MerkeGewinn("100k", 100000);
			else if (Verlosung(strLosnummer, 1, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			// BMW 1er
			if (Verlosung(strLosnummer, 175, false)) intBarwert += MerkeGewinn("BMW 1er", 28000);
			// Extra-Lohn
			if (Verlosung(strLosnummer, 30, false)) intBarwert += MerkeGewinn("EL-3000", 36000);
			// Millionen-Joker
			if (MillionenJoker && Verlosung(strLosnummer, 15)) intBarwert += MerkeGewinn("1M", 1000000);
			// Renten-Joker
			if (RentenJoker && Verlosung(strLosnummer, 1, false)) intBarwert += MerkeGewinn("SR-5000", (5000 * 120));
			else if (RentenJoker && Verlosung(strLosnummer, 4, false, false)) intBarwert += MerkeGewinn("SR-3000", (3000 * 120));
			else if (RentenJoker && Verlosung(strLosnummer, 45, false, false)) intBarwert += MerkeGewinn("SR-1000", (1000 * 120));

			// Jackpot
			if (GenerateRandomNumber(0, 2) == 0)
			{
				if (Verlosung(strLosnummer, 1, false)) intBarwert += MerkeGewinn("Jackpot " + Jackpot, Jackpot);
				Jackpot = 0;
			}

			return intBarwert;
		}

		static int Klasse3(string strLosnummer)
		{
			Jackpot += 2000000;

			var intBarwert = 0;

			// 14 x 1 Mio.
			for (var i = 0; i < 14; i++)
			{
				if (Verlosung(strLosnummer, 1)) intBarwert += MerkeGewinn("1M", 1000000);
			}
			// 13 x 100 x 10.000 €
			for (var i = 0; i < 13; i++)
			{
				if (Verlosung(strLosnummer, 100)) intBarwert += MerkeGewinn("10k", 10000);
			}
			// Hauptziehung 1
			if (Verlosung(strLosnummer, 225000)) intBarwert += MerkeGewinn("160", 160);
			else if (Verlosung(strLosnummer, 600, true, false)) intBarwert += MerkeGewinn("1k", 1000);
			else if (Verlosung(strLosnummer, 30, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			else if (Verlosung(strLosnummer, 5, true, false)) intBarwert += MerkeGewinn("100k", 100000);
			else if (Verlosung(strLosnummer, 1, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			// Hauptziehung 2
			if (Verlosung(strLosnummer, 6000)) intBarwert += MerkeGewinn("160", 160);
			else if (Verlosung(strLosnummer, 600, true, false)) intBarwert += MerkeGewinn("1k", 1000);
			else if (Verlosung(strLosnummer, 30, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			else if (Verlosung(strLosnummer, 5, true, false)) intBarwert += MerkeGewinn("100k", 100000);
			else if (Verlosung(strLosnummer, 1, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			// Hauptziehung 3
			if (Verlosung(strLosnummer, 3000)) intBarwert += MerkeGewinn("160", 160);
			else if (Verlosung(strLosnummer, 600, true, false)) intBarwert += MerkeGewinn("1k", 1000);
			else if (Verlosung(strLosnummer, 30, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			else if (Verlosung(strLosnummer, 5, true, false)) intBarwert += MerkeGewinn("100k", 100000);
			else if (Verlosung(strLosnummer, 1, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			// Hauptziehung 4
			if (Verlosung(strLosnummer, 3000)) intBarwert += MerkeGewinn("160", 160);
			else if (Verlosung(strLosnummer, 600, true, false)) intBarwert += MerkeGewinn("1k", 1000);
			else if (Verlosung(strLosnummer, 30, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			else if (Verlosung(strLosnummer, 5, true, false)) intBarwert += MerkeGewinn("100k", 100000);
			else if (Verlosung(strLosnummer, 1, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			// Auto
			if (Verlosung(strLosnummer, 150, false)) intBarwert += MerkeGewinn("Benz GLA", 32000);
			// Extra-Lohn
			if (Verlosung(strLosnummer, 40, false)) intBarwert += MerkeGewinn("EL-3000", 36000);
			// Millionen-Joker
			if (MillionenJoker && Verlosung(strLosnummer, 25)) intBarwert += MerkeGewinn("1M", 1000000);
			// Renten-Joker
			if (RentenJoker && Verlosung(strLosnummer, 1, false)) intBarwert += MerkeGewinn("SR-5000", (5000 * 120));
			else if (RentenJoker && Verlosung(strLosnummer, 4, false, false)) intBarwert += MerkeGewinn("SR-3000", (3000 * 120));
			else if (RentenJoker && Verlosung(strLosnummer, 45, false, false)) intBarwert += MerkeGewinn("SR-1000", (1000 * 120));
			// Silvester-Sonderziehung
			if (Verlosung(strLosnummer, 30000)) intBarwert += MerkeGewinn("1k", 1000);

			// Jackpot
			if (GenerateRandomNumber(0, 2) == 0)
			{
				if (Verlosung(strLosnummer, 1, false)) intBarwert += MerkeGewinn("Jackpot " + Jackpot, Jackpot);
				Jackpot = 0;
			}

			return intBarwert;
		}

		static int Klasse4(string strLosnummer)
		{
			Jackpot += 2000000;

			var intBarwert = 0;
			// 14 x 1 Mio.
			for (var i = 0; i < 14; i++)
			{
				if (Verlosung(strLosnummer, 1)) intBarwert += MerkeGewinn("1M", 1000000);
			}
			// 13 x 100 x 10.000 €
			for (var i = 0; i < 13; i++)
			{
				if (Verlosung(strLosnummer, 100)) intBarwert += MerkeGewinn("10k", 10000);
			}
			// Hauptziehung 1
			if (Verlosung(strLosnummer, 240000)) intBarwert += MerkeGewinn("160", 160);
			else if (Verlosung(strLosnummer, 900, true, false)) intBarwert += MerkeGewinn("1k", 1000);
			else if (Verlosung(strLosnummer, 30, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			else if (Verlosung(strLosnummer, 30, true, false)) intBarwert += MerkeGewinn("100k", 100000);
			else if (Verlosung(strLosnummer, 1, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			// Hauptziehung 2
			if (Verlosung(strLosnummer, 6000)) intBarwert += MerkeGewinn("160", 160);
			else if (Verlosung(strLosnummer, 900, true, false)) intBarwert += MerkeGewinn("1k", 1000);
			else if (Verlosung(strLosnummer, 30, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			else if (Verlosung(strLosnummer, 5, true, false)) intBarwert += MerkeGewinn("100k", 100000);
			else if (Verlosung(strLosnummer, 1, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			// Hauptziehung 3
			if (Verlosung(strLosnummer, 3000)) intBarwert += MerkeGewinn("160", 160);
			else if (Verlosung(strLosnummer, 900, true, false)) intBarwert += MerkeGewinn("1k", 1000);
			else if (Verlosung(strLosnummer, 30, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			else if (Verlosung(strLosnummer, 5, true, false)) intBarwert += MerkeGewinn("100k", 100000);
			else if (Verlosung(strLosnummer, 1, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			// Hauptziehung 4
			if (Verlosung(strLosnummer, 3000)) intBarwert += MerkeGewinn("160", 160);
			else if (Verlosung(strLosnummer, 900, true, false)) intBarwert += MerkeGewinn("1k", 1000);
			else if (Verlosung(strLosnummer, 30, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			else if (Verlosung(strLosnummer, 5, true, false)) intBarwert += MerkeGewinn("100k", 100000);
			else if (Verlosung(strLosnummer, 1, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			// Auto
			if (Verlosung(strLosnummer, 100, false)) intBarwert += MerkeGewinn("Audi A5 Cabrio", 56000);
			// Extra-Lohn
			if (Verlosung(strLosnummer, 50, false)) intBarwert += MerkeGewinn("EL-3000", 36000);
			// Millionen-Joker
			if (MillionenJoker && Verlosung(strLosnummer, 30)) intBarwert += MerkeGewinn("1M", 1000000);
			// Renten-Joker
			if (RentenJoker && Verlosung(strLosnummer, 1, false)) intBarwert += MerkeGewinn("SR-5000", (5000 * 120));
			else if (RentenJoker && Verlosung(strLosnummer, 4, false, false)) intBarwert += MerkeGewinn("SR-3000", (3000 * 120));
			else if (RentenJoker && Verlosung(strLosnummer, 45, false, false)) intBarwert += MerkeGewinn("SR-1000", (1000 * 120));
			// Neujahrs-Sonderziehung
			if (Verlosung(strLosnummer, 1, false)) intBarwert += MerkeGewinn("Insel", 3000000);
			// Jackpot
			if (GenerateRandomNumber(0, 2) == 0)
			{
				if (Verlosung(strLosnummer, 1, false)) intBarwert += MerkeGewinn("Jackpot " + Jackpot, Jackpot);
				Jackpot = 0;
			}
			return intBarwert;
		}

		static int Klasse5(string strLosnummer)
		{
			Jackpot += 2000000;

			var intBarwert = 0;
			// 12 x 1 Mio.
			for (var i = 0; i < 12; i++)
			{
				if (Verlosung(strLosnummer, 1)) intBarwert += MerkeGewinn("1M", 1000000);
			}
			// 12 x 100 x 10.000 €
			for (var i = 0; i < 12; i++)
			{
				if (Verlosung(strLosnummer, 100)) intBarwert += MerkeGewinn("10k", 10000);
			}
			// Hauptziehung 1
			if (Verlosung(strLosnummer, 300000)) intBarwert += MerkeGewinn("160", 160);
			else if (Verlosung(strLosnummer, 900, true, false)) intBarwert += MerkeGewinn("1k", 1000);
			else if (Verlosung(strLosnummer, 60, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			else if (Verlosung(strLosnummer, 30, true, false)) intBarwert += MerkeGewinn("100k", 100000);
			else if (Verlosung(strLosnummer, 2, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			// Hauptziehung 2
			if (Verlosung(strLosnummer, 6000)) intBarwert += MerkeGewinn("160", 160);
			else if (Verlosung(strLosnummer, 900, true, false)) intBarwert += MerkeGewinn("1k", 1000);
			else if (Verlosung(strLosnummer, 60, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			else if (Verlosung(strLosnummer, 30, true, false)) intBarwert += MerkeGewinn("100k", 100000);
			else if (Verlosung(strLosnummer, 1, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			// Hauptziehung 3
			if (Verlosung(strLosnummer, 6000)) intBarwert += MerkeGewinn("160", 160);
			else if (Verlosung(strLosnummer, 900, true, false)) intBarwert += MerkeGewinn("1k", 1000);
			else if (Verlosung(strLosnummer, 60, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			else if (Verlosung(strLosnummer, 5, true, false)) intBarwert += MerkeGewinn("100k", 100000);
			else if (Verlosung(strLosnummer, 1, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			// Hauptziehung 4
			if (Verlosung(strLosnummer, 3000)) intBarwert += MerkeGewinn("160", 160);
			else if (Verlosung(strLosnummer, 900, true, false)) intBarwert += MerkeGewinn("1k", 1000);
			else if (Verlosung(strLosnummer, 40, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			else if (Verlosung(strLosnummer, 5, true, false)) intBarwert += MerkeGewinn("100k", 100000);
			else if (Verlosung(strLosnummer, 1, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			// Auto
			if (Verlosung(strLosnummer, 60, false)) intBarwert += MerkeGewinn("Mercedes-AMG GT", 138000);
			// Extra-Lohn
			if (Verlosung(strLosnummer, 60, false)) intBarwert += MerkeGewinn("EL-3000", 36000);
			// Millionen-Joker
			if (MillionenJoker && Verlosung(strLosnummer, 50, false)) intBarwert += MerkeGewinn("1M", 1000000);
			// Renten-Joker
			if (RentenJoker && Verlosung(strLosnummer, 1, false)) intBarwert += MerkeGewinn("SR-5000", (5000 * 120));
			else if (RentenJoker && Verlosung(strLosnummer, 4, false, false)) intBarwert += MerkeGewinn("SR-3000", (3000 * 120));
			else if (RentenJoker && Verlosung(strLosnummer, 45, false, false)) intBarwert += MerkeGewinn("SR-1000", (1000 * 120));
			// Jackpot
			if (GenerateRandomNumber(0, 2) == 0)
			{
				if (Verlosung(strLosnummer, 1, false)) intBarwert += MerkeGewinn("Jackpot " + Jackpot, Jackpot);
				Jackpot = 0;
			}
			return intBarwert;
		}

		static int Klasse6(string strLosnummer)
		{
			Jackpot += 5000000;

			var intBarwert = 0;
			// Woche 1
			if (Verlosung(strLosnummer, 100)) intBarwert += MerkeGewinn("10k", 10000);
			else if (Verlosung(strLosnummer, 5, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			else if (Verlosung(strLosnummer, 30, true, false)) intBarwert += MerkeGewinn("100k", 100000);
			else if (Verlosung(strLosnummer, 60, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			else if (Verlosung(strLosnummer, 780000, true, false)) intBarwert += MerkeGewinn("960", 960);
			else if (Verlosung(strLosnummer, 15, true, false)) intBarwert += MerkeGewinn("Haus", 450000);
			else if (Verlosung(strLosnummer, 1, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			else if (Verlosung(strLosnummer, 100, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			else if (Verlosung(strLosnummer, 1, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			else if (Verlosung(strLosnummer, 100, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			else if (Verlosung(strLosnummer, 1, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			else if (Verlosung(strLosnummer, 100, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			// Woche 2
			else if (Verlosung(strLosnummer, 5, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			else if (Verlosung(strLosnummer, 30, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			else if (Verlosung(strLosnummer, 72000, true, false)) intBarwert += MerkeGewinn("960", 960);
			else if (Verlosung(strLosnummer, 100, true, false)) intBarwert += MerkeGewinn("EL-3000", 36000);
			else if (Verlosung(strLosnummer, 1, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			else if (Verlosung(strLosnummer, 100, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			else if (Verlosung(strLosnummer, 1, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			else if (Verlosung(strLosnummer, 100, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			else if (Verlosung(strLosnummer, 1, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			else if (Verlosung(strLosnummer, 100, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			// Woche 3
			else if (Verlosung(strLosnummer, 10, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			else if (Verlosung(strLosnummer, 30, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			else if (Verlosung(strLosnummer, 24000, true, false)) intBarwert += MerkeGewinn("960", 960);
			else if (Verlosung(strLosnummer, 1, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			else if (Verlosung(strLosnummer, 100, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			else if (Verlosung(strLosnummer, 1, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			else if (Verlosung(strLosnummer, 100, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			else if (Verlosung(strLosnummer, 1, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			else if (Verlosung(strLosnummer, 100, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			// Woche 4
			else if (Verlosung(strLosnummer, 20, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			else if (Verlosung(strLosnummer, 30, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			else if (Verlosung(strLosnummer, 12000, true, false)) intBarwert += MerkeGewinn("960", 960);
			else if (Verlosung(strLosnummer, 1, true, false)) intBarwert += MerkeGewinn("Jackpott " + Jackpot, Jackpot);
			else if (Verlosung(strLosnummer, 100, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			else if (Verlosung(strLosnummer, 1, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			else if (Verlosung(strLosnummer, 100, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			else if (Verlosung(strLosnummer, 1, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			else if (Verlosung(strLosnummer, 100, true, false)) intBarwert += MerkeGewinn("10k", 10000);
			else if (Verlosung(strLosnummer, 30, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			else if (Verlosung(strLosnummer, 1, true, false)) intBarwert += MerkeGewinn("1M", 1000000);
			// Millionen-Joker
			if (MillionenJoker && Verlosung(strLosnummer, 100)) intBarwert += MerkeGewinn("1M", 1000000);
			// Renten-Joker
			if (RentenJoker && Verlosung(strLosnummer, 1, false)) intBarwert += MerkeGewinn("SR-5000", (5000 * 120));
			else if (RentenJoker && Verlosung(strLosnummer, 4, false)) intBarwert += MerkeGewinn("SR-3000", (3000 * 120));
			else if (RentenJoker && Verlosung(strLosnummer, 45, false)) intBarwert += MerkeGewinn("SR-1000", (1000 * 120));

			return intBarwert;
		}

		private static bool Verlosung(string strLosnummer, int intAnzahlZiehungen = 1, bool bWithPartial = true, bool bGelosteLeeren = true)
		{
			var strGelost = "";
			if (bGelosteLeeren) Geloste.Clear();

			if (Losanteil < 1 && !bWithPartial) 
                if (GenerateRandomNumber(0, 15) < (Losanteil * 16)) return false;

			// Ziehung eine Endziffer für Ermittlung von 300.000 Gewinnerlose
			while ((intAnzahlZiehungen - 300000) >= 0)
			{
				do
				{
					strGelost = Convert.ToString(GenerateRandomNumber(0, 9));
				} while (Geloste.Any(strGelost.EndsWith));

				Geloste.Add(strGelost);
				intAnzahlZiehungen -= 300000;

				if (strLosnummer.EndsWith(strGelost)) return true;
			}

			// Ziehung zwei Endziffern für Ermittlung von 30.000 Gewinnerlose
			while ((intAnzahlZiehungen - 30000) >= 0)
			{
				do
				{
					strGelost = Convert.ToString(GenerateRandomNumber(0, 99)).PadLeft(2, '0');
				} while (Geloste.Any(strGelost.EndsWith));

				Geloste.Add(strGelost);
				intAnzahlZiehungen -= 30000;
			}

			// Ziehung drei Endziffern für Ermittlung von 3.000 Gewinnerlose
			while ((intAnzahlZiehungen - 3000) >= 0)
			{
				do
				{
					strGelost = Convert.ToString(GenerateRandomNumber(0, 999)).PadLeft(3, '0');
				} while (Geloste.Any(strGelost.EndsWith));

				Geloste.Add(strGelost);
				intAnzahlZiehungen -= 3000;
			}

			// Ziehung vier Endziffern für Ermittlung von 300 Gewinnerlose
			while ((intAnzahlZiehungen - 300) >= 0)
			{
				do
				{
					strGelost = Convert.ToString(GenerateRandomNumber(0, 9999)).PadLeft(4, '0');
				} while (Geloste.Any(strGelost.EndsWith));

				Geloste.Add(strGelost);
				intAnzahlZiehungen -= 300;
			}

			// Ziehung fünf Endziffern für Ermittlung von 30 Gewinnerlose
			while ((intAnzahlZiehungen - 30) >= 0)
			{
				do
				{
					strGelost = Convert.ToString(GenerateRandomNumber(0, 99999)).PadLeft(5, '0');
				} while (Geloste.Any(strGelost.EndsWith));

				Geloste.Add(strGelost);
				intAnzahlZiehungen -= 30;
			}

			// Noch offene Ziehungen werden über volle Losnummern durchgefürt
			while (intAnzahlZiehungen-- > 0)
			{
				do
				{
					strGelost = Convert.ToString(GenerateRandomNumber(0, 3000000)).PadLeft(7, '0');
				} while (Geloste.Any(strGelost.EndsWith));

				Geloste.Add(strGelost);
			}
			return Geloste.Any(strLosnummer.EndsWith);
		}

		private static readonly RNGCryptoServiceProvider CryptoProvider = new RNGCryptoServiceProvider();
		static long GenerateRandomNumber(int min, int max)
		{
			// Ein integer benötigt 4 Byte
			var randomNumber = new byte[4];
			// dann füllen wir den Array mit zufälligen Bytes
			CryptoProvider.GetBytes(randomNumber);
			// schließlich wandeln wir den Byte-Array in einen Integer um
			// da bis jetzt noch keine Begrenzung der Zahlen vorgenommen wurde,
			// wird diese Begrenzung mit einer einfachen Modulo-Rechnung hinzu-
			// gefügt
			var x = BitConverter.ToInt32(randomNumber, 0);
			return (x == Int32.MinValue ? (x * -1) : Math.Abs(x)) % max + min;
		}
    }
}
