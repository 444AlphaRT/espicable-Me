using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class MinionCounterUI : MonoBehaviour
{
    private TMP_Text counterText;

    private void Awake()
    {
        counterText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (ResourceManager.Instance == null)
        {
            counterText.text = "0 / 5";
            return;
        }

        int collected = ResourceManager.Instance.totalCollectedMinions;
        int total = ResourceManager.Instance.totalMinionsToWin;

        counterText.text = collected.ToString() + " / " + total.ToString();
    }
}