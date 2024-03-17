using UnityEngine;
using TMPro;

public class EndGameController : MonoBehaviour
{
    public TextMeshProUGUI finalTimeText;

    public void SetFinalTime(string time)
    {
        if (GameManager.Instance.getLifes() > 0 ){
            finalTimeText.text = "You Win! " + time;
        } else {
            finalTimeText.text = "You Lose! " + time;
        }
        
    }
}
