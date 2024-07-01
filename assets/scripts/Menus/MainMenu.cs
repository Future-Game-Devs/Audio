using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public GameObject optionsMenu;
	public GameObject mainMenu;
	
	public string levelToLoad = "SaveSelect";

	public SceneFader sceneFader;

	public void Play ()
	{
		sceneFader.FadeTo(levelToLoad);
	}
	public void Load ()
	{

	}
	public void OptionsMenu()
	{
		optionsMenu.SetActive(true);
		mainMenu.SetActive(false);
		Debug.Log("Clicked Options");
	}

	public void Quit ()
	{
		Application.Quit();
	}
}
