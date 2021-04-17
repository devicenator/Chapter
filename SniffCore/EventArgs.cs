using System;

namespace SniffCore
{
    public class EventArgs<T> : EventArgs
    {
        public EventArgs(T item1)
        {
            Item1 = item1;
        }

        public T Item1 { get; }
    }

    public class EventArgs<T1, T2> : EventArgs<T1>
    {
        public EventArgs(T1 item1, T2 item2)
            : base(item1)
        {
            Item2 = item2;
        }

        public T2 Item2 { get; }
    }

    public class EventArgs<T1, T2, T3> : EventArgs<T1, T2>
    {
        public EventArgs(T1 item1, T2 item2, T3 item3)
            : base(item1, item2)
        {
            Item3 = item3;
        }

        public T3 Item3 { get; }
    }

    public class EventArgs<T1, T2, T3, T4> : EventArgs<T1, T2, T3>
    {
        public EventArgs(T1 item1, T2 item2, T3 item3, T4 item4)
            : base(item1, item2, item3)
        {
            Item4 = item4;
        }

        public T4 Item4 { get; }
    }

    public class EventArgs<T1, T2, T3, T4, T5> : EventArgs<T1, T2, T3, T4>
    {
        public EventArgs(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
            : base(item1, item2, item3, item4)
        {
            Item5 = item5;
        }

        public T5 Item5 { get; }
    }

    public class EventArgs<T1, T2, T3, T4, T5, T6> : EventArgs<T1, T2, T3, T4, T5>
    {
        public EventArgs(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
            : base(item1, item2, item3, item4, item5)
        {
            Item6 = item6;
        }

        public T6 Item6 { get; }
    }

    public class EventArgs<T1, T2, T3, T4, T5, T6, T7> : EventArgs<T1, T2, T3, T4, T5, T6>
    {
        public EventArgs(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
            : base(item1, item2, item3, item4, item5, item6)
        {
            Item7 = item7;
        }

        public T7 Item7 { get; }
    }

    public class EventArgs<T1, T2, T3, T4, T5, T6, T7, T8> : EventArgs<T1, T2, T3, T4, T5, T6, T7>
    {
        public EventArgs(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
            : base(item1, item2, item3, item4, item5, item6, item7)
        {
            Item8 = item8;
        }

        public T8 Item8 { get; }
    }
}