using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace ReinstallSys.Service.TaskQueue
{
    // The MIT License (MIT)

    // Copyright (c) 2016 Jari Pennanen

    // Permission is hereby granted, free of charge, to any person obtaining a copy
    // of this software and associated documentation files (the "Software"), to deal
    // in the Software without restriction, including without limitation the rights
    // to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    // copies of the Software, and to permit persons to whom the Software is
    // furnished to do so, subject to the following conditions:

    // The above copyright notice and this permission notice shall be included in all
    // copies or substantial portions of the Software.

    // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    // IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    // FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    // AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    // LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    // OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    // SOFTWARE.
    public class TaskQueueService
    {
        private readonly ConcurrentQueue<Func<Task>> _processingQueue = new ConcurrentQueue<Func<Task>>();
        private readonly ConcurrentDictionary<int, Task> _runningTasks = new ConcurrentDictionary<int, Task>();
        private readonly int _maxParallelizationCount;
        private readonly int _maxQueueLength;
        private TaskCompletionSource<bool> _tscQueue = new TaskCompletionSource<bool>();

        public TaskQueueService(int? maxParallelizationCount = null, int? maxQueueLength = null)
        {
            _maxParallelizationCount = maxParallelizationCount ?? int.MaxValue;
            _maxQueueLength = maxQueueLength ?? int.MaxValue;
        }

        public bool Queue(Func<Task> futureTask)
        {
            if (_processingQueue.Count < _maxQueueLength)
            {
                _processingQueue.Enqueue(futureTask);
                return true;
            }
            return false;
        }

        public int GetQueueCount()
        {
            return _processingQueue.Count;
        }

        public int GetRunningCount()
        {
            return _runningTasks.Count;
        }

        public async Task Process()
        {
            var t = _tscQueue.Task;
            StartTasks();
            await t;
        }

        public void ProcessBackground(Action<Exception> exception = null)
        {
            Task.Run(Process).ContinueWith(t => {
                exception?.Invoke(t.Exception);
            }, TaskContinuationOptions.OnlyOnFaulted);
        }

        private void StartTasks()
        {
            var startMaxCount = _maxParallelizationCount - _runningTasks.Count;
            for (int i = 0; i < startMaxCount; i++)
            {
                Func<Task> futureTask;
                if (!_processingQueue.TryDequeue(out futureTask))
                {
                    // Queue is most likely empty
                    break;
                }

                var t = Task.Run(futureTask);
                if (!_runningTasks.TryAdd(t.GetHashCode(), t))
                {
                    throw new Exception("Should not happen, hash codes are unique");
                }

                t.ContinueWith((t2) =>
                {
                    Task _temp;
                    if (!_runningTasks.TryRemove(t2.GetHashCode(), out _temp))
                    {
                        throw new Exception("Should not happen, hash codes are unique");
                    }

                    // Continue the queue processing
                    StartTasks();
                });
            }

            if (_processingQueue.IsEmpty && _runningTasks.IsEmpty)
            {
                // Interlocked.Exchange might not be necessary
                var _oldQueue = Interlocked.Exchange(
                    ref _tscQueue, new TaskCompletionSource<bool>());
                _oldQueue.TrySetResult(true);
            }
        }
    }
}
