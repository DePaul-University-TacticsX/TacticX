using UnityEngine;
using System;
using System.Collections.Generic;
using TacticsX.Grid;
using UnityEditor.VersionControl;

namespace TacticsX.GridImplementation
{
    public class Grid : MonoBehaviour
    {
        private static Grid Instance;
        
        private GridManager grid;

        private GridCameraManager cameraManager;
        private CursorManager cursorManager;
        private AssetFactory assetFactory;
        private GridController gridController;

        private GridCell selectedCell;

        private Action<GridCell> SelectedCellChangeAction;

        private int row;
        private int column;

        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            grid = new GridManager(5);
            grid.Build();

            cameraManager = new GridCameraManager();
            cursorManager = new CursorManager();
            assetFactory = new AssetFactory();
            gridController = new GridController();

            cameraManager.SetCameraPosition(4, 4);

            // Set Boundaries 
            AddGamePiece(GamePieceType.Well, 4, 4);

            //powerups
            AddGamePiece(GamePieceType.HealthPowerUp, 4, 2);
            AddGamePiece(GamePieceType.DefencePowerUp, 3, 2);
            AddGamePiece(GamePieceType.DamagePowerUp, 2, 2);
            AddGamePiece(GamePieceType.MovementPowerUp, 1, 2);
            AddGamePiece(GamePieceType.MultiattackPowerUp, 1, 3);

            AddGamePiece(GamePieceType.Well, 0, 0);
            AddGamePiece(GamePieceType.Well, 4, 0);
            AddGamePiece(GamePieceType.Well, 0, 4);

            // Player Army.
            AddGamePiece(GamePieceType.Warrior, 0, 3);
            AddGamePiece(GamePieceType.Archer, 0, 2);
            AddGamePiece(GamePieceType.Mage, 0, 1);

        }

        private void Update()
        {
            if (FindObjectOfType<DialogueManager>().isActive == true)
            {
                return;
            }
            if (FindObjectOfType<MiniGameManager>() != null)
            {
                if(FindObjectOfType<MiniGameManager>().isActive == true)
                {
                    return;
                }
            }

            if(Input.GetKeyDown(KeyCode.Space))
            {
                SelectCell();
            }

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

        void SelectCell()
        {
            gridController.SelectCell();
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
            selectedCell = grid.FindGridCell(row, column);
            SelectedCellChangeAction?.Invoke(selectedCell);
        }

        public GridCell GetSelectedCell()
        {
            return selectedCell;
        }

        public static void AddSelectedCellChangedObserver(Action<GridCell> onCellChanged)
        {
            Instance.SelectedCellChangeAction += onCellChanged;
        }

        public static void RemoveSelectedCellChangedObserver(Action<GridCell> onCellChanged)
        {
            Instance.SelectedCellChangeAction -= onCellChanged;
        }


        public static Vector3 GetCurrentCellPosition()
        {
            return Instance.privGetCurrentCellPosition();
        }

        private Vector3 privGetCurrentCellPosition()
        {
            GridCell cell = Instance.grid.FindGridCell(row, column);
            return cell.GetPosition();
        }

        public static GamePiece AddGamePiece(GamePieceType piece,int row,int column)
        {
            return Instance.privAddGamePiece(piece, row, column);
        }

        private GamePiece privAddGamePiece(GamePieceType piece, int row, int column)
        {
            GamePiece newGamePiece = (GamePiece) assetFactory.Get(piece);
            GridCell cell = grid.FindGridCell(row, column);
            grid.SetNode(newGamePiece, cell);
            newGamePiece.SetPosition(cell.GetPosition());
            return newGamePiece;
        }
    }
}