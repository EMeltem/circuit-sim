using Project.Signals;
using ToolBox.Pools;
using UnityEngine;

namespace Project.Managers
{
    public class TextNotifyManager : MonoBehaviour
    {
        public static bool IsEnabled = true;
        [SerializeField] private TextNotify m_TextPopUpPrefab;
        [SerializeField] private TextNotify m_TextPopUpCanvasPrefab;
        [SerializeField] private Canvas m_Canvas;

        private void Awake()
        {
            Subscriptions(true);
        }

        private void OnDestroy()
        {
            Subscriptions(false);
        }

        protected void Subscriptions(bool register)
        {
            if (register)
            {
                UISignals.OnTextPopUpRequest += OnTextPopUpRequest;
            }
            else
            {
                UISignals.OnTextPopUpRequest -= OnTextPopUpRequest;
            }
        }

        private void OnTextPopUpRequest(TextNotifyArgs arg)
        {
            if (!IsEnabled) return;
            if (arg.IsWorldSpace)
            {
                WorldSpacePopUp(arg);
            }
            else
            {
                CanvasSpacePopUp(arg);
            }
        }

        private void CanvasSpacePopUp(TextNotifyArgs arg)
        {
            m_TextPopUpCanvasPrefab.gameObject.Reuse<TextNotify>(m_Canvas.transform).Initialize(arg, m_Canvas);
        }

        private void WorldSpacePopUp(TextNotifyArgs arg)
        {
            m_TextPopUpPrefab.gameObject.Reuse<TextNotify>(transform).Initialize(arg, m_Canvas);
        }
    }

    public struct TextNotifyArgs
    {
        public Sprite Icon;
        public string Text;
        public Vector3 Position;
        public Color Color;
        public bool IsWorldSpace;
        public float ScaleFactor;
        public bool UseAnimation;
    }
}