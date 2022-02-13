﻿using ScintillaNET;

namespace AutoGenerateDDDProjectForNetCore.controls
{
    public class CodeEditor : ScintillaNET.Scintilla
    {
        public CodeEditor()
        {
            // INITIAL VIEW CONFIG
            this.WrapMode = WrapMode.None;
            this.IndentationGuides = IndentView.LookBoth;

            // STYLING
            InitColors();
            InitSyntaxColoring();

            // BOOKMARK MARGIN
            InitBookmarkMargin();

            // NUMBER MARGIN
            InitNumberMargin();

            // CODE FOLDING MARGIN
            InitCodeFolding();
        }

        #region Numbers, Bookmarks, Code Folding

        /// <summary>
        /// the background color of the text area
        /// </summary>
        private const int BACK_COLOR = 0x2A211C;

        /// <summary>
        /// default text color of the text area
        /// </summary>
        private const int FORE_COLOR = 0xB7B7B7;

        /// <summary>
		/// change this to whatever margin you want the line numbers to show in
		/// </summary>
		private const int NUMBER_MARGIN = 1;

        /// <summary>
        /// change this to whatever margin you want the bookmarks/breakpoints to show in
        /// </summary>
        private const int BOOKMARK_MARGIN = 2;

        private const int BOOKMARK_MARKER = 2;

        /// <summary>
		/// change this to whatever margin you want the code folding tree (+/-) to show in
		/// </summary>
		private const int FOLDING_MARGIN = 3;

        /// <summary>
		/// set this true to show circular buttons for code folding (the [+] and [-] buttons on the margin)
		/// </summary>
		private const bool CODEFOLDING_CIRCULAR = true;

        private void this_MarginClick(object sender, MarginClickEventArgs e)
        {
            if (e.Margin == BOOKMARK_MARGIN)
            {
                // Do we have a marker for this line?
                const uint mask = (1 << BOOKMARK_MARKER);
                var line = this.Lines[this.LineFromPosition(e.Position)];
                if ((line.MarkerGet() & mask) > 0)
                {
                    // Remove existing bookmark
                    line.MarkerDelete(BOOKMARK_MARKER);
                }
                else
                {
                    // Add bookmark
                    line.MarkerAdd(BOOKMARK_MARKER);
                }
            }
        }

        private void InitNumberMargin()
        {
            this.Styles[Style.LineNumber].BackColor = IntToColor(BACK_COLOR);
            this.Styles[Style.LineNumber].ForeColor = IntToColor(FORE_COLOR);
            this.Styles[Style.IndentGuide].ForeColor = IntToColor(FORE_COLOR);
            this.Styles[Style.IndentGuide].BackColor = IntToColor(BACK_COLOR);

            var nums = this.Margins[NUMBER_MARGIN];
            nums.Width = 30;
            nums.Type = MarginType.Number;
            nums.Sensitive = true;
            nums.Mask = 0;

            this.MarginClick += this_MarginClick;
        }

        private void InitColors()
        {
            this.SetSelectionBackColor(true, IntToColor(0x114D9C));
        }

        public static Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }

        private void InitSyntaxColoring()
        {
            // Configure the default style
            this.StyleResetDefault();
            this.Styles[Style.Default].Font = "Consolas";
            this.Styles[Style.Default].Size = 10;
            this.Styles[Style.Default].BackColor = IntToColor(0x212121);
            this.Styles[Style.Default].ForeColor = IntToColor(0xFFFFFF);
            this.StyleClearAll();

            // Configure the CPP (C#) lexer styles
            this.Styles[Style.Cpp.Identifier].ForeColor = IntToColor(0xD0DAE2);
            this.Styles[Style.Cpp.Comment].ForeColor = IntToColor(0xBD758B);
            this.Styles[Style.Cpp.CommentLine].ForeColor = IntToColor(0x40BF57);
            this.Styles[Style.Cpp.CommentDoc].ForeColor = IntToColor(0x2FAE35);
            this.Styles[Style.Cpp.Number].ForeColor = IntToColor(0xFFFF00);
            this.Styles[Style.Cpp.String].ForeColor = IntToColor(0xFFFF00);
            this.Styles[Style.Cpp.Character].ForeColor = IntToColor(0xE95454);
            this.Styles[Style.Cpp.Preprocessor].ForeColor = IntToColor(0x8AAFEE);
            this.Styles[Style.Cpp.Operator].ForeColor = IntToColor(0xE0E0E0);
            this.Styles[Style.Cpp.Regex].ForeColor = IntToColor(0xff00ff);
            this.Styles[Style.Cpp.CommentLineDoc].ForeColor = IntToColor(0x77A7DB);
            this.Styles[Style.Cpp.Word].ForeColor = IntToColor(0x48A8EE);
            this.Styles[Style.Cpp.Word2].ForeColor = IntToColor(0xF98906);
            this.Styles[Style.Cpp.CommentDocKeyword].ForeColor = IntToColor(0xB3D991);
            this.Styles[Style.Cpp.CommentDocKeywordError].ForeColor = IntToColor(0xFF0000);
            this.Styles[Style.Cpp.GlobalClass].ForeColor = IntToColor(0x48A8EE);

            this.Lexer = Lexer.Cpp;
            this.SetKeywords(0, "class extends implements import interface new case do while else if for in switch throw get set function var try catch finally while with default break continue delete return each const namespace package include use is as instanceof typeof author copy default deprecated eventType example exampleText exception haxe inheritDoc internal link mtasc mxmlc param private return see serial serialData serialField since throws usage version langversion playerversion productversion dynamic private public partial static intrinsic internal native override protected AS3 final super this arguments null Infinity NaN undefined true false abstract as base break by case catch checked class const continue default delegate do descending explicit event extern else enum false finally fixed for foreach from goto group if implicit in interface internal into is lock new null namespace operator out override orderby params private protected public readonly ref return switch struct sealed sizeof stackalloc static select this throw true try typeof unchecked unsafe using virtual volatile while where yield Project PropertyGroup ItemGroup");
            this.SetKeywords(1, $"void Null ArgumentError arguments Array Boolean Class Date DefinitionError Error EvalError Function int Math Namespace Number Object RangeError ReferenceError RegExp SecurityError String SyntaxError TypeError uint XML XMLList Boolean Byte Char DateTime Decimal Double Int16 Int32 Int64 IntPtr SByte Single UInt16 UInt32 UInt64 UIntPtr Void long ulong uint string int float decimal object bool byte sbyte char short ushort var Color double Path File System Windows Forms ScintillaNET tung95it TargetFramework ImplicitUsings Nullable ProjectReference Reference");
        }

        private void InitBookmarkMargin()
        {
            //this.SetFoldMarginColor(true, IntToColor(BACK_COLOR));

            var margin = this.Margins[BOOKMARK_MARGIN];
            margin.Width = 20;
            margin.Sensitive = true;
            margin.Type = MarginType.Symbol;
            margin.Mask = (1 << BOOKMARK_MARKER);
            //margin.Cursor = MarginCursor.Arrow;

            var marker = this.Markers[BOOKMARK_MARKER];
            marker.Symbol = MarkerSymbol.Circle;
            marker.SetBackColor(IntToColor(0xFF003B));
            marker.SetForeColor(IntToColor(0x000000));
            marker.SetAlpha(100);
        }

        private void InitCodeFolding()
        {
            this.SetFoldMarginColor(true, IntToColor(BACK_COLOR));
            this.SetFoldMarginHighlightColor(true, IntToColor(BACK_COLOR));

            // Enable code folding
            this.SetProperty("fold", "1");
            this.SetProperty("fold.compact", "1");

            // Configure a margin to display folding symbols
            this.Margins[FOLDING_MARGIN].Type = MarginType.Symbol;
            this.Margins[FOLDING_MARGIN].Mask = Marker.MaskFolders;
            this.Margins[FOLDING_MARGIN].Sensitive = true;
            this.Margins[FOLDING_MARGIN].Width = 20;

            // Set colors for all folding markers
            for (int i = 25; i <= 31; i++)
            {
                this.Markers[i].SetForeColor(IntToColor(BACK_COLOR)); // styles for [+] and [-]
                this.Markers[i].SetBackColor(IntToColor(FORE_COLOR)); // styles for [+] and [-]
            }

            // Configure folding markers with respective symbols
            this.Markers[Marker.Folder].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlus : MarkerSymbol.BoxPlus;
            this.Markers[Marker.FolderOpen].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinus : MarkerSymbol.BoxMinus;
            this.Markers[Marker.FolderEnd].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlusConnected : MarkerSymbol.BoxPlusConnected;
            this.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            this.Markers[Marker.FolderOpenMid].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinusConnected : MarkerSymbol.BoxMinusConnected;
            this.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            this.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            // Enable automatic folding
            this.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);
        }

        #endregion Numbers, Bookmarks, Code Folding
    }
}