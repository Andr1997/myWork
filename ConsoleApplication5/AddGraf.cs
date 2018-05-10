using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConsoleApplication5
{
    class AddGraf
    {
        public Program p = new Program();

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
            p.Writeln("Dati numarul virfurilor : ");
            n = Convert.ToInt32(Console.ReadLine());

            List<int>[] tablou = new List<int>[n + 1];
            for (int x = 0; x <= n; x++) { tablou[x] = new List<int>(); }

            p.Writeln("Introduceti datele coardelor separate prin spatii,");
            p.Writeln("in forma de matrice, ");
            p.Writeln("dupa care finisati ciclul cu cuvintul 'stop' ! ");

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
            p.Writeln("Dati numarul de virfuri : ");
            n = Convert.ToInt32(Console.ReadLine());
            for (int i = 1; i <= n; i++)
            {
                p.Writeln("Dati indicii virfului " + i + " separati prin spatii ");
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
            p.NewRow();
            return grafFinal;
        }

        //Adaugarea grafului cu ajutorul Listei de adiacenta
        public List<virf> AddGrafList()
        {
            List<virf> graffinal = new List<virf>();
            int n = 0;
            string virfuristring = "";
            var listfinvirf = new List<int>();
            p.Writeln("Dati numarul de virfuri : ");
            n = Convert.ToInt32(Console.ReadLine());
            for (int i = 1; i <= n; i++)
            {
                p.Writeln("Dati indicii virfului " + i + " separati prin '_' finisind cu 0 ");
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
    }
}
