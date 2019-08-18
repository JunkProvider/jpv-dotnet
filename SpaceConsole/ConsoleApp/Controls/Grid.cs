using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SpaceConsole.ConsoleApp.Rendering;

namespace SpaceConsole.ConsoleApp.Controls
{
    public sealed class Grid : Control
    {
        public Action<Grid> UpdateFunc { get; set; }

        private List<List<IControl>> Rows { get; } = new List<List<IControl>>();

        public void AddRow(IEnumerable<IControl> cells)
        {
            Rows.Add(cells.ToList());
        }

        public void AddCell(int x, int y, IControl cell)
        {
            while (Rows.Count <= y)
            {
                Rows.Add(new List<IControl>());
            }

            var cells = Rows[y];

            while (cells.Count <= x)
            {
                cells.Add(new Box());
            }

            cells[x] = cell;
        }

        public void ClearRows()
        {
            Rows.Clear();
        }

        protected override void DoUpdate()
        {
            UpdateFunc?.Invoke(this);

            foreach (var cell in GetCells())
            {
                cell.Update();
            }
        }

        protected override MeasureSize DoMeassure()
        {
            foreach (var cell in GetCells())
            {
                cell.Meassure();
            }

            return new MeasureSize(MeasureWidth(), MeasureHeight());
        }

        protected override void DoArrange()
        {
            var cellSizes = GetCellSizes();

            for (var y = 0; y < Rows.Count; y++)
            {
                var cells = Rows[y];

                for (var x = 0; x < cells.Count; x++)
                {
                    var cell = cells[x];

                    cell.Arrange(MeasureMath.Min(cell.MeasuredSize, cellSizes[x, y]));
                }
            }
        }

        protected override ConsoleBitmap DoRender()
        {
            var cellSizes = GetCellSizes();

            var bitmap = ConsoleBitmap.Empty;

            for (var y = 0; y < Rows.Count; y++)
            {
                var rowBitmap = ConsoleBitmap.Empty;
                var cells = Rows[y];

                for (var x = 0; x < cells.Count; x++)
                {
                    var cell = cells[x];
                    var cellSize = cellSizes[x, y];

                    var cellBitmap = cell.Render()
                        .WithSize(cellSize);

                    rowBitmap = rowBitmap.CombinedWith(rowBitmap.Width, 0, cellBitmap);
                }

                bitmap = bitmap.CombinedWith(0, bitmap.Height, rowBitmap);
            }

            return bitmap;
        }

        private Size[,] GetCellSizes()
        {
            var remainingHeight = ActualSize.Height;

            var columns = GetColumns();

            var rowCount = Rows.Count;
            var columnCount = columns.Count;

            var sizes = new Size[columnCount, rowCount];

            for (var y = 0; y < rowCount; y++)
            {
                var rowHeight = MeasureMath.Min(MeasureRowHeight(Rows[y]), remainingHeight);
                remainingHeight -= rowHeight;

                var remainingWidth = ActualSize.Width;

                for (var x = 0; x < columnCount; x++)
                {
                    var columnWidth = MeasureMath.Min(MeasureColumnWidth(columns[x]), remainingWidth);
                    sizes[x, y] = new Size(columnWidth, rowHeight);
                    remainingWidth -= columnWidth;
                }
            }

            return sizes;
        }

        private Dimension MeasureWidth()
        {
            return GetColumns().Select(MeasureColumnWidth).Sum();
        }

        private Dimension MeasureHeight()
        {
            return Rows.Select(MeasureRowHeight).Sum();
        }

        private static Dimension MeasureRowHeight(IEnumerable<IControl> cells)
        {
            return cells.Select(cell => cell.MeasuredSize.Height).Max();
        }

        private static Dimension MeasureColumnWidth(IEnumerable<IControl> cells)
        {
            return cells.Select(cell => cell.MeasuredSize.Width).Max();
        }

        private IReadOnlyList<IReadOnlyList<IControl>> GetColumns()
        {
            var columns = new List<IControl>[GetColumnCount()];

            for (var x = 0; x < columns.Length; x++)
            {
                columns[x] = new List<IControl>();
            }

            foreach (var cells in Rows)
            {
                for (var columIndex = 0; columIndex < cells.Count; columIndex++)
                {
                    columns[columIndex].Add(cells[columIndex]);
                }
            }

            return columns.ToList();
        }

        private int GetColumnCount()
        {
            return Rows.Count == 0 ? 0 : Rows.Max(r => r.Count);
        }

        private IEnumerable<IControl> GetCells()
        {
            return Rows.SelectMany(row => row);
        }
    }
}
