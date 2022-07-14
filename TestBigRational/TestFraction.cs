using System;
using System.Numerics;
using ExtendedNumerics;
using NUnit.Framework;

namespace TestBigRational
{
	[TestFixture(Category = "Core")]
	public class TestFraction
	{
		public TestContext TestContext { get { return m_testContext; } set { m_testContext = value; } }
		private TestContext m_testContext;

		[Test]
		public void TestSimplify()
		{
			Fraction eighteenTwos = new Fraction(18, 2);
			Fraction eighteenFours = new Fraction(18, 4);
			Fraction noChange = new Fraction(1, 8);
			Fraction reduced = new Fraction(2, 6);
			Fraction reducedNegative = new Fraction(-2, 8);

			Fraction expectedValueEighteenTwos = new Fraction(9, 1);
			Fraction expectedValueEighteenFours = new Fraction(9, 2);
			Fraction expectedValueNoChange = new Fraction(1, 8);
			Fraction expectedValueReduced = new Fraction(1, 3);
			Fraction expectedValueReducedNegative = new Fraction(-1, 4);

			Assert.AreEqual(expectedValueEighteenTwos, eighteenTwos);
			Assert.AreEqual(expectedValueEighteenFours, eighteenFours);
			Assert.AreEqual(expectedValueNoChange, noChange);
			Assert.AreEqual(expectedValueReduced, reduced);
			Assert.AreEqual(expectedValueReducedNegative, reducedNegative);
		}

		[Test]
		public void TestNormalizeSign()
		{
			Fraction noChange1 = new Fraction(3, 11);
			Fraction noChange2 = new Fraction(-3, 13);
			Fraction normalized = new Fraction(3, -17);

			Fraction expectedValueNoChange1 = new Fraction(3, 11);
			Fraction expectedValueNoChange2 = new Fraction(-3, 13);
			Fraction expectedValueNormalized = new Fraction(-3, 17);

			Assert.AreEqual(expectedValueNoChange1, noChange1);
			Assert.AreEqual(expectedValueNoChange2, noChange2);
			Assert.AreEqual(expectedValueNormalized, normalized);
		}

		[Test]
		public void TestReduceToProperFraction()
		{
			BigRational reducedNegative = Fraction.ReduceToProperFraction(new Fraction(-3, 2));
			BigRational reduced = Fraction.ReduceToProperFraction(new Fraction(16, 7));
			BigRational noChange = Fraction.ReduceToProperFraction(new Fraction(7, 16));

			BigRational expectedValueReducedNegative = new BigRational(-1, 1, 2);
			BigRational expectedValueReduced = new BigRational(2, 2, 7);
			BigRational expectedValueNoChange = new BigRational(0, 7, 16);

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
			Fraction testA1 = new Fraction(1, 31);
			Fraction testA2 = new Fraction(2, 31);

			Fraction testB1 = new Fraction(4919, 2);
			Fraction testB2 = new Fraction(9839, 4);

			Assert.AreNotEqual(testA1.GetHashCode(), testA2.GetHashCode());
			Assert.AreNotEqual(testB1.GetHashCode(), testB2.GetHashCode());
		}

		[Test]
		public void TestCompare()
		{
			Fraction toCompareAgainst = new Fraction(3, 5);

			Fraction same = new Fraction(6, 10);
			Fraction larger = new Fraction(61, 100);
			Fraction smaller = new Fraction(59, 100);
			Fraction negative = new Fraction(-3, 5);

			int expected_Same = 0;
			int expected_Larger = -1;
			int expected_Smaller = 1;
			int expected_Negative = 1;

			int result_Same = Fraction.Compare(toCompareAgainst, same);
			int result_Larger = Fraction.Compare(toCompareAgainst, larger);
			int result_Smaller = Fraction.Compare(toCompareAgainst, smaller);
			int resultl_Negative = Fraction.Compare(toCompareAgainst, negative);

			Assert.AreEqual(expected_Same, result_Same);
			Assert.AreEqual(expected_Larger, result_Larger);
			Assert.AreEqual(expected_Smaller, result_Smaller);
			Assert.AreEqual(expected_Negative, resultl_Negative);
		}
	}
}
