using csharp_cartographer_backend._03.Models.Tokens;
using Microsoft.CodeAnalysis.CSharp;

namespace csharp_cartographer_backend._05.Services.Tokens
{
    public static class ClassificationHelper
    {
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
            if (token.AncestorKinds.GreatGrandParent == SyntaxKind.FieldDeclaration)
            {
                return true;
            }

            return false;
        }

        public static bool IsFieldReferenceIdentifier(NavToken token)
        {
            if (token.RoslynClassification == "field name" &&
                token.AncestorKinds.Parent == SyntaxKind.IdentifierName)
            {
                return true;
            }

            return false;
        }

        public static bool IsFieldDataTypeIdentifier(NavToken token)
        {
            var greatGrandParentKind = token.AncestorKinds.GreatGrandParent;
            var greatGreatGrandParentKind = token.AncestorKinds.GreatGreatGrandParent;

            // field declaration kind will be 3rd level if non-nullable, 4th level if nullable
            if (greatGrandParentKind == SyntaxKind.FieldDeclaration ||
                greatGreatGrandParentKind == SyntaxKind.FieldDeclaration)
            {
                return true;
            }

            return false;
        }
        #endregion

        public static bool IsGenericDataTypeIdentifier(NavToken token)
        {
            if (token.AncestorKinds.Parent == SyntaxKind.GenericName)
            {
                return true;
            }
            return false;
        }

        public static bool IsNullableDataTypeIdentifier(NavToken token)
        {
            if (token.AncestorKinds.GrandParent == SyntaxKind.NullableType)
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
                SyntaxKind.ClassDeclaration,
                SyntaxKind.FieldDeclaration,
                SyntaxKind.InterfaceDeclaration,
                SyntaxKind.MethodDeclaration,
                SyntaxKind.NamespaceDeclaration,
                SyntaxKind.PropertyDeclaration,
                SyntaxKind.RecordDeclaration,
                SyntaxKind.RecordStructDeclaration,
                SyntaxKind.StructDeclaration,
                SyntaxKind.VariableDeclaration,
            ];

            var grandParentNodeKind = token.AncestorKinds.GrandParent;
            var greatGrandParentNodeKind = token.AncestorKinds.GreatGrandParent;

            // Prefer the closest ancestor (grandparent)
            if (declarationKinds.Contains(grandParentNodeKind))
            {
                return grandParentNodeKind;
            }

            if (declarationKinds.Contains(greatGrandParentNodeKind))
            {
                return greatGrandParentNodeKind;
            }

            return SyntaxKind.None;
        }
    }
}
