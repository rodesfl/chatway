import 'dart:convert';

import 'package:chatwaymobile/models/chat.model.dart';
import 'package:chatwaymobile/utils/chatway.http.dart' as chatwayhttp;

class ChatService {
  static final String _url = "/chat";

  static Future<Chat> getAberto(String id) async {
    final response = await chatwayhttp.get("$_url/aberto/$id");
    try {
      return Chat.fromJson(jsonDecode(response.body));
    } catch (e) {
      return null;
    }
  }

  static Future<Chat> get(String id) async {
    final response = await chatwayhttp.get("$_url/$id");
    return Chat.fromJson(jsonDecode(response.body));
  }

  static List<Chat> _toList(String body) {
    Iterable list = json.decode(body);
    return list.map((chat) => Chat.fromJson(chat)).toList();
  }
}
