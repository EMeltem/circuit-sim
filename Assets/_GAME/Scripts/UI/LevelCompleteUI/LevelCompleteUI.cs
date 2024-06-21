using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Project.Signals;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using log4net.Core;

public class LevelCompleteUI : MonoBehaviour
{
    [SerializeField] private GameObject m_Panel;
    [SerializeField] private Sprite m_WingBG, m_LoseBG;
    [SerializeField] private Image m_BG;

    [SerializeField] private List<GameObject> m_WinObjects;
    [SerializeField] private List<GameObject> m_LoseObjects;

    [SerializeField] private List<LevelEndStar> m_Stars;

    [SerializeField] private TMP_Text m_LevelEndText;
    [SerializeField] private string m_WinText, m_LoseText;

    private void Awake()
    {
        GameSignals.OnLevelCompleted += OnLevelCompleted;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameSignals.OnLevelCompleted -= OnLevelCompleted;
    }

    private async void OnLevelCompleted(LevelData data, bool isWin)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        Cursor.visible = true;
        gameObject.SetActive(true);
        m_Panel.GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        m_Panel.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        SelectMessage(isWin);
        SelectBackground(isWin);
        SetStateObjects(isWin);
        AdjustStars(isWin);
    }

    private void SetStateObjects(bool isWin)
    {
        foreach (var obj in m_WinObjects)
        {
            obj.SetActive(isWin);
        }

        foreach (var obj in m_LoseObjects)
        {
            obj.SetActive(!isWin);
        }
    }

    private void SelectMessage(bool isWin)
    {
        var _message = isWin ? m_WinText : m_LoseText;
        StartCoroutine(UpdateTextMeshOverTime(_message, 1.5f));
    }

    private async void AdjustStars(bool isWin)
    {
        foreach (var star in m_Stars)
        {
            star.Close();
        }

        if (!isWin) return;
        foreach (var star in m_Stars)
        {
            await star.OpenWithAnimation();
        }
    }

    private void SelectBackground(bool isWin)
    {
        m_BG.sprite = isWin ? m_WingBG : m_LoseBG;
    }

    private IEnumerator UpdateTextMeshOverTime(string newText, float duration)
    {
        float elapsedTime = 0;
        string originalText = m_LevelEndText.text;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            int charCount = Mathf.FloorToInt(t * newText.Length);
            m_LevelEndText.text = newText.Substring(0, charCount);
            yield return null;
        }

        m_LevelEndText.text = newText;
    }

    public void MenuClick()
    {
        LevelLoader.LoadMenu();
    }

    public void LoadNextLevel()
    {
        LevelLoader.LoadNextLevel().Forget();
    }

    public void ReloadCurrentLevel()
    {
        LevelLoader.ReloadLevel().Forget();
    }
}