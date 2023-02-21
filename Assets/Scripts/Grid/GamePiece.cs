using UnityEngine;
using TacticsX.Grid;
using DG.Tweening;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.UIElements;

namespace TacticsX.GridImplementation
{
    public class GamePiece : Node
    {
        private GameObject gameObject;
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

        public override void SetPosition(float x, float y)
        {
            this.gameObject.transform.DOMove(new Vector3(x, 0, y), tweenTime).SetEase(Ease.OutCirc);
        }

        public void SetPosition(Vector3 position)
        {
            SetPosition(position.x, position.z);
        }


        public virtual void DoAction(){}
    }
}