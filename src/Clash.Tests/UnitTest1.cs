namespace Clash.Tests
{
    public class UnitTest1
    {
        public static string[] Array1 { get; set; } = new string[] { "--name", "John Doe", "--count", "23", "--address", "192.168.24.1" };
        [Fact]
        public void TestParse()
        {
            var args = new string[] { "--name", "John Doe", "--count", "23", "--address", "192.168.24.1" };
            var parser = new Parser();

            var result = parser.Parse<CliModel>(args);

            Assert.NotNull(result);
            Assert.IsType<CliModel>(result);
            Assert.Equal("John Doe", result.Name);
            Assert.Equal("192.168.24.1", result.IpAddress);
            Assert.Equal(23, result.Count);
            Assert.IsType<int>(result.Count);

        }
        [Fact]
        public void TestTryParse()
        {
            var args = new string[] { "--name", "John Doe", "--count", "23", "--address", "192.168.24.1" };
            var parser = new Parser();

            parser.TryParse<CliModel>(args, out var result);

            Assert.NotNull(result);
            Assert.IsType<CliModel>(result);
            Assert.Equal("John Doe", result.Name);
            Assert.Equal("192.168.24.1", result.IpAddress);
            Assert.Equal(23, result.Count);
            Assert.IsType<int>(result.Count);

        }


        [Theory]
        [MemberData(nameof(DataShortFlags))]
        public void TestMultipleParseCallsShortArgFlags(string arg1, string arg2, string arg3, string arg4, string arg5, string arg6, string expectedName, int expectedCount, string expectedIpAddress)
        {
            var parser = new Parser();

            var input = new string[] { arg1, arg2, arg3, arg4, arg5, arg6 };

            var result = parser.Parse<CliModel>(input);

            Assert.NotNull(result);
            Assert.IsType<CliModel>(result);
            Assert.Equal(expectedName, result.Name);
            Assert.Equal(expectedCount, result.Count);
            Assert.Equal(expectedIpAddress, result.IpAddress);
            Assert.IsType<int>(result.Count);
        }


        [Theory]
        [MemberData(nameof(DataLongFlags))]
        public void TestMultipleParseCallsLongArgFlags(string arg1, string arg2, string arg3, string arg4, string arg5, string arg6, string expectedName, int expectedCount, string expectedIpAddress)
        {
            var parser = new Parser();

            var input = new string[] { arg1, arg2, arg3, arg4, arg5, arg6 };

            var result = parser.Parse<CliModel>(input);

            Assert.NotNull(result);
            Assert.IsType<CliModel>(result);
            Assert.Equal(expectedName, result.Name);
            Assert.Equal(expectedCount, result.Count);
            Assert.Equal(expectedIpAddress, result.IpAddress);
            Assert.IsType<int>(result.Count);
        }

        [Theory]
        [MemberData(nameof(DataShortFlags))]
        public void TestMultipleTryParseCallsShortArgFlags(string arg1, string arg2, string arg3, string arg4, string arg5, string arg6, string expectedName, int expectedCount, string expectedIpAddress)
        {
            var parser = new Parser();

            var input = new string[] { arg1, arg2, arg3, arg4, arg5, arg6 };

            parser.TryParse<CliModel>(input, out var result);

            Assert.NotNull(result);
            Assert.IsType<CliModel>(result);
            Assert.Equal(expectedName, result.Name);
            Assert.Equal(expectedCount, result.Count);
            Assert.Equal(expectedIpAddress, result.IpAddress);
            Assert.IsType<int>(result.Count);
        }


        [Theory]
        [MemberData(nameof(DataLongFlags))]
        public void TestMultipleTryParseCallsLongArgFlags(string arg1, string arg2, string arg3, string arg4, string arg5, string arg6, string expectedName, int expectedCount, string expectedIpAddress)
        {
            var parser = new Parser();

            var input = new string[] { arg1, arg2, arg3, arg4, arg5, arg6 };

            parser.TryParse<CliModel>(input, out var result);

            Assert.NotNull(result);
            Assert.IsType<CliModel>(result);
            Assert.Equal(expectedName, result.Name);
            Assert.Equal(expectedCount, result.Count);
            Assert.Equal(expectedIpAddress, result.IpAddress);
            Assert.IsType<int>(result.Count);
        }


        public static IEnumerable<object[]> DataShortFlags =>
                new List<object[]>
                {
            new object[] { "-n", "John Doe", "-c", "23", "-i", "192.168.24.1", "John Doe", 23, "192.168.24.1" },
            new object[] { "-n", "Alex Smith", "-c", "2", "-i", "127.0.0.1", "Alex Smith", 2, "127.0.0.1" },
            new object[] { "-n", "Jane Doe", "-c", "15", "-i", "127.0.0.3", "Jane Doe", 15, "127.0.0.3" },
            new object[] { "-n", "Rob C Martin", "-c", "100", "-i", "192.168.88.5", "Rob C Martin", 100, "192.168.88.5" },
            new object[] { "-n", "Brain Kernighan", "-c", "10", "-i", "0.0.0.1", "Brain Kernighan", 10, "0.0.0.1" },

                };

        public static IEnumerable<object[]> DataLongFlags =>
                new List<object[]>
                {
            new object[] { "--name", "John Doe", "--count", "23", "--address", "192.168.24.1", "John Doe", 23, "192.168.24.1" },
            new object[] { "--name", "Alex Smith", "--count", "2", "--address", "127.0.0.1", "Alex Smith", 2, "127.0.0.1" },
            new object[] { "--name", "Jane Doe", "--count", "15", "--address", "127.0.0.3", "Jane Doe", 15, "127.0.0.3" },
            new object[] { "--name", "Rob C Martin", "--count", "100", "--address", "192.168.88.5", "Rob C Martin", 100, "192.168.88.5" },
            new object[] { "--name", "Brain Kernighan", "--count", "10", "--address", "0.0.0.1", "Brain Kernighan", 10, "0.0.0.1" },
            
                };
        
    }
}