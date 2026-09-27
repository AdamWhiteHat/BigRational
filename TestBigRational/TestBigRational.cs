using System;
using System.Numerics;
using ExtendedNumerics;
using NUnit.Framework;

namespace TestBigRational
{
	[TestFixture(Category = "Core")]
	public class TestBigRational
	{
		public TestContext TestContext { get { return m_testContext; } set { m_testContext = value; } }
		private TestContext m_testContext;

		[Test]
		public void TestSimplify()
		{
			BigRational eighteenTwos = new BigRational(18, 2);
			BigRational eighteenFours = new BigRational(18, 4);
			BigRational noChange = new BigRational(1, 8);
			BigRational reduced = new BigRational(2, 6);
			BigRational reducedNegative = new BigRational(-2, 8);

			BigRational expectedValueEighteenTwos = new BigRational(9, 1);
			BigRational expectedValueEighteenFours = new BigRational(9, 2);
			BigRational expectedValueNoChange = new BigRational(1, 8);
			BigRational expectedValueReduced = new BigRational(1, 3);
			BigRational expectedValueReducedNegative = new BigRational(-1, 4);

			Assert.AreEqual(expectedValueEighteenTwos, eighteenTwos);
			Assert.AreEqual(expectedValueEighteenFours, eighteenFours);
			Assert.AreEqual(expectedValueNoChange, noChange);
			Assert.AreEqual(expectedValueReduced, reduced);
			Assert.AreEqual(expectedValueReducedNegative, reducedNegative);
		}

		[Test]
		public void TestNormalizeSign()
		{
			BigRational noChange1 = new BigRational(3, 11);
			BigRational noChange2 = new BigRational(-3, 13);
			BigRational normalized = new BigRational(3, -17);

			BigRational expectedValueNoChange1 = new BigRational(3, 11);
			BigRational expectedValueNoChange2 = new BigRational(-3, 13);
			BigRational expectedValueNormalized = new BigRational(-3, 17);

			Assert.AreEqual(expectedValueNoChange1, noChange1);
			Assert.AreEqual(expectedValueNoChange2, noChange2);
			Assert.AreEqual(expectedValueNormalized, normalized);
		}

		[Test]
		public void TestReduceToProperFraction()
		{
			MixedFraction reducedNegative = BigRational.ReduceToProperFraction(new BigRational(-3, 2));
			MixedFraction reduced = BigRational.ReduceToProperFraction(new BigRational(16, 7));
			MixedFraction noChange = BigRational.ReduceToProperFraction(new BigRational(7, 16));

			MixedFraction expectedValueReducedNegative = new MixedFraction(-1, 1, 2);
			MixedFraction expectedValueReduced = new MixedFraction(2, 2, 7);
			MixedFraction expectedValueNoChange = new MixedFraction(0, 7, 16);

			Assert.AreEqual(expectedValueReducedNegative, reducedNegative);
			Assert.AreEqual(expectedValueReduced, reduced);
			Assert.AreEqual(expectedValueNoChange, noChange);
		}

		[Test]
		public void TestGetHashCode()
		{
			/*
				4919/2 = 2459.5
				9839/4 = 2459.75
			 */
			BigRational testA1 = new BigRational(1, 31);
			BigRational testA2 = new BigRational(2, 31);

			BigRational testB1 = new BigRational(4919, 2);
			BigRational testB2 = new BigRational(9839, 4);

			Assert.AreNotEqual(testA1.GetHashCode(), testA2.GetHashCode());
			Assert.AreNotEqual(testB1.GetHashCode(), testB2.GetHashCode());
		}

		[Test]
		public void TestCompare()
		{
			BigRational toCompareAgainst = new BigRational(3, 5);

			BigRational same = new BigRational(6, 10);
			BigRational larger = new BigRational(61, 100);
			BigRational smaller = new BigRational(59, 100);
			BigRational negative = new BigRational(-3, 5);

			int expected_Same = 0;
			int expected_Larger = -1;
			int expected_Smaller = 1;
			int expected_Negative = 1;

			int result_Same = BigRational.Compare(toCompareAgainst, same);
			int result_Larger = BigRational.Compare(toCompareAgainst, larger);
			int result_Smaller = BigRational.Compare(toCompareAgainst, smaller);
			int resultl_Negative = BigRational.Compare(toCompareAgainst, negative);

			Assert.AreEqual(expected_Same, result_Same);
			Assert.AreEqual(expected_Larger, result_Larger);
			Assert.AreEqual(expected_Smaller, result_Smaller);
			Assert.AreEqual(expected_Negative, resultl_Negative);
		}

		[Test]
		public void TestParse()
		{
			string expected0 = "0";
			string expected1 = "34";
			string expected2 = "7/12";
			string expected3 = "-1";
			string expected4 = "-1/2";

			BigRational frac0 = BigRational.Parse(expected0);
			BigRational frac1 = BigRational.Parse(expected1);
			BigRational frac2 = BigRational.Parse(expected2);
			BigRational frac3 = BigRational.Parse(expected3);
			BigRational frac4 = BigRational.Parse(expected4);

			string actual0 = frac0.ToString();
			string actual1 = frac1.ToString();
			string actual2 = frac2.ToString();
			string actual3 = frac3.ToString();
			string actual4 = frac4.ToString();

			Assert.AreEqual(expected0, actual0);
			Assert.AreEqual(expected1, actual1);
			Assert.AreEqual(expected2, actual2);
			Assert.AreEqual(expected3, actual3);
			Assert.AreEqual(expected4, actual4);
		}
	}
}
