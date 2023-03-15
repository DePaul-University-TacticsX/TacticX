using UnityEngine;
using System;
using System.Collections.Generic;
using TacticsX.Grid;
using TacticsX.SoundEngine;
using UnityEngine.Analytics;

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
        private DialogueManager dialogueManager;

        private GridCell selectedCell;

        private Action<GridCell> SelectedCellChangeAction;

        private int row;
        private int column;
        private List<GamePiece> listPC = new List<GamePiece>();
        private List<GamePiece> listNPC = new List<GamePiece>();
        public GameObject canvasMovementUI;

        private void Awake()
        {
            Instance = this;
            Analytics.CustomEvent("Level_1_loaded");
        }

        void Start()
        {
            grid = new GridManager(10);
            grid.Build();

            canvasMovementUI = Instantiate(Resources.Load<GameObject>("MovementUI"));
            canvasMovementUI.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
            canvasMovementUI.AddComponent<Billboard>();

            cameraManager = new GridCameraManager();
            cursorManager = new CursorManager();
            assetFactory = new AssetFactory();
            gridController = new GridController(canvasMovementUI);
            dialogueManager = new DialogueManager();

            cameraManager.SetCameraPosition(4, 4);
            TurnManager.AddTurnChangedObserver(OnTurnChanged);

            // Set Boundaries 
            AddGamePiece(GamePieceType.Well, 4, 4);

            //powerups
            AddGamePiece(GamePieceType.HealthPowerUp, 3, 2);
            AddGamePiece(GamePieceType.DefencePowerUp, 3, 1);
            AddGamePiece(GamePieceType.DamagePowerUp, 2, 2);
            AddGamePiece(GamePieceType.MovementPowerUp, 1, 2);
            AddGamePiece(GamePieceType.MultiattackPowerUp, 1, 3);

            AddGamePiece(GamePieceType.Well, 0, 0);
            AddGamePiece(GamePieceType.Well, 4, 0);
            AddGamePiece(GamePieceType.Well, 0, 4);

            // Player Army.
            GamePiece pcKnight        = AddGamePiece(GamePieceType.Knight, 0, 1, new Vector3(0, 0.9f, 0));
            GamePiece pcArcher      = AddGamePiece(GamePieceType.Archer, 0, 2, new Vector3(0, 0.9f, 0));
            GamePiece pcWarrior     = AddGamePiece(GamePieceType.Warrior, 0, 3, new Vector3(0, 0.9f, 0));
            GamePiece npcBarbarian0    = AddGamePiece(GamePieceType.Barbarian, 4, 1, new Vector3(0, 0.9f, 0));
            GamePiece npcBarbarian1    = AddGamePiece(GamePieceType.Barbarian, 4, 2, new Vector3(0, 0.9f, 0));
            GamePiece npcBarbarian2    = AddGamePiece(GamePieceType.Barbarian, 4, 3, new Vector3(0, 0.9f, 0));

            TurnManager.AddParticipant(pcKnight, Resources.Load<Sprite>("Sprites/Characters/Knights/Character_1/Sword and Shield/Idle"), false);
            TurnManager.AddParticipant(pcArcher, Resources.Load<Sprite>("Sprites/Characters/Archers/Character_1/Idle"), false);
            TurnManager.AddParticipant(pcWarrior, Resources.Load<Sprite>("Sprites/Characters/Warriors/Character_3/Idle"), false);
            TurnManager.AddParticipant(npcBarbarian0, Resources.Load<Sprite>("Sprites/Enemies/Barbarian/Idle"), true);
            TurnManager.AddParticipant(npcBarbarian1, Resources.Load<Sprite>("Sprites/Enemies/Barbarian/Idle"), true);
            TurnManager.AddParticipant(npcBarbarian2, Resources.Load<Sprite>("Sprites/Enemies/Barbarian/Idle"), true);

            listPC.Add(pcWarrior);
            listPC.Add(pcArcher);
            listPC.Add(pcKnight);

            listNPC.Add(npcBarbarian0);
            listNPC.Add(npcBarbarian1);
            listNPC.Add(npcBarbarian2);

            TurnManager.Build(true);

            gridController.SetMovementUIToCurrentTurn();

            dialogueManager.UpdateDialogueState(DialogueState.Start);
        }

        private void Update()
        {
            if (FindObjectOfType<DialogueManager>() != null)
            {
                if (FindObjectOfType<DialogueManager>().isActive == true)
                {
                    return;
                }
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

            if (Input.GetKeyDown(KeyCode.A))
            {
                gridController.SetState(ControllerState.Attacking);
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                gridController.SetState(ControllerState.Moving);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                gridController.SetState(ControllerState.EndingTurn);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                RemoveGamePiece(TurnManager.GetCurrentTurn().GamePiece,true);
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
            if (column > grid.COLUMNS - 1) column = grid.COLUMNS - 1;
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
            if (row > grid.ROWS - 1) row = grid.ROWS - 1;
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

        public static GamePiece AddGamePiece(GamePieceType piece, int row, int column, Vector3 positionOffset = new Vector3())
        {
            //Can't set Vector3.zero as a default for the param
            if (positionOffset == new Vector3()) positionOffset = Vector3.zero;
            return Instance.privAddGamePiece(piece, row, column, positionOffset);
        }

        public static void RemoveGamePiece(GamePiece piece,bool isParticipant)
        {
            Instance.privRemoveGamePiece(piece, isParticipant);            
        }

        private GamePiece privAddGamePiece(GamePieceType piece, int row, int column, Vector3 positionOffset)
        {
            GamePiece newGamePiece = (GamePiece) assetFactory.Get(piece);
            GridCell cell = grid.FindGridCell(row, column);
            grid.SetNode(newGamePiece, cell);
            newGamePiece.SetPosition(new Vector3(cell.GetPosition().x + positionOffset.x, cell.GetPosition().y - 0.5f + positionOffset.y, cell.GetPosition().z + positionOffset.z));
            return newGamePiece;
        }

        private void privRemoveGamePiece(GamePiece piece, bool isParticipant)
        {
            Instance.grid.RemoveNode(piece);
                        
            if (isParticipant)
            {                
                if(listPC.Contains(piece))
                {
                    listPC.Remove(piece);
                }
                else if(listNPC.Contains(piece))
                {
                    listNPC.Remove(piece);
                }

                if(listPC.Count == 0)
                {
                    Debug.Log("AI WINS");
                    Analytics.CustomEvent("Level_1_Complete_Lose");
                    dialogueManager = new DialogueManager();
                    dialogueManager.UpdateDialogueState(DialogueState.Lose);
                    return;
                }
                else if(listNPC.Count == 0)
                {
                    Debug.Log("PLAYER WINS");
                    Analytics.CustomEvent("Level_1_Complete_Win");
                    dialogueManager = new DialogueManager();
                    dialogueManager.UpdateDialogueState(DialogueState.Win);
                    return;
                }

                TurnManager.RemoveParticipant(piece);
            }

            piece.Destroy();
        }

        private void OnTurnChanged(Participant turn)
        {
            row = turn.GamePiece.cell.ROW;
            column = turn.GamePiece.cell.COLUMN;
            SetPosition();
        }
    }
}