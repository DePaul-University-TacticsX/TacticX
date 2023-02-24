using UnityEngine;

namespace TacticsX.GridImplementation
{
    public class Turn
    {
        public GamePiece GamePiece { get; private set; }
        public Sprite Sprite { get; private set; }

        public bool DidMove;
        public bool DidAttack;
        public bool DidUseItem;

        public Turn(GamePiece gamePiece,Sprite sprite)
        {
            GamePiece = gamePiece;
            Sprite = sprite;
        }

        public Turn Clone()
        {
            return new Turn(GamePiece, Sprite);
        }

        public bool GetIsAI()
        {
            return GamePiece.GetIsAI();
        }
    }
}