using System;
using System.Collections.Concurrent;
using System.Threading;

public class ThreadManager : SingletonGetMono<ThreadManager>
{
    private readonly ConcurrentQueue<Action> queue = new ConcurrentQueue<Action>();

    private readonly int mainThreadId = Thread.CurrentThread.ManagedThreadId;

    private Action a;


    public void runOnMainThread(Action action)
    {
        if (Thread.CurrentThread.ManagedThreadId == mainThreadId)
        {
            action.Invoke();
            return;
        }

        queue.Enqueue(action);
    }


    private void Update()
    {
        if (queue.TryDequeue(out a))
        {
            a.Invoke();
        }
    }
}