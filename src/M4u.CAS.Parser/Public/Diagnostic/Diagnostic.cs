namespace M4u.CAS.Parser;

public sealed record Diagnostic
{
    public DiagnosticCode Code { get; }
    public TextSpan Span { get; }
    public string Message {  get; }


    public Diagnostic(DiagnosticCode code, TextSpan span, string message)
    {
        Code = code;
        Span = span;
        Message = message;
    }
}
