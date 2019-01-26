using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace EvolveDB.Tests
{
    public class MigrationVersionTests
    {
        public static IEnumerable<object[]> MigrationVersion_TestData()
        {
            yield return new object[] { new MigrationVersion("8"), new MigrationVersion("3_1"), 1 };
            yield return new object[] { new MigrationVersion("3_1"), new MigrationVersion("3_1_1"), -1 };
            yield return new object[] { new MigrationVersion("5_2"), new MigrationVersion("5_2"), 0 };
            yield return new object[] { new MigrationVersion("6_1_1"), new MigrationVersion("6_1_2"), -1 };
        }

        [Fact]
        public void InvalidFormatTest()
        {
            Assert.Throws<FormatException>(() => new MigrationVersion("1_2_2i"));
        }

        [Theory]
        [MemberData(nameof(MigrationVersion_TestData))]
        public void ComparisonTest(MigrationVersion v1, MigrationVersion v2, int result)
        {
            Assert.Equal(result, v1.CompareTo(v2));
        }
        [Fact]
        public void ComparisonWithNullTest()
        {
            var v1 = new MigrationVersion("1");
            MigrationVersion v2 = null;

            Assert.Equal(1, v1.CompareTo(v2));
        }
        [Fact]
        public void ComparisonWithSameTest()
        {
            var v1 = new MigrationVersion("2_1");

            Assert.Equal(0, v1.CompareTo(v1));
        }
        [Fact]
        public void EmptyVersionTest()
        {
            Assert.Throws<ArgumentNullException>(() => new MigrationVersion(string.Empty));
        }
        [Fact]
        public void EqualityTest()
        {
            var v1 = new MigrationVersion("2_1_2_4");
            var v2 = new MigrationVersion("2_1_2_4");

            Assert.True(v1.Equals(v2));
        }
        [Fact]
        public void InvalidComparisonTest()
        {
            var v1 = new MigrationVersion("2_4_5");

            Assert.Throws<ArgumentException>(() => v1.CompareTo("2_1_4"));
        }
        [Fact]
        public void NoEqualityTest()
        {
            var v1 = new MigrationVersion("2_1_2");
            var v2 = new MigrationVersion("2_1_2_1");

            Assert.False(v1.Equals(v2));
        }
        [Theory]
        [InlineData("1")]
        [InlineData("3_1_4")]
        public void PartsTest(string version)
        {
            Assert.Equal(new MigrationVersion(version).Parts, version.Split('_').Select(long.Parse).ToList());
        }
    }
}
