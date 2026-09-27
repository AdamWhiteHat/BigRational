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
			BigRational oneThird = new BigRational(1, 3);
			BigRational oneFifth = new BigRational(1, 5);

			BigRational ninety = new BigRational(90 / 1);

			BigRational expectedValueEightFifteenths = new BigRational(8, 15);
			BigRational expectedValueThirtysixTwentyfifths = new BigRational(2, 15);

			BigRational expected271Thirds = new BigRational(271, 3);

			BigRational resultEightFifteenths = BigRational.Add(oneThird, oneFifth);
			BigRational resultThirtysixTwentyfifths = BigRational.Add(oneThird, BigRational.Negate(oneFifth));

			BigRational result271Thirds = BigRational.Add(ninety, oneThird);

			Assert.AreEqual(expectedValueEightFifteenths, resultEightFifteenths);
			Assert.AreEqual(expectedValueThirtysixTwentyfifths, resultThirtysixTwentyfifths);
			Assert.AreEqual(expected271Thirds, result271Thirds);
		}

		[Test]
		public void TestSubtraction()
		{
			BigRational oneHalf = new BigRational(1, 2);
			BigRational oneSixth = new BigRational(1, 6);

			BigRational expectedValueOneThird = new BigRational(1, 3);
			BigRational expectedValueNegativeTwoThirds = new BigRational(2, 3);

			BigRational resultOneThird = BigRational.Subtract(oneHalf, oneSixth);
			BigRational resultNegativeTwoThirds = BigRational.Subtract(oneHalf, BigRational.Negate(oneSixth));

			Assert.AreEqual(expectedValueOneThird, resultOneThird);
			Assert.AreEqual(expectedValueNegativeTwoThirds, resultNegativeTwoThirds);
		}

		[Test]
		public void TestImproperSubtraction()
		{
			BigRational oneHalf = new BigRational(1, 2);
			BigRational oneSixth = new BigRational(1, 6);

			BigRational expectedValueOneThird = new BigRational(1, 3);

			BigRational resultOneThird = BigRational.Subtract(oneHalf, oneSixth);

			Assert.AreEqual(expectedValueOneThird, resultOneThird);
		}

		[Test]
		public void TestMultiplication()
		{
			BigRational oneHalf = new BigRational(1, 2);
			BigRational twoFifths = new BigRational(2, 5);

			BigRational expectedValueOneFifth = new BigRational(1, 5);
			BigRational expectedValueNegativeFourTwentyFifths = new BigRational(-4, 25);

			BigRational resultOneFifth = BigRational.Multiply(oneHalf, twoFifths);
			BigRational resultNegativeFourTwentyFifths = BigRational.Multiply(twoFifths, BigRational.Negate(twoFifths));

			Assert.AreEqual(expectedValueOneFifth, resultOneFifth);
			Assert.AreEqual(expectedValueNegativeFourTwentyFifths, resultNegativeFourTwentyFifths);
		}

		[Test]
		public void TestDivision()
		{
			BigRational oneHalf = new BigRational(1, 2);
			BigRational oneSixth = new BigRational(1, 6);
			BigRational negativeOneSixth = new BigRational(-1, 6);

			BigRational expectedValueThree = new BigRational(3, 1);
			BigRational expectedValueOneThird = new BigRational(1, 3);
			BigRational expectedValueNegativeThree = new BigRational(-3, 1);
			BigRational expectedValueNegativeOneThird = new BigRational(-1, 3);

			BigRational resultThree = BigRational.Divide(oneHalf, oneSixth);
			BigRational resultNegativeThree = BigRational.Divide(oneHalf, negativeOneSixth);
			BigRational resultOneThird = BigRational.Divide(oneSixth, oneHalf);
			BigRational resultNegativeOneThird = BigRational.Divide(oneSixth, BigRational.Negate(oneHalf));

			Assert.AreEqual(expectedValueThree, resultThree);
			Assert.AreEqual(expectedValueNegativeThree, resultNegativeThree);
			Assert.AreEqual(expectedValueOneThird, resultOneThird);
			Assert.AreEqual(expectedValueNegativeOneThird, resultNegativeOneThird);
		}

		[Test]
		public void TestPow()
		{
			// (4/5)^2 == 16/25
			BigRational fourFifths = new BigRational(4, 5);

			BigRational expected = new BigRational(16, 25);
			BigRational result = BigRational.Pow(fourFifths, 2);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void TestSqrt()
		{
			BigRational oneOverSixteen = new BigRational(1, 16); //  2#(1/16) = 1/4
			BigRational twentyFive = new BigRational(25); // sqrt(25) == 5			
			BigRational fourNinths = new BigRational(4, 9); // sqrt(4/9) == 2/3

			BigRational expected1 = new BigRational(1, 4);
			BigRational expected2 = new BigRational(5);
			BigRational expected3 = new BigRational(2, 3);

			BigRational result1 = BigRational.Sqrt(oneOverSixteen);
			BigRational result2 = BigRational.Sqrt(twentyFive);
			BigRational result3 = BigRational.Sqrt(fourNinths);

			Assert.AreEqual(expected1, result1);
			Assert.AreEqual(expected2, result2);
			Assert.AreEqual(expected3, result3);
		}

		[Test]
		public void TestNthRoot001()
		{
			BigRational twoOneEightSeven = new BigRational(2187);// 7#2187 = 3
			BigRational oneOverEightyOne = new BigRational(1, 81); // 4#(1/81) = 1/3
			BigRational twentySevenOverSixtyFour = new BigRational(27, 64); // 3#(27/64) = 3/4
			BigRational twentyTwoOverThreeOneTwoFive = new BigRational(32, 3125); // 5#(32/3125) = 2/5

			BigRational expected1 = new BigRational(3);
			BigRational expected2 = new BigRational(1, 3);
			BigRational expected3 = new BigRational(3, 4);
			BigRational expected4 = new BigRational(2, 5);

			BigRational result1 = BigRational.NthRoot(twoOneEightSeven, 7);
			BigRational result2 = BigRational.NthRoot(oneOverEightyOne, 4);
			BigRational result3 = BigRational.NthRoot(twentySevenOverSixtyFour, 3);
			BigRational result4 = BigRational.NthRoot(twentyTwoOverThreeOneTwoFive, 5);

			Assert.AreEqual(expected1, result1);
			Assert.AreEqual(expected2, result2);
			Assert.AreEqual(expected3, result3);
			Assert.AreEqual(expected4, result4);
		}


		[Test]
		public void TestNthRoot002()
		{
			BigRational cubeRootOf50_Expected = new BigRational(15313185253378309, 4156637981795142);
			BigRational cubeRootOf100_Expected = new BigRational(7336085559573722, 1580511721858759);
			BigRational sqrRootOf2_Expected = new BigRational(2470433131948081, 1746860020068409);
			BigRational sqrRootOf65_Expected = new BigRational(2368403439540328, 293764292023553);
			BigRational cubeRootOf65_Expected = new BigRational(4726945758417767, 1175644906474928);
			BigRational cubeRootOf125_Expected = new BigRational(5, 1);
			BigRational sqrtRootOf100_Expected = new BigRational(10, 1);
			BigRational sqrtRootOf4_Expected = new BigRational(2, 1);

			BigRational cubeRootOf50_Result = BigRational.NthRoot(50, 3);
			BigRational cubeRootOf100_Result = BigRational.NthRoot(100, 3);
			BigRational sqrRootOf2_Result = BigRational.NthRoot(2, 2);
			BigRational sqrRootOf65_Result = BigRational.NthRoot(65, 2);
			BigRational cubeRootOf65_Result = BigRational.NthRoot(65, 3);
			BigRational cubeRootOf125_Result = BigRational.NthRoot(125, 3);
			BigRational sqrtRootOf100_Result = BigRational.NthRoot(100, 2);
			BigRational sqrtRootOf4_Result = BigRational.NthRoot(4, 2);

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

			BigRational oldFrac1 = new BigRational(a, 1);
			BigRational oldFrac2 = new BigRational(b, q);
			BigRational oldFrac3 = BigRational.Pow(oldFrac1, 2);
			BigRational oldFrac4 = new BigRational(p, a);
			BigRational oldFrac5 = new BigRational(p, c);

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

			BigRational newFrac1 = new BigRational(a, 1);
			BigRational newFrac2 = new BigRational(b, q);
			BigRational newFrac3 = BigRational.Pow(oldFrac1, 2);
			BigRational newFrac4 = new BigRational(p, a);
			BigRational newFrac5 = new BigRational(p, c);

			Assert.AreEqual(oldFrac1, newFrac1);
			Assert.AreEqual(oldFrac2, newFrac2);
			Assert.AreEqual(oldFrac3, newFrac3);
			Assert.AreEqual(oldFrac4, newFrac4);
			Assert.AreEqual(oldFrac5, newFrac5);
		}
	}
}
