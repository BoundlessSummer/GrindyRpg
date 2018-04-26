using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class FloatingDamageNumbers : MonoBehaviour {

    //To make one of these appear:
    //var clone = (GameObject)Instantiate(FloatingDamageNumber, hitPoint.position,  Quaternion.Euler(Vector3.zero));

    public float moveSpeed;
    public float damageNumber;
    public Text displayText;

    public float duration;

	// Use this for initialization
	void Start () {
        moveSpeed = 1;

        duration = 1f;
	}
	
	// Update is called once per frame
	void Update () {
        displayText.text = "" + damageNumber;
        transform.position = new Vector3(transform.position.x, transform.position.y + (moveSpeed * Time.deltaTime), transform.position.z);

        duration -= Time.deltaTime;
        if(duration <= 0f)
        {
            Destroy(gameObject);
        }
	}
}
