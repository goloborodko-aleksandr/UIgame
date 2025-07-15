using Engine.AxGridUnityTools.Base;
using UnityEngine;

namespace Engine.AxGridUnityTools.Binders {

	/// <summary>
	/// Хелпер привязки полей Display для отображения надписей
	/// </summary>
	[RequireComponent(typeof(UnityEngine.UI.Text))]
	public class UITextDataBindDisplay : MonoBehaviourExt {
		
		
		[Header("Форматироване")]
		[Tooltip("Smart.Format(format, model)")]
		public string format = "{Display.InSlot.Button.}";
		
		private UnityEngine.UI.Text uiText;

		[OnAwake]
		void SetUIText()
		{
			uiText = GetComponent<UnityEngine.UI.Text>();
		}

		
		[OnStart]
		public void Bind()
		{
			Settings.Model.EventManager.AddAction("OnDisplayChanged", Changed);
			Changed();
		}

		
		[OnDestroy]
		public void UnBind()
		{
			Settings.Model.EventManager.RemoveAction("OnDisplayChanged", Changed);
		}

        
		protected void Changed()
		{
			string s = Text.Text.Get(format, Settings.Model);
			if (!string.IsNullOrEmpty(s))
				uiText.text = s;
		}

	}
}