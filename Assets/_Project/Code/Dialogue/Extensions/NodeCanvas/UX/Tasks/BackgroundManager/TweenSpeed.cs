using UnityEngine;
using Sycamore.FX.UX;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using DG.Tweening;

namespace Sycamore.Dialogue.Extensions
{
	[Category ("Sycamore/FX/UX")]
	public class TweenSpeed : ActionTask
	{
		public BBParameter<float> to = new BBParameter<float> (1f);
		public BBParameter<float> duration = new BBParameter<float> (2f);
		public BBParameter<Ease> ease = new BBParameter<Ease> (Ease.InOutSine);

		protected override void OnExecute ()
		{
			BackgroundManager.Instance.TweenSpeed (to.value, duration.value, ease.value);
			EndAction ();
		}
	}
}