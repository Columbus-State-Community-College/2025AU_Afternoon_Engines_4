using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshPro))]
[RequireComponent(typeof(TextMeshProUGUI))]
public class HintTextScript : MonoBehaviour
{
    public String Hint_Text;
    public TMPro.TextMeshProUGUI TextComponent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        /* foreach (var item in this.GetComponents<Component>())
        {
            Debug.Log(item);
        } */
        TextComponent = this.GetComponent<TMPro.TextMeshProUGUI>();
        
        //TextComponent.text = Hint_Text;
        // Debug.Log("Hint: " + MainManager.Instance.ProgressTracker.)
    }

    // Update is called once per frame
    void Update()
    {
        TextComponent.text = Hint_Text;
    }
}
