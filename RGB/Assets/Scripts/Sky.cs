using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class Sky : MonoBehaviour
{
    Material skyboxMaterial;
    public Toggle checkBoxSky;
    public TextMeshProUGUI dayText;
    public Light light;
    LightingSettings lightingSettings;

    // Start is called before the first frame update
    void Start()
    {
        skyboxMaterial = RenderSettings.skybox;
        dayText.text = "Night";
        dayText.color = Color.black;
        ChangeSky(checkBoxSky.isOn);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(skyboxMaterial.GetFloat("_Blend") == 0)
        {
            skyboxMaterial.SetTextureOffset("_FrontTex", skyboxMaterial.GetTextureOffset("_FrontTex") + new Vector2(0.01f, 0) * Time.deltaTime);
            skyboxMaterial.SetTextureOffset("_RightTex", skyboxMaterial.GetTextureOffset("_RightTex") + new Vector2(0.01f, 0) * Time.deltaTime);
            skyboxMaterial.SetTextureOffset("_LeftTex", skyboxMaterial.GetTextureOffset("_LeftTex") + new Vector2(0.01f, 0) * Time.deltaTime);
            skyboxMaterial.SetTextureOffset("_BackTex", skyboxMaterial.GetTextureOffset("_BackTex") + new Vector2(0.01f, 0) * Time.deltaTime);
        }
        else if(skyboxMaterial.GetFloat("_Blend") == 1)
        {
            skyboxMaterial.SetTextureOffset("_FrontTex2", skyboxMaterial.GetTextureOffset("_FrontTex2") + new Vector2(0.01f, 0) * Time.deltaTime);
            skyboxMaterial.SetTextureOffset("_RightTex2", skyboxMaterial.GetTextureOffset("_RightTex2") + new Vector2(0.01f, 0) * Time.deltaTime);
            skyboxMaterial.SetTextureOffset("_LeftTex2", skyboxMaterial.GetTextureOffset("_LeftTex2") + new Vector2(0.01f, 0) * Time.deltaTime);
            skyboxMaterial.SetTextureOffset("_BackTex2", skyboxMaterial.GetTextureOffset("_BackTex2") + new Vector2(0.01f, 0) * Time.deltaTime);
        }
    }

    public void ChangeSky(bool isOn)
    {
        if (isOn == false) //Nicht
        {
            skyboxMaterial.SetFloat("_Blend", 1);
            dayText.text = "Day";
            //dayText.color = new Color(1f, .93f, .8f);
            dayText.color = new Color(.8f, .93f, 1f);
            Moonlight();
        }
        else if (isOn == true) //Afternoon
        {
            skyboxMaterial.SetFloat("_Blend", 0);
            dayText.text = "Night";
            dayText.color = Color.black;
            Sunlight();

        }
            
    }

    public void Moonlight()
    {
        light.color = new Color(0.8773585f, 0.8465682f, 0.4924795f, 0.5450981f);
        light.intensity = 0.5f;
        RenderSettings.fog = true;
        RenderSettings.fogColor = Color.black;
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogStartDistance = 1;
        RenderSettings.fogEndDistance = 40;

    }

    public void Sunlight()
    {
        light.color = new Color(0.8584906f, 0.7819723f, 0.6114721f, 1);
        light.intensity = 1.2f;
        RenderSettings.fog = false;
        RenderSettings.reflectionIntensity = 0.4f;

    }
}
