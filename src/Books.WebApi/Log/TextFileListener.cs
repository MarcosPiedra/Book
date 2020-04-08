using Books.CrossCutting;
using System;
using System.IO;

namespace Books.WebApi.Log
{
    public class TextFileListener : ILoggerListener
    {
        StreamWriter _currentLog = null;
        string _dateFormat = "dd-MM-yyyy";
        string _workingDir = "";
        string _lastPath = "";
        object _sync = new object();

        public TextFileListener()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var root = System.IO.Directory.GetDirectoryRoot(baseDir);
            _workingDir = Path.Combine(root, "Books_Log");
            if (!Directory.Exists(_workingDir))
                Directory.CreateDirectory(_workingDir);
        }

        ~TextFileListener()
        {
            Close();
        }

        public void Init()
        {
            _lastPath = GetCurrentPath();
            _currentLog = File.AppendText(_lastPath);
        }

        public void Close()
        {
            if (_currentLog != null && _currentLog.BaseStream.CanWrite)
            {
                _currentLog.Flush();
                _currentLog.Close();
            }
            _currentLog = null;
        }

        public void Write(string message)
        {
            lock (_sync)
            {
                var currrentPath = GetCurrentPath();
                if (!_lastPath.Equals(currrentPath))
                {
                    Close();
                    Init();
                }
            }
            _currentLog.Write(message);
            _currentLog.Flush();
        }

        private string GetCurrentPath()
        {
            var name = DateTime.Now.ToString(_dateFormat);
            return Path.Combine(_workingDir, $"Log_{name}.txt");
        }
    }
}
