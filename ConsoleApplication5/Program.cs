using System;
using System.Collections.Generic;

namespace ConsoleApplication5
{

    public class Program
    {
        static Program p = new Program();
        static AddGraf Add = new AddGraf();
        static MetodeVirfuri MetsVirf = new MetodeVirfuri();
        static ConvertGraf ConvertIn = new ConvertGraf();


        #region  Delegate for Work

        public delegate void WriteLine(string sir);
        public delegate void WriteLineNewRow();

        public WriteLine Writeln = (str) => Console.WriteLine(str);
        public WriteLine Write = (str) => Console.Write(str);
        public WriteLineNewRow NewRow = () => Console.WriteLine();

        #endregion

        static void Main(string[] args)
        {
            p.MeniuAdaugareGraf();
            Console.ReadKey();
        }

        public void MeniuAdaugareGraf()
        {
            var x = new List<virf>();

            Writeln("Meniu de adaugare a grafului : ");
            Writeln("Adaugare graf existent 1 model - 1 ");
            Writeln("Adaugare graf existent 2 model - 2 ");
            Writeln("Adaugare graf prin Matrice de incidenta - 3 ");
            Writeln("Adaugare graf prin Matrice de adiacenta - 4 ");
            Writeln("Adaugare graf prin Lista de adiacenta - 5 ");
            int k = Convert.ToInt32(Console.ReadLine());
            switch (k)
            {
                case 1:
                    x = Add.AddCreatedGraf1();
                    break;
                case 2:
                    x = Add.AddCreatedGraf2();
                    break;
                case 3:
                    x = Add.AddGrafMatriceIncident();
                    break;
                case 4:
                    x = Add.AddGrafMatriceAdiacent();
                    break;
                case 5:
                    x = Add.AddGrafList();
                    break;
            }
            MetsVirf.AfisareVirfuri(x);

        CicluMenuConvert:
            ConvertIn.MeniuConvertireGraf(x);
            NewRow();
            x = MetsVirf.MeniuAddAndOrEdit(x);

            Writeln("Continuati? da/nu");
            if (Convert.ToString(Console.ReadLine()) == "da" || Convert.ToString(Console.ReadLine()) == "Da" || Convert.ToString(Console.ReadLine()) == "DA")
            {
                goto CicluMenuConvert;
            }
            Environment.Exit(0);            
        }
    }
}
