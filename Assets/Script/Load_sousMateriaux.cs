using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Load_sousMateriaux : MonoBehaviour
{
	public GameObject prefab_object;
	public GameObject materiaux;
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
					foreach (Item element in model.SousMateriaux)
					{
						List<GameObject> temp_list_toggle = new List<GameObject>();
						//Debug.Log("item name : " + element.Nom);
						GameObject objet = (GameObject)Instantiate(prefab_object, this.transform);
						objet.name = element.Nom;
						Toggle temp_toggle = objet.GetComponentInChildren<Toggle>();
						temp_toggle.name = element.Nom;
						if (obj.SousMateriauxActif.Contains(element.ID))
						{
							//Debug.Log("toggle is on");
							temp_toggle.isOn = true;
						}
						else if (obj.SousMateriauxInactif.Contains(element.ID))
						{
							//Debug.Log("toggle is off");
							temp_toggle.isOn = false;
						}
						Text temp_texte = temp_toggle.GetComponentInChildren<Text>();
						temp_texte.text = element.Nom;
						temp_toggle.group = this.GetComponent<ToggleGroup>();

						foreach (int temp_SousmateriauxActive in obj.SousMateriauxActif)
						{
							//Debug.Log("forme active : " + temp_formeActive + " ; element ID : " + element.ID);
							if (temp_SousmateriauxActive == element.ID)
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

						InitiateToggleMateriaux(temp_toggle,temp_list_toggle);                  

						temp_toggle.onValueChanged.AddListener(delegate
						{
							ToggleSousMateriauxValueChanged(temp_toggle);
						});
						temp_toggle.onValueChanged.AddListener(delegate
						{
							DisableMateraiux(temp_toggle,temp_list_toggle);
						});

					}
					//Debug.Log("model forme : " + model.Forme);
				}
            }


        }

    }

	void InitiateToggleMateriaux(Toggle change, List<GameObject> temp_list){
		//Debug.Log("change name : " + change.name);
		Toggle[] toggles_with_id = materiaux.GetComponentsInChildren<Toggle>();
		Item Sous_materiaux = model.SousMateriaux.Find(r => r.Nom == change.name);
        foreach (Materiaux element in model.Materiaux)
        {
			if (element.SousCategorie == Sous_materiaux.ID)
            {
				//Debug.Log("Element with same id : " +element.Nom);
                foreach (Toggle toggle in toggles_with_id)
                {
					//Debug.Log("element name : " + element.Nom);
                    if (toggle.name == element.Nom)
					{
						//Debug.Log("toggle name : " + toggle.name);
						toggle.gameObject.SetActive(change.isOn);
						temp_list.Add(toggle.gameObject);
                    }
                }
				//Debug.Log("nombre element in list : " + temp_list.Count);
            }
        }
	}
	void DisableMateraiux(Toggle change,List<GameObject> temp_list){
		foreach(GameObject temp in temp_list){
			temp.SetActive(change.isOn);
		}
	}

    void ToggleSousMateriauxValueChanged(Toggle change)
    {
        Objets temp = model.Objets.Find(r => r.Modification == true);
        Item item = model.SousMateriaux.Find(r => r.Nom == change.name);
        //Debug.Log("item name : " + item.Nom);
        if (change.isOn)
        {
            if (temp.SousMateriauxInactif.Contains(item.ID))
            {
                temp.SousMateriauxInactif.Remove(item.ID);
            }
            if (!temp.SousMateriauxActif.Contains(item.ID))
            {
                temp.SousMateriauxActif.Add(item.ID);
            }
        }
        else
        {
            if (temp.SousMateriauxActif.Contains(item.ID))
            {
                temp.SousMateriauxActif.Remove(item.ID);
            }
            if (!temp.SousMateriauxInactif.Contains(item.ID))
            {
                temp.SousMateriauxInactif.Add(item.ID);
            }
        }
    }
}
