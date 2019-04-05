using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Load_justification : MonoBehaviour
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
					foreach (Item justification in model.JustificationMateriel)
                    {

                        GameObject objet = (GameObject)Instantiate(prefab_object, this.transform);
                        objet.name = justification.Nom;
                        //Debug.Log("element name : " + element.Nom);
                        Toggle temp_toggle = objet.GetComponentInChildren<Toggle>();
                        temp_toggle.name = justification.Nom;

						temp_toggle.isOn = false;
                        
                        Text temp_texte = temp_toggle.GetComponentInChildren<Text>();
                        temp_texte.text = justification.Nom;

                       
                        Image temp_background = (Image)temp_toggle.GetComponentInChildren<Image>();
                        Image temp_selected = (Image)temp_toggle.graphic;

                        temp_background.sprite = Resources.Load<Sprite>(justification.Image + "");
                        temp_selected.sprite = Resources.Load<Sprite>(justification.Image + "");
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
