using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Hone : ENE_COMMON
{

	private int lifetime;
	private int move_mode;
	private int move_cnt;

	private float goal_x = 0.0f;
	private float goal_y = 0.0f;
	private float goal_time = 1.0f;

	private float vxf;
	private float vyf;


	private void Awake()
	{
		init_zako(this.gameObject, 0);
		set_hp_score_exp(2, 10, 0);
		move_mode = 0;
		lifetime = 1100;
		move_cnt = 0;
	}

	public void set_goal_xyt(float gx, float gy,float tm) {
		goal_x = gx;
		goal_y = gy;
		goal_time = tm;
	}




	// Start is called before the first frame update
	void Start()
	{
		vxf = 0.05f;
		vyf = -0.01f;

		Vector3 pos = transform.position;

		Sequence seq = DOTween.Sequence();
		seq.Append(
			transform.DOLocalMove(new Vector3(goal_x, goal_y, pos.z), goal_time)
		);
		seq.Join(
			transform.DOLocalRotate(new Vector3(0, 0, 630f), goal_time, RotateMode.FastBeyond360)
		);
		seq.OnComplete(() => hone_goal());
		seq.Play();
	}

	void hone_goal(){
		move_mode = 1;
		vyf = -0.04f;
	}

	// Update is called once per frame
	void Update()
	{
		if (GG.pause == 1) return;
		EneMove();
		lifetime--;
		if (lifetime <= 0)
		{
			Destroy(this.gameObject);
		}
	}


	private void EneMove()
	{
		Vector3 pos = transform.position;

		if(move_mode == 0){
		}
		else if(move_mode == 1){
			pos.y += vyf;
		}

		transform.position = pos;
	}

}
