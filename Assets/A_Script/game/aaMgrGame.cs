using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;
using System;


public class aaMgrGame : MonoBehaviour
{


	public Image image_title_base;
	public Text text_title_hiscore;
	public Text text_title_stage;


	public Image image_gameover_base;

	public Text text_game_score;
	public Text text_game_hiscore;
	public Image image_game_base;
	public Image image_option_base;


	public GameObject pf_ene_bigslime;
	public GameObject pf_ene_bigslime_blue;
	public GameObject pf_ene_bigslime_red;
	public GameObject pf_ene_bigkani;


	public GameObject pf_myshot;
	public GameObject pf_ziki;
	public GameObject pf_block;

	public Image image_boss_hp;
	public Text text_boss_hp;
	public Text text_start;

	public Text text_stage;
	public Slider slider_volume;
	public Slider slider_pitch;

	public Image image_clear;
	public GameObject GO_ItemWindow;

	public Image image_how;
	public Image image_how1_2;
	public Image image_how2_1;
	public Image image_how2_2;

	private int title_push_button;
	public Button button_pause;
	public Image image_pause_board;

	private void Awake()
	{
		DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
		Application.targetFrameRate = 60;
		GG.first_read = 1;
		GG.stage = 1;
		GG.data_load();
		sound_volume_set(GG.SoundVolume);
		TitleFirst();
		TitleInit();
	}

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		soundsound.sound_play();
	}

	void shotset(float x,float y){
		Vector3 pp = new Vector3(x, y, 0.0f);
		GameObject obj_zikishot = (GameObject)Instantiate(pf_myshot, pp, Quaternion.Euler(0, 0, 45));

	}

	//------------------- title -------------
	void TitleFirst()
	{
		image_title_base.gameObject.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

	}


	void TitleInit()
	{
		Time.timeScale = 1.0f;

		pause_button_set(false);
		pause_menu_set(false);
		Option_First();
		soundtest_init();
		howto_init();
		//BGMを止める
		soundsound.playsnd2("BGM_GAME", 1);
		GG.GameMode = GG.GAMEMODE_TITLE;
		boss_hp_gage_off();
		text_start_set(" ");
		put_text_title_hiscore();
		title_push_button = 1;
		text_game_score.text = " ";
		text_game_hiscore.text = " ";
		title_stage_put();

	}

	void put_text_title_hiscore()
	{
		text_title_hiscore.text = "HI:" + (GG.hiscore.ToString());
	}

	public void push_title_ranking()
	{
		if (title_push_button == 1)
		{
			GG.sound_play("SE_BUTTON");

			score_ranking_start(GG.hiscore);
		}
	}


	public void push_start_button()
	{
		//スタートボタン
		if (title_push_button == 1)
		{
			GG.sound_play("SE_START");
			title_push_button = 0;
			title_move_up();
		}
	}

	public void push_option_button(){
		if(title_push_button == 1){
			title_push_button = 2;

			Observable.Timer(TimeSpan.FromSeconds(0.5f)).Subscribe(_ =>
				image_option_base.gameObject.transform.DOLocalMove(new Vector3(0.0f, 0.0f, 0.0f), 0.2f).SetEase(Ease.InOutQuart)
				.OnComplete(() => push_option_button_move_completed())

			);


		}
	}

	void push_option_button_move_completed(){
		soundsound.OnPitchSliderChanged(0);
		GG.sound_play("BGM_GAME");

	}

	public void push_option_return(){
		GG.data_save();
		title_push_button = 1;
		image_option_base.gameObject.transform.DOLocalMove(new Vector3(0.0f, -2000.0f, 0.0f), 0.2f).SetEase(Ease.InOutQuart);
		soundsound.playsnd2("BGM_GAME", 1);
	}


	void title_move_up()
	{
		image_title_base.gameObject.transform.DOLocalMove(new Vector3(0.0f, 1500.0f, 0.0f), 1f).SetEase(Ease.InOutQuart)
		.OnComplete(() => title_move_up_completed());
	}

	void title_move_up_completed()
	{
		GG.score = 0;
		GameInit();
		GG.combo_counter = 1;


	}


	void title_move_down()
	{
		image_title_base.gameObject.transform.DOLocalMove(new Vector3(0.0f, 0.0f, 0.0f), 1f).SetEase(Ease.InOutQuart)
		.OnComplete(() => title_move_down_completed());
	}

	void title_move_down_completed()
	{
		TitleInit();
	}



	public void push_title_left(){
		if (title_push_button != 1) return;
		if (GG.stage > 1)
		{
			GG.stage--;
			GG.sound_play("SE_SWITCH1");
			title_stage_put();
		}
	}

	public void push_title_right()
	{
		if (title_push_button != 1) return;
		if (GG.stage < GG.stage_max)
		{
			GG.stage++;
			GG.sound_play("SE_SWITCH1");
			title_stage_put();
		}
	}


	void title_stage_put(){
		text_title_stage.text = GG.stage.ToString();
	}



	void over_board_move_start()
	{
		image_gameover_base.gameObject.transform.DOLocalMove(new Vector3(0.0f, 0.0f, 0.0f), 1f).SetEase(Ease.InOutQuart)
		.OnComplete(() => over_board_move_start_completed());

	}

	void over_board_move_start_completed()
	{
		if(GG.score > GG.hiscore)
		{
			GG.hiscore = GG.score;
			GG.data_save();
		}
		score_ranking_start(GG.hiscore);

	}

	public void push_over_totitle()
	{
		image_gameover_base.gameObject.transform.DOLocalMove(new Vector3(1000.0f, 0.0f, 0.0f), 1f).SetEase(Ease.InOutQuart)
		.OnComplete(() => push_over_totitle_completed());

	}

	void push_over_totitle_completed()
	{
	}


	void GameInit()
	{
		pause_button_set(true);
		pause_menu_set(false);
		GG.pause = 0;
		soundsound.OnPitchSliderChanged(0);
		howto_init();
		GO_ItemWindow.gameObject.SendMessage("window_item_set", -1);
		GG.window_item = 0;


		GG.minislime_z_cnt = 0.0f;
		GG.shield_z_cnt = 0.0f;
		clear_out();
		GG.GameMode = GG.GAMEMODE_GAME;
		GG.item_cnt = 0;

		put_game_score();
		put_game_hiscore();
		put_text_stage();
		enemyset(GG.stage);
		GG.sound_play("BGM_GAME");

		float px = -3.28f;
		float py = -7.2f;
		for(int i = 0;i < 11;i++){
			zikiset(px,py+ UnityEngine.Random.Range(-0.08f, 0.08f));
			px += 0.64f;

		}
		howto_call();
	}



	void Option_First(){
		image_option_base.gameObject.transform.localPosition = new Vector3(0.0f, -2000.0f, 0.0f);
	}





	public void push_game_plus()
	{
		GG.score += 100;
		put_game_score();
	}

	public void push_game_minus()
	{
		GG.score -= 100;
		put_game_score();
	}

	public void score_add(int pt)
	{
		if ((GG.GameMode == GG.GAMEMODE_GAME)||
		(GG.GameMode == GG.GAMEMODE_CLEAR))
		{
			GG.score += pt;
			put_game_score();
		}
	}


	void put_game_score()
	{
		text_game_score.text = "SCORE:"+GG.score.ToString();
	}

	void put_game_hiscore(){
		text_game_hiscore.text = "HI:"+GG.hiscore.ToString();
	}

	void score_ranking_start(int sc)
	{
		// Type == Number の場合
		naichilab.RankingLoader.Instance.SendScoreAndShowRanking(sc);

	}



	public void boss_hp_gage_put()
	{
		int hp_max = GG.boss_hp_max;
		float hp_now = GG.boss_hp;
		float hp_calc = 1.0f;
		int pitchchange = 0;
		float tmp = 0.0f;
		if (hp_now >= hp_max)
		{
			hp_calc = 1.0f;
		}
		else
		{
			hp_calc = (float)hp_now / (float)hp_max;
			if(hp_calc < 0.2f){
				tmp = (hp_calc * 1000);//0～200
				pitchchange = 200 - (int)tmp;
				soundsound.OnPitchSliderChanged(pitchchange);

			}
		}

		Vector3 pos = image_boss_hp.gameObject.transform.position;
		pos.x = -360.0f + (720.0f) * hp_calc;
		image_boss_hp.gameObject.transform.position = pos;
		put_text_boss_hp();
	}

	void put_text_boss_hp(){
		int hpp = GG.boss_hp;
		if (hpp <= 0)
		{//表示しなくする
			text_boss_hp.text = " ";
		}
		else
		{
			text_boss_hp.text = hpp.ToString();
		}
	}

	public void boss_hp_gage_on()
	{
		image_boss_hp.gameObject.SetActive(true);
		text_boss_hp.gameObject.SetActive(true);
	}
	public void boss_hp_gage_off()
	{
		image_boss_hp.gameObject.SetActive(false);
		text_boss_hp.gameObject.SetActive(false);
	}



	public void game_clear()
	{

		pause_button_set(false);
		//ＢＧＭ止める
		soundsound.playsnd2("BGM_GAME", 1);

		GG.GameMode = GG.GAMEMODE_CLEAR;
		Observable.Timer(TimeSpan.FromSeconds(2)).Subscribe(_ =>
			game2visual()
		);

	}

	void game2visual()
	{
		soundsound.OnPitchSliderChanged(0);
		GG.sound_play("SE_WIN");
		game_obj_all_kill();

		clear_start();
	}


	void game_obj_all_kill(){
		GameObject[] takaras = GameObject.FindGameObjectsWithTag("TAG_ENEMY");
		foreach (GameObject takara in takaras)
		{
			Destroy(takara.gameObject);
		}

		GameObject[] takaras2 = GameObject.FindGameObjectsWithTag("TAG_ZIKI");
		foreach (GameObject takara in takaras2)
		{
			Destroy(takara.gameObject);
		}

		GameObject[] takaras3 = GameObject.FindGameObjectsWithTag("TAG_ENESHOT");
		foreach (GameObject takara in takaras3)
		{
			Destroy(takara.gameObject);
		}

		GameObject[] takaras4 = GameObject.FindGameObjectsWithTag("TAG_BLOCK");
		foreach (GameObject takara in takaras4)
		{
			Destroy(takara.gameObject);
		}


	}


	public void exp_add(int pl)
	{
	}

	void enemyset(int st){
		if (st == 1)
		{

			enepos_bigslime(0.0f, 8.0f, 0);
			boss_hp_gage_put();
			boss_hp_gage_on();

		}
		else if (st == 2)
		{
			blockset(-0.64f, 0.0f);
			blockset(0.0f, 0.0f);
			blockset(0.64f, 0.0f);

			enepos_bigslime_blue(0.0f, 8.0f, 0);
			boss_hp_gage_put();
			boss_hp_gage_on();

		}
		else if (st == 3)
		{

			blockset(-0.64f, 0.0f);
			blockset(0.0f, 0.0f);
			blockset(0.64f, 0.0f);

			blockset(-0.64f, -0.64f);
			blockset(0.0f, -0.64f);
			blockset(0.64f, -0.64f);

			enepos_bigslime_red(0.0f, 8.0f, 0);

			boss_hp_gage_put();
			boss_hp_gage_on();
		}
		else if (st == 4)
		{
			enepos_bigkani(0.0f, 8.0f, 0);
			boss_hp_gage_put();
			boss_hp_gage_on();
		}
		else if (st == 5)
		{
			enepos_bigkani_white(0.0f, 8.0f, 0);
			boss_hp_gage_put();
			boss_hp_gage_on();
		}
		else if (st == 6)
		{
			enepos_bigkani_red(0.0f, 8.0f, 0);
			boss_hp_gage_put();
			boss_hp_gage_on();
		}

	}


	void blockset(float px,float py){
		Vector3 pos = new Vector3(px, py, 0.0f);
		Instantiate(pf_block, pos, Quaternion.identity);
	}

	void enepos_bigslime(float px, float py, int mode = 0)
	{
		Vector3 pos = new Vector3(px, py, 0.0f);
		GameObject obj_zako = (GameObject)Instantiate(pf_ene_bigslime, pos, Quaternion.identity);
	}

	void enepos_bigslime_blue(float px, float py, int mode = 0)
	{
		Vector3 pos = new Vector3(px, py, 0.0f);
		GameObject obj_zako = (GameObject)Instantiate(pf_ene_bigslime_blue, pos, Quaternion.identity);

	}

	void enepos_bigslime_red(float px, float py, int mode = 0)
	{
		Vector3 pos = new Vector3(px, py, 0.0f);
		GameObject obj_zako = (GameObject)Instantiate(pf_ene_bigslime_red, pos, Quaternion.identity);

	}

	void enepos_bigkani(float px, float py, int mode = 0)
	{
		Vector3 pos = new Vector3(px, py, 0.0f);
		GameObject obj_zako = (GameObject)Instantiate(pf_ene_bigkani, pos, Quaternion.identity);
		obj_zako.GetComponent<E_BigKani>().SetType(0);

	}

	void enepos_bigkani_white(float px, float py, int mode = 0)
	{
		Vector3 pos = new Vector3(px, py, 0.0f);
		GameObject obj_zako = (GameObject)Instantiate(pf_ene_bigkani, pos, Quaternion.identity);
		obj_zako.GetComponent<E_BigKani>().SetType(1);

	}

	void enepos_bigkani_red(float px, float py, int mode = 0)
	{
		Vector3 pos = new Vector3(px, py, 0.0f);
		GameObject obj_zako = (GameObject)Instantiate(pf_ene_bigkani, pos, Quaternion.identity);
		obj_zako.GetComponent<E_BigKani>().SetType(2);


	}


	void zikiset(float x ,float y){
		Vector3 pp = new Vector3(x, y, 0.0f);
		GameObject obj_zikishot = (GameObject)Instantiate(pf_ziki, pp, Quaternion.identity);

	}

	public void alldeadcheck(int nn){
		//全員死んでいるか ゲームオーバー
		GameObject[] takaras = GameObject.FindGameObjectsWithTag("TAG_ZIKI");
		if (takaras.Length == 1){
			//いないのでゲームオーバーに
			//0ではなく1の状態で来る
			pause_button_set(false);
			GG.GameMode = GG.GAMEMODE_OVER;
			text_start_set("GAME OVER");

			Observable.Timer(TimeSpan.FromSeconds(1)).Subscribe(_ =>
				boardstart()
			);

		}


	}

	void boardstart(){
		text_start_set(" ");
		over_board_move_start_completed();
		image_gameover_base.gameObject.transform.DOLocalMove(new Vector3(0.0f, 0.0f, 0.0f), 0.1f).SetEase(Ease.InOutQuart);
	}

	public void push_totitle_button() {
		Observable.Timer(TimeSpan.FromSeconds(0.5)).Subscribe(_ =>
			push_totitle_button_2()
		);
	}

	void push_totitle_button_2(){ 
		image_gameover_base.gameObject.transform.DOLocalMove(new Vector3(1000.0f, 0.0f, 0.0f), 0.1f).SetEase(Ease.InOutQuart);
		game_obj_all_kill();
		title_move_down();
	}



	void text_start_set(string st){
		text_start.text = st;
	}


	void put_text_stage(){
		text_stage.text = "ROUND " + (GG.stage).ToString() + "/" + (GG.stage_max).ToString();

	}

	void sound_volume_set(float vol){
		slider_volume.value = vol;
		soundsound.volumeset(vol);
	}

	public void volume_slider_set(){
		float vol = slider_volume.value;
		GG.SoundVolume = vol;
		soundsound.volumeset(vol);
		GG.sound_play("SE_SWITCH1");
	}

	public void slider_pitch_test(){
		int pitch= (int)slider_pitch.value;
		soundsound.OnPitchSliderChanged(pitch);
	}


	void clear_start(){
		image_clear.gameObject.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
	}

	void clear_out()
	{
		image_clear.gameObject.transform.localPosition = new Vector3(0.0f, 1200.0f, 0.0f);
	}


	public void push_clear_button_next(){
		GG.sound_play("SE_SWITCH1");
		GG.stage++;
		if(GG.stage == (GG.stage_max+1)){
			GG.stage = 1;//タイトルに戻ったときの表示に使うので１にする
			clear_out();
			boardstart();
		}
		else{
			GameInit();
		}
	}


	void howto_call()
	{
		if (GG.stage == 1)
		{
			Observable.Timer(TimeSpan.FromSeconds(0.25)).Subscribe(_ =>
				howto_start()
			);
		}
		else if (GG.stage == 2)
		{
			Observable.Timer(TimeSpan.FromSeconds(0.25)).Subscribe(_ =>
				howto_start2()
			);
		}
	}

	void howto_start()
	{
		Sequence seq = DOTween.Sequence();
		seq.Append(
			image_how.gameObject.transform.DOLocalMove(new Vector3(170.0f, 100.0f, 0.0f), 1f)
			.SetDelay(2.0f)
		);
		seq.Append(
			image_how.gameObject.transform.DOLocalMove(new Vector3(700.0f, 100.0f, 0.0f), 1f)
			.SetDelay(10.0f)
		);
		seq.Append(
			image_how1_2.gameObject.transform.DOLocalMove(new Vector3(170.0f, 100.0f, 0.0f), 1f)
			.SetDelay(5.0f)
		);
		seq.Append(
			image_how1_2.gameObject.transform.DOLocalMove(new Vector3(700.0f, 100.0f, 0.0f), 1f)
			.SetDelay(10.0f)
		);
		seq.Play();
	}

	void howto_start2()
	{
		Sequence seq = DOTween.Sequence();
		seq.Append(
			image_how2_1.gameObject.transform.DOLocalMove(new Vector3(170.0f, 100.0f, 0.0f), 1f)
			.SetDelay(2.0f)
		);
		seq.Append(
			image_how2_1.gameObject.transform.DOLocalMove(new Vector3(700.0f, 100.0f, 0.0f), 1f)
			.SetDelay(10.0f)
		);
		seq.Append(
			image_how2_2.gameObject.transform.DOLocalMove(new Vector3(170.0f, 100.0f, 0.0f), 1f)
			.SetDelay(2.0f)
		);
		seq.Append(
			image_how2_2.gameObject.transform.DOLocalMove(new Vector3(700.0f, 100.0f, 0.0f), 1f)
			.SetDelay(10.0f)
		);
		seq.Play();
	}


	void howto_init()
	{
		image_how.gameObject.transform.localPosition = new Vector3(700.0f, 100.0f, 0.0f);
		image_how1_2.gameObject.transform.localPosition = new Vector3(700.0f, 100.0f, 0.0f);
		image_how2_1.gameObject.transform.localPosition = new Vector3(700.0f, 100.0f, 0.0f);
		image_how2_2.gameObject.transform.localPosition = new Vector3(700.0f, 100.0f, 0.0f);
	}

	void howto_move_in()
	{
		image_how.gameObject.transform.DOLocalMove(new Vector3(170.0f, 100.0f, 0.0f), 1f);
	}

	void howto_move_out()
	{
		image_how.gameObject.transform.DOLocalMove(new Vector3(500.0f, 100.0f, 0.0f), 1f);
	}

	public Text text_soundtest;
	private int soundtest_no;
	void soundtest_init(){
		soundtest_no = 0;
	}

	public void soundtest_plus(){
		soundtest_no++;
		soundtestput();
	}

	public void soundtest_minus(){
		soundtest_no--;
		soundtestput();
	}

	void soundtestput(){
		text_soundtest.text = soundtest_no.ToString();
	}

	public void soundtest_go(){
		soundsound.PlayCueId(soundtest_no);
	}

	public void push_privacypolicy()
	{
		string url = "http://www5f.biglobe.ne.jp/~akagiken/privacypolicy_fueru.html";
#if UNITY_EDITOR
		Application.OpenURL(url);
#elif UNITY_WEBGL
        Application.ExternalEval(string.Format("window.open('{0}','_blank')", url));
#else
        Application.OpenURL(url);
#endif

	}

	//ポーズボタンを押す
	public void push_pause_button(){
		GG.pause = 1;
		Time.timeScale = 0;
		//ポーズボタンを消す
		pause_button_set(false);
		pause_menu_set(true);
	}


	private void pause_button_set(bool onoff){
		button_pause.gameObject.SetActive(onoff);

	}

	private void pause_menu_set(bool onoff){
		image_pause_board.gameObject.SetActive(onoff);
	}

	public void push_pause_return() {
		Time.timeScale = 1.0f;//動き出す
		Observable.Timer(TimeSpan.FromSeconds(0.5f)).Subscribe(_ =>
			push_pause_return_2()
		);
	}

	void push_pause_return_2()
	{
		//ポーズ解除	
		GG.pause = 0;
		pause_button_set(true);
		pause_menu_set(false);
	}

	public void push_pause_totitle() {
		Time.timeScale = 1.0f;
		Observable.Timer(TimeSpan.FromSeconds(0.5f)).Subscribe(_ =>
			push_pause_totitle_2()
		);
	}

	void push_pause_totitle_2()
	{
		//タイトルへ
		pause_menu_set(false);
		game_obj_all_kill();
		title_move_down();
	}


}



