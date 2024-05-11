// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using Gma.System.MouseKeyHook.Implementation.Keyboard;
using Gma.System.MouseKeyHook.Implementation.Mouse;
namespace Gma.System.MouseKeyHook.Implementation
{
    internal class AppEventFacade : EventFacade
    {
        override protected MouseListener CreateMouseListener()
        {
            return new AppMouseListener();
        }

        override protected KeyListener CreateKeyListener()
        {
            return new AppKeyListener();
        }
    }
}