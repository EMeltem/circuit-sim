using UnityEngine;

namespace Project.Avometer
{
    public enum AvometerState { None, Draw, Cutting, ResistorCreation }
    public class AvometerManager : MonoBehaviour
    {
        public delegate void OnStateChanged(AvometerState state);
        public static event OnStateChanged OnStateChangedEvent;
        public static AvometerState CurrentState { get; private set; } = AvometerState.None;
        public static void ChangeState(AvometerState state)
        {
            CurrentState = state;
            OnStateChangedEvent?.Invoke(state);
        }
    }
}