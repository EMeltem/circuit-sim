using UnityEngine;

namespace Project.Managers
{
    public class GameManager : MonoBehaviour
    {
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
    }
}