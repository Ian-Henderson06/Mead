namespace Mead.AST
{
    internal class NumberNode : INode
    {
        private Token token;

        public NumberNode(Token token)
        {
            this.token = token;
        }
    }

    internal class BinOpNode : INode
    {
        private INode leftNode;
        private INode rightNode;
        private Token op;

        public BinOpNode(INode leftNode, Token op, INode rightNode)
        {
            this.leftNode = leftNode;
            this.op = op;
            this.rightNode = rightNode;
        }
    }
}