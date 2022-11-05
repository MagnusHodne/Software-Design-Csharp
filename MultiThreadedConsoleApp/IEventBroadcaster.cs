namespace MultiThreadedConsoleApp;

public interface IEventBroadcaster
{
	public void SubscribeToBroadcaster(object id, Action onEventCallback);
	public void UnsubscribeToBroadcaster(object id);
}