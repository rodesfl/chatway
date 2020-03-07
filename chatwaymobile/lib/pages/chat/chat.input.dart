import 'package:flutter/material.dart';

class ChatInput extends StatefulWidget {
  bool enabled;
  FocusNode focusNode;
  VoidCallback enviarArquivo;
  TextEditingController textEditingController;

  ChatInput(this.enabled, this.focusNode, this.enviarArquivo,
      this.textEditingController);

  @override
  _ChatInputState createState() => _ChatInputState();
}

class _ChatInputState extends State<ChatInput> {
  @override
  Widget build(BuildContext context) {
    return Container(
      margin: EdgeInsets.only(right: 4),
      decoration: BoxDecoration(
        borderRadius: BorderRadius.circular(10),
        color: Colors.white,
      ),
      child: Row(
        children: <Widget>[
          //TextField
          Expanded(
            child: TextField(
              enabled: this.widget.enabled,
              style: TextStyle(fontSize: 18.0),
              controller: this.widget.textEditingController,
              decoration: InputDecoration(
                  hintText: "Digite sua mensagem aqui...",
                  border: OutlineInputBorder(
                    borderRadius: BorderRadius.only(
                      topLeft: Radius.circular(10),
                      bottomLeft: Radius.circular(10),
                    ),
                    borderSide: BorderSide(width: 0, style: BorderStyle.none),
                  ),
                  filled: true,
                  contentPadding:
                      EdgeInsets.symmetric(vertical: 4, horizontal: 12),
                  fillColor: Colors.white),
              focusNode: this.widget.focusNode,
              maxLines: 8,
              minLines: 1,
            ),
          ),

          //Button attach file
          IconButton(
            icon: Icon(
              Icons.attach_file,
              color: Colors.grey,
            ),
            onPressed: this.widget.enabled ? this.widget.enviarArquivo : null,
          ),
        ],
      ),
    );
  }
}
