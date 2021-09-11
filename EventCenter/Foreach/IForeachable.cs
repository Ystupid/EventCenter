using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IForeachable<T>
{
    void Foreach(in IList<T> list,in EventAction<int,T> action);
}
