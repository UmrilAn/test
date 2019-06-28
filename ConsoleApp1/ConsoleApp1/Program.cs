using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Fms //couple
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
    class Сouple //два числа: индексы начала и конца закрашиваемого отрезка текста
    {
        public int Number { get; set; }
        public string Beg { get; set; }
        public string End { get; set; }
    }
    public static class Extensions
    {
        public static bool CaseInsensitiveContains(this string text, string value,
            StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase)
        {
            return text.IndexOf(value, stringComparison) >= 0;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var db = new List<string>
            {
                "аааа",
                "ббб",
                "ввв",
                "ввв э",
                "ввв эээ ююю я",
                "ввв ээ ююю я",
                "ввв ыыыы ююю я",
                "ввв ээээ ттт яяя ккк",
                "ввв эээк ююю яяяя",

            };
            var req = "ввв ээээ";
            var resp = intelliFind(req, db);


            //вытащим данные из файла фмс
            var fms_base = new List<Fms>();
            using (var parser = new TextFieldParser(@"d:\fms_unit.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    //Processing row
                    string[] fields = parser.ReadFields();
                    if (fields[0] == "code")
                        continue;
                    fms_base.Add(new Fms { Code = fields[0], Name = fields[1] });
                }
            }

            //проверим список на прохождение валидации поля
            var errList = new List<Fms>();
            var regex = new Regex(@"^[0-9а-яА-ЯёЁ\s\-\.\(\)\;\/\,\?\?\""\'№]+$");

            foreach (var fms in fms_base)
            {
                if (!regex.IsMatch(fms.Name))
                    errList.Add(fms);
            }

            req = "отделом уфмс России по москве би";
            resp = intelliFind(req, fms_base.Select(i => i.Name).ToList());

            Console.ReadKey();
        }
        static List<string> intelliFind(string request, List<string> input)
        {
            int maxRespCount = 5;
            var result = new List<string>();
            var tempList = new List<string>(input);
            //поделим строку на список из слов, поочередно будем отсеивать лишние строки на основе списка
            var searchWords = new List<string>();
            searchWords = request.Split(' ').ToList();

            //цикл по словам
            for (int i = 0; i < searchWords.Count; i++)
            {
                result = tempList.Where(s => s.CaseInsensitiveContains(searchWords[i])).ToList();
                tempList = new List<string>(result);
                result.Clear();
            }
            return tempList.Take(maxRespCount).ToList();
        }

        static List<Сouple> getPositionsForColor(string request, List<string> searchResults)
        {
            var list = new List<Сouple>();
            var searchWords = new List<string>();
            searchWords = request.Split(' ').ToList();
            for (int i = 0; i < searchWords.Count; i++)
            {
                foreach (var r in searchResults)
                {

                }
            }
            return list;
        }
    }
}
