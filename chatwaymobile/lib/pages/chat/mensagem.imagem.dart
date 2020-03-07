import 'dart:io';

import 'package:chatwaymobile/models/mensagem.model.dart';
import 'package:flutter/material.dart';
import 'package:network_to_file_image/network_to_file_image.dart';
import 'package:path/path.dart' as p;
import 'package:path_provider/path_provider.dart';

class MensagemImagem extends StatefulWidget {
  final Mensagem _mensagem;

  MensagemImagem(this._mensagem);

  @override
  _MensagemImagemState createState() => _MensagemImagemState();
}

class _MensagemImagemState extends State<MensagemImagem> {
  Image _image;

  @override
  void initState() {
    this._carregarImagem();
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Container(
      child: this._image == null
          ? Container(
              width: double.infinity,
              child: CircularProgressIndicator(),
            )
          : this._image,
    );
  }

  void _carregarImagem() async {
    Directory dir = await getApplicationDocumentsDirectory();
    String pathName = p.join(dir.path, this.widget._mensagem.id);
    NetworkToFileImage fileImage = NetworkToFileImage(
        url: this.widget._mensagem.path, file: File(pathName));
    setState(() {
      this._image = Image(image: fileImage);
    });
  }
}
