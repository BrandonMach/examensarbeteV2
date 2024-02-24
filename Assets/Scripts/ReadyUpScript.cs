using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReadyUpScript : MonoBehaviour
{
    [SerializeField] Image _readyUpIndicator;
    [SerializeField] TextMeshProUGUI _readyText;
   

    public void IsReady()
    {
        _readyUpIndicator.color = Color.green;
        _readyText.text = "Ready";
    }

    public IEnumerator AllPlayersReady()
    {
        yield return new WaitForSeconds(0.2f);
        this.gameObject.SetActive(false);
    }
}
