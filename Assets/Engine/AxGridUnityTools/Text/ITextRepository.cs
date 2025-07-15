using System.Collections.Generic;

namespace Engine.AxGridUnityTools.Text {
    public interface ITextRepository {
        Dictionary<string, string> Translations { get; }
        string Get(string key, string def = null);
    }
}