import React, { useState, useEffect } from "react";
import "./App.css";
import TicTacToe from "./components/TicTacToe";

interface AppProps {}

function App({}: AppProps) {

  return (
    <div className="App">
      <TicTacToe />
    </div>
  );
}

export default App;
