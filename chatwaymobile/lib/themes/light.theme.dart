import 'package:flutter/material.dart';

const brightness = Brightness.light;
const primaryColor = const Color.fromARGB(255, 87, 155, 236);
const lightColor = const Color(0xFFFFFFFF);
const backgroundColor = const Color(0xFFF5F5F5);

ThemeData lightTheme() {
  return ThemeData(
    brightness: brightness,
    textTheme: TextTheme(
      body1: TextStyle(color: Colors.black),
      body2: TextStyle(
        color: Colors.white,
        fontSize: 24,
      ),
      display4: TextStyle(fontSize: 78),
      button: TextStyle(color: Colors.green),
    ),
    // tabBarTheme:
    // accentIconTheme:
    // accentTextTheme:
    // appBarTheme: AppBarTheme(color: primaryColor),),
    // bottomAppBarTheme:
    buttonTheme: ButtonThemeData(
      buttonColor: Colors.orange,
      textTheme: ButtonTextTheme.primary,
      minWidth: 200,
    ),
    cardTheme: CardTheme(
      elevation: 5,
      color: Colors.indigo,
    ),
    // chipTheme:
    // dialogTheme:
    // floatingActionButtonTheme:
    // iconTheme:
    // inputDecorationTheme:
    // pageTransitionsTheme:
    // primaryIconTheme:
    // primaryTextTheme:
    // sliderTheme:
    primaryColor: primaryColor,
    accentColor: primaryColor,
    //fontFamily: 'Montserrat',
    buttonColor: Color(0xFF00C569),
    // scaffoldBackgroundColor: backgroundColor,
    cardColor: Colors.white,
  );
}
