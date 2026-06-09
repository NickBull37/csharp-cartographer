using csharp_cartographer_backend._01.Configuration.Enums;
using csharp_cartographer_backend._03.Models.Tokens;

namespace csharp_cartographer_backend._05.Services.Keys
{
    public static partial class KeyMaker
    {
        /*
         * DEFAULT KEY
         *  
         *    ID:{token.SemanticRole}
         *    
         * SPECIAL KEYS
         * 
         *    ID:[reference string]
         *    ID:[generic string]
         *    
         *  Identifiers don't have specific definitions like
         *  keywords or operators. Add an extension for all
         *  identifiers defined in the uploaded file to add
         *  a little more information when possible.
         */

        private static List<SemanticRole> DeclarationRoles =
        [
            SemanticRole.FieldDeclaration,
            SemanticRole.LambdaParameter,
            SemanticRole.LocalVariableDeclaration,
            SemanticRole.LoopIteratorDeclaration,
            SemanticRole.Parameter
        ];

        private static string? GetIdentifierKey(NavToken token)
        {
            if (token.IsGenericType())
                return Key(ID, "GenericType");

            if (token.IsUsingResourceDeclaration())
                return Key(ID, "UsingResourceDeclaration");

            if (ShouldUseReferenceExtension(token))
                return GetIdentifierReferenceKey(token);

            return Key(ID, token.SemanticRole.ToString());
        }

        private static bool ShouldUseReferenceExtension(NavToken token)
        {
            bool isDeclarationRole = DeclarationRoles.Contains(token.SemanticRole);
            bool isDefinedInFile = token.Classifications.Corrected
                is "event name"
                or "event field name"
                or "field name"
                or "local name"
                or "parameter name"
                or "property name";

            return !isDeclarationRole && isDefinedInFile;
        }

        private static string? GetIdentifierReferenceKey(NavToken token)
        {
            var extension = token.Classifications.Corrected switch
            {
                "event name" => null,
                "event field name" => null,
                "field name" => "FieldReference",
                "local name" => token.IsOutVariableDeclaration()
                                    ? "OutVariableReference"
                                    : "LocalVariableReference",
                "parameter name" => token.IsLambdaParameterReference()
                                        ? "LambdaParameterReference"
                                        : "ParameterReference",
                "property name" => "PropertyReference",
                _ => null,
            };

            if (extension is null)
            {
                // log error
                return null;
            }

            return Key(ID, extension);
        }
    }
}
