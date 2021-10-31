using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public bool isDisplaying;
    public GameObject pawnPromotion;
    public Button pawnToRookButton;
    public Button pawnToKnightButton;
    public Button pawnToBishopButton;
    public Button pawnToQueenButton;

    public void ShowPawnPromotionUI()
    {
        pawnPromotion.SetActive(true);
        isDisplaying = true;
    }

    public void HidePawnPromotionUI()
    {
        isDisplaying = false;
        pawnPromotion.SetActive(false);
        pawnToRookButton.onClick.RemoveAllListeners();
        pawnToKnightButton.onClick.RemoveAllListeners();
        pawnToBishopButton.onClick.RemoveAllListeners();
        pawnToQueenButton.onClick.RemoveAllListeners();
    }
}
