using SQLitePCL;
using DuckLib;

Batteries.Init();

DuckAPI.AddCaseLog(1, "hello");
Console.WriteLine(DuckAPI.GetCaseLogs(1)[0].Text);