using System;
using System.Threading.Tasks;

namespace Xamarin.Forms
{
    public class AsyncCommand : Command
    {
        public AsyncCommand(Func<Task> execute)
            : base(() => execute().ConfigureAwait(false))
        {
        }

        public AsyncCommand(Func<Task> execute, Func<bool> canExecute)
            : base(() => execute().ConfigureAwait(false), () => canExecute())
        {
        }

        public AsyncCommand(Func<object, Task> execute, Func<object, bool> canExecute)
            : base((o) => execute(o).ConfigureAwait(false), (o) => canExecute(o))
        {
        }

        public AsyncCommand(Func<object, Task> execute)
            : base(o => execute(o).ConfigureAwait(false))
        {
        }
    }

    public sealed class AsyncCommand<T> : Command
    {
        public AsyncCommand(Func<T, Task> execute, Func<object, bool> canExecute)
            : base(async a => await execute((T)a).ConfigureAwait(false), (a) => canExecute(a))
        {
        }

        public AsyncCommand(Func<T, Task> execute, Func<bool> canExecuteParameter)
            : base(async o => await execute((T)o).ConfigureAwait(false), (o) => canExecuteParameter())
        {
        }

        public AsyncCommand(Func<T, Task> execute)
            : base(async o => await execute((T)o).ConfigureAwait(false))
        {
        }
    }
}