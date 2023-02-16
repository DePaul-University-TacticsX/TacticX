using TacticsX.Grid;

namespace TacticsX.GridImplementation
{
    public class GridManager : AbstractGridManager
    {
        public static GridManager Instance { get; private set; }

        CellStateFactory stateFactory;

        public GridManager(int size) 
            : base(size)
        {
            Instance = this;
            Init();
        }

        public GridManager(int rows, int columns) 
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
            return (GridCell)Find(row, column);
        }

        protected override Cell CreateCell(int row, int column)
        {
            return new GridCell(row, column,1f, stateFactory);
        }
    }
}