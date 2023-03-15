using UnityEngine;
using System.Collections.Generic;
using System;

namespace TacticsX.Grid
{
    public abstract class Node
    {
        public Cell cell;

        protected void Init() {}

        public abstract void Show();
        public abstract void Hide();
        public abstract void Destroy();
        public abstract void SetPosition(float x, float y, float z);
        public abstract void MoveToPosition(Vector3 position);
    }
}