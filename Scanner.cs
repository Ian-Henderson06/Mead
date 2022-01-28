using System;
using System.Collections.Generic;
using System.Text;

namespace Mead
{

    class Scanner
    {
        readonly Dictionary<string, TokenType> keywords = new Dictionary<string, TokenType>
        {
            {"def", TokenType.KW_DEF},
            {"set", TokenType.KW_SET},
            {"print", TokenType.KW_PRINT}
        };



        string source;
        List<Token> tokens = new List<Token>();
        int start = 0;
        int current = 0;
        int line = 1;

        public Scanner(string source)
        {
            this.source = source;
        }

        /// <summary>
        /// Scan all tokens from source.
        /// </summary>
        /// <returns></returns>
        public List<Token> ScanTokens()
        {
            while (!isAtEnd())
            {
                start = current;
                scanToken();
            }

            tokens.Add(new Token(TokenType.EOF, "", null, line));
            return tokens;
        }

        /// <summary>
        /// Called when an individual token is scanned.
        /// </summary>
        void scanToken()
        {
            char c = advance();
            switch (c)
            {
                case '(': addToken(TokenType.LEFT_PAREN); break;
                case ')': addToken(TokenType.RIGHT_PAREN); break;
                case '+': addToken(TokenType.PLUS); break;
                case '-': addToken(TokenType.MINUS); break;
                case '*': addToken(TokenType.STAR); break;
                case '=':
                    if (match('='))
                        addToken(TokenType.EQUAL_EQUAL);
                    break;
                case '<':
                    addToken(match('=') ? TokenType.LESSTHAN_EQUAL : TokenType.LESSTHAN);
                    break;
                case '>':
                    addToken(match('=') ? TokenType.MORETHAN_EQUAL : TokenType.MORETHAN);
                    break;

                case '/':
                    if (match('/'))
                    {
                        // A comment goes until the end of the line.
                        while (peek() != '\n' && !isAtEnd()) advance();
                    }
                    else
                    {
                        addToken(TokenType.SLASH);
                    }
                    break;

                case '"': handleString(); break;
                case ' ':
                case '\r':
                case '\t':
                    // Ignore whitespace.
                    break;
                case '\n':
                    line++;
                    break;
                default:
                    if (isDigit(c))
                        handleNumber();
                    else if (isAlpha(c))
                        handleIdentifier();
                    else
                        Mead.Error(line, "Unexpected character " + c + " .");
                    break;
            }
        }

        
        private void handleString() {
            while (peek() != '"' && !isAtEnd()) {
             if (peek() == '\n') line++;
             advance();
            }

            if (isAtEnd()) {
                Mead.Error(line, "Unterminated string.");
                return;
            }

            // The closing ".
            advance();

            // Trim the surrounding quotes.
            string value = source.SubstringWithEnd(start + 1, current - 1);
            addToken(TokenType.STRING, value);
        }

        private void handleNumber()
        {
            while (isDigit(peek())) advance();

            // Look for a fractional part.
            if (peek() == '.' && isDigit(peekNext()))
            {
                // Consume the "."
                advance();

                while (isDigit(peek())) advance();
            }

            addToken(TokenType.NUMBER,
                Double.Parse(source.SubstringWithEnd(start, current)));
        }

        private void handleIdentifier()
        {
            while (isAlphaNumeric(peek())) advance();

            string text = source.SubstringWithEnd(start, current);
            TokenType type = keywords[text];
            if (type == null) type = TokenType.IDENTIFIER;
            addToken(type);
        }

        private bool isAlpha(char c)
        {
            return (c >= 'a' && c <= 'z') ||
                   (c >= 'A' && c <= 'Z') ||
                    c == '_';
        }

        private bool isAlphaNumeric(char c)
        {
            return isAlpha(c) || isDigit(c);
        }

        /// <summary>
        /// Checks if specified character is a digit.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        bool isDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        /// <summary>
        /// Checks if we are at the end of the file.
        /// </summary>
        /// <returns></returns>
        bool isAtEnd()
        {
            return current >= source.Length;
        }

        /// <summary>
        /// Consumes the next character in the source file.
        /// </summary>
        /// <returns></returns>
        char advance()
        {
            return source[current++];
        }

        /// <summary>
        /// Consumes the next character in the source file if it matches parameter.
        /// </summary>
        /// <param name="expected"></param>
        /// <returns></returns>
        bool match(char expected)
        {
            if (isAtEnd()) return false;
            if (source[current] != expected) return false;

            current++;
            return true;
        }

        /// <summary>
        /// Looks at next character without consumption.
        /// </summary>
        /// <returns></returns>
        char peek()
        {
            if (isAtEnd()) return '\0';
            return source[current];
        }

        private char peekNext()
        {
            if (current + 1 >= source.Length) return '\0';
            return source[current + 1];
        }

        void addToken(TokenType type)
        {
            addToken(type, null);
        }

        void addToken(TokenType type, Object literal)
        {
            string text = source.SubstringWithEnd(start, current);
            tokens.Add(new Token(type, text, literal, line));
        }
    }
}
