using System.Collections.Generic;
using System.Linq;

namespace BusinessLibrary
{
    public class Business : Interfaces.IBusiness
    {
        public Dictionary<string, List<string>> _items = new Dictionary<string, List<string>>();

        /// <summary>
        /// Will add a new Key
        /// </summary>
        /// <param name="key">Name of Key being added</param>
        /// <param name="member">Value being added to the Key</param>
        /// <returns>Result object with status of 201 if Key and Value Added Successfully, 500 for any other error</returns>
        public Models.Result AddKey(string key, string member)
        {
            if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(member))
                return new Models.Result
                {
                    StatusCode = 500,
                    Message = "Invalid Arguments"
                };

            Models.Result result = new Models.Result
            {
                StatusCode = 201,
                Message = "Value Added"
            };

            if (_items.ContainsKey(key))
            {
                var item = _items[key];
                if (item.Contains(member))

                    return new Models.Result
                    {
                        StatusCode = 500,
                        Message = $"Value Already Exists for Key: {key}"
                    };

                item.Add(member);
            }
            else
            {
                _items.Add(key, new List<string> { member });
            }

            return result;
        }

        /// <summary>
        /// Will retrieve all Members for all Keys
        /// </summary>
        /// <returns>MemberResult with all of the Members of every key, status of 200 if successfull, 500 for any other error</returns>
        public Models.MembersResult AllMembers()
        {
            Models.MembersResult membersResult = new Models.MembersResult
            {
                StatusCode = 200,
                Members = new List<string>()
            };

            foreach (var item in _items)
            {
                foreach (var value in item.Value)
                {
                    membersResult.Members.Add(value);
                }
            }

            return membersResult;
        }

        /// <summary>
        /// Will remove all Keys and Members from the system
        /// </summary>
        /// <returns>Result object with status of 200 if successfull, 500 for any other error</returns>
        public Models.Result Clear()
        {
            _items = new Dictionary<string, List<string>>();
            return new Models.Result
            {
                StatusCode = 200,
                Message = "All keys and all members have been removed"
            };
        }

        /// <summary>
        /// Will retrieve all of the keys in the system
        /// </summary>
        /// <returns>List<string> of all keys in the system</string></returns>
        public List<string> GetKeys()
        {
            List<string> keys = new List<string>();
            foreach(var item in _items)
            {
                keys.Add(item.Key);
            }

            return keys;
        }

        /// <summary>
        /// Will retreive all the Members for a specific key in the system
        /// </summary>
        /// <param name="key">Key for the Members that are being retrieved</param>
        /// <returns>MemberResult with all of the Members of a specific key, status of 200 if successfull, 500 for any other error</returns>
        public Models.MembersResult GetMembers(string key)
        {

            if (string.IsNullOrWhiteSpace(key))
                return new Models.MembersResult
                {
                    StatusCode = 500,
                    Message = "Invalid Argument"
                };

            if (!_items.ContainsKey(key))
                return new Models.MembersResult
                {
                    StatusCode = 500,
                    Message = "Key doesnt exist"
                };

            Models.MembersResult membersResult = new Models.MembersResult
            {
                StatusCode = 200,
                Members = new List<string>()
            };

            foreach(var member in _items[key])
            {
                membersResult.Members.Add(member);
            }

            return membersResult;
        }

        /// <summary>
        /// Will retrieve all Keys and the Members for each of those keys
        /// </summary>
        /// <returns>MemberResult with all of the keys and members for each of those keys, status of 200 if successfull, 500 for any other error</returns>
        public Models.MembersResult Items()
        {
            Models.MembersResult membersResult = new Models.MembersResult
            {
                StatusCode = 200,
                Members = new List<string>()
            };

            int count = 1;

            foreach (var item in _items)
            {
                foreach (var value in item.Value)
                {
                    membersResult.Members.Add($"{ count}) {item.Key}: {value}");
                    count++;
                }
            }

            return membersResult;
        }

        /// <summary>
        /// Will determine if a specific key exists
        /// </summary>
        /// <param name="key">Key that is being checked to see if it exists</param>
        /// <returns>True if Key exists, False if it doesnt, status of 200 if successfull, 500 for any other error</returns>
        public Models.Result KeyExists(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return new Models.Result
                {
                    StatusCode = 500,
                    Message = "Invalid Argument"
                };

            return new Models.Result
            {
                StatusCode = 200,
                Message = _items.ContainsKey(key).ToString()
            };
        }

        /// <summary>
        /// Will remove a specific value from a specific key
        /// </summary>
        /// <param name="key">Name of Key for the Value that is being removed</param>
        /// <param name="value">Value that is being removed</param>
        /// <returns>status of 200 if successfull, 500 for any other error</returns>
        public Models.Result Remove(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(value))
                return new Models.Result
                {
                    StatusCode = 500,
                    Message = "Invalid Arguments"
                };

            if (!_items.ContainsKey(key))
                return new Models.Result
                {
                    StatusCode = 500,
                    Message = "Key doesnt exist"
                };

            var values = _items[key];
            bool exists = values.Remove(value);
            if (!exists)
                return new Models.Result
                {
                    StatusCode = 500,
                    Message = $"Value:{value} doesnt exist in Key:{key}"
                };

            if (!values.Any())
                _items.Remove(key);

            return new Models.Result
            {
                StatusCode = 200,
                Message = $"Value:{value} removed from Key:{key}"
            };
        }

        /// <summary>
        /// Will remove all Members for a specific Key
        /// </summary>
        /// <param name="key">The name of the key whose Members are being removed</param>
        /// <returns></returns>
        public Models.Result Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return new Models.Result
                {
                    StatusCode = 500,
                    Message = "Invalid Arguments"
                };

            if (!_items.ContainsKey(key))
                return new Models.Result
                {
                    StatusCode = 500,
                    Message = "Key doesnt exist"
                };

            _items.Remove(key);

            return new Models.Result
            {
                StatusCode = 200,
                Message = $"All Members removed from Key:{key}"
            };
        }

        /// <summary>
        /// Will determine if a specific Value exists in a specific key
        /// </summary>
        /// <param name="key">Name of the key for the Value that is being determined if it exists</param>
        /// <param name="value">Value that is being determined if it exists</param>
        /// <returns>True if Value exists for specific Key, False if it doesnt, status of 200 if successfull, 500 for any other error</returns>
        public Models.Result ValueExists(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(value))
                return new Models.Result
                {
                    StatusCode = 500,
                    Message = "Invalid Arguments"
                };

            if (!_items.ContainsKey(key))
                return new Models.Result
                {
                    StatusCode = 500,
                    Message = "False"
                };

            return new Models.Result
            {
                StatusCode = 200,
                Message = _items[key].Contains(value).ToString()
            };
        }
    }
}