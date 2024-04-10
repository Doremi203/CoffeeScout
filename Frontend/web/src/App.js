import {BrowserRouter, Route, Routes} from "react-router-dom";
import Login from "./pages/Login";
import MainPage from "./pages/MainPage";

function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/login" exact Component={Login}/>
                <Route path="/main" exact Component={MainPage}/>
            </Routes>
        </BrowserRouter>
    );
}

export default App;
