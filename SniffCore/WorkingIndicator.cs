using System;

namespace SniffCore
{
    public class WorkingIndicator : IDisposable
    {
        private bool _flag;

        public WorkingIndicator()
        {
            _flag = true;
        }

        public void Dispose()
        {
            _flag = false;
        }

        public static bool IsActive(WorkingIndicator indicator)
        {
            return indicator?._flag == true;
        }
    }
}