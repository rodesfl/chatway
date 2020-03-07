import 'dart:convert';

import 'package:chatwaymobile/models/usuario.model.dart';
import 'package:shared_preferences/shared_preferences.dart';

class ChatwaySharedPreferences {
  static Future<Usuario> getUsuario() async {
    return Usuario.fromJson(jsonDecode(await _read("usuario")));
  }

  static Future<String> getIdentificador() async {
    return _read("identificador");
  }

  static Future<String> _read(String key) async {
    final prefs = await SharedPreferences.getInstance();
    return prefs.getString(key);
  }

  static _saveObject(String key, dynamic object) async {
    final prefs = await SharedPreferences.getInstance();
    prefs.setString(key, json.encode(object));
  }

  static _saveString(String key, String value) async {
    final prefs = await SharedPreferences.getInstance();
    prefs.setString(key, value);
  }

  static _remove(String key) async {
    final prefs = await SharedPreferences.getInstance();
    prefs.remove(key);
  }
}
