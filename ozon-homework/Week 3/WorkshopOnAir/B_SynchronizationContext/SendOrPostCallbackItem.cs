namespace B_SynchronizationContext;

public class SendOrPostCallbackItem
{
    public SendOrPostCallback Callback { get; set; }
    public object State { get; set; }

    public void Execute()
    {
        Callback(State);
    }
}