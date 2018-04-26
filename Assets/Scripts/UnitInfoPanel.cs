using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UnitInfoPanel : MonoBehaviour {

    public StatBlockText statBlockText;
    public GameObject portrait;
    public Image portraitImage;
    public Text DescriptionText;

    private void Awake()
    {
        statBlockText = GetComponentInChildren<StatBlockText>();
        //portraitImage = GetComponentInChildren<Image>();
        portraitImage = portrait.GetComponent<Image>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.K))
        {
            gameObject.SetActive(false);
        }
	}

    public void Set(BaseUnit unit)
    {
        statBlockText.Set(unit);

        portraitImage.sprite = unit.portraitSprite;
    }

    public void SetDescriptionText(string text_in)
    {
        DescriptionText.text = text_in;
    }
}
