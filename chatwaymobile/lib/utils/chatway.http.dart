import 'dart:convert';

import 'package:chatwaymobile/environment.dart';
import 'package:http/http.dart' as http;

String _urlBase = Environment.apiUrl;

Future<http.Response> get(String mapping, {Map<String, String> headers}) async {
  headers = await _prepararHeader(headers);
  mapping = _prepararMapping(mapping);
  return http.get('$_urlBase$mapping', headers: headers);
}

Future<http.Response> post(String mapping, dynamic body,
    {Map<String, String> headers}) async {
  headers = await _prepararHeader(headers);
  mapping = _prepararMapping(mapping);
  return http.post('$_urlBase$mapping',
      headers: headers, body: jsonEncode(body));
}

Future<http.Response> put(String mapping, String body,
    {Map<String, String> headers}) async {
  headers = await _prepararHeader(headers);
  mapping = _prepararMapping(mapping);
  return http.put('$_urlBase$mapping',
      headers: headers, body: jsonEncode(body));
}

Future<http.Response> delete(String mapping,
    {Map<String, String> headers}) async {
  headers = await _prepararHeader(headers);
  mapping = _prepararMapping(mapping);
  return http.delete('$_urlBase$mapping', headers: headers);
}

Future<Map<String, String>> _prepararHeader(Map<String, String> headers) async {
  if (headers != null) {
    headers.addAll({'Content-Type': 'application/json'});
  } else {
    headers = {'Content-Type': 'application/json'};
  }
  return headers;
}

String _prepararMapping(String mapping) {
  if (mapping.startsWith('/')) {
    mapping = mapping.replaceFirst('/', '');
  }
  return mapping;
}

String getApiUrl() {
  return _urlBase;
}

Uri parse(String mapping) {
  mapping = _prepararMapping(mapping);
  return Uri.parse("$_urlBase$mapping");
}
