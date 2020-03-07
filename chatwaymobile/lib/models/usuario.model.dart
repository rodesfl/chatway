class Usuario {
  String id;
  String nome;
  Null login;
  Null senha;
  String unidade;
  Null tipo;
  String dispositivo;
  DateTime dataCriacao;

  Usuario(
      {this.id,
      this.nome,
      this.login,
      this.senha,
      this.unidade,
      this.tipo,
      this.dispositivo,
      this.dataCriacao});

  Usuario.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    nome = json['nome'];
    login = json['login'];
    senha = json['senha'];
    unidade = json['unidade'];
    tipo = json['tipo'];
    dispositivo = json['dispositivo'];
    dataCriacao = DateTime.parse(json['dataCriacao']);
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['id'] = this.id;
    data['nome'] = this.nome;
    data['login'] = this.login;
    data['senha'] = this.senha;
    data['unidade'] = this.unidade;
    data['tipo'] = this.tipo;
    data['dispositivo'] = this.dispositivo;
    //data['dataCriacao'] = this.dataCriacao;
    return data;
  }
}
