
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    public static void Main(string[] args)
    {
        // Определяем тестовые случаи: каждый случай содержит список отрезков и ожидаемый результат
        var testCases = new List<(List<(int, int)> segments, int expectedResult)>
        {
            (new List<(int, int)> { (1, 2), (4, 5) }, 2),
            (new List<(int, int)> { (1, 5), (3, 7) }, 6),
            (new List<(int, int)> { (1, 3), (3, 6) }, 5),
            (new List<(int, int)> { (1, 10), (3, 5) }, 9),
            (new List<(int, int)> { (1, 4), (1, 4) }, 3),
            (new List<(int, int)> { (1, 1), (2, 2) }, 0),
            (new List<(int, int)> { (1, 3), (2, 4), (6, 8), (7, 9), (10, 12) }, 8)
        };

        // Тестирование всех случаев
        foreach (var testCase in testCases)
        {
            // Вычисляем суммарную длину тени для текущего набора отрезков
            int result = CalculateTotalShadowLength(testCase.segments);

            // Вывод входных данных и результатов теста
            Console.WriteLine($"Входные данные: {FormatSegments(testCase.segments)}");
            Console.WriteLine($"Ожидаемый результат: {testCase.expectedResult}, Фактический результат: {result}");
            Console.WriteLine(result == testCase.expectedResult ? "Тест пройден" : "Тест не пройден");
            Console.WriteLine();
        }
    }

    // Метод для вычисления суммарной длины тени отрезков
    public static int CalculateTotalShadowLength(List<(int start, int end)> segments)
    {
        // Если список отрезков пустой, возвращаем 0
        if (segments == null || segments.Count == 0)
            return 0;

        // Сортируем отрезки по начальной точке
        segments.Sort((a, b) => a.start.CompareTo(b.start));

        // Список для хранения объединенных отрезков
        List<(int start, int end)> mergedSegments = new List<(int start, int end)>();

        // Инициализируем текущий объединенный отрезок первым отрезком в списке
        (int start, int end) currentSegment = segments[0];

        // Перебираем оставшиеся отрезки
        foreach (var segment in segments.Skip(1))
        {
            // Если текущий отрезок пересекается или смежен с текущим объединенным отрезком, объединяем их
            if (segment.start <= currentSegment.end)
            {
                currentSegment.end = Math.Max(currentSegment.end, segment.end);
            }
            else
            {
                // Если не пересекается, добавляем текущий объединенный отрезок в список и начинаем новый
                mergedSegments.Add(currentSegment);
                currentSegment = segment;
            }
        }

        // Добавляем последний объединенный отрезок в список
        mergedSegments.Add(currentSegment);

        // Вычисляем суммарную длину всех объединенных отрезков
        int totalLength = 0;
        foreach (var mergedSegment in mergedSegments)
        {
            totalLength += mergedSegment.end - mergedSegment.start;
        }

        return totalLength;
    }

    // Вспомогательный метод для форматирования списка отрезков в строку
    public static string FormatSegments(List<(int start, int end)> segments)
    {
        return string.Join(", ", segments.Select(segment => $"({segment.start}, {segment.end})"));
    }
}
