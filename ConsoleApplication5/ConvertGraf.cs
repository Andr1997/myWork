using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApplication5
{
    public class ConvertGraf
    {
        static Program p = new Program();
        static MetodeVirfuri MetsV = new MetodeVirfuri();

        //Meniul de convertire a grafului din starea initiala in starea dorita 
        public void MeniuConvertireGraf(List<virf> _listGraf)
        {
            p.Writeln("Convertire graf in forma de : ");
            p.Writeln("Matrice de incidenta - 1 ");
            p.Writeln("Matrice de adiacenta - 2 ");
            p.Writeln("Lista de adiacenta - 3 ");
            p.Writeln("Mergem la editare - 4 ");

            int k = Convert.ToInt32(Console.ReadLine());

            switch (k)
            {
                case 1:
                    ConvertToMatriceIncidenta(_listGraf);
                    p.NewRow();
                    p.Writeln("Graful a fost pastrat in fisier!");
                    break;
                case 2:
                    ConvertToMatriceAdiacenta(_listGraf);
                    p.NewRow();
                    p.Writeln("Graful a fost pastrat in fisier!");
                    break;
                case 3:
                    ConvertToListAdiacent(_listGraf);
                    p.NewRow();
                    p.Writeln("Graful a fost pastrat in fisier!");
                    break;
                case 4:
                    MetsV.MeniuAddAndOrEdit(_listGraf);
                    break;

            }
            p.NewRow();
            p.Writeln("Graful a fost pastrat in fisier!");
        }

        //Convert to Matrice de incidenta
        public void ConvertToMatriceIncidenta(List<virf> _listGraf)
        {
            string path = @"C:\Users\Andrian\Desktop\grafuri universitate\matriceIncidenta.txt";

            using (StreamWriter sw = File.CreateText(path))
            {
                p.Writeln("Afisarea grafului in forma Matricei de Incidenta");
                sw.WriteLine("Afisarea grafului in forma Matricei de Incidenta");
                int n = _listGraf.Count;
                List<int> temporarItem = new List<int>();
                for (int z = 1; z <= n; z++) { p.Write("x" + z + " "); sw.Write("x" + z + " "); }
                p.NewRow();
                sw.WriteLine();
                foreach (var itemGraf in _listGraf)
                {
                    List<Int32> temporarList = new List<Int32>(itemGraf.Brate);
                    for (int j = 0; j < itemGraf.Brate.Count; j++)
                    {
                        temporarItem.Add(temporarList.First());
                        for (int i = 1; i <= n; i++)
                        {
                            if (i == itemGraf.Nume && temporarItem.Contains(i))
                            {
                                p.Write(" 2 ");
                                sw.Write(" 2 ");
                                temporarItem.Clear();
                                temporarList.RemoveAt(0);
                            }
                            else if (i == itemGraf.Nume)
                            {
                                p.Write("-1 ");
                                sw.Write("-1 ");
                            }
                            else if (i != itemGraf.Nume && temporarItem.Contains(i))
                            {
                                p.Write(" 1 ");
                                sw.Write(" 1 ");
                                temporarItem.Clear();
                                temporarList.RemoveAt(0);
                            }
                            else
                            {
                                p.Write(" 0 ");
                                sw.Write(" 0 ");
                            }
                        }
                        p.NewRow();
                        sw.WriteLine();
                    }

                }
            }

        }

        //Convert to Matrice de adiacenta
        public void ConvertToMatriceAdiacenta(List<virf> _listGraf)
        {
            string path = @"C:\Users\Andrian\Desktop\grafuri universitate\matriceAdiacenta.txt";
            using (StreamWriter sw = File.CreateText(path))
            {
                p.Writeln("Afisarea grafului in forma Matricei de adiacenta");
                p.NewRow();
                p.Write("  ");
                sw.WriteLine("Afisarea grafului in forma Matricei de adiacenta");
                sw.WriteLine();
                sw.Write("  ");

                for (int i = 1; i <= _listGraf.Count; i++)
                {
                    p.Write(" x" + i);
                    sw.Write(" x" + i);
                }
                p.NewRow();
                sw.WriteLine();
                foreach (var itemGraf in _listGraf)
                {
                    p.Write("x" + itemGraf.Nume + " ");
                    sw.Write("x" + itemGraf.Nume + " ");

                    for (int i = 1; i <= _listGraf.Count; i++)
                    {
                        if (itemGraf.Brate.Contains(i)) { p.Write("1  "); sw.Write("1  "); }
                        else { p.Write("0  "); sw.Write("0  "); }
                    }
                    p.NewRow();
                    sw.WriteLine();
                }
            }
        }

        //Convert to Matrice de incidenta
        public void ConvertToListAdiacent(List<virf> _listGraf)
        {
            string path = @"C:\Users\Andrian\Desktop\grafuri universitate\listaAdiacenta.txt";
            using (StreamWriter sw = File.CreateText(path))
            {
                p.Writeln("Afisarea grafului in forma Listei de adiacenta");
                sw.WriteLine("Afisarea grafului in forma Listei de adiacenta");
                foreach (var item in _listGraf)
                {
                    string brate = string.Join("_", item.Brate.ToArray());
                    string res = brate.Length > 2 ? item.Nume + " | " + brate + "_0" : item.Nume + " | 0";
                    p.Writeln(res);
                    sw.WriteLine(res);
                }
            }
        }
    }
}
