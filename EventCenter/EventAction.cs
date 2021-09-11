using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void EventAction();
public delegate void EventAction<in T>(T arg);
public delegate void EventAction<in T1, in T2>(T1 arg0, T2 arg1);
public delegate void EventAction<in T1, in T2, in T3>(T1 arg0, T2 arg1, T3 arg2);
public delegate void EventAction<in T1, in T2, in T3, in T4>(T1 arg0, T2 arg1, T3 arg2, T4 arg3);
public delegate void EventAction<in T1, in T2, in T3, in T4, in T5>(T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4);
public delegate void EventAction<in T1, in T2, in T3, in T4, in T5, in T6>(T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5);