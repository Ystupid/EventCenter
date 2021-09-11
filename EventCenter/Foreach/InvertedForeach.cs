using System.Collections;
using System.Collections.Generic;
using System;

public class InvertedForeach : IForeachable<Delegate>
{
    public void Foreach(in IList<Delegate> list, in EventAction<int, Delegate> action)
    {
        if (list == null || list.Count <= 0 || action == null) return;

        for (int i = list.Count; i >= 0; i--)
        {
            action(i, list[i]);
        }
    }
}
