﻿namespace TacticsX.Grid
{
    public abstract class AbstractGridManager
    {
        public readonly int ROWS;
        public readonly int COLUMNS;
        private readonly int CELL_COUNT;

        private readonly Cell[] CELLS;
        private readonly Node[] NODES;

        public AbstractGridManager(int size)
        {
            ROWS = size;
            COLUMNS = size;
            CELL_COUNT = ROWS * COLUMNS;
            CELLS = new Cell[CELL_COUNT];
            NODES = new Node[CELL_COUNT];
        }

        public AbstractGridManager(int rows,int columns)
        {
            ROWS = rows;
            COLUMNS = columns;
            CELL_COUNT = ROWS * COLUMNS;
            CELLS = new Cell[CELL_COUNT];
            NODES = new Node[CELL_COUNT];
        }

        public void Build()
        {
            int index;
            int rowOffset;

            for (int row = 0; row < ROWS; row++)
            {
                rowOffset = row * COLUMNS;

                for (int column = 0; column < COLUMNS; column++)
                {
                    index = rowOffset + column;

                    Cell n = CreateCell(row, column);
                    n.SetPosition(row, column);
                    CELLS[index] = n;
                }
            }
        }

        public bool GetCanSetNodeInCell(Cell cell)
        {
            return NODES[GetCellIndex(cell)] == null;
        }

        public Node GetNode(Cell cell)
        {
            int index = GetCellIndex(cell);
            return NODES[index] ?? null;
        }

        public void SetNode(Node node, Cell cell)
        {
            NODES[GetCellIndex(cell)] = node;
            node.cell = cell;
        }    
        
        public void MoveNode(Node node, Cell next)
        {
            SetNode(node, next);
            NODES[GetCellIndex(node.cell)] = null;
            node.cell = next;
        }

        public void RemoveNode(Node node)
        {
            NODES[GetCellIndex(node.cell)] = null;
            node.cell = null;
        }

        public Cell Find(int row, int column)
        {
            int index = FindHelper(row, column, 0, CELLS.Length);
            return index != -1 ? CELLS[index] : null;
        }

        //Binary search to find node
        private int FindHelper(int row, int column, int left, int right)
        {
            if (left > right)           return -1;
            if (left >= CELLS.Length)   return -1;
            if (right <= -1)            return -1;

            int mid = (left + right) / 2;

            Cell n = CELLS[mid];
            int compare = CELLS[mid].Compare(row, column);

            if (compare == 0) { return mid; }

            if (compare < 0) { return FindHelper(row, column, mid + 1, right); }
            else { return FindHelper(row, column, left, right - 1); }
        }

        private int GetCellIndex(Cell cell)
        {
            return FindHelper(cell.ROW, cell.COLUMN, 0, CELLS.Length);
        }

        protected abstract Cell CreateCell(int row, int column);
    }
}