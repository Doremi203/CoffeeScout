import logo from './logo.svg';
import './App.css';
import {BrowserRouter, Route, Routes} from "react-router-dom";


import Login from "./pages/Login";
import MainPage from "./pages/MainPage";
import Reg from "./pages/Reg";

function App() {
  return (
      <BrowserRouter>
        <Routes>
           <Route path="/" exact Component={Reg} />
            <Route path="/login" exact Component={Login} />
            <Route path="/main" exact Component={MainPage} />
        </Routes>
      </BrowserRouter>
  );
}

export default App;
