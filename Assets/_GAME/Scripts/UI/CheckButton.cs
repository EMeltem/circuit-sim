using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheckButton : MonoBehaviour
{
    protected Button m_CheckButton;
    [SerializeField] private TMP_Text m_OutputText;
    public static CheckButton Instance { get; private set; }

    private void Awake()
    {
        m_CheckButton = GetComponent<Button>();
        Instance = this;
    }

    public void OnCorrect()
    {
        CorrectFeedback();
        LevelLoader.CompleteLevel();
    }

    public void OnIncorrect()
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
}