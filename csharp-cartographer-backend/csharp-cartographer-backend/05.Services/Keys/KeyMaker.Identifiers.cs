using csharp_cartographer_backend._03.Models.Tokens;
using csharp_cartographer_backend._03.Models.Tokens.TokenMaps;

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
            SemanticRole.Parameter
        ];

        private static DefinitionKey? GetIdentifierKey(NavToken token)
        {
            if (token.IsGenericType())
                return new DefinitionKey(IdentifierKind, "GenericType", []);

            var key = GetIdentifierReferenceKey(token);
            if (key is not null)
                return key;

            return new DefinitionKey(IdentifierKind, token.SemanticRole.ToString(), []);
        }

        private static DefinitionKey? GetIdentifierReferenceKey(NavToken token)
        {
            bool isDeclarationRole = DeclarationRoles.Contains(token.SemanticRole);
            bool isPotentialReferenceRole = token.Classifications.Final
                is "field name"
                or "event name"
                or "event field name"
                or "local name"
                or "parameter name"
                or "property name";

            if (isDeclarationRole || !isPotentialReferenceRole)
            {
                return null;
            }

            return token.Classifications.Final switch
            {
                "event name" => null,
                "event field name" => null,
                "field name" => new DefinitionKey(IdentifierKind, "FieldReference", []),
                "local name" => new DefinitionKey(IdentifierKind, "LocalVariableReference", []),
                "parameter name" => token.IsLambdaParameterReference()
                                        ? new DefinitionKey(IdentifierKind, "LambdaParameterReference", [])
                                        : new DefinitionKey(IdentifierKind, "ParameterReference", []),
                "property name" => new DefinitionKey(IdentifierKind, "PropertyReference", []),
                _ => null,
            };
        }
    }
}
