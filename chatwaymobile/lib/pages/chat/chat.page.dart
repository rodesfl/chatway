import 'dart:async';
import 'dart:convert';
import 'dart:io';
import 'dart:math';
import 'package:audio_recorder/audio_recorder.dart';
import 'package:chatwaymobile/pages/chat/chat.input.dart';
import 'package:http/http.dart' as http;
import 'package:path/path.dart' as path;
import 'package:async/async.dart';
import 'package:path/path.dart' as p;

import 'package:chatwaymobile/animations/fade.translate.animation.dart';
import 'package:chatwaymobile/models/chat.model.dart';
import 'package:chatwaymobile/models/mensagem.model.dart';
import 'package:chatwaymobile/pages/chat/mensagem.tile.dart';
import 'package:chatwaymobile/services/chat.service.dart';
import 'package:chatwaymobile/services/mensagem.service.dart';
import 'package:chatwaymobile/utils/chatway.signalR.dart';
import 'package:file_picker/file_picker.dart';
import 'package:flutter/material.dart';
import 'package:path_provider/path_provider.dart';
import 'package:signalr_client/signalr_client.dart';
import 'package:chatwaymobile/utils/chatway.http.dart' as chatwayhttp;

class ChatPage extends StatefulWidget {
  Chat chat;
  HubConnection hubConnection;
  final VoidCallback _callback;

  ChatPage(this.chat, this.hubConnection, this._callback) {
    if (hubConnection == null) {
      SignalRConnection.getChathubConnection().then((hub) {
        this.hubConnection = hub;
      });
    }
  }

  @override
  _ChatPageState createState() => _ChatPageState();
}

class _ChatPageState extends State<ChatPage> {
  final TextEditingController _textEditingController = TextEditingController();
  final ScrollController _controller = ScrollController();
  final FocusNode _focusNode = new FocusNode();

  List<Mensagem> _mensagemList = List<Mensagem>();

  String _textoAnterior = "";

  _hubMethods() async {
    if (widget.hubConnection == null) {
      widget.hubConnection = await SignalRConnection.getChathubConnection();
    }
    widget.hubConnection.on(
      "MensagemRecebida",
      (map) {
        if (this.mounted) {
          this._mensagemList.add(Mensagem.fromJson(map[0]));
          setState(() {});
          Timer(Duration(milliseconds: 500),
              () => _controller.jumpTo(_controller.position.maxScrollExtent));
        }
      },
    );
    widget.hubConnection.on("ChatAtendido", (map) {
      setState(() {
        this.widget.chat = Chat.fromJson(map[0]);
      });
    });
    widget.hubConnection.on("ChatFinalizado", (map) {
      this.widget._callback();
      setState(() {
        this.widget.chat.concluido = true;
      });
    });
  }

  @override
  void initState() {
    super.initState();
    _hubMethods();
    _buscarChat();
    _buscarMensagensAnteriores();
    _focusNode.addListener(onFocusChange);
    _textEditingController.addListener(
      () {
        if (_textEditingController.text.length == 0 ||
            _textoAnterior.length == 0) {
          setState(() {});
        }
      },
    );
  }

  _buscarMensagensAnteriores() async {
    List<Mensagem> mensagens = await MensagemService.getByChat(widget.chat.id);
    setState(() {
      this._mensagemList.addAll(mensagens);
    });
  }

  _buscarChat() async {
    this.widget.chat = await ChatService.get(this.widget.chat.id);
    setState(() {});
  }

  void onFocusChange() {
    if (_focusNode.hasFocus) {}
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text("Suporte"),
        centerTitle: true,
      ),
      backgroundColor: Color.fromARGB(255, 226, 240, 255),
      body: SafeArea(
        child: Stack(
          children: <Widget>[
            Center(
              child: Image.asset("assets/logo/waydatasolution.png"),
            ),
            Column(
              children: <Widget>[
                _buildMensagemSuperior(),
                _buildMensagens(),
                Container(
                  child: _buildInput(),
                  padding:
                      EdgeInsets.only(bottom: 8, top: 4, left: 8, right: 8),
                ),
              ],
            )
          ],
        ),
      ),
    );
  }

  Widget _buildMensagemSuperior() {
    if (this.widget.chat.atendente == null) {
      return Container(
        margin: EdgeInsets.only(bottom: 4),
        height: 25,
        width: double.infinity,
        child: Center(
          child: Text(
            "Em breve você será atendido.",
            style: TextStyle(color: Colors.black54),
          ),
        ),
        decoration: BoxDecoration(
          color: Color.fromARGB(255, 232, 146, 149),
          boxShadow: [
            BoxShadow(
              color: Colors.grey,
              blurRadius: 3,
              spreadRadius: 1,
              offset: Offset(0, 0),
            ),
          ],
        ),
      );
    }
    return Container(
      margin: EdgeInsets.only(bottom: 4),
      height: 25,
      width: double.infinity,
      child: Center(
        child: Text(
          this.widget.chat.concluido ? "Suporte Finalizado." : "Em atendimento",
          style: TextStyle(color: Colors.black54),
        ),
      ),
      decoration: BoxDecoration(
        color: this.widget.chat.concluido
            ? Color.fromARGB(255, 255, 241, 226)
            : Color.fromARGB(255, 240, 255, 226),
        boxShadow: [
          BoxShadow(
            color: Colors.grey,
            blurRadius: 3,
            spreadRadius: 1,
            offset: Offset(0, 0),
          ),
        ],
      ),
    );
  }

  Widget _buildMensagens() {
    return Flexible(
        child: ListView.builder(
      controller: _controller,
      padding: EdgeInsets.all(10),
      itemCount: _mensagemList.length,
      itemBuilder: (context, index) {
        return FadeTranslateAnimation(
            0, _mensagemItem(this._mensagemList[index]));
      },
    ));
  }

  Widget _mensagemItem(Mensagem mensagem) {
    return Container(
      margin: EdgeInsets.symmetric(vertical: 4.0),
      padding: EdgeInsets.symmetric(horizontal: 8.0),
      child: Row(
        mainAxisAlignment: mensagem.remetente == widget.chat.motorista
            ? MainAxisAlignment.end
            : MainAxisAlignment.start,
        children: <Widget>[
          MensagemTile(mensagem, this.widget.chat.motorista),
        ],
      ),
    );
  }

  Widget _buildInput() {
    return Container(
      child: Row(
        children: <Widget>[
          Expanded(
              child: ChatInput(!this.widget.chat.concluido, _focusNode,
                  _enviarArquivo, this._textEditingController)),
          _buildEnviarButton(),
        ],
      ),
    );
  }

  Widget _buildEnviarButton() {
    return _textEditingController.text.length > 0
        ? Material(
            child: Container(
              margin: EdgeInsets.symmetric(horizontal: 8.0),
              child: IconButton(
                icon: Icon(
                  Icons.send,
                  color: Colors.white,
                ),
                onPressed: !this.widget.chat.concluido
                    ? () {
                        Mensagem mensagem = Mensagem(
                          chat: this.widget.chat.id,
                          conteudo: _textEditingController.text,
                          remetente: this.widget.chat.motorista,
                          tipo: 1,
                        );
                        this._enviarMensagem(mensagem);
                      }
                    : null,
              ),
            ),
            color: Theme.of(context).primaryColor,
            borderRadius: BorderRadius.circular(10),
          )
        : Material(
            child: Container(
              margin: EdgeInsets.symmetric(horizontal: 8.0),
              child: GestureDetector(
                child: IconButton(
                  icon: Icon(
                    Icons.mic,
                    color: Colors.white,
                  ),
                  onPressed: null,
                ),
                onLongPress: _startRecording,
                onLongPressUp: _stopRecording,
              ),
            ),
            color: Theme.of(context).primaryColor,
            borderRadius: BorderRadius.circular(10),
          );
  }

  void _enviarMensagem(Mensagem mensagem) async {
    widget.hubConnection
        .invoke("EnviarMensagem", args: [mensagem]).then((response) {
      setState(() {
        this._mensagemList.add(Mensagem.fromJson(response));
      });
    });
    this._textEditingController.clear();
  }

  void _startRecording() async {
    Directory dir = await getApplicationDocumentsDirectory();
    var random = Random();
    String pathName =
        p.join(dir.path, (random.nextDouble() * 1000000).toString());
    await AudioRecorder.start(
        path: pathName, audioOutputFormat: AudioOutputFormat.AAC);
  }

  void _stopRecording() async {
    Recording recording = await AudioRecorder.stop();
    _upload(File(recording.path));
  }

  void _enviarArquivo() async {
    File file = await FilePicker.getFile(type: FileType.ANY);
    if (file != null) {
      _upload(file);
    }
  }

  _upload(File file) async {
    // open a bytestream
    var stream = new http.ByteStream(DelegatingStream.typed(file.openRead()));
    // get file length
    var length = await file.length();

    // string to uri
    var uri = chatwayhttp.parse(
        "/mensagem/upload/${this.widget.chat.id}/${this.widget.chat.motorista}");

    // create multipart request
    var request = new http.MultipartRequest("POST", uri);

    // multipart that takes file
    var multipartFile = new http.MultipartFile('file', stream, length,
        filename: path.basename(file.path));

    // add file to multipart
    request.files.add(multipartFile);

    // send
    var response = await request.send();

    // listen for response
    response.stream.transform(utf8.decoder).listen((value) {
      Mensagem mensagem = Mensagem.fromJson(jsonDecode(value));
      print(mensagem.path);
    });
  }
}
