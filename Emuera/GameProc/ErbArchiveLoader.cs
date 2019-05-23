using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using MinorShift.Emuera.Sub;
using MinorShift.Emuera.GameView;
using MinorShift.Emuera.GameData.Expression;
using MinorShift.Emuera.GameData.Variable;
using MinorShift.Emuera.GameProc.Function;
using MinorShift._Library;
using MinorShift.Emuera.GameData;

namespace MinorShift.Emuera.GameProc
{
    internal sealed class ErbArchiveLoader : IErbLoader
    {
        readonly Process parentProcess;
        readonly ExpressionMediator mediator;
        readonly EmueraConsole output;

        #region public
        public Dictionary<string, Int64> warningDic = new Dictionary<string, Int64>();

        public ErbArchiveLoader(EmueraConsole main, ExpressionMediator exm, Process proc)
        {
            parentProcess = proc;
            mediator = exm;
            output = main;
        }

        public bool LoadErbFiles(string erbDir, bool displayReport, LabelDictionary labelDirectory)
        {
            throw new NotImplementedException();
        }

        public bool loadErbs(List<string> path, LabelDictionary labelDictionary)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region private

        #endregion
    }
}
