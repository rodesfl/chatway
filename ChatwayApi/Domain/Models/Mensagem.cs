using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Domain.Models {
    public class Mensagem {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Conteudo { get; set; }
        public string Remetente { get; set; }
        public string Chat { get; set; }
        public string Path { get; set; }
        public int Tipo { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }
}
