using System;
using System.Collections.Generic;
using System.Text;

namespace Lemma.AADS.Structures
{
    public class ModificationException : Exception
    {
        //Todo implement this !!!
        public string VersionInfo { get; private set; }
        public ModificationException(string message)
        {
            VersionInfo = message;
        }
    }
}
