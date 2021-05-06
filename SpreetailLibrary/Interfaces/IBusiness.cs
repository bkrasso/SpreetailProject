using System.Collections.Generic;

namespace BusinessLibrary.Interfaces
{
    public interface IBusiness
    {
        Models.Result AddKey(string key, string member);
        Models.MembersResult AllMembers();
        Models.Result Clear();
        List<string> GetKeys();
        Models.MembersResult GetMembers(string key);
        Models.MembersResult Items();
        Models.Result KeyExists(string key);
        Models.Result Remove(string key, string member);
        Models.Result Remove(string key);
        Models.Result ValueExists(string key, string value);
    }
}
