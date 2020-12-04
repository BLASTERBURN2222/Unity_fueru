using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Button2m : MonoBehaviour
{

	//これはButton2で、ScaleのXがマイナス（反転）の場合に使う

	void Awake()
	{
		Button button = GetComponent<Button>();
		button.onClick.AddListener(OnClick);
	}

	void OnClick()
	{
		Sequence seq = DOTween.Sequence();
		seq.Append(
			transform.DOScaleX(-1.1f, 0.2f).SetEase(Ease.OutElastic)
		).Join(transform.DOScaleY(1.1f, 0.2f).SetEase(Ease.OutElastic));
		seq.Append(
			transform.DOScaleX(-1.0f, 0.2f).SetEase(Ease.OutElastic)
		).Join(transform.DOScaleY(1.0f, 0.2f).SetEase(Ease.OutElastic));
		seq.Play();
	}
}
