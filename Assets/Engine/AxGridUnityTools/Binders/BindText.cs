using Engine.AxGridUnityTools.Base;
using Engine.AxGridUnityTools.Model;

namespace Engine.AxGridUnityTools.Binders
{
	/// <inheritdoc />
	/// <summary>
	/// Класс привязывается на событие модели, изменения поля
	/// </summary>
	public class BindText : MonoBehaviourExtBind
	{
		public string fieldName = "";

		/// <summary>
		/// Модель изменилась
		/// </summary>
		/// <param name="fieldName">Поле, которое поменялось</param>
		[Bind]
		public void ModelChanged(string fieldName)
		{
			if (fieldName == this.fieldName)
				GetComponent<UnityEngine.UI.Text>().text = 
					Text.Text.Get(Settings.Model.GetString(fieldName), Settings.Model);
		}
	}
}