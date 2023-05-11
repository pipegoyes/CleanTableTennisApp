namespace CleanTableTennisApp.Application.Common.Enconders;

public interface IUrlSafeIntEncoder
{
    string Encode(int num);
    int Decode(string encodedString);
}

public class UrlSafeIntEncoder : IUrlSafeIntEncoder
{
    static readonly char[] Padding = { '=' };

    public string Encode(int num)
    {
        byte[] numBytes = BitConverter.GetBytes(num);
        return Convert.ToBase64String(numBytes)
            .TrimEnd(Padding).Replace('+', '-').Replace('/', '_');
    }
    
    public int Decode(string encodedString)
    {
        string incoming = encodedString
            .Replace('_', '/').Replace('-', '+');
        switch(encodedString.Length % 4) {
            case 2: incoming += "=="; break;
            case 3: incoming += "="; break;
        }
        byte[] bytes = Convert.FromBase64String(incoming);
        return BitConverter.ToInt32(bytes);
    }
}