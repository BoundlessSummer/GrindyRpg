using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {

    public WeaponHolder theWeaponHolder;
    public GameObject sword;
    public SpriteRenderer swordSpriteRenderer;
    public Sprite nextSword;

	// Use this for initialization
	void Start () {
        swordSpriteRenderer = sword.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.U))
        {
            nextSword = theWeaponHolder.swords[1];
            swordSpriteRenderer.sprite = nextSword;
        }
        if (Input.GetKeyUp(KeyCode.Y))
        {
            nextSword = theWeaponHolder.swords[0];
            swordSpriteRenderer.sprite = nextSword;
        }
    }
}
