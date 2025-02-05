using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Avometer
{
    public class Avometer_Toolbox : MonoBehaviour
    {
        [SerializeField] private GameObject m_Toolbox;
        [SerializeField] private Button m_DefaultButton, m_DrawButton, m_CuttingButton, m_ResistorCreateButton;
        [SerializeField] private GameObject m_DrawCursor, m_CuttingCursor, m_ResistorCreateCursor;
        public static Avometer_Toolbox Instance;
        private void Awake() => Instance = this;

        private void Start()
        {
            m_DefaultButton.onClick.AddListener(() => OnDefaultButtonClicked());
            m_DrawButton.onClick.AddListener(() => OnDrawButtonClicked());
            m_CuttingButton.onClick.AddListener(() => OnCuttingButtonClicked());
            m_ResistorCreateButton.onClick.AddListener(() => OnResistorCreateButtonClicked());
            m_DefaultButton.onClick.Invoke();
        }

        public void OnDefaultButtonClicked()
        {
            AvometerManager.ChangeState(AvometerState.None);
            CableCreatorLR.Instance.Active = false;
            ColorSetup(m_DefaultButton);
            CursorSetup(null, true);
        }

        private void OnDrawButtonClicked()
        {
            AvometerManager.ChangeState(AvometerState.Draw);
            CableCreatorLR.Instance.Active = true;
            ColorSetup(m_DrawButton);
            CursorSetup(m_DrawCursor);
        }

        private void OnCuttingButtonClicked()
        {
            AvometerManager.ChangeState(AvometerState.Cutting);
            CableCreatorLR.Instance.Active = false;
            ColorSetup(m_CuttingButton);
            CursorSetup(m_CuttingCursor);
        }

        private void OnResistorCreateButtonClicked()
        {
            AvometerManager.ChangeState(AvometerState.ResistorCreation);
            CableCreatorLR.Instance.Active = false;
            ColorSetup(m_ResistorCreateButton);
            CursorSetup(m_ResistorCreateCursor, true);
            ResistorCreator.Instance.ReadyPrefab();
        }

        private void ColorSetup(Button button)
        {
            m_DefaultButton.image.color = Color.white;
            m_DrawButton.image.color = Color.white;
            m_CuttingButton.image.color = Color.white;
            m_ResistorCreateButton.image.color = Color.white;
            button.image.color = Color.black;
        }

        private void OnDestroy()
        {
            m_DefaultButton.onClick.RemoveAllListeners();
            m_DrawButton.onClick.RemoveAllListeners();
            m_CuttingButton.onClick.RemoveAllListeners();
            m_ResistorCreateButton.onClick.RemoveAllListeners();
        }

        private void CursorSetup(GameObject cursor, bool cursorActive = false)
        {
            Cursor.visible = cursorActive;
            m_DrawCursor?.SetActive(false);
            m_CuttingCursor?.SetActive(false);
            m_ResistorCreateCursor?.SetActive(false);
            if (cursor != null) cursor.SetActive(true);
        }

        public void CloseToolbox()
        {
            m_Toolbox.SetActive(false);
        }

        public void OpenToolbox()
        {
            m_Toolbox.SetActive(true);
        }

        private void Update()
        {
            CursorMovement();
        }

        private void CursorMovement()
        {
            if (m_DrawCursor.activeSelf)
                m_DrawCursor.transform.position = Input.mousePosition;
            if (m_CuttingCursor.activeSelf)
                m_CuttingCursor.transform.position = Input.mousePosition;
            if (m_ResistorCreateCursor.activeSelf)
                m_ResistorCreateCursor.transform.position = Input.mousePosition;
        }
    }
}