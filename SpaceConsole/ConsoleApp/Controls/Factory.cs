using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceConsole.ConsoleApp.Controls
{
    public static class Factory
    {
        public static IControl Headline1(string text)
        {
            return Headline1(() => text);
        }

        public static IControl Headline1(Func<string> textFunc)
        {
            return new Box
            {
                Width = Dimension.Infinite,
                PaddingBottom = 2,
                Content = new Text
                {
                    UpdateFunc = control => control.Value = textFunc(),
                    ForegroundColor = ConsoleColor.Green
                }
            };
        }


        public static IControl Headline2(string text)
        {
            return new Box
            {
                PaddingBottom = 1,
                Content = new Text
                {
                    Value = text,
                    ForegroundColor = ConsoleColor.Green
                }
            };
        }

        public static IControl Table<T>(Func<IEnumerable<T>> itemsFunc, IReadOnlyDictionary<string, Func<T, string>> columns, string emptyText)
        {
            var grid = new Grid
            {
                UpdateFunc = g =>
                {
                    g.ClearRows();

                    g.AddRow(columns.Select(column => Headline2(column.Key + "   ")));

                    var items = itemsFunc().ToList();

                    if (items.Count == 0)
                    {
                        g.AddRow(new List<IControl> {Text(() => emptyText)});
                        return;
                    }

                    foreach (var item in items)
                    {
                        g.AddRow(columns.Select(column => Text(() => column.Value(item) + "   ")));
                    }
                }
            };


            return grid;
        }

        public static IControl DefinitionList(IReadOnlyDictionary<string, Func<string>> rows)
        {
            var grid = new Grid();

            var rowIndex = 0;
            foreach (var row in rows)
            {
                grid.AddCell(0, rowIndex, new Text { ForegroundColor = ConsoleColor.DarkGreen, Value = row.Key });
                grid.AddCell(1, rowIndex, new Text { ForegroundColor = ConsoleColor.DarkGreen, UpdateFunc = text => text.Value = row.Value() });
                rowIndex++;
            }

            return grid;
        }

        public static IControl List(Func<IEnumerable<string>> rowsFunc)
        {
            var grid = new Grid
            {
                UpdateFunc = g =>
                {
                    var rowIndex = 0;
                    foreach (var row in rowsFunc())
                    {
                        g.AddCell(0, rowIndex, new Text {ForegroundColor = ConsoleColor.DarkGreen, Value = row});
                        rowIndex++;
                    }
                }
            };


            return grid;
        }

        public static IControl HorizintalSplit(IControl left, int leftWidth, IControl right)
        {
            var grid = new Grid();
            grid.AddRow(new List<IControl> {
                new Box
                {
                    Width = Dimension.Real(leftWidth),
                    Height = Dimension.Infinite,
                    Content = left
                },
                new Box
                {
                    Width = Dimension.Infinite,
                    Height = Dimension.Infinite,
                    Content = right
                }
            });
            return grid;
        }

        public static IControl Box(Dimension? width = null, Dimension? height = null, int padding = 0, IControl content = null, ConsoleColor? backgroundColor = null)
        {
            return new Box
            {
                Width = width ?? Dimension.Infinite,
                Height = height ?? Dimension.Infinite,
                Padding = padding,
                BackgroundColor = backgroundColor,
                Content = content
            };
        }

        public static IControl StackPanel(IEnumerable<IControl> rows)
        {
            return new StackPanel
            {
                Rows = rows.ToList()
            };
        }

        public static IControl Text(Func<string> textFunc)
        {
            return new Text { UpdateFunc = control => control.Value = textFunc(), ForegroundColor = ConsoleColor.DarkGreen };
        }
    }
}
