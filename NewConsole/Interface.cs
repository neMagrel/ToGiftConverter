
using System;
using Terminal.Gui;
using System.Text.Json;
using System.IO;

class NotePades
{
    private static JsonAnalys json = new JsonAnalys();
    private static Gift gift = new Gift();

    static ColorScheme customColorScheme = new ColorScheme()
    {
        Normal = Application.Driver.MakeAttribute(Color.Black, Color.BrightYellow),
        Focus = Application.Driver.MakeAttribute(Color.Black, Color.White),
        HotNormal = Application.Driver.MakeAttribute(Color.Black, Color.BrightYellow),
        HotFocus = Application.Driver.MakeAttribute(Color.Black, Color.White)
    };

    static void Main()
    {
        Application.Init();

        var top = Application.Top;
        var mainWin = new Window("Конвектор")
        {
            X = 0,
            Y = 1,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            ColorScheme = customColorScheme
        };
        top.Add(mainWin);


        var label = new Label("Добро пожаловать в конвектор!")
        {
            X = Pos.Center(),
            Y = Pos.Center() - 2
        };
        mainWin.Add(label);

        var button = new Button("Перейти к конвертированию")
        {
            X = Pos.Center(),
            Y = Pos.Center()
        };
        button.Clicked += () =>
        {
            OpenСonvertFile(top, mainWin);
        };
        mainWin.Add(button);

        Application.Run();
    }
    static void OpenСonvertFile(Toplevel top, Window parentWindow)
    {
        var convertWin = new Window("Конвектор")
        {
            X = 0,
            Y = 1,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            ColorScheme = customColorScheme
        };

        var convertButton = new Button("Конвертировать")
        {
            X = Pos.Center(),
            Y = Pos.Center()
        };
        convertButton.Clicked += () =>
        {
            var dialog = new FileDialog("Выберите файл", "", "", "");
            Application.Run(dialog);

            string currentDirectory = Path.GetFileName(Directory.GetCurrentDirectory());
            string filePath = dialog.FilePath.ToString();
            string fileName = Path.GetFileName(filePath);

            if (fileName == currentDirectory)
            {
                return;
            }

            if (Path.GetExtension(filePath).ToLower() != ".json")
            {
                MessageBox.ErrorQuery("Ошибка", "Выбранный файл не является файлом JSON.", "ОК");
                return;
            }

            string jsonString = "";
            try
            {
                // Читаем содержимое файла
                jsonString = File.ReadAllText(filePath);

                ConverterMethod(jsonString);
                string name = GetNameGift(jsonString);
                if (File.Exists(name))
                {
                    MessageBox.Query("Успешно!", $"Файл успешно конвертирован в {name}", "ОК");
                }
            }
            catch (JsonException ex)
            {
                MessageBox.ErrorQuery("Ошибка", $"Ошибка при парсинге JSON: {ex.Message}", "ОК");
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.ErrorQuery("Ошибка доступа", ex.Message, "ОК");
            }
            catch (Exception ex)
            {
                MessageBox.ErrorQuery("Ошибка", $"Произошла ошибка: {ex.Message}", "ОК");
            }

        };
        convertWin.Add(convertButton);

        top.Remove(parentWindow);
        top.Add(convertWin);
        Application.Refresh();
    }

    private static void ConverterMethod(string jsonString)
    {
        gift.MethodGift(json.MethodJson(jsonString));
    }

    private static string GetNameGift(string jsonSrings)
    {
        string name = gift.MethodGift(json.MethodJson(jsonSrings));
        return name;
    }
}
