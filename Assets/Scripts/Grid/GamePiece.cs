using UnityEngine;
using TacticsX.Grid;
using DG.Tweening;

namespace TacticsX.GridImplementation
{
    public class GamePiece : Node
    {        
        public GameObject gameObject;
        private float tweenTime = 0.25f;
        public bool isSprite = false;

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

        public override void SetPosition(float x, float y, float z)
        {
            gameObject.transform.position = new Vector3(x, y, z);            
        }

        public void SetPosition(Vector3 position)
        {
            if (this.isSprite == false)
                SetPosition(position.x, position.y - 0.5f, position.z);
            else
                SetPosition(position.x, position.y - 0.5f + 0.9f, position.z);
        }

        public override void MoveToPosition(Vector3 position)
        {
            if (this.isSprite == false)
                this.gameObject.transform.DOMove(new Vector3(position.x, position.y - 0.5f, position.z), tweenTime).SetEase(Ease.OutCirc);
            else
                this.gameObject.transform.DOMove(new Vector3(position.x, position.y - 0.5f + 0.9f, position.z), tweenTime).SetEase(Ease.OutCirc);
        }

        public virtual void DoAction(){}
    }
}