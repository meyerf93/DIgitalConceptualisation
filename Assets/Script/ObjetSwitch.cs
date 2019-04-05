using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjetSwitch : MonoBehaviour
{
	private ChoiceController model;

    // Start is called before the first frame update
    void Start()
    {
		model = FindObjectOfType<ChoiceController>();

		Image temp = this.GetComponent<Image>();
        foreach (Objets element in model.Objets)
        {
            //Debug.Log("element modification : " + element.Modification + " ; name  : " + element.Nom);
            if (element.Modification)
            {
				temp.sprite = Resources.Load<Sprite>(element.Image+"_unselected");
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
