using System;
using System.Diagnostics.Tracing;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using NUnit.Framework;

namespace UnitTestWindows
{


    public class SingleKeyPressTest
    {

        private Channel<KeyPressEventArgs> _channel;
        private AutoResetEvent _handle;
        private string _buffer;

        [SetUp]
        public void Setup()
        {
            _channel = Channel.CreateUnbounded<KeyPressEventArgs>();
            Hook.GlobalEvents().KeyPress += OnKeyPress;
            _handle = new AutoResetEvent(false);
            _buffer= string.Empty;
        }

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            //var canWrite = channel.Writer.TryWrite(e);
            //Assert.True(canWrite);
            System.Console.WriteLine(e.KeyChar);
            e.Handled = true;
            _buffer += e.KeyChar;
            _handle.Reset();
        }

        [TearDown]
        public void TearDown()
        {
            Hook.GlobalEvents().KeyPress -= OnKeyPress;
            this._channel.Writer.Complete();
        }

        [Test]
        public void TestAllRegularKeystrokes()
        {
            var expected = "abcdefghijklmnopqrstuvwxyz1234567890`-=[]\\;',./";

            // Simulate keystrokes for all regular and special keys
            foreach (var key in expected)
            {
                _handle.Set();
                // Send the keystroke using SendKeys.SendWait
                SendKeys.SendWait("{" + key + "}");
                Application.DoEvents();
                _handle.WaitOne(100);
            }
            var actual = _buffer;
            Assert.AreEqual(expected, actual);
        }
    }
}