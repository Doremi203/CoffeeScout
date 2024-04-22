import {BrowserRouter, Route, Routes} from "react-router-dom";
import Login from "./pages/Login";
import MainPage from "./pages/MainPage";
import Profile from "./pages/Profile";

function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="" exact Component={Login}/>
                <Route path="/main" exact Component={MainPage}/>
                <Route path="/profile" exact Component={Profile}/>
            </Routes>
        </BrowserRouter>
    );
}

export default App;
