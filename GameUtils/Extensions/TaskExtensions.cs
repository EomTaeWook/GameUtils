using System.Threading.Tasks;

namespace Utils.Extensions
{
    public static class TaskExtensions
    {
        public static T GetResult<T>(this Task<T> task, bool continueOnCapturedContext = false)
        {
            return task.ConfigureAwait(continueOnCapturedContext).GetAwaiter().GetResult();
        }
        public static void GetResult(this Task task, bool continueOnCapturedContext = false)
        {
            task.ConfigureAwait(continueOnCapturedContext).GetAwaiter().GetResult();
        }
    }
}
