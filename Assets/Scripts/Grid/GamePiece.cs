﻿using UnityEngine;
using TacticsX.Grid;

namespace TacticsX.GridImplementation
{
    public class GamePiece : Node
    {
        private GameObject gameObject;

        public GamePiece(GameObject prefab)
        {
            Init();
            gameObject = Object.Instantiate(prefab);
        }

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public override void SetPosition(float x, float y)
        {
            gameObject.transform.position = new Vector3(x, 0, y);
        }

        public void SetPosition(Vector3 position)
        {
            SetPosition(position.x, position.z);
        }

        public virtual void DoAction(){}
    }
}