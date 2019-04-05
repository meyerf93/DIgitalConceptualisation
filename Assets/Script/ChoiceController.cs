using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class ChoiceController : MonoBehaviour
{
	public static ChoiceController controller;

	public Profils Profils;
	public List<Historique> Historique;
	public List<Objets> Objets;
	public List<Association_justification> AssociationJustifications;
	public List<JustificationMateriel> JustificationLienMateriel;
	public List<CoherenceMaterielTechniqueOutils> CoherenceMaterielTechniqueOutils;
	public List<Item> Forme;
	public List<Taille> TailleObjet;
	public List<Item> JustificationMateriel;
	public List<Item> Outils;
	public List<Item> Technique;
	public List<Item> SousMateriaux;
	public List<Materiaux> Materiaux;
	public List<Question> Questions;

	public const string PROFILS_FILENAME = "Profils_utilisateur.csv";
	public const string OBJETS_FILENAME = "Objets.csv";
	public const string TAILLE_FILENAME = "Taile_objet.csv";
	public const string FORME_FILENAME = "Forme.csv";
	public const string MATERIEL_FILENAME = "Materiel.csv";
	public const string JUSTIFICATION_FILENAME = "Justification_materiel.csv";
	public const string OUTILS_FILENAME = "Outils.csv";
	public const string TECHNIQUE_FILENAME = "Technique.csv";


	public const string COHERENCE_FILENAME = "Coherence_materiel_technique_outils.csv";
	public const string MATERIELJUSTIFICATION_FILENAME = "Lien_materiel_justification.csv";

	public const string CURRENTSTATE_FILENAME = "Etat_actuel.csv";

	// Start is called before the first frame update
	void Awake()
	{
		if (controller == null)
		{
			DontDestroyOnLoad(gameObject);
			controller = this;
		}
		else if (controller != this)
		{
			Destroy(gameObject);
		}

		Profils = new Profils();
		Historique = new List<Historique>();
		Objets = new List<Objets>();
		AssociationJustifications = new List<Association_justification>();
		JustificationLienMateriel = new List<JustificationMateriel>();
		CoherenceMaterielTechniqueOutils = new List<CoherenceMaterielTechniqueOutils>();



		Forme = new List<Item>();
		TailleObjet = new List<Taille>();
		JustificationMateriel = new List<Item>();
		Outils = new List<Item>();
		Technique = new List<Item>();
		SousMateriaux = new List<Item>();
		Materiaux = new List<Materiaux>();
		Questions = new List<Question>();

		load_CoherenceMateriel();
		load_JustificationMateriel();

		load_Profils();
		load_Objets();
		load_taille();
		load_Item(FORME_FILENAME, Forme);
		load_Item(JUSTIFICATION_FILENAME, JustificationMateriel);
		load_Item(OUTILS_FILENAME, Outils);
		load_Item(TECHNIQUE_FILENAME, Technique);
		load_Materiaux();

		load_currentState();
	}



	string[] load_File(string filename)
	{
		string _file = "";
		if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
		{
			//Debug.Log("in windows or mac platform");
			WWW request = new WWW("file://" + Application.streamingAssetsPath + "/" + filename);
			while (!request.isDone) { }
			_file = request.text as string;
		}
		else if (Application.platform == RuntimePlatform.Android)
		{
			//Debug.Log("in android platform");
			WWW request = new WWW("jar:file://" + Application.dataPath + "!/assets/" + filename);
			while (!request.isDone) { }
			_file = request.text as string;
		}
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			//Debug.Log("in iphone plateform");
			WWW request = new WWW("file://" + Application.streamingAssetsPath + "/" + filename);
			while (!request.isDone) { }
			_file = request.text as string;
		}

		string[] file = Regex.Split(_file, "\n");
		return file;
	}

	void load_Profils()
	{
		string[] file_line = load_File(PROFILS_FILENAME);

		string Header = null;

		int size = file_line.Length;
		//Debug.Log("Number of line of the configuration file : " + size);
		int marker = 0;

		for (int i = 0; i < size; i++)
		{
			string[] elements = file_line[i].Split(';');
			if (elements[0].CompareTo("") != 0)
			{
				Header = elements[0];
				marker = i;
				//Debug.Log("header : " + Header);
                //Debug.Log(Header.CompareTo("Question"));
                //Debug.Log("i : " + i + " ; marker : " + marker);
			}
            if (Header.CompareTo("Historique") == 0 && i > marker)
			{
				//Debug.Log("in historique");

				Historique temp = new Historique();
				temp.ID = int.Parse(elements[1]);
				temp.Objet = int.Parse(elements[2]);

				string[] forme;
				for (int j = 3; j < elements.Length - 1; j++)
				{
					List<int> liste = new List<int>();

					if (elements[j].CompareTo("") != 0)
					{
						forme = elements[j].Split(',');
						foreach (string thing in forme)
						{
							liste.Add(int.Parse(thing));
						}
					}
					switch (j)
					{
						case 3:
                            temp.FormeActive = liste;
                            break;
                        case 4:
                            temp.FormeInactive = liste;
                            break;
                        case 5:
                            temp.TailleActive = liste;
                            break;
                        case 6:
                            temp.TailleInactive = liste;
                            break;
                        case 7:
                            temp.MateriauxActif = liste;
                            break;
                        case 8:
                            temp.MateriauxInactif = liste;
                            break;
                        case 9:
                            temp.SousMateriauxActif = liste;
                            break;
                        case 10:
                            temp.SousMateriauxInactif = liste;
                            break;
                        case 11:
                            temp.OutilsActif = liste;
                            break;
                        case 12:
                            temp.OutilsInactif = liste;
                            break;
                        case 13:
                            temp.TechniqueActive = liste;
                            break;
                        case 14:
                            temp.TechniqueInactive = liste;
                            break;
                        case 15:
                            temp.AssociationJustification = liste;
                            break;
                        default:
                            break;
					}
				}
				Historique.Add(temp);
			}
			else if (Header.CompareTo("Profils") == 0 && i > marker){
				Profils.ID = int.Parse(elements[1]);
				Profils.Etoiles_actuel = int.Parse(elements[2]);
				Profils.Etoiles_depenser = int.Parse(elements[3]);
			}
		}
	}

	void load_currentState()
	{
		string[] file_line = load_File(CURRENTSTATE_FILENAME);

		string Header = null;

		int size = file_line.Length;
		//Debug.Log("Number of line of the configuration file : " + size);
		int marker = 0;

		for (int i = 0; i < size; i++)
		{
			string[] elements = file_line[i].Split(';');
			if (elements[0].CompareTo("") != 0)
			{
				Header = elements[0];
				marker = i;
			}
			else if (Header.CompareTo("Objet") == 0 && i > marker)
			{
				//Debug.Log("in object");

				Objets temp = Objets.Find(r => r.ID == int.Parse(elements[1]));
				string[] forme;
				for (int j = 2; j < elements.Length - 1; j++)
				{
					//Debug.Log("element j : " + elements[j]);
					List<int> liste = new List<int>();

					if (elements[j].CompareTo("") != 0)
					{
						//Debug.Log("element j : " + elements[j]);
						forme = elements[j].Split(',');
						foreach (string thing in forme)
						{
							liste.Add(int.Parse(thing));
						}
					}
					switch (j)
					{
						case 2:
							temp.FormeActive = liste;
							break;
						case 3:
							temp.FormeInactive = liste;
							break;
						case 4:
							temp.TailleActive = liste;
							break;
						case 5:
							temp.TailleInactive = liste;
							break;
						case 6:
							temp.MateriauxActif = liste;
							break;
						case 7:
							temp.MateriauxInactif = liste;
							break;
						case 8:
							temp.SousMateriauxActif = liste;
							break;
						case 9:
							temp.SousMateriauxInactif = liste;
							break;
						case 10:
							temp.OutilsActif = liste;
							break;
						case 11:
							temp.OutilsInactif = liste;
							break;
						case 12:
							temp.TechniqueActive = liste;
							break;
						case 13:
							temp.TechniqueInactive = liste;
							break;
						case 14:
							temp.AssociationJustification = liste;
							break;
						default:
							break;
					}
				}
				temp.Modification = bool.Parse(elements[15]);
			}
			else if (Header.CompareTo("Association_justification") == 0 && i > marker)
			{
				//Debug.Log("in association justification");

				Association_justification temp = new Association_justification();
				temp.ID = int.Parse(elements[1]);
				temp.Materiaux = int.Parse(elements[2]);

				for (int j = 3; j < elements.Length - 1; j++)
				{
					List<int> liste = new List<int>();
					if (elements[j].CompareTo("") != 0)
					{
						string[] forme = elements[j].Split(',');
						foreach (string thing in forme)
						{
							liste.Add(int.Parse(thing));
						}
					}
					switch (j)
					{
						case 3:
							temp.JustificationActive = liste;
							break;
						case 4:
							temp.JustificaionInactive = liste;
							break;
						default:
							break;
					}
				}
				AssociationJustifications.Add(temp);

			}
			else if (Header.CompareTo("Question") == 0 && i > marker)
			{
				Question temp = new Question();
				Debug.Log("Element 1 : " + elements[1]);
				temp.ID = int.Parse(elements[1]);
				temp.Nom = elements[2];
				temp.Description = elements[3];
				if (elements[4] != null)
				{
					temp.Reponse = elements[4];
				}
				else
				{
					temp.Reponse = "";
				}
				temp.Modification = bool.Parse(elements[5]);
				Questions.Add(temp);
			}
		}
	}

	void load_JustificationMateriel()
	{
		string[] file_line = load_File(MATERIELJUSTIFICATION_FILENAME);

		string Header = null;

		int size = file_line.Length;
		//Debug.Log("Number of line of the configuration file : " + size);
		int marker = 0;

		for (int i = 0; i < size; i++)
		{
			string[] elements = file_line[i].Split(';');
			if (elements[0].CompareTo("") != 0)
			{
				Header = elements[0];
				marker = i;
				//Debug.Log("header : " + Header);
				//Debug.Log(Header.CompareTo("Question"));
				//Debug.Log("i : " + i + " ; marker : " + marker);
			}
			if (Header.CompareTo("Lien_materiel_justification") == 0 && i > marker)
			{
				JustificationMateriel temp = new JustificationMateriel();
				temp.ID = int.Parse(elements[1]);
				temp.Objet = int.Parse(elements[2]);
				temp.Materiel = int.Parse(elements[3]);
				temp.Justification = int.Parse(elements[4]);
				temp.Score = int.Parse(elements[5]);
				JustificationLienMateriel.Add(temp);
			}
		}
	}

	void load_CoherenceMateriel()
	{
		string[] file_line = load_File(COHERENCE_FILENAME);

		string Header = null;

		int size = file_line.Length;
		//Debug.Log("Number of line of the configuration file : " + size);
		int marker = 0;

		for (int i = 0; i < size; i++)
		{
			string[] elements = file_line[i].Split(';');
			if (elements[0].CompareTo("") != 0)
			{
				Header = elements[0];
				marker = i;
				//Debug.Log("header : " + Header);
				//Debug.Log(Header.CompareTo("Question"));
				//Debug.Log("i : " + i + " ; marker : " + marker);
			}
			if (Header.CompareTo("Coherence materiel technique outils") == 0 && i > marker)
			{
				CoherenceMaterielTechniqueOutils temp = new CoherenceMaterielTechniqueOutils();
				temp.ID = int.Parse(elements[1]);
				temp.Objet = int.Parse(elements[2]);
				//Debug.Log("parse a element of coherence : " + temp.ID + " ; " + temp.Materiels);

				temp.Materiels = int.Parse(elements[3]);
				temp.Technique = int.Parse(elements[4]);
				temp.Outils = int.Parse(elements[5]);
				temp.Score = int.Parse(elements[6]);
				CoherenceMaterielTechniqueOutils.Add(temp);
			}
		}
	}

	void load_Objets()
	{
		string[] file_line = load_File(OBJETS_FILENAME);

		string Header = null;

		int size = file_line.Length;
		//Debug.Log("Number of line of the configuration file : " + size);
		int marker = 0;

		for (int i = 0; i < size; i++)
		{
			string[] elements = file_line[i].Split(';');
			if (elements[0].CompareTo("") != 0)
			{
				Header = elements[0];
				marker = i;
				//Debug.Log("header : " + Header);
				//Debug.Log(Header.CompareTo("Question"));
				//Debug.Log("i : " + i + " ; marker : " + marker);
			}
			if (Header.CompareTo("Objets") == 0 && i > marker)
			{

				Objets temp = new Objets();
				temp.ID = int.Parse(elements[1]);
				temp.Nom = elements[2];
				string[] forme;
				for (int j = 3; j < elements.Length - 1; j++)
				{
					//Debug.Log("element j : " + elements[j]);
					List<int> liste = new List<int>();

					if (elements[j].CompareTo("") != 0)
					{
						//Debug.Log("element j : " + elements[j]);
						forme = elements[j].Split(',');
						foreach (string thing in forme)
						{
							liste.Add(int.Parse(thing));
						}
					}
					switch (j)
					{
						case 3:
							temp.Justification_materiel = liste;
							break;
						case 4:
							temp.Coherence_materiel_technique_outils = liste;
							break;
						default:
							break;
					}
				}
				temp.Image = elements[5];
				Objets.Add(temp);
			}
		}
	}

	void load_Materiaux()
	{
		string[] file_line = load_File(MATERIEL_FILENAME);

		string Header = null;

		int size = file_line.Length;
		//Debug.Log("Number of line of the configuration file : " + size);
		int marker = 0;

		for (int i = 0; i < size; i++)
		{
			string[] elements = file_line[i].Split(';');
			if (elements[0].CompareTo("") != 0)
			{
				Header = elements[0];
				marker = i;
				//Debug.Log("header : " + Header);
				//Debug.Log(Header.CompareTo("Question"));
				//Debug.Log("i : " + i + " ; marker : " + marker);
			}
			if (Header.CompareTo("Sous_categorie_Materiaux") == 0 && i > marker)
			{
				Item temp_item = new Item();
				temp_item.ID = int.Parse(elements[1]);
				temp_item.Nom = elements[2];
				temp_item.Image = elements[3];
				temp_item.Min_etoile = int.Parse(elements[4]);
				SousMateriaux.Add(temp_item);
			}
			else if (Header.CompareTo("Materiaux") == 0 && i > marker)
			{
				Materiaux temp_materiaux = new Materiaux();
				temp_materiaux.ID = int.Parse(elements[1]);
				temp_materiaux.Nom = elements[2];

				temp_materiaux.Image = elements[3];
				temp_materiaux.Min_etoile = int.Parse(elements[4]);
				temp_materiaux.SousCategorie = int.Parse(elements[5]);
				Materiaux.Add(temp_materiaux);
			}
		}
	}

	void load_Item(string file_name, List<Item> list_item)
	{
		string[] file_line = load_File(file_name);

		string Header = null;

		int size = file_line.Length;
		//Debug.Log("Number of line of the configuration file : " + size);
		int marker = 0;

		for (int i = 0; i < size; i++)
		{
			string[] elements = file_line[i].Split(';');
			if (elements[0].CompareTo("") != 0)
			{
				Header = elements[0];
				marker = i;
				//Debug.Log("header : " + Header);
				//Debug.Log(Header.CompareTo("Question"));
				//Debug.Log("i : " + i + " ; marker : " + marker);
			}
			if (i > marker)
			{
				Item temp = new Item();
				temp.ID = int.Parse(elements[1]);
				temp.Nom = elements[2];
				temp.Image = elements[3];
				temp.Min_etoile = int.Parse(elements[4]);

				list_item.Add(temp);
			}
		}
	}

	void load_taille()
	{
		string[] file_line = load_File(TAILLE_FILENAME);

		string Header = null;

		int size = file_line.Length;
		//Debug.Log("Number of line of the configuration file : " + size);
		int marker = 0;

		for (int i = 0; i < size; i++)
		{
			string[] elements = file_line[i].Split(';');
			if (elements[0].CompareTo("") != 0)
			{
				Header = elements[0];
				marker = i;
				//Debug.Log("header : " + Header);
				//Debug.Log(Header.CompareTo("Question"));
				//Debug.Log("i : " + i + " ; marker : " + marker);
			}
			if (Header.CompareTo("Taille_objet") == 0 && i > marker)
			{
				Taille temp = new Taille();
				temp.ID = int.Parse(elements[1]);
				temp.Nom = elements[2];
				temp.Dimension = int.Parse(elements[3]);
				temp.Image = elements[4];
				temp.Min_etoile = int.Parse(elements[5]);

				TailleObjet.Add(temp);
			}
		}

	}
	/*
	void load() {
		string _file = "";
		if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
		{
			//Debug.Log("in windows or mac platform");
			WWW request = new WWW("file://" + Application.streamingAssetsPath + "/"+path);
			while (!request.isDone) { }
			_file = request.text as string;
		}
		else if (Application.platform == RuntimePlatform.Android)
		{
			//Debug.Log("in android platform");
			WWW request = new WWW("jar:file://" + Application.dataPath + "!/assets/" + path);
			while (!request.isDone) { }
			_file = request.text as string;
		}
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			//Debug.Log("in iphone plateform");
			WWW request = new WWW("file://" + Application.streamingAssetsPath + "/" + path);
			while (!request.isDone) { }
			_file = request.text as string;
		}

		string Header = null;

		string[] file = Regex.Split(_file, "\n");


		int size = file.Length;
		//Debug.Log("Number of line of the configuration file : " + size);
		int marker = 0;

		for (int i = 0; i < size; i++)
		{
			string[] elements = file[i].Split(';');
			if (elements[0].CompareTo("") != 0) {
				Header = elements[0];
				marker = i;
				//Debug.Log("header : " + Header);
				//Debug.Log(Header.CompareTo("Question"));
				//Debug.Log("i : " + i + " ; marker : " + marker);
			}
			if (Header.CompareTo("Profils") == 0 && i > marker)
			{
				//Debug.Log("in profils");
				Profils.ID = int.Parse(elements[1]);
				Profils.Etoiles_actuel = int.Parse(elements[2]);
				Profils.Etoiles_depenser = int.Parse(elements[3]);
			}
			else if (Header.CompareTo("Historique") == 0 && i > marker)
			{
				//Debug.Log("in historique");

				Historique temp = new Historique();
				temp.ID = int.Parse(elements[1]);
				temp.Objet = int.Parse(elements[2]);

				string[] forme;
				for (int j = 3; j < elements.Length - 1; j++)
				{
					List<int> liste = new List<int>();

					if (elements[j].CompareTo("") != 0)
					{
						forme = elements[j].Split(',');
						foreach (string thing in forme)
						{
							liste.Add(int.Parse(thing));
						}
					}
					switch (j)
					{
						case 3:
							temp.FormeActive = liste;
							break;
						case 4:
							temp.FormeInactive = liste;
							break;
						case 5:
							temp.TailleActive = liste;
							break;
						case 6:
							temp.TailleInactive = liste;
							break;
						case 7:
							temp.MateriauxActif = liste;
							break;
						case 8:
							temp.MateriauxInactif = liste;
							break;
						case 9:
							temp.SousMateriauxActif = liste;
							break;
						case 10:
							temp.SousMateriauxInactif = liste;
							break;
						case 11:
							temp.OutilsActif = liste;
							break;
						case 12:
							temp.OutilsInactif = liste;
							break;
						case 13:
							temp.TechniqueActive = liste;
							break;
						case 14:
							temp.TechniqueInactive = liste;
							break;
						default:
							break;
					}
				}
				Historique.Add(temp);
			}
			else if (Header.CompareTo("Objet") == 0 && i > marker)
			{
				//Debug.Log("in object");

				Objets temp = new Objets();
				temp.ID = int.Parse(elements[1]);
				temp.Nom = elements[2];
				string[] forme;
				for (int j = 3; j < elements.Length - 3; j++)
				{
					//Debug.Log("element j : " + elements[j]);
					List<int> liste = new List<int>();

					if (elements[j].CompareTo("") != 0)
					{
						//Debug.Log("element j : " + elements[j]);
						forme = elements[j].Split(',');
						foreach (string thing in forme)
						{
							liste.Add(int.Parse(thing));
						}
					}
					switch (j)
					{
						case 3:
							temp.Association = liste;
							break;
						case 4:
							temp.Conflit = liste;
							break;
						case 5:
							temp.FormeActive = liste;
							break;
						case 6:
							temp.FormeInactive = liste;
							break;
						case 7:
							temp.TailleActive = liste;
							break;
						case 8:
							temp.TailleInactive = liste;
							break;
						case 9:
							temp.MateriauxActif = liste;
							break;
						case 10:
							temp.MateriauxInactif = liste;
							break;
						case 11:
							temp.SousMateriauxActif = liste;
							break;
						case 12:
							temp.SousMateriauxInactif = liste;
							break;
						case 13:
							temp.OutilsActif = liste;
							break;
						case 14:
							temp.OutilsInactif = liste;
							break;
						case 15:
							temp.TechniqueActive = liste;
							break;
						case 16:
							temp.TechniqueInactive = liste;
							break;
						case 17:
							temp.AssociationJustification = liste;
							break;
						default:
							break;
					}
				}
				//Debug.Log("bool : " + elements[15]);
				//Debug.Log(bool.Parse(elements[15]));
				temp.Modification = bool.Parse(elements[18]);
				//Debug.Log("image : " + elements[19]);
				temp.Image = elements[19];
				Objets.Add(temp);
			}
			else if (Header.CompareTo("Association_justification") == 0 && i > marker)
			{
				//Debug.Log("in association justification");

				Association_justification temp = new Association_justification();
				temp.ID = int.Parse(elements[1]);
				temp.Materiaux = int.Parse(elements[2]);

				for (int j = 3; j < elements.Length - 1;j++){
					List<int> liste = new List<int>();
					if (elements[j].CompareTo("") != 0)
					{
						string[] forme = elements[j].Split(',');
						foreach (string thing in forme)
						{
							liste.Add(int.Parse(thing));
						}
					}
					switch(j)
					{
						case 3:
							temp.JustificationActive = liste;
							break;
						case 4:
							temp.JustificaionInactive = liste;
							break;
						default:
							break;
					}
				}
				AssociationJustifications.Add(temp);

			}
			else if (Header.CompareTo("Association") == 0 && i > marker)
			{
				//Debug.Log("in association");

				Association temp = new Association();
				temp.ID = int.Parse(elements[1]);
				temp.Materiaux = int.Parse(elements[2]);

				if (elements[3].CompareTo("") != 0)
				{
					string[] forme = elements[3].Split(',');
					List<int> liste = new List<int>();
					foreach (string thing in forme)
					{
						liste.Add(int.Parse(thing));
					}
					temp.Outils = liste;
				}

				if (elements[4].CompareTo("") != 0)
				{
					string[] forme = elements[4].Split(',');
					List<int> liste = new List<int>();
					foreach (string thing in forme)
					{
						liste.Add(int.Parse(thing));
					}
					temp.Technique = liste;
				}
				Associations.Add(temp);
			}
			else if (Header.CompareTo("Question") == 0 && i > marker)
			{
				Question temp = new Question();
				temp.ID = int.Parse(elements[1]);
				temp.Nom = elements[2];
				if(elements[3] != null)
				{
				   temp.Reponse = elements[3];
				}
				else{
					temp.Reponse = "";
				}
				temp.Modification = bool.Parse(elements[4]);
				Questions.Add(temp);
			}
			else if (i > marker)
			{
				//Debug.Log("in others");


				Item temp = new Item();
				temp.ID = int.Parse(elements[1]);
				temp.Nom = elements[2];

				temp.Image = elements[3];
				temp.Min_etoile = int.Parse(elements[4]);

				if (Header.CompareTo("Forme") == 0 && i > marker)
				{
					this.Forme.Add(temp);
				}
				else if (Header.CompareTo("Taille_objet") == 0 && i > marker)
				{
					TailleObjet.Add(temp);
				}
				else if (Header.CompareTo("Justification_materiel") == 0 && i > marker)
				{
					JustificationMateriel.Add(temp);
				}
				else if (Header.CompareTo("Outils") == 0 && i > marker)
				{
					Outils.Add(temp);
				}
				else if (Header.CompareTo("Technique") == 0 && i > marker)
				{
					Technique.Add(temp);
				}
				else if (Header.CompareTo("Sous_categorie_Materiaux") == 0 && i > marker)
				{
					SousMateriaux.Add(temp);
				}
				else if (Header.CompareTo("Materiaux") == 0 && i > marker)
				{
					Materiaux temp_materiaux = new Materiaux();
					temp_materiaux.ID = int.Parse(elements[1]);
					temp_materiaux.Nom = elements[2];

					temp_materiaux.Image = elements[3];                    
					temp_materiaux.Min_etoile = int.Parse(elements[4]);

					if (elements[5].CompareTo("") != 0)
					{
						string[] forme = elements[5].Split(',');
						List<int> liste = new List<int>();
						foreach (string thing in forme)
						{
							liste.Add(int.Parse(thing));
						}
						temp_materiaux.Association = liste;
					}
					temp_materiaux.SousCategorie = int.Parse(elements[6]);
					if(elements[7].CompareTo("") != 0){
						string[] justificationActive = elements[7].Split(',');
						List<int> liste = new List<int>();
						foreach (string thing in justificationActive)
						{
							liste.Add(int.Parse(thing));
						}
						temp_materiaux.JustificationActive = liste;
					}
					if (elements[8].CompareTo("") != 0)
					{
						string[] justificationInactive = elements[8].Split(',');
						List<int> liste = new List<int>();
						foreach (string thing in justificationInactive)
						{
							liste.Add(int.Parse(thing));
						}
						temp_materiaux.JustificationInactive = liste;
					}

					Materiaux.Add(temp_materiaux);
				}
			}
		}
	}*/

	void Save()
	{

	}
}
