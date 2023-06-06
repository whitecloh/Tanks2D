using UnityEngine;

namespace Tanks
{
    public class BaseComponent : MonoBehaviour
    {
        [SerializeField]
        private SideType _side;
        public SideType GetSide => _side;

        private UImanager _ui;

        private void Start()
        {
            _ui = FindObjectOfType<UImanager>();
            _ui.GetResultsImage.SetActive(false);
        }
        public void ImageResult()
        {
            if (_side==SideType.Player)
            {
                _ui.GetResultsText.text = "You Lose";
            }
            else
            {
                _ui.GetResultsText.text = "You Win";
            }
            _ui.OnEndGame();
        }
        private void OnDestroy()
        {
            var exp = Instantiate(_ui.GetDeadAnimation,transform.parent);
            exp.transform.position = transform.position;
        }
    }
}
