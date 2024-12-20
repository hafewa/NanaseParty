using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
namespace OBJScripts
{
    public class PopupMessageControler : MonoBehaviour
    {
        public Text Title;
        public Text Content;
        public Button OKButton;
        public RectTransform MainPopup;
        public TweenCallback Call;
        public void Close ()
        {
            Call?.Invoke();

            MainPopup.DOScale(Vector3.zero, 0.25f).OnComplete(() =>
            {

                Destroy(this.gameObject);

            });

        }
        public void Close_Parent ()
        {
            MainPopup.DOScale(Vector3.zero, 0.25f).OnComplete(() =>
            {
                Destroy(this.gameObject.transform.parent.gameObject);
            });
        }
        public void SetValue (string section_key_value)
        {
            string[] _strings = section_key_value.Split('_');
            GameManager.ServerManager.Config.GameCommonConfig.SetValue(_strings[0], _strings[1], _strings[2]);
        }
    }
}