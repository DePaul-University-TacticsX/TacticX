using UnityEngine;

namespace TacticsX.GridImplementation
{
    public class CursorManager
    {
        public static CursorManager Instance { get; private set; }

        public GameObject cursor;

        public CursorManager()
        {
            Instance = this;

            cursor = new GameObject("Cursor");
            SpriteRenderer renderer = cursor.AddComponent<SpriteRenderer>();
            Texture2D texture = Resources.Load<Texture2D>("Textures/Cursor");
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f,0.5f),128f);
            renderer.sprite = sprite;
            cursor.transform.forward = new Vector3(0, -1, 0);
            cursor.transform.position = new Vector3(0, 0.51f, 0);
            Grid.AddSelectedCellChangedObserver(OnSelectedCellChanged);
        }

        private void OnSelectedCellChanged(GridCell cell)
        {
            SetCursorPosition(cell);
        }

        public void SetCursorPosition(GridCell cell)
        {
            Vector3 cellPosition = cell.GetPosition();
            Vector3 newPosition = cellPosition + new Vector3(0, 0.01f, 0);
            cursor.transform.position = newPosition;
        }

        public void SetCursorPosition(int row, int column)
        {
            GridCell cell = GridManager.Instance.FindGridCell(row, column);
            Vector3 cellPosition = cell.GetPosition();
            Vector3 newPosition = cellPosition + new Vector3(0, 0.01f, 0);
            cursor.transform.position = newPosition;
        }
    }
}