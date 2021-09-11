using System.Collections;
using System.Collections.Generic;
using System;

public class PositiveForeach : IForeachable<Delegate>
{
    public void Foreach(in IList<Delegate> list, in EventAction<int, Delegate> action)
    {
        if (list == null || list.Count <= 0 || action == null) return;

        var eventCount = list.Count;

        for (int i = 0; i < eventCount; i++)
        {
                action(i, list[i]);

            if (list.Count != eventCount)
                throw new Exception("不能在Foreach中对集合数据进行修改");
        }
    }
}
