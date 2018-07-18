using UnityEngine;
using Sycamore.FX.UX;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using DG.Tweening;

namespace Sycamore.Dialogue.Extensions
{
	[Category ("Sycamore/FX/UX/Background")]
	public class TweenColors : ActionTask
	{
		public BBParameter<Color> toA = new BBParameter<Color> (Color.white);
		public BBParameter<Color> toB = new BBParameter<Color> (Color.black);
		public BBParameter<float> duration = new BBParameter<float> (2f);
		public BBParameter<Ease> ease = new BBParameter<Ease> (Ease.InOutSine);

		protected override void OnExecute ()
		{
			BackgroundManager.Instance.TweenColorA (toA.value, duration.value, ease.value);
			BackgroundManager.Instance.TweenColorB (toB.value, duration.value, ease.value);
			EndAction ();
		}
	}
}