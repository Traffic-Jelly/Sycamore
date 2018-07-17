using UnityEngine;
using Sycamore.FX.UX;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using DG.Tweening;

namespace Sycamore.Dialogue.Extensions
{
	[Category ("Sycamore/FX/UX")]
	public class TweenStateCustom : ActionTask
	{
		[RequiredField]
		public BBParameter<BackgroundState> to;
		public BBParameter<float> duration = new BBParameter<float> (2f);
		public BBParameter<AnimationCurve> curve = new BBParameter<AnimationCurve> (AnimationCurve.Linear (0f, 0f, 1f, 1f));

		protected override void OnExecute ()
		{
			BackgroundManager.Instance.TweenState (to.value, duration.value, curve.value);
			EndAction ();
		}
	}
}