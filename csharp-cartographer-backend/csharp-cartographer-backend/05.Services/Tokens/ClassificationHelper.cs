using csharp_cartographer_backend._03.Models.Tokens;
using Microsoft.CodeAnalysis.CSharp;

namespace csharp_cartographer_backend._05.Services.Tokens
{
    public static class ClassificationHelper
    {
        const int Parent = 0;
        const int GrandParent = 1;
        const int GreatGrandParent = 2;
        const int GreatGreatGrandParent = 3;

        // these declaration kinds will always appear at the "parent" level
        public static bool IsDeclarationIdentifierForEntity(SyntaxKind entityKind, NavToken token)
        {
            var parentKind = token.AncestorKinds.Parent;
            var grandParentKind = token.AncestorKinds.GrandParent;
            var greatGrandParentKind = token.AncestorKinds.GreatGrandParent;

            List<SyntaxKind> parentLevelKinds =
            [
                SyntaxKind.ClassDeclaration,
                SyntaxKind.InterfaceDeclaration,
                SyntaxKind.MethodDeclaration,
                SyntaxKind.PropertyDeclaration,
                SyntaxKind.RecordDeclaration,
                SyntaxKind.RecordStructDeclaration,
                SyntaxKind.StructDeclaration,
            ];

            if (parentLevelKinds.Contains(parentKind) && entityKind == parentKind)
            {
                return true;
            }

            List<SyntaxKind> grandParentLevelKinds =
            [
                SyntaxKind.VariableDeclaration,
            ];

            if (grandParentLevelKinds.Contains(grandParentKind) && entityKind == grandParentKind)
            {
                return true;
            }

            List<SyntaxKind> greatGrandParentLevelKinds =
            [
                SyntaxKind.FieldDeclaration,
                SyntaxKind.LocalDeclarationStatement
            ];

            if (greatGrandParentLevelKinds.Contains(greatGrandParentKind) && entityKind == greatGrandParentKind)
            {
                return true;
            }

            return false;
        }

        #region Fields
        public static bool IsFieldDeclarationIdentifier(NavToken token)
        {
            if (token.AncestorKinds.Ancestors[GreatGrandParent] == SyntaxKind.FieldDeclaration)
            {
                return true;
            }
            return false;
        }

        public static bool IsFieldDataTypeIdentifier(NavToken token)
        {
            // field declaration kind will be 3rd level if non-nullable, 4th level if nullable
            if (token.AncestorKinds.Ancestors[GreatGrandParent] == SyntaxKind.FieldDeclaration ||
                token.AncestorKinds.Ancestors[GreatGreatGrandParent] == SyntaxKind.FieldDeclaration)
            {
                return true;
            }
            return false;
        }
        #endregion

        public static bool IsGenericDataTypeIdentifier(NavToken token)
        {
            if (token.AncestorKinds.Ancestors[Parent] == SyntaxKind.GenericName)
            {
                return true;
            }
            return false;
        }

        public static bool IsNullableDataTypeIdentifier(NavToken token)
        {
            if (token.AncestorKinds.Ancestors[GrandParent] == SyntaxKind.NullableType)
            {
                return true;
            }
            return false;
        }

        // determine if it is a field declaration, local var declaration, etc.
        public static SyntaxKind GetDeclarationEntity(NavToken token)
        {
            SyntaxKind[] declarationKinds =
            [
                // 3rd level
                SyntaxKind.ClassDeclaration,
                SyntaxKind.InterfaceDeclaration,
                SyntaxKind.MethodDeclaration,
                SyntaxKind.NamespaceDeclaration,
                SyntaxKind.PropertyDeclaration,
                SyntaxKind.RecordDeclaration,
                SyntaxKind.RecordStructDeclaration,
                SyntaxKind.StructDeclaration,
                // 4th level
                SyntaxKind.FieldDeclaration,
                SyntaxKind.LocalDeclarationStatement,
            ];

            var grandParentKind = token.AncestorKinds.Ancestors[GrandParent];
            if (declarationKinds.Contains(grandParentKind))
            {
                return grandParentKind;
            }

            var greatGrandParentKind = token.AncestorKinds.Ancestors[GreatGrandParent];
            if (declarationKinds.Contains(greatGrandParentKind))
            {
                return greatGrandParentKind;
            }

            throw new Exception();
        }
    }
}
