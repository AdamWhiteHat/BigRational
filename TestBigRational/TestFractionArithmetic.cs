using System;
using System.Numerics;
using ExtendedNumerics;
using NUnit.Framework;

namespace TestBigRational
{
	[TestFixture(Category = "Arithmetic")]
	public class TestFractionArithmetic
	{
		public TestContext TestContext { get { return m_testContext; } set { m_testContext = value; } }
		private TestContext m_testContext;

		[Test]
		public void TestAddition()
		{
			Fraction oneThird = new Fraction(1, 3);
			Fraction oneFifth = new Fraction(1, 5);

			Fraction ninety = new Fraction(90 / 1);

			Fraction expectedValueEightFifteenths = new Fraction(8, 15);
			Fraction expectedValueThirtysixTwentyfifths = new Fraction(2, 15);

			Fraction expected271Thirds = new Fraction(271, 3);

			Fraction resultEightFifteenths = Fraction.Add(oneThird, oneFifth);
			Fraction resultThirtysixTwentyfifths = Fraction.Add(oneThird, Fraction.Negate(oneFifth));

			Fraction result271Thirds = Fraction.Add(ninety, oneThird);

			Assert.AreEqual(expectedValueEightFifteenths, resultEightFifteenths);
			Assert.AreEqual(expectedValueThirtysixTwentyfifths, resultThirtysixTwentyfifths);
			Assert.AreEqual(expected271Thirds, result271Thirds);
		}

		[Test]
		public void TestSubtraction()
		{
			Fraction oneHalf = new Fraction(1, 2);
			Fraction oneSixth = new Fraction(1, 6);

			Fraction expectedValueOneThird = new Fraction(1, 3);
			Fraction expectedValueNegativeTwoThirds = new Fraction(2, 3);

			Fraction resultOneThird = Fraction.Subtract(oneHalf, oneSixth);
			Fraction resultNegativeTwoThirds = Fraction.Subtract(oneHalf, Fraction.Negate(oneSixth));

			Assert.AreEqual(expectedValueOneThird, resultOneThird);
			Assert.AreEqual(expectedValueNegativeTwoThirds, resultNegativeTwoThirds);
		}

		[Test]
		public void TestImproperSubtraction()
		{
			Fraction oneHalf = new Fraction(1, 2);
			Fraction oneSixth = new Fraction(1, 6);

			Fraction expectedValueOneThird = new Fraction(1, 3);

			Fraction resultOneThird = Fraction.Subtract(oneHalf, oneSixth);

			Assert.AreEqual(expectedValueOneThird, resultOneThird);
		}

		[Test]
		public void TestMultiplication()
		{
			Fraction oneHalf = new Fraction(1, 2);
			Fraction twoFifths = new Fraction(2, 5);

			Fraction expectedValueOneFifth = new Fraction(1, 5);
			Fraction expectedValueNegativeFourTwentyFifths = new Fraction(-4, 25);

			Fraction resultOneFifth = Fraction.Multiply(oneHalf, twoFifths);
			Fraction resultNegativeFourTwentyFifths = Fraction.Multiply(twoFifths, Fraction.Negate(twoFifths));

			Assert.AreEqual(expectedValueOneFifth, resultOneFifth);
			Assert.AreEqual(expectedValueNegativeFourTwentyFifths, resultNegativeFourTwentyFifths);
		}

		[Test]
		public void TestDivision()
		{
			Fraction oneHalf = new Fraction(1, 2);
			Fraction oneSixth = new Fraction(1, 6);
			Fraction negativeOneSixth = new Fraction(-1, 6);

			Fraction expectedValueThree = new Fraction(3, 1);
			Fraction expectedValueOneThird = new Fraction(1, 3);
			Fraction expectedValueNegativeThree = new Fraction(-3, 1);
			Fraction expectedValueNegativeOneThird = new Fraction(-1, 3);

			Fraction resultThree = Fraction.Divide(oneHalf, oneSixth);
			Fraction resultNegativeThree = Fraction.Divide(oneHalf, negativeOneSixth);
			Fraction resultOneThird = Fraction.Divide(oneSixth, oneHalf);
			Fraction resultNegativeOneThird = Fraction.Divide(oneSixth, Fraction.Negate(oneHalf));

			Assert.AreEqual(expectedValueThree, resultThree);
			Assert.AreEqual(expectedValueNegativeThree, resultNegativeThree);
			Assert.AreEqual(expectedValueOneThird, resultOneThird);
			Assert.AreEqual(expectedValueNegativeOneThird, resultNegativeOneThird);
		}

		[Test]
		public void TestPow()
		{
			// (4/5)^2 == 16/25
			Fraction fourFifths = new Fraction(4, 5);

			Fraction expected = new Fraction(16, 25);
			Fraction result = Fraction.Pow(fourFifths, 2);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void TestBitShifting()
		{
			BigInteger a = new BigInteger(255); // two to the power of 8

			BigInteger b = new BigInteger(65537); // close to power of 2
			BigInteger q = new BigInteger(65539); // prime

			BigInteger p = new BigInteger(524309); // prime
			BigInteger c = new BigInteger(32768);  // power of two

			Fraction oldFrac1 = new Fraction(a, 1);
			Fraction oldFrac2 = new Fraction(b, q);
			Fraction oldFrac3 = Fraction.Pow(oldFrac1, 2);
			Fraction oldFrac4 = new Fraction(p, a);
			Fraction oldFrac5 = new Fraction(p, c);

			BigInteger someResult1n = oldFrac1.Numerator >> 1;
			BigInteger someResult2n = oldFrac2.Numerator >> 2;
			BigInteger someResult3n = oldFrac3.Numerator << 2;
			BigInteger someResult4n = oldFrac4.Numerator << 2;
			BigInteger someResult7n = oldFrac5.Numerator << 2;

			BigInteger someResult1d = oldFrac1.Denominator >> 2;
			BigInteger someResult2d = oldFrac2.Denominator >> 2;
			BigInteger someResult3d = oldFrac3.Denominator << 2;
			BigInteger someResult4d = oldFrac4.Denominator >> 3;
			BigInteger someResult6d = oldFrac5.Denominator >> 1;

			Fraction newFrac1 = new Fraction(a, 1);
			Fraction newFrac2 = new Fraction(b, q);
			Fraction newFrac3 = Fraction.Pow(oldFrac1, 2);
			Fraction newFrac4 = new Fraction(p, a);
			Fraction newFrac5 = new Fraction(p, c);

			Assert.AreEqual(oldFrac1, newFrac1);
			Assert.AreEqual(oldFrac2, newFrac2);
			Assert.AreEqual(oldFrac3, newFrac3);
			Assert.AreEqual(oldFrac4, newFrac4);
			Assert.AreEqual(oldFrac5, newFrac5);
		}
	}
}
