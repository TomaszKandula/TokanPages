import * as React from "react";
import * as ReactDOM from "react-dom";
import { Provider } from "react-redux";
import { ConnectedRouter } from "connected-react-router";
import { createBrowserHistory } from "history";
import { ThemeProvider } from "@material-ui/core";
import theme from "./Theme/theme";
import CssBaseline from "@material-ui/core/CssBaseline";
import * as Sentry from "@sentry/react";
import { Integrations } from "@sentry/tracing";
import configureStore from "./Redux/configureStore";
import App from "./app";

// Create browser history to use in the Redux store
const baseUrl = document.getElementsByTagName("base")[0].getAttribute("href") as string;
const history = createBrowserHistory({ basename: baseUrl });

// Get the application-wide store instance, 
// prepopulating with state from the server where available.
const store = configureStore(history);

// Gather analytics for Sentry
Sentry.init(
{
    dsn: process.env.REACT_APP_SENTRY,
    integrations: [new Integrations.BrowserTracing()],
    tracesSampleRate: 1.0,
});

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
