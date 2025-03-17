/* раздели метод на 2 метода:
1 определяет тип вопроса
2 работает только с вопросами вида вопрос/ответ (после 1 метода)
*/
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class JsonAnalys
{
    public List<KeyValuePair<string, string>> MethodJson(string jsonString)
    {
        // Десериализация JSON в JArray
        JArray jsonArray = JsonConvert.DeserializeObject<JArray>(jsonString);

        // Определение типа вопросов
        List<JObject> qaItems = DetermineQuestionType(jsonArray);

        // Обработка вопросов формата "вопрос/ответ"
        List<KeyValuePair<string, string>> resultList = ProcessQAItems(qaItems);

        foreach (var item in resultList)
        {
            Console.WriteLine($"Question: {item.Key}");
            Console.WriteLine($"Answer: {item.Value}");
        }

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

    public static List<KeyValuePair<string, string>> ProcessQAItems(List<JObject> qaItems)
    {
        // Создание словаря для хранения пар "вопрос"/"ответ"
        Dictionary<string, string> qaDictionary = new Dictionary<string, string>();

        // Заполнение словаря
        foreach (JObject item in qaItems)
        {
            string question = item["Question"].ToString();
            string answer = item["Answer"].ToString();

            // Проверка на наличие ключа в словаре
            if (!qaDictionary.ContainsKey(question))
            {
                qaDictionary.Add(question, answer);
            }
            else
            {
                // Если ключ уже существует, обновляем значение
                qaDictionary[question] = answer;
            }
        }

        // Преобразование словаря в список
        List<KeyValuePair<string, string>> resultList = new List<KeyValuePair<string, string>>(qaDictionary);

        return resultList;
    }
}
