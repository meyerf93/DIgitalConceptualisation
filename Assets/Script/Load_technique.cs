﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Load_technique : MonoBehaviour
{
	public GameObject prefab_object;
    private ChoiceController model;
    // Start is called before the first frame update
    void Start()
    {
        model = FindObjectOfType<ChoiceController>();
        //Debug.Log("model : " + model);
        if (model != null)
        {
            foreach (Objets obj in model.Objets)
            {
                if (obj.Modification == true)
                {
                    //Debug.Log("model forme : " + model.Forme);
					foreach (Item element in model.Technique)
                    {

                        GameObject objet = (GameObject)Instantiate(prefab_object, this.transform);
                        objet.name = element.Nom;
                        //Debug.Log("element name : " + element.Nom);
                        Toggle temp_toggle = objet.GetComponentInChildren<Toggle>();
                        temp_toggle.name = element.Nom;
						if (obj.TechniqueActive.Contains(element.ID))
                        {
                            //Debug.Log("toggle is on");
                            temp_toggle.isOn = true;
                        }
						else if (obj.TechniqueInactive.Contains(element.ID))
                        {
                            //Debug.Log("toggle is off");
                            temp_toggle.isOn = false;
                        }
                        Text temp_texte = temp_toggle.GetComponentInChildren<Text>();
                        temp_texte.text = element.Nom;

						foreach (int temp_techniqueActive in obj.TechniqueActive)
                        {
                            //Debug.Log("forme active : " + temp_formeActive + " ; element ID : " + element.ID);
							if (temp_techniqueActive == element.ID)
                            {
                                //Debug.Log("toggle is on");
                                temp_toggle.isOn = true;
                                break;
                            }
                            else
                            {
                                //Debug.Log("toggle is off");
                                temp_toggle.isOn = false;
                            }
                        }
                        Image temp_background = (Image)temp_toggle.GetComponentInChildren<Image>();
                        Image temp_selected = (Image)temp_toggle.graphic;

                        temp_background.sprite = Resources.Load<Sprite>(element.Image + "");
                        temp_selected.sprite = Resources.Load<Sprite>(element.Image + "");
                        temp_selected.color = Color.grey;
                        temp_toggle.onValueChanged.AddListener(delegate {
                            ToggleValueChanged(temp_toggle);
                        });
                    }
                }
            }


        }

    }


    void ToggleValueChanged(Toggle change)
    {
        Objets temp = model.Objets.Find(r => r.Modification == true);
		Item item = model.Technique.Find(r => r.Nom == change.name);
        //Debug.Log("item name : " + item.Nom);
        if (change.isOn)
        {
			if (temp.TechniqueInactive.Contains(item.ID))
            {
				temp.TechniqueInactive.Remove(item.ID);
            }
			if (!temp.TechniqueActive.Contains(item.ID))
            {
				temp.TechniqueActive.Add(item.ID);
            }
        }
        else
        {
			if (temp.TechniqueActive.Contains(item.ID))
            {
				temp.TechniqueActive.Remove(item.ID);
            }
			if (!temp.TechniqueInactive.Contains(item.ID))
            {
				temp.TechniqueInactive.Add(item.ID);
            }
        }
    }
}
