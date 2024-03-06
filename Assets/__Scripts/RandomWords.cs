using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RandomWords : MonoBehaviour
{
    public string[] words;
    public TextMeshProUGUI textbox;
    private void OnEnable()
    {
        textbox.text = words[Random.Range(0, words.Length-1)];
    }
}
