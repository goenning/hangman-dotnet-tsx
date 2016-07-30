import * as React from "react";
import * as ReactDOM from "react-dom";

import { Game } from "./Components/Game";

ReactDOM.render(
    <Game endpoint="http://localhost:5000/api" />,
    document.getElementById("bootstrap")
);