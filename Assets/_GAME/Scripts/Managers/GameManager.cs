using NaughtyAttributes;
using Project.Resistors;
using Project.Utilities;
using UnityEngine;

namespace Project.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static bool GameLoaded { get; set; }
        public static string UserName { get; set; }

        private void Start()
        {
            CursorSettings();
        }

        private void CursorSettings()
        {
            Cursor.visible = false;
#if !UNITY_EDITOR
            Cursor.lockState = CursorLockMode.Confined;
#endif
        }

        [Button]
        private void GenerateRandomResistor()
        {
            var resistorValue = RandomResistorValue();
            Debug.Log($"Resistor: {resistorValue.resistor}, Tolerance: {resistorValue.tolerance}");
        }

        private (double resistor, double tolerance) RandomResistorValue(string message = "")
        {
            var A = ResistorManager.GetRandomDataWithFlag(ResistorRingType.A);
            var B = ResistorManager.GetRandomDataWithFlag(ResistorRingType.B);
            var C = ResistorManager.GetRandomDataWithFlag(ResistorRingType.C);
            var T = ResistorManager.GetRandomDataWithFlag(ResistorRingType.T);
            message = $"A: {A.Name}, B: {B.Name}, C: {C.Name}, T: {T.Name}";
            Debug.Log(message);
            return Utils.CalculateResistance(A, B, C, T);
        }
    }
}