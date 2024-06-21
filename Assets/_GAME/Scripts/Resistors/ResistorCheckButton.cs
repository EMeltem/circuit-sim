using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Project.Resistors;
using Project.Signals;
using Project.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResistorCheckButton : MonoBehaviour
{
    [SerializeField] private Button m_CheckButton;
    [SerializeField] private TMP_Text m_OutputText;
    [SerializeField] private string m_Unit = "Ω";

    private void Awake()
    {
        m_CheckButton.onClick.AddListener(OnCheckButtonClicked);
    }

    private bool m_IsClickValid = true;
    private void OnCheckButtonClicked()
    {
        if (!m_IsClickValid) return;
        var (resistor, tolerance) = Resistor.Instance.CalculateResistance();
        UpdateText(resistor, tolerance);
        CheckIsCorrect(resistor, tolerance);
        m_IsClickValid = false;
    }

    private async void CheckIsCorrect(float resistor, float tolerance)
    {
        var levelData = LevelLoader.CurrentLevelData as ResistorLevelData;
        var (targetResistor, targetTolerance) = levelData.ValueTolerancePair;
        var resistorCorrect = Math.Abs(resistor - targetResistor) < 0.1f;
        var toleranceCorrect = Math.Abs(tolerance - targetTolerance) < 0.1f;

        if (resistorCorrect && toleranceCorrect)
        {
            OnCorrect();
        }
        else
        {
            OnIncorrect();
        }

        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        m_IsClickValid = true;
    }

    private void OnCorrect()
    {
        CorrectFeedback();
        LevelLoader.CompleteLevel();
    }

    private void OnIncorrect()
    {
        IncorrectFeedback();
        if (CanvasStars.Instance.GetStartCount() > 0)
        {
            CanvasStars.Instance.OnStarLose();
        }
        if (CanvasStars.Instance.GetStartCount() != 0) return;
        LevelLoader.FailLevel();
    }

    private void IncorrectFeedback()
    {
        m_CheckButton.image.DOColor(Color.red, 0.5f)
            .OnComplete(() => m_CheckButton.image.DOColor(Color.white, 0.5f));
        m_CheckButton.transform.DOShakePosition(0.5f, 10, 90, 90);
        ScreenShaker.Instance.Shake(0.5f, 0.1f);
    }

    private void CorrectFeedback()
    {
        m_CheckButton.image.DOColor(Color.green, 0.5f)
            .OnComplete(() => m_CheckButton.image.DOColor(Color.white, 0.5f));
    }

    private void UpdateText(float resistor, float tolerance)
    {
        m_OutputText.text = $"{Utils.SimplfyNumber(resistor)} {m_Unit} ± {tolerance}%";
    }
}