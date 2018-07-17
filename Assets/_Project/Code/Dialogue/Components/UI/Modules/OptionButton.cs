using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace Sycamore.Dialogue.UI
{
	public class OptionButton : MonoBehaviour
	{
		[Header ("Animation")]
		[SerializeField] private float fadeDuration = 0.5f;
		[SerializeField] private AnimationCurve fadeCurve = AnimationCurve.EaseInOut (0f, 0f, 1f, 1f);
		[SerializeField] private bool slideOut;
		[SerializeField] private float slideDistance = 50f;
		[SerializeField] private float slideDuration = 1f;
		[SerializeField] private AnimationCurve slideCurve = AnimationCurve.EaseInOut (0f, 0f, 1f, 1f);
		[Header ("References")]
		[SerializeField] private Button button;
		[SerializeField] private TextMeshProUGUI text;
		[SerializeField] private RectTransform background;
		[SerializeField] private CanvasGroup canvasGroup;

		public Button Button { get { return button; } }
		public TextMeshProUGUI Text { get { return text;  } }

		public void Show (float delay = 0f)
		{
			StartCoroutine (ShowRoutine (delay));
		}

		public void Hide (float delay = 0f)
		{
			StartCoroutine (HideRoutine (delay));
		}

		public void SetInteractable (bool interactable)
		{
			canvasGroup.interactable = canvasGroup.blocksRaycasts = interactable;
		}

		public void SetAlpha (float a)
		{
			canvasGroup.alpha = a;
		}

		private IEnumerator ShowRoutine (float delay)
		{
			yield return new WaitForSeconds (delay);

			var s = DOTween.Sequence ();
			s.Insert (0f, canvasGroup.DOFade (1f, fadeDuration).SetEase (fadeCurve));
			s.Insert (0f, background.DOAnchorPosX (-slideDistance, slideDuration).From ().SetEase (slideCurve));
			s.onComplete += () => SetInteractable (true);
			s.Play ();
		}

		private IEnumerator HideRoutine (float delay)
		{
			SetInteractable (false);
			yield return new WaitForSeconds (delay);
			var s = DOTween.Sequence ();
			s.Insert (0f, canvasGroup.DOFade (0f, fadeDuration).SetEase (fadeCurve));
			if (slideOut)
				s.Insert (0f, background.DOAnchorPosX (slideDistance, slideDuration).SetEase (slideCurve));
			s.Play ();

			s.onComplete += ResetPositionX;
		}

		private void ResetPositionX ()
		{
			var position = background.anchoredPosition;
			position.x = 0;
			background.anchoredPosition = position;
		}
	}
}