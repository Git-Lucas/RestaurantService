namespace ItemService.Event;

public interface IEventProcessor
{
    void Process(string message);
}
