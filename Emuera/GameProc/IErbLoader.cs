using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinorShift.Emuera.GameProc
{
    internal interface IErbLoader
    {
        // Public Functions
        bool LoadErbFiles(string erbDir, bool displayReport, LabelDictionary labelDirectory);
        bool loadErbs(List<string> path, LabelDictionary labelDictionary);

    }
}
