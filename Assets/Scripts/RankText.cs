using UnityEngine;
using UnityEngine.UI;

public class RankText : MonoBehaviour
{

    public string[] rankStrings;

    private Text text;

	void Start()
    {
        text = GetComponent<Text>();
	}
	
    public void ChangeRank(int newRank)
    {
        if (newRank < rankStrings.Length)
        {
            text.text = rankStrings[newRank];
        }
    }
}
