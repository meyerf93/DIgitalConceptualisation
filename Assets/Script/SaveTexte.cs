using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveTexte : MonoBehaviour
{
	private ChoiceController model;
	private Question temp_question;

    // Start is called before the first frame update
    void Start()
    {
		model = FindObjectOfType<ChoiceController>();

		if(model != null)
		{
			temp_question = model.Questions.Find(r => r.Modification == true);
			Text temp_text = this.GetComponentInChildren<Text>();
			if (temp_question.Reponse != null && temp_question.Reponse != "") 
			{
				temp_text.text = temp_question.Reponse;
			}
		}
    }

	public void Save_content(string contenu)
	{
		if(temp_question != null)
		{
			temp_question.Reponse = contenu;
		}

	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
