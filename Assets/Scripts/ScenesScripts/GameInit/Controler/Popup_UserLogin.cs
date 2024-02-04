using Common;
using Common.Network;
using Common.UI;
using DG.Tweening;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
namespace ScenesScripts
{
    public class Popup_UserLogin : MonoBehaviour
    {
        public InputField InputField_User;
        public InputField InputField_Password;
        public RectTransform MainPopup;
        public async void Button_Click_Login ()
        {
            var _account = InputField_User.text;
            var _password = InputField_Password.text;
            if (!IsEmailOrPhone(_account))
            {
                PopupManager.PopMessage("请您规范输入哦~", "账号格式不正确，应为邮箱地址或手机号。");
                return;
            }
            if (_password.Length < 5)
            {
                PopupManager.PopMessage("请您规范输入哦~", "密码长度不正确。");
                return;
            }

            var _loading = new ShowLoading("正在请求");
            var _params = new
            {
                account = _account,
                password = GameAPI.GenerateSha256(_password)
            };
            Debug.Log(JsonConvert.SerializeObject(_params));
            var _res = await NetworkHelp.Post($"{GameConst.API_URL}/Account/Login", _params);
            _loading.KillLoading();
            //_res为返回的json
            Debug.Log(_res);
            try
            {
                //检查是否正确
                JsonConvert.DeserializeObject<JObject>(_res)["status"].Equals("success");

            }
            catch
            {
                PopupManager.PopMessage("错误", "登录失败！");
                return;
            }


        }
        private static bool IsEmailOrPhone (string s)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            string phonePattern = @"^1[3-9]\d{9}$";

            if (Regex.IsMatch(s, emailPattern) || Regex.IsMatch(s, phonePattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Button_Click_Close ()
        {
            MainPopup.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InQuint).OnComplete(() =>
            {
                Destroy(this.gameObject);
            });
        }



    }

}