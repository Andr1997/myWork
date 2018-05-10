using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApplication5
{
    class Program
    {
        public class graf
        {
            public int Nume { get; set; }
            public List<int> Brate { get; set; }
        }

        static void Main(string[] args)
        {
            MeniuAdaugareGraf();
            Console.ReadKey();
        }

        static void MeniuAdaugareGraf()
        {
            var x = new List<graf>();
            Console.WriteLine("Meniu de adaugare a grafului : ");
            Console.WriteLine("Adaugare graf existent 1 model - 1 ");
            Console.WriteLine("Adaugare graf existent 2 model - 2 ");
            Console.WriteLine("Adaugare graf prin Matrice de incidenta - 3 ");
            Console.WriteLine("Adaugare graf prin Matrice de adiacenta - 4 ");
            Console.WriteLine("Adaugare graf prin Lista de adiacenta - 5 ");
            int k = Convert.ToInt32(Console.ReadLine());
            switch (k)
            {
                case 1:
                    x = AddCreatedGraf1();
                    break;
                case 2:
                    x = AddCreatedGraf2();
                    break;
                case 3:
                    x = AddGrafMatriceIncident();
                    break;
                case 4:
                    x = AddGrafMatriceAdiacent();
                    break;
                case 5:
                    x = AddGrafList();
                    break;
            }
            AfisareVirfuri(x);

        CicluMenuConvert:
            MeniuConvertireGraf(x);
            Console.WriteLine();
            x = MeniuAddAndOrEdit(x);
            AfisareVirfuri(x);

            Console.WriteLine("Continuati? da/nu");
            if (Convert.ToString(Console.ReadLine()) == "da")
            {
                goto CicluMenuConvert;
            }
            
            Environment.Exit(0);

        }


        #region ConvertireGraf

        //Meniul de convertire a grafului din starea initiala in starea dorita 
        static void MeniuConvertireGraf(List<graf> _listGraf)
        {
            Console.WriteLine("Convertire graf in forma de : ");
            Console.WriteLine("Matrice de incidenta - 1 ");
            Console.WriteLine("Matrice de adiacenta - 2 ");
            Console.WriteLine("Lista de adiacenta - 3 ");

            int k = Convert.ToInt32(Console.ReadLine());

            switch (k)
            {
                case 1:
                    ConvertToMatriceIncidenta(_listGraf);
                    break;
                case 2:
                    ConvertToMatriceAdiacenta(_listGraf);
                    break;
                case 3:
                    ConvertToListAdiacent(_listGraf);
                    break;
            }
            Console.WriteLine();
            Console.WriteLine("Graful a fost pastrat in fisier!");
        }

        //Convert to Matrice de incidenta
        static void ConvertToMatriceIncidenta(List<graf> _listGraf)
        {
            string path = @"C:\Users\Andrian\Desktop\grafuri universitate\matriceIncidenta.txt";

            using (StreamWriter sw = File.CreateText(path))
            {
                Console.WriteLine("Afisarea grafului in forma Matricei de Incidenta");
                sw.WriteLine("Afisarea grafului in forma Matricei de Incidenta");
                int n = _listGraf.Count;
                List<int> temporarItem = new List<int>();
                for (int z = 1; z <= n; z++) { Console.Write("x" + z + " "); sw.Write("x" + z + " "); }
                Console.WriteLine();
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
                                Console.Write(" 2 ");
                                sw.Write(" 2 ");
                                temporarItem.Clear();
                                temporarList.RemoveAt(0);
                            }
                            else if (i == itemGraf.Nume)
                            {
                                Console.Write("-1 ");
                                sw.Write("-1 ");
                            }
                            else if (i != itemGraf.Nume && temporarItem.Contains(i))
                            {
                                Console.Write(" 1 ");
                                sw.Write(" 1 ");
                                temporarItem.Clear();
                                temporarList.RemoveAt(0);
                            }
                            else
                            {
                                Console.Write(" 0 ");
                                sw.Write(" 0 ");
                            }
                        }
                        Console.WriteLine();
                        sw.WriteLine();
                    }

                }
            }

        }

        //Convert to Matrice de adiacenta
        static void ConvertToMatriceAdiacenta(List<graf> _listGraf)
        {
            string path = @"C:\Users\Andrian\Desktop\grafuri universitate\matriceAdiacenta.txt";
            using (StreamWriter sw = File.CreateText(path))
            {
                Console.WriteLine("Afisarea grafului in forma Matricei de adiacenta");
                Console.WriteLine();
                Console.Write("  ");
                sw.WriteLine("Afisarea grafului in forma Matricei de adiacenta");
                sw.WriteLine();
                sw.Write("  ");

                for (int i = 1; i <= _listGraf.Count; i++)
                {
                    Console.Write(" x" + i);
                    sw.Write(" x" + i);
                }
                Console.WriteLine();
                sw.WriteLine();
                foreach (var itemGraf in _listGraf)
                {
                    Console.Write("x" + itemGraf.Nume + " ");
                    sw.Write("x" + itemGraf.Nume + " ");

                    for (int i = 1; i <= _listGraf.Count; i++)
                    {
                        if (itemGraf.Brate.Contains(i)) { Console.Write("1  "); sw.Write("1  "); }
                        else { Console.Write("0  "); sw.Write("0  "); }
                    }
                    Console.WriteLine();
                    sw.WriteLine();
                }
            }
        }

        //Convert to Matrice de incidenta
        static void ConvertToListAdiacent(List<graf> _listGraf)
        {
            string path = @"C:\Users\Andrian\Desktop\grafuri universitate\listaAdiacenta.txt";
            using (StreamWriter sw = File.CreateText(path))
            {
                Console.WriteLine("Afisarea grafului in forma Listei de adiacenta");
                sw.WriteLine("Afisarea grafului in forma Listei de adiacenta");
                foreach (var item in _listGraf)
                {
                    string brate = string.Join("_", item.Brate.ToArray());
                    string res = brate.Length > 2 ? item.Nume + " | " + brate + "_0" : item.Nume + " | 0";
                    Console.WriteLine(res);
                    sw.WriteLine(res);
                }
            }

        }

        #endregion

        #region AddGraf

        //Adaugarea grafului din program
        static List<graf> AddCreatedGraf1()
        {
            var _listGraf = new List<graf>();

            //graf Universitate
            _listGraf.Add(new graf { Nume = 1, Brate = new List<int> { 2, 5 } });
            _listGraf.Add(new graf { Nume = 2, Brate = new List<int> { 1, 3 } });
            _listGraf.Add(new graf { Nume = 3, Brate = new List<int> { 2, 3, 5 } });
            _listGraf.Add(new graf { Nume = 4, Brate = new List<int> { 2, 3 } });
            _listGraf.Add(new graf { Nume = 5, Brate = new List<int> { 1, 4 } });
            _listGraf.Add(new graf { Nume = 6, Brate = new List<int> { } });

            return _listGraf;
        }
        static List<graf> AddCreatedGraf2()
        {
            var _listGraf = new List<graf>();

            //graf Internet
            _listGraf.Add(new graf { Nume = 1, Brate = new List<int> { 2 } });
            _listGraf.Add(new graf { Nume = 2, Brate = new List<int> { 4 } });
            _listGraf.Add(new graf { Nume = 3, Brate = new List<int> { 1, 4 } });
            _listGraf.Add(new graf { Nume = 4, Brate = new List<int> { 1 } });

            return _listGraf;
        }

        //Adaugarea grafului cu ajutorul Matricei de incidenta
        static List<graf> AddGrafMatriceIncident()
        {
            List<graf> grafFinal = new List<graf>();
            int n;
            string coarda = "";
            var listprimvirf = new List<int>();
            Console.WriteLine("Dati numarul virfurilor : ");
            n = Convert.ToInt32(Console.ReadLine());

            List<int>[] tablou = new List<int>[n + 1];
            for (int x = 0; x <= n; x++) { tablou[x] = new List<int>(); }

            Console.WriteLine("Introduceti datele coardelor separate prin spatii,");
            Console.WriteLine("in forma de matrice, ");
            Console.WriteLine("dupa care finisati ciclul cu cuvintul 'stop' ! ");

            int i = 1;

        StartCiclu:
            {
                //Console.WriteLine("Coarda nr : " + i);
                coarda = Convert.ToString(Console.ReadLine());
                coarda = Regex.Replace(coarda, @"\s+", " ");

                if (coarda == "stop" || coarda == "STOP" || coarda == "Stop")
                {
                    goto StopCiclu;
                }

                string[] virfuriarr = coarda.Split(' ');

                foreach (var item in virfuriarr)
                {
                    listprimvirf.Add(Convert.ToInt32(item));
                }

                if (listprimvirf.Contains(2))
                {
                    tablou[listprimvirf.IndexOf(2) + 1].Add(listprimvirf.IndexOf(2) + 1);
                }
                else
                {
                    int inceput = listprimvirf.IndexOf(-1) + 1;
                    int sfarsit = listprimvirf.IndexOf(1) + 1;
                    tablou[inceput].Add(sfarsit);
                }

                listprimvirf.Clear();
                i++;
                goto StartCiclu;
            }

        StopCiclu:
            for (int j = 1; j <= n; j++)
            {
                grafFinal.Add(new graf { Nume = j, Brate = new List<int>(tablou[j]) });
            }
            return grafFinal;
        }

        //Adaugarea grafului cu ajutorul Matricei de adiacenta
        static List<graf> AddGrafMatriceAdiacent()
        {
            List<graf> grafFinal = new List<graf>();
            int n = 0;
            string virfuristring = "";
            var listprimvirf = new List<int>();
            var listfinvirf = new List<int>();
            Console.WriteLine("Dati numarul de virfuri : ");
            n = Convert.ToInt32(Console.ReadLine());
            for (int i = 1; i <= n; i++)
            {
                Console.WriteLine("Dati indicii virfului " + i + " separati prin spatii ");
                virfuristring = Convert.ToString(Console.ReadLine());
                string[] virfuriarr = virfuristring.Split(' ');
                foreach (var virfprim in virfuriarr)
                {
                    listprimvirf.Add(Convert.ToInt32(virfprim));
                }

                for (int j = 0; j < listprimvirf.Count; j++)
                {
                    if (listprimvirf[j] != 0)
                    {
                        listfinvirf.Add(j + 1);
                    }
                }
                grafFinal.Add(new graf { Nume = i, Brate = new List<int>(listfinvirf) });
                listprimvirf.Clear();
                listfinvirf.Clear();
            }
            Console.WriteLine();
            return grafFinal;
        }

        //Adaugarea grafului cu ajutorul Listei de adiacenta
        static List<graf> AddGrafList()
        {
            List<graf> graffinal = new List<graf>();
            int n = 0;
            string virfuristring = "";
            var listfinvirf = new List<int>();
            Console.WriteLine("Dati numarul de virfuri : ");
            n = Convert.ToInt32(Console.ReadLine());
            for (int i = 1; i <= n; i++)
            {
                Console.WriteLine("Dati indicii virfului " + i + " separati prin '_' finisind cu 0 ");
                virfuristring = Convert.ToString(Console.ReadLine());
                string[] virfuriarr = virfuristring.Split('_');
                foreach (var virfprim in virfuriarr)
                {
                    listfinvirf.Add(Convert.ToInt32(virfprim));
                }
                listfinvirf.RemoveAt(listfinvirf.Count - 1);
                graffinal.Add(new graf { Nume = i, Brate = new List<int>(listfinvirf) });
                listfinvirf.Clear();
            }

            return graffinal;
        }

        #endregion

        #region MetodsWithVirfs--xd :) 
        //Meniul de Editare a grafului 
        static List<graf> MeniuAddAndOrEdit(List<graf> x)
        {
            Console.WriteLine("Meniu de adaugare sau editare a unui virf : ");
            Console.WriteLine("Adaugare virf - 1 ");
            Console.WriteLine("Editare virf - 2 ");
            Console.WriteLine("Stergere Varf - 3 ");

            int k = Convert.ToInt32(Console.ReadLine());
            switch (k)
            {
                case 1:
                    AddVirf(x);
                    break;
                case 2:
                    EditVirf(x);
                    break;
                case 3:
                    StergeVirf(x);
                    break;
            }
            return x;
        }

        //Adaugare virf la graf
        static List<graf> AddVirf(List<graf> _listaGrafuri)
        {
            int nume = 0;
            List<int> listprimvirf = new List<int>();

            Console.WriteLine("Introduceti numarul virfului : ");
            nume = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Dati bratele separate printr-un singur spatiu!");
            string brateEdit = Convert.ToString(Console.ReadLine());

            string[] tablist = brateEdit.Split(' ');

            foreach (var item in tablist)
            {
                listprimvirf.Add(Convert.ToInt32(item));
            }

            var virf = new graf { Nume = nume, Brate = new List<int>(listprimvirf) };
            _listaGrafuri.Add(virf);
            return _listaGrafuri;

        }

        //Editare virf la graf
        static List<graf> EditVirf(List<graf> _listGrafuri)
        {
            List<int> listprimvirf = new List<int>();
            Console.WriteLine("Dati varful grafului ce va fi editat :");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Dati bratele separate printr-un singur spatiu!");
            string brateEdit = Convert.ToString(Console.ReadLine());

            string[] tablist = brateEdit.Split(' ');

            foreach (var item in tablist)
            {
                listprimvirf.Add(Convert.ToInt32(item));
            }

            graf newItem = new graf();
            newItem.Nume = n;
            newItem.Brate = listprimvirf;
            _listGrafuri[n - 1] = newItem;

            return _listGrafuri;
        }

        //Stergere virf la graf
        static List<graf> StergeVirf(List<graf> _listaGrafuri)
        {
            Console.WriteLine("Dati virful care vreti ca sa fie sters : ");
            int virf = Convert.ToInt32(Console.ReadLine());
            int aux = 0;
            foreach (var item in _listaGrafuri)
            {
                if (item.Nume == virf)
                {
                    aux = _listaGrafuri.IndexOf(item);
                }
            }
            _listaGrafuri.RemoveAt(aux);
            return _listaGrafuri;
        } 

        //Afisare Virfuri cu brate
        static void AfisareVirfuri(List<graf> _listaGrafuri)
        {
            foreach (var item in _listaGrafuri)
            {
                Console.Write("Graful [" + item.Nume + "] cu bratele spre : ");
                foreach (var itembrat in item.Brate)
                {
                    Console.Write(itembrat + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }

        #endregion
    }
}
