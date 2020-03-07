using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Domain.Models {
    public class Chat {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Atendente { get; set; }
        public string Motorista { get; set; }
        public bool Concluido { get; set; }
        public string Unidade { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }
}
