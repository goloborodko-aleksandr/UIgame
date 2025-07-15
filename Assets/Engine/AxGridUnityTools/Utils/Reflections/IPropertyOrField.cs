using System;

namespace Engine.AxGridUnityTools.Utils.Reflections
{
    public interface IPropertyOrField
    {
        string Name { get; }
        Type ValueType { get; }
        bool IsIndexed { get; }
    }
}