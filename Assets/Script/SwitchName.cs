using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchName : MonoBehaviour
{
	private ChoiceController model;

    // Start is called before the first frame update
    void Start()
    {
		model = FindObjectOfType<ChoiceController>();

        Text temp = this.GetComponent<Text>();
        foreach (Objets element in model.Objets)
        {
            if (element.Modification)
            {
				temp.text = element.Nom;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
