using System.Collections.Generic;
using Calabonga.StateManager.ConsoleTest;
using Calabonga.StateManager.ConsoleTest.Entities;
using Calabonga.StatusProcessor;
using Moq;
using Xunit;

namespace Calabonga.StateManager.Tests {

    public class AccidentRulesProcessorTests : IClassFixture<AccidentRulesProcessorFixture> {
        private readonly AccidentRulesProcessorFixture _fixture;

        public AccidentRulesProcessorTests(AccidentRulesProcessorFixture fixture) {
            _fixture = fixture;
        }

        [Fact]
        public void ItShouldInstantiatedByFixture() {
            // arrange
            var sut = _fixture.Create();

            // act

            // assert
            Assert.NotNull(sut);
        }
    }

    /// <summary>
    /// Fixture for unit-testing RuleProcessor
    /// </summary>
    public class AccidentRulesProcessorFixture {

        /// <summary>
        /// Factory method
        /// </summary>
        public AccidentRuleProcessor Create() {
            var rules = new Mock<IEnumerable<IStatusRule<Accident, IAccidentState>>>();
            var statuses = new Mock<IEnumerable<IAccidentState>>();
            return new AccidentRuleProcessor(rules.Object, statuses.Object);
        }
    }

}