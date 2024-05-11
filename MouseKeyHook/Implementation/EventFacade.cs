﻿// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System;
using System.Windows.Forms;
using Gma.System.MouseKeyHook.Implementation.Keyboard;
using Gma.System.MouseKeyHook.Implementation.Mouse;

namespace Gma.System.MouseKeyHook.Implementation
{
    abstract internal class EventFacade : IKeyboardMouseEvents
    {
        private KeyListener _mKeyListenerCache;
        private MouseListener _mMouseListenerCache;

        public event KeyEventHandler KeyDown
        {
            add { GetKeyListener().KeyDown += value; }
            remove { GetKeyListener().KeyDown -= value; }
        }

        public event EventHandler<KeyEventArgsExt> KeyDownExt
        {
            add { GetKeyListener().KeyDownExt += value; }
            remove { GetKeyListener().KeyDownExt -= value; }
        }

        public event KeyPressEventHandler KeyPress
        {
            add { GetKeyListener().KeyPress += value; }
            remove { GetKeyListener().KeyPress -= value; }
        }

	    public event EventHandler<KeyDownTxtEventArgs> KeyDownTxt
	    {
		    add { GetKeyListener().KeyDownTxt += value; }
		    remove { GetKeyListener().KeyDownTxt -= value; }
	    }

	    public event KeyEventHandler KeyUp
        {
            add { GetKeyListener().KeyUp += value; }
            remove { GetKeyListener().KeyUp -= value; }
        }

        public event EventHandler<KeyEventArgsExt> KeyUpExt
        {
            add { GetKeyListener().KeyUpExt += value; }
            remove { GetKeyListener().KeyUpExt -= value; }
        }

        public event MouseEventHandler MouseMove
        {
            add { GetMouseListener().MouseMove += value; }
            remove { GetMouseListener().MouseMove -= value; }
        }

        public event EventHandler<MouseEventExtArgs> MouseMoveExt
        {
            add { GetMouseListener().MouseMoveExt += value; }
            remove { GetMouseListener().MouseMoveExt -= value; }
        }

        public event MouseEventHandler MouseClick
        {
            add { GetMouseListener().MouseClick += value; }
            remove { GetMouseListener().MouseClick -= value; }
        }

        public event MouseEventHandler MouseDown
        {
            add { GetMouseListener().MouseDown += value; }
            remove { GetMouseListener().MouseDown -= value; }
        }

        public event EventHandler<MouseEventExtArgs> MouseDownExt
        {
            add { GetMouseListener().MouseDownExt += value; }
            remove { GetMouseListener().MouseDownExt -= value; }
        }

        public event MouseEventHandler MouseUp
        {
            add { GetMouseListener().MouseUp += value; }
            remove { GetMouseListener().MouseUp -= value; }
        }

        public event EventHandler<MouseEventExtArgs> MouseUpExt
        {
            add { GetMouseListener().MouseUpExt += value; }
            remove { GetMouseListener().MouseUpExt -= value; }
        }

        public event MouseEventHandler MouseWheel
        {
            add { GetMouseListener().MouseWheel += value; }
            remove { GetMouseListener().MouseWheel -= value; }
        }

        public event EventHandler<MouseEventExtArgs> MouseWheelExt
        {
            add { GetMouseListener().MouseWheelExt += value; }
            remove { GetMouseListener().MouseWheelExt -= value; }
        }

        public event MouseEventHandler MouseHWheel
        {
            add { GetMouseListener().MouseHWheel += value; }
            remove { GetMouseListener().MouseHWheel -= value; }
        }

        public event EventHandler<MouseEventExtArgs> MouseHWheelExt
        {
            add { GetMouseListener().MouseHWheelExt += value; }
            remove { GetMouseListener().MouseHWheelExt -= value; }
        }

        public event MouseEventHandler MouseDoubleClick
        {
            add { GetMouseListener().MouseDoubleClick += value; }
            remove { GetMouseListener().MouseDoubleClick -= value; }
        }

        public event MouseEventHandler MouseDragStarted
        {
            add { GetMouseListener().MouseDragStarted += value; }
            remove { GetMouseListener().MouseDragStarted -= value; }
        }

        public event EventHandler<MouseEventExtArgs> MouseDragStartedExt
        {
            add { GetMouseListener().MouseDragStartedExt += value; }
            remove { GetMouseListener().MouseDragStartedExt -= value; }
        }

        public event MouseEventHandler MouseDragFinished
        {
            add { GetMouseListener().MouseDragFinished += value; }
            remove { GetMouseListener().MouseDragFinished -= value; }
        }

        public event EventHandler<MouseEventExtArgs> MouseDragFinishedExt
        {
            add { GetMouseListener().MouseDragFinishedExt += value; }
            remove { GetMouseListener().MouseDragFinishedExt -= value; }
        }

        public void Dispose()
        {
            if (_mMouseListenerCache != null) _mMouseListenerCache.Dispose();
            if (_mKeyListenerCache != null) _mKeyListenerCache.Dispose();
        }

        private KeyListener GetKeyListener()
        {
            var target = _mKeyListenerCache;
            if (target != null) return target;
            target = CreateKeyListener();
            _mKeyListenerCache = target;
            return target;
        }

        private MouseListener GetMouseListener()
        {
            var target = _mMouseListenerCache;
            if (target != null) return target;
            target = CreateMouseListener();
            _mMouseListenerCache = target;
            return target;
        }

        abstract protected MouseListener CreateMouseListener();
        abstract protected KeyListener CreateKeyListener();
    }
}