using TacticsX.Grid;

namespace TacticsX.GridImplementation
{
    public class Grid : GridManager
    {
        public static Grid Instance;

        CellStateFactory stateFactory;

        public Grid(int size) 
            : base(size)
        {
            Instance = this;
            Init();
        }

        public Grid(int rows, int columns) 
            : base(rows,columns)
        {
            Init();
        }

        void Init()
        {
            stateFactory = new CellStateFactory();
        }

        public GridCell FindGridCell(int row, int column)
        {
            return (GridCell)base.Find(row, column);
        }

        protected override Cell CreateCell(int row, int column)
        {
            return new GridCell(row, column, stateFactory);
        }
    }
}