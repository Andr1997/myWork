using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConsoleApplication5
{
    public class Program
    {
        #region Class virf and Delegate for Work

        public class virf
        {
            public int Nume { get; set; }
            public List<int> Brate { get; set; }
        }

        public delegate void WriteLine(string sir);
        public delegate void WriteLineNewRow();

        public WriteLine Writeln = (str) => Console.WriteLine(str);
        public WriteLine Write = (str) => Console.Write(str);
        public WriteLineNewRow NewRow = () => Console.WriteLine();

        #endregion

        static void Main(string[] args)
        {
            Program p = new Program();
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
            NewRow();
            x = MeniuAddAndOrEdit(x);
            AfisareVirfuri(x);

            Writeln("Continuati? da/nu");
            if (Convert.ToString(Console.ReadLine()) == "da")
            {
                goto CicluMenuConvert;
            }
            
            Environment.Exit(0);

        }


        #region ConvertireGraf

        //Meniul de convertire a grafului din starea initiala in starea dorita 
        public void MeniuConvertireGraf(List<virf> _listGraf)
        {
            Writeln("Convertire graf in forma de : ");
            Writeln("Matrice de incidenta - 1 ");
            Writeln("Matrice de adiacenta - 2 ");
            Writeln("Lista de adiacenta - 3 ");

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
            NewRow();
            Writeln("Graful a fost pastrat in fisier!");
        }

        //Convert to Matrice de incidenta
        public void ConvertToMatriceIncidenta(List<virf> _listGraf)
        {
            string path = @"C:\Users\Andrian\Desktop\grafuri universitate\matriceIncidenta.txt";

            using (StreamWriter sw = File.CreateText(path))
            {
                Writeln("Afisarea grafului in forma Matricei de Incidenta");
                sw.WriteLine("Afisarea grafului in forma Matricei de Incidenta");
                int n = _listGraf.Count;
                List<int> temporarItem = new List<int>();
                for (int z = 1; z <= n; z++) { Write("x" + z + " "); sw.Write("x" + z + " "); }
                NewRow();
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
                                Write(" 2 ");
                                sw.Write(" 2 ");
                                temporarItem.Clear();
                                temporarList.RemoveAt(0);
                            }
                            else if (i == itemGraf.Nume)
                            {
                                Write("-1 ");
                                sw.Write("-1 ");
                            }
                            else if (i != itemGraf.Nume && temporarItem.Contains(i))
                            {
                                Write(" 1 ");
                                sw.Write(" 1 ");
                                temporarItem.Clear();
                                temporarList.RemoveAt(0);
                            }
                            else
                            {
                                Write(" 0 ");
                                sw.Write(" 0 ");
                            }
                        }
                        NewRow();
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
                Writeln("Afisarea grafului in forma Matricei de adiacenta");
                NewRow();
                Write("  ");
                sw.WriteLine("Afisarea grafului in forma Matricei de adiacenta");
                sw.WriteLine();
                sw.Write("  ");

                for (int i = 1; i <= _listGraf.Count; i++)
                {
                    Write(" x" + i);
                    sw.Write(" x" + i);
                }
                NewRow();
                sw.WriteLine();
                foreach (var itemGraf in _listGraf)
                {
                    Write("x" + itemGraf.Nume + " ");
                    sw.Write("x" + itemGraf.Nume + " ");

                    for (int i = 1; i <= _listGraf.Count; i++)
                    {
                        if (itemGraf.Brate.Contains(i)) { Write("1  "); sw.Write("1  "); }
                        else { Write("0  "); sw.Write("0  "); }
                    }
                    NewRow();
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
                Writeln("Afisarea grafului in forma Listei de adiacenta");
                sw.WriteLine("Afisarea grafului in forma Listei de adiacenta");
                foreach (var item in _listGraf)
                {
                    string brate = string.Join("_", item.Brate.ToArray());
                    string res = brate.Length > 2 ? item.Nume + " | " + brate + "_0" : item.Nume + " | 0";
                    Writeln(res);
                    sw.WriteLine(res);
                }
            }

        }

        #endregion

        #region AddugareGraf

        //Adaugarea grafului din program
        public List<virf> AddCreatedGraf1()
        {
            var _listGraf = new List<virf>();

            //graf Universitate
            _listGraf.Add(new virf { Nume = 1, Brate = new List<int> { 2, 5 } });
            _listGraf.Add(new virf { Nume = 2, Brate = new List<int> { 1, 3 } });
            _listGraf.Add(new virf { Nume = 3, Brate = new List<int> { 2, 3, 5 } });
            _listGraf.Add(new virf { Nume = 4, Brate = new List<int> { 2, 3 } });
            _listGraf.Add(new virf { Nume = 5, Brate = new List<int> { 1, 4 } });
            _listGraf.Add(new virf { Nume = 6, Brate = new List<int> { } });

            return _listGraf;
        }
        public List<virf> AddCreatedGraf2()
        {
            var _listGraf = new List<virf>();

            //graf Internet
            _listGraf.Add(new virf { Nume = 1, Brate = new List<int> { 2 } });
            _listGraf.Add(new virf { Nume = 2, Brate = new List<int> { 4 } });
            _listGraf.Add(new virf { Nume = 3, Brate = new List<int> { 1, 4 } });
            _listGraf.Add(new virf { Nume = 4, Brate = new List<int> { 1 } });

            return _listGraf;
        }

        //Adaugarea grafului cu ajutorul Matricei de incidenta
        public List<virf> AddGrafMatriceIncident()
        {
            List<virf> grafFinal = new List<virf>();
            int n;
            string coarda = "";
            var listprimvirf = new List<int>();
            Writeln("Dati numarul virfurilor : ");
            n = Convert.ToInt32(Console.ReadLine());

            List<int>[] tablou = new List<int>[n + 1];
            for (int x = 0; x <= n; x++) { tablou[x] = new List<int>(); }

            Writeln("Introduceti datele coardelor separate prin spatii,");
            Writeln("in forma de matrice, ");
            Writeln("dupa care finisati ciclul cu cuvintul 'stop' ! ");

            int i = 1;

        StartCiclu:
            {
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
                grafFinal.Add(new virf { Nume = j, Brate = new List<int>(tablou[j]) });
            }
            return grafFinal;
        }

        //Adaugarea grafului cu ajutorul Matricei de adiacenta
        public List<virf> AddGrafMatriceAdiacent()
        {
            List<virf> grafFinal = new List<virf>();
            int n = 0;
            string virfuristring = "";
            var listprimvirf = new List<int>();
            var listfinvirf = new List<int>();
            Writeln("Dati numarul de virfuri : ");
            n = Convert.ToInt32(Console.ReadLine());
            for (int i = 1; i <= n; i++)
            {
                Writeln("Dati indicii virfului " + i + " separati prin spatii ");
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
                grafFinal.Add(new virf { Nume = i, Brate = new List<int>(listfinvirf) });
                listprimvirf.Clear();
                listfinvirf.Clear();
            }
            NewRow();
            return grafFinal;
        }

        //Adaugarea grafului cu ajutorul Listei de adiacenta
        public List<virf> AddGrafList()
        {
            List<virf> graffinal = new List<virf>();
            int n = 0;
            string virfuristring = "";
            var listfinvirf = new List<int>();
            Writeln("Dati numarul de virfuri : ");
            n = Convert.ToInt32(Console.ReadLine());
            for (int i = 1; i <= n; i++)
            {
                Writeln("Dati indicii virfului " + i + " separati prin '_' finisind cu 0 ");
                virfuristring = Convert.ToString(Console.ReadLine());
                string[] virfuriarr = virfuristring.Split('_');
                foreach (var virfprim in virfuriarr)
                {
                    listfinvirf.Add(Convert.ToInt32(virfprim));
                }
                listfinvirf.RemoveAt(listfinvirf.Count - 1);
                graffinal.Add(new virf { Nume = i, Brate = new List<int>(listfinvirf) });
                listfinvirf.Clear();
            }

            return graffinal;
        }

        #endregion

        #region Metode cu virfuri 
        //Meniul de Editare a grafului 
        public List<virf> MeniuAddAndOrEdit(List<virf> x)
        {
            Writeln("Meniu de adaugare sau editare a unui virf : ");
            Writeln("Adaugare virf - 1 ");
            Writeln("Editare virf - 2 ");
            Writeln("Stergere Varf - 3 ");

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
        public List<virf> AddVirf(List<virf> _listaGrafuri)
        {
            int nume = 0;
            List<int> listprimvirf = new List<int>();

            Writeln("Introduceti numarul virfului : ");
            nume = Convert.ToInt32(Console.ReadLine());
            Writeln("Dati bratele separate printr-un singur spatiu!");
            string brateEdit = Convert.ToString(Console.ReadLine());

            string[] tablist = brateEdit.Split(' ');

            foreach (var item in tablist)
            {
                listprimvirf.Add(Convert.ToInt32(item));
            }

            var virf = new virf { Nume = nume, Brate = new List<int>(listprimvirf) };
            _listaGrafuri.Add(virf);
            return _listaGrafuri;

        }

        //Editare virf la graf
        public List<virf> EditVirf(List<virf> _listGrafuri)
        {
            List<int> listprimvirf = new List<int>();
            Writeln("Dati varful grafului ce va fi editat :");
            int n = Convert.ToInt32(Console.ReadLine());
            Writeln("Dati bratele separate printr-un singur spatiu!");
            string brateEdit = Convert.ToString(Console.ReadLine());

            string[] tablist = brateEdit.Split(' ');

            foreach (var item in tablist)
            {
                listprimvirf.Add(Convert.ToInt32(item));
            }

            virf newItem = new virf();
            newItem.Nume = n;
            newItem.Brate = listprimvirf;
            _listGrafuri[n - 1] = newItem;

            return _listGrafuri;
        }

        //Stergere virf la graf
        public List<virf> StergeVirf(List<virf> _listaGrafuri)
        {
            Writeln("Dati virful care vreti ca sa fie sters : ");
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
        public void AfisareVirfuri(List<virf> _listaGrafuri)
        {
            foreach (var item in _listaGrafuri)
            {
                Write("Graful [" + item.Nume + "] cu bratele spre : ");
                foreach (var itembrat in item.Brate)
                {
                    Write(itembrat + " ");
                }
                NewRow();
            }
            NewRow();
            NewRow();
            NewRow();
        }

        #endregion
    }
}
