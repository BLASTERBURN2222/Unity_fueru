using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAGICSHIELD : MonoBehaviour
{
	private int lifetime;
    // Start is called before the first frame update
    void Start()
    {
		lifetime = 60;
    }

    // Update is called once per frame
    void Update()
    {
		if (GG.pause == 1) return;

		lifetime--;
		if(lifetime <= 0){
			Destroy(this.gameObject);
		}
    }

	void OnTriggerEnter2D(Collider2D other)
	{

		if (other.gameObject.tag == "TAG_ENESHOT")
		{

			GG.sound_play("SE_H");
			lifetime = 1;

			Destroy(other.gameObject);


		}




	}




}
