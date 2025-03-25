using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class JsonAnalys
{
    public List<Dictionary<string, string>> MethodJson(string jsonString)
    {
        // Десериализация JSON в JArray
        JArray jsonArray = JsonConvert.DeserializeObject<JArray>(jsonString);

        // Определение типа вопросов
        List<JObject> qaItems = DetermineQuestionType(jsonArray);

        // Обработка вопросов формата "вопрос/ответ"
        List<Dictionary<string, string>> resultList = ProcessQAItems(qaItems);

        return resultList;
    }

    public static List<JObject> DetermineQuestionType(JArray jsonArray)
    {
        List<JObject> qaItems = new List<JObject>();

        foreach (JObject item in jsonArray)
        {
            // Проверка наличия обоих полей "Question" и "Answer"
            if (item["Question"] != null && item["Answer"] != null)
            {
                qaItems.Add(item);
            }
        }

        return qaItems;
    }

    public static List<Dictionary<string, string>> ProcessQAItems(List<JObject> qaItems)
    {
        // Создание списка для хранения словарей "вопрос"/"ответ"
        List<Dictionary<string, string>> resultList = new List<Dictionary<string, string>>();

        // Заполнение списка словарями
        foreach (JObject item in qaItems)
        {
            string question = item["Question"].ToString();
            string answer = item["Answer"].ToString();

            // Создание нового словаря для каждой пары "вопрос"/"ответ"
            Dictionary<string, string> qaDictionary = new Dictionary<string, string>
            {
                { question, answer }
            };

            // Добавление словаря в список
            resultList.Add(qaDictionary);
        }

        return resultList;
    }
}