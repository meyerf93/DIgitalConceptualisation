using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Load_choice : MonoBehaviour
{
	public GameObject prefab_object;
	private ChoiceController model;
    // Start is called before the first frame update
    void Start()
	{
		model = FindObjectOfType<ChoiceController>();
		//Debug.Log("model : " + model);
		if (model != null){
			Debug.Log("number of element in objets : " + model.Objets.Count);
			foreach (Objets element in model.Objets)
            {
				GameObject objet = new GameObject();
				objet =(GameObject)Instantiate(prefab_object, this.transform);
				objet.name = element.Nom;

				Toggle temp_toggle = objet.GetComponentInChildren<Toggle>();
				temp_toggle.name = element.Nom;
				temp_toggle.group = this.GetComponent<ToggleGroup>();
				temp_toggle.isOn = element.Modification;
				Image temp_background = (Image)temp_toggle.GetComponentInChildren<Image>();
				Image temp_selected = (Image)temp_toggle.graphic;

				Debug.Log("image path : " + element.Image+"_unselected");
				Sprite temp_sprite = Resources.Load<Sprite>(element.Image + "_unselected");
				Debug.Log("image is null ? : " + temp_sprite == null);
				Debug.Log("image background is null ? : " + temp_background.sprite == null);
				Debug.Log(element.Image + "_unselected");
				temp_background.sprite = temp_sprite;
				//Resources.Load<Sprite>(element.Image + "_unselected");
				temp_selected.sprite = Resources.Load<Sprite>(element.Image + "_selected");
				temp_toggle.onValueChanged.AddListener(delegate {
					ToggleValueChanged(temp_toggle);
                });
            }
		}

	}


	void ToggleValueChanged(Toggle change)
    {
		//Debug.Log("game object name : " + change.name);
		Objets temp = model.Objets.Find(r => r.Nom == change.name);
		temp.Modification = change.isOn;
    }  
}
