// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using System.ComponentModel;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace Demo
{
    public partial class Main : Form
    {
        private IKeyboardMouseEvents _mEvents;

        public Main()
        {
            InitializeComponent();
            radioGlobal.Checked = true;
            SubscribeGlobal();
            FormClosing += Main_Closing;
        }

        private void Main_Closing(object sender, CancelEventArgs e)
        {
            Unsubscribe();
        }

        private void SubscribeApplication()
        {
            Unsubscribe();
            Subscribe(Hook.AppEvents());
        }

        private void SubscribeGlobal()
        {
            Unsubscribe();
            Subscribe(Hook.GlobalEvents());
        }

        private void Subscribe(IKeyboardMouseEvents events)
        {
            _mEvents = events;
            _mEvents.KeyDown += OnKeyDown;
            _mEvents.KeyUp += OnKeyUp;
            _mEvents.KeyPress += HookManager_KeyPress;

            _mEvents.MouseUp += OnMouseUp;
            _mEvents.MouseClick += OnMouseClick;
            _mEvents.MouseDoubleClick += OnMouseDoubleClick;

            _mEvents.MouseMove += HookManager_MouseMove;

            _mEvents.MouseDragStarted += OnMouseDragStarted;
            _mEvents.MouseDragFinished += OnMouseDragFinished;

            if (checkBoxSupressMouseWheel.Checked)
            {
                _mEvents.MouseWheelExt += HookManager_MouseWheelExt;
                _mEvents.MouseHWheelExt += HookManager_MouseHWheelExt;
            }
            else
            {
                _mEvents.MouseWheel += HookManager_MouseWheel;
                _mEvents.MouseHWheel += HookManager_MouseHWheel;
            }

            if (checkBoxSuppressMouse.Checked)
                _mEvents.MouseDownExt += HookManager_Supress;
            else
                _mEvents.MouseDown += OnMouseDown;
        }

        private void Unsubscribe()
        {
            if (_mEvents == null) return;
            _mEvents.KeyDown -= OnKeyDown;
            _mEvents.KeyUp -= OnKeyUp;
            _mEvents.KeyPress -= HookManager_KeyPress;

            _mEvents.MouseUp -= OnMouseUp;
            _mEvents.MouseClick -= OnMouseClick;
            _mEvents.MouseDoubleClick -= OnMouseDoubleClick;

            _mEvents.MouseMove -= HookManager_MouseMove;

            _mEvents.MouseDragStarted -= OnMouseDragStarted;
            _mEvents.MouseDragFinished -= OnMouseDragFinished;

            if (checkBoxSupressMouseWheel.Checked)
            {
                _mEvents.MouseWheelExt -= HookManager_MouseWheelExt;
                _mEvents.MouseHWheelExt -= HookManager_MouseHWheelExt;
            }
            else
            {
                _mEvents.MouseWheel -= HookManager_MouseWheel;
                _mEvents.MouseHWheel -= HookManager_MouseHWheel;
            }

            if (checkBoxSuppressMouse.Checked)
                _mEvents.MouseDownExt -= HookManager_Supress;
            else
                _mEvents.MouseDown -= OnMouseDown;

            _mEvents.Dispose();
            _mEvents = null;
        }

        private void HookManager_Supress(object sender, MouseEventExtArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                Log(string.Format("MouseDown \t\t {0}\n", e.Button));
                return;
            }

            Log(string.Format("MouseDown \t\t {0} Suppressed\n", e.Button));
            e.Handled = true;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            Log(string.Format("KeyDown  \t\t {0}\n", e.KeyCode));
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            Log(string.Format("KeyUp  \t\t\t {0}\n", e.KeyCode));
        }

        private void HookManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            Log(string.Format("KeyPress \t\t\t {0}\n", e.KeyChar));
        }

        private void HookManager_MouseMove(object sender, MouseEventArgs e)
        {
            labelMousePosition.Text = string.Format("x={0:0000}; y={1:0000}", e.X, e.Y);
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            Log(string.Format("MouseDown \t\t {0}\n", e.Button));
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            Log(string.Format("MouseUp \t\t {0}\n", e.Button));
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            Log(string.Format("MouseClick \t\t {0}\n", e.Button));
        }

        private void OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            Log(string.Format("MouseDoubleClick \t\t {0}\n", e.Button));
        }

        private void OnMouseDragStarted(object sender, MouseEventArgs e)
        {
            Log("MouseDragStarted\n");
        }

        private void OnMouseDragFinished(object sender, MouseEventArgs e)
        {
            Log("MouseDragFinished\n");
        }

        private void HookManager_MouseWheel(object sender, MouseEventArgs e)
        {
            labelWheel.Text = string.Format("Wheel={0:000}", e.Delta);
        }

        private void HookManager_MouseWheelExt(object sender, MouseEventExtArgs e)
        {
            labelWheel.Text = string.Format("Wheel={0:000}", e.Delta);
            Log("Mouse Wheel Move Suppressed.\n");
            e.Handled = true;
        }

        private void HookManager_MouseHWheel(object sender, MouseEventArgs e)
        {
            labelHWheel.Text = string.Format("HWheel={0:000}", e.Delta);
        }

        private void HookManager_MouseHWheelExt(object sender, MouseEventExtArgs e)
        {
            labelHWheel.Text = string.Format("HWheel={0:000}", e.Delta);
            Log("Horizontal Mouse Wheel Move Suppressed.\n");
            e.Handled = true;
        }

        private void Log(string text)
        {
            if (IsDisposed) return;
            textBoxLog.AppendText(text);
            textBoxLog.AppendText(Environment.NewLine);
            textBoxLog.ScrollToCaret();
        }

        private void radioApplication_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton) sender).Checked) SubscribeApplication();
        }

        private void radioGlobal_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton) sender).Checked) SubscribeGlobal();
        }

        private void radioNone_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton) sender).Checked) Unsubscribe();
        }

        private void checkBoxSupressMouseWheel_CheckedChanged(object sender, EventArgs e)
        {
            if (_mEvents == null) return;

            if (((CheckBox) sender).Checked)
            {
                _mEvents.MouseWheel -= HookManager_MouseWheel;
                _mEvents.MouseWheelExt += HookManager_MouseWheelExt;
                _mEvents.MouseHWheel -= HookManager_MouseHWheel;
                _mEvents.MouseHWheelExt += HookManager_MouseHWheelExt;
            }
            else
            {
                _mEvents.MouseWheelExt -= HookManager_MouseWheelExt;
                _mEvents.MouseWheel += HookManager_MouseWheel;
                _mEvents.MouseHWheelExt -= HookManager_MouseHWheelExt;
                _mEvents.MouseHWheel += HookManager_MouseHWheel;
            }
        }

        private void checkBoxSuppressMouse_CheckedChanged(object sender, EventArgs e)
        {
            if (_mEvents == null) return;

            if (((CheckBox) sender).Checked)
            {
                _mEvents.MouseDown -= OnMouseDown;
                _mEvents.MouseDownExt += HookManager_Supress;
            }
            else
            {
                _mEvents.MouseDownExt -= HookManager_Supress;
                _mEvents.MouseDown += OnMouseDown;
            }
        }

        private void clearLog_Click(object sender, EventArgs e)
        {
            textBoxLog.Clear();
        }
    }
}