using UnityEngine;

namespace TacticsX.GridImplementation
{
    public class Turn
    {
        public GamePiece GamePiece { get; private set; }
        public Sprite Sprite { get; private set; }
        public bool AI { get; private set; }

        public bool DidMove;
        public bool DidAttack;
        public bool DidUseItem;

        public Turn(GamePiece gamePiece,Sprite sprite,bool isAI)
        {
            GamePiece = gamePiece;
            Sprite = sprite;
            AI = isAI;
        }

        public Turn Clone()
        {
            return new Turn(GamePiece, Sprite, AI);
        }
    }
}