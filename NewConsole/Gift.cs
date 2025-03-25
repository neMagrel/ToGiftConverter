using System.Collections;
using System.Collections.Generic;
using System.Globalization;

internal class Gift
{
    public string MethodGift(List<Dictionary<string, string>> list)
    {
        string fileName = Convert(list);
        return fileName;
    }
    private static string Convert(List<Dictionary<string, string>> list)
    {
        string fileName = "ФайлВопрос.txt";
        StreamWriter sw = new StreamWriter(fileName, true);
        int i = 0;
        var length = list.Count;
        foreach (var dictionary in list)
        {
            foreach (var key in dictionary) 
            {
                sw.Write($"::Вопрос:: {key.Key} ");
                sw.Write($"{{= {key.Value}}}\n");
            }
        }
        //while (true)
        //{
        //    if (i >= length)
        //    {
        //        break;
        //    }
        //    var xxx = list[i];
        //    sw.Write($"::Вопрос:: {xxx.Key} ");
        //    sw.Write($"{{= {xxx.Value}}}\n");
        //    i++;

        //}
        sw.Close();
        return fileName;
    }
}