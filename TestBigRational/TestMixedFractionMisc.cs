using System;
using System.Numerics;
using System.Diagnostics;
using System.Collections.Generic;
using ExtendedNumerics;
using NUnit.Framework;

namespace TestBigRational
{
	[TestFixture(Category = "Misc")]
	public class TestMixedFractionMisc
	{
		public TestContext TestContext { get { return m_testContext; } set { m_testContext = value; } }
		private TestContext m_testContext;

		[Test]
		public void TestIteratingOnInsertionIndexDownDirectionAlternate2()
		{
			int _iterations = 1000;
			int _count = 1000;

			List<MixedFraction> testSetLower = new List<MixedFraction>(_count);
			List<MixedFraction> testSetUpper = new List<MixedFraction>(_count);

			Stopwatch timer = Stopwatch.StartNew();

			for (int i = 0; i < _count; ++i)
			{
				testSetLower.Add((MixedFraction)i);
				testSetUpper.Add((MixedFraction)(i + 1));
			}
			for (int iteration = 0; iteration < _iterations; ++iteration)
			{
				for (int i = 0; i < _count; ++i)
				{
					// Get pairs of number, take half way between them, and check it
					// is less than the high end and greater than the lower end
					MixedFraction newValue = (testSetLower[i] + testSetUpper[i]) / (MixedFraction)2;
					Assert.IsTrue(newValue > testSetLower[i]);
					Assert.IsTrue(newValue < testSetUpper[i]);
					testSetUpper[i] = newValue;
				}
			}

			timer.Stop();

			TimeSpan timeElapsed = timer.Elapsed;

			// Write result.
			TestContext.WriteLine($"Time elapsed: {HelperMethods.FormatTimeSpan(timeElapsed)}");
		}
	}
}
