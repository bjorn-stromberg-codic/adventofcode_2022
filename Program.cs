﻿using System.Reflection;
using System.Text.RegularExpressions;

var solutions = typeof(Solution)
    .GetFields(BindingFlags.Static | BindingFlags.Public)
    .Select(field => (
        day: int.Parse(Regex.Match(field.Name, @"(\d+)$").Value),
        _: field.GetValue(null) as Solution.Day
    ))
    .OrderBy(one => one.day);

foreach (var (day, solution) in solutions)
{
    var text = File.ReadAllText($"input/day{day}.txt");

    var (first, second) = solution!(text.Split("\n"));

    Console.WriteLine($"Day {day} answers: {first}, {second}");
}