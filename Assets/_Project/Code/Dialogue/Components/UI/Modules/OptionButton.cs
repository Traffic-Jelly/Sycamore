using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System.Collections;

namespace Sycamore.Dialogue.UI
{
	public class OptionButton : MonoBehaviour
	{
		public enum SlideMode { None, SlideIn, SlideOut, Slide, AlwaysSlideInButOnlySlideOutIfClicked}
		[Header ("Animation")]
		[SerializeField] private float fadeDuration = 0.5f;
		[SerializeField] private AnimationCurve fadeCurve = AnimationCurve.EaseInOut (0f, 0f, 1f, 1f);
		[SerializeField] private SlideMode slideMode = SlideMode.SlideIn;
		[SerializeField] private float slideDistance = 50f;
		[SerializeField] private float slideDuration = 1f;
		[SerializeField] private AnimationCurve slideCurve = AnimationCurve.EaseInOut (0f, 0f, 1f, 1f);
		[SerializeField] private float outlineDuration = 0.2f;
		[SerializeField] private Color outlineColor = Color.white;
		[SerializeField] private AnimationCurve outlineCurve = AnimationCurve.Linear (0f, 0f, 1f, 1f);
		[Header ("References")]
		[SerializeField] private Button button;
		[SerializeField] private TextMeshProUGUI text;
		[SerializeField] private Image outline;
		[SerializeField] private RectTransform background;
		[SerializeField] private CanvasGroup canvasGroup;

		public Button Button { get { return button; } }
		public TextMeshProUGUI Text { get { return text; } }

		private Color noOutlineColor { get { return new Color (outlineColor.r, outlineColor.g, outlineColor.b, 0f); } }

		private void Awake ()
		{
			ResetOutline ();
		}

		public void Show (float delay)
		{
			StartCoroutine (ShowRoutine (delay));
		}

		public void Hide (float delay)
		{
			StartCoroutine (HideRoutine (delay));
		}

		public void Select ()
		{
			StartCoroutine (SelectRoutine ());
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
			if (slideMode == SlideMode.SlideIn || slideMode == SlideMode.Slide || slideMode == SlideMode.AlwaysSlideInButOnlySlideOutIfClicked)
				s.Insert (0f, background.DOAnchorPosX (-slideDistance, slideDuration).From ().SetEase (slideCurve));
			s.onComplete += () => SetInteractable (true);
			s.Play ();
		}

		private IEnumerator HideRoutine (float delay, bool wasSelected = false)
		{
			SetInteractable (false);
			yield return new WaitForSeconds (delay + outlineDuration);
			var s = DOTween.Sequence ();
			s.Insert (0f, canvasGroup.DOFade (0f, fadeDuration).SetEase (fadeCurve));
			if (slideMode == SlideMode.SlideOut || slideMode == SlideMode.Slide || (slideMode == SlideMode.AlwaysSlideInButOnlySlideOutIfClicked && wasSelected))
				s.Insert (0f, background.DOAnchorPosX (slideDistance, slideDuration).SetEase (slideCurve));
			s.onComplete += ResetPositionX;
			s.Play ();
		}

		private IEnumerator SelectRoutine ()
		{
			var s = DOTween.Sequence ();
			s.Insert (0f, outline.DOColor (outlineColor, outlineDuration).SetEase (outlineCurve));
			s.Append (outline.DOColor (noOutlineColor, outlineDuration).SetEase (outlineCurve));
			s.Play ();

			yield return s.WaitForCompletion ();
			yield return HideRoutine (0f, true);
		}

		private void ResetPositionX ()
		{
			var position = background.anchoredPosition;
			position.x = 0;
			background.anchoredPosition = position;
		}

		private void ResetOutline ()
		{
			var c = outline.color;
			c.a = 0f;
			outline.color = c;
		}
	}
}