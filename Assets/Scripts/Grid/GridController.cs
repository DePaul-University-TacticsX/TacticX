using System;

namespace TacticsX.GridImplementation
{
    public class GridController
    {
        private static GridController Instance;

        GridCell selectedCell;
        GamePiece selectedNode;
        GridCell savedCell;

        public GridController()
        {
            Instance = this;
            Grid.AddSelectedCellChangedObserver(OnSelectedCellChanged);                       
        }

        public void SelectCell()
        {
            if (savedCell == null)
            {
                savedCell = selectedCell;
                selectedNode = (GamePiece)GridManager.Instance.GetNode(savedCell);
            }
            else 
            {
                ProcessCells();
                selectedCell = null;
                savedCell = null;
                selectedNode = null;
            }
        }

        void ProcessCells()
        {
            if (GridManager.Instance.GetCanSetNodeInCell(selectedCell))
            {
                //move
                if (selectedNode != null)
                {
                    //Update the data
                    GridManager.Instance.MoveNode(selectedNode, savedCell, selectedCell);
                    //Do action probably should be renamed to move since
                    //we know we are moving
                    selectedNode.DoAction();
                }
            }
            else
            {
                if (selectedNode != null)
                {
                    //The code for doaction maybe needs to
                    //look at selected objects and make a judgement call.
                    //If it is an enemy maybe it needs to look at GridController
                    //class and identify who is doing battle to kick things off.
                    //Or if it's the collectable maybe it just needs to do the minigame
                    //Ask cameron if confused
                    selectedNode.DoAction();
                }
            }
        }

        void OnSelectedCellChanged(GridCell cell)
        {
            selectedCell = cell;
        }
    }
}