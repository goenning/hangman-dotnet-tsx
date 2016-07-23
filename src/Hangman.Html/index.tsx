import * as React from "react";
import * as ReactDOM from "react-dom";

import { Hello } from "./Components/Hello";

ReactDOM.render(
    <Hello compiler="TypeScript 3" framework="React" />,
    document.getElementById("example")
);