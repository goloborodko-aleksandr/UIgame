using System;

namespace Engine.AxGridUnityTools.Flow
{
    public interface IFlowContext<S, A> 
        where S : struct
        where A : struct
    {
        S? State { get; set; }
        A? Action { get; set; }
        A? LastAction { get; }
        Exception Throwable { get; set; }
    }
}