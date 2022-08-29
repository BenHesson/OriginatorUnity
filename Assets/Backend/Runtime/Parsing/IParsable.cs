namespace OriginatorKids.Parser
{
    //Hopefully this doesn't seem too weird. Since this marker is used at compile time
    //for identification, docs say it's an acceptable use.
    //https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-studio-2010/ms182128(v=vs.100)
    //I could see an argument against it, but I think at that point it's just a matter of personal preference.

    /// <summary>
    /// Acts as a marker interface for all types that can be parsed
    /// </summary>
    public interface IParsable { }
}