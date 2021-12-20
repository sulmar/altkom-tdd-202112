using FluentAssertions;
using NUnit.Framework;
using System;

namespace TestApp.UnitTests
{

    public class MarkdownFormatterTests
    {        
        private const string DoubleAsterisk = "**";

        private MarkdownFormatter markdownFormatter;

        [SetUp]
        public void Setup()
        {  
            // Arrange
            markdownFormatter = new MarkdownFormatter();
        }

        [TestCase("a")]
        [TestCase("abc")]
        [TestCase("lorem ipsum")]        
        public void FormatAsBold_ValidContent_ShouldReturnsContentEncloseDoubleAsterisk(string content)
        {
            // Act
            string result = markdownFormatter.FormatAsBold(content);

            // Assert            
            //Assert.That(result, Does.StartWith("**"));
            //Assert.That(result, Does.Contain(content));
            //Assert.That(result, Does.EndWith("**"));

            // Install-Package FluentAssertions
            result.Should()
                .StartWith(DoubleAsterisk)
                .And.Contain(content)
                .And.EndWith(DoubleAsterisk);
        }

        
        [Test]
        public void FormatAsBold_EmptyContent_ShouldThrowsArgumentNullException()
        {
            // Act
            Action act = () => markdownFormatter.FormatAsBold(string.Empty);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
