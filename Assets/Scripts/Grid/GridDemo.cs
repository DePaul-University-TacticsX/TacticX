using UnityEngine;
using System.Collections.Generic;
using TacticsX.Grid;

namespace TacticsX.GridImplementation
{
    public class GridDemo : MonoBehaviour
    {
        private Grid grid;
        private AssetFactory factory;
        private SelectionManager selectionManager;
        private GridCameraManager cameraManager;
        private Node selectedNode;
        private List<Cell> selectedCells;
        private bool canSetNode;

        private int row;
        private int column;

        void Start()
        {
            grid = new Grid(5);
            grid.Build();

            factory = new AssetFactory();
            selectionManager = new SelectionManager();
            cameraManager = new GridCameraManager();

            selectedCells = new List<Cell>();

            cameraManager.SetCameraPosition(4, 4);

            InputManager.AddListenerMouseMove(OnMouseMoveAction);
            InputManager.AddListenerMouseClick(OnMouseClickAction);
            SelectionManager.AddListenerSelectPiece(OnSelectPieceAction);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveLeft();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveRight();
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                MoveUp();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                MoveDown();
            }
        }

        void MoveLeft()
        {
            column--;
            if (column < 0) column = 0;
            else SetPosition();
        }

        void MoveRight()
        {
            column++;
            if (column > 4) column = 4;
            else SetPosition();
        }

        void MoveUp()
        {
            row--;
            if (row < 0) row = 0;
            else SetPosition();
        }

        void MoveDown()
        {
            row++;
            if (row > 4) row = 4;
            else SetPosition();
        }

        [ContextMenu("Move Camera")]
        public void SetPosition()
        {
            cameraManager.MoveToPosition(row, column);
        }

        void OnMouseMoveAction(Vector3 v)
        {
            for(int i = 0; i < selectedCells.Count; i++)
            {
                selectedCells[i].SetState(CellStateType.Normal);
            }

            selectedCells.Clear();

            if (selectedNode == null) return;

            RowColumnPair center = selectedNode.GetCenterPointFromMouseCoord(v.x, v.z);
            RowColumnPair[] coords = selectedNode.GetCoordsFromPoint(center.Row, center.Column);
            int nCoords = coords.Length;
            int nCellsFound = 0;

            for(int i = 0; i < coords.Length; i++)
            {
                RowColumnPair coord = coords[i];
                Cell cell = grid.Find((int)coord.Row, (int)coord.Column);
                if (cell != null)
                {
                    nCellsFound++;
                    selectedCells.Add(cell);
                }
            }

            if(nCellsFound == nCoords)
            {
                selectedNode.SetPosition(center.Row, center.Column);
                selectedNode.Show();
                canSetNode = true;

                for (int i = 0; i < selectedCells.Count; i++)
                {
                    Cell cell = selectedCells[i];
                    CellStateType state = grid.GetCanSetNodeInCell(cell) ? CellStateType.Open : CellStateType.Blocked;
                    cell.SetState(state);
                    if (state == CellStateType.Blocked)
                    {
                        canSetNode = false;
                        selectedNode.Hide();
                    }
                }
            }
            else
            {
                selectedNode.Hide();
            }
        }

        void OnMouseClickAction(Vector3 v)
        {
            if (selectedNode == null) return;

            if(canSetNode)
            {
                for (int i = 0; i < selectedCells.Count; i++)
                {
                    grid.SetNode(selectedNode, selectedCells[i]);
                }
            }
            else
            {
                selectedNode.Hide();
            }

            selectedNode = null;
        }

        void OnSelectPieceAction(GamePieceType piece)
        {
            selectedNode = factory.Get(piece);
            selectedNode.Hide();
        }
    }
}