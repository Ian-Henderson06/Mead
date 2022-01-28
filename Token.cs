using System;
using System.Collections.Generic;
using System.Text;

namespace Mead
{
    
    enum TokenType
    {
        //Single Char Tokens
        PLUS,
        MINUS,
        STAR,
        SLASH,
        LESSTHAN,
        MORETHAN,
        LEFT_PAREN,
        RIGHT_PAREN,

        //Double Char Tokens
        LESSTHAN_EQUAL,
        MORETHAN_EQUAL,
        EQUAL_EQUAL,

        //Literals
        IDENTIFIER, STRING, NUMBER,

        //Keywords
        KW_DEF, KW_SET, KW_PRINT,

        EOF
    }

    class Token
    {
        TokenType type;
        string lexeme;
        object literal;
        int line;

        public Token(TokenType type, string lexeme, object literal, int line)
        {
            this.type = type;
            this.lexeme = lexeme;
            this.literal = literal;
            this.line = line;
        }

        public override string ToString()
        {
            return type + " " + lexeme + " " + literal;
        }
    }
}
