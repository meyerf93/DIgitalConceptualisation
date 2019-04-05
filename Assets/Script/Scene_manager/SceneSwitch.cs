using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitch : MonoBehaviour
{
	
    private ChoiceController model;
	private Button button;

    
	private void Start()
	{
		model = FindObjectOfType<ChoiceController>();
		Button button = this.GetComponent<Button>();
		button.interactable = false;
		foreach (Objets element in model.Objets)
        {
            //Debug.Log("element modification : " + element.Modification + " ; name  : " + element.Nom);
			if (element.Modification)
            {
				button.interactable = true;
            }
        }
	}

	private void Update()
	{
		Button button = this.GetComponent<Button>();
		int true_count = 0;
		foreach (Objets element in model.Objets)
        {
            if (element.Modification)
            {
				true_count++;
            }
        }
		if(true_count > 0){
			button.interactable = true;
		}
		else
		{
			button.interactable = false;
		}
	}

	public void SelectionObjet(){
		SceneManager.LoadScene("Selection_objet");
	}
	public void ContextObjet(){
		
		SceneManager.LoadScene("Context_objet");
	}
	public void EtapeCreation(){
		SceneManager.LoadScene("Etape_creation");
	}
    public void FormeObjet()
    {
        SceneManager.LoadScene("Forme_objet");
    }

    public void FonctionaliteSwitch()
    {
        SceneManager.LoadScene("Fonctionalite");
    }

    public void MaterialSwitch()
    {
        SceneManager.LoadScene("Material");
    }

    public void FormeSwitch()
    {
        SceneManager.LoadScene("Forme");
    }
    public void TailleSwitch()
    {
        SceneManager.LoadScene("Taille");
    }
    public void OutilsSwitch()
    {
        SceneManager.LoadScene("Outils");
    }
    public void TechniqueSwitch()
    {
        SceneManager.LoadScene("Technique");
    }
	public void ImaginonsObjet(){
		SceneManager.LoadScene("Imaginons_objet");
	}
	public void ReponseQuestion(Button button){
		SceneManager.LoadScene("Question_eciture");
		foreach(Question element in model.Questions)
		{
			if(element.Nom == button.name)
			{
				element.Modification = true;
			}
			else
			{
				element.Modification = false;
			}
		}
	}
}
