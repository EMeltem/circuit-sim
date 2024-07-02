using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using Project.Managers;
using Project.Utilities;

public class LoginPanel : MonoBehaviour
{
    [SerializeField] private GameObject LoadingPanelObject;
    [SerializeField] private TMP_InputField InputField;
    [SerializeField] private Button StartButton;

    private void Start()
    {
        if(GameManager.GameLoaded) Destroy(gameObject);
        InputField.onEndEdit.AddListener(OnEndEdit);
        StartButton.onClick.AddListener(OnStartButtonClick);
    }

    private async void OnStartButtonClick()
    {
        var _name = InputField.text;
        if (string.IsNullOrEmpty(_name))
        {
            Debug.LogError("Name is empty!");
            var _pos = Utils.GetCanvasCenterPos() + Vector3.up * 160f;
            Utils.NotifyCanvas("Lütfen önce adınızı giriniz!", color: Color.black, scaleFactor: 1.5f, position: _pos);
            return;
        }
        StartButton.transform.DOScale(Vector3.one * 0.9f, 0.15f).OnComplete(() =>
        {
            StartButton.transform.DOScale(Vector3.one, 0.15f);
        });
        UserNameSetter.OnUserNameSet?.Invoke(_name);
        GameManager.GameLoaded = true;
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        LoadingPanelObject.SetActive(false);
        Destroy(gameObject);
    }

    private void OnEndEdit(string name)
    {
        GameManager.UserName = name;
    }
}