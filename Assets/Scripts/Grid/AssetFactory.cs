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
            assetDict = new Dictionary<GamePieceType, GameObject>();
            assetDict[GamePieceType.Well] = Load("Well");
            assetDict[GamePieceType.HealthPowerUp] = Load("HealthPowerUp");
            assetDict[GamePieceType.DefencePowerUp] = Load("DefencePowerUp");
            assetDict[GamePieceType.DamagePowerUp] = Load("DamagePowerUp");
            assetDict[GamePieceType.MovementPowerUp] = Load("MovementPowerUp");
            assetDict[GamePieceType.MultiattackPowerUp] = Load("MultiattackPowerUp");
            /*assetDict[GamePieceType.Bridge] = Load("Bridge");
            assetDict[GamePieceType.House] = Load("House");
            assetDict[GamePieceType.House2] = Load("House2");
            assetDict[GamePieceType.Lamp] = Load("Lamp");
            assetDict[GamePieceType.Well] = Load("Well");*/
        }

        public Node Get(GamePieceType piece)
        {
            GameObject prefab = assetDict[piece];

            switch(piece)
            {
                case GamePieceType.Well: return new Well(prefab);
                case GamePieceType.HealthPowerUp: return new HealthPowerUp(prefab);
                case GamePieceType.DefencePowerUp: return new DefencePowerUp(prefab);
                case GamePieceType.MovementPowerUp: return new MovementPowerUp(prefab);
                case GamePieceType.MultiattackPowerUp: return new MultiattackPowerUp(prefab);
                case GamePieceType.DamagePowerUp: return new DamagePowerUp(prefab);

            }

            return null;
        }

        private GameObject Load(string asset)
        {
            return (GameObject)Resources.Load(string.Format("Prefabs/{0}", asset));
        }
 
    }
}