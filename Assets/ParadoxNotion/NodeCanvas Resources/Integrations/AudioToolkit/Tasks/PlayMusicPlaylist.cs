using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace NodeCanvas.Tasks.AudioToolkit{

	[Category("AudioToolkit")]
	public class PlayMusicPlaylist : ActionTask{

		[RequiredField]
		public BBParameter<string> playListName = "Default";

		protected override string info{
			get {return string.Format("Play List {0}", playListName);}
		}

		protected override void OnExecute(){
			AudioController.PlayMusicPlaylist( playListName.value );
			EndAction();
		}
	}
}