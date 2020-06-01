using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MOS_Management.Wpf.Utilitaire
{
    public class GestionGrille
    {
        public void GridVisibilty(Grid g, int pos)
        {
            int nb = g.Children.Count;
            g.Children[pos].Visibility = Visibility.Visible;
            for (int i = 0; i < pos; ++i)
            {
                g.Children[i].Visibility = Visibility.Hidden;
            }
            for (int i = pos + 1; i < nb; ++i)
            {
                g.Children[i].Visibility = Visibility.Hidden;
            }
        }



        public List<Grid> GetGrid(Grid grid)
        {
            List<Grid> listes = new List<Grid>();
            foreach (Grid g in grid.Children)
            {
                listes.Add(g);
            }
            return listes;
        }

        public int GetIndexGrille(Grid grid)
        {
            //Recherche de la grille qui est visible
            int i = -1;
            foreach (Grid g in grid.Children)
            {
                if (g.IsVisible)
                {
                    i = grid.Children.IndexOf(g);
                }
            }
            return i;
        }

        public List<TextBox> GetTextBox(Grid grid)
        {
            List<TextBox> listes = new List<TextBox>();
            foreach (Control c in grid.Children)
            {
                if (c is TextBox)
                {
                    if (c.BorderBrush.ToString() == "#FFFFFF00")
                    {
                        TextBox tb = (TextBox)c;
                        listes.Add(tb);
                    }
                }
            }
            return listes;
        }
    }
}
