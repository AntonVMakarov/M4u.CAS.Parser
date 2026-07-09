namespace M4u.CAS.Parser;

/// <summary>
/// Фабрика для создания токенизатора.
/// </summary>
internal interface ITokenizerFactory
{
    ITokenizer Create();
}
