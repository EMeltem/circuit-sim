using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using System.Collections;
using ToolBox.Pools;
using Project.Signals;
using Project.Managers;
using Project.Resistors;

namespace Project.Utilities
{
    public static class Utils
    {
        public static CancellationTokenSource GlobalCancellationSource;
        public static CancellationToken GlobalCancellationToken => GlobalCancellationSource.Token;
        public static bool IsPointerOverUI()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return true;
            }
            else
            {
                PointerEventData pe = new PointerEventData(EventSystem.current);
                pe.position = Input.mousePosition;
                List<RaycastResult> hits = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pe, hits);
                return hits.Count > 0;
            }
        }

        public static void TimeCounter(ref float time, System.Action action, bool wait = false)
        {
            if (wait) return;
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                action();
                return;
            }
        }

        public static void DelayedCall(float delay, Action action, bool unscaled = false)
        {
            DOTween.Sequence().SetDelay(delay).SetUpdate(unscaled).OnComplete(() => action()).Play();
        }

        public static async Task DelayedCallAsync(float delay, System.Action action, bool unscaled = false, CancellationToken token = default)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: token);
            action();
        }

        public static float Remap(float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }

        public static Vector2 GetScreenPos(Vector3 worldPos)
        {
            return Camera.main.WorldToScreenPoint(worldPos);
        }

        private static Dictionary<string, int> m_NotifyCounter = new Dictionary<string, int>();
        private async static void DecreaseCounter(string key)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(2));
            if (m_NotifyCounter.TryGetValue(key, out var _count))
            {
                m_NotifyCounter[key] = _count - 1;
            }
        }

        public static void NotifyCanvas(string text, Vector3 position = default, Sprite icon = null
            , Color color = default, float scaleFactor = 1f, bool useAnimation = true)
        {
            if (m_NotifyCounter.TryGetValue(text, out var _count))
            {
                if (_count > 2) return;
                m_NotifyCounter[text] = _count + 1;
            }
            else
            {
                m_NotifyCounter.Add(text, 1);
            }

            color = color == default ? Color.gray : color;
            position = position == default ? GetCanvasCenterPos() : position;
            var _args = new TextNotifyArgs
            {
                Text = text,
                Position = position,
                Color = color,
                Icon = icon,
                IsWorldSpace = false,
                ScaleFactor = scaleFactor,
                UseAnimation = useAnimation
            };

            UISignals.OnTextPopUpRequest?.Invoke(_args);
            DecreaseCounter(text);
        }

        public static Vector3 GetCanvasCenterPos()
        {
            return new Vector3(Screen.width / 2, Screen.height / 2, 0);
        }

        public static void NotifyWorldSpace(string text, Vector3 position, Sprite icon = null
            , Color color = default, float scaleFactor = 1f, bool useAnimation = false)
        {
            color = color == default ? Color.white : color;
            var _args = new TextNotifyArgs
            {
                Text = text,
                Position = position,
                Color = color,
                Icon = icon,
                IsWorldSpace = true,
                ScaleFactor = scaleFactor,
                UseAnimation = useAnimation
            };

            UISignals.OnTextPopUpRequest?.Invoke(_args);
        }

        public static async void SafeDelay(float delay, CancellationToken token, UnityAction action)
        {
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: token).ContinueWith(onComplete);
            }
            catch (OperationCanceledException)
            {
                OperationCancelledLog();
                return;
            }

            void onComplete()
            {
                action?.Invoke();
            }
        }

        public static void UiRebuilder(ICanvasElement canvasElement)
        {
            canvasElement.Rebuild(CanvasUpdate.PreRender);
        }

        public static void LayoutForceRebuild(Object rectTransform)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform as RectTransform);
        }

        public static void OperationCancelledLog()
        {
            Debug.LogWarning("OCEXCEPT");
        }

        public static float GetRatio(float value, float min, float max)
        {
            return (value - min) / (max - min);
        }

        public static char CheckmarkSymbol => '\u2713';
        public static char CrossmarkSymbol => '\u2717';

        public static string GetCheckmarkString(bool value)
        {
            return value ? CheckmarkSymbol.ToString() : CrossmarkSymbol.ToString();
        }


        public static T GetOrAddComponent<T>(this Component component) where T : Component
        {
            return component.GetComponent<T>() ?? component.gameObject.AddComponent<T>();
        }

        private static Camera s_MainCamera { get; set; }
        public static Camera MainCamera
        {
            get
            {
                return s_MainCamera ?? (s_MainCamera = Camera.main);
            }
        }

        public static bool IsRenderingByCamera(Transform transform)
        {
            if (MainCamera == null) return false;
            var _viewportPos = MainCamera.WorldToViewportPoint(transform.position);
            return _viewportPos.z > 0 && _viewportPos.x > 0 && _viewportPos.x < 1 && _viewportPos.y > 0 && _viewportPos.y < 1;
        }

        public static bool IsMouseInsideRect(RectTransform rectTransform)
        {
            var _rect = rectTransform.rect;
            var _mousePos = Input.mousePosition;
            var _screenPos = rectTransform.position;
            var _min = _screenPos - new Vector3(_rect.width / 2, _rect.height / 2, 0);
            var _max = _screenPos + new Vector3(_rect.width / 2, _rect.height / 2, 0);
            return _mousePos.x > _min.x && _mousePos.x < _max.x && _mousePos.y > _min.y && _mousePos.y < _max.y;
        }

        public static void Skip() { }

        public static float CurrentFPS => 1f / Time.deltaTime;

        public static IEnumerator ReleaseParticle(ParticleSystem particle, float delay = 3f)
        {
            yield return new WaitForSeconds(delay);
            particle.Stop(true);
            particle.gameObject.Release();
        }

        public static async void ReleaseParticleAsync(ParticleSystem particle, float delay = 3f, CancellationToken token = default)
        {
            token = token == default ? GlobalCancellationToken : token;
            await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: token);
            particle.Stop(true);
            particle.gameObject.Release();
        }

        /// <summary>
        /// Get the volume in decibels from a linear volume value.
        /// </summary>
        /// <param name="volume">The linear volume value. (0.0 - 1.0) </param>
        ///  <returns>The volume in decibels.</returns>
        public static float GetAudioMixVolume(float volume)
        {
            return Mathf.Log10(volume) * 20;
        }

        public static (float resistor, float tolerance) CalculateResistance(List<ResistorColor> resistorColorData)
        {
            var A = resistorColorData.Find(ring => (ring.AllowedTypes & ResistorRingType.A) == ResistorRingType.A);
            var B = resistorColorData.Find(ring => (ring.AllowedTypes & ResistorRingType.B) == ResistorRingType.B);
            var C = resistorColorData.Find(ring => (ring.AllowedTypes & ResistorRingType.C) == ResistorRingType.C);
            var T = resistorColorData.Find(ring => (ring.AllowedTypes & ResistorRingType.T) == ResistorRingType.T);
            return CalculateResistance(A, B, C, T);
        }

        public static (float resistance, float tolerance) CalculateResistance(ResistorColor a, ResistorColor b, ResistorColor c, ResistorColor t)
        {
            var _resistance = (a.Value * 10 + b.Value) * (int)Math.Pow(10, c.Value);
            var _tolerance = t.ToleranceValue;

            return (_resistance, _tolerance);
        }


        public static string SimplfyNumber(float value)
        {
            if (value < 1000)
            {
                return value.ToString();
            }
            else if (value < 1000000)
            {
                return $"{value / 1000}K";
            }
            else if (value < 1000000000)
            {
                return $"{value / 1000000}M";
            }
            else
            {
                return $"{value / 1000000000}G";
            }
        }
    }
}