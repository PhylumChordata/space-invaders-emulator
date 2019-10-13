using Xunit;

namespace JustinCredible.SIEmulator.Tests
{
    public class DITests : BaseTest
    {
        [Fact]
        public void TestDI()
        {
            var rom = AssembleSource($@"
                org 00h
                DI
                HLT
            ");

            var initialState = new InitialCPUState()
            {
                InterruptsEnabled = true,
            };

            var state = Execute(rom, initialState);

            Assert.False(state.InterruptsEnabled);

            AssertFlagsFalse(state);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(7 + 4, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }
    }
}
