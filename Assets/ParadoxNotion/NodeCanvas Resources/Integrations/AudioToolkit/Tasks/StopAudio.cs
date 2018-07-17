using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace NodeCanvas.Tasks.AudioToolkit{

	[Category("AudioToolkit")]
	public class StopAudio : ActionTask{

		[RequiredField]
		public BBParameter<string> audioID;
		public BBParameter<float> fadeOut = 0;

		protected override string info{
			get {return string.Format("Stop {0}", audioID);}
		}

		protected override void OnExecute(){
			AudioController.Stop( audioID.value, fadeOut.value );
			EndAction();
		}
	}
}