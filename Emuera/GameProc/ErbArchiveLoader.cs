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

        #region nested data structure
        private sealed class PPState
        {
            bool skip = false;
            bool done = false;
            Stack<bool> disabledStack = new Stack<bool>();
            Stack<bool> doneStack = new Stack<bool>();
            Stack<string> ppMatch = new Stack<string>();

            public bool Disabled = false;

            internal void AddKeyword(string token, string token2, ScriptPosition position)
            {
                var token2Enabled = (token2?.Length == 0);
            
                if (!token2Enabled)
                {
                    ParserMediator.Warn(token + "に余分な引数があります", position, 1);
                }
                else 
                {
                    switch (token)
                    {
                        case "SKIPSTART":
                            if (skip)
                            {
                                ParserMediator.Warn("[SKIPSTART]が重複して使用されています", position, 1);
                                break;
                            }

                            ppMatch.Push("SKIPEND");
                            disabledStack.Push(Disabled);
                            doneStack.Push(done);

                            done = false;
                            Disabled = skip = true;
                            break;

                        case "IF_DEBUG":
                            ppMatch.Push("ELSEIF");
                            disabledStack.Push(Disabled);
                            doneStack.Push(done);

                            Disabled = !Program.DebugMode;
                            done = !Disabled;
                            break;

                        case "IF_NDEBUG":
                            ppMatch.Push("ELSEIF");
                            disabledStack.Push(Disabled);
                            doneStack.Push(done);

                            Disabled = Program.DebugMode;
                            done = !Disabled;
                            break;

                        case "IF":
                            ppMatch.Push("ELSEIF");
                            disabledStack.Push(Disabled);
                            doneStack.Push(done);

                            Disabled = GlobalStatic.IdentifierDictionary.GetMacro(token2) == null;
                            done = !Disabled;
                            break;

                        case "ELSEIF":
                            if (ppMatch.Count == 0 || ppMatch.Pop() != "ELSEIF")
                            {
                                ParserMediator.Warn("不適切な[ELSEIF]です", position, 1);
                                break;
                            }
                            ppMatch.Push("ELSEIF");
                            
                            Disabled = done || (GlobalStatic.IdentifierDictionary.GetMacro(token2) == null);
                            done |= !Disabled;
                            break;

                        case "ELSE":
                            if (!string.IsNullOrEmpty(token2))
                            {
                                ParserMediator.Warn(token + "に余分な引数があります", position, 1);
                                break;
                            }
                            if (ppMatch.Count == 0 || ppMatch.Pop() != "ELSEIF")
                            {
                                ParserMediator.Warn("不適切な[ELSE]です", position, 1);
                                break;
                            }
                            ppMatch.Push("ENDIF");

                            Disabled = done;
                            done = true;
                            break;

                        case "SKIPEND":
                            { 
                                string match = ppMatch.Count == 0 ? "" : ppMatch.Pop();
                                if (match != "SKIPEND")
                                {
                                    ParserMediator.Warn("[SKIPSTART]と対応しない[SKIPEND]です", position, 1);
                                    break;
                                }
                                skip = false;
                                Disabled = disabledStack.Pop();
                                done = doneStack.Pop();
                            }
                            break;

                        case "ENDIF":
                            { 
                                string match = ppMatch.Count == 0 ? "" : ppMatch.Pop();
                                if (match != "ENDIF" && match != "ELSEIF")
                                {
                                    ParserMediator.Warn("対応する[IF]のない[ENDIF]です", position, 1);
                                    break;
                                }
                                Disabled = disabledStack.Pop();
                                done = doneStack.Pop();
                            }
                            break;

                        default:
                            ParserMediator.Warn("認識できないプリプロセッサです", position, 1);
                            break;
                    }
                }

                if (skip)
                    Disabled = true;
            }

            internal void FileEnd(ScriptPosition position)
            {
                if (ppMatch.Count != 0)
                {
                    var match = ppMatch.Pop();
                    if (match == "ELSEIF")
                        match = "ENDIF";

                    ParserMediator.Warn($"[{match}]がありません", position, 1);
                }
            }

        }
        #endregion

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
        public bool LoadErbs(List<string> path, LabelDictionary labelDictionary)
        {
            throw new NotImplementedException();
        }
        
        #endregion

        #region private

        #endregion
    }
}
