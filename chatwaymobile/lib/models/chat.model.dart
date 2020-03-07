class Chat {
  String id;
  String atendente;
  String motorista;
  bool concluido;
  String unidade;
  DateTime dataCriacao;

  Chat(
      {this.id,
      this.atendente,
      this.motorista,
      this.concluido,
      this.unidade,
      this.dataCriacao});

  Chat.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    atendente = json['atendente'];
    motorista = json['motorista'];
    concluido = json['concluido'];
    unidade = json['unidade'];
    dataCriacao = DateTime.parse(json['dataCriacao']);
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['id'] = this.id;
    data['atendente'] = this.atendente;
    data['motorista'] = this.motorista;
    data['concluido'] = this.concluido;
    data['unidade'] = this.unidade;
    //data['dataCriacao'] = this.dataCriacao;
    return data;
  }
}
