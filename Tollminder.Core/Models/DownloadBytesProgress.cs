﻿using System;

namespace Tollminder.Core.Models
{
	public class DownloadBytesProgress
	{
		public DownloadBytesProgress(string fileName, long bytesReceived, long totalBytes)
		{
			Filename = fileName;
			BytesReceived = bytesReceived;
			TotalBytes = totalBytes;
		}

		public long TotalBytes { get; private set; }

		public long BytesReceived { get; private set; }

		public int PercentComplete { get { return (int)(((float)BytesReceived / TotalBytes) * 100); } }

		public float PercentCompleteInFloat { get { return (float)BytesReceived / TotalBytes * 100; } }

		public string Filename { get; private set; }

		public bool IsFinished { get { return BytesReceived == TotalBytes; } }
	}
}

