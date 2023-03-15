using System;
using UnityEngine;

namespace TacticsX.GridImplementation
{
    public class GridController
    {
        private static GridController Instance;

        GridCell selectedCell;
        GamePiece selectedNode;
        ControllerState state = ControllerState.None;
        private GameObject canvasMovementUI;
        
        public GridController(GameObject canvasMovementUI)
        {
            Instance = this;
            this.canvasMovementUI = canvasMovementUI;
            Grid.AddSelectedCellChangedObserver(OnSelectedCellChanged);
            TurnManager.AddTurnChangedObserver(OnTurnChanged);
        }

        public void SetState(ControllerState state)
        {
            switch(state)
            {
                //Would actually like to set a variable reference
                //to a class that uses command pattern for each state
                //but this is lazy approach for now
                case ControllerState.Attacking:                 
                case ControllerState.Moving:
                    Instance.state = state;
                    break;
                case ControllerState.EndingTurn:
                    TurnManager.NextTurn();
                    SetMovementUIToCurrentTurn();
                    break;
            }
        }

        ///We have to do this here, because we can't mess with instantiated objects in the static TurnManager
        public void SetMovementUIToCurrentTurn() {
            canvasMovementUI.transform.SetParent(TurnManager.GetCurrentTurn().GamePiece.gameObject.transform);

            //This vector3 places the UI above and in front of the game piece
            canvasMovementUI.transform.localPosition = new Vector3(-0.57f, 1.95f, 0.68f);
        }

        public void SelectCell()
        {
            if(state == ControllerState.Moving)
            {
                if (TurnManager.GetCurrentTurn().DidMove) return;                
                if (GridManager.Instance.GetCanSetNodeInCell(selectedCell) == false) return;

                GridManager.Instance.MoveNode(TurnManager.GetCurrentTurn().GamePiece, selectedCell);
                TurnManager.GetCurrentTurn().GamePiece.MoveToPosition(selectedCell.GetPosition());
                SetMovementUIToCurrentTurn();
                TurnManager.GetCurrentTurn().DidMove = true;
            }
            else if(state == ControllerState.Attacking)
            {
                if (TurnManager.GetCurrentTurn().DidAttack) return;
                if (GridManager.Instance.GetCanSetNodeInCell(selectedCell)) return;

                selectedNode = (GamePiece)GridManager.Instance.GetNode(selectedCell);
                if (selectedCell.Equals(TurnManager.GetCurrentTurn().GamePiece.cell)) return;

                selectedNode.DoAction();
                TurnManager.GetCurrentTurn().DidAttack = true;
            }

            /*if (savedCell == null)
            {
                savedCell = selectedCell;
                selectedNode = (GamePiece)GridManager.Instance.GetNode(savedCell);
            }
            else 
            {
                ProcessCells();
                //selectedCell = null;
                savedCell = null;
                selectedNode = null;
            }*/
        }

        void ProcessCells()
        {
            //Check if cell is empty
            if (GridManager.Instance.GetCanSetNodeInCell(selectedCell))
            {
                //move
                if (selectedNode != null)
                {
                    //Update the data
                    GridManager.Instance.MoveNode(selectedNode, selectedCell);
                    //Do action probably should be renamed to move since
                    //we know we are moving
                    //selectedNode.SetPosition(selectedCell.GetPosition());
                    selectedNode.MoveToPosition(selectedCell.GetPosition());
                }
            }
            //It is not empty so we are going to run an action on the object
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

        void OnTurnChanged(Participant turn)
        {
            state = ControllerState.None;
        }
    }
}