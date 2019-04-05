using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard_mobile : MonoBehaviour
{
	private ChoiceController model;
	private string Question_name;
	// Start is called before the first frame update
	void Start()
    {
		model = FindObjectOfType<ChoiceController>();
		if(model != null) 
		{
			
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
