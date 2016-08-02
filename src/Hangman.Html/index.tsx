declare var require: any

import * as React from "react";
import * as ReactDOM from "react-dom";

require('bootstrap/dist/css/bootstrap.css');
require('./index.css');
require('./index.html');

import { Game } from "./Components/Game";

ReactDOM.render(
    <Game endpoint="/api" />,
    document.getElementById("bootstrap")
);