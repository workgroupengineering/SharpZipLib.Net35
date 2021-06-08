
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

		public static void CopyTo(this Stream fromStream, Stream toStream)

		{

			if (fromStream == null)

				throw new ArgumentNullException("fromStream");

			if (toStream == null)

				throw new ArgumentNullException("toStream");



			var bytes = new byte[8092];

			int dataRead;

			while ((dataRead = fromStream.Read(bytes, 0, bytes.Length)) > 0)

				toStream.Write(bytes, 0, dataRead);

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

		public static Task<int> ReadAsync(this Stream stream, byte[] buffer, int offset, int count)
		{
			var tcs = new TaskCompletionSource<int>();

			stream.BeginRead(buffer, offset, count, asr =>
				   {
					   try
					   {
						   var result = stream.EndRead(asr);
						   tcs.SetResult(result);
					   }
					   catch (Exception ex)
					   {

						   tcs.SetException(ex);
					   }
				   }, null);

			return tcs.Task;
		}

		public static Task WriteAsync(this Stream stream, byte[] buffer, int offset, int count)
		{
			var tcs = new TaskCompletionSource<bool>();
			stream.BeginWrite(buffer, offset, count, asr =>
				   {
					   try
					   {
						   stream.EndWrite(asr);
						   tcs.SetResult(true);
					   }
					   catch (Exception ex)
					   {
						   tcs.SetException(ex);
					   }
				   }, null);
			return tcs.Task;
		}

		public static Task FlushAsync(this Stream stream)
		{
			var tcs = new TaskCompletionSource<bool>();
			Task.Run(() => stream.Flush())
				.ContinueWith(t =>
				{
					if (t.IsCompleted)
					{
						tcs.SetResult(true);
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

