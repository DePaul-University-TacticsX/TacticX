using UnityEngine;
using DG.Tweening;

namespace TacticsX.GridImplementation
{
    public class GridCameraManager
    {
        private Camera cam;
        private Vector3 anchorDir = new Vector3(4f, 3.33f, 4f);
        private Vector3 baseVector = new Vector3(2.0f, 0.5f, 2.0f);
        private float distance = 1.75f;
        private float tweenTime = 0.75f;

        public GridCameraManager()
        {
            cam = Camera.main;
            anchorDir = anchorDir - baseVector;
        }

        public void MoveToPosition(int row, int column)
        {
            Vector3 newPosition = GetTargetPostion(row, column);
            cam.transform.DOMove(newPosition, tweenTime).SetEase(Ease.OutCirc);
        }

        public void SetCameraPosition(int row, int column)
        {
            cam.transform.position = GetTargetPostion(row, column);
           
        }

        private Vector3 GetTargetPostion(int row, int column)
        {
            GridCell cell = Grid.Instance.FindGridCell(row, column);
            Vector3 cellPosition = cell.GetPosition() + new Vector3(0, 0.5f, 0);
            Vector3 newPosition = cellPosition + (anchorDir * distance);
            return newPosition;
        }
    }
}