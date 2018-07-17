using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace NodeCanvas.Tasks.AudioToolkit{

	[Category("AudioToolkit")]
	public class StopAllAudio : ActionTask{

		public BBParameter<float> fadeOut = 0;

		protected override void OnExecute(){
			AudioController.StopAll( fadeOut.value );
			EndAction();
		}
	}
}