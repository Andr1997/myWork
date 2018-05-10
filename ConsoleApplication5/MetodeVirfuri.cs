﻿using System;
using System.Collections.Generic;

namespace ConsoleApplication5
{
    class MetodeVirfuri
    {
        Program p = new Program();
        //Meniul de Editare a grafului 
        public List<virf> MeniuAddAndOrEdit(List<virf> x)
        {
            p.Writeln("Meniu de adaugare sau editare a unui virf : ");
            p.Writeln("Adaugare virf - 1 ");
            p.Writeln("Editare virf - 2 ");
            p.Writeln("Stergere Varf - 3 ");

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

            p.Writeln("Introduceti numarul virfului : ");
            nume = Convert.ToInt32(Console.ReadLine());
            p.Writeln("Dati bratele separate printr-un singur spatiu!");
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
            p.Writeln("Dati varful grafului ce va fi editat :");
            int n = Convert.ToInt32(Console.ReadLine());
            p.Writeln("Dati bratele separate printr-un singur spatiu!");
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
            p.Writeln("Dati virful care vreti ca sa fie sters : ");
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
                p.Write("Graful [" + item.Nume + "] cu bratele spre : ");
                foreach (var itembrat in item.Brate)
                {
                    p.Write(itembrat + " ");
                }
                p.NewRow();
            }
            p.NewRow();
            p.NewRow();
        }
    }
}
