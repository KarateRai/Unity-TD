using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class RoundsSurvived : MonoBehaviour
{
    public Text roundsText;

    private float waitTime = 0.5f;

    private void OnEnable()
    {
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        roundsText.text = "0";
        int round = 0;

        yield return new WaitForSeconds(0.7f);

        while (round < PlayerStats.Rounds)
        {
            round++;
            roundsText.text = round.ToString();
            waitTime -= 0.05f;
            yield return new WaitForSeconds(waitTime);
        }
    }
}
