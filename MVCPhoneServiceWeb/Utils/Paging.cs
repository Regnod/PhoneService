using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPhoneServiceWeb.Utils
{
    public static class Paging<T>
    {
        public static Tuple<List<T>, int, int, int> Pages(List<T> t_List, string page, int cpage, bool next, bool previous, int pageLength = 20, int topPages = 20)
        {
            List<List<T>> pages = new List<List<T>>(); //lista que contiene las paginas
            List<T> list = new List<T>(); // cada una de estas listas es una pagina
            int i = 0; // current item number
            int j = 0; // pagina
            foreach (var item in t_List)
            {
                if (i == pageLength) // solamente se van a poner 20 por pagina
                {
                    j++;
                    i = 0;
                    pages.Add(list);
                    list = new List<T>();
                }
                list.Add(item);
                i++;
            }
            if (i < pageLength) // esto por si la ultima pagina tiene mmenos de 20 items poner la ultima pagina tambien
            {
                pages.Add(list);
                j++;
            }
            // elegir pagina
            int currentPage = 0; // numero de pagina a devolver en la lista de paginas
            if (page != null)
            {
                currentPage = (Parse.IntTryParse(page) != -1) ? (Parse.IntTryParse(page) - 1 >= j)? j-1: Parse.IntTryParse(page) - 1 : (cpage >= j) ? 0 : cpage;
            }
            else if (next)
                currentPage = (cpage + 1 >= j) ? cpage : cpage + 1;
            else if (previous)
                currentPage = (cpage - 1 < 0) ? 0 : cpage - 1;

            int mult = currentPage / topPages;
            int top;

            if (j % topPages == 0 || j - (mult*topPages) > topPages)
                top = topPages;
            else
                top = j % topPages;

            if (pages.Count != 0)
            {
                return new Tuple<List<T>, int, int, int>(pages[currentPage], top, mult, currentPage);
            }
            else
                return new Tuple<List<T>, int, int, int>(new List<T>(), top, mult, currentPage);
        }
    }
}
