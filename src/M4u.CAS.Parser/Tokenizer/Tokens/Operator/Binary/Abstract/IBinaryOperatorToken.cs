//namespace M4u.CAS.Parser;

//internal abstract class IBinaryOperatorToken : IToken
//{
//    public override TokenKind Type => TokenKind.BINARY_OPERATOR;

//    /// <summary>логическая переменная, принимающая значение true тогда и только тогда,
//    /// когда оператор правоассоциативен (т.е. если какой-либо оператор является и лево- и 
//    /// правоассоциативным одновременно (обладает свойством ассоциативности, т.е. 
//    /// (x op y) op z == x op (y op z)), то эта переменная принимает значение false). 
//    /// Свойство ассоциативности имеет отношение только к бинарным операторам</summary>
//    public virtual bool IsRightAssociativeOnly => false;

//    public IBinaryOperatorToken(string value) : base(value) { }
//}
