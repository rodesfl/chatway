import 'dart:convert';

import 'package:chatwaymobile/animations/slide.route.animation.dart';
import 'package:chatwaymobile/models/chat.model.dart';
import 'package:chatwaymobile/models/mensagem.model.dart';
import 'package:chatwaymobile/pages/chat/chat.page.dart';
import 'package:chatwaymobile/services/chat.service.dart';
import 'package:chatwaymobile/services/mensagem.service.dart';
import 'package:chatwaymobile/utils/chatway.sharedpreferences.dart';
import 'package:chatwaymobile/utils/chatway.signalR.dart';
import 'package:chatwaymobile/widgets/expandablecard/expandablecard.dart';
import 'package:chatwaymobile/widgets/simpleslistitem/simplelistitem.widget.dart';
import 'package:flutter/material.dart';
import 'package:signalr_client/signalr_client.dart';

class HomePage extends StatefulWidget {
  @override
  _HomePageState createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {
  List<Mensagem> _mensagemPadrao = List<Mensagem>();
  bool _loading = false;
  Chat _chat;

  @override
  void initState() {
    _buscarChatAberto();
    _buscarMensagemPadrao();
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text("Home"),
        leading: Container(),
        centerTitle: true,
      ),
      body: this._chat == null
          ? Stack(
              children: <Widget>[
                ExpandableCard(
                  page: Container(),
                  maxHeight: 500,
                  cardBody: <Widget>[
                    Container(
                      height: 350,
                      child: this._mensagemPadrao.length > 0
                          ? ListView.builder(
                              itemCount: _mensagemPadrao.length,
                              itemBuilder: (_, _index) {
                                return GestureDetector(
                                  onTap: () => _mensagemSelecionada(
                                      _mensagemPadrao[_index]),
                                  child: SimpleListItem(
                                      _mensagemPadrao[_index].conteudo),
                                );
                              },
                            )
                          : Image.asset(
                              "assets/icons/no_data.png",
                              width: 200,
                              height: 200,
                            ),
                    ),
                  ],
                  header: RichText(
                    text: TextSpan(
                      style: TextStyle(
                          fontWeight: FontWeight.bold,
                          color: Colors.black54,
                          fontSize: 19.0),
                      children: <TextSpan>[
                        TextSpan(text: "Preciso de ajuda"),
                      ],
                    ),
                  ),
                ),
                this._loading
                    ? Container(
                        alignment: Alignment.bottomCenter,
                        child: CircularProgressIndicator(),
                        margin: EdgeInsets.only(bottom: 16),
                      )
                    : Container(),
              ],
            )
          : Center(
              child: Column(
              mainAxisAlignment: MainAxisAlignment.center,
              children: <Widget>[
                RaisedButton(
                  color: Theme.of(context).primaryColor,
                  child: Text("Abrir chat"),
                  onPressed: _abrirChat,
                ),
                Text("Já existe um chat aberto.")
              ],
            )),
    );
  }

  void _mensagemSelecionada(Mensagem selecionada) async {
    setState(() {
      this._loading = true;
    });
    //Preparação da mensagem
    Mensagem mensagem = Mensagem(conteudo: selecionada.conteudo);
    mensagem.remetente = (await ChatwaySharedPreferences.getUsuario()).id;
    mensagem.tipo = 1;

    HubConnection hubConnection =
        await SignalRConnection.getChathubConnection();

    final response =
        await hubConnection.invoke("EnviarMensagem", args: [mensagem]);
    mensagem = Mensagem.fromJson(json.decode(json.encode(response)));
    Chat chat = Chat(
        id: mensagem.chat, motorista: mensagem.remetente, concluido: false);
    await Navigator.push(
      context,
      SlideLeftRoute(
        page: ChatPage(chat, hubConnection, _buscarChatAberto),
      ),
    );
    _buscarChatAberto();
    setState(() {
      this._loading = false;
    });
  }

  _abrirChat() async {
    SlideLeftRoute route = SlideLeftRoute(
      page: ChatPage(this._chat, null, _buscarChatAberto),
    );
    Navigator.push(context, route);
  }

  _buscarMensagemPadrao() async {
    setState(() {
      this._loading = true;
    });
    this._mensagemPadrao.addAll(await MensagemService.getPadrao());
    this._mensagemPadrao.add(Mensagem(conteudo: "Outros", id: "0"));
    setState(() {
      this._loading = false;
    });
  }

  _buscarChatAberto() async {
    setState(() {
      this._loading = true;
    });
    this._chat = await ChatService.getAberto(
        (await ChatwaySharedPreferences.getUsuario()).id);
    setState(() {
      this._loading = false;
    });
  }
}
