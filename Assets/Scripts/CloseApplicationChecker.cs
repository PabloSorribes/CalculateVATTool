using UnityEngine;

public class CloseApplicationChecker : MonoBehaviour
{
	[SerializeField] private GameObject closeApplicationUI;

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && !closeApplicationUI.activeSelf)
		{
			ToggleCloseApplicationUI(true);
		}
		else if (Input.GetKeyDown(KeyCode.Escape) && closeApplicationUI.activeSelf)
		{
			ToggleCloseApplicationUI(false);
		}
		else if (closeApplicationUI.activeSelf && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
		{
			CloseApplication();
		}
	}

	public void ToggleCloseApplicationUI(bool activate)
	{
		closeApplicationUI.SetActive(activate);
	}

	public void CloseApplication()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
	Application.OpenURL(webplayerQuitURL);
#else
	Application.Quit();
#endif
	}
}
