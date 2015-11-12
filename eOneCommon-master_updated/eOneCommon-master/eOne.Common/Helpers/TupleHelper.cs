using System;
using System.Collections.Generic;
using System.Linq;

namespace eOne.Common.Helpers
{
    public class TupleHelper
    {

        public static List<Tuple<string, string>> CreateTupleStringList(params string[] items)
        {
            var tupleList = new List<Tuple<string, string>>();
            var firstItem = string.Empty;
            var itemNumber = 1;
            foreach (var item in items)
            {
                if (itemNumber == 1)
                {
                    firstItem = item;
                    itemNumber = 2;
                }
                else
                {
                    var tupleItem = new Tuple<string, string>(firstItem, item);
                    tupleList.Add(tupleItem);
                    itemNumber = 1;
                }
            }
            return tupleList;
        }

        public static List<Tuple<string, string>> AppendTupleStringList(List<Tuple<string, string>> tupleList, params string[] items)
        {
            var newTuples = CreateTupleStringList(items);
            tupleList.AddRange(newTuples);
            return tupleList;
        }

        public static List<string> CreateStringListFromTupleItem(List<Tuple<string, string>> tupleList, int itemNumber)
        {
            return tupleList.Select(tuple => itemNumber == 1 ? tuple.Item1 : tuple.Item2).ToList();
        }

    }
}
