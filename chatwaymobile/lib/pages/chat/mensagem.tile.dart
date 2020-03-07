import 'package:chatwaymobile/models/mensagem.model.dart';
import 'package:chatwaymobile/pages/chat/mensagem.imagem.dart';
import 'package:flutter/material.dart';
import 'dart:math';

import 'package:intl/intl.dart';

class MensagemTile extends StatelessWidget {
  Mensagem _mensagem;
  String _self;

  MensagemTile(this._mensagem, this._self);

  @override
  Widget build(BuildContext context) {
    Widget _conteudo;

    switch (this._mensagem.tipo) {
      case 1:
        _conteudo = _buildTexto();
        break;
      case 2:
        _conteudo = _buildAudio();
        break;
      case 3:
        _conteudo = _buildImagem();
        break;
      default:
        return Container();
    }

    if (_mensagem.remetente == this._self) {
      final clipped = ClipPath(
        clipper: MensagemClipper(2.5),
        child: ClipRRect(
          borderRadius: BorderRadius.all(Radius.circular(2.5)),
          child: Container(
            constraints:
                BoxConstraints.loose(MediaQuery.of(context).size * 0.8),
            padding: EdgeInsets.fromLTRB(8.0 + 2 * 2.5, 8.0, 8.0, 8.0),
            color: Color.fromARGB(255, 255, 252, 226),
            child: Transform(
              transform: Matrix4.diagonal3Values(-1.0, 1.0, 1.0),
              child: _conteudo,
              alignment: Alignment.center,
            ),
          ),
        ),
      );
      return Transform(
        transform: Matrix4.diagonal3Values(-1.0, 1.0, 1.0),
        child: clipped,
        alignment: Alignment.center,
      );
    }

    final clipped = ClipPath(
      clipper: MensagemClipper(2.5),
      child: ClipRRect(
        borderRadius: BorderRadius.all(Radius.circular(2.5)),
        child: Container(
          constraints: BoxConstraints.loose(MediaQuery.of(context).size * 0.8),
          padding: EdgeInsets.fromLTRB(8.0 + 2 * 2.5, 8.0, 8.0, 8.0),
          color: Colors.white,
          child: _conteudo,
        ),
      ),
    );
    return clipped;
  }

  Widget _buildTexto() {
    return Column(
      mainAxisSize: MainAxisSize.min,
      crossAxisAlignment: CrossAxisAlignment.end,
      children: <Widget>[
        Text(
          this._mensagem.conteudo,
          softWrap: true,
        ),
        Container(
          height: 4,
          width: 2,
        ),
        Text(DateFormat("hh:mm").format(this._mensagem.dataCriacao)),
      ],
    );
  }

  Widget _buildAudio() {}

  Widget _buildImagem() {
    return Column(
      mainAxisSize: MainAxisSize.min,
      crossAxisAlignment: CrossAxisAlignment.end,
      children: <Widget>[
        Container(
          child: MensagemImagem(this._mensagem),
        ),
        Text(DateFormat("hh:mm").format(this._mensagem.dataCriacao)),
      ],
    );
  }
}

class MensagemClipper extends CustomClipper<Path> {
  final double chatRadius;

  MensagemClipper(this.chatRadius);

  @override
  Path getClip(Size size) {
    final path = Path();
    path.lineTo(0.0, chatRadius);
    // path.lineTo(chatRadius, chatRadius + chatRadius / 2);
    final r = chatRadius;
    final angle = 0.785;
    path.conicTo(
      r - r * sin(angle),
      r + r * cos(angle),
      r - r * sin(angle * 0.5),
      r + r * cos(angle * 0.5),
      1,
    );

    final moveIn = 2 * r; // need to be > 2 * r
    path.lineTo(moveIn, r + moveIn * tan(angle));

    path.lineTo(moveIn, size.height - chatRadius);

    path.conicTo(
      moveIn + r - r * cos(angle),
      size.height - r + r * sin(angle),
      moveIn + r,
      size.height,
      1,
    );

    path.lineTo(size.width, size.height);
    path.lineTo(size.width, 0.0);

    path.close();
    return path;
  }

  @override
  bool shouldReclip(CustomClipper<Path> oldClipper) {
    return false;
  }
}
