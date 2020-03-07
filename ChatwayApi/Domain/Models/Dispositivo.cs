using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Domain.Models {
    public class Dispositivo {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Modelo { get; set; }
        public string CodigoAtivacao { get; set; }
        public string Identificador { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;

    }
}
