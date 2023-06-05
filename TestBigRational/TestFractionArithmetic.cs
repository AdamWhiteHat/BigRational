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
		public void TestSqrt()
		{
			Fraction oneOverSixteen = new Fraction(1, 16); //  2#(1/16) = 1/4
			Fraction twentyFive = new Fraction(25); // sqrt(25) == 5			
			Fraction fourNinths = new Fraction(4, 9); // sqrt(4/9) == 2/3

			Fraction expected1 = new Fraction(1, 4);
			Fraction expected2 = new Fraction(5);
			Fraction expected3 = new Fraction(2, 3);

			Fraction result1 = Fraction.Sqrt(oneOverSixteen);
			Fraction result2 = Fraction.Sqrt(twentyFive);
			Fraction result3 = Fraction.Sqrt(fourNinths);

			Assert.AreEqual(expected1, result1);
			Assert.AreEqual(expected2, result2);
			Assert.AreEqual(expected3, result3);
		}

		[Test]
		public void TestNthRoot001()
		{
			Fraction twoOneEightSeven = new Fraction(2187);// 7#2187 = 3
			Fraction oneOverEightyOne = new Fraction(1, 81); // 4#(1/81) = 1/3
			Fraction twentySevenOverSixtyFour = new Fraction(27, 64); // 3#(27/64) = 3/4
			Fraction twentyTwoOverThreeOneTwoFive = new Fraction(32, 3125); // 5#(32/3125) = 2/5

			Fraction expected1 = new Fraction(3);
			Fraction expected2 = new Fraction(1, 3);
			Fraction expected3 = new Fraction(3, 4);
			Fraction expected4 = new Fraction(2, 5);

			Fraction result1 = Fraction.NthRoot(twoOneEightSeven, 7);
			Fraction result2 = Fraction.NthRoot(oneOverEightyOne, 4);
			Fraction result3 = Fraction.NthRoot(twentySevenOverSixtyFour, 3);
			Fraction result4 = Fraction.NthRoot(twentyTwoOverThreeOneTwoFive, 5);

			Assert.AreEqual(expected1, result1);
			Assert.AreEqual(expected2, result2);
			Assert.AreEqual(expected3, result3);
			Assert.AreEqual(expected4, result4);
		}


		[Test]
		public void TestNthRoot002()
		{
			Fraction cubeRootOf50_Expected = new Fraction(15313185253378309, 4156637981795142);
			Fraction cubeRootOf100_Expected = new Fraction(7336085559573722, 1580511721858759);
			Fraction sqrRootOf2_Expected = new Fraction(2470433131948081, 1746860020068409);
			Fraction sqrRootOf65_Expected = new Fraction(2368403439540328, 293764292023553);
			Fraction cubeRootOf65_Expected = new Fraction(4726945758417767, 1175644906474928);
			Fraction cubeRootOf125_Expected = new Fraction(5, 1);
			Fraction sqrtRootOf100_Expected = new Fraction(10, 1);
			Fraction sqrtRootOf4_Expected = new Fraction(2, 1);

			Fraction cubeRootOf50_Result = Fraction.NthRoot(50, 3);
			Fraction cubeRootOf100_Result = Fraction.NthRoot(100, 3);
			Fraction sqrRootOf2_Result = Fraction.NthRoot(2, 2);
			Fraction sqrRootOf65_Result = Fraction.NthRoot(65, 2);
			Fraction cubeRootOf65_Result = Fraction.NthRoot(65, 3);
			Fraction cubeRootOf125_Result = Fraction.NthRoot(125, 3);
			Fraction sqrtRootOf100_Result = Fraction.NthRoot(100, 2);
			Fraction sqrtRootOf4_Result = Fraction.NthRoot(4, 2);

			Assert.AreEqual(cubeRootOf50_Expected, cubeRootOf50_Result);
			Assert.AreEqual(cubeRootOf100_Expected, cubeRootOf100_Result);
			Assert.AreEqual(sqrRootOf2_Expected, sqrRootOf2_Result);
			Assert.AreEqual(sqrRootOf65_Expected, sqrRootOf65_Result);
			Assert.AreEqual(cubeRootOf65_Expected, cubeRootOf65_Result);
			Assert.AreEqual(cubeRootOf125_Expected, cubeRootOf125_Result);
			Assert.AreEqual(sqrtRootOf100_Expected, sqrtRootOf100_Result);
			Assert.AreEqual(sqrtRootOf4_Expected, sqrtRootOf4_Result);
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
