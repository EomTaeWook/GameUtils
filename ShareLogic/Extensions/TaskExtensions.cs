using System.Threading.Tasks;

namespace GameUtils.Extensions
{
    public static class TaskExtensions
    {
        public static T GetResult<T>(this Task<T> task, bool continueOnCapturedContext = false)
        {
            return task.ConfigureAwait(continueOnCapturedContext).GetAwaiter().GetResult();
        }
    }
}
