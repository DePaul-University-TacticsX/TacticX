using System.Collections.Generic;
using System;

namespace TacticsX.Grid
{
    public abstract class Node
    {
        const float GRID_LINE_OFFSET = 0.5f;
        
        public Cell cell;

        protected void Init()
        {

        }

        public abstract void Show();
        public abstract void Hide();
        public abstract void SetPosition(float x, float y);
        public abstract void MoveToPosition(float x, float y);
    }
}