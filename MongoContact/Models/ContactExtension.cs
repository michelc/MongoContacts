using System;

namespace MongoContact.Models
{
    public static class ContactExtension
    {
        public static string IdString(this Contact c)
        {
            var result = BitConverter.ToString(c.Id.Value);
            result = result.Replace("-", string.Empty).ToLower();
            return result;
        }
    }
}
