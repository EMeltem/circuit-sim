using UnityEngine;

public class LevelSelectionStars : MonoBehaviour
{
    [SerializeField] private GameObject m_OneStar;
    [SerializeField] private GameObject m_TwoStar;
    [SerializeField] private GameObject m_ThreeStar;

    public void SelectStars(int stars)
    {
        CloseAll();
        switch (stars)
        {
            case 1:
                m_OneStar.SetActive(true);
                break;
            case 2:
                m_TwoStar.SetActive(true);
                break;
            case 3:
                m_ThreeStar.SetActive(true);
                break;
        }
    }

    private void CloseAll()
    {
        m_OneStar.SetActive(false);
        m_TwoStar.SetActive(false);
        m_ThreeStar.SetActive(false);
    }
}