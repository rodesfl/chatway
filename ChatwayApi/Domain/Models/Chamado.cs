using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Domain.Models {
    public class Chamado {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Resumo { get; set; }
        public string Midia { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }
}
