using Xunit;

namespace JustinCredible.SIEmulator.Tests
{
    public class STCTests : BaseTest
    {
        [Fact]
        public void TestSTC_SetsFalseToTrue()
        {
            var rom = AssembleSource($@"
                org 00h
                STC
                HLT
            ");

            var initialState = new CPUConfig()
            {
                Flags = new ConditionFlags()
                {
                    Carry = false,
                },
            };

            var state = Execute(rom, initialState);

            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Parity);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(7 + 4, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }

        [Fact]
        public void TestSTC_LeavesTrue()
        {
            var rom = AssembleSource($@"
                org 00h
                STC
                HLT
            ");

            var initialState = new CPUConfig()
            {
                Flags = new ConditionFlags()
                {
                    Carry = true,
                },
            };

            var state = Execute(rom, initialState);

            Assert.False(state.Flags.Zero);
            Assert.False(state.Flags.Sign);
            Assert.False(state.Flags.Parity);
            Assert.True(state.Flags.Carry);

            Assert.Equal(2, state.Iterations);
            Assert.Equal(7 + 4, state.Cycles);
            Assert.Equal(0x01, state.ProgramCounter);
        }
    }
}
