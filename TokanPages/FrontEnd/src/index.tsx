import * as React from "react";
import * as ReactDOM from "react-dom";
import { Provider } from "react-redux";
import { ConnectedRouter } from "connected-react-router";
import { createBrowserHistory } from "history";
import { ThemeProvider } from "@material-ui/core";
import theme from './theme/theme';
import CssBaseline from '@material-ui/core/CssBaseline';
import configureStore from "./redux/store";
import App from "./app";

import "./styles/main.scss";

// Create browser history to use in the Redux store
const baseUrl = document.getElementsByTagName("base")[0].getAttribute("href") as string;
const history = createBrowserHistory({ basename: baseUrl });

// Get the application-wide store instance, 
// prepopulating with state from the server where available.
const store = configureStore(history);

ReactDOM.render(
    <Provider store={store}>
        <ConnectedRouter history={history}>
            <ThemeProvider theme={theme}>
                <CssBaseline />
                <App />
            </ThemeProvider>
        </ConnectedRouter>
    </Provider>,
    document.getElementById("root")
);
