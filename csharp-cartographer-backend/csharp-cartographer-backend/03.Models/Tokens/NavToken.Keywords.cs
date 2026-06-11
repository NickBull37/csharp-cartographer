using Microsoft.CodeAnalysis.CSharp;

namespace csharp_cartographer_backend._03.Models.Tokens
{
    public partial class NavToken
    {
        public bool IsKeyword() => IsRegularKeyword() || IsControlKeyword();

        public bool IsRegularKeyword() => Classifications.Corrected == Keyword;

        public bool IsControlKeyword() => Classifications.Corrected == KeywordControl;

        public bool IsPredefinedType()
        {
            return Kind
                is SyntaxKind.BoolKeyword
                or SyntaxKind.ByteKeyword
                or SyntaxKind.SByteKeyword
                or SyntaxKind.CharKeyword
                or SyntaxKind.DecimalKeyword
                or SyntaxKind.DoubleKeyword
                or SyntaxKind.FloatKeyword
                or SyntaxKind.IntKeyword
                or SyntaxKind.UIntKeyword
                or SyntaxKind.LongKeyword
                or SyntaxKind.ULongKeyword
                or SyntaxKind.ObjectKeyword
                or SyntaxKind.ShortKeyword
                or SyntaxKind.UShortKeyword
                or SyntaxKind.StringKeyword;
        }

        #region ------------------- Role Checks --------------------
        public bool IsAccessModifierKeyword()
        {
            /// public / private / protected / internal

            return Kind
                is SyntaxKind.PublicKeyword
                or SyntaxKind.PrivateKeyword
                or SyntaxKind.ProtectedKeyword
                or SyntaxKind.InternalKeyword;
        }

        public bool IsAccessorKeyword()
        {
            /// get / set / init / add / remove

            var validKind = Kind
                is SyntaxKind.GetKeyword
                or SyntaxKind.SetKeyword
                or SyntaxKind.InitKeyword
                or SyntaxKind.AddKeyword
                or SyntaxKind.RemoveKeyword;

            var validParent = Ancestors.HasAncestorAt(0, SyntaxKind.GetAccessorDeclaration)
                || Ancestors.HasAncestorAt(0, SyntaxKind.SetAccessorDeclaration)
                || Ancestors.HasAncestorAt(0, SyntaxKind.InitAccessorDeclaration)
                || Ancestors.HasAncestorAt(0, SyntaxKind.AddAccessorDeclaration)
                || Ancestors.HasAncestorAt(0, SyntaxKind.RemoveAccessorDeclaration);

            return validKind && validParent;
        }

        public bool IsArgumentModifierKeyword()
        {
            /// in / out / ref

            var validKind = Kind
                is SyntaxKind.InKeyword
                or SyntaxKind.OutKeyword
                or SyntaxKind.RefKeyword;

            return validKind && Ancestors.HasAncestorAt(0, SyntaxKind.Argument);
        }

        public bool IsCompilationScopeKeyword()
        {
            /// alias / global / namespace / using

            bool validKind = Kind
                is SyntaxKind.AliasKeyword
                or SyntaxKind.GlobalKeyword
                or SyntaxKind.NamespaceKeyword
                or SyntaxKind.UsingKeyword;

            bool validParent = Ancestors.GetParent()
                is SyntaxKind.ExternAliasDirective
                or SyntaxKind.IdentifierName
                or SyntaxKind.NamespaceDeclaration
                or SyntaxKind.UsingDirective;

            return validKind && validParent;
        }

        public bool IsConcurrencyKeyword()
        {
            /// await / lock

            bool validKind = Kind
                is SyntaxKind.AwaitKeyword
                or SyntaxKind.LockKeyword;

            bool validParent = Ancestors.GetParent()
                is SyntaxKind.AwaitExpression
                or SyntaxKind.LockStatement;

            return validKind && validParent;
        }

        public bool IsConditionalBranchingKeyword()
        {
            /// if / else

            var validKind = Kind
                is SyntaxKind.IfKeyword
                or SyntaxKind.ElseKeyword;

            var validParent = Ancestors.GetParent()
                is SyntaxKind.IfStatement
                or SyntaxKind.ElseClause;

            return validKind && validParent;
        }

        public bool IsConstraintKeyword()
        {
            /// notnull, unmanaged, where

            return IsWhereConstraintKeyword() || IsNotNullOrUnmangedConstraintKeyword();

            bool IsWhereConstraintKeyword()
            {
                return Kind is SyntaxKind.WhereKeyword
                    && Ancestors.HasAncestorAt(0, SyntaxKind.TypeParameterConstraintClause);
            }

            bool IsNotNullOrUnmangedConstraintKeyword()
            {
                var validText = Text is "notnull" or "unmanaged";
                var validKind = Kind is SyntaxKind.IdentifierToken;
                var validParent = Ancestors.HasAncestorAt(0, SyntaxKind.IdentifierName)
                    || Ancestors.HasAncestorAt(0, SyntaxKind.TypeConstraint);

                return validText && validKind && validParent;
            }
        }

        public bool IsControlFlowKeyword()
        {
            /// case, default, switch

            var validKind = Kind
                is SyntaxKind.CaseKeyword
                or SyntaxKind.DefaultKeyword
                or SyntaxKind.SwitchKeyword;

            bool validParent = Ancestors.GetParent()
                is SyntaxKind.CaseSwitchLabel
                or SyntaxKind.CasePatternSwitchLabel
                or SyntaxKind.DefaultSwitchLabel
                or SyntaxKind.SwitchExpression
                or SyntaxKind.SwitchStatement;

            return validKind && validParent;
        }

        public bool IsDiscardValueKeyword()
        {
            /// discard (_)

            return IsDiscardAssignment() || IsDiscardDeconstruction();

            bool IsDiscardAssignment()
            {
                bool validText = Text == "_";
                bool validKind = Kind is SyntaxKind.IdentifierToken;
                bool validClass = Classifications.Original == Keyword;

                return validText && validKind && validClass;
            }

            bool IsDiscardDeconstruction()
            {
                bool validKind = Kind is SyntaxKind.UnderscoreToken;
                bool validParent = Ancestors.HasAncestorAt(0, SyntaxKind.DiscardDesignation);

                return validKind && validParent;
            }
        }

        public bool IsDiscardPatternKeyword()
        {
            /// discard (_) in switch expressions

            return Kind == SyntaxKind.UnderscoreToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.DiscardPattern);
        }

        public bool IsExceptionHandlingKeyword()
        {
            /// try / catch / finally / throw

            var validKind = Kind
                is SyntaxKind.TryKeyword
                or SyntaxKind.CatchKeyword
                or SyntaxKind.FinallyKeyword
                or SyntaxKind.ThrowKeyword;

            bool validParent = Ancestors.GetParent()
                is SyntaxKind.CatchClause
                or SyntaxKind.FinallyClause
                or SyntaxKind.ThrowStatement
                or SyntaxKind.TryStatement;

            return validKind && validParent;
        }

        public bool IsIteratorKeyword()
        {
            /// yield

            return Kind == SyntaxKind.YieldKeyword
                && Ancestors.HasAncestorAt(0, SyntaxKind.YieldReturnStatement);
        }

        public bool IsJumpStatementKeyword()
        {
            /// break / continue / goto / return

            var validKind = Kind
                is SyntaxKind.BreakKeyword
                or SyntaxKind.ContinueKeyword
                or SyntaxKind.GotoKeyword
                or SyntaxKind.ReturnKeyword;

            var validParent = Ancestors.GetParent()
                is SyntaxKind.BreakStatement
                or SyntaxKind.ContinueStatement
                or SyntaxKind.GotoStatement
                or SyntaxKind.ReturnStatement
                or SyntaxKind.YieldReturnStatement;

            return validKind && validParent;
        }

        public bool IsLoopStatementKeyword()
        {
            /// do / for / foreach / in / while

            var validKind = Kind
                is SyntaxKind.DoKeyword
                or SyntaxKind.ForKeyword
                or SyntaxKind.ForEachKeyword
                or SyntaxKind.InKeyword
                or SyntaxKind.WhileKeyword;

            var validParent = Ancestors.GetParent()
                is SyntaxKind.DoStatement
                or SyntaxKind.ForStatement
                or SyntaxKind.ForEachStatement
                or SyntaxKind.ForEachVariableStatement
                or SyntaxKind.WhileStatement;

            return validKind && validParent;
        }

        public bool IsMemberDeclarationKeyword()
        {
            /// event / operator

            var validKind = Kind
                is SyntaxKind.EventKeyword
                or SyntaxKind.OperatorKeyword;

            var validParent = Ancestors.GetParent()
                is SyntaxKind.ConversionOperatorDeclaration
                or SyntaxKind.EventDeclaration
                or SyntaxKind.EventFieldDeclaration
                or SyntaxKind.OperatorDeclaration;

            return validKind && validParent;
        }

        public bool IsMemberModifierKeyword()
        {
            /// abstract / async / const / explicit / extern / implicit / new / override
            /// partial / readonly / required / sealed / static / unsafe / virtual / volatile

            var validKind = Kind
                is SyntaxKind.AbstractKeyword
                or SyntaxKind.AsyncKeyword
                or SyntaxKind.ConstKeyword
                or SyntaxKind.ExplicitKeyword
                or SyntaxKind.ExternKeyword
                or SyntaxKind.ImplicitKeyword
                or SyntaxKind.NewKeyword
                or SyntaxKind.OverrideKeyword
                or SyntaxKind.PartialKeyword
                or SyntaxKind.ReadOnlyKeyword
                or SyntaxKind.RequiredKeyword
                or SyntaxKind.SealedKeyword
                or SyntaxKind.StaticKeyword
                or SyntaxKind.UnsafeKeyword
                or SyntaxKind.VirtualKeyword
                or SyntaxKind.VolatileKeyword;

            var validParent = Ancestors.GetParent()
                is SyntaxKind.ConversionOperatorDeclaration
                or SyntaxKind.EventDeclaration
                or SyntaxKind.EventFieldDeclaration
                or SyntaxKind.FieldDeclaration
                or SyntaxKind.MethodDeclaration
                or SyntaxKind.OperatorDeclaration
                or SyntaxKind.PropertyDeclaration;

            return validKind && validParent;
        }

        public bool IsObjectConstructionKeyword()
        {
            /// base / new / this / with

            var validKind = Kind
                is SyntaxKind.BaseKeyword
                or SyntaxKind.NewKeyword
                or SyntaxKind.ThisKeyword
                or SyntaxKind.WithKeyword;

            var validParent = Ancestors.GetParent()
                is SyntaxKind.AnonymousObjectCreationExpression
                or SyntaxKind.ArrayCreationExpression
                or SyntaxKind.ImplicitArrayCreationExpression
                or SyntaxKind.ImplicitObjectCreationExpression
                or SyntaxKind.ObjectCreationExpression
                or SyntaxKind.ThisExpression
                or SyntaxKind.WithExpression;

            return validKind && validParent;
        }

        public bool IsParameterModifierKeyword()
        {
            /// in / out / params / ref / scoped / this

            var validKind = Kind
                is SyntaxKind.InKeyword
                or SyntaxKind.OutKeyword
                or SyntaxKind.ParamsKeyword
                or SyntaxKind.RefKeyword
                or SyntaxKind.ScopedKeyword
                or SyntaxKind.ThisKeyword;

            return validKind && Ancestors.HasAncestorAt(0, SyntaxKind.Parameter);
        }

        public bool IsPatternMatchingKeyword()
        {
            /// and, is, not, or, var, when

            var validKind = Kind
                is SyntaxKind.IsKeyword
                or SyntaxKind.AndKeyword
                or SyntaxKind.NotKeyword
                or SyntaxKind.OrKeyword
                or SyntaxKind.VarKeyword
                or SyntaxKind.WhenKeyword;

            var validParent = Ancestors.GetParent()
                is SyntaxKind.IsPatternExpression
                or SyntaxKind.AndPattern
                or SyntaxKind.NotPattern
                or SyntaxKind.OrPattern
                or SyntaxKind.VarPattern
                or SyntaxKind.WhenClause
                or SyntaxKind.CatchFilterClause;

            return validKind && validParent;
        }

        public bool IsQueryExpressionKeyword()
        {
            /// ascending / by / descending / equals / from / group /
            /// in / into / join / let / on / orderby / select / where

            var validKind = Kind
                is SyntaxKind.AscendingKeyword
                or SyntaxKind.ByKeyword
                or SyntaxKind.DescendingKeyword
                or SyntaxKind.EqualsKeyword
                or SyntaxKind.FromKeyword
                or SyntaxKind.GroupKeyword
                or SyntaxKind.InKeyword
                or SyntaxKind.IntoKeyword
                or SyntaxKind.JoinKeyword
                or SyntaxKind.LetKeyword
                or SyntaxKind.OnKeyword
                or SyntaxKind.OrderByKeyword
                or SyntaxKind.SelectKeyword
                or SyntaxKind.WhereKeyword;

            var validParent = Ancestors.GetParent()
                is SyntaxKind.AscendingOrdering
                or SyntaxKind.DescendingOrdering
                or SyntaxKind.FromClause
                or SyntaxKind.GroupClause
                or SyntaxKind.JoinClause
                or SyntaxKind.JoinIntoClause
                or SyntaxKind.LetClause
                or SyntaxKind.OrderByClause
                or SyntaxKind.QueryContinuation
                or SyntaxKind.SelectClause
                or SyntaxKind.WhereClause;

            return validKind && validParent;
        }

        public bool IsResourceManagementKeyword()
        {
            /// using

            var validKind = Kind is SyntaxKind.UsingKeyword;

            var validParent = Ancestors.GetParent()
                is SyntaxKind.LocalDeclarationStatement
                or SyntaxKind.UsingStatement;

            return validKind && validParent;
        }

        public bool IsSafetyContextKeyword()
        {
            /// checked / fixed / stackalloc / unchecked / unsafe

            var validKind = Kind
                is SyntaxKind.CheckedKeyword
                or SyntaxKind.FixedKeyword
                or SyntaxKind.StackAllocKeyword
                or SyntaxKind.UncheckedKeyword
                or SyntaxKind.UnsafeKeyword;

            var validParent = Ancestors.GetParent()
                is SyntaxKind.CheckedStatement
                or SyntaxKind.FixedStatement
                or SyntaxKind.StackAllocArrayCreationExpression
                or SyntaxKind.UncheckedStatement
                or SyntaxKind.UnsafeStatement;

            return validKind && validParent;
        }

        public bool IsTypeDeclarationKeyword()
        {
            /// class / delegate / enum / extension / interface / operator / record / struct

            var validKind = Kind
                is SyntaxKind.ClassKeyword
                or SyntaxKind.DelegateKeyword
                or SyntaxKind.EnumKeyword
                or SyntaxKind.ExtensionKeyword
                or SyntaxKind.InterfaceKeyword
                or SyntaxKind.RecordKeyword
                or SyntaxKind.StructKeyword;

            var validParent = Ancestors.GetParent()
                is SyntaxKind.ClassDeclaration
                or SyntaxKind.DelegateDeclaration
                or SyntaxKind.EnumDeclaration
                or SyntaxKind.ExtensionBlockDeclaration
                or SyntaxKind.InterfaceDeclaration
                or SyntaxKind.RecordDeclaration
                or SyntaxKind.RecordStructDeclaration
                or SyntaxKind.StructDeclaration;

            return validKind && validParent;
        }

        public bool IsTypeModifierKeyword()
        {
            /// abstract / file / partial / readonly / ref / sealed / static / unsafe

            var validKind = Kind
                is SyntaxKind.AbstractKeyword
                or SyntaxKind.FileKeyword
                or SyntaxKind.PartialKeyword
                or SyntaxKind.ReadOnlyKeyword
                or SyntaxKind.RefKeyword
                or SyntaxKind.SealedKeyword
                or SyntaxKind.StaticKeyword
                or SyntaxKind.UnsafeKeyword;

            var validParent = Ancestors.GetParent()
                is SyntaxKind.ClassDeclaration
                or SyntaxKind.DelegateDeclaration
                or SyntaxKind.EnumDeclaration
                or SyntaxKind.ExtensionBlockDeclaration
                or SyntaxKind.InterfaceDeclaration
                or SyntaxKind.RecordDeclaration
                or SyntaxKind.RecordStructDeclaration
                or SyntaxKind.StructDeclaration;

            return validKind && validParent;
        }

        public bool IsTypeSystemKeyword()
        {
            /// bool / byte / sbyte / char / decimal / double / float / int / uint
            /// nint* / nuint* / long / ulong / object / short / ushort / string

            var validKind = Kind
                is SyntaxKind.BoolKeyword
                or SyntaxKind.ByteKeyword
                or SyntaxKind.SByteKeyword
                or SyntaxKind.CharKeyword
                or SyntaxKind.DecimalKeyword
                or SyntaxKind.DoubleKeyword
                or SyntaxKind.FloatKeyword
                or SyntaxKind.IntKeyword
                or SyntaxKind.UIntKeyword
                or SyntaxKind.LongKeyword
                or SyntaxKind.ULongKeyword
                or SyntaxKind.ObjectKeyword
                or SyntaxKind.ShortKeyword
                or SyntaxKind.UShortKeyword
                or SyntaxKind.StringKeyword;

            var validAncestors =
                Ancestors.HasAncestorAt(0, SyntaxKind.PredefinedType) &&
                Ancestors.HasAncestorAt(1, SyntaxKind.ObjectCreationExpression);

            return (validKind && validAncestors) || IsNintOrNuint();

            bool IsNintOrNuint()
            {
                var validText = Text is "nint" or "nuint";
                var validKind = Kind is SyntaxKind.IdentifierToken;
                var validClass = Classifications.Corrected == Keyword;
                var validParent = Ancestors.HasAncestorAt(1, SyntaxKind.ObjectCreationExpression);
                var validSemantics = SemanticData?.SymbolName is "IntPtr" or "UIntPtr";

                return validText && validKind && validClass && validParent && validSemantics;
            }
        }

        public bool IsUsingDirectiveModifierKeyword()
        {
            /// global / static

            var validKind = Kind
                is SyntaxKind.GlobalKeyword
                or SyntaxKind.StaticKeyword;

            return validKind && Ancestors.HasAncestorAt(0, SyntaxKind.UsingDirective);
        }
        #endregion

        #region ------------------- Key Checks --------------------
        public bool IsDefaultLiteral()
        {
            return Kind == SyntaxKind.DefaultKeyword
                && Ancestors.HasAncestorAt(0, SyntaxKind.DefaultLiteralExpression);
        }

        public bool IsVarPatternKeyword()
        {
            return Kind == SyntaxKind.VarKeyword
                && Ancestors.HasAncestorAt(0, SyntaxKind.VarPattern);
        }
        #endregion
    }
}
