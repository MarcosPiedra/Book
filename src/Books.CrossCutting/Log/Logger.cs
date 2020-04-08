using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Books.CrossCutting
{
    public class Logger : ILogger
    {
        List<ILoggerListener> _listenners = new List<ILoggerListener>();
        string _lineFormat = "hh:mm:ss.ffff";
        StringBuilder _lineToLog = new StringBuilder();

        Queue<Action> _writeQueue = new Queue<Action>();
        ManualResetEvent _newItem = new ManualResetEvent(false);
        ManualResetEvent _terminateLog = new ManualResetEvent(false);
        ManualResetEvent _waitingLog = new ManualResetEvent(false);
        Task _logger;

        public Logger(Func<IEnumerable<ILoggerListener>> listeners)
        {
            var lists = listeners.Invoke();
            foreach (var listener in lists)
            {
                _listenners.Add(listener);
            }

            if (_listenners.Count == 0)
                return;

            Init();

            _logger = Task.Factory.StartNew(() =>
            {
                ProcessQueue();
            }, TaskCreationOptions.LongRunning);
        }

        ~Logger()
        {
            foreach (var listener in _listenners)
            {
                listener.Close();
            }
        }

        void ProcessQueue()
        {
            while (true)
            {
                _waitingLog.Set();
                int indxEvent = WaitHandle.WaitAny(new WaitHandle[] { _newItem, _terminateLog });
                // terminate was signaled 
                if (indxEvent == 1) return;
                _newItem.Reset();
                _waitingLog.Reset();

                Queue<Action> _queueCopy;
                lock (_writeQueue)
                {
                    _queueCopy = new Queue<Action>(_writeQueue);
                    _writeQueue.Clear();
                }

                foreach (var _write in _queueCopy)
                {
                    _write();
                }
            }
        }

        public void Init()
        {
            try
            {
                foreach (var listener in _listenners)
                {
                    listener.Init();
                }

                Write("/*************** Log Started  ***************/", Level.Info);
            }
            catch (Exception _ex)
            {
                throw _ex;
            }
        }

        public void Close()
        {
            Write("/*************** Log Finished ***************/", Level.Info);

            _terminateLog.Set();
            _logger.Wait();

            foreach (var listener in _listenners)
            {
                listener.Close();
            }
            _listenners.Clear();
        }

        public void Write(string message, Level level)
        {
            _lineToLog.Length = 0;
            _lineToLog.Append(DateTime.Now.ToString(_lineFormat));
            _lineToLog.Append(" ");
            _lineToLog.Append(GetLevelName(level));
            _lineToLog.Append(" ");
            _lineToLog.AppendLine(message);

            EnqueueMessage(_lineToLog.ToString());
        }

        public void Write(Exception ex)
        {
            _lineToLog.Length = 0;
            _lineToLog.Append(DateTime.Now.ToString(_lineFormat));
            _lineToLog.Append(" ");
            _lineToLog.AppendLine(GetLevelName(Level.Error));
            _lineToLog.AppendLine("-- Ini ex.");
            _lineToLog.AppendLine(ex.Message);
            _lineToLog.AppendLine(ex.StackTrace);
            _lineToLog.AppendLine("-- Fin ex.");

            EnqueueMessage(_lineToLog.ToString());
        }

        private void EnqueueMessage(string message)
        {
            lock (_writeQueue)
            {
                foreach (var listener in _listenners)
                {
                    _writeQueue.Enqueue(() => listener.Write(message));
                }
            }
            _newItem.Set();
        }

        private string GetLevelName(Level level)
        {
            switch (level)
            {
                case Level.Info:
                    return "Info".PadRight(7, ' ');
                case Level.Warn:
                    return "Warning";
                case Level.Error:
                    return "Error".PadRight(7, ' ');
                case Level.Fatal:
                    return "Fatal".PadRight(7, ' ');
                case Level.Debug:
                    return "Debug".PadRight(7, ' ');
                case Level.All:
                default:
                    return "";
            }
        }

    }
}
