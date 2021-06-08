using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
#if NET35
namespace System.Threading.Tasks
{

	/// <summary>
	/// 
	/// </summary>
	public static class TaskEx
	{
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="result"></param>
		/// <returns></returns>
		public static Task<TResult> FromResult<TResult>(TResult result)
		{
			var tcs = new TaskCompletionSource<TResult>();
			tcs.SetResult(result);
			return tcs.Task;
		}
	}
}
#endif
