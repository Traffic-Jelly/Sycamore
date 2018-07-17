using UnityEngine;
using Sycamore.FX.UX;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace Sycamore.Dialogue.Extensions
{
	[Category ("Sycamore/FX/UX")]
	public class TweenSpeedCustom : ActionTask
	{
		public BBParameter<float> to = new BBParameter<float> (1f);
		public BBParameter<float> duration = new BBParameter<float> (2f);
		public BBParameter<AnimationCurve> curve = new BBParameter<AnimationCurve> (AnimationCurve.Linear (0f, 0f, 1f, 1f));

		protected override void OnExecute ()
		{
			BackgroundManager.Instance.TweenSpeed (to.value, duration.value, curve.value);
			EndAction ();
		}
	}
}