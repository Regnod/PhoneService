using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPhoneServiceWeb.Utils
{
    public class DataFilter<TClass>
    {
        public static IEnumerable<TClass> Filter(int? id, Func<TClass, int> member, IEnumerable<TClass> items)
        {
            if (id == -1)
                return items;

            List<TClass> filteredItems = new List<TClass>();

            foreach (var item in items)
            {
                var f = member(item);
                if (f == id && !filteredItems.Contains(item))
                    filteredItems.Add(item);
            }

            return filteredItems.ToList();
        }
        public static IEnumerable<TClass> Filter(float? id, Func<TClass, float> member, IEnumerable<TClass> items)
        {
            if (id == -1)
                return items;

            List<TClass> filteredItems = new List<TClass>();

            foreach (var item in items)
            {
                var f = member(item);
                if (f == id && !filteredItems.Contains(item))
                    filteredItems.Add(item);
            }

            return filteredItems.ToList();
        }
        public static IEnumerable<TClass> Filter(string id, Func<TClass, bool> member, IEnumerable<TClass> items)
        {
            if (id == null)
                return items;

            List<TClass> filteredItems = new List<TClass>();

            foreach (var item in items)
            {
                var f = member(item);
                if (f && !filteredItems.Contains(item))
                    filteredItems.Add(item);
            }

            return filteredItems.ToList();
        }
        public static IEnumerable<TClass> Filter(string id, Func<TClass, string> member, IEnumerable<TClass> items)
        {
            if (id == null)
                return items;
            List<TClass> filteredItems = new List<TClass>();

            foreach (var item in items)
            {
                var f = member(item);
                f = f.ToLower();
                if (f.Contains(id) && !filteredItems.Contains(item))
                    filteredItems.Add(item);
            }

            return filteredItems.ToList();
        }
        public static IEnumerable<TClass> Filter(List<int> ids, Func<TClass, int> member, IEnumerable<TClass> items)
        {
            if (ids.Count == 0)
                return items;

            List<TClass> filteredItems = new List<TClass>();

            foreach (var item in items)
            {
                var f = member(item);
                if (ids.Contains(f) && !filteredItems.Contains(item))
                    filteredItems.Add(item);
            }

            return filteredItems.ToList();
        }
        public static IEnumerable<TClass> Filter(List<string> ids, Func<TClass, string> member, IEnumerable<TClass> items, bool toLower = false)
        {
            if (ids.Count == 0)
                return items;
            List<TClass> filteredItems = new List<TClass>();

            if (toLower)
            {
                var lowered = new List<string>();
                foreach (var item in ids)
                {
                    var l = item.ToLower();
                    lowered.Add(l);
                }
                ids = lowered;
            }

            foreach (var item in items)
            {
                var f = member(item);
                f = toLower ? f.ToLower() : f;
                foreach (var it in ids)
                {
                    if (f.Contains(it) && !filteredItems.Contains(item))
                        filteredItems.Add(item);
                }
                //if (ids.Contains(f))
                //    filteredItems.Add(item);
            }

            return filteredItems.ToList();
        }
        public static IEnumerable<TClass> Filter(int min, int max, Func<TClass, int> member, IEnumerable<TClass> items)
        {
            List<TClass> filteredItems = new List<TClass>();
            if (min == -1)
                return Filter(0, max, member, items);
            if (max == -1)
                return Filter(min, int.MaxValue, member, items);

            foreach (var item in items)
            {
                var f = member(item);
                if (min <= f && f <= max && !filteredItems.Contains(item))
                    filteredItems.Add(item);
            }

            return filteredItems.ToList();
        }
        public static IEnumerable<TClass> Filter(float min, float max, Func<TClass, float> member, IEnumerable<TClass> items)
        {
            List<TClass> filteredItems = new List<TClass>();
            if (min == -1)
                return Filter(0, max, member, items);
            if (max == -1)
                return Filter(min, int.MaxValue, member, items);

            foreach (var item in items)
            {
                var f = member(item);
                if (min <= f && f <= max && !filteredItems.Contains(item))
                    filteredItems.Add(item);
            }

            return filteredItems.ToList();
        }
    }
}
