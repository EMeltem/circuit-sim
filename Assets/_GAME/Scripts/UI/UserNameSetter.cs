using System;
using Project.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UserNameSetter : MonoBehaviour
{
    public static UnityAction<string> OnUserNameSet = delegate { };
    [SerializeField] private TMP_Text UserNameText;

    private void Awake()
    {
        UserNameText.text = GameManager.UserName;
        OnUserNameSet += OnUserNameSetHandler;
    }

    private void OnDestroy()
    {
        OnUserNameSet -= OnUserNameSetHandler;
    }

    private void OnUserNameSetHandler(string name)
    {
        UserNameText.text = name;
    }
}