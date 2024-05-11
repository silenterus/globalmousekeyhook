// This code is distributed under MIT license. 
// Copyright (c) 2015 George Mamaladze
// See license.txt or https://mit-license.org/

using System.Collections.Generic;
using Gma.System.MouseKeyHook.WinApi;
namespace Gma.System.MouseKeyHook.Implementation.Keyboard
{
    internal class GlobalKeyListener : KeyListener
    {
        public GlobalKeyListener()
            : base(HookHelper.HookGlobalKeyboard)
        {
        }

        override protected IEnumerable<KeyPressEventArgsExt> GetPressEventArgs(CallbackData data)
        {
            return KeyPressEventArgsExt.FromRawDataGlobal(data);
        }

        override protected KeyEventArgsExt GetDownUpEventArgs(CallbackData data)
        {
            return KeyEventArgsExt.FromRawDataGlobal(data);
        }
    }
}