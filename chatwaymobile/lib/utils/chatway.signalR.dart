import 'package:chatwaymobile/environment.dart';
import 'package:chatwaymobile/models/usuario.model.dart';
import 'package:chatwaymobile/utils/chatway.sharedpreferences.dart';
import 'package:signalr_client/signalr_client.dart';

class SignalRConnection {
  static HubConnection _chathubConnection;

  static Future<HubConnection> getChathubConnection() async {
    if (_chathubConnection != null &&
        _chathubConnection.state == HubConnectionState.Connected) {
      return _chathubConnection;
    }

    _chathubConnection =
        HubConnectionBuilder().withUrl(Environment.hubUrl).build();
    _chathubConnection.onclose((error) => print("Connection Closed"));

    await _chathubConnection.start();

    Usuario usuario = await ChatwaySharedPreferences.getUsuario();
    await _chathubConnection.invoke("Autenticar", args: [usuario, "motorista"]);

    return _chathubConnection;
  }
}
