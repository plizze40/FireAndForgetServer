using Microsoft.Extensions.Logging;

namespace FireAndForget.Server.Services;

using DispatcherTask = Func<ValueTask>;

public sealed class DispatcherService
{
    private readonly Queue<DispatcherTask> _tasks = new();
    private readonly object _lock = new();
    private readonly ILogger<DispatcherService> _logger;

    public DispatcherService(ILogger<DispatcherService> logger)
    {
        _logger = logger;
    }

    public void AddTask(DispatcherTask task)
    {
        lock (_lock)
        {
            _tasks.Enqueue(task);
        }
    }

    public void AddTask(Action task)
    {
        AddTask(() =>
        {
            task();
            return ValueTask.CompletedTask;
        });
    }

    public async ValueTask FlushPendingAsync()
    {
        Queue<DispatcherTask> copy;
        lock (_lock)
        {
            if (_tasks.Count == 0)
                return;

            copy = new Queue<DispatcherTask>(_tasks);
            _tasks.Clear();
        }

        while (copy.Any())
        {
            DispatcherTask task = copy.Dequeue();
            try
            {
                await task();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing task");
            }
        }
    }
}