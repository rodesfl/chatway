import 'package:chatwaymobile/themes/light.theme.dart';
import 'package:chatwaymobile/utils/chatway.routes.dart';
import 'package:flutter/material.dart';

void main() => runApp(MaterialApp(
      routes: routes,
      initialRoute: '/',
      debugShowCheckedModeBanner: true,
      theme: lightTheme(),
    ));
