import 'package:flutter/material.dart';

class SimpleListItem extends StatelessWidget {
  final String _text;

  SimpleListItem(this._text);

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: <Widget>[
        Container(
          width: double.infinity,
          padding: EdgeInsets.all(8),
          child: RichText(
            text: TextSpan(
              style: TextStyle(color: Colors.black87, fontSize: 18.0),
              children: <TextSpan>[
                TextSpan(text: _text),
              ],
            ),
          ),
        ),
        Divider()
      ],
    );
  }
}
