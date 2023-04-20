namespace D_Channels;

public class Name
{
    public string title { get; set; }
    public string first { get; set; }
    public string last { get; set; }
}

public class Result
{
    public Name name { get; set; }
}

public class RootObject
{
    public List<Result> results { get; set; }
}