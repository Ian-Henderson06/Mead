using System.Collections.Generic;

namespace Mead
{
    class Parser
    {
        private List<Token> tokens;
        private int token_index = 0;
        
        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        Token advance()
        {
            token_index++;
            Token currentToken = null;

            if (token_index < tokens.Count)
            {
                currentToken = tokens[token_index];
            }

            return currentToken;
        }
    }
}