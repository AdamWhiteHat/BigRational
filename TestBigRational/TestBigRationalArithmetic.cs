using System;
using System.Numerics;
using ExtendedNumerics;
using NUnit.Framework;

namespace TestBigRational
{
	[TestFixture(Category = "Arithmetic")]
	public class TestBigRationalArithmetic
	{
		public TestContext TestContext { get { return m_testContext; } set { m_testContext = value; } }
		private TestContext m_testContext;

		[Test]
		public void TestAddition()
		{
			// 3/2 + 10/8 == 201/2
			BigRational threeHalfs = new BigRational(BigInteger.Zero, new Fraction(3, 2));
			BigRational tenEighths = new BigRational(BigInteger.Zero, new Fraction(10, 8));
			BigRational expected1 = BigRational.Reduce(new BigRational(BigInteger.Zero, new Fraction(11, 4)));

			// 1/100 + 1/2 == 11/4
			BigRational oneHundred = new BigRational(100);
			BigRational oneHalf = new BigRational(BigInteger.Zero, new Fraction(1, 2));
			BigRational expected2 = BigRational.Reduce(new BigRational(BigInteger.Zero, new Fraction(201, 2)));

			// 1/3 + 1/5 == 1/2 + 1/30 == 8/15
			BigRational oneThird = new BigRational(0, 1, 3);
			BigRational oneFifth = new BigRational(0, new Fraction(1, 5));
			BigRational oneThirtieth = new BigRational(0, new Fraction(1, 30));
			BigRational expected3and4 = BigRational.Reduce(new BigRational(BigInteger.Zero, new Fraction(8, 15)));

			// Calculate
			BigRational result1 = BigRational.Add(threeHalfs, tenEighths);
			BigRational result2 = BigRational.Add(oneHundred, oneHalf);
			BigRational result3 = BigRational.Add(oneThird, oneFifth);
			BigRational result4 = BigRational.Add(oneHalf, oneThirtieth);

			// Assert
			Assert.AreEqual(expected1, result1);
			Assert.AreEqual(expected2, result2);
			Assert.AreEqual(expected3and4, result3);
			Assert.AreEqual(expected3and4, result4);
		}

		[Test]
		public void TestAddingNegative()
		{
			var low = new BigRational(-7);
			var high = new BigRational(-6);

			BigRational sumLowHigh = (low + high);
			BigRational sumHighLow = (high + low);
			BigRational subtractLowHigh = (low - high);
			BigRational subtractHightLow = (high - low);

			Assert.AreEqual(new BigRational(-13), sumLowHigh, $"{low} + {high} = {sumLowHigh}");
			Assert.AreEqual(new BigRational(-13), sumHighLow, $"{high} + {low} = {sumHighLow}");
			Assert.AreEqual(new BigRational(-1), subtractLowHigh, $"{low} + {high} = {subtractLowHigh}");
			Assert.AreEqual(new BigRational(1), subtractHightLow, $"{high} + {low} = {subtractHightLow}");
		}

		[Test]
		public void TestSubtraction()
		{
			BigRational sevenTwoths = new BigRational(3, 1, 2);
			BigRational sevenFifths = new BigRational(1, 2, 5);

			BigRational expected = new BigRational(2, 1, 10);
			BigRational result = BigRational.Subtract(sevenTwoths, sevenFifths);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void TestMultiplication()
		{
			BigRational sevenTwoths = new BigRational(BigInteger.Zero, new Fraction(7, 2));
			BigRational sevenFifths = new BigRational(BigInteger.Zero, new Fraction(7, 5));

			BigRational expected = BigRational.Reduce(new BigRational(BigInteger.Zero, 49, 10));
			BigRational result = BigRational.Multiply(sevenTwoths, sevenFifths);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void TestDivisionNegative()
		{
			BigRational expected = BigRational.One;
			BigRational result = BigRational.Divide(BigRational.MinusOne, BigRational.MinusOne);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void TestDivision()
		{
			BigRational sevenTwoths = new BigRational(BigInteger.Zero, new Fraction(7, 2));
			BigRational sevenFifths = new BigRational(BigInteger.Zero, new Fraction(7, 5));

			BigRational expected = BigRational.Reduce(new BigRational(BigInteger.Zero, 5, 2));
			BigRational result = BigRational.Divide(sevenTwoths, sevenFifths);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void TestPow()
		{
			BigRational nineFifths = new BigRational(1, 4, 5);

			BigRational expected = new BigRational(3, 6, 25);
			BigRational result = BigRational.Pow(nineFifths, 2);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void TestMidPoint()
		{
			var low = (BigRational)(-7);
			var high = (BigRational)(-6);

			// Take the mid point of the above 2 numbers
			BigRational sum = (low + high);
			BigRational mid = sum / (BigRational)2;

			double expectedSum = -13d;
			double actualSum = (double)sum;
			Assert.AreEqual(expectedSum, actualSum, $"{low} + {high} = {sum}");

			double expectedMid = -6.5d;
			double actualMid = (double)mid;
			Assert.AreEqual(expectedMid, actualMid, $"({low} + {high})/2 = mid");

			TestContext.WriteLine($"{low} < {mid} < {high}");

			Assert.IsTrue(mid > low, "mid > low");
			Assert.IsTrue(mid < high, "mid < high");
		}
	}
}
