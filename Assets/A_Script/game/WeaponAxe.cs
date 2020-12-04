using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAxe : MonoBehaviour
{


	private int lifetime_;
	private int dead_flg;

	private Rigidbody2D m_rigidbody2D;

	private GameObject pf_baku;
	private float jumpPower = 700f;

	List<GameObject> hitobj = new List<GameObject>();

	private void Awake()
	{
		pf_baku = (GameObject)Resources.Load("BAKU");

		m_rigidbody2D = GetComponent<Rigidbody2D>();
		hitobj.Clear();
	}


	// Start is called before the first frame update
	void Start()
    {
		lifetime_ = 360;
		dead_flg = 0;
		m_rigidbody2D.AddForce(new Vector2(150f, jumpPower));


	}

	// Update is called once per frame
	void Update()
    {
		if (GG.pause == 1) return;

		transform.Rotate(new Vector3(0, 0, 5.0f));


		lifetime_--;
		if (lifetime_ <= 0)
		{
			dead_flg = 1;
			Destroy(this.gameObject);
		}

	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (dead_flg == 1) return;
		if (other.gameObject.tag == "TAG_ENEMY")
		{

			if (hitobj.Contains(other.gameObject))
			{
			}
			else
			{
				hitobj.Add(other.gameObject);

				Instantiate(pf_baku, this.transform.position, Quaternion.identity);

				other.gameObject.SendMessage("damage", 3);
				GG.sound_play("SE_1");

			}
		}

		if (other.gameObject.tag == "TAG_BLOCK")
		{
			GG.sound_play("SE_BAKU");

			Instantiate(pf_baku, other.transform.position, Quaternion.identity);

			Destroy(other.gameObject);
			Destroy(this.gameObject);
			dead_flg = 1;
		}

	}


}
