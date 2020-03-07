using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Utils.Utils;

namespace Domain.Models {
    public class Usuario {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Unidade { get; set; }
        public string Tipo { get; set; }
        public string Dispositivo { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }
}
