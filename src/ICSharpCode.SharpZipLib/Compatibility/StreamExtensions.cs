
using System.Text;
using System.Threading.Tasks;

namespace System.IO
{
	static class StreamExtensions
	{
#if NET35

		class StreamWriterEx : StreamWriter
		{
			private bool leaveOpen;

			public StreamWriterEx(Stream stream, Encoding encoding, int bufferSize, bool leaveOpen) : base(stream, encoding, bufferSize)
			{
				this.leaveOpen = leaveOpen;
			}

			protected override void Dispose(bool disposing)
			{
				if (leaveOpen == false)
					base.Dispose(disposing);
			}
		}
#endif
		public static StreamWriter GetWriter(this Stream stream, Text.Encoding encoding, int bufferSize, bool leaveOpen)
		{
			StreamWriter writer;
#if NET35
			writer = new StreamWriterEx(stream, encoding, bufferSize, leaveOpen);
#else
			writer =new StreamWriter(stream,encoding,bufferSize,leaveOpen);
#endif
			return writer;
		}

#if NET35
		public static Task<string> ReadToEndAsync(this StreamReader stream)
		{
			TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();			
			Task.Run(() => stream.ReadToEnd())
				.ContinueWith(t =>
					{
						if (t.IsCompleted)
						{
							tcs.SetResult(t.Result);
						}
						else if (t.IsFaulted)
						{
							tcs.SetException(t.Exception);
						}
						else
						{
							tcs.SetCanceled();
						}
					});
			return tcs.Task;
		}
#endif
	}
}

