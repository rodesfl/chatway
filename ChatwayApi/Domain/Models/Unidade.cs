using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Domain.Models {
    public class Unidade {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Empresa { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }
}
