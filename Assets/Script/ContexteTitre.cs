using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContexteTitre : MonoBehaviour
{
    private ChoiceController model;

    // Start is called before the first frame update
    void Start()
    {
		model = FindObjectOfType<ChoiceController>();
		//Debug.Log("model : " + model);
		if (model != null)
		{
			Question temp_question = model.Questions.Find(r => r.Modification == true);
			Text temp_text = this.GetComponent<Text>();
			temp_text.text = temp_question.Description;
		}
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
