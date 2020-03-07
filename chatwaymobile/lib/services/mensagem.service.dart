import 'dart:convert';

import 'package:chatwaymobile/models/mensagem.model.dart';
import 'package:chatwaymobile/utils/chatway.http.dart' as chatwayHttp;

class MensagemService {
  static final String _url = "/mensagem";

  static Future<List<Mensagem>> getPadrao() async {
    final response = await chatwayHttp.get("$_url/padrao");
    return _toList(response.body);
  }

  static Future<Mensagem> post(String mensagem) async {
    Mensagem obj = Mensagem(conteudo: mensagem);
    final response = await chatwayHttp.post("$_url/", obj);
    return Mensagem.fromJson(json.decode(response.body));
  }

  static Future<List<Mensagem>> getByChat(String chat) async {
    final response = await chatwayHttp.get("$_url/getbychat/$chat");
    return _toList(response.body);
  }

  static List<Mensagem> _toList(String body) {
    Iterable list = json.decode(body);
    return list.map((mensagem) => Mensagem.fromJson(mensagem)).toList();
  }
}
