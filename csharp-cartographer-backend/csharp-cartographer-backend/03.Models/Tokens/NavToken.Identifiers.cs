using csharp_cartographer_backend._01.Configuration.Enums;
using csharp_cartographer_backend._02.Utilities.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Immutable;

namespace csharp_cartographer_backend._03.Models.Tokens
{
    public partial class NavToken
    {
        private static List<SemanticRole> DeclarationRoles =
        [
            SemanticRole.ConstantDeclaration,
            SemanticRole.FieldDeclaration,
            SemanticRole.LambdaParameter,
            SemanticRole.LocalVariableDeclaration,
            SemanticRole.LoopIteratorDeclaration,
            SemanticRole.Parameter,
            SemanticRole.ParameterLabel, // not a declaration but still invalid
        ];

        public bool IsIdentifier() => Kind is SyntaxKind.IdentifierToken;

        #region ------------------- Group Checks --------------------
        public bool IsInvocationIdentifier()
        {
            return SemanticRole
                is SemanticRole.ConstructorInvocation
                or SemanticRole.GenericMethodInvocation
                or SemanticRole.MethodInvocation;
        }

        public bool IsLocalDeclarationIdentifier()
        {
            return SemanticRole
                is SemanticRole.CatchExceptionVariable
                or SemanticRole.DeconstructionVariable
                or SemanticRole.FixedPointerDeclaration
                or SemanticRole.LocalConstantDeclaration
                or SemanticRole.LocalFunctionDeclaration
                or SemanticRole.LocalVariableDeclaration
                or SemanticRole.LoopIteratorDeclaration
                or SemanticRole.OutVariableDeclaration
                or SemanticRole.PatternBindingVariable
                or SemanticRole.UsingResourceDeclaration;
        }

        public bool IsMemberDeclarationIdentifier()
        {
            return SemanticRole
                is SemanticRole.ConstantDeclaration
                or SemanticRole.ConstructorDeclaration
                or SemanticRole.EnumMemberDeclaration
                or SemanticRole.EventFieldDeclaration
                or SemanticRole.EventPropertyDeclaration
                or SemanticRole.FieldDeclaration
                or SemanticRole.GenericMethodDeclaration
                or SemanticRole.MethodDeclaration
                or SemanticRole.OperatorDeclaration
                or SemanticRole.PropertyDeclaration;
        }

        public bool IsParameterDeclarationIdentifier()
        {
            return SemanticRole
                is SemanticRole.LambdaParameter
                or SemanticRole.Parameter;
        }

        public bool IsTypeDeclarationIdentifier()
        {
            return SemanticRole
                is SemanticRole.ClassDeclaration
                or SemanticRole.DelegateDeclaration
                or SemanticRole.EnumDeclaration
                or SemanticRole.InterfaceDeclaration
                or SemanticRole.RecordDeclaration
                or SemanticRole.RecordStructDeclaration
                or SemanticRole.StructDeclaration;
        }
        #endregion

        #region ------------------- Role Checks --------------------

        /// non-grouped
        public bool IsAssignmentRecipient()
        {
            if (NextToken is null)
                return false;

            return Kind == SyntaxKind.IdentifierToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.IdentifierName)
                && NextToken.IsAssignmentOperator();
        }

        public bool IsAttribute()
        {
            return Ancestors.HasAncestorAt(1, SyntaxKind.Attribute);
        }

        public bool IsBaseType()
        {
            return Kind == SyntaxKind.IdentifierToken
                && Ancestors.HasAncestorAt(1, SyntaxKind.SimpleBaseType);
        }

        public bool IsCatchExceptionType()
        {
            return Kind == SyntaxKind.IdentifierToken
                && Ancestors.HasAncestorAt(1, SyntaxKind.CatchDeclaration);
        }

        public bool IsConditionValue()
        {
            bool validAncestors =
                Ancestors.HasAncestorAt(0, SyntaxKind.IdentifierName) &&
                Ancestors.HasAncestorAt(1, SyntaxKind.IfStatement);

            bool validNeighbors =
                PrevToken?.Kind == SyntaxKind.OpenParenToken &&
                NextToken?.Kind == SyntaxKind.CloseParenToken;

            return validAncestors && validNeighbors;
        }

        public bool IsEventFieldType()
        {
            // covers nullable and non-nullable types
            return Ancestors.HasAncestorAt(2, SyntaxKind.EventFieldDeclaration)
                || Ancestors.HasAncestorAt(3, SyntaxKind.EventFieldDeclaration);
        }

        public bool IsEventPropertyType()
        {
            // covers nullable and non-nullable types
            return Ancestors.HasAncestorAt(1, SyntaxKind.EventDeclaration)
                || Ancestors.HasAncestorAt(2, SyntaxKind.EventDeclaration);
        }

        public bool IsForEachLoopCollectionIdentifier()
        {
            bool validNext = NextToken?.Kind == SyntaxKind.CloseParenToken;
            bool validParent = Ancestors.HasAncestorAt(0, SyntaxKind.IdentifierName);
            bool validGrandParent = Ancestors.GetGrandParent()
                is SyntaxKind.ForEachStatement
                or SyntaxKind.ForEachVariableStatement;

            return validNext && validParent && validGrandParent;
        }

        public bool IsLockTarget()
        {
            bool validNeighbors =
                PrevToken?.Kind == SyntaxKind.OpenParenToken &&
                NextToken?.Kind == SyntaxKind.CloseParenToken;

            bool validAncestors =
                Ancestors.HasAncestorAt(0, SyntaxKind.IdentifierName) &&
                Ancestors.HasAncestorAt(1, SyntaxKind.LockStatement);

            return validNeighbors && validAncestors;
        }

        public bool IsParameterLabel()
        {
            if (IsPropertyPattern())
                return false;

            return Kind == SyntaxKind.IdentifierToken
                && Ancestors.HasAncestorAt(1, SyntaxKind.NameColon)
                && NextToken?.Kind == SyntaxKind.ColonToken;
        }

        public bool IsTargetMember()
        {
            return IsTargetMember()
                || IsConditionalAccessTargetMember()
                || IsPointerTargetMember();

            bool IsTargetMember()
            {
                bool validPrev = PrevToken?.Kind == SyntaxKind.DotToken;
                bool validPrevPrev = PrevToken?.PrevToken?.SemanticRole
                    is not SemanticRole.AliasQualifier
                    and not SemanticRole.NamespaceQualifier;
                bool validAncestors = Ancestors.GetGrandParent() is SyntaxKind.SimpleMemberAccessExpression or SyntaxKind.QualifiedName
                    && Ancestors.GetLastAncestor() is not SyntaxKind.UsingDirective;

                return IsIdentifier()
                    && validPrev
                    && validPrevPrev
                    && validAncestors;
            }

            bool IsConditionalAccessTargetMember()
            {
                bool validPrev = PrevToken?.Kind == SyntaxKind.DotToken;
                bool validPrevPrev = PrevToken?.PrevToken?.Kind == SyntaxKind.QuestionToken;
                bool validParent = Ancestors.HasAncestorAt(1, SyntaxKind.MemberBindingExpression);
                bool validGrandParent = Ancestors.HasAncestorAt(2, SyntaxKind.ConditionalAccessExpression)
                    || Ancestors.HasAncestorAt(2, SyntaxKind.SimpleMemberAccessExpression);

                return IsIdentifier()
                    && validPrev
                    && validPrevPrev
                    && validParent
                    && validGrandParent;
            }

            bool IsPointerTargetMember()
            {
                return PrevToken?.Kind == SyntaxKind.MinusGreaterThanToken
                    && Ancestors.HasAncestorAt(1, SyntaxKind.PointerMemberAccessExpression);
            }
        }

        public bool IsTernaryCondition()
        {
            return Kind == SyntaxKind.IdentifierToken
                && NextToken?.Kind == SyntaxKind.QuestionToken
                && Ancestors.HasAncestorAt(1, SyntaxKind.ConditionalExpression);
        }

        public bool IsTupleElementName()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.TupleElement);
        }

        public bool IsTypeIdentifier()
        {
            bool validSymbol = SemanticData?.IsTypeSymbol == true;
            bool validAncestor = Ancestors.GetLastAncestor() == SyntaxKind.UsingDirective;

            return IsIdentifier()
                && validSymbol
                && validAncestor;
        }

        public bool IsWithExpressionSource()
        {
            return Kind == SyntaxKind.IdentifierToken
                && NextToken?.Kind == SyntaxKind.WithKeyword
                && Ancestors.HasAncestorAt(1, SyntaxKind.WithExpression);
        }

        /// Alias Declarations group
        public bool IsNamespaceAliasDeclarationIdentifier()
        {
            if (SemanticData is null)
                return false;

            //       ⌄
            // using IO = System.IO;
            bool validTarget = SemanticData.AliasTargetSymbol?.Kind
                is SymbolKind.Namespace
                or SymbolKind.ErrorType; // default to namespace alias if target symbol can't be identified

            bool validAncestor = Ancestors.GetLastAncestor() == SyntaxKind.UsingDirective;

            return validTarget && validAncestor && SemanticData.IsAliasSymbol;
        }

        public bool IsTypeAliasDeclarationIdentifier()
        {
            if (SemanticData is null)
                return false;

            // alias declarations only appear in using statements, other instances are alias references
            if (Ancestors.GetLastAncestor() != SyntaxKind.UsingDirective)
                return false;

            //          ⌄
            // using Handler = System.Action<int>;
            return SemanticData.IsAliasSymbol
                && SemanticData.AliasTargetSymbol?.Kind == SymbolKind.NamedType;
        }

        /// Invocations group
        public bool IsConstructorInvocation()
        {
            return IsInvocation() || IsQualifiedInvocation();

            bool IsInvocation()
            {
                return Ancestors.HasAncestorAt(1, SyntaxKind.ObjectCreationExpression);
            }

            /// var token = new MyToken.NavToken();
            bool IsQualifiedInvocation()
            {
                return PrevToken?.Kind == SyntaxKind.DotToken
                    && Ancestors.HasAncestorAt(1, SyntaxKind.QualifiedName)
                    && Ancestors.HasAncestorAt(2, SyntaxKind.ObjectCreationExpression);
            }
        }

        public bool IsGenericMethodInvocation()
        {
            var validNext = NextToken?.Text == "<";
            if (!validNext)
                return false;

            var validAncestors =
                Ancestors.HasAncestorAt(1, SyntaxKind.InvocationExpression) ||
                Ancestors.HasAncestorAt(2, SyntaxKind.InvocationExpression);

            bool hasArgList = false;
            var forwardToken = NextToken;
            while (forwardToken?.Text != "." && forwardToken?.Text != ";")
            {
                var foundClosingClip = forwardToken?.Text == ">";
                if (foundClosingClip && forwardToken?.NextToken?.Text == "(")
                {
                    hasArgList = true;
                }

                forwardToken = forwardToken?.NextToken;
            }

            return validAncestors && hasArgList;
        }

        public bool IsMethodInvocation()
        {
            var validNext = NextToken?.Kind == SyntaxKind.OpenParenToken;
            var validAncestors =
                Ancestors.HasAncestorAt(1, SyntaxKind.InvocationExpression) ||
                Ancestors.HasAncestorAt(2, SyntaxKind.InvocationExpression);

            return validNext && validAncestors;
        }

        /// Local Declarations group
        public bool IsCatchExceptionVariable()
        {
            return Kind == SyntaxKind.IdentifierToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.CatchDeclaration);
        }

        public bool IsDeconstructionVariable()
        {
            /// var (x, y) = (10, 20);
            /// (left, right) = (100, 200);

            bool prevValidText = PrevToken?.Text is "(" or ",";
            bool prevValidRole = PrevToken?.SemanticRole
                is SemanticRole.DeconstructionBoundary
                or SemanticRole.DeconstructionVariableSeparator;

            bool nextValidText = NextToken?.Text is "," or ")";

            bool prevValid = prevValidText && prevValidRole;
            bool nextValid = nextValidText;
            bool parentValid = Ancestors.HasAncestorAt(0, SyntaxKind.SingleVariableDesignation)
                || Ancestors.HasAncestorAt(0, SyntaxKind.IdentifierName);

            if (prevValid && nextValid && parentValid)
                return true;

            /// (int id2, string name2) = GetUser();
            bool hasValidAncestors = Ancestors.HasAncestorAt(0, SyntaxKind.SingleVariableDesignation)
                && Ancestors.HasAncestorAt(1, SyntaxKind.DeclarationExpression)
                && Ancestors.HasAncestorAt(2, SyntaxKind.Argument)
                && Ancestors.HasAncestorAt(3, SyntaxKind.TupleExpression);

            if (Kind == SyntaxKind.IdentifierToken && hasValidAncestors)
                return true;

            return false;
        }

        public bool IsFixedPointerDeclaration()
        {
            return Kind == SyntaxKind.IdentifierToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.VariableDeclarator)
                && Ancestors.HasAncestorAt(2, SyntaxKind.FixedStatement);
        }

        public bool IsLocalConstantDeclaration()
        {
            return Classifications.Corrected == ConstantName
                && Ancestors.HasAncestorAt(0, SyntaxKind.VariableDeclarator)
                && Ancestors.HasAncestorAt(1, SyntaxKind.VariableDeclaration)
                && Ancestors.HasAncestorAt(2, SyntaxKind.LocalDeclarationStatement);
        }

        public bool IsLocalFunctionDeclaration()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.LocalFunctionStatement);
        }

        public bool IsLocalVariableDeclaration()
        {
            // covered by UsingResourceDeclaration role
            if (IsUsingResourceDeclaration())
                return false;

            return Classifications.Corrected == LocalName
                && Ancestors.HasAncestorAt(2, SyntaxKind.LocalDeclarationStatement);
        }

        public bool IsLoopIteratorDeclaration()
        {
            return IsForLoopIterator() || IsForEachLoopIterator();

            bool IsForLoopIterator()
            {
                return Ancestors.HasAncestorAt(0, SyntaxKind.VariableDeclarator)
                    && Ancestors.HasAncestorAt(1, SyntaxKind.VariableDeclaration)
                    && Ancestors.HasAncestorAt(2, SyntaxKind.ForStatement);
            }

            bool IsForEachLoopIterator()
            {
                return NextToken?.Kind == SyntaxKind.InKeyword
                    && Ancestors.HasAncestorAt(0, SyntaxKind.ForEachStatement);
            }
        }

        public bool IsOutVariableDeclaration()
        {
            return Kind == SyntaxKind.IdentifierToken
                && Ancestors.HasAncestorAt(0, SyntaxKind.SingleVariableDesignation)
                && Ancestors.HasAncestorAt(1, SyntaxKind.DeclarationExpression)
                && Ancestors.HasAncestorAt(2, SyntaxKind.Argument)
                && !Ancestors.HasAncestorAt(3, SyntaxKind.TupleExpression);
        }

        public bool IsUsingResourceDeclaration()
        {
            return IsInlineUsingResource() || IsUsingBlockResource();

            /// using var reader = new StreamReader(path);
            bool IsInlineUsingResource()
            {
                bool validPrev =
                    PrevToken?.PrevToken?.Kind == SyntaxKind.UsingKeyword ||
                    PrevToken?.PrevToken?.PrevToken?.Kind == SyntaxKind.UsingKeyword;

                return validPrev
                    && Ancestors.HasAncestorAt(0, SyntaxKind.VariableDeclarator)
                    && Ancestors.HasAncestorAt(1, SyntaxKind.VariableDeclaration)
                    && Ancestors.HasAncestorAt(2, SyntaxKind.LocalDeclarationStatement);
            }

            /// using (var reader = new StreamReader(path))
            bool IsUsingBlockResource()
            {
                return Ancestors.HasAncestorAt(0, SyntaxKind.VariableDeclarator)
                    && Ancestors.HasAncestorAt(1, SyntaxKind.VariableDeclaration)
                    && Ancestors.HasAncestorAt(2, SyntaxKind.UsingStatement);
            }
        }

        /// Member Declarations group
        public bool IsConstantDeclaration()
        {
            return Classifications.Corrected == ConstantName
                && Ancestors.HasAncestorAt(0, SyntaxKind.VariableDeclarator)
                && Ancestors.HasAncestorAt(2, SyntaxKind.FieldDeclaration);
        }

        public bool IsConstructorDeclaration()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.ConstructorDeclaration);
        }

        public bool IsEnumMemberDeclaration()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.EnumMemberDeclaration);
        }

        public bool IsEventFieldDeclaration()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.VariableDeclarator)
                && Ancestors.HasAncestorAt(1, SyntaxKind.VariableDeclaration)
                && Ancestors.HasAncestorAt(2, SyntaxKind.EventFieldDeclaration);
        }

        public bool IsEventPropertyDeclaration()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.EventDeclaration);
        }

        public bool IsFieldDeclaration()
        {
            return Classifications.Corrected == FieldName
                && Ancestors.HasAncestorAt(0, SyntaxKind.VariableDeclarator)
                && Ancestors.HasAncestorAt(2, SyntaxKind.FieldDeclaration);
        }

        public bool IsGenericMethodDeclaration()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.MethodDeclaration)
                && NextToken?.Kind == SyntaxKind.LessThanToken;
        }

        public bool IsMethodDeclaration()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.MethodDeclaration)
                && NextToken?.Kind == SyntaxKind.OpenParenToken;
        }

        public bool IsPropertyDeclaration()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.PropertyDeclaration);
        }

        /// Parameter Declarations group
        public bool IsLambdaParameterDeclaration()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.Parameter)
                && Ancestors.HasAncestorAt(1, SyntaxKind.SimpleLambdaExpression);
        }

        public bool IsParameterDeclaration()
        {
            // skip lambda params
            if (Ancestors.HasAncestorAt(1, SyntaxKind.SimpleLambdaExpression))
                return false;

            return Ancestors.HasAncestorAt(0, SyntaxKind.Parameter);
        }

        /// Qualifiers group
        public bool IsAliasQualifier()
        {
            return SemanticData?.IsAliasSymbol == true
                && Ancestors.GetLastAncestor() != SyntaxKind.UsingDirective;
        }

        public bool IsCollectionInstanceQualifer()
        {
            /// span[0] = 42;
            return NextToken?.Kind == SyntaxKind.OpenBracketToken
                && Ancestors.HasAncestorAt(1, SyntaxKind.ElementAccessExpression);
        }

        public bool IsImplicitInstanceQualifier()
        {
            // TODO: this role incorrectly covers type qualifiers that share the name of a property
            //       defined in the file (see NavToken.cs: SemanticRole)

            bool validClass = Classifications.Corrected == PropertyName;
            bool validParent = !Ancestors.HasAncestorAt(0, SyntaxKind.PropertyDeclaration);
            bool validPrev = PrevToken?.Kind is not SyntaxKind.DotToken;
            bool validNext = NextToken?.Kind
                is SyntaxKind.DotToken
                or SyntaxKind.QuestionToken
                or SyntaxKind.ExclamationToken;

            return IsIdentifier()
                && validClass
                && validParent
                && validPrev
                && validNext;
        }

        public bool IsInstanceQualifier()
        {
            /*
             *  identifier class must only check query expressions 
             *  or will classify type qualifiers unintentionally
             */

            return Classifications.Corrected switch
            {
                FieldName or LocalName or ParameterName => HasValidAncestors(),
                Identifier => Ancestors.HasAncestor(SyntaxKind.QueryExpression) && HasValidAncestors(),
                _ => false,
            };

            bool HasValidAncestors()
            {
                return (NextToken?.Kind) switch
                {
                    SyntaxKind.DotToken => Ancestors.HasAncestorAt(1, SyntaxKind.SimpleMemberAccessExpression),
                    SyntaxKind.QuestionToken => Ancestors.HasAncestorAt(1, SyntaxKind.ConditionalAccessExpression),
                    SyntaxKind.MinusGreaterThanToken => Ancestors.HasAncestorAt(1, SyntaxKind.PointerMemberAccessExpression),
                    SyntaxKind.ExclamationToken => Ancestors.HasAncestorAt(1, SyntaxKind.SuppressNullableWarningExpression)
                                                && Ancestors.HasAncestorAt(2, SyntaxKind.SimpleMemberAccessExpression),
                    _ => false,
                };
            }
        }

        public bool IsNamespaceQualifier()
        {
            if (IsAliasQualifier())
                return false;

            /// global using System.Text;
            /// using IO = System.IO;
            /// bool test = System.IO.File.Exists("demo.txt");
            /// namespace Testing_Namespace
            /// var token = new _03.Models.Tokens.NavToken();
            /// System.Console.WriteLine("test");
            /// global::System.Console.WriteLine(global::System.DateTime.Now);

            return IsInlineNamespaceQualifier()
                || IsNamespaceDeclarationQualifier()
                || IsUsingDirectiveNamespaceQualifier()
                || IsSystemNamespaceQualifier();

            bool IsInlineNamespaceQualifier()
            {
                bool validKind = Kind == SyntaxKind.IdentifierToken;
                bool validPrev = PrevToken?.Kind != SyntaxKind.IsKeyword;
                bool validNext = NextToken?.Kind == SyntaxKind.DotToken;
                bool validClassification = Classifications.Corrected is NamespaceName or Identifier;
                bool validAncestor = Ancestors.HasAncestor(SyntaxKind.QualifiedName)
                    && Ancestors.GetLastAncestor() != SyntaxKind.UsingDirective;
                bool validSemanticData = SemanticData?.SymbolKind == SymbolKind.Namespace
                    && SemanticData?.IsAliasSymbol == false;

                return validKind
                    && validPrev
                    && validNext
                    && validClassification
                    && (validAncestor || validSemanticData);
            }

            bool IsNamespaceDeclarationQualifier()
            {
                // check ancestors minus "QualifiedName" since namespaces
                // vary in length of qualifying segments
                var expectedAncestors = ImmutableArray.Create(
                    SyntaxKind.IdentifierName,
                    SyntaxKind.NamespaceDeclaration);

                bool validKind = Kind == SyntaxKind.IdentifierToken;
                bool validClassification = Classifications.Corrected == NamespaceName;
                bool validAncestors = Ancestors.Ancestors
                    .Where(kind => kind != SyntaxKind.QualifiedName)
                    .Distinct()
                    .ToHashSet()
                    .SetEquals(expectedAncestors);

                return validKind && validClassification && validAncestors;
            }

            bool IsUsingDirectiveNamespaceQualifier()
            {
                return IsIdentifiableNamespace() || IsUnIdentifiableNamespace();

                /// using IO = System.IO;
                bool IsIdentifiableNamespace()
                {
                    bool validClass = Classifications.Corrected is NamespaceName or Identifier;
                    bool validSemanticData = SemanticData?.SymbolKind == SymbolKind.Namespace;
                    bool validAncestor = Ancestors.GetLastAncestor() == SyntaxKind.UsingDirective;

                    return IsIdentifier()
                        && validClass
                        && validSemanticData
                        && validAncestor;
                }

                /// using MyToken = csharp_cartographer_backend._03.Models.Tokens;
                bool IsUnIdentifiableNamespace()
                {
                    bool validSymbol = SemanticData?.IsTypeSymbol != true;
                    bool validAncestor = Ancestors.HasAncestor(SyntaxKind.QualifiedName)
                        && Ancestors.GetLastAncestor() == SyntaxKind.UsingDirective;

                    return IsIdentifier()
                        && validSymbol
                        && validAncestor;
                }
            }

            bool IsSystemNamespaceQualifier()
            {
                // "System" namespace looks a like regular TypeQualifer (use semantic data)
                bool validKind = Kind == SyntaxKind.IdentifierToken;
                bool validText = Text == "System";
                bool validSemanticData = SemanticData?.SymbolKind == SymbolKind.Namespace;

                return validKind && validText && validSemanticData;
            }
        }

        /// Type Declarations group
        public bool IsClassDeclaration()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.ClassDeclaration);
        }

        public bool IsDelegateDeclaration()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.DelegateDeclaration);
        }

        public bool IsEnumDeclaration()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.EnumDeclaration);
        }

        public bool IsInterfaceDeclaration()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.InterfaceDeclaration);
        }

        public bool IsRecordDeclaration()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.RecordDeclaration);
        }

        public bool IsRecordStructDeclaration()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.RecordStructDeclaration);
        }

        public bool IsStructDeclaration()
        {
            return Ancestors.HasAncestorAt(0, SyntaxKind.StructDeclaration);
        }

        /// Query Expressions group
        public bool IsQueryExpressionVariable()
        {
            List<SyntaxKind> kinds =
            [
                SyntaxKind.FromClause,
                SyntaxKind.LetClause,
                SyntaxKind.JoinClause,
                SyntaxKind.QueryContinuation,
                SyntaxKind.QueryBody,
                SyntaxKind.QueryExpression,
                SyntaxKind.WhereClause,
                SyntaxKind.WhenClause,
                SyntaxKind.GroupClause,
                SyntaxKind.OrderByClause,
                SyntaxKind.SelectClause,
                SyntaxKind.AscendingOrdering,
                SyntaxKind.DescendingOrdering,
            ];

            foreach (var kind in Ancestors.Ancestors)
            {
                if (kinds.Contains(kind))
                    return true;
            }

            return false;
        }

        public bool IsRangeVariable()
        {
            //                  ⌄
            // var query = from n in numbers
            return Ancestors.HasAncestorAt(0, SyntaxKind.FromClause)
                && Ancestors.HasAncestorAt(1, SyntaxKind.QueryExpression)
                && PrevToken?.Text == "from";
        }

        public bool IsQuerySource()
        {
            //                          ⌄
            // var query = from n in numbers
            return Ancestors.HasAncestorAt(0, SyntaxKind.IdentifierName)
                && Ancestors.HasAncestorAt(1, SyntaxKind.FromClause)
                && PrevToken?.Text == "in";
        }

        public bool IsJoinRangeVariable()
        {
            //      ⌄
            // join l in labels on n.Id equals l.Id
            return Ancestors.HasAncestorAt(0, SyntaxKind.JoinClause)
                && Ancestors.HasAncestorAt(1, SyntaxKind.QueryBody)
                && PrevToken?.Text == "join";
        }

        public bool IsJoinSource()
        {
            //             ⌄
            // join l in labels on n.Id equals l.Id
            return Ancestors.HasAncestorAt(0, SyntaxKind.IdentifierName)
                && Ancestors.HasAncestorAt(1, SyntaxKind.JoinClause)
                && PrevToken?.Text == "in";
        }

        public bool IsLetVariable()
        {
            //        ⌄
            // let doubled = n.Value * 2
            return Ancestors.HasAncestorAt(0, SyntaxKind.LetClause)
                && Ancestors.HasAncestorAt(1, SyntaxKind.QueryBody)
                && PrevToken?.Text == "let";
        }

        public bool IsGroupContinuationRangeVariable()
        {
            //                                             ⌄
            // group new { n, l, doubled } by n.Value into g
            return Ancestors.HasAncestorAt(0, SyntaxKind.QueryContinuation)
                && Ancestors.HasAncestorAt(1, SyntaxKind.QueryBody)
                && PrevToken?.Text == "into";
        }

        public bool IsGroupElement()
        {
            //       ⌄
            // group n by n.Category;
            return Kind == SyntaxKind.IdentifierToken
                && Ancestors.HasAncestorAt(1, SyntaxKind.GroupClause)
                && PrevToken?.Text == "group";
        }

        public bool IsJoinIntoRangeVariable()
        {
            //                                              ⌄
            // join l in labels on n.Id equals l.Id into matches
            return Ancestors.HasAncestorAt(0, SyntaxKind.JoinIntoClause)
                && Ancestors.HasAncestorAt(1, SyntaxKind.JoinClause)
                && PrevToken?.Text == "into";
        }
        #endregion

        #region ------------------- Key Checks --------------------
        public bool IsLambdaParameterReference()
        {
            // must have lambda ancestor
            if (!Ancestors.HasAncestor(SyntaxKind.SimpleLambdaExpression))
                return false;

            var prevToken = PrevToken;
            bool isLambdaParamRef = false;

            while (prevToken is not null)
            {
                bool isLambdaParamDecl = prevToken.SemanticRole == SemanticRole.LambdaParameter;
                bool textMatches = prevToken.Text == Text;

                if (isLambdaParamDecl && textMatches)
                    return true;

                // Semicolons signals the end of the prev statement which is outside the scope
                // of the current lambda expression. End search here.
                if (prevToken.Text == ";")
                    return false;

                prevToken = prevToken.PrevToken;
            }

            return isLambdaParamRef;
        }
        #endregion

        #region ------------------- Focused Label Checks --------------------
        public bool TryGetDeclarationFocusedLabel(out string label)
        {
            label = null;

            bool isDeclaration = GroupRole
                is GroupRole.LocalDeclaration
                or GroupRole.MemberDeclaration
                or GroupRole.ParameterDeclaration
                or GroupRole.TypeDeclaration;

            if (!isDeclaration)
            {
                return false;
            }

            label = SemanticRole
                .ToSpacedString()
                .Replace("Declaration", string.Empty)
                .Trim();

            return true;
        }

        public bool TryGetInvocationFocusedLabel(out string label)
        {
            label = null;

            if (GroupRole is not GroupRole.Invocation)
                return false;

            label = SemanticRole.ToSpacedString();

            return true;
        }

        public bool TryGetParamLabelFocusedLabel(out string label)
        {
            label = null;

            if (SemanticRole is not SemanticRole.ParameterLabel)
                return false;

            label = SemanticRole.ToSpacedString();

            return true;
        }

        public bool TryGetReferenceFocusedLabel(out string label)
        {
            label = null;

            bool isDeclarationRole = DeclarationRoles.Contains(SemanticRole);
            bool isDefinedInFile = Classifications.Corrected
                is "constant name"
                or "event name"
                or "event field name"
                or "field name"
                or "local name"
                or "parameter name"
                or "property name";

            if (isDeclarationRole || !isDefinedInFile)
                return false;

            label = Classifications.Corrected switch
            {
                "constant name" => "Constant Reference",
                "event name" => "Event Reference",
                "event field name" => "Event Field Reference",
                "field name" => "Field Reference",
                "local name" => IsOutVariableDeclaration()
                                    ? "Out Variable Reference"
                                    : "Local Variable Reference",
                "parameter name" => IsLambdaParameterReference()
                                        ? "Lambda Parameter Reference"
                                        : "Parameter Reference",
                "property name" => "Property Reference",
                _ => string.Empty,
            };

            return true;
        }
        #endregion

        #region ------------------- Highlighting Checks --------------------
        public bool IsAttributePropertyAssignmentIdentifier()
        {
            return Kind == SyntaxKind.IdentifierToken
                && Ancestors.HasAncestorAt(1, SyntaxKind.NameEquals)
                && Ancestors.HasAncestorAt(2, SyntaxKind.AttributeArgument);
        }

        public bool IsPropertyAssignmentIdentifier()
        {
            return Kind == SyntaxKind.IdentifierToken
                && Ancestors.HasAncestorAt(2, SyntaxKind.ObjectInitializerExpression);
        }

        // TODO: factor this out - copied from role
        public bool IsUsingDirectiveNamespace()
        {
            return IsIdentifiableNamespace() || IsUnIdentifiableNamespace();

            /// using IO = System.IO;
            bool IsIdentifiableNamespace()
            {
                bool validClass = Classifications.Corrected is NamespaceName or Identifier;
                bool validSemanticData = SemanticData?.SymbolKind == SymbolKind.Namespace;
                bool validAncestor = Ancestors.GetLastAncestor() == SyntaxKind.UsingDirective;

                return IsIdentifier()
                    && validClass
                    && validSemanticData
                    && validAncestor;
            }

            /// using MyToken = csharp_cartographer_backend._03.Models.Tokens;
            bool IsUnIdentifiableNamespace()
            {
                bool validSymbol = SemanticData?.IsTypeSymbol != true;
                bool validAncestor = Ancestors.HasAncestor(SyntaxKind.QualifiedName)
                    && Ancestors.GetLastAncestor() == SyntaxKind.UsingDirective;

                return IsIdentifier()
                    && validSymbol
                    && validAncestor;
            }
        }

        // TODO: factor this out - copied from role
        public bool IsNamespaceDeclaration()
        {
            // check ancestors minus "QualifiedName" since namespaces
            // vary in length of qualifying segments
            var expectedAncestors = ImmutableArray.Create(
                SyntaxKind.IdentifierName,
                SyntaxKind.NamespaceDeclaration);

            bool validKind = Kind == SyntaxKind.IdentifierToken;
            bool validClassification = Classifications.Corrected == NamespaceName;
            bool validAncestors = Ancestors.Ancestors
                .Where(kind => kind != SyntaxKind.QualifiedName)
                .Distinct()
                .ToHashSet()
                .SetEquals(expectedAncestors);

            return validKind && validClassification && validAncestors;
        }
        #endregion
    }
}
