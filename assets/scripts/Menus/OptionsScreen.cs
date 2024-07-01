using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScreen : MonoBehaviour
{
    // The top-level menus
    public GameObject optionsMenu;
    public GameObject pauseMenu;
    public GameObject mainMenu;
    // The different menus within "options"
    public GameObject optionsUI;
    public GameObject graphicsUI;
    public GameObject gameplayUI;
    public Toggle fullscreenTog, vsyncTog;

    public List<AspectRatio> ratio = new List<AspectRatio>();
    public List<ResItems4x3> resolution4x3 = new List<ResItems4x3>();
    public List<ResItems16x9> resolution16x9 = new List<ResItems16x9>();
    private int horizontal, vertical, i, selectedRatio;
    public Text aspectRatioLabel;
    public Text resolutionLabel;

    //This is the Dropdown
    public Dropdown Dropdown;

    void Start()
    {
        Dropdown.ClearOptions();

        fullscreenTog.isOn = Screen.fullScreen;

        if(QualitySettings.vSyncCount == 0)
        {
            vsyncTog.isOn = false;
        }
        else
        {
            vsyncTog.isOn = true;
        }

        if (Camera.main.aspect < 1.7)
        {
            selectedRatio = 0;
            Debug.Log("4:3");
        }
        else
        {
            selectedRatio = 1;
            Debug.Log("16:9");
        }

        UpdateAspectLabel();

        if (selectedRatio == 0)
        {
            Reso4x3();
        }
        else
        {
            Reso16x9();
        }
        
        ToggleRes();
    }
    
    public void AspectLeft()
    {
        selectedRatio--;
        if(selectedRatio < 0)
        {
            selectedRatio = 0;
        }
        UpdateAspectLabel();
        ToggleRes();
    }
    public void AspectRight()
    {
        selectedRatio++;
        if(selectedRatio >= ratio.Count - 1)
        {
            selectedRatio = ratio.Count -1;
        }
        UpdateAspectLabel();
        ToggleRes();
    }
    public void UpdateAspectLabel()
    {
        aspectRatioLabel.text = ratio[selectedRatio].horizontal.ToString() + " x " + ratio[selectedRatio].vertical.ToString();
    }
    public void ToggleRes()
    {
        if(selectedRatio == 0)
        {
            // show 4x3 list
            Dropdown.options.Clear();
            foreach (var ResItems4x3 in resolution4x3)
            {
                var resItems4x3 = ResItems4x3.horizontal4x3.ToString() + " x " + ResItems4x3.vertical4x3.ToString();
                Dropdown.options.Add(new Dropdown.OptionData(resItems4x3));
            }
            Dropdown.RefreshShownValue();
        }
        else
        {
            // show 16x9 list   
            Dropdown.options.Clear();
            foreach (var ResItems16x9 in resolution16x9)
            {
                var resItems16x9 = ResItems16x9.horizontal16x9.ToString() + " x " + ResItems16x9.vertical16x9.ToString();
                Dropdown.options.Add(new Dropdown.OptionData(resItems16x9));
            }
            Dropdown.RefreshShownValue();
        }
        
        SelectedRes();
    }
    public void Reso4x3()
    {
        for (int i = 0; i < resolution4x3.Count; i++) ;
            {
                if (Screen.width == resolution4x3[i].horizontal4x3 && Screen.height == resolution4x3[i].vertical4x3)
                {
                    horizontal = resolution4x3[i].horizontal4x3;
                    vertical = resolution4x3[i].vertical4x3;
                }
            }
    }
    public void Reso16x9()
    {
        for (int i = 0; i < resolution16x9.Count; i++) ;
            {
                if (Screen.width == resolution16x9[i].horizontal16x9 && Screen.height == resolution16x9[i].vertical16x9)
                {
                    horizontal = resolution16x9[i].horizontal16x9;
                    vertical = resolution16x9[i].vertical16x9;
                }
            }
    }
    public void SelectedRes()
    {
        int i = Dropdown.value;
        if(selectedRatio == 0)
        {
            horizontal = resolution4x3[i].horizontal4x3;
            vertical = resolution4x3[i].vertical4x3;            
        }
        else
        {
            horizontal = resolution16x9[i].horizontal16x9;
            vertical = resolution16x9[i].vertical16x9;
        }
        UpdateResLabel();
        Debug.Log("Hori = " + horizontal + " .. vert = " + vertical + " ..ratio = " + selectedRatio + " int = " + i);
    }
    public void UpdateResLabel()
    {
        resolutionLabel.text = horizontal.ToString() + " x " + vertical.ToString();
    }
    public void ApplyGraphics()
    {
        Debug.Log("Hori = " + horizontal + " .. vert = " + vertical);
        if(vsyncTog.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }

        Screen.SetResolution(horizontal, vertical, fullscreenTog.isOn);
        
        Debug.Log("Applying settings");
    }
    public void OptionsUI()
    {
        optionsUI.SetActive(true);
        graphicsUI.SetActive(false);
        // gameplayUI.SetActive(false);
    }
    public void GraphicsUI()
    {
        graphicsUI.SetActive(true);
        optionsUI.SetActive(false);
        // gameplayUI.SetActive(false);
    }
    public void GameplayUI()
    {
        gameplayUI.SetActive(true);
        optionsUI.SetActive(false);
        graphicsUI.SetActive(false);
    }
    public void HideGraphicsUI()
    {
        optionsUI.SetActive(true);
        graphicsUI.SetActive(false);
    }
    public void PauseMenu()
	{
        pauseMenu.SetActive(true);
        optionsUI.SetActive(false);
	}
    public void MainMenu()
	{
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
	}
}
[System.Serializable]
public class AspectRatio
{
    public int horizontal, vertical;
}

[System.Serializable]
public class ResItems4x3
{
    public int horizontal4x3, vertical4x3;
}

[System.Serializable]
public class ResItems16x9
{
    public int horizontal16x9, vertical16x9;
}