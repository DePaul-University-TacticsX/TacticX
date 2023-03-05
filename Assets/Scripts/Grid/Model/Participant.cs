using UnityEngine;

namespace TacticsX.GridImplementation
{
    public class Participant
    {
        public GamePiece GamePiece { get; private set; }
        public Sprite Sprite { get; private set; }
        public bool AI { get; private set; }

        public bool DidMove;
        public bool DidAttack;
        public bool DidUseItem;

        public Participant(GamePiece gamePiece,Sprite sprite,bool isAI)
        {
            GamePiece = gamePiece;
            Sprite = sprite;
            AI = isAI;
        }

        public Participant Clone()
        {
            return new Participant(GamePiece, Sprite, AI);
        }
    }
}