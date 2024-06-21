using Cysharp.Threading.Tasks;
using DG.Tweening;
using Project.Utilities;
using TMPro;
using ToolBox.Pools;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Managers
{
    [RequireComponent(typeof(CanvasGroup))]
    public class TextNotify : MonoBehaviour
    {
        [SerializeField] private CanvasGroup m_CanvasGroup;
        [SerializeField] private TMP_Text m_TextMesh;
        [SerializeField] private Image m_IconImage;
        public void Initialize(TextNotifyArgs args, Canvas canvas)
        {
            m_CanvasGroup.alpha = 0f;
            m_MainCanvas = canvas;
            if (args.Icon != null)
            {
                m_IconImage.sprite = args.Icon;
                m_IconImage.gameObject.SetActive(true);
            }
            else
            {
                m_IconImage.gameObject.SetActive(false);
            }

            var _scaleFactor = args.ScaleFactor > 0 ? args.ScaleFactor : 1f;
            transform.position = args.Position;
            transform.localScale = Vector3.one * _scaleFactor;
            m_TextMesh.SetText(args.Text);
            m_TextMesh.color = args.Color;
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform.GetChild(0).GetComponent<RectTransform>());
            if (!args.IsWorldSpace) FitInScreen();
            else m_CanvasGroup.alpha = 1f;
            StartUpMovement(args);
        }

        private Canvas m_MainCanvas;
        private RectTransform m_RectTransform;
        private async void FitInScreen() // FIXME: not working properly
        {
            await UniTask.DelayFrame(2, cancellationToken: this.GetCancellationTokenOnDestroy());
            m_RectTransform = transform.GetChild(0).GetComponent<RectTransform>();
            m_RectTransform.anchoredPosition = Vector2.zero;
            var _screenPos = GetComponent<RectTransform>().anchoredPosition;
            var _rect = m_RectTransform.rect;
            var _refResolution = m_MainCanvas.GetComponent<CanvasScaler>().referenceResolution;
            var _x = Utils.Remap(_screenPos.x, -_refResolution.x * 0.5f, _refResolution.x * 0.5f, +_rect.width / 2f, -_rect.width / 2f);
            var _y = Utils.Remap(_screenPos.y, -_refResolution.y * 0.5f, _refResolution.y * 0.5f, +_rect.height / 2f, -_rect.height / 2f);
            m_RectTransform.anchoredPosition = new Vector2(_x, _y);
            m_CanvasGroup.alpha = 1f;
        }

        private void StartUpMovement(TextNotifyArgs args)
        {
            var _startDelay = args.UseAnimation ? 1f : 0f;
            var _targetPos = transform.position.y + 100f;
            if (args.IsWorldSpace)
                _targetPos = transform.position.y + 2f;

            if (args.UseAnimation)
            {
                transform.localScale = Vector3.zero;
                transform.DOScale(Vector3.one * args.ScaleFactor, 0.35f)
                .SetEase(Ease.OutBack, 1.25f)
                .OnComplete(() => UpMovement());
            }
            else
            {
                UpMovement();
            }

            void UpMovement()
            {
                transform.DOMoveY(_targetPos, 2.5f)
                    .SetDelay(_startDelay)
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        gameObject.Release();
                        m_CanvasGroup.alpha = 1f;
                    });
                StartFadeOut();
            }
        }

        private void StartFadeOut()
        {
            m_CanvasGroup.DOFade(0f, 1.5f).SetDelay(1.2f);
        }
    }
}
