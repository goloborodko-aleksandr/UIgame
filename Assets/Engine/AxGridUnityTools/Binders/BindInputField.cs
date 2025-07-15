using Engine.AxGridUnityTools.Base;
using Engine.AxGridUnityTools.Model;
using UnityEngine.UI;

namespace Engine.AxGridUnityTools.Binders
{
    /// <inheritdoc />
    /// <summary>
    /// Класс привязывается на событие модели, изменения поля
    /// </summary>
    public class BindInputField : MonoBehaviourExtBind
    {
        public string fieldName = "";

        /// <summary>
        /// Модель изменилась
        /// </summary>
        /// <param name="fieldName">Поле, которое поменялось</param>
        /// <param name="newValue">Новое значение</param>
        [Bind]
        public void ModelChanged(string fieldName)
        {
            if (fieldName == this.fieldName)
            {

                GetComponent<InputField>().text = Settings.Model.GetString(fieldName);
                GetComponent<InputField>().Select();
            }
        }
    }
}