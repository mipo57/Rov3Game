using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterOpacity : MonoBehaviour {

    public float waterLevel = 12f;
    private bool isUnderwater;
    public Color waterColor = new Color(0.22f, 0.65f, 0.77f, 0.5f);
    public float waterFog = 0.2f;

	// Use this for initialization
	void Start () {
        RenderSettings.fogMode = FogMode.Exponential;
        RenderSettings.fogColor = waterColor;
        RenderSettings.fogDensity = waterFog;
    }
	
	// Update is called once per frame
	void Update () {
        if ((transform.position.y < waterLevel) != isUnderwater)
        {
            isUnderwater = (transform.position.y < waterLevel);
            if (isUnderwater) SetUnderwater();
            else SetNormal();
        }
	}

    void SetNormal()
    {
        RenderSettings.fog = false;
        Debug.Log("Normal");
    }

    void SetUnderwater()
    {
        RenderSettings.fog = true;
        Debug.Log("Underwater");
    }
}
 