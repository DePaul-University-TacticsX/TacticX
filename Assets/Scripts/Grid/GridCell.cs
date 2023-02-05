using UnityEngine;
using TacticsX.Grid;

namespace TacticsX.GridImplementation
{
    public class GridCell : Cell
    {
        private Renderer renderer;
        private GameObject gameObject;
        private CellStateFactory stateFactory;
        private Vector3 positionOffset;

        public GridCell(int row, int column, float height, CellStateFactory stateFactory) 
            : base(row, column)
        {
            this.stateFactory = stateFactory;
            this.positionOffset = new Vector3(0, height/2, 0);

            //I'm using a full 1x1 cube for my prefab, but painting
            //a full terrain with cubes is difficult so you could
            //consider the using a 2D sprite as your node that is
            //layed on top of a textured plane.
            gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            renderer = gameObject.GetComponent<Renderer>();

            gameObject.transform.localPosition = new Vector3(1, height, 1);

            SetState(CellStateType.Normal);
        }

        public Vector3 GetPosition()
        {
            return gameObject.transform.position + positionOffset;
        }

        public override void SetPosition(int x, int y)
        {
            gameObject.transform.position = new Vector3(x, 0, y);
        }

        public override void SetState(CellStateType state)
        {
            stateFactory.Get(state).SetState(renderer);
        }
    }
}