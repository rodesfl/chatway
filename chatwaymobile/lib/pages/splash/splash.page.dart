import 'dart:convert';

import 'package:chatwaymobile/models/usuario.model.dart';
import 'package:chatwaymobile/pages/home/home.page.dart';
import 'package:chatwaymobile/utils/chatway.http.dart';
import 'package:chatwaymobile/utils/chatway.sharedpreferences.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:shared_preferences/shared_preferences.dart';

class SplashPage extends StatefulWidget {
  @override
  _SplashPageState createState() => _SplashPageState();
}

class _SplashPageState extends State<SplashPage> {
  SharedPreferences _prefs;

  @override
  void initState() {
    super.initState();
    _buscarInformacoes();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Color.fromARGB(255, 89, 96, 226),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            Hero(
              tag: 'waylogo',
              child: CircleAvatar(
                backgroundColor: Colors.transparent,
                radius: 36.0,
                child: Image.asset('assets/logo/waydatasolution.png'),
              ),
            ),
            Padding(
              padding: EdgeInsets.all(8.0),
            ),
            CircularProgressIndicator(
              valueColor: AlwaysStoppedAnimation<Color>(Colors.white),
            ),
          ],
        ),
      ),
    );
  }

  _buscarInformacoes() async {
    this._prefs = await SharedPreferences.getInstance();

    String _identificador = await ChatwaySharedPreferences.getIdentificador();
    if (_identificador != null) {
      get("/auth/loginDispositivo/$_identificador").then((response) {
        if (response.statusCode == 200) {
          this._prefs.setString("usuario", response.body);
          Navigator.pushNamed(context, "/home");
        } else {
          Navigator.pushNamed(context, "/login");
        }
      });
    } else {
      this._prefs.setString("identificador", "4QLKJXIYEK0TTUN");
      _buscarInformacoes();
    }
  }
}
