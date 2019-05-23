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

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, Int64> warningDic = new Dictionary<string, Int64>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="main"></param>
        /// <param name="exm"></param>
        /// <param name="proc"></param>
        public ErbArchiveLoader(EmueraConsole main, ExpressionMediator exm, Process proc)
        {
            parentProcess = proc;
            mediator = exm;
            output = main;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="erbDir"></param>
        /// <param name="displayReport"></param>
        /// <param name="labelDirectory"></param>
        /// <returns></returns>
        public bool LoadErbFiles(string erbDir, bool displayReport, LabelDictionary labelDirectory)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="labelDictionary"></param>
        /// <returns></returns>
        public bool loadErbs(List<string> path, LabelDictionary labelDictionary)
        {
            throw new NotImplementedException();
        }
        
        #endregion

        #region private

        #endregion
    }
}
