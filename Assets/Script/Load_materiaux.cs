using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Load_materiaux : MonoBehaviour
{
	public GameObject prefab_object;
	public GameObject justification;
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
					foreach (Materiaux sous_element in model.Materiaux)
                    {
						List<GameObject> temp_list_justification = new List<GameObject>();;
                        GameObject objet = (GameObject)Instantiate(prefab_object, this.transform);
                        objet.name = sous_element.Nom;
                        //Debug.Log("element name : " + element.Nom);
                        Toggle temp_toggle = objet.GetComponentInChildren<Toggle>();
                        temp_toggle.name = sous_element.Nom;
						if (obj.MateriauxActif.Contains(sous_element.ID))
                        {
                            //Debug.Log("toggle is on");
                            temp_toggle.isOn = true;
                        }
						else if (obj.MateriauxInactif.Contains(sous_element.ID))
                        {
                            //Debug.Log("toggle is off");
                            temp_toggle.isOn = false;
                        }
                        Text temp_texte = temp_toggle.GetComponentInChildren<Text>();
                        temp_texte.text = sous_element.Nom;

						foreach (int temp_materiauxActive in obj.MateriauxActif)
                        {
                            //Debug.Log("forme active : " + temp_formeActive + " ; element ID : " + element.ID);
                            if (temp_materiauxActive == sous_element.ID)
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

                        temp_background.sprite = Resources.Load<Sprite>(sous_element.Image + "");
                        temp_selected.sprite = Resources.Load<Sprite>(sous_element.Image + "");
                        temp_selected.color = Color.grey;

						InitiateJustification(temp_toggle, temp_list_justification);


						temp_toggle.onValueChanged.AddListener(delegate {
                            ToggleValueChanged(temp_toggle);
                        });
                    }
                }
            }

            
        }

    }

	void InitiateJustification(Toggle toggle, List<GameObject> list){

	}


    void ToggleValueChanged(Toggle change)
    {
        Objets temp = model.Objets.Find(r => r.Modification == true);
		Materiaux item = model.Materiaux.Find(r => r.Nom == change.name);
        //Debug.Log("item name : " + item.Nom);
        if (change.isOn)
        {
			if (temp.MateriauxInactif.Contains(item.ID))
            {
				temp.MateriauxInactif.Remove(item.ID);
            }
			if (!temp.MateriauxActif.Contains(item.ID))
            {
				temp.MateriauxActif.Add(item.ID);
            }
        }
        else
        {
			if (temp.MateriauxActif.Contains(item.ID))
            {
				temp.MateriauxActif.Remove(item.ID);
            }
			if (!temp.MateriauxInactif.Contains(item.ID))
            {
				temp.MateriauxInactif.Add(item.ID);
            }
        }
    }
}
