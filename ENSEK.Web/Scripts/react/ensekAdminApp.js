import React from "react";
import ReactDOM from "react-dom";
import { Switch, BrowserRouter, Route } from "react-router-dom";

import Accounts from "./components/Accounts";
import NoMatch from "./components/NoMatch";

const EnsekAdminApp = () => {
    return (
        <BrowserRouter>
            <Switch>
                <Route exact path="/" component={Accounts} />
                <Route component={NoMatch} />
            </Switch>
        </BrowserRouter>
    );
};

window.renderEnsekAdminApp = props => {
    ReactDOM.render(
        <EnsekAdminApp />,
        props.container
    );
}