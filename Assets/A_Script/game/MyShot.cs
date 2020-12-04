using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyShot : MonoBehaviour
{

	private int lifetime_;
	private int dead_flg;


	private GameObject pf_baku;


	private void Awake()
	{
		pf_baku = (GameObject)Resources.Load("BAKU");

	}



	// Start is called before the first frame update
	void Start()
	{
		lifetime_ = 120;
		dead_flg = 0;

	}

	// Update is called once per frame
	void Update()
	{
		if (GG.pause == 1) return;

		lifetime_--;
		if (lifetime_ <= 0)
		{
			GG.combo_counter = 1;
			Destroy(this.gameObject);
		}
		else
		{
			Vector3 pos = transform.position;
			pos.y += 0.2f;
			transform.position = pos;
		}










	}




	void OnTriggerEnter2D(Collider2D other)
	{
		if (dead_flg == 1) return;
		if (other.gameObject.tag == "TAG_ENEMY")
		{

			Instantiate(pf_baku, this.transform.position, Quaternion.identity);

			other.gameObject.SendMessage("damage", 1);
			GG.sound_play("SE_2");

			Destroy(this.gameObject);
		}

		if (other.gameObject.tag == "TAG_BLOCK")
		{
			GG.sound_play("SE_2");

			Instantiate(pf_baku, this.transform.position, Quaternion.identity);

			Destroy(this.gameObject);
		}

	}




}
