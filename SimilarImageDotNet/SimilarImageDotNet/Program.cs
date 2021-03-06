﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Getopt;
using LibSimilarImageDotNet;

namespace SimilarImageDotNet
{
	class Program : GetoptCommandLineProgram
	{
		[Command("-L", "--levels")]
		[Description("Set hash levels (default = 4)")]
		[Values(1, 2, 3, 4, 5, 6)]
		//[Example("-L 3")]
		public int Levels = 4;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="FileName1"></param>
		/// <param name="FileName2"></param>
		[Command("-C", "--compare")]
		[Description("Compares two images and get a coefficient of similarity (0.0=Completely distinct, 1.0=Equal)")]
		[Example("-C file1.png file2.png")]
		virtual protected void CompareImages(string FileName1, string FileName2)
		{
			var Hash1 = SimilarImage.GetCompressedImageHashAsString(new Bitmap(Image.FromFile(FileName1)), Levels);
			var Hash2 = SimilarImage.GetCompressedImageHashAsString(new Bitmap(Image.FromFile(FileName2)), Levels);
			Console.Write("{0:0.000000}", SimilarImage.CompareHashes(Hash1, Hash2));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="Hash1"></param>
		/// <param name="Hash2"></param>
		[Command("-CH", "--compare-hash")]
		[Description("Compares two images and get a coefficient of similarity (0.0=Completely distinct, 1.0=Equal)")]
		[Example("-CH HASH1 HASH2")]
		virtual protected void CompareHashes(string Hash1, string Hash2)
		{
			Console.Write("{0:0.000000}", SimilarImage.CompareHashes(Hash1, Hash2));
		}

		/// <summary>
		/// 
		/// </summary>
		[Command("-H", "--hash")]
		[Description("Generates a hash")]
		[Example("-H file.png")]
		virtual protected void DisplayHash(string FileName)
		{
			Console.Write("{0}", SimilarImage.GetCompressedImageHashAsString(new Bitmap(Image.FromFile(FileName)), Levels));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
			new Program().Run(args);
		}
	}
}
