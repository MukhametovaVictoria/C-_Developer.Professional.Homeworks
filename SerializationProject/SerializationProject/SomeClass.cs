using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace SerializationProject
{
    [Serializable]
    class SomeClass : ISerializable
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid AuthorId { get; set; }
        public int Likes { get; set; }

        public SomeClass()
        { }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        private SomeClass(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            Id = new Guid(info.GetString("Id"));
            Title = info.GetString("Title");
            Content = info.GetString("Content");
            ShortDescription = info.GetString("ShortDescription");
            CreatedAt = info.GetDateTime("CreatedAt");
            UpdatedAt = info.GetDateTime("UpdatedAt");
            AuthorId = new Guid(info.GetString("AuthorId"));
            Likes = info.GetInt32("Likes");
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");
            
            info.AddValue("Id", Id);
            info.AddValue("Title", Title);
            info.AddValue("Content", Content);
            info.AddValue("ShortDescription", ShortDescription);
            info.AddValue("CreatedAt", CreatedAt);
            info.AddValue("UpdatedAt", UpdatedAt);
            info.AddValue("AuthorId", AuthorId);
            info.AddValue("Likes", Likes);
        }

        public void ShowItems()
        {
            Console.WriteLine($"Id={Id.ToString()}, " +
                $"Title={Title}, Content={Content}, " +
                $"ShortDescription={ShortDescription}, CreatedAt={CreatedAt.ToString()}," +
                $"UpdatedAt={UpdatedAt.ToString()}, AuthorId={AuthorId.ToString()}, Likes={Likes}");
        }
    }
}
