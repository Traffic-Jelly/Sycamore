using UnityEngine;
using Sycamore.FX.UX;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace Sycamore.Dialogue.Extensions
{
	[Category ("Sycamore/FX/UX")]
	public class TweenColorsCustom : ActionTask
	{
		public BBParameter<Color> toA = new BBParameter<Color> (Color.white);
		public BBParameter<Color> toB = new BBParameter<Color> (Color.black);
		public BBParameter<float> duration = new BBParameter<float> (2f);
		public BBParameter<AnimationCurve> curveA = new BBParameter<AnimationCurve> (AnimationCurve.Linear (0f, 0f, 1f, 1f));
		public BBParameter<AnimationCurve> curveB = new BBParameter<AnimationCurve> (AnimationCurve.Linear (0f, 0f, 1f, 1f));

		protected override void OnExecute ()
		{
			BackgroundManager.Instance.TweenColorA (toA.value, duration.value, curveA.value);
			BackgroundManager.Instance.TweenColorB (toB.value, duration.value, curveB.value);
			EndAction ();
		}
	}
}