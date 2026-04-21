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

            if (!DeclarationRoles.Contains(token.SemanticRole))
            {
                if (token.Classifications.Final == "parameter name")
                {
                    return token.IsLambdaParameterReference()
                        ? new DefinitionKey(IdentifierKind, "LambdaParameterReference", [])
                        : new DefinitionKey(IdentifierKind, "ParameterReference", []);
                }

                if (token.Classifications.Final == "local name")
                    return new DefinitionKey(IdentifierKind, "LocalVariableReference", []);

                if (token.Classifications.Final == "field name")
                    return new DefinitionKey(IdentifierKind, "FieldReference", []);

                if (token.Classifications.Final == "property name")
                    return new DefinitionKey(IdentifierKind, "PropertyReference", []);
            }

            return new DefinitionKey(IdentifierKind, token.SemanticRole.ToString(), []);
        }
    }
}
