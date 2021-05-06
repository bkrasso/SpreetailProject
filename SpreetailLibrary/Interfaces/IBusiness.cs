using System.Collections.Generic;

namespace BusinessLibrary.Interfaces
{
    public interface IBusiness
    {
        /// <summary>
        /// Will add a new Key
        /// </summary>
        /// <param name="key">Name of Key being added</param>
        /// <param name="member">Value being added to the Key</param>
        /// <returns>Result object with status of 201 if Key and Value Added Successfully, 500 for any other error</returns>
        Models.Result AddKey(string key, string member);
        /// <summary>
        /// Will retrieve all Members for all Keys
        /// </summary>
        /// <returns>MemberResult with all of the Members of every key, status of 200 if successfull, 500 for any other error</returns>
        Models.MembersResult AllMembers();
        /// <summary>
        /// Will remove all Keys and Members from the system
        /// </summary>
        /// <returns>Result object with status of 200 if successfull, 500 for any other error</returns>
        Models.Result Clear();
        /// <summary>
        /// Will retrieve all of the keys in the system
        /// </summary>
        /// <returns>List<string> of all keys in the system</string></returns>
        List<string> GetKeys();
        /// <summary>
        /// Will retreive all the Members for a specific key in the system
        /// </summary>
        /// <param name="key">Key for the Members that are being retrieved</param>
        /// <returns>MemberResult with all of the Members of a specific key, status of 200 if successfull, 500 for any other error</returns>
        Models.MembersResult GetMembers(string key);
        /// <summary>
        /// Will retrieve all Keys and the Members for each of those keys
        /// </summary>
        /// <returns>MemberResult with all of the keys and members for each of those keys, status of 200 if successfull, 500 for any other error</returns>
        Models.MembersResult Items();
        /// <summary>
        /// Will determine if a specific key exists
        /// </summary>
        /// <param name="key">Key that is being checked to see if it exists</param>
        /// <returns>True if Key exists, False if it doesnt, status of 200 if successfull, 500 for any other error</returns>
        Models.Result KeyExists(string key);
        /// <summary>
        /// Will remove a specific value from a specific key
        /// </summary>
        /// <param name="key">Name of Key for the Value that is being removed</param>
        /// <param name="value">Value that is being removed</param>
        /// <returns>status of 200 if successfull, 500 for any other error</returns>
        Models.Result Remove(string key, string member);
        /// <summary>
        /// Will remove all Members for a specific Key
        /// </summary>
        /// <param name="key">The name of the key whose Members are being removed</param>
        Models.Result Remove(string key);
        /// <summary>
        /// Will determine if a specific Value exists in a specific key
        /// </summary>
        /// <param name="key">Name of the key for the Value that is being determined if it exists</param>
        /// <param name="value">Value that is being determined if it exists</param>
        /// <returns>True if Value exists for specific Key, False if it doesnt, status of 200 if successfull, 500 for any other error</returns>
        Models.Result ValueExists(string key, string value);
    }
}
