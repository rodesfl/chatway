import 'package:chatwaymobile/pages/chat/chat.page.dart';
import 'package:chatwaymobile/pages/home/home.page.dart';
import 'package:chatwaymobile/pages/login/login.page.dart';
import 'package:chatwaymobile/pages/splash/splash.page.dart';
import 'package:flutter/material.dart';

final routes = {
  '/': (BuildContext context) => SplashPage(),
  '/login': (BuildContext context) => LoginPage(),
  '/home': (BuildContext context) => HomePage(),
};
