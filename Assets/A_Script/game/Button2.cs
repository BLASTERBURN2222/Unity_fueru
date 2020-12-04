using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Button2 : MonoBehaviour
{
	void Awake()
	{
		Button button = GetComponent<Button>();
		button.onClick.AddListener(OnClick);
	}

	void OnClick()
	{
		Sequence seq = DOTween.Sequence();
		seq.Append(
			transform.DOScale(1.1f, 0.2f).SetEase(Ease.OutElastic)
		);
		seq.Append(
			transform.DOScale(1.0f, 0.2f).SetEase(Ease.OutElastic)
		);
		seq.Play();
	}
}
