using System.Collections.Generic;
using UnityEngine;
using TacticsX.Grid;

namespace TacticsX.GridImplementation
{
    public class AssetFactory
    {
        //Depending on your language's data structures you can use something
        //else, create your own custom data structure, or create a variable for 
        //each type of object you need to return.
        //I used a dictionary because it is a C# convenience
        Dictionary<GamePieceType, GameObject> assetDict; 

        public AssetFactory()
        {
            /* 
             *  gprt -> code from GridPiece [grid-based behavior] to CharacterEntity [Real-Time Behavior]
             *  other grid-based entities will obviously impact different scenes like mini-games 
             *  or environmental impacts for example. Well is a reference hold-over, maybe it should be
             *  an environment piece ethat a battle can be fought on, or something.
             */
            assetDict = new Dictionary<GamePieceType, GameObject>();
            assetDict[GamePieceType.Well] = Load("Well");
            assetDict[GamePieceType.HealthPowerUp] = Load("HealthPowerUp");
            assetDict[GamePieceType.DefencePowerUp] = Load("DefencePowerUp");
            assetDict[GamePieceType.DamagePowerUp] = Load("DamagePowerUp");
            assetDict[GamePieceType.MovementPowerUp] = Load("MovementPowerUp");
            assetDict[GamePieceType.MultiattackPowerUp] = Load("MultiattackPowerUp");
            assetDict[GamePieceType.Well] = Load("Well_gpenv");
            assetDict[GamePieceType.Knight] = Load("Knight_gprt");
            assetDict[GamePieceType.Archer] = Load("Archer_gprt");
            assetDict[GamePieceType.Warrior] = Load("Warrior_gprt");
            assetDict[GamePieceType.Barbarian] = Load("Barbarian_gprt");
        }

        public Node Get(GamePieceType piece)
        {
            GameObject prefab = assetDict[piece];

            switch(piece)
            {
                case GamePieceType.HealthPowerUp: return new HealthPowerUp(prefab);
                case GamePieceType.DefencePowerUp: return new DefencePowerUp(prefab);
                case GamePieceType.MovementPowerUp: return new MovementPowerUp(prefab);
                case GamePieceType.MultiattackPowerUp: return new MultiattackPowerUp(prefab);
                case GamePieceType.DamagePowerUp: return new DamagePowerUp(prefab);
                case GamePieceType.Well: return new Well_gpenv(prefab);
                case GamePieceType.Knight: return new Knight_gprt(prefab);
                case GamePieceType.Archer: return new Archer_gprt(prefab);
                case GamePieceType.Warrior: return new Warrior_gprt(prefab);
                case GamePieceType.Barbarian: return new Barbarian_gprt(prefab);
            }

            return null;
        }

        private GameObject Load(string asset)
        {
            return (GameObject)Resources.Load(string.Format("Prefabs/{0}", asset));
        }
 
    }
}