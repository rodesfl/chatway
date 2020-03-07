class Mensagem {
  String id;
  String conteudo;
  String remetente;
  String chat;
  String path;
  int tipo;
  DateTime dataCriacao;

  Mensagem(
      {this.id,
      this.conteudo,
      this.remetente,
      this.chat,
      this.path,
      this.tipo,
      this.dataCriacao});

  Mensagem.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    conteudo = json['conteudo'];
    remetente = json['remetente'];
    chat = json['chat'];
    path = json['path'];
    tipo = json['tipo'];
    dataCriacao = DateTime.parse(json['dataCriacao']);
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['id'] = this.id;
    data['conteudo'] = this.conteudo;
    data['remetente'] = this.remetente;
    data['chat'] = this.chat;
    data['path'] = this.path;
    data['tipo'] = this.tipo;
    //data['dataCriacao'] = this.dataCriacao;
    return data;
  }
}
