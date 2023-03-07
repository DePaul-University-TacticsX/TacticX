using UnityEngine;
using TacticsX.Grid;
using DG.Tweening;

namespace TacticsX.GridImplementation
{
    public class GamePiece : Node
    {        
        public GameObject gameObject;
        private float tweenTime = 0.25f;

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

        public override void Destroy()
        {
            Object.Destroy(gameObject);
        }

        public override void SetPosition(float x, float y)
        {
            gameObject.transform.position = new Vector3(x, 0, y);            
        }

        public void SetPosition(Vector3 position)
        {
            SetPosition(position.x, position.z);
        }

        public override void MoveToPosition(float x, float y)
        {
            this.gameObject.transform.DOMove(new Vector3(x, 0, y), tweenTime).SetEase(Ease.OutCirc);
        }

        public void MoveToPosition(Vector3 position)
        {
            MoveToPosition(position.x, position.z);
        }

        public virtual void DoAction(){}
    }
}